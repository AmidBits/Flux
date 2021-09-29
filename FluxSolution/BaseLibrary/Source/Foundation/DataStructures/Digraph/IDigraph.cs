namespace Flux.DataStructures.Graph
{
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(abstract_data_type)"/>
  public interface IDigraph<TKey, TVertexValue, TEdgeValue>
    where TKey : notnull
  {
    /// <summary>Returns the degree of the vertex.</summary>
    int GetDegree(TKey vertex);
    /// <summary>Returns the neighbors of the vertex (i.e. all edges that has the vertex as the source).</summary>
    System.Collections.Generic.IEnumerable<TKey> GetNeighbors(TKey vertex);
    /// <summary>Determines whether the source and target are adjacent (i.e. whether there are any edges from source to target).</summary>
    bool IsAdjacent(TKey source, TKey target);
    /// <summary>Determines whether the vertex is a loop (i.e. whether the vertex has an edge to itself).</summary>
    bool IsLoop(TKey vertex);

    /// <summary>Adds a vertex with the specified value.</summary>
    void AddVertex(TKey vertex, TVertexValue value);
    /// <summary>Adds a vertex.</summary>
    void AddVertex(TKey vertex);
    /// <summary>Determines whether the graph contains the specified vertex.</summary>
    bool ContainsVertex(TKey vertex);
    /// <summary>Removes a vertex.</summary>
    void RemoveVertex(TKey vertex);

    /// <summary>Gets the value of the specified vertex.</summary>
    TVertexValue GetVertexValue(TKey vertex);
    /// <summary>Sets the value of the specified vertex.</summary>
    void SetVertexValue(TKey vertex, TVertexValue value);

    /// <summary>Creates a new sequence with all vertices in the graph.</summary>
    System.Collections.Generic.IEnumerable<TKey> GetVertices();

    /// <summary>Adds an edge with the specified value.</summary>
    void AddEdge(TKey source, TKey target, TEdgeValue value);
    /// <summary>Adds an edge without any values (if it already exists, nothing changes).</summary>
    void AddEdge(TKey source, TKey target);
    /// <summary>Determines whether the graph contains the specified edge.</summary>
    bool ContainsEdge(TKey source, TKey target);
    /// <summary>Removes an edge.</summary>
    void RemoveEdge(TKey source, TKey target);

    ///// <summary>Returns a single value if such exists.</summary>
    //TEdgeValue GetEdgeValue(TVertex source, TVertex target);
    ///// <summary>Replaces any existing edge values with the single value.</summary>
    //void SetEdgeValue(TVertex source, TVertex target, TEdgeValue value);

    //System.Collections.Generic.List<TEdgeValue> GetEdgeValues(TVertex source, TVertex target);
    //void SetEdgeValues(TVertex source, TVertex target, System.Collections.Generic.List<TEdgeValue> value);

    /// <summary>Creates a new sequence with all edges in the graph.</summary>
    System.Collections.Generic.IEnumerable<(TKey source, TKey target, TEdgeValue value)> GetEdges();
  }
}
