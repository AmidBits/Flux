namespace Flux.DataStructures.Graph
{
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(abstract_data_type)"/>
  public interface ISimpleGraph<TKey, TVertexValue, TEdgeValue>
    where TKey : notnull
  {
    /// <summary>Returns the value of the edge.</summary>
    public TEdgeValue GetEdgeValue(TKey source, TKey target);
    /// <summary>Replaces the value of the edge.</summary>
    public void SetEdgeValue(TKey source, TKey target, TEdgeValue value);
  }
}
