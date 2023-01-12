namespace Flux.Numerics
{
  /// <summary>
  /// <para>The observation numbered closest to Np. Here, h indicates rounding to the nearest integer, choosing the even integer in the case of a tie.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// </summary>
  public record class QuantileR3
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
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var h = TSelf.CreateChecked(sample.Count()) * p - TSelf.One.Divide(2);

      var index = System.Convert.ToInt32(TSelf.Round(h, System.MidpointRounding.ToEven)); // Round h to the nearest integer, choosing the even integer in the case of a tie.

      return sample.ElementAt(index - 1); // Adjust for 0-based indexing.
    }
  }
}
