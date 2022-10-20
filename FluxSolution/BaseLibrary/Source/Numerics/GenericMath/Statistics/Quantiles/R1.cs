#if NET7_0_OR_GREATER
namespace Flux.Quantilers
{
  /// <see cref="https://en.wikipedia.org/wiki/Quantile"/>
  public record class R1
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new R1();

    /// <summary>Inverse of empirical distribution function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IList<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var h = TSelf.CreateChecked(sample.Count) * p;

      var index = System.Convert.ToInt32(TSelf.Ceiling(h)) - 1;

      return sample[System.Math.Clamp(index, 0, sample.Count - 1)];
    }
  }
}
#endif
