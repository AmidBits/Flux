//namespace Flux
//{
//  public static partial class PercentileExtensions
//  {
//    public static TPercent ComputePercentileRank<TCount, TPercent>(this TCount count, TPercent p, Statistics.Percentile.PercentileAlgorithm algorithm)
//      where TCount : System.Numerics.IBinaryInteger<TCount>
//      where TPercent : System.Numerics.IFloatingPoint<TPercent>
//      => algorithm switch
//      {
//        Statistics.Percentile.PercentileAlgorithm.ExcelInc => Statistics.Percentile.Variant2.PercentileRank(count, p),
//        Statistics.Percentile.PercentileAlgorithm.ExcelExc => Statistics.Percentile.Variant3.PercentileRank(count, p),
//        Statistics.Percentile.PercentileAlgorithm.NearestRankMethod => Statistics.Percentile.NearestRank.PercentileRank(count, p),
//        _ => throw new NotImplementedException(),
//      };

//    public static TPercent ComputePercentileScore<TScore, TPercent>(this System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p, Statistics.Percentile.PercentileAlgorithm algorithm)
//      where TScore : System.Numerics.INumber<TScore>
//      where TPercent : System.Numerics.IFloatingPoint<TPercent>
//      => algorithm switch
//      {
//        Statistics.Percentile.PercentileAlgorithm.ExcelInc => Statistics.Percentile.Variant2.PercentileScore(distribution, p),
//        Statistics.Percentile.PercentileAlgorithm.ExcelExc => Statistics.Percentile.Variant3.PercentileScore(distribution, p),
//        Statistics.Percentile.PercentileAlgorithm.NearestRankMethod => Statistics.Percentile.NearestRank.PercentileScore(distribution, p),
//        _ => throw new NotImplementedException(),
//      };
//  }
//}
