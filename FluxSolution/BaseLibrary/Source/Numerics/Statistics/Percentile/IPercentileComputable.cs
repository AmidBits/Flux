namespace Flux.Statistics
{
  /// <summary>
  /// <para>A k-th percentile (percentile score or centile) is a score below which a given percentage k of scores in its frequency distribution falls (exclusive definition) or a score at or below which a given percentage falls (inclusive definition).</para>
  /// <remarks>The distributions are enumerated multiple times.</remarks>
  /// <para><see href="https://en.wikipedia.org/wiki/Percentile"/></para>
  /// </summary>
  public interface IPercentileComputable
  {
#if NET7_0_OR_GREATER

    /// <summary>Computes a percentile rank according to the implementation.</summary>
    /// <returns>The computed percentile rank.</returns>
    TPercent ComputePercentileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>;

    /// <summary>Computes a percentile score according to the implementation.</summary>
    /// <returns>The computed percentile score.</returns>
    TPercent ComputePercentileScore<TScore, TPercent>(System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>;

#else

    /// <summary>Computes a percentile rank according to the implementation.</summary>
    /// <returns>The computed percentile rank.</returns>
    double ComputePercentileRank(double count, double p);

    /// <summary>Computes a percentile score according to the implementation.</summary>
    /// <returns>The computed percentile score.</returns>
    double ComputePercentileScore(System.Collections.Generic.IEnumerable<double> distribution, double p);

#endif
  }
}