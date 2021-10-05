namespace Flux.DataStructures.Graphs
{
  public interface IGraphDirected<TKey, TEdgeValue>
    : IGraphVertex<TKey>
    where TKey : notnull
  {
    System.Collections.Generic.IEnumerable<(TKey keySource, TKey keyTarget, TEdgeValue value)> GetDirectedEdges();
  }
}
