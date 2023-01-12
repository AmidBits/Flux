namespace Flux.Numerics
{
  /// <summary>
  /// <para>This rule is employed by the TI-83 calculator boxplot and "1-Var Stats" functions.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_1"/></para>
  /// </summary>
  public record class QuartileMethod1
    : IQuartileComputable
  {
    public (TSelf q1, TSelf q2, TSelf q3) ComputeQuartiles<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Compute(sample);

    public static (TSelf q1, TSelf q2, TSelf q3) Compute<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var sampleCount = sample.Count();

      var m2 = sampleCount / 2;
      var q2 = int.IsEvenInteger(sampleCount) ? (sample.ElementAt(m2 - 1) + sample.ElementAt(m2)).Divide(2) : sample.ElementAt(m2);

      var o2 = int.IsOddInteger(m2);

      var m1 = m2 / 2;
      var q1 = o2 ? sample.ElementAt(m1) : (sample.ElementAt(m1) + sample.ElementAt(m1 + 1)).Divide(2);

      var m3 = sampleCount - (m2 - m1);
      var q3 = o2 ? sample.ElementAt(m3) : (sample.ElementAt(m3 - 1) + sample.ElementAt(m3)).Divide(2);

      return (q1, q2, q3);
    }
  }
}
