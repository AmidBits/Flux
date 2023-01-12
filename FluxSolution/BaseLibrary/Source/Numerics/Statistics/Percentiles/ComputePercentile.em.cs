namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static TSelf ComputePercentile<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> distribution, TSelf p, PercentileAlgorithm algorithm)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => algorithm switch
      {
        PercentileAlgorithm.ExcelInc => Numerics.PercentileVariant2.PercentValue(distribution, p),
        PercentileAlgorithm.ExcelExc => Numerics.PercentileVariant3.PercentValue(distribution, p),
        PercentileAlgorithm.NearestRankMethod => Numerics.PercentileNearestRankMethod.PercentValue(distribution, p),
        _ => throw new NotImplementedException(),
      };

  }
}
