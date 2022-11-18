namespace Flux.Quartiles
{
  /// <summary>Linear interpolation of the empirical distribution function.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// <see cref="https://en.wikipedia.org/wiki/Empirical_distribution_function"/>
  public record class Method3
    : IQuartileEstimatable
  {
    /// <summary>Finds the interpolated value from the specified index. The integer part signifies the low index in the sequence and the fractional portion is the interpolation percentage between the low and high (low + 1) index.</summary>
    /// <param name="sample">The sequence of values.</param>
    /// <param name="h">The index with fractions for interpolated values.</param>
    /// <returns>The value corresponding to the fractional index (the value is interpolated between the integer index, using the fractional part, and the next index).</returns>
    public (TSelf q1, TSelf q2, TSelf q3) EstimateQuartiles<TSelf>(System.Collections.Generic.IList<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      Estimate(sample, out var q1, out var q2, out var q3);

      return (q1, q2, q3);
    }

    public static void Estimate<TSelf>(System.Collections.Generic.IList<TSelf> sample, out TSelf q1, out TSelf q2, out TSelf q3)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => throw new System.NotImplementedException();
  }
}
