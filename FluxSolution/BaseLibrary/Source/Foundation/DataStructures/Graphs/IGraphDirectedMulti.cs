namespace Flux.DataStructures.Graphs
{
  public interface IGraphDirectedMulti<TKey, TEdgeValue>
    : IGraphDirected<TKey, TEdgeValue>
    where TKey : notnull
  {
    /// <summary>Adds an edge to the graph.</summary>
    bool AddMultiDirectedEdge(TKey keySource, TKey keyTarget, TEdgeValue value);

    /// <summary>Determines whether the specified edges exists in the graph.</summary>
    bool ContainsMultiDirectedEdge(TKey keySource, TKey keyTarget);
    /// <summary>Determines whether the specified edge exists in the graph.</summary>
    bool ContainsMultiDirectedEdge(TKey keySource, TKey keyTarget, TEdgeValue value);

    /// <summary>Removes the specified edge from the graph.</summary>
    bool RemoveMultiDirectedEdge(TKey keySource, TKey keyTarget, TEdgeValue value);
    /// <summary>Removes all specified edges (any value) from the graph.</summary>
    bool RemoveMultiDirectedEdge(TKey keySource, TKey keyTarget);

    /// <summary>Tries to get the values for the specified edge and returns whether it succeeded.</summary>
    public bool TryGetMultiDirectedEdgeValues(TKey keySource, TKey keyTarget, out System.Collections.Generic.List<TEdgeValue> list);
    /// <summary>Tries to set the values for the specified edge and returns whether it succeeded.</summary>
    public bool TrySetMultiDirectedEdgeValues(TKey keySource, TKey keyTarget, System.Collections.Generic.List<TEdgeValue> list);
  }
}
