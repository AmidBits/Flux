namespace Flux.Numerics
{
  /// <summary>
  /// <para>Piecewise linear function where the knots are the values midway through the steps of the empirical distribution function.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR5
    : IQuantile
  {
    public static IQuantile Default => new QuantileR5();

    public TPercent ComputeQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      if (TPercent.IsNegative(p) || p > TPercent.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      return TPercent.CreateChecked(count) * p + TPercent.One.Divide(2);
    }

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => QuantileEdf.Lerp(ordered, ComputeQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.
  }
}
