namespace FluxNet.Numerics.Statistics
{
  public static partial class Extensions
  {
    public static TPercent AssertPercent<TPercent>(TPercent percent)
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(percent);
      System.ArgumentOutOfRangeException.ThrowIfGreaterThan(percent, TPercent.One);

      return percent;
    }

    public static TPercent ComputeQuantileRank<TCount, TPercent>(this TCount count, TPercent p, Statistics.Quantile.QuantileAlgorithm algorithm)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        Quantile.QuantileAlgorithm.EDF => Quantile.Edf.Default.EstimateQuantileRank(count, p),
        Quantile.QuantileAlgorithm.R1 => Quantile.R1.Default.EstimateQuantileRank(count, p),
        Quantile.QuantileAlgorithm.R2 => Quantile.R2.Default.EstimateQuantileRank(count, p),
        Quantile.QuantileAlgorithm.R3 => Quantile.R3.Default.EstimateQuantileRank(count, p),
        Quantile.QuantileAlgorithm.R4 => Quantile.R4.Default.EstimateQuantileRank(count, p),
        Quantile.QuantileAlgorithm.R5 => Quantile.R5.Default.EstimateQuantileRank(count, p),
        Quantile.QuantileAlgorithm.R6 => Quantile.R6.Default.EstimateQuantileRank(count, p),
        Quantile.QuantileAlgorithm.R7 => Quantile.R7.Default.EstimateQuantileRank(count, p),
        Quantile.QuantileAlgorithm.R8 => Quantile.R8.Default.EstimateQuantileRank(count, p),
        Quantile.QuantileAlgorithm.R9 => Quantile.R9.Default.EstimateQuantileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static TPercent EstimateQuantileValue<TValue, TPercent>(this System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p, Statistics.Quantile.QuantileAlgorithm algorithm)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        Quantile.QuantileAlgorithm.EDF => Quantile.Edf.Default.EstimateQuantileValue(ordered, p),
        Quantile.QuantileAlgorithm.R1 => Quantile.R1.Default.EstimateQuantileValue(ordered, p),
        Quantile.QuantileAlgorithm.R2 => Quantile.R2.Default.EstimateQuantileValue(ordered, p),
        Quantile.QuantileAlgorithm.R3 => Quantile.R3.Default.EstimateQuantileValue(ordered, p),
        Quantile.QuantileAlgorithm.R4 => Quantile.R4.Default.EstimateQuantileValue(ordered, p),
        Quantile.QuantileAlgorithm.R5 => Quantile.R5.Default.EstimateQuantileValue(ordered, p),
        Quantile.QuantileAlgorithm.R6 => Quantile.R6.Default.EstimateQuantileValue(ordered, p),
        Quantile.QuantileAlgorithm.R7 => Quantile.R7.Default.EstimateQuantileValue(ordered, p),
        Quantile.QuantileAlgorithm.R8 => Quantile.R8.Default.EstimateQuantileValue(ordered, p),
        Quantile.QuantileAlgorithm.R9 => Quantile.R9.Default.EstimateQuantileValue(ordered, p),
        _ => throw new NotImplementedException(),
      };
  }
}
