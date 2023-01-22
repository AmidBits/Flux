namespace Flux.Numerics
{
  /// <summary>
  /// <para>The observation numbered closest to Np. Here, h indicates rounding to the nearest integer, choosing the even integer in the case of a tie.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR3
    : IQuantile
  {
    public static IQuantile Default => new QuantileR3();

    public TPercent ComputeQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      if (TPercent.IsNegative(p) || p > TPercent.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      return TPercent.CreateChecked(count) * p - TPercent.One.Divide(2);
    }

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      var count = ordered.Count();
      var index = System.Convert.ToInt32(TPercent.Round(ComputeQuantileRank(count, p), System.MidpointRounding.ToEven)); // Round h to the nearest integer, choosing the even integer in the case of a tie.

      index = int.Clamp(index, 0, count - 1); // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.

      return TPercent.CreateChecked(ordered.ElementAt(index));
    }
  }
}
