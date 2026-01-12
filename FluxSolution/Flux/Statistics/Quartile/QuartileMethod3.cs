namespace Flux.Statistics.Quartile
{
  /// <summary>
  /// <para>This method is not implemented at this time.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_3"/></para>
  /// </summary>
  /// <remarks>Quartile method 3 is equivalent to <see cref="Quantile.R5"/>.</remarks>
  public record class Method3
    : IQuartileComputable
  {
    public static IQuartileComputable Default => new Method3();

    public (double q1, double q2, double q3) ComputeQuartiles(System.Collections.Generic.IEnumerable<double> sample)
      => Compute(sample);

    public static (double q1, double q2, double q3) Compute(System.Collections.Generic.IEnumerable<double> sample)
      => (
        Quantile.R5.Default.EstimateQuantileValue(sample, 0.25),
        Quantile.R5.Default.EstimateQuantileValue(sample, 0.50),
        Quantile.R5.Default.EstimateQuantileValue(sample, 0.75)
      );
  }
}
