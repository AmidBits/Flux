namespace Flux.Numerics
{
  /// <summary>
  /// <para>Linear interpolation of the approximate medians for order statistics.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR8
    : IQuantileEstimatable
  {
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Estimate(sample, p);

    /// <summary>Linear interpolation of the approximate medians for order statistics.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static TSelf Estimate<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var oneThird = TSelf.CreateChecked(1) / TSelf.CreateChecked(3);

      var h = (TSelf.CreateChecked(sample.Count()) + oneThird) * p + oneThird;

      return QuantileEdf.Estimate(sample, h - TSelf.One); // Adjust for 0-based indexing.
    }
  }
}
