namespace Flux.Statistics.Quantile
{
  /// <summary>
  /// <para>Piecewise linear function where the knots are the values midway through the steps of the empirical distribution function.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  /// <remarks>This quantile is equivalent to <see cref="QuartileMethod3"/></remarks>
  public record class R5
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R5();

    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => TPercent.CreateChecked(count) * Quantities.UnitInterval.AssertMember(p, IntervalNotation.Closed, nameof(p)) + TPercent.CreateChecked(0.5);

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => Edf.Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.
  }
}
