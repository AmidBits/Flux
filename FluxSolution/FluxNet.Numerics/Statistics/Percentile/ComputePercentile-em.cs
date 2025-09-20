namespace FluxNet.Numerics.Statistics
{
  public static partial class Extensions
  {
    public static TPercent ComputePercentileRank<TCount, TPercent>(this TCount count, TPercent p, Percentile.PercentileAlgorithm algorithm)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        Percentile.PercentileAlgorithm.ExcelInc => Percentile.Variant2.PercentileRank(count, p),
        Percentile.PercentileAlgorithm.ExcelExc => Percentile.Variant3.PercentileRank(count, p),
        Percentile.PercentileAlgorithm.NearestRankMethod => Percentile.NearestRank.PercentileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static TPercent ComputePercentileScore<TScore, TPercent>(this System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p, Statistics.Percentile.PercentileAlgorithm algorithm)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        Percentile.PercentileAlgorithm.ExcelInc => Percentile.Variant2.PercentileScore(distribution, p),
        Percentile.PercentileAlgorithm.ExcelExc => Percentile.Variant3.PercentileScore(distribution, p),
        Percentile.PercentileAlgorithm.NearestRankMethod => Percentile.NearestRank.PercentileScore(distribution, p),
        _ => throw new NotImplementedException(),
      };
  }
}
