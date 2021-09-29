namespace Flux.DataStructures.Graph
{
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(abstract_data_type)"/>
  public interface IMultiGraph<TKey, TVertexValue, TEdgeValue>
    where TKey : notnull
  {
    /// <summary>Adds a value to an existing edge.</summary>
    public void AddEdgeValue(TKey source, TKey target, TEdgeValue value);
    /// <summary>Determines whether an edge has the specified value.</summary>
    public bool ContainsEdgeValue(TKey source, TKey target, TEdgeValue value);
    /// <summary>Removes a value from an existing edge.</summary>
    public bool RemoveEdgeValue(TKey source, TKey target, TEdgeValue value);
    /// <summary>Removes the specified values from an existing edge. Values that do not exist, are ignored.</summary>
    public void RemoveEdgeValues(TKey source, TKey target, params TEdgeValue[] values);

    /// <summary>Returns all edge values (edges) between <paramref name="source"/> and <paramref name="target"/>.</summary>
    System.Collections.Generic.List<TEdgeValue> GetEdgeValues(TKey source, TKey target);
    /// <summary>Replaces all edge values (edges) with the specified values.</summary>
    void SetEdgeValues(TKey source, TKey target, System.Collections.Generic.List<TEdgeValue> value);
  }
}
