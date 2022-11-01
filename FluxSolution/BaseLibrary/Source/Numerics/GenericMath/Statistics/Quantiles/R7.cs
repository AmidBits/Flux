#if NET7_0_OR_GREATER
namespace Flux.Quantilers
{
  /// <summary>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</summary>
  /// <remarks>Equivalent to Excel's PERCENTILE and PERCENTILE.INC and Python's optional "inclusive" method.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  public record class R7
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R7();

    /// <summary>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var h = (TSelf.CreateChecked(sample.Count() - 1) * p) - TSelf.One;

      return EmpiricalDistributionFunction.Default.EstimateQuantile(sample, h);
    }
  }
}
#endif
