namespace Flux.Numerics
{
  /// <summary>
  /// <para>The same as R1, but with averaging at discontinuities.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR2
    : IQuantileEstimatable
  {
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Estimate(sample, p);

    /// <summary>The same as R1, but with averaging at discontinuities.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static TSelf Estimate<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var half = TSelf.One.Divide(2);

      var h = TSelf.CreateChecked(sample.Count()) * p + half;

      var indexLo = System.Convert.ToInt32(TSelf.Ceiling(h - half)); // ceiling(h - 0.5). 
      var indexHi = System.Convert.ToInt32(TSelf.Floor(h + half)); // floor(h + 0.5).

      return (sample.ElementAt(indexLo - 1) + sample.ElementAt(indexHi - 1)).Divide(2); // Adjust for 0-based indexing.
    }
  }
}
