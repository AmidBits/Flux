namespace Flux.Collections.Generic.Graph
{
  public interface IGraph<TVertex, TWeight>
    where TVertex : System.IEquatable<TVertex>
    where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
  {
    bool AddVertex(TVertex vertex);
    bool RemoveVertex(TVertex vertex);

    void AddDirectedEdge(TVertex source, TVertex target, TWeight weight);
    void AddUndirectedEdge(TVertex source, TVertex target, TWeight weight);

    bool RemoveDirectedEdge(TVertex source, TVertex target, TWeight weight);
    bool RemoveUndirectedEdge(TVertex source, TVertex target, TWeight weight);
  }
}
