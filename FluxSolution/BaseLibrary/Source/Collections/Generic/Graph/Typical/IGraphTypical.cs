using System.Linq;

namespace Flux.Collections.Generic.Graph
{
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(abstract_data_type)"/>
  public interface IGraphTypical<TVertex, TValue>
    where TVertex : System.IEquatable<TVertex>
  {
    bool IsAdjacent(TVertex source, TVertex target);
    System.Collections.Generic.IEnumerable<TVertex> GetNeighbors(TVertex source);
    void AddVertex(TVertex vertex);
    void RemoveVertex(TVertex vertex);
    void AddEdge(TVertex source, TVertex target);
    void RemoveEdge(TVertex source, TVertex target);

    //TValue GetVertexValue(TVertex vertex);
    //void SetVertexValue(TVertex vertex, TValue value);
    //TValue GetEdgeValue(TVertex source, TVertex target);
    //void SetEdgeValue(TVertex source, TVertex target, TValue value);
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
