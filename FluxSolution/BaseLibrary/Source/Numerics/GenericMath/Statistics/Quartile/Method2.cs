namespace Flux.Quartiles
{
  /// <summary>
  /// <para>The values found by this method are also known as "Tukey's hinges".</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_2"/></para>
  /// </summary>
  public record class Method2
    : IQuartileComputable
  {
    public (TSelf q1, TSelf q2, TSelf q3) ComputeQuartiles<TSelf>(System.Collections.Generic.IList<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      Compute(sample, out var q1, out var q2, out var q3);

      return (q1, q2, q3);
    }

    public static void Compute<TSelf>(System.Collections.Generic.IList<TSelf> sample, out TSelf q1, out TSelf q2, out TSelf q3)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var o2 = (sample.Count & 1) == 1;

      var m2 = sample.Count / 2;
      q2 = o2 ? sample[m2] : (sample[m2 - 1] + sample[m2]).Div2();

      if (o2) m2 += 1; // If odd counts, include median in both halfs.

      o2 = (m2 & 1) == 1;

      var m1 = m2 / 2;
      q1 = o2 ? sample[m1] : (sample[m1 - 1] + sample[m1]).Div2();

      var m3 = sample.Count - (m2 - m1);
      q3 = o2 ? sample[m3] : (sample[m3 - 1] + sample[m3]).Div2();
    }
  }
}
