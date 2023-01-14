namespace Flux.Numerics
{
  /// <summary>
  /// <para>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</para>
  /// <para><remarks>Equivalent to Excel's PERCENTILE and PERCENTILE.INC and Python's optional "inclusive" method.</remarks></para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR7
    : IQuantile
  {
    public static IQuantile Default => new QuantileR7();

    public TPercent ComputeQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      if (TPercent.IsNegative(p) || p > TPercent.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      return TPercent.CreateChecked(count - TCount.One) * p + TPercent.One;
    }

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => QuantileEdf.Lerp(ordered, ComputeQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.
  }
}
