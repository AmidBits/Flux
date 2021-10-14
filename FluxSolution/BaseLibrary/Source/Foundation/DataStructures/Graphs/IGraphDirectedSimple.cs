namespace Flux.DataStructures.Graphs
{
  public interface IGraphDirectedSimple<TKey, TEdgeValue>
    : IGraphDirected<TKey, TEdgeValue>
    where TKey : notnull
  {
    /// <summary>Adds an edge to the graph.</summary>
    bool AddEdge(TKey keySource, TKey keyTarget, TEdgeValue value);

    ///// <summary>Determines whether the specified edge exists in the graph.</summary>
    //bool ContainsEdge(TKey keySource, TKey keyTarget, TEdgeValue value);
    /// <summary>Determines whether the specified edge exists in the graph.</summary>
    bool ContainsEdge(TKey keySource, TKey keyTarget);

    /// <summary>Removes the specified edge from the graph, if it exists.</summary>
    bool RemoveEdge(TKey keySource, TKey keyTarget);

    /// <summary>Tries to get the value for the specified edge and returns whether it succeeded.</summary>
    public bool TryGetEdgeValue(TKey keySource, TKey keyTarget, out TEdgeValue value);
    /// <summary>Tries to set the value for the specified edge and returns whether it succeeded.</summary>
    public bool TrySetEdgeValue(TKey keySource, TKey keyTarget, TEdgeValue value);
  }
}
