namespace Flux.Collections.Generic.Graph
{
  public class Vertex<TVertex>
    where TVertex : System.IEquatable<TVertex>
  {
    public Vertex(TVertex value, int degree)
    {
      Value = value;
      Degree = degree;
    }

    public int Degree { get; }
    public TVertex Value { get; }

    // Statics
    public static bool operator ==(Vertex<TVertex> a, Vertex<TVertex> b)
      => a.Equals(b);
    public static bool operator !=(Vertex<TVertex> a, Vertex<TVertex> b)
      => !a.Equals(b);

    // Equatable
    public bool Equals(Vertex<TVertex>? other)
      => other is not null && Value.Equals(other.Value) && Degree.Equals(other.Degree);

    // Overrides
    public override bool Equals(object? obj)
      => obj is Vertex<TVertex> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Value, Degree);
    public override string ToString()
      => $"<{nameof(Vertex<TVertex>)}: {Value}, Degree: {Degree}>";
  }
}
