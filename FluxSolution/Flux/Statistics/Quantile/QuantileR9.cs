namespace Flux.Statistics.Quantile
{
  /// <summary>
  /// <para>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class R9
    : IQuantileEstimatable
  {
    public const double OneFourth = 1 / 4;
    public const double ThreeEights = 3 / 8;
    public static IQuantileEstimatable Default => new R9();

    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => (TPercent.CreateChecked(count) + TPercent.CreateChecked(OneFourth)) * Units.UnitInterval.AssertWithin(p, IntervalNotation.Closed, nameof(p)) + TPercent.CreateChecked(ThreeEights);

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => Edf.Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.
  }
}
