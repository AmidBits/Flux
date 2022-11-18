namespace Flux.Quartiles
{
  /// <summary>Linear interpolation of the empirical distribution function.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
  /// <see cref="https://en.wikipedia.org/wiki/Empirical_distribution_function"/>
  public record class Empirical
    : IQuartileEstimatable
  {
    /// <summary>Finds the interpolated value from the specified index. The integer part signifies the low index in the sequence and the fractional portion is the interpolation percentage between the low and high (low + 1) index.</summary>
    /// <param name="sample">The sequence of values.</param>
    /// <param name="h">The index with fractions for interpolated values.</param>
    /// <returns>The value corresponding to the fractional index (the value is interpolated between the integer index, using the fractional part, and the next index).</returns>
    public (TSelf q1, TSelf q2, TSelf q3) EstimateQuartiles<TSelf>(System.Collections.Generic.IList<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        Estimate(sample, TSelf.CreateChecked(0.25)),
        Estimate(sample, TSelf.CreateChecked(0.50)),
        Estimate(sample, TSelf.CreateChecked(0.75))
      );

    public static TSelf Estimate<TSelf>(System.Collections.Generic.IList<TSelf> source, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var a = p * TSelf.CreateChecked(source.Count + 1);
      var k =TSelf.Truncate(a);

      a -= k;

      var c = source[System.Convert.ToInt32(k) - 1];

      return c + a * (source[System.Convert.ToInt32(k)] - c);
    }
  }
}
