namespace Flux.Quantiles
{
  /// <summary>
  /// <para>The observation numbered closest to Np. Here, h indicates rounding to the nearest integer, choosing the even integer in the case of a tie.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile"/>
  /// </summary>
  public record class R3
    : IQuantileEstimatable
  {
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Estimate(sample, p);

    /// <summary>The observation numbered closest to Np. Here, h indicates rounding to the nearest integer, choosing the even integer in the case of a tie.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    public static TSelf Estimate<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var sampleCount = sample.Count();

      var h = TSelf.CreateChecked(sampleCount) * p - TSelf.One.Div2();

      var index = System.Convert.ToInt32(TSelf.Round(h, System.MidpointRounding.ToEven)) - 1;

      return sample.ElementAt(System.Math.Clamp(index, 0, sampleCount - 1));
    }
  }
}
