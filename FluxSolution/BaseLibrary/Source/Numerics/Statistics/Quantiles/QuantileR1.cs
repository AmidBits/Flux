namespace Flux.Numerics
{
  /// <summary>
  /// <para>Inverse of empirical distribution function.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR1
    : IQuantile
  {
    public static IQuantile Default => new QuantileR1();

    public TPercent ComputeQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      if (TPercent.IsNegative(p) || p > TPercent.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      return TPercent.CreateChecked(count) * p;
    }

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      var index = System.Convert.ToInt32(TPercent.Ceiling(ComputeQuantileRank(ordered.Count(), p)));

      return TPercent.CreateChecked(ordered.ElementAt(index - 1)); // Adjust for 0-based indexing.
    }
  }
}
