namespace Flux.Statistics.Quantile
{
  /// <summary>
  /// <para>The same as R1, but with averaging at discontinuities.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class R2
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R2();

    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => TPercent.CreateChecked(count) * Units.UnitInterval.AssertMember(p, IntervalNotation.Closed, nameof(p)) + TPercent.CreateChecked(0.5);

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      var count = ordered.Count();

      var h = EstimateQuantileRank(count, p);

      var half = TPercent.CreateChecked(0.5);

      var indexLo = System.Convert.ToInt32(TPercent.Ceiling(h - half)); // ceiling(h - 0.5).
      var indexHi = System.Convert.ToInt32(TPercent.Floor(h + half)); // floor(h + 0.5).

      // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.
      indexLo = int.Clamp(indexLo, 1, count) - 1;
      indexHi = int.Clamp(indexHi, 1, count) - 1;

      return TPercent.CreateChecked(ordered.ElementAt(indexLo) + ordered.ElementAt(indexHi)) / TPercent.CreateChecked(2);
    }
  }
}
