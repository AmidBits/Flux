#if NET7_0_OR_GREATER
namespace Flux.Quantilers
{
  /// <summary>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  public record class R9
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R9();

    /// <summary>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IList<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var h = (TSelf.CreateChecked(sample.Count + 0.25) * p) + TSelf.CreateChecked(3.0 / 8.0);

      return LinearInterpolationEmpiricalDistributionFunction.Default.EstimateQuantile(sample, h);
    }
  }
}
#endif