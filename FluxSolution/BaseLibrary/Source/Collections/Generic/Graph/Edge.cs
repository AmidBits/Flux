namespace Flux.Collections.Generic.Graph
{
  public class Edge<TVertex, TWeight>
    where TVertex : System.IEquatable<TVertex>
    where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
  {
    public Edge(Vertex<TVertex> source, Vertex<TVertex> target, TWeight weight)
    {
      Source = source;
      Target = target;

      Weight = weight;
    }

    public Vertex<TVertex> Source { get; }
    public Vertex<TVertex> Target { get; }

    public TWeight Weight { get; }

    public override string ToString()
      => $"<{nameof(Edge<TVertex, TWeight>)}: {Source}, {Target}, Weight: {Weight}>";
  }
}
