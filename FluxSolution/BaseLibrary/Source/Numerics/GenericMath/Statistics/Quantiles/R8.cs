namespace Flux.Quantilers
{
  /// <summary>Linear interpolation of the approximate medians for order statistics.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  public record class R8
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R8();

    /// <summary>Linear interpolation of the approximate medians for order statistics.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var h = (TSelf.CreateChecked(sample.Count() + 1.0 / 3.0) * p) + TSelf.CreateChecked(1.0 / 3.0);

      return EmpiricalDistributionFunction.Default.EstimateQuantile(sample, h);
    }
  }
}
