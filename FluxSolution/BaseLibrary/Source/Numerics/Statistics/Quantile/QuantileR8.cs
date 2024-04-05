namespace Flux.Statistics
{
  /// <summary>
  /// <para>Linear interpolation of the approximate medians for order statistics.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR8
    : IQuantileEstimatable
  {
    public const double OneThird = 1 / 3;

    public static IQuantileEstimatable Default => new QuantileR8();

#if NET7_0_OR_GREATER

    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => (TPercent.CreateChecked(count) + TPercent.CreateChecked(OneThird)) * Quantities.UnitInterval.AssertMember(p, IntervalNotation.Closed, nameof(p)) + TPercent.CreateChecked(OneThird);

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => QuantileEdf.Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.

#else

    public double EstimateQuantileRank(double count, double p)
    {
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      var oneThird = 1.0 / 3.0;

      return ((double)count + oneThird) * p + oneThird;
    }

    public double EstimateQuantileValue(System.Collections.Generic.IEnumerable<double> ordered, double p)
      => QuantileEdf.Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - 1); // Adjust for 0-based indexing.

#endif
  }
}
