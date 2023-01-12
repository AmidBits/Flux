namespace Flux.Numerics
{
  /// <summary>
  /// <para>The values found by this method are also known as "Tukey's hinges".</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_2"/></para>
  /// </summary>
  public record class QuartileMethod2
    : IQuartileComputable
  {
    public (TSelf q1, TSelf q2, TSelf q3) ComputeQuartiles<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Compute(sample);

    public static (TSelf q1, TSelf q2, TSelf q3) Compute<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var sampleCount = sample.Count();

      var o2 = int.IsOddInteger(sampleCount);

      var m2 = sampleCount / 2;
      var q2 = o2 ? sample.ElementAt(m2) : (sample.ElementAt(m2 - 1) + sample.ElementAt(m2)).Divide(2);

      if (o2) m2 += 1; // If odd counts, include median in both halfs.

      o2 = int.IsOddInteger(m2);

      var m1 = m2 / 2;
      var q1 = o2 ? sample.ElementAt(m1) : (sample.ElementAt(m1 - 1) + sample.ElementAt(m1)).Divide(2);

      var m3 = sampleCount - (m2 - m1);
      var q3 = o2 ? sample.ElementAt(m3) : (sample.ElementAt(m3 - 1) + sample.ElementAt(m3)).Divide(2);

      return (q1, q2, q3);
    }
  }
}
