namespace Flux.DataStructures.Graphs
{
  /// <summary>
  /// <para></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TVertexValue"></typeparam>
  /// <typeparam name="TEdgeValue"></typeparam>
  public interface IGraph<TVertexValue, TEdgeValue>
  {
    IGraph<TVertexValue, TEdgeValue> CloneEmpty();

    /// <summary>Adds the vertex <paramref name="x"/>, if it is not there.</summary>
    bool AddVertex(int x);

    /// <summary>Adds the vertex <paramref name="x"/> with the <paramref name="value"/>, if it is not there.</summary>
    bool AddVertex(int x, TVertexValue value);

    /// <summary>Removes vertex <paramref name="x"/>, if it is there.</summary>
    bool RemoveVertex(int x);

    /// <summary>Returns the <paramref name="value"/> associated with vertex <paramref name="x"/>. A vertex can exists without a value.</summary>
    bool TryGetVertexValue(int x, out TVertexValue value);

    /// <summary>Removes the value vertex <paramref name="x"/> and whether the removal was successful.</summary>
    bool RemoveVertexValue(int x);

    /// <summary>Sets the <paramref name="value"/> associated with vertex <paramref name="x"/>.</summary>
    void SetVertexValue(int x, TVertexValue value);

    /// <summary>Adds the edge (<paramref name="x"/>, <paramref name="y"/>) with the <paramref name="value"/>, if it is not there.</summary>
    bool AddEdge(int x, int y, TEdgeValue value);

    /// <summary>Tests whether there is an edge (<paramref name="x"/>, <paramref name="y"/>), either directed or a loop.</summary>
    bool EdgeExists(int x, int y, TEdgeValue value);

    /// <summary>Removes the edge (<paramref name="x"/>, <paramref name="y"/>), if it is there.</summary>
    bool RemoveEdge(int x, int y, TEdgeValue value);

    System.Collections.Generic.IEnumerable<(int x, int y, TEdgeValue value)> GetEdges();

    System.Collections.Generic.IEnumerable<(int x, TVertexValue value)> GetVertices();

    System.Collections.Generic.IEnumerable<(int x, TVertexValue value, int degree)> GetVerticesWithDegree();
  }
}
