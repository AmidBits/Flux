namespace Flux.DataStructures.Graph
{
  public class Edge<TVertex, TValue>
    : System.IEquatable<Edge<TVertex, TValue>>
    where TVertex : System.IEquatable<TVertex>
    where TValue : System.IEquatable<TValue>
  {
    public Edge(Vertex<TVertex> source, Vertex<TVertex> target, TValue weight)
    {
      Source = source;
      Target = target;
      Weight = weight;
    }

    public Vertex<TVertex> Source { get; }
    public Vertex<TVertex> Target { get; }
    public TValue Weight { get; }

    public bool IsLoop
      => Source == Target;

    // Statics
    public static bool operator ==(Edge<TVertex, TValue> a, Edge<TVertex, TValue> b)
      => a.Equals(b);
    public static bool operator !=(Edge<TVertex, TValue> a, Edge<TVertex, TValue> b)
      => !a.Equals(b);

    // Equatable
    public bool Equals(Edge<TVertex, TValue>? other)
      => !(other is null) && Source == other.Source && Target == other.Target && Weight.Equals(other.Weight);

    // Overrides
    public override bool Equals(object? obj)
      => obj is Edge<TVertex, TValue> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Source, Target, Weight);
    public override string ToString()
      => $"<{nameof(Edge<TVertex, TValue>)}: {Source}, {Target}, Weight: {Weight}, Loop: {IsLoop}>";
  }
}
