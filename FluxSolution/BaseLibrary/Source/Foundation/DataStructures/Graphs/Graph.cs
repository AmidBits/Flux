//using System.Linq;

namespace Flux.DataStructures.Graphs
{
  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
  public class Graph<TKey, TValue, TWeight>
    //: IGraph<TKey, TWeight>
    where TKey : System.IComparable<TKey>, System.IEquatable<TKey>
    where TValue : System.IComparable<TValue>, System.IEquatable<TValue>
    where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
  {
    private readonly System.Collections.Generic.HashSet<Vertex<TKey, TValue>> m_vertices = new System.Collections.Generic.HashSet<Vertex<TKey, TValue>>();
    private readonly System.Collections.Generic.HashSet<Edge<TKey, TWeight>> m_edges = new System.Collections.Generic.HashSet<Edge<TKey, TWeight>>();

    public bool AddVertex(Vertex<TKey, TValue> vertex)
    {
      if (ContainsVertex(vertex.Key))
        return false; // Already contains the vertex.

      m_vertices.Add(vertex);

      return true;
    }
    public bool AddVertex(TKey key, TValue value)
      => AddVertex(new Vertex<TKey, TValue>(key, value));
    public bool AddVertex(TKey key)
      => AddVertex(new Vertex<TKey, TValue>(key, default!));
    public bool ContainsVertex(TKey key)
    {
      foreach (var vertex in m_vertices)
        if (vertex.Key.Equals(key))
          return true;

      return false;
    }
    public bool ContainsVertexWhere(System.Predicate<Vertex<TKey, TValue>> match)
    {
      foreach (var vertex in m_vertices)
        if (match(vertex))
          return true;

      return false;
    }
    public bool TryGetVertexWhere(System.Predicate<Vertex<TKey, TValue>> match, out Vertex<TKey, TValue> result)
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

    public bool TryGetVertexValue(TKey key, out TValue value)
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
    public bool TrySetVertexValue(TKey key, TValue value)
    {
      if (TryGetVertexWhere(v => v.Key.Equals(key), out var vertex))
      {
        vertex.Value = value;
        return true;
      }

      return false;
    }

    public void AddEdge(Edge<TKey, TWeight> edge)
    {
      m_edges.Add(edge);
    }
    public void AddEdge(TKey source, TKey target, bool isDirected, params TWeight[] weight)
      => AddEdge(new Edge<TKey, TWeight>(source, target, isDirected, weight));
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
    public bool ContainsEdge(TKey source, TKey target, TWeight weight)
    {
      foreach (var edge in m_edges)
        if (edge.SourceKey.Equals(source) && edge.TargetKey.Equals(target))
          if (edge.Values.Contains(weight))
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

    public System.Collections.Generic.IEnumerable<TKey> GetVertices()
    {
      foreach (var vertex in m_vertices)
        yield return vertex.Key;
    }

    public System.Collections.Generic.IEnumerable<(TKey, TKey, TWeight)> GetEdges()
    {
      foreach (var edge in m_edges)
        foreach (var value in edge.Values)
          if (edge.IsDirected)
          {
            yield return (edge.SourceKey, edge.TargetKey, value);
          }
          else // Undirected, return both ways.
          {
            yield return (edge.SourceKey, edge.TargetKey, value);
            yield return (edge.TargetKey, edge.SourceKey, value);
          }
    }

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
