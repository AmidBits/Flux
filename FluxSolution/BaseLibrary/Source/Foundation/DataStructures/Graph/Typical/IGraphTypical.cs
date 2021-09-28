namespace Flux.DataStructures.Graph
{
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(abstract_data_type)"/>
  public interface IGraphTypical<TVertex, TVertexValue, TEdgeValue>
    where TVertex : System.IEquatable<TVertex>
  {
    System.Collections.Generic.IEnumerable<TVertex> GetNeighbors(TVertex source);
    bool IsAdjacent(TVertex source, TVertex target);

    void AddVertex(TVertex vertex, TVertexValue value);
    void AddVertex(TVertex vertex);
    void RemoveVertex(TVertex vertex);

    TVertexValue GetVertexValue(TVertex vertex);
    void SetVertexValue(TVertex vertex, TVertexValue value);

    System.Collections.Generic.IEnumerable<(TVertex vertex, TVertexValue value, int degree)> GetVertices();

    void AddEdge(TVertex source, TVertex target, TEdgeValue value);
    void AddEdge(TVertex source, TVertex target);
    void RemoveEdge(TVertex source, TVertex target);

    System.Collections.Generic.List<TEdgeValue> GetEdgeValues(TVertex source, TVertex target);
    void SetEdgeValues(TVertex source, TVertex target, System.Collections.Generic.List<TEdgeValue> value);

    System.Collections.Generic.IEnumerable<(TVertex source, TVertex target, TEdgeValue value)> GetEdges();
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
