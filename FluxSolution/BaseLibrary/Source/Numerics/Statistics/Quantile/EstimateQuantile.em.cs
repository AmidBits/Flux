namespace Flux
{
  public static partial class Fx
  {
#if NET7_0_OR_GREATER

    public static TPercent ComputeQuantileRank<TCount, TPercent>(this TCount count, TPercent p, Statistics.QuantileAlgorithm algorithm)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        Statistics.QuantileAlgorithm.EDF => Statistics.QuantileEdf.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R1 => Statistics.QuantileR1.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R2 => Statistics.QuantileR2.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R3 => Statistics.QuantileR3.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R4 => Statistics.QuantileR4.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R5 => Statistics.QuantileR5.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R6 => Statistics.QuantileR6.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R7 => Statistics.QuantileR7.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R8 => Statistics.QuantileR8.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R9 => Statistics.QuantileR9.Default.EstimateQuantileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static TPercent EstimateQuantileValue<TValue, TPercent>(this System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p, Statistics.QuantileAlgorithm algorithm)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        Statistics.QuantileAlgorithm.EDF => Statistics.QuantileEdf.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R1 => Statistics.QuantileR1.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R2 => Statistics.QuantileR2.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R3 => Statistics.QuantileR3.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R4 => Statistics.QuantileR4.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R5 => Statistics.QuantileR5.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R6 => Statistics.QuantileR6.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R7 => Statistics.QuantileR7.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R8 => Statistics.QuantileR8.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R9 => Statistics.QuantileR9.Default.EstimateQuantileValue(ordered, p),
        _ => throw new NotImplementedException(),
      };

#else
    public static double ComputeQuantileRank(this double count, double p, Statistics.QuantileAlgorithm algorithm)
      => algorithm switch
      {
        Statistics.QuantileAlgorithm.EDF => Statistics.QuantileEdf.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R1 => Statistics.QuantileR1.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R2 => Statistics.QuantileR2.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R3 => Statistics.QuantileR3.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R4 => Statistics.QuantileR4.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R5 => Statistics.QuantileR5.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R6 => Statistics.QuantileR6.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R7 => Statistics.QuantileR7.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R8 => Statistics.QuantileR8.Default.EstimateQuantileRank(count, p),
        Statistics.QuantileAlgorithm.R9 => Statistics.QuantileR9.Default.EstimateQuantileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static double EstimateQuantileValue(this System.Collections.Generic.IEnumerable<double> ordered, double p, Statistics.QuantileAlgorithm algorithm)
      => algorithm switch
      {
        Statistics.QuantileAlgorithm.EDF => Statistics.QuantileEdf.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R1 => Statistics.QuantileR1.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R2 => Statistics.QuantileR2.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R3 => Statistics.QuantileR3.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R4 => Statistics.QuantileR4.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R5 => Statistics.QuantileR5.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R6 => Statistics.QuantileR6.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R7 => Statistics.QuantileR7.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R8 => Statistics.QuantileR8.Default.EstimateQuantileValue(ordered, p),
        Statistics.QuantileAlgorithm.R9 => Statistics.QuantileR9.Default.EstimateQuantileValue(ordered, p),
        _ => throw new NotImplementedException(),
      };

#endif
  }
}
