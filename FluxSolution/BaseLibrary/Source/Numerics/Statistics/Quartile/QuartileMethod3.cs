namespace Flux.Numerics
{
  /// <summary>
  /// <para>This method is not implemented at this time.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_3"/></para>
  /// </summary>
  /// <remarks>Quartile method 3 is equivalent to Quantile R5.</remarks>
  public record class QuartileMethod3
    : IQuartileComputable
  {
    public (TSelf q1, TSelf q2, TSelf q3) ComputeQuartiles<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Compute(sample);

    public static (TSelf q1, TSelf q2, TSelf q3) Compute<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        QuantileR5.Estimate(sample, TSelf.CreateChecked(0.25)),
        QuantileR5.Estimate(sample, TSelf.CreateChecked(0.50)),
        QuantileR5.Estimate(sample, TSelf.CreateChecked(0.75))
      );
  }
}
