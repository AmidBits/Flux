﻿using System.Linq;

namespace Flux.DataStructures.Graph
{
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

  /// <summary>Represents a graph using an adjacency list. Limited to two edges per vertex pair (one weight per direction).</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)/
  public class AdjacentListTypical<TVertex, TValue>
    : IGraphTypical<TVertex, TValue>
    where TVertex : System.IEquatable<TVertex>
    where TValue : System.IEquatable<TValue>
  {
    private readonly OrderedDictionary<TVertex, OrderedDictionary<TVertex, System.Collections.Generic.List<TValue>>> m_data
      = new OrderedDictionary<TVertex, OrderedDictionary<TVertex, System.Collections.Generic.List<TValue>>>();

    public bool IsAdjacent(TVertex source, TVertex target)
      => m_data.ContainsKey(source) && m_data[source].ContainsKey(target);
    public bool IsAdjacent(TVertex source, TVertex target, TValue value)
      => m_data.ContainsKey(source) && m_data[source].ContainsKey(target) && m_data[source][target].Contains(value);

    public System.Collections.Generic.IEnumerable<TVertex> GetNeighbors(TVertex source)
      => m_data[source].Keys;

    public void AddVertex(TVertex vertex, TValue value)
    {
      if (!m_data.ContainsKey(vertex))
        m_data.Add(vertex, new OrderedDictionary<TVertex, System.Collections.Generic.List<TValue>>());
    }
    public void AddVertex(TVertex vertex)
      => AddVertex(vertex, default!);
    public void RemoveVertex(TVertex vertex)
    {
      if (m_data.ContainsKey(vertex))
      {
        foreach (var kvp in m_data)
          if (kvp.Value.ContainsKey(vertex))
            kvp.Value.Remove(vertex); // Remove any vertex as a target.

        m_data.Remove(vertex); // Remove vertex as a source.
      }
    }

    public void AddEdge(TVertex source, TVertex target, TValue value)
    {
      AddVertex(source);
      AddVertex(target);

      if (!m_data[source].ContainsKey(target))
        m_data[source].Add(target, new System.Collections.Generic.List<TValue>());

      m_data[source][target].Add(value);
    }
    public void AddEdge(TVertex source, TVertex target)
      => AddEdge(source, target, default!);

    public void RemoveEdge(TVertex source, TVertex target, TValue value)
    {
      if (m_data.ContainsKey(source) && m_data[source].ContainsKey(target) && m_data[source][target].Contains(value))
      {
        m_data[source][target].Remove(value);

        if (!m_data[source][target].Any())
          m_data[source].Remove(target);
      }
    }
    public void RemoveEdge(TVertex source, TVertex target)
    {
      if (m_data.ContainsKey(source) && m_data[source].ContainsKey(target))
        m_data[source].Remove(target);
    }

    public System.Collections.Generic.IEnumerable<(TVertex vertex, int degree)> GetVertices()
    //=> m_data.Select(kvp => (kvp.Key, kvp.Value.Sum(kvp => kvp.Value.Count) + m_data.Sum(kvpSub => kvpSub.Value.ContainsKey(kvp.Key) ? kvpSub.Value[kvp.Key].Count : 0)));
    {
      foreach (var vertex in m_data.Keys)
        yield return (vertex, GetEdges().Sum(e => e.source.Equals(vertex) || e.target.Equals(vertex) ? e.source.Equals(e.target) ? 2 : 1 : 0));
    }
    public System.Collections.Generic.IEnumerable<(TVertex source, TVertex target, TValue value)> GetEdges()
    {
      foreach (var source in m_data.Keys)
        foreach (var target in m_data[source].Keys)
          foreach (var value in m_data[source][target])
            yield return (source, target, value);
    }

    // Overrides.
    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();
      var index = 0;
      foreach (var edge in GetEdges())
        sb.AppendLine($"#{++index}: {edge}");
      sb.Insert(0, $"<{nameof(AdjacentListTypical<TVertex, TValue>)}: ({GetVertices().Count()} vertices, {index} edges)>{System.Environment.NewLine}");
      return sb.ToString();
    }

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