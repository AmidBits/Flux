namespace Flux.DataStructures.Graphs
{
  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
  /// https://www.tutorialspoint.com/representation-of-graphs
  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
  public struct UndirectedEdge<TVertexKey, TValue>
    : System.IEquatable<UndirectedEdge<TVertexKey, TValue>>
    where TVertexKey : System.IEquatable<TVertexKey>
    where TValue : System.IEquatable<TValue>
  {
    TVertexKey Source { get; }
    TVertexKey Target { get; }
    TValue Value { get; }

    public UndirectedEdge(TVertexKey source, TVertexKey target, TValue value)
    {
      Source = source;
      Target = target;
      Value = value;
    }

    public bool Equals(UndirectedEdge<TVertexKey, TValue> other)
      => Source.Equals(other.Source) && Target.Equals(other.Target) && Value.Equals(other.Value);

    public override bool Equals(object? obj)
      => obj is UndirectedEdge<TVertexKey, TValue> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Source, Target, Value);
    public override string ToString()
      => $"{GetType().Name} {{ {Source}, {Target} = {Value} }}";
  }
}
