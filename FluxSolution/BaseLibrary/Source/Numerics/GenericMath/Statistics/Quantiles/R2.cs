namespace Flux.Quantilers
{
  /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
  public record class R2
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R2();

    /// <summary>The same as R1, but with averaging at discontinuities.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var half = TSelf.One.Div2();

      var sourceCount = sample.Count();

      var h = TSelf.CreateChecked(sourceCount) * p + half;

      var indexLo = System.Convert.ToInt32(TSelf.Ceiling(h - half)) - 1;
      var indexHi = System.Convert.ToInt32(TSelf.Floor(h + half)) - 1;

      var sourceCountM1 = sourceCount - 1;

      return (sample.ElementAt(System.Math.Clamp(indexLo, 0, sourceCountM1)) + sample.ElementAt(System.Math.Clamp(indexHi, 0, sourceCountM1))).Div2();
    }
  }
}
