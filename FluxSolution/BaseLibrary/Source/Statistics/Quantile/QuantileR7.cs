namespace Flux.Statistics.Quantile
{
  /// <summary>
  /// <para>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</para>
  /// <para><remarks>Equivalent to Excel's PERCENTILE and PERCENTILE.INC and Python's optional "inclusive" method.</remarks></para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class R7
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R7();

    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => TPercent.CreateChecked(count - TCount.One) * Quantities.UnitInterval.AssertMember(p, IntervalNotation.Closed, nameof(p)) + TPercent.One;

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => Edf.Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.
  }
}
