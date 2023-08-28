namespace Flux
{
  public static partial class StatisticsExtensionMethods
  {
#if NET7_0_OR_GREATER

    public static TPercent ComputePercentileRank<TCount, TPercent>(this TCount count, TPercent p, Statistics.PercentileAlgorithm algorithm)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        Statistics.PercentileAlgorithm.ExcelInc => Statistics.PercentileVariant2.PercentileRank(count, p),
        Statistics.PercentileAlgorithm.ExcelExc => Statistics.PercentileVariant3.PercentileRank(count, p),
        Statistics.PercentileAlgorithm.NearestRankMethod => Statistics.PercentileNearestRank.PercentileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static TPercent ComputePercentileScore<TScore, TPercent>(this System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p, Statistics.PercentileAlgorithm algorithm)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        Statistics.PercentileAlgorithm.ExcelInc => Statistics.PercentileVariant2.PercentileScore(distribution, p),
        Statistics.PercentileAlgorithm.ExcelExc => Statistics.PercentileVariant3.PercentileScore(distribution, p),
        Statistics.PercentileAlgorithm.NearestRankMethod => Statistics.PercentileNearestRank.PercentileScore(distribution, p),
        _ => throw new NotImplementedException(),
      };

#else

    public static double ComputePercentileRank(this double count, double p, Statistics.PercentileAlgorithm algorithm)
      => algorithm switch
      {
        Statistics.PercentileAlgorithm.ExcelInc => Statistics.PercentileVariant2.PercentileRank(count, p),
        Statistics.PercentileAlgorithm.ExcelExc => Statistics.PercentileVariant3.PercentileRank(count, p),
        Statistics.PercentileAlgorithm.NearestRankMethod => Statistics.PercentileNearestRank.PercentileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static double ComputePercentileScore(this System.Collections.Generic.IEnumerable<double> distribution, double p, Statistics.PercentileAlgorithm algorithm)
      => algorithm switch
      {
        Statistics.PercentileAlgorithm.ExcelInc => Statistics.PercentileVariant2.PercentileScore(distribution, p),
        Statistics.PercentileAlgorithm.ExcelExc => Statistics.PercentileVariant3.PercentileScore(distribution, p),
        Statistics.PercentileAlgorithm.NearestRankMethod => Statistics.PercentileNearestRank.PercentileScore(distribution, p),
        _ => throw new NotImplementedException(),
      };

#endif
  }
}
