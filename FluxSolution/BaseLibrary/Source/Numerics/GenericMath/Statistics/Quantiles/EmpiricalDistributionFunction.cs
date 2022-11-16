namespace Flux.Quantilers
{
  /// <summary>Linear interpolation of the empirical distribution function.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// <see cref="https://en.wikipedia.org/wiki/Empirical_distribution_function"/>
  public record class EmpiricalDistributionFunction
    : IQuantileEstimatable
  {
    public static IQuantileEstimatable Default => new EmpiricalDistributionFunction();

    /// <summary>Finds the interpolated value from the specified index. The integer part signifies the low index in the sequence and the fractional portion is the interpolation percentage between the low and high (low + 1) index.</summary>
    /// <param name="sample">The sequence of values.</param>
    /// <param name="h">The index with fractions for interpolated values.</param>
    /// <returns>The value corresponding to the fractional index (the value is interpolated between the integer index, using the fractional part, and the next index).</returns>
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf h)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var hf = TSelf.Floor(h); // Floor of h.
      var hc = TSelf.Ceiling(h); // Ceiling of h.

      var sourceCountM1 = sample.Count() - 1;

      var sf = sample.ElementAt(System.Math.Clamp(System.Convert.ToInt32(hf), 0, sourceCountM1)); // Floor of sample value at hf.
      var sc = sample.ElementAt(System.Math.Clamp(System.Convert.ToInt32(hc), 0, sourceCountM1)); // Ceiling of sample value at hc.

      return sf + (h - hf) * (sc - sf); // Linear interpolation.
    }
  }
}
