namespace Flux.Numerics
{
  /// <summary>
  /// <para>This interpolates between data points to find the pth empirical quantile</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_4"/></para>
  /// </summary>
  public record class QuartileMethod4
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
      q1 = QuartileEmpirical.Estimate(sample, TSelf.CreateChecked(0.25));
      q2 = QuartileEmpirical.Estimate(sample, TSelf.CreateChecked(0.50));
      q3 = QuartileEmpirical.Estimate(sample, TSelf.CreateChecked(0.75));
    }
  }
}
