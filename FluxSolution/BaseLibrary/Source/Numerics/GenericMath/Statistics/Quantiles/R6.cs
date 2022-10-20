#if NET7_0_OR_GREATER
namespace Flux.Quantilers
{
  /// <summary>Linear interpolation of the expectations for the order statistics for the uniform distribution on [0,1]. That is, it is the linear interpolation between points (ph, xh), where ph = h/(N+1) is the probability that the last of (N+1) randomly drawn values will not exceed the h-th smallest of the first N randomly drawn values.</summary>
  /// <remarks>Equivalent to Excel's PERCENTILE.EXC and Python's default "exclusive" method</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  public record class R6
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R6();

    /// <summary>Linear interpolation of the expectations for the order statistics for the uniform distribution on [0,1]. That is, it is the linear interpolation between points (ph, xh), where ph = h/(N+1) is the probability that the last of (N+1) randomly drawn values will not exceed the h-th smallest of the first N randomly drawn values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IList<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var h = TSelf.CreateChecked(sample.Count + 1) * p;

      return LinearInterpolationEmpiricalDistributionFunction.Default.EstimateQuantile(sample, h);
    }
  }
}
#endif
