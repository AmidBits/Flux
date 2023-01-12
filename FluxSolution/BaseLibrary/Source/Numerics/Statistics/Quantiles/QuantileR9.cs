namespace Flux.Numerics
{
  /// <summary>
  /// <para>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR9
    : IQuantileEstimatable
  {
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Estimate(sample, p);

    /// <summary>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static TSelf Estimate<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var oneFourth = TSelf.CreateChecked(1) / TSelf.CreateChecked(4);
      var threeEights = TSelf.CreateChecked(3) / TSelf.CreateChecked(8);

      var h = (TSelf.CreateChecked(sample.Count()) + oneFourth) * p + threeEights;

      return QuantileEdf.Estimate(sample, h - TSelf.One); // Adjust for 0-based indexing.
    }
  }
}
