﻿//namespace Flux.DataStructures.Graphs
//{
//  /// <summary>Represents a graph using an adjacency list. Can represent a multigraph (i.e. it allows multiple edges to have the same pair of endpoints). Loops can be represented.</summary>
//  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
//  /// https://www.tutorialspoint.com/representation-of-graphs
//  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
//  /// https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)/
//  public class AdjacencyList<TKey, TVertexValue, TEdgeValue>
//    : IGraphVertexValue<TKey, TVertexValue>, IGraphDirectedMulti<TKey, TEdgeValue>
//    where TKey : System.IEquatable<TKey>
//    where TVertexValue : System.IEquatable<TVertexValue>
//    where TEdgeValue : System.IEquatable<TEdgeValue>
//  {
//    #region Graph storage
//    private readonly System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<TEdgeValue>>> m_list = new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<TEdgeValue>>>();

//    private readonly System.Collections.Generic.Dictionary<TKey, TVertexValue> m_vertexValues = new System.Collections.Generic.Dictionary<TKey, TVertexValue>();
//    #endregion Graph storage

//    // IGraphCommon<>
//    public int GetDegree(TKey key)
//    {
//      var degree = 0;

//      foreach (var (source, target, value) in GetDirectedEdges())
//        if (source.Equals(key) || target.Equals(key))
//          degree += source.Equals(target) ? 2 : 1;

//      return degree;
//    }
//    public System.Collections.Generic.IEnumerable<TKey> GetNeighbors(TKey key)
//      => m_list[key].Keys;
//    public bool IsAdjacent(TKey keySource, TKey keyTarget)
//      => m_list.ContainsKey(keySource) && m_list[keySource].ContainsKey(keyTarget) && m_list[keySource][keyTarget].Count > 0;

//    // IGraphVertex<>
//    public bool AddVertex(TKey vertex)
//      => AddVertex(vertex, default!);
//    public bool ContainsVertex(TKey vertex)
//      => m_list.ContainsKey(vertex);
//    public System.Collections.Generic.IEnumerable<TKey> GetVertices()
//      => m_list.Keys;
//    public bool RemoveVertex(TKey vertex)
//    {
//      if (m_list.ContainsKey(vertex))
//      {
//        m_vertexValues.Remove(vertex); // Remove the value of the vertex.

//        foreach (var kvp in m_list)
//          if (kvp.Value is var targets && targets.ContainsKey(vertex))
//            targets.Remove(vertex); // Remove any vertex as a target.

//        m_list.Remove(vertex); // Remove vertex as a source.

//        return true;
//      }

//      return false;
//    }

//    // IGraphVertexValue<>
//    public bool AddVertex(TKey vertex, TVertexValue value)
//    {
//      if (!m_list.ContainsKey(vertex))
//      {
//        m_list.Add(vertex, new System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<TEdgeValue>>());

//        m_vertexValues.Add(vertex, value);

//        return true;
//      }

//      return false;
//    }
//    public bool IsVertexValueEqualTo(TKey vertex, TVertexValue value)
//      => TryGetVertexValue(vertex, out var vertexValue) && vertexValue.Equals(value);
//    public bool TryGetVertexValue(TKey vertex, out TVertexValue value)
//    {
//      if (m_vertexValues.ContainsKey(vertex))
//      {
//        value = m_vertexValues[vertex];
//        return true;
//      }
//      else
//      {
//        value = default!;
//        return false;
//      }
//    }
//    public bool TrySetVertexValue(TKey vertex, TVertexValue value)
//    {
//      if (m_vertexValues.ContainsKey(vertex))
//        m_vertexValues[vertex] = value;
//      else
//        m_vertexValues.Add(vertex, value);

//      return true;
//    }

//    // IGraphDirected<>
//    public System.Collections.Generic.IEnumerable<(TKey keySource, TKey keyTarget, TEdgeValue value)> GetDirectedEdges()
//    {
//      foreach (var source in m_list.Keys)
//        foreach (var target in m_list[source].Keys)
//          foreach (var value in m_list[source][target])
//            yield return (source, target, value);
//    }
//    // IGraphDirectedMulti<>
//    public bool AddMultiDirectedEdge(TKey source, TKey target, TEdgeValue value)
//    {
//      if (m_list.ContainsKey(source) && m_list.ContainsKey(target))
//      {
//        if (m_list[source].ContainsKey(target))
//          m_list[source][target].Add(value);
//        else // No matching endpoint exists, so we add it.
//          m_list[source].Add(target, new System.Collections.Generic.List<TEdgeValue>() { value });

//        return true;
//      }

//      return false;
//    }
//    public bool ContainsMultiDirectedEdge(TKey source, TKey target)
//      => m_list.ContainsKey(source) && m_list[source].ContainsKey(target);
//    public bool ContainsMultiDirectedEdge(TKey source, TKey target, TEdgeValue value)
//      => ContainsMultiDirectedEdge(source, target) && m_list[source][target].Contains(value);
//    public bool RemoveMultiDirectedEdge(TKey source, TKey target, TEdgeValue value)
//    {
//      if (ContainsMultiDirectedEdge(source, target, value))
//      {
//        var rv = m_list[source][target].Remove(value);

//        if (m_list[source][target].Count == 0)
//          m_list[source].Remove(target);

//        return rv;
//      }

//      return false;
//    }
//    public bool RemoveMultiDirectedEdge(TKey source, TKey target)
//    {
//      if (ContainsMultiDirectedEdge(source, target))
//        return m_list[source].Remove(target);

//      return false;
//    }
//    public bool TryGetMultiDirectedEdgeValues(TKey source, TKey target, out System.Collections.Generic.List<TEdgeValue> list)
//    {
//      if (ContainsMultiDirectedEdge(source, target))
//      {
//        list = m_list[source][target];
//        return true;
//      }

//      list = default!;
//      return false;
//    }
//    public bool TrySetMultiDirectedEdgeValues(TKey source, TKey target, System.Collections.Generic.List<TEdgeValue> list)
//    {
//      if (ContainsMultiDirectedEdge(source, target))
//      {
//        m_list[source][target] = list ?? throw new System.ArgumentNullException(nameof(list));
//        return true;
//      }

//      return false;
//    }

//    #region Object overrides.
//    public override string ToString()
//    {
//      var sb = new System.Text.StringBuilder();
//      var edgeCount = 0;
//      foreach (var edge in GetDirectedEdges())
//        sb.AppendLine($"#{++edgeCount}: {edge}");
//      sb.Insert(0, $"<{GetType().Name}: ({m_list.Keys.Count} vertices, {edgeCount} edges)>{System.Environment.NewLine}");
//      return sb.ToString();
//    }
//    #endregion Object overrides.

//    //public System.Collections.Generic.List<Edge<TPoint, TWeight>> KruskalMinimumSpanningTree()
//    //{
//    //  var mst = new System.Collections.Generic.List<Edge<TPoint, TWeight>>();

//    //  var vertexes = Edges.Keys;
//    //  // Edges by increasing order of weight
//    //  var edgesByWeight = Edges.Values.SelectMany(e => e).OrderBy(e => e.Weight).ToList();

//    //  // Use a Disjoint Set to keep track of the connected components
//    //  var spanningTreeUnion = new UnionFind.QuickUnion<TPoint>(vertexes);

//    //  // A minimum spanning tree has V–1 edges where V is the number of vertices in the given graph
//    //  // Greedy strategy: pick the smallest weight edge that does not cause a cycle in the MST constructed so far
//    //  int minSpanningTreeCount = 0;
//    //  for (var i = 0; i < edgesByWeight.Count && minSpanningTreeCount < vertexes.Count - 1; i++)
//    //  {
//    //    var edge = edgesByWeight[i];

//    //    var sourceSubset = spanningTreeUnion.Find(edge.Source);
//    //    var targetSubset = spanningTreeUnion.Find(edge.Target);

//    //    if (sourceSubset == targetSubset) // will cause a cycle
//    //      continue;

//    //    spanningTreeUnion.Union(spanningTreeUnion.Values[sourceSubset], spanningTreeUnion.Values[targetSubset]);
//    //    mst.Add(edge);

//    //    minSpanningTreeCount++;
//    //  }

//    //  return mst;
//    //}

//    //public System.Collections.Generic.List<Edge<TPoint>> PrimsMinimumSpanningTree()
//    //{
//    //  var mst = new System.Collections.Generic.Dictionary<TPoint, Edge<TPoint>>();

//    //  if (!AdjacencyList.Any())
//    //    return mst.Values.ToList();

//    //  var vertexToMinCost = new System.Collections.Generic.Dictionary<TPoint, PathCost>();

//    //  // Min Heap storing min cost of the spanning tree
//    //  var minHeapNodes = new IndexedHeap<PathCost>(
//    //      new System.Collections.Generic.List<PathCost> {
//    //                (vertexToMinCost[AdjacencyList.Keys.First()] = new PathCost(AdjacencyList.Keys.First(), 0))
//    //  }, (p1, p2) => p1.cost.CompareTo(p2.cost));

//    //  foreach (var vertex in AdjacencyList.Keys.Skip(1))
//    //    minHeapNodes.Push(vertexToMinCost[vertex] = new PathCost(vertex, int.MaxValue));

//    //  // Traverse the node by min cost
//    //  // Greedy strategy: Select the edge that minimize the cost for reaching node from its neighbors
//    //  while (minHeapNodes.Any())
//    //  {
//    //    var minNode = minHeapNodes.Pop();

//    //    // Visit all the neighbors and update the corresponding cost
//    //    foreach (var edge in AdjacencyList[minNode.vertex])
//    //    {
//    //      // Check that the current cost of reaching the adjacent node is less than the current one
//    //      if (minHeapNodes.Contains(vertexToMinCost[edge.Target]) && edge.weight < vertexToMinCost[edge.destination].cost)
//    //      {
//    //        vertexToMinCost[edge.Target].cost = edge.Weight;
//    //        mst[edge.Target] = edge;

//    //        // Sift up the heap starting from the current index (log n)
//    //        minHeapNodes.SiftUp(fromIndex: minHeapNodes.IndexOf(vertexToMinCost[edge.destination]));
//    //      }
//    //    }
//    //  }

//    //  return mst.Values.ToList();
//    //}

//    //public System.Collections.Generic.List<Edge<TPoint, TWeight>> DijkstraShortestPath(TPoint source, TPoint target = default)
//    //{
//    //  var sp = new System.Collections.Generic.Dictionary<TPoint, Edge<TPoint, TWeight>>();

//    //  if (!AdjacencyList.Any())
//    //    return sp.Values.ToList();

//    //  var vertexToPathCost = new System.Collections.Generic.Dictionary<TPoint, PathCost>();

//    //  // Min Heap storing accumulated min cost for reaching target node greedily
//    //  var minHeapNodes = new IndexedHeap<PathCost>(
//    //      new System.Collections.Generic.List<PathCost> { (vertexToPathCost[source] = new PathCost(source, 0)) },
//    //      compareFunc: (p1, p2) => p1.cost.CompareTo(p2.cost));

//    //  foreach (var vertex in AdjacencyList.Keys)
//    //  {
//    //    if (vertex == source)
//    //      continue;

//    //    minHeapNodes.Push(vertexToPathCost[vertex] = new PathCost(vertex, int.MaxValue));
//    //  }

//    //  // Greedy strategy: Select the path that minimized the accumulated cost up to this node
//    //  // Traverse the node by min cost
//    //  while (minHeapNodes.Any())
//    //  {
//    //    var minNode = minHeapNodes.Pop();

//    //    // Visit all the neighbors and update the corresponding cost
//    //    foreach (var edge in AdjacencyList[minNode.vertex])
//    //    {
//    //      // Check that the current cost of reaching the adjacent node is less than the current one
//    //      if (minHeapNodes.Contains(vertexToPathCost[edge.Target]) && minNode.cost + edge.Weight < vertexToPathCost[edge.Target].cost)
//    //      {
//    //        vertexToPathCost[edge.Target].cost = minNode.cost + edge.Weight;
//    //        sp[edge.Target] = edge;

//    //        // Sift up the heap starting from the current index (log n)
//    //        minHeapNodes.SiftUp(fromIndex: minHeapNodes.IndexOf(vertexToPathCost[edge.Target]));
//    //      }
//    //    }

//    //    if (minNode.vertex.Equals(target))
//    //      break;
//    //  }

//    //  return sp.Values.ToList();
//    //}
//  }
//}

///*
//      // Adjacent List.

//      var al = new Flux.Collections.Generic.Graph.AdjacentList<char, int>();

//      al.AddVertex('a');
//      al.AddVertex('b');
//      al.AddVertex('c');
//      al.AddVertex('d');

//      //g.AddDirectedEdge('a', 'b', 1);
//      //g.AddDirectedEdge('a', 'c', 1);
//      //g.AddDirectedEdge('b', 'a', 1);
//      //g.AddDirectedEdge('b', 'c', 1);
//      //g.AddDirectedEdge('c', 'a', 1);
//      //g.AddDirectedEdge('c', 'b', 1);
//      //g.AddDirectedEdge('c', 'd', 1);
//      //g.AddDirectedEdge('d', 'c', 1);

//      al.AddDirectedEdge('a', 'b', 2);
//      al.AddUndirectedEdge('a', 'c', 1);
//      al.AddUndirectedEdge('b', 'c', 1);
//      al.AddUndirectedEdge('c', 'd', 1);

//      al.RemoveUndirectedEdge('c', 'b', 1);

//      System.Console.WriteLine(al.ToString());

//      foreach (var edge in al.GetEdges())
//        System.Console.WriteLine(edge);
// */

/////// <summary>
/////// Helper data structure for Prim and Dijstra
/////// </summary>
////public class PathCost<TValue, TWeight>
////  where TValue : System.IEquatable<TWeight>
////  where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
////{
////  public Vertex<TValue> m_vertex;
////  public TWeight m_cost;

////  public PathCost(Vertex<TValue> vertex, TWeight cost)
////  {
////    m_vertex = vertex;
////    m_cost = cost;
////  }

////  public int CompareTo(object obj)
////    => obj is PathCost<TValue, TWeight> pathCost ? m_cost.CompareTo(pathCost.m_cost) : default;
////}
