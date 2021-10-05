//using System.Linq;

namespace Flux.DataStructures.Graphs
{
  /// <summary>Represents a graph using an adjacency list. Can represent a multigraph (i.e. it allows multiple edges to have the same pair of endpoints). Loops can be represented.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)/
  public class AdjacencyList<TKey, TVertexValue, TEdgeValue>
    where TKey : System.IEquatable<TKey>
    where TVertexValue : System.IEquatable<TVertexValue>
    where TEdgeValue : System.IEquatable<TEdgeValue>
  {
    private readonly System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<TEdgeValue>>> m_list = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<TEdgeValue>>>();

    private readonly System.Collections.Generic.Dictionary<TKey, TVertexValue> m_vertexValues = new System.Collections.Generic.Dictionary<TKey, TVertexValue>();

    public System.Collections.Generic.IReadOnlyCollection<TKey> Vertices
      => m_list.Keys;

    /// <summary>Returns the degree of the specified vertex. Returns -1 if not found.</summary>
    public int GetDegree(TKey vertex)
    {
      var degree = 0;

      foreach (var (source, target, value) in GetEdges())
        if (source.Equals(vertex) || target.Equals(vertex))
          degree++;

      return degree;
    }
    /// <summary>Creates a new sequence of all vertices that are edge destinations from the specified vertex.</summary>
    public System.Collections.Generic.IEnumerable<TKey> GetNeighbors(TKey vertex)
      => m_list[vertex].Keys;
    /// <summary>Determines whether an edge exists from source to target.</summary>
    public bool IsAdjacent(TKey source, TKey target)
      => m_list.ContainsKey(source) && m_list[source].ContainsKey(target) && m_list[source][target].Count > 0;

    /// <summary>Adds a vertex with a value to the graph.</summary>
    public void AddVertex(TKey vertex, TVertexValue value)
    {
      if (!m_list.ContainsKey(vertex))
      {
        m_list.Add(vertex, new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<TEdgeValue>>());

        m_vertexValues.Add(vertex, value);
      }
    }
    /// <summary>Adds a vertex with the default value to the graph.</summary>
    public void AddVertex(TKey vertex)
      => AddVertex(vertex, default!);
    /// <summary>Determins whether a vertex exists in the graph.</summary>
    public bool ContainsVertex(TKey vertex)
      => m_list.ContainsKey(vertex);
    /// <summary>Removes a vertex from the graph.</summary>
    public void RemoveVertex(TKey vertex)
    {
      if (m_list.ContainsKey(vertex))
      {
        m_vertexValues.Remove(vertex); // Remove the value of the vertex.

        foreach (var kvp in m_list)
          if (kvp.Value is var targets && targets.ContainsKey(vertex))
            targets.Remove(vertex); // Remove any vertex as a target.

        m_list.Remove(vertex); // Remove vertex as a source.
      }
    }

    /// <summary>Tries to get the value for the specified vertex and returns whether it succeeded.</summary>
    public bool TryGetVertexValue(TKey vertex, out TVertexValue value)
    {
      if (m_vertexValues.ContainsKey(vertex))
      {
        value = m_vertexValues[vertex];
        return true;
      }
      else
      {
        value = default!;
        return false;
      }
    }
    /// <summary>Tries to set the value for the specified vertex and returns whether it succeeded.</summary>
    public bool TrySetVertexValue(TKey vertex, TVertexValue value)
    {
      if (m_vertexValues.ContainsKey(vertex))
        m_vertexValues[vertex] = value;
      else
        m_vertexValues.Add(vertex, value);

      return true;
    }

    /// <summary>Adds a directed edge with a value to the graph.</summary>
    public void AddEdge(TKey source, TKey target, TEdgeValue value)
    {
      if (m_list.ContainsKey(source) && m_list.ContainsKey(target))
      {
        if (m_list[source].ContainsKey(target))
          m_list[source][target].Add(value);
        else // No matching endpoint exists, so we add it.
          m_list[source].Add(target, new System.Collections.Generic.List<TEdgeValue>() { value });
      }
    }
    /// <summary>Adds a directed edge with the default value to the graph.</summary>
    public void AddEdge(TKey source, TKey target)
      => AddEdge(source, target, default!);
    /// <summary>Adds a looped edge with a value to the graph.</summary>
    public void AddEdgeAsLoop(TKey vertex, TEdgeValue value)
      => AddEdge(vertex, vertex, value);
    /// <summary>Adds a undirected edge with a value to the graph.</summary>
    public void AddEdgeAsUndirected(TKey source, TKey target, TEdgeValue value)
    {
      AddEdge(source, target, value);
      AddEdge(target, source, value);
    }
    /// <summary>Determines whether an edge exists in the graph.</summary>
    public bool ContainsEdge(TKey source, TKey target)
      => ContainsVertex(source) && m_list[source].ContainsKey(target);
    /// <summary>Determines whether an edge with the specified value exists in the graph.</summary>
    public bool ContainsEdge(TKey source, TKey target, TEdgeValue value)
      => ContainsEdge(source, target) && m_list[source][target].Contains(value);
    /// <summary>Removes the edge matching the specified value. If no such value is found, nothing is removed.</summary>
    public void RemoveEdge(TKey source, TKey target, TEdgeValue value)
    {
      if (ContainsEdge(source, target))
      {
        m_list[source][target].Remove(value);

        if (m_list[source][target].Count == 0)
        {
          m_list[source].Remove(target);

          if (m_list[source].Count == 0)
            m_list.Remove(source);
        }
      }
    }
    /// <summary>Removes the edge regardless of the number of weights it contains.</summary>
    public void RemoveEdge(TKey source, TKey target)
      => RemoveEdge(source, target, default!);

    /// <summary>Tries to get the values for the specified edge and returns whether it succeeded.</summary>
    public bool TryGetEdgeValues(TKey source, TKey target, out System.Collections.Generic.List<TEdgeValue> list)
    {
      if (ContainsEdge(source, target))
      {
        list = m_list[source][target];
        return true;
      }

      list = default!;
      return false;
    }
    /// <summary>Tries to set the values for the specified edge and returns whether it succeeded.</summary>
    public bool TrySetEdgeValues(TKey source, TKey target, System.Collections.Generic.List<TEdgeValue> list)
    {
      if (ContainsEdge(source, target))
      {
        m_list[source][target] = list ?? throw new System.ArgumentNullException(nameof(list));
        return true;
      }

      return false;
    }

    /// <summary>Creates a new sequence with the shortest path tree, i.e. the shortest paths from the specified origin vertex to all reachable vertices.</summary>
    /// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
    public System.Collections.Generic.IEnumerable<(TKey destination, double distance)> GetDijkstraShortestPathTree(TKey origin, System.Func<TEdgeValue, double> distanceSelector)
    {
      var vertices = System.Linq.Enumerable.ToList(Vertices);

      var distances = System.Linq.Enumerable.ToDictionary(vertices, v => v, v => v.Equals(origin) ? 0 : double.PositiveInfinity);

      var edges = System.Linq.Enumerable.ToList(GetEdges()); // Cache edges, because we need it while there are available distances.

      while (System.Linq.Enumerable.Any(distances)) // As long as there are nodes available.
      {
        var shortest = System.Linq.Enumerable.First(System.Linq.Enumerable.OrderBy(distances, v => v.Value)); // Get the node with the shortest distance.

        if (shortest.Value < double.PositiveInfinity) // If the distance to the node is less than infinity, it was reachable so it should be returned.
          yield return (shortest.Key, shortest.Value);

        distances.Remove(shortest.Key); // This node is now final, so remove it.

        foreach (var (source, target, value) in System.Linq.Enumerable.Where(edges, e => e.source.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
        {
          if (distances.TryGetValue(target, out var distanceToEdgeTarget))
          {
            var distanceViaShortest = shortest.Value + distanceSelector(value); // Distance via the current node.

            if (distanceViaShortest < distanceToEdgeTarget) // If the distance via the current node is shorter than the currently recorded distance, replace it.
              distances[target] = distanceViaShortest;
          }
        }
      }
    }

    /// <summary>Creates a new sequence with all existing edges.</summary>
    public System.Collections.Generic.IEnumerable<(TKey source, TKey target, TEdgeValue value)> GetEdges()
    {
      foreach (var source in m_list.Keys)
        foreach (var target in m_list[source].Keys)
          foreach (var value in m_list[source][target])
            yield return (source, target, value);
    }

    /// <summary>Creates a new sequence with all vertices and their respective value.</summary>
    public System.Collections.Generic.IEnumerable<(TKey key, TVertexValue value)> GetVerticesWithValue()
      => System.Linq.Enumerable.Select(m_list.Keys, vk => (vk, m_vertexValues[vk]));
    /// <summary>Creates a new sequence with all vertices and their respective value and degree.</summary>
    public System.Collections.Generic.IEnumerable<(TKey key, TVertexValue value, int degree)> GetVerticesWithValueAndDegree()
      => System.Linq.Enumerable.Select(m_list.Keys, vk => (vk, m_vertexValues[vk], GetDegree(vk)));

    #region Object overrides.
    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();
      var index = 0;
      foreach (var edge in GetEdges())
        sb.AppendLine($"#{++index}: {edge}");
      sb.Insert(0, $"<{GetType().Name}: ({Vertices.Count} vertices, {index} edges)>{System.Environment.NewLine}");
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

