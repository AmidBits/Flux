namespace Flux.Quantilers
{
  /// <summary>Piecewise linear function where the knots are the values midway through the steps of the empirical distribution function.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  public record class R5
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R5();

    /// <summary>Piecewise linear function where the knots are the values midway through the steps of the empirical distribution function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var h = TSelf.CreateChecked(sample.Count()) * p + TSelf.One.Div2();

      return EmpiricalDistributionFunction.Default.EstimateQuantile(sample, h);
    }
  }
}
