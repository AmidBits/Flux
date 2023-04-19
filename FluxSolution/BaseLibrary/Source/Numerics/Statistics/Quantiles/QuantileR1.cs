using System.Linq;

namespace Flux.Numerics
{
  /// <summary>
  /// <para>Inverse of empirical distribution function.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR1
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new QuantileR1();

#if NET7_0_OR_GREATER

    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
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
      var count = ordered.Count();
      var index = System.Convert.ToInt32(TPercent.Ceiling(EstimateQuantileRank(count, p)));

      index = int.Clamp(index, 1, count) - 1; // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.

      return TPercent.CreateChecked(ordered.ElementAt(index));
    }

#else

    public double EstimateQuantileRank(double count, double p)
    {
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return (double)count * p;
    }

    public double EstimateQuantileValue(System.Collections.Generic.IEnumerable<double> ordered, double p)
    {
      var count = ordered.Count();
      var index = System.Convert.ToInt32(System.Math.Ceiling(EstimateQuantileRank(count, p)));

      index = System.Math.Clamp(index, 1, count) - 1; // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.

      return (double)ordered.ElementAt(index);
    }

#endif
  }
}
