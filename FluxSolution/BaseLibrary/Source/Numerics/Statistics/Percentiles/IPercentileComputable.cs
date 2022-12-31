namespace Flux.Numerics
{
  /// <summary>
  /// <para>A k-th percentile (percentile score or centile) is a score below which a given percentage k of scores in its frequency distribution falls (exclusive definition) or a score at or below which a given percentage falls (inclusive definition).</para>
  /// <remarks>The distributions are enumerated multiple times.</remarks>
  /// <para><see href="https://en.wikipedia.org/wiki/Percentile"/></para>
  /// </summary>
  public interface IPercentileComputable
  {
    /// <summary>Computes a percentile according to the implementation.</summary>
    /// <returns>The computed percentile.</returns>
    TSelf ComputePercentile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> distribution, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>;
  }
}
