namespace Flux.Statistics.Percentile
{
  /// <summary>
  /// <para>One definition of percentile, often given in texts, is that the P-th percentile of a list of N ordered values (sorted from least to greatest) is the smallest value in the list such that no more than P percent of the data is strictly less than the value and at least P percent of the data is less than or equal to that value.</para>
  /// <see href="https://en.wikipedia.org/wiki/Percentile#The_nearest-rank_method"/>
  /// </summary>
  public record class NearestRank
    : IPercentileComputable
  {
    /// <summary>Computes the ordinal rank.</summary>
    public static TPercent PercentileRank<TCount, TPercent>(TCount count, TPercent percent)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      //=> IntervalNotation.Closed.AssertValidMember(percent, TPercent.Zero, TPercent.One, nameof(percent)) * TPercent.CreateChecked(count.AssertNonNegative());
      => Units.UnitInterval.AssertWithin(percent, IntervalNotation.Closed, nameof(percent)) * TPercent.CreateChecked(count.AssertNonNegativeNumber());

    /// <summary>
    /// <para>Inverse of empirical distribution function.</para>
    /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    /// </summary>
    public static TPercent PercentileScore<TScore, TPercent>(System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => TPercent.CreateChecked(distribution.ElementAt(System.Convert.ToInt32(TPercent.Ceiling(PercentileRank(distribution.Count(), Units.UnitInterval.AssertWithin(p, IntervalNotation.Closed, nameof(p))))) - 1));

    public TPercent ComputePercentileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => PercentileRank(count, p);

    public TPercent ComputePercentileScore<TScore, TPercent>(System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => PercentileScore(distribution, p);
  }
}
