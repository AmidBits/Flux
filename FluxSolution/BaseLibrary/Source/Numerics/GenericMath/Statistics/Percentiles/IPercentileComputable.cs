namespace Flux
{
  /// <summary>
  /// <para>A k-th percentile (percentile score or centile) is a score below which a given percentage k of scores in its frequency distribution falls (exclusive definition) or a score at or below which a given percentage falls (inclusive definition).</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Percentile"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Percentile#The_linear_interpolation_between_closest_ranks_method"/></para>
  /// </summary>
  public interface IPercentileComputable
  {
    /// <summary>Computes a percentile according to the implementation.</summary>
    /// <returns>The computed percentile.</returns>
    TSelf ComputePercentile<TSelf>(System.Collections.Generic.IList<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>;
  }
}
