using System.Linq;

namespace Flux.DataStructures.Graphs
{
  /// <summary>Represents a graph using an adjacency list. Can represent a multigraph (i.e. it allows multiple edges to have the same pair of endpoints). Loops can be represented.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)/
  public class DigraphAdjacentList<TKey, TVertexValue, TEdgeValue>
    : IDigraph<TKey, TVertexValue, TEdgeValue>, IDigraphMulti<TKey, TVertexValue, TEdgeValue>
    where TKey : notnull
    where TEdgeValue : System.IEquatable<TEdgeValue>
  {
    private readonly System.Collections.Generic.Dictionary<TKey, TVertexValue> m_vertexValues = new System.Collections.Generic.Dictionary<TKey, TVertexValue>();

    private readonly System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<TEdgeValue>>> m_list = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<TEdgeValue>>>();

    public int GetDegree(TKey vertex)
    {
      var degree = 0;

      foreach (var (source, target, value) in GetEdges())
        if (source.Equals(vertex) || target.Equals(vertex))
          degree++;

      return degree;
    }
    public System.Collections.Generic.IEnumerable<TKey> GetNeighbors(TKey vertex)
      => m_list[vertex].Keys;
    public bool IsAdjacent(TKey source, TKey target)
      => m_list.ContainsKey(source) && m_list[source].ContainsKey(target) && m_list[source][target].Count > 0;
    public bool IsAdjacent(TKey source, TKey target, TEdgeValue value)
      => m_list.ContainsKey(source) && m_list[source].ContainsKey(target) && m_list[source][target].Contains(value);
    public bool IsLoop(TKey vertex)
      => m_list.ContainsKey(vertex) && m_list[vertex].ContainsKey(vertex) && GetEdgeValues(vertex, vertex).Count > 0;

    public void AddVertex(TKey vertex)
    {
      if (!m_list.ContainsKey(vertex))
      {
        m_list.Add(vertex, new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<TEdgeValue>>());
      }
      else throw new System.ArgumentException($"The vertex <{vertex}> already exist.");
    }
    public void AddVertex(TKey vertex, TVertexValue value)
    {
      AddVertex(vertex);
      SetVertexValue(vertex, value);
    }
    public bool ContainsVertex(TKey vertex)
      => m_list.ContainsKey(vertex);
    public void RemoveVertex(TKey vertex)
    {
      if (m_list.ContainsKey(vertex))
      {
        foreach (var kvp in m_list)
          if (kvp.Value is var targets && targets.ContainsKey(vertex))
            targets.Remove(vertex); // Remove any vertex as a target.

        m_list.Remove(vertex); // Remove vertex as a source.
      }
    }

    public TVertexValue GetVertexValue(TKey vertex)
      => m_vertexValues.ContainsKey(vertex) ? m_vertexValues[vertex] : default!;
    public void SetVertexValue(TKey vertex, TVertexValue value)
      => m_vertexValues[vertex] = value;

    public System.Collections.Generic.IEnumerable<TKey> GetVertices()
      => m_list.Keys;

    public void AddEdge(TKey source, TKey target)
    {
      if (ContainsVertex(source) && ContainsVertex(target))
        if (!m_list[source].ContainsKey(target))
          m_list[source].Add(target, new System.Collections.Generic.List<TEdgeValue>());
    }
    public void AddEdge(TKey source, TKey target, TEdgeValue value)
    {
      AddEdge(source, target);
      AddEdgeValue(source, target, value);
    }
    public bool ContainsEdge(TKey source, TKey target)
      => m_list.ContainsKey(source) && m_list[source].ContainsKey(target) && m_list[source][target].Count > 0;
    public void RemoveEdge(TKey source, TKey target)
    {
      if (m_list.ContainsKey(source) && m_list[source].ContainsKey(target))
        m_list[source].Remove(target);
    }

    /// <summary>Adds a value to an existing edge.</summary>
    public void AddEdgeValue(TKey source, TKey target, TEdgeValue value)
    {
      var ev = GetEdgeValues(source, target);
      ev.Add(value);
      SetEdgeValues(source, target, ev);
    }
    /// <summary>Determines whether an edge has the specified value.</summary>
    public bool ContainsEdgeValue(TKey source, TKey target, TEdgeValue value)
      => GetEdgeValues(source, target).Contains(value);
    /// <summary>Removes a value from an existing edge.</summary>
    public bool RemoveEdgeValue(TKey source, TKey target, TEdgeValue value)
    {
      var ev = GetEdgeValues(source, target);
      var rv = ev.Remove(value);
      SetEdgeValues(source, target, ev);
      return rv;
    }
    /// <summary>Removes the specified values from an existing edge. Values that do not exist, are ignored.</summary>
    public void RemoveEdgeValues(TKey source, TKey target, params TEdgeValue[] values)
    {
      var ev = GetEdgeValues(source, target);
      for (var index = values.Length - 1; index >= 0; index--)
        ev.Remove(values[index]);
      SetEdgeValues(source, target, ev);
    }

    /// <summary>Returns all edge values (edges) between <paramref name="source"/> and <paramref name="target"/>.</summary>
    public System.Collections.Generic.List<TEdgeValue> GetEdgeValues(TKey source, TKey target)
      => m_list[source][target];
    /// <summary>Replaces all edge values (edges) with the specified values.</summary>
    public void SetEdgeValues(TKey source, TKey target, System.Collections.Generic.List<TEdgeValue> list)
      => m_list[source][target] = list ?? throw new System.ArgumentNullException(nameof(list));

    public System.Collections.Generic.IEnumerable<(TKey source, TKey target, TEdgeValue value)> GetEdges()
    {
      foreach (var source in m_list.Keys)
        foreach (var target in m_list[source].Keys)
          foreach (var value in m_list[source][target])
            yield return (source, target, value);
    }

    #region Object overrides.
    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();
      var index = 0;
      foreach (var edge in GetEdges())
        sb.AppendLine($"#{++index}: {edge}");
      sb.Insert(0, $"<{GetType().Name}: ({GetVertices().Count()} vertices, {index} edges)>{System.Environment.NewLine}");
      return sb.ToString();
    }
    #endregion Object overrides.

    //public System.Collections.Generic.List<Edge<TPoint, TWeight>> KruskalMinimumSpanningTree()
    //{
    //  var mst = new System.Collections.Generic.List<Edge<TPoint, TWeight>>();

    //  var vertexes = Edges.Keys;
    //  // Edges by increasing order of weight
    //  var edgesByWeight = Edges.Values.SelectMany(e => e).OrderBy(e => e.Weight).ToList();

    //  // Use a Disjoint Set to keep track of the connected components
    //  var spanningTreeUnion = new UnionFind.QuickUnion<TPoint>(vertexes);

    //  // A minimum spanning tree has V–1 edges where V is the number of vertices in the given graph
    //  // Greedy strategy: pick the smallest weight edge that does not cause a cycle in the MST constructed so far
    //  int minSpanningTreeCount = 0;
    //  for (var i = 0; i < edgesByWeight.Count && minSpanningTreeCount < vertexes.Count - 1; i++)
    //  {
    //    var edge = edgesByWeight[i];

    //    var sourceSubset = spanningTreeUnion.Find(edge.Source);
    //    var targetSubset = spanningTreeUnion.Find(edge.Target);

    //    if (sourceSubset == targetSubset) // will cause a cycle
    //      continue;

    //    spanningTreeUnion.Union(spanningTreeUnion.Values[sourceSubset], spanningTreeUnion.Values[targetSubset]);
    //    mst.Add(edge);

    //    minSpanningTreeCount++;
    //  }

    //  return mst;
    //}

    //public System.Collections.Generic.List<Edge<TPoint>> PrimsMinimumSpanningTree()
    //{
    //  var mst = new System.Collections.Generic.Dictionary<TPoint, Edge<TPoint>>();

    //  if (!AdjacencyList.Any())
    //    return mst.Values.ToList();

    //  var vertexToMinCost = new System.Collections.Generic.Dictionary<TPoint, PathCost>();

    //  // Min Heap storing min cost of the spanning tree
    //  var minHeapNodes = new IndexedHeap<PathCost>(
    //      new System.Collections.Generic.List<PathCost> {
    //                (vertexToMinCost[AdjacencyList.Keys.First()] = new PathCost(AdjacencyList.Keys.First(), 0))
    //  }, (p1, p2) => p1.cost.CompareTo(p2.cost));

    //  foreach (var vertex in AdjacencyList.Keys.Skip(1))
    //    minHeapNodes.Push(vertexToMinCost[vertex] = new PathCost(vertex, int.MaxValue));

    //  // Traverse the node by min cost
    //  // Greedy strategy: Select the edge that minimize the cost for reaching node from its neighbors
    //  while (minHeapNodes.Any())
    //  {
    //    var minNode = minHeapNodes.Pop();

    //    // Visit all the neighbors and update the corresponding cost
    //    foreach (var edge in AdjacencyList[minNode.vertex])
    //    {
    //      // Check that the current cost of reaching the adjacent node is less than the current one
    //      if (minHeapNodes.Contains(vertexToMinCost[edge.Target]) && edge.weight < vertexToMinCost[edge.destination].cost)
    //      {
    //        vertexToMinCost[edge.Target].cost = edge.Weight;
    //        mst[edge.Target] = edge;

    //        // Sift up the heap starting from the current index (log n)
    //        minHeapNodes.SiftUp(fromIndex: minHeapNodes.IndexOf(vertexToMinCost[edge.destination]));
    //      }
    //    }
    //  }

    //  return mst.Values.ToList();
    //}

    //public System.Collections.Generic.List<Edge<TPoint, TWeight>> DijkstraShortestPath(TPoint source, TPoint target = default)
    //{
    //  var sp = new System.Collections.Generic.Dictionary<TPoint, Edge<TPoint, TWeight>>();

    //  if (!AdjacencyList.Any())
    //    return sp.Values.ToList();

    //  var vertexToPathCost = new System.Collections.Generic.Dictionary<TPoint, PathCost>();

    //  // Min Heap storing accumulated min cost for reaching target node greedily
    //  var minHeapNodes = new IndexedHeap<PathCost>(
    //      new System.Collections.Generic.List<PathCost> { (vertexToPathCost[source] = new PathCost(source, 0)) },
    //      compareFunc: (p1, p2) => p1.cost.CompareTo(p2.cost));

    //  foreach (var vertex in AdjacencyList.Keys)
    //  {
    //    if (vertex == source)
    //      continue;

    //    minHeapNodes.Push(vertexToPathCost[vertex] = new PathCost(vertex, int.MaxValue));
    //  }

    //  // Greedy strategy: Select the path that minimized the accumulated cost up to this node
    //  // Traverse the node by min cost
    //  while (minHeapNodes.Any())
    //  {
    //    var minNode = minHeapNodes.Pop();

    //    // Visit all the neighbors and update the corresponding cost
    //    foreach (var edge in AdjacencyList[minNode.vertex])
    //    {
    //      // Check that the current cost of reaching the adjacent node is less than the current one
    //      if (minHeapNodes.Contains(vertexToPathCost[edge.Target]) && minNode.cost + edge.Weight < vertexToPathCost[edge.Target].cost)
    //      {
    //        vertexToPathCost[edge.Target].cost = minNode.cost + edge.Weight;
    //        sp[edge.Target] = edge;

    //        // Sift up the heap starting from the current index (log n)
    //        minHeapNodes.SiftUp(fromIndex: minHeapNodes.IndexOf(vertexToPathCost[edge.Target]));
    //      }
    //    }

    //    if (minNode.vertex.Equals(target))
    //      break;
    //  }

    //  return sp.Values.ToList();
    //}
  }
}

/*
      // Adjacent List.

      var al = new Flux.Collections.Generic.Graph.AdjacentList<char, int>();

      al.AddVertex('a');
      al.AddVertex('b');
      al.AddVertex('c');
      al.AddVertex('d');

      //g.AddDirectedEdge('a', 'b', 1);
      //g.AddDirectedEdge('a', 'c', 1);
      //g.AddDirectedEdge('b', 'a', 1);
      //g.AddDirectedEdge('b', 'c', 1);
      //g.AddDirectedEdge('c', 'a', 1);
      //g.AddDirectedEdge('c', 'b', 1);
      //g.AddDirectedEdge('c', 'd', 1);
      //g.AddDirectedEdge('d', 'c', 1);

      al.AddDirectedEdge('a', 'b', 2);
      al.AddUndirectedEdge('a', 'c', 1);
      al.AddUndirectedEdge('b', 'c', 1);
      al.AddUndirectedEdge('c', 'd', 1);

      al.RemoveUndirectedEdge('c', 'b', 1);

      System.Console.WriteLine(al.ToString());

      foreach (var edge in al.GetEdges())
        System.Console.WriteLine(edge);
 */

///// <summary>
///// Helper data structure for Prim and Dijstra
///// </summary>
//public class PathCost<TValue, TWeight>
//  where TValue : System.IEquatable<TWeight>
//  where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
//{
//  public Vertex<TValue> m_vertex;
//  public TWeight m_cost;

//  public PathCost(Vertex<TValue> vertex, TWeight cost)
//  {
//    m_vertex = vertex;
//    m_cost = cost;
//  }

//  public int CompareTo(object obj)
//    => obj is PathCost<TValue, TWeight> pathCost ? m_cost.CompareTo(pathCost.m_cost) : default;
//}

