namespace Flux.Collections.Generic.Graph
{
  /// <summary>Interface for a common approch to graphs.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)
  public interface IGraph<TVertex, TWeight>
    where TVertex : System.IEquatable<TVertex>
    //where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
  {
    bool AddVertex(TVertex vertex);
    bool RemoveVertex(TVertex vertex);

    void AddDirectedEdge(TVertex source, TVertex target, TWeight weight);
    void AddUndirectedEdge(TVertex source, TVertex target, TWeight weight);

    bool RemoveDirectedEdge(TVertex source, TVertex target, TWeight weight);
    bool RemoveUndirectedEdge(TVertex source, TVertex target, TWeight weight);
  }

}
