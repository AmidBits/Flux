namespace Flux.DataStructures.Graphs
{
  public class AdjacentEdge<TVertex, TValue>
    : System.IEquatable<AdjacentEdge<TVertex, TValue>>
    where TVertex : System.IEquatable<TVertex>
    where TValue : System.IEquatable<TValue>
  {
    public AdjacentEdge(AdjacentVertex<TVertex> source, AdjacentVertex<TVertex> target, TValue weight)
    {
      Source = source;
      Target = target;
      Weight = weight;
    }

    public AdjacentVertex<TVertex> Source { get; }
    public AdjacentVertex<TVertex> Target { get; }
    public TValue Weight { get; }

    public bool IsLoop
      => Source == Target;

    // Statics
    public static bool operator ==(AdjacentEdge<TVertex, TValue> a, AdjacentEdge<TVertex, TValue> b)
      => a.Equals(b);
    public static bool operator !=(AdjacentEdge<TVertex, TValue> a, AdjacentEdge<TVertex, TValue> b)
      => !a.Equals(b);

    // Equatable
    public bool Equals(AdjacentEdge<TVertex, TValue>? other)
      => !(other is null) && Source == other.Source && Target == other.Target && Weight.Equals(other.Weight);

    // Overrides
    public override bool Equals(object? obj)
      => obj is AdjacentEdge<TVertex, TValue> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Source, Target, Weight);
    public override string ToString()
      => $"<{nameof(AdjacentEdge<TVertex, TValue>)}: {Source}, {Target}, Weight: {Weight}, Loop: {IsLoop}>";
  }
}
