namespace Flux.Numerics
{
  /// <summary>
  /// <para>The same as R1, but with averaging at discontinuities.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR2
    : IQuantile
  {
    public static IQuantile Default => new QuantileR2();

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
    {
      var h = ComputeQuantileRank(ordered.Count(), p);

      var half = TPercent.One.Divide(2);

      var indexLo = System.Convert.ToInt32(TPercent.Ceiling(h - half)); // ceiling(h - 0.5).
      var indexHi = System.Convert.ToInt32(TPercent.Floor(h + half)); // floor(h + 0.5).

      return TPercent.CreateChecked(ordered.ElementAt(indexLo - 1) + ordered.ElementAt(indexHi - 1)).Divide(2); // Adjust for 0-based indexing.
    }
  }
}
