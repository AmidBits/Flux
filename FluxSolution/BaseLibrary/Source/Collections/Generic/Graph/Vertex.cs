namespace Flux.Collections.Generic.Graph
{
  public class Vertex<TVertex>
  {
    public Vertex(TVertex value, int degree)
    {
      Value = value;

      Degree = degree;
    }

    public TVertex Value { get; }

    public int Degree { get; }

    public override string ToString()
      => $"<{nameof(Vertex<TVertex>)}: {Value}, Degree: {Degree}>";
  }
}
