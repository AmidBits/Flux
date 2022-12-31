namespace Flux.Numerics
{
  /// <summary>
  /// <para>Inverse of empirical distribution function.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR1
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new QuantileR1();

    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Estimate(sample, p);

    /// <summary>
    /// <para>Inverse of empirical distribution function.</para>
    /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    /// </summary>
    public static TSelf Estimate<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var sourceCount = sample.Count();

      var h = TSelf.CreateChecked(sourceCount) * p;

      var index = System.Convert.ToInt32(TSelf.Ceiling(h)) - 1;

      return sample.ElementAt(System.Math.Clamp(index, 0, sourceCount - 1));
    }
  }
}
