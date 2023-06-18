using System.Linq;

namespace Flux.Maths
{
  /// <summary>
  /// <para>The same as R1, but with averaging at discontinuities.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR2
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new QuantileR2();

#if NET7_0_OR_GREATER

    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => TPercent.CreateChecked(count) * GenericMath.AssertUnitInterval(p, nameof(p)) + TPercent.One.Divide(2);

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      var count = ordered.Count();

      var h = EstimateQuantileRank(count, p);

      var half = TPercent.One.Divide(2);

      var indexLo = System.Convert.ToInt32(TPercent.Ceiling(h - half)); // ceiling(h - 0.5).
      var indexHi = System.Convert.ToInt32(TPercent.Floor(h + half)); // floor(h + 0.5).

      // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.
      indexLo = int.Clamp(indexLo, 1, count) - 1;
      indexHi = int.Clamp(indexHi, 1, count) - 1;

      return TPercent.CreateChecked(ordered.ElementAt(indexLo) + ordered.ElementAt(indexHi)).Divide(2);
    }

#else

    public double EstimateQuantileRank(double count, double p)
    {
      if (p < 0 || p > 1) throw new System.ArgumentOutOfRangeException(nameof(p));

      return (double)count * p + 0.5;
    }

    public double EstimateQuantileValue(System.Collections.Generic.IEnumerable<double> ordered, double p)
    {
      var count = ordered.Count();

      var h = EstimateQuantileRank(count, p);

      var indexLo = System.Convert.ToInt32(System.Math.Ceiling(h - 0.5)); // ceiling(h - 0.5).
      var indexHi = System.Convert.ToInt32(System.Math.Floor(h + 0.5)); // floor(h + 0.5).

      // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.
      indexLo = System.Math.Clamp(indexLo, 1, count) - 1;
      indexHi = System.Math.Clamp(indexHi, 1, count) - 1;

      return (double)(ordered.ElementAt(indexLo) + ordered.ElementAt(indexHi)) / 2;
    }

#endif
  }
}
