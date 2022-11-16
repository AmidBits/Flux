namespace Flux.Quantilers
{
  /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
  public record class R4
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R4();

    /// <summary>Linear interpolation of the empirical distribution function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var h = TSelf.CreateChecked(sample.Count()) * p;

      return EmpiricalDistributionFunction.Default.EstimateQuantile(sample, h);
    }
  }
}
