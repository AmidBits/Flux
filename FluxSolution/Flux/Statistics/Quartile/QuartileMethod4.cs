namespace Flux.Statistics.Quartile
{
  /// <summary>
  /// <para>This interpolates between data points to find the pth empirical quantile</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quartile#Method_4"/></para>
  /// </summary>
  /// <remarks>Quartile method 4 is equivalent to <see cref="QuantileR6"/>.</remarks>
  public record class Method4
    : IQuartileComputable
  {
    public static IQuartileComputable Default => new Method4();

    public (double q1, double q2, double q3) ComputeQuartiles(System.Collections.Generic.IEnumerable<double> sample)
      => Compute(sample);

    public static (double q1, double q2, double q3) Compute(System.Collections.Generic.IEnumerable<double> sample)
      => Empirical.Compute(sample);
  }
}
