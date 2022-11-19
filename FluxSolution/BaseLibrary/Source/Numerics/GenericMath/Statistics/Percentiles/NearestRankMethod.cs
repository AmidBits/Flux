namespace Flux.Percentiles
{
  /// <summary>
  /// <para>One definition of percentile, often given in texts, is that the P-th percentile of a list of N ordered values (sorted from least to greatest) is the smallest value in the list such that no more than P percent of the data is strictly less than the value and at least P percent of the data is less than or equal to that value.</para>
  /// <see href="https://en.wikipedia.org/wiki/Percentile#The_nearest-rank_method"/>
  /// </summary>
  public record class NearestRankMethod
    : IPercentileComputable
  {
    public TSelf ComputePercentile<TSelf>(System.Collections.Generic.IList<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => PercentValue(sample, p);

    /// <summary>Computes the ordinal rank.</summary>
    public static TPercent PercentRank<TPercent, TCount>(TPercent percent, TCount count)
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => percent < TPercent.Zero || percent > TPercent.One
      ? throw new System.ArgumentOutOfRangeException(nameof(percent))
      : count < TCount.Zero
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : TPercent.Ceiling(percent * TPercent.CreateChecked(count));

    /// <summary>
    /// <para>Inverse of empirical distribution function.</para>
    /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    /// </summary>
    public static TSelf PercentValue<TSelf>(System.Collections.Generic.IList<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      return sample[System.Convert.ToInt32(PercentRank(p, sample.Count))];
    }
  }
}
