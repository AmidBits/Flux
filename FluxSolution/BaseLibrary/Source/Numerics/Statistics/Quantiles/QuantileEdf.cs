namespace Flux.Numerics
{
  /// <summary>
  /// <para>Linear interpolation of the empirical distribution function.</para>
  /// <see href="https://en.wikipedia.org/wiki/Quantile"/>
  /// <see href="https://en.wikipedia.org/wiki/Empirical_distribution_function"/>
  /// </summary>
  public record class QuantileEdf
    : IQuantileEstimatable
  {
    /// <summary>Finds the interpolated value from the specified index. The integer part signifies the low index in the sequence and the fractional portion is the interpolation percentage between the low and high (low + 1) index.</summary>
    /// <param name="sample">The sequence of values.</param>
    /// <param name="h">The index with fractions for interpolated values.</param>
    /// <returns>The value corresponding to the fractional index (the value is interpolated between the integer index, using the fractional part, and the next index).</returns>
    public TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf h)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => EstimateQuantile(sample, h);

    public static TSelf Estimate<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf h)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var hf = TSelf.Floor(h); // Floor of h.
      var hc = TSelf.Ceiling(h); // Ceiling of h.

      var sf = sample.ElementAt(System.Convert.ToInt32(hf)); // Floor of sample value at hf.
      var sc = sample.ElementAt(System.Convert.ToInt32(hc)); // Ceiling of sample value at hc.

      return sf + (h - hf) * (sc - sf); // Linear interpolation.
    }
  }
}
