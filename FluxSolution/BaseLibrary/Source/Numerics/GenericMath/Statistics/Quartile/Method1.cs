namespace Flux.Quartiles
{
  /// <summary>
  /// <para>This rule is employed by the TI-83 calculator boxplot and "1-Var Stats" functions.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_1"/></para>
  /// </summary>
  public record class Method1
    : IQuartileComputable
  {
    public (TSelf q1, TSelf q2, TSelf q3) ComputeQuartiles<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      Compute(sample, out var q1, out var q2, out var q3);

      return (q1, q2, q3);
    }

    public static void Compute<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, out TSelf q1, out TSelf q2, out TSelf q3)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var e2 = (sample.Count() & 1) == 0;

      var m2 = sample.Count() / 2;
      q2 = e2 ? (sample.ElementAt(m2 - 1) + sample.ElementAt(m2)).Div2() : sample.ElementAt(m2);

      var o2 = (m2 & 1) == 1;

      var m1 = m2 / 2;
      q1 = o2 ? sample.ElementAt(m1) : (sample.ElementAt(m1) + sample.ElementAt(m1 + 1)).Div2();

      var m3 = sample.Count() - (m2 - m1);
      q3 = o2 ? sample.ElementAt(m3) : (sample.ElementAt(m3 - 1) + sample.ElementAt(m3)).Div2();
    }
  }
}
