namespace Flux.DataStructures.Graphs
{
  public interface IGraphDirectedSimple<TKey, TEdgeValue>
    : IGraphDirected<TKey, TEdgeValue>
    where TKey : notnull
  {
    /// <summary>Adds an edge to the graph.</summary>
    bool AddSimpleDirectedEdge(TKey keySource, TKey keyTarget, TEdgeValue value);

    /// <summary>Determines whether the specified edge exists in the graph.</summary>
    bool ContainsSimpleDirectedEdge(TKey keySource, TKey keyTarget, TEdgeValue value);
    /// <summary>Determines whether the specified edge exists in the graph.</summary>
    bool ContainsSimpleDirectedEdge(TKey keySource, TKey keyTarget);

    /// <summary>Removes the specified edge from the graph, if it exists.</summary>
    bool RemoveSimpleDirectedEdge(TKey keySource, TKey keyTarget);

    /// <summary>Tries to get the value for the specified edge and returns whether it succeeded.</summary>
    public bool TryGetSimpleDirectedEdgeValue(TKey keySource, TKey keyTarget, out TEdgeValue value);
    /// <summary>Tries to set the value for the specified edge and returns whether it succeeded.</summary>
    public bool TrySetSimpleDirectedEdgeValue(TKey keySource, TKey keyTarget, TEdgeValue value);
  }
}
