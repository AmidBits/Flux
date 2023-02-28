namespace Flux.Numerics
{
  /// <summary>
  /// <para>This interpolates between data points to find the pth empirical quantile</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_4"/></para>
  /// </summary>
  /// <remarks>Quartile method 4 is equivalent to <see cref="QuantileR6"/>.</remarks>
  public record class QuartileMethod4
    : IQuartileComputable
  {
    public (TSelf q1, TSelf q2, TSelf q3) ComputeQuartiles<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Compute(sample);

    public static (TSelf q1, TSelf q2, TSelf q3) Compute<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => QuartileEmpirical.Compute(sample);
  }
}
