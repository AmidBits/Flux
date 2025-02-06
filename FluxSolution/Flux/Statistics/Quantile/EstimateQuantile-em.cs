namespace Flux
{
  public static partial class Em
  {
    public static TPercent ComputeQuantileRank<TCount, TPercent>(this TCount count, TPercent p, Statistics.Quantile.QuantileAlgorithm algorithm)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        Statistics.Quantile.QuantileAlgorithm.EDF => Statistics.Quantile.Edf.Default.EstimateQuantileRank(count, p),
        Statistics.Quantile.QuantileAlgorithm.R1 => Statistics.Quantile.R1.Default.EstimateQuantileRank(count, p),
        Statistics.Quantile.QuantileAlgorithm.R2 => Statistics.Quantile.R2.Default.EstimateQuantileRank(count, p),
        Statistics.Quantile.QuantileAlgorithm.R3 => Statistics.Quantile.R3.Default.EstimateQuantileRank(count, p),
        Statistics.Quantile.QuantileAlgorithm.R4 => Statistics.Quantile.R4.Default.EstimateQuantileRank(count, p),
        Statistics.Quantile.QuantileAlgorithm.R5 => Statistics.Quantile.R5.Default.EstimateQuantileRank(count, p),
        Statistics.Quantile.QuantileAlgorithm.R6 => Statistics.Quantile.R6.Default.EstimateQuantileRank(count, p),
        Statistics.Quantile.QuantileAlgorithm.R7 => Statistics.Quantile.R7.Default.EstimateQuantileRank(count, p),
        Statistics.Quantile.QuantileAlgorithm.R8 => Statistics.Quantile.R8.Default.EstimateQuantileRank(count, p),
        Statistics.Quantile.QuantileAlgorithm.R9 => Statistics.Quantile.R9.Default.EstimateQuantileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static TPercent EstimateQuantileValue<TValue, TPercent>(this System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p, Statistics.Quantile.QuantileAlgorithm algorithm)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        Statistics.Quantile.QuantileAlgorithm.EDF => Statistics.Quantile.Edf.Default.EstimateQuantileValue(ordered, p),
        Statistics.Quantile.QuantileAlgorithm.R1 => Statistics.Quantile.R1.Default.EstimateQuantileValue(ordered, p),
        Statistics.Quantile.QuantileAlgorithm.R2 => Statistics.Quantile.R2.Default.EstimateQuantileValue(ordered, p),
        Statistics.Quantile.QuantileAlgorithm.R3 => Statistics.Quantile.R3.Default.EstimateQuantileValue(ordered, p),
        Statistics.Quantile.QuantileAlgorithm.R4 => Statistics.Quantile.R4.Default.EstimateQuantileValue(ordered, p),
        Statistics.Quantile.QuantileAlgorithm.R5 => Statistics.Quantile.R5.Default.EstimateQuantileValue(ordered, p),
        Statistics.Quantile.QuantileAlgorithm.R6 => Statistics.Quantile.R6.Default.EstimateQuantileValue(ordered, p),
        Statistics.Quantile.QuantileAlgorithm.R7 => Statistics.Quantile.R7.Default.EstimateQuantileValue(ordered, p),
        Statistics.Quantile.QuantileAlgorithm.R8 => Statistics.Quantile.R8.Default.EstimateQuantileValue(ordered, p),
        Statistics.Quantile.QuantileAlgorithm.R9 => Statistics.Quantile.R9.Default.EstimateQuantileValue(ordered, p),
        _ => throw new NotImplementedException(),
      };
  }
}
