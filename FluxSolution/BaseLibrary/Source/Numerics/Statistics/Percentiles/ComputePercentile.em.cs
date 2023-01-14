namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static TPercent ComputePercentileRank<TCount, TPercent>(this TCount count, TPercent p, PercentileAlgorithm algorithm)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        PercentileAlgorithm.ExcelInc => Numerics.PercentileVariant2.PercentileRank(count, p),
        PercentileAlgorithm.ExcelExc => Numerics.PercentileVariant3.PercentileRank(count, p),
        PercentileAlgorithm.NearestRankMethod => Numerics.PercentileNearestRank.PercentileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static TPercent ComputePercentileScore<TScore, TPercent>(this System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p, PercentileAlgorithm algorithm)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        PercentileAlgorithm.ExcelInc => Numerics.PercentileVariant2.PercentileScore(distribution, p),
        PercentileAlgorithm.ExcelExc => Numerics.PercentileVariant3.PercentileScore(distribution, p),
        PercentileAlgorithm.NearestRankMethod => Numerics.PercentileNearestRank.PercentileScore(distribution, p),
        _ => throw new NotImplementedException(),
      };

  }
}
