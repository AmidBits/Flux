using System.Linq;

namespace Flux.Maths
{
  /// <summary>
  /// <para>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR9
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new QuantileR9();

#if NET7_0_OR_GREATER

    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
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
      => QuantileEdf.Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.

#else

    public double EstimateQuantileRank(double count, double p)
    {
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return ((double)count + 1.0 / 4.0) * p + 3.0 / 8.0;
    }

    public double EstimateQuantileValue(System.Collections.Generic.IEnumerable<double> ordered, double p)
      => QuantileEdf.Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - 1); // Adjust for 0-based indexing.

#endif
  }
}