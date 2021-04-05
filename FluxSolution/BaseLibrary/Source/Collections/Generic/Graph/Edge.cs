namespace Flux.Collections.Generic.Graph
{
  public class Edge<TVertex, TWeight>
    where TVertex : System.IEquatable<TVertex>
    where TWeight : System.IComparable<TWeight>, System.IEquatable<TWeight>
  {
    private readonly TVertex m_source, m_target;

    private readonly TWeight m_weight;

    public Edge(TVertex source, TVertex target, TWeight weight)
    {
      m_source = source;
      m_target = target;

      m_weight = weight;
    }

    public TVertex Source
      => m_source;
    public TVertex Target
      => m_target;

    public TWeight Weight
      => m_weight;

    public override string ToString()
      => $"<{nameof(Edge<TVertex, TWeight>)}: {m_source}, {m_target}, {m_weight}>";
  }
}
