namespace Flux.Numerics
{
  /// <summary>
  /// <para>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR9
    : IQuantile
  {
    public static IQuantile Default => new QuantileR9();

    public TPercent ComputeQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      if (TPercent.IsNegative(p) || p > TPercent.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var oneFourth = TPercent.CreateChecked(1) / TPercent.CreateChecked(4);
      var threeEights = TPercent.CreateChecked(3) / TPercent.CreateChecked(8);

      return (TPercent.CreateChecked(count) + oneFourth) * p + threeEights;
    }

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => QuantileEdf.Lerp(ordered, ComputeQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.
  }
}
