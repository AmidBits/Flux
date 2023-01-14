namespace Flux.Numerics
{
  /// <summary>
  /// <para>Linear interpolation of the approximate medians for order statistics.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR8
    : IQuantile
  {
    public static IQuantile Default => new QuantileR8();

    public TPercent ComputeQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      if (TPercent.IsNegative(p) || p > TPercent.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var oneThird = TPercent.CreateChecked(1) / TPercent.CreateChecked(3);

      return (TPercent.CreateChecked(count) + oneThird) * p + oneThird;
    }

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => QuantileEdf.Lerp(ordered, ComputeQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.
  }
}
