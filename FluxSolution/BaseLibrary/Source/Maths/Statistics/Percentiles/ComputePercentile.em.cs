namespace Flux
{
  public static partial class StatisticsExtensionMethods
  {
#if NET7_0_OR_GREATER

    public static TPercent ComputePercentileRank<TCount, TPercent>(this TCount count, TPercent p, PercentileAlgorithm algorithm)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        PercentileAlgorithm.ExcelInc => Maths.PercentileVariant2.PercentileRank(count, p),
        PercentileAlgorithm.ExcelExc => Maths.PercentileVariant3.PercentileRank(count, p),
        PercentileAlgorithm.NearestRankMethod => Maths.PercentileNearestRank.PercentileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static TPercent ComputePercentileScore<TScore, TPercent>(this System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p, PercentileAlgorithm algorithm)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        PercentileAlgorithm.ExcelInc => Maths.PercentileVariant2.PercentileScore(distribution, p),
        PercentileAlgorithm.ExcelExc => Maths.PercentileVariant3.PercentileScore(distribution, p),
        PercentileAlgorithm.NearestRankMethod => Maths.PercentileNearestRank.PercentileScore(distribution, p),
        _ => throw new NotImplementedException(),
      };

#else

    public static double ComputePercentileRank(this double count, double p, PercentileAlgorithm algorithm)
      => algorithm switch
      {
        PercentileAlgorithm.ExcelInc => Numerics.PercentileVariant2.PercentileRank(count, p),
        PercentileAlgorithm.ExcelExc => Numerics.PercentileVariant3.PercentileRank(count, p),
        PercentileAlgorithm.NearestRankMethod => Numerics.PercentileNearestRank.PercentileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static double ComputePercentileScore(this System.Collections.Generic.IEnumerable<double> distribution, double p, PercentileAlgorithm algorithm)
      => algorithm switch
      {
        PercentileAlgorithm.ExcelInc => Numerics.PercentileVariant2.PercentileScore(distribution, p),
        PercentileAlgorithm.ExcelExc => Numerics.PercentileVariant3.PercentileScore(distribution, p),
        PercentileAlgorithm.NearestRankMethod => Numerics.PercentileNearestRank.PercentileScore(distribution, p),
        _ => throw new NotImplementedException(),
      };

#endif
  }
}