namespace Flux.Numerics
{
  /// <summary>
  /// <para>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</para>
  /// <para><remarks>Equivalent to Excel's PERCENTILE and PERCENTILE.INC and Python's optional "inclusive" method.</remarks></para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR7
    : IQuantileEstimatable
  {
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Estimate(sample, p);

    /// <summary>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static TSelf Estimate<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var h = TSelf.CreateChecked(sample.Count() - 1) * p + TSelf.One;

      return QuantileEdf.Estimate(sample, h - TSelf.One); // Adjust for 0-based indexing.
    }
  }
}
