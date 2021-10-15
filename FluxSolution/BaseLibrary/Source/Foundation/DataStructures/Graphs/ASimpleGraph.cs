//namespace Flux.DataStructures.Graphs2
//{
//  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
//  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
//  /// https://www.tutorialspoint.com/representation-of-graphs
//  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
//  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
//  public abstract class ASimpleGraph<TKey>
//  {
//    public abstract System.Collections.Generic.IEnumerable<TKey> GetNeighbors(TKey x);
//    public abstract bool IsAdjacent(TKey x, TKey y);

//    public abstract bool AddVertex(TKey x);
//    public abstract bool ContainsVertex(TKey x);
//    public abstract System.Collections.Generic.ICollection<TKey> GetVertices();
//    public abstract bool RemoveVertex(TKey x);

//    public abstract bool TryGetVertexValue(TKey vertex, out object value);
//    public abstract bool TrySetVertexValue(TKey vertex, object value);

//    public abstract bool AddEdge(TKey x, TKey y, object value);
//    public abstract bool AddEdge(TKey x, TKey y);
//    public abstract bool ContainsEdge(TKey x, TKey y);
//    public abstract bool ContainsEdge(TKey x, TKey y, object value);
//    public abstract bool RemoveSimpleDirectedEdge(TKey x, TKey y);

//    public abstract bool TryGetEdgeValue(TKey x, TKey y, out object value);
//    public abstract bool TrySetEdgeValue(TKey x, TKey y, object value);
//  }
//}
