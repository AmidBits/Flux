namespace Flux.DataStructures.Graph
{
  public class Edge<TVertex, TWeight>
    : System.IEquatable<Edge<TVertex, TWeight>>
    where TVertex : System.IEquatable<TVertex>
    where TWeight : System.IEquatable<TWeight>
  {
    public Edge(Vertex<TVertex> source, Vertex<TVertex> target, TWeight weight)
    {
      Source = source;
      Target = target;
      Weight = weight;
    }

    public bool IsLoop
      => Source == Target;
    public Vertex<TVertex> Source { get; }
    public Vertex<TVertex> Target { get; }
    public TWeight Weight { get; }

    // Statics
    public static bool operator ==(Edge<TVertex, TWeight> a, Edge<TVertex, TWeight> b)
      => a.Equals(b);
    public static bool operator !=(Edge<TVertex, TWeight> a, Edge<TVertex, TWeight> b)
      => !a.Equals(b);

    // Equatable
    public bool Equals(Edge<TVertex, TWeight>? other)
      => !(other is null) && Source == other.Source && Target == other.Target && Weight.Equals(other.Weight);

    // Overrides
    public override bool Equals(object? obj)
      => obj is Edge<TVertex, TWeight> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Source, Target, Weight);
    public override string ToString()
      => $"<{nameof(Edge<TVertex, TWeight>)}: {Source}, {Target}, Weight: {Weight}, Loop: {IsLoop}>";
  }
}
