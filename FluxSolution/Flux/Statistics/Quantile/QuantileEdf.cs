namespace Flux.Statistics.Quantile
{
  /// <summary>
  /// <para>An empirical distribution function (commonly also called an empirical Cumulative Distribution Function, eCDF) is the distribution function associated with the empirical measure of a sample.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile"/>
  /// <see href="https://en.wikipedia.org/wiki/Empirical_distribution_function"/>
  /// </summary>
  public record class Edf
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new Edf();

    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => Units.UnitInterval.AssertWithin(p, IntervalNotation.Closed, nameof(p)) * TPercent.CreateChecked(count + TCount.One);

    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - TPercent.One);

    /// <summary>
    /// <para>Computes by linear interpolation of the EDF.</para>
    /// </summary>
    /// <param name="ordered"></param>
    /// <param name="h"></param>
    /// <returns>An estimated value.</returns>
    /// <exception cref="System.ArgumentNullException"/>
    public static TPercent Lerp<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent h)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      var hf = TPercent.Floor(h); // Floor of h.
      var hc = TPercent.Ceiling(h); // Ceiling of h.

      var indexHf = System.Convert.ToInt32(hf);
      var indexHc = System.Convert.ToInt32(hc);

      var maxIndex = ordered.Count() - 1;

      // Ensure roundings are clamped to quantile rank [0, maxIndex] range (variable 'h' on Wikipedia). There are no adjustment for 0-based indexing.
      indexHf = int.Clamp(indexHf, 0, maxIndex);
      indexHc = int.Clamp(indexHc, 0, maxIndex);

      var sf = ordered.ElementAt(indexHf); // Floor of sample value at hf.
      var sc = ordered.ElementAt(indexHc); // Ceiling of sample value at hc.

      return TPercent.CreateChecked(sf) + (h - hf) * TPercent.CreateChecked(sc - sf); // Linear interpolation.
    }
  }
}
