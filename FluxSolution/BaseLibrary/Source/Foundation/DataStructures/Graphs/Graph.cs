//using System.Linq;

namespace Flux.DataStructures.Graphs
{
  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
  public class Graph<TKey, TVertexValue, TEdgeValue>
    where TKey : System.IComparable<TKey>, System.IEquatable<TKey>
    where TVertexValue : System.IComparable<TVertexValue>, System.IEquatable<TVertexValue>
    where TEdgeValue : System.IComparable<TEdgeValue>, System.IEquatable<TEdgeValue>
  {
    private readonly System.Collections.Generic.HashSet<Vertex<TKey, TVertexValue>> m_vertices = new System.Collections.Generic.HashSet<Vertex<TKey, TVertexValue>>();
    private readonly System.Collections.Generic.HashSet<Edge<TKey, TEdgeValue>> m_edges = new System.Collections.Generic.HashSet<Edge<TKey, TEdgeValue>>();

    public bool AddVertex(Vertex<TKey, TVertexValue> vertex)
    {
      if (ContainsVertex(vertex.Key))
        return false; // Already contains the vertex.

      m_vertices.Add(vertex);

      return true;
    }
    public bool AddVertex(TKey key, TVertexValue value)
      => AddVertex(new Vertex<TKey, TVertexValue>(key, value));
    public bool AddVertex(TKey key)
      => AddVertex(new Vertex<TKey, TVertexValue>(key, default!));
    public bool ContainsVertex(TKey key)
    {
      foreach (var vertex in m_vertices)
        if (vertex.Key.Equals(key))
          return true;

      return false;
    }
    public bool ContainsVertexWhere(System.Predicate<Vertex<TKey, TVertexValue>> match)
    {
      foreach (var vertex in m_vertices)
        if (match(vertex))
          return true;

      return false;
    }
    public bool TryGetVertexWhere(System.Predicate<Vertex<TKey, TVertexValue>> match, out Vertex<TKey, TVertexValue> result)
    {
      foreach (var vertex in m_vertices)
        if (match(vertex))
        {
          result = vertex;
          return true;
        }

      result = default!;
      return false;
    }
    public bool RemoveVertex(TKey key)
    {
      if (!ContainsVertex(key))
        return false; // Contains no such vertex.

      m_edges.RemoveWhere(v => v.SourceKey.Equals(key) || v.TargetKey.Equals(key)); // Remove all edges with the key as an endpoint.

      m_vertices.RemoveWhere(v => v.Key.Equals(key)); // Remove the vertex.

      return true;
    }

    public bool TryGetVertexValue(TKey key, out TVertexValue value)
    {
      foreach (var vertex in m_vertices)
        if (vertex.Key.Equals(key))
        {
          value = vertex.Value;
          return true;
        }

      value = default!;
      return false;
    }
    public bool TrySetVertexValue(TKey key, TVertexValue value)
    {
      if (TryGetVertexWhere(v => v.Key.Equals(key), out var vertex))
      {
        vertex.Value = value;
        return true;
      }

      return false;
    }

    public void AddEdge(Edge<TKey, TEdgeValue> edge)
    {
      m_edges.Add(edge);
    }
    public void AddEdge(TKey source, TKey target, bool isDirected, params TEdgeValue[] weight)
    {
      for (var index = 0; index < weight.Length; index++)
        AddEdge(new Edge<TKey, TEdgeValue>(source, target, isDirected, weight[index]));
    }

    public bool ContainsEdge(TKey key)
    {
      foreach (var edge in m_edges)
        if (edge.SourceKey.Equals(key) || edge.TargetKey.Equals(key))
          return true;

      return false;
    }
    public bool ContainsEdge(TKey source, TKey target)
    {
      foreach (var edge in m_edges)
        if (edge.SourceKey.Equals(source) && edge.TargetKey.Equals(target))
          return true;

      return false;
    }
    public bool ContainsEdge(TKey source, TKey target, TEdgeValue weight)
    {
      foreach (var edge in m_edges)
        if (edge.SourceKey.Equals(source) && edge.TargetKey.Equals(target))
          if (edge.Value.Equals(weight))
            return true;

      return false;
    }
    public int RemoveEdge(TKey key)
      => m_edges.RemoveWhere(v => v.SourceKey.Equals(key) || v.TargetKey.Equals(key)); // Remove all edges with the key as any endpoint.
    public int RemoveEdge(TKey source, TKey target)
      => m_edges.RemoveWhere(v => v.SourceKey.Equals(source) && v.TargetKey.Equals(target)); // Remove all edges with the source and target as endpoints.
    public int RemoveEdge(TKey source, TKey target, bool isDirected)
      => m_edges.RemoveWhere(v => v.SourceKey.Equals(source) && v.TargetKey.Equals(target) && v.IsDirected == isDirected); // Remove all edges with the source and target as endpoints.
    //public int RemoveEdge(TKey source, TKey target, TWeight weight) 
    //  => m_edges.RemoveWhere(v => v.SourceKey.Equals(source) && v.TargetKey.Equals(target)&&v.Values.Cont); // Remove all edges with the key as an endpoint.

    /// <summary>Computes the shortest path from the start vertex to all reachable vertices.</summary>
    /// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
    public System.Collections.Generic.IEnumerable<(TKey destination, double distance)> GetDijkstraShortestPathTree(TKey origin, System.Func<TEdgeValue, double> distanceSelector)
    {
      var vertices = System.Linq.Enumerable.ToList(GetVertices());

      var distances = System.Linq.Enumerable.ToDictionary(vertices, v => v.key, v => v.key.Equals(origin) ? 0 : double.PositiveInfinity);

      var edges = System.Linq.Enumerable.ToList(GetEdges()); // Cache edges, because we need it while there are available distances.

      while (System.Linq.Enumerable.Any(distances)) // As long as there are nodes available.
      {
        var shortest = System.Linq.Enumerable.First(System.Linq.Enumerable.OrderBy(distances, v => v.Value)); // Get the node with the shortest distance.

        if (shortest.Value < double.PositiveInfinity) // If the distance to the node is less than infinity, it was reachable so it should be returned.
          yield return (shortest.Key, shortest.Value);

        distances.Remove(shortest.Key); // This node is now final, so remove it.

        foreach (var edge in System.Linq.Enumerable.Where(edges, e => e.source.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
        {
          if (distances.TryGetValue(edge.target, out var distanceToEdgeTarget))
          {
            var distanceViaShortest = shortest.Value + distanceSelector(edge.value); // Distance via the current node.

            if (distanceViaShortest < distanceToEdgeTarget) // If the distance via the current node is shorter than the currently recorded distance, replace it.
              distances[edge.target] = distanceViaShortest;
          }
        }
      }
    }

    /// <summary>Returns all edges in the graph.</summary>
    public System.Collections.Generic.IEnumerable<(TKey source, TKey target, TEdgeValue value)> GetEdges()
    {
      foreach (var edge in m_edges)
        if (edge.IsDirected)
        {
          yield return (edge.SourceKey, edge.TargetKey, edge.Value);
        }
        else // Undirected, return both ways.
        {
          yield return (edge.SourceKey, edge.TargetKey, edge.Value);
          yield return (edge.TargetKey, edge.SourceKey, edge.Value);
        }
    }
    /// <summary>Returns all edges in the graph sorted by source, target and value.</summary>
    public System.Collections.Generic.IEnumerable<(TKey source, TKey target, TEdgeValue value)> GetEdgesSorted()
      => System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.OrderBy(System.Linq.Enumerable.OrderBy(GetEdges(), e => e.value), e => e.target), e => e.source);

    /// <summary>Returns all vertices in the graph.</summary>
    public System.Collections.Generic.IEnumerable<(TKey key, TVertexValue value)> GetVertices()
    {
      foreach (var vertex in m_vertices)
        yield return (vertex.Key, vertex.Value);
    }

    //public System.Collections.Generic.Dictionary<TKey, double> GetDijkstraShortestPathTree(TKey start, System.Func<TEdgeValue, double> distanceSelector)
    //{
    //  var distances = System.Linq.Enumerable.ToDictionary(GetVertices(), v => v.key, v => v.key.Equals(start) ? 0 : double.PositiveInfinity);

    //  var spt = new System.Collections.Generic.Dictionary<TKey, double>(); // Initial shortest path tree is empty.

    //  var edges = GetEdges();

    //  while (System.Linq.Enumerable.Any(distances)) // As long as there are nodes available.
    //  {
    //    var shortest = System.Linq.Enumerable.First(System.Linq.Enumerable.OrderBy(distances, v => v.Value)); // Get the node with the shortest distance.

    //    if (shortest.Value < double.PositiveInfinity) // If the distance to the node is less than infinity, it was reachable so it should be returned.
    //      spt.Add(shortest.Key, shortest.Value);

    //    distances.Remove(shortest.Key); // This node is now final, so remove it.

    //    foreach (var edge in System.Linq.Enumerable.Where(edges, e => e.source.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
    //    {
    //      if (distances.TryGetValue(edge.target, out var distanceToEdgeTarget))
    //      {
    //        var distanceViaShortest = shortest.Value + distanceSelector(edge.value); // Distance via the current node.

    //        if (distanceViaShortest < distanceToEdgeTarget) // If the distance via the current node is shorter than the currently recorded distance, replace it.
    //          distances[edge.target] = distanceViaShortest;
    //      }
    //    }
    //  }

    //  return spt;
    //}

    // Overrides.
    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();
      var index = 0;
      foreach (var edge in GetEdges())
        sb.AppendLine($"#{++index}: {edge}");
      sb.Insert(0, $"<{GetType().Name}: ({m_vertices.Count} vertices, {index} edges)>{System.Environment.NewLine}");
      return sb.ToString();
    }
  }
}

/*
      // Adjacent Matrix.

      var am = new Flux.Collections.Generic.Graph.AdjacentMatrix<char, int>();

      am.AddVertex('a');
      am.AddVertex('b');
      am.AddVertex('c');
      am.AddVertex('d');

      //am.AddDirectedEdge('a', 'b', 1);
      //am.AddDirectedEdge('a', 'c', 1);
      //am.AddDirectedEdge('b', 'a', 1);
      //am.AddDirectedEdge('b', 'c', 1);
      //am.AddDirectedEdge('c', 'a', 1);
      //am.AddDirectedEdge('c', 'b', 1);
      //am.AddDirectedEdge('c', 'd', 1);
      //am.AddDirectedEdge('d', 'c', 1);

      am.AddDirectedEdge('a', 'b', 2);
      am.AddUndirectedEdge('a', 'c', 1);
      am.AddUndirectedEdge('b', 'c', 1);
      am.AddUndirectedEdge('c', 'd', 1);

      am.RemoveUndirectedEdge('c', 'b', 1);

      System.Console.WriteLine(am.ToConsoleString());

      foreach (var edge in am.GetEdges())
        System.Console.WriteLine(edge);
 */
