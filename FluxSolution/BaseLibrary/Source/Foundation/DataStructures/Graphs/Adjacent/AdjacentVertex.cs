namespace Flux.DataStructures.Graphs
{
  public class AdjacentVertex<TVertex>
    where TVertex : System.IEquatable<TVertex>
  {
    public AdjacentVertex(TVertex value, int degree)
    {
      Value = value;
      Degree = degree;
    }

    public int Degree { get; }
    public TVertex Value { get; }

    // Statics
    public static bool operator ==(AdjacentVertex<TVertex> a, AdjacentVertex<TVertex> b)
      => a.Equals(b);
    public static bool operator !=(AdjacentVertex<TVertex> a, AdjacentVertex<TVertex> b)
      => !a.Equals(b);

    // Equatable
    public bool Equals(AdjacentVertex<TVertex>? other)
      => !(other is null) && Value.Equals(other.Value) && Degree.Equals(other.Degree);

    // Overrides
    public override bool Equals(object? obj)
      => obj is AdjacentVertex<TVertex> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Value, Degree);
    public override string ToString()
      => $"<{nameof(AdjacentVertex<TVertex>)}: {Value}, Degree: {Degree}>";
  }
}
