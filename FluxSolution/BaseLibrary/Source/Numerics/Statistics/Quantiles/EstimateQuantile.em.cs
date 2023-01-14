namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static TPercent ComputeQuantileRank<TCount, TPercent>(this TCount count, TPercent p, QuantileAlgorithm algorithm)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        QuantileAlgorithm.EDF => Numerics.QuantileEdf.Default.ComputeQuantileRank(count, p),
        QuantileAlgorithm.R1 => Numerics.QuantileR1.Default.ComputeQuantileRank(count, p),
        QuantileAlgorithm.R2 => Numerics.QuantileR2.Default.ComputeQuantileRank(count, p),
        QuantileAlgorithm.R3 => Numerics.QuantileR3.Default.ComputeQuantileRank(count, p),
        QuantileAlgorithm.R4 => Numerics.QuantileR4.Default.ComputeQuantileRank(count, p),
        QuantileAlgorithm.R5 => Numerics.QuantileR5.Default.ComputeQuantileRank(count, p),
        QuantileAlgorithm.R6 => Numerics.QuantileR6.Default.ComputeQuantileRank(count, p),
        QuantileAlgorithm.R7 => Numerics.QuantileR7.Default.ComputeQuantileRank(count, p),
        QuantileAlgorithm.R8 => Numerics.QuantileR8.Default.ComputeQuantileRank(count, p),
        QuantileAlgorithm.R9 => Numerics.QuantileR9.Default.ComputeQuantileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static TPercent EstimateQuantileValue<TValue, TPercent>(this System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p, QuantileAlgorithm algorithm)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        QuantileAlgorithm.EDF => Numerics.QuantileEdf.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R1 => Numerics.QuantileR1.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R2 => Numerics.QuantileR2.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R3 => Numerics.QuantileR3.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R4 => Numerics.QuantileR4.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R5 => Numerics.QuantileR5.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R6 => Numerics.QuantileR6.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R7 => Numerics.QuantileR7.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R8 => Numerics.QuantileR8.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R9 => Numerics.QuantileR9.Default.EstimateQuantileValue(ordered, p),
        _ => throw new NotImplementedException(),
      };
  }
}
