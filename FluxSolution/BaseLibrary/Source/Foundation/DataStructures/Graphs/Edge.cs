//namespace Flux.DataStructures.Graphs
//{
//  /// <summary>Represents a graph using an adjacency matrix. Unlimited edge combinations and types.</summary>
//  /// https://docs.microsoft.com/en-us/previous-versions/ms379574(v=vs.80)
//  /// https://www.tutorialspoint.com/representation-of-graphs
//  /// https://www.geeksforgeeks.org/graph-data-structure-and-algorithms/
//  /// <see cref="https://en.wikipedia.org/wiki/Graph_(discrete_mathematics)"/>
//  public struct Edge<TKey, TValue>
//    : System.IEquatable<Edge<TKey, TValue>>
//    where TKey : System.IEquatable<TKey>
//    where TValue : System.IEquatable<TValue>
//  {
//    TKey Source { get; }
//    TKey Target { get; }
//    TValue Value { get; }

//    public Edge(TKey source, TKey target, TValue value)
//    {
//      Source = source;
//      Target = target;
//      Value = value;
//    }

//    public bool Equals(Edge<TKey, TValue> other)
//      => Source.Equals(other.Source) && Target.Equals(other.Target) && Value.Equals(other.Value);

//    public override bool Equals(object? obj)
//      => obj is Edge<TKey, TValue> o && Equals(o);
//    public override int GetHashCode()
//      => System.HashCode.Combine(Source, Target, Value);
//    public override string ToString()
//      => $"{GetType().Name} {{ {Source}, {Target} = {Value} }}";
//  }
//}
