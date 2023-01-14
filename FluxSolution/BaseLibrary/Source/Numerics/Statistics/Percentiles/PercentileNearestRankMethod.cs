namespace Flux.Numerics
{
  /// <summary>
  /// <para>One definition of percentile, often given in texts, is that the P-th percentile of a list of N ordered values (sorted from least to greatest) is the smallest value in the list such that no more than P percent of the data is strictly less than the value and at least P percent of the data is less than or equal to that value.</para>
  /// <see href="https://en.wikipedia.org/wiki/Percentile#The_nearest-rank_method"/>
  /// </summary>
  public record class PercentileNearestRank
    : IPercentileComputable
  {
    public TPercent ComputePercentileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => PercentileRank(count, p);

    public TPercent ComputePercentileScore<TScore, TPercent>(System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => PercentileScore(distribution, p);

    /// <summary>Computes the ordinal rank.</summary>
    public static TPercent PercentileRank<TCount, TPercent>(TCount count, TPercent percent)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => percent < TPercent.Zero || percent > TPercent.One
      ? throw new System.ArgumentOutOfRangeException(nameof(percent))
      : count < TCount.Zero
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : percent * TPercent.CreateChecked(count);

    /// <summary>
    /// <para>Inverse of empirical distribution function.</para>
    /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    /// </summary>
    public static TPercent PercentileScore<TScore, TPercent>(System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      if (TPercent.IsNegative(p) || p > TPercent.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      return TPercent.CreateChecked(distribution.ElementAt(System.Convert.ToInt32(TPercent.Ceiling(PercentileRank(distribution.Count(), p))) - 1));
    }
  }
}
