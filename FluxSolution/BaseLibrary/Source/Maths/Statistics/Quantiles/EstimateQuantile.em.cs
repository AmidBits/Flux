namespace Flux
{
  public static partial class StatisticsExtensionMethods
  {
#if NET7_0_OR_GREATER

    public static TPercent ComputeQuantileRank<TCount, TPercent>(this TCount count, TPercent p, QuantileAlgorithm algorithm)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        QuantileAlgorithm.EDF => Maths.QuantileEdf.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R1 => Maths.QuantileR1.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R2 => Maths.QuantileR2.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R3 => Maths.QuantileR3.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R4 => Maths.QuantileR4.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R5 => Maths.QuantileR5.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R6 => Maths.QuantileR6.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R7 => Maths.QuantileR7.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R8 => Maths.QuantileR8.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R9 => Maths.QuantileR9.Default.EstimateQuantileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static TPercent EstimateQuantileValue<TValue, TPercent>(this System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p, QuantileAlgorithm algorithm)
      where TValue : System.Numerics.INumber<TValue>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => algorithm switch
      {
        QuantileAlgorithm.EDF => Maths.QuantileEdf.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R1 => Maths.QuantileR1.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R2 => Maths.QuantileR2.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R3 => Maths.QuantileR3.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R4 => Maths.QuantileR4.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R5 => Maths.QuantileR5.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R6 => Maths.QuantileR6.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R7 => Maths.QuantileR7.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R8 => Maths.QuantileR8.Default.EstimateQuantileValue(ordered, p),
        QuantileAlgorithm.R9 => Maths.QuantileR9.Default.EstimateQuantileValue(ordered, p),
        _ => throw new NotImplementedException(),
      };

#else
    public static double ComputeQuantileRank(this double count, double p, QuantileAlgorithm algorithm)
      => algorithm switch
      {
        QuantileAlgorithm.EDF => Numerics.QuantileEdf.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R1 => Numerics.QuantileR1.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R2 => Numerics.QuantileR2.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R3 => Numerics.QuantileR3.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R4 => Numerics.QuantileR4.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R5 => Numerics.QuantileR5.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R6 => Numerics.QuantileR6.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R7 => Numerics.QuantileR7.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R8 => Numerics.QuantileR8.Default.EstimateQuantileRank(count, p),
        QuantileAlgorithm.R9 => Numerics.QuantileR9.Default.EstimateQuantileRank(count, p),
        _ => throw new NotImplementedException(),
      };

    public static double EstimateQuantileValue(this System.Collections.Generic.IEnumerable<double> ordered, double p, QuantileAlgorithm algorithm)
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

#endif
  }
}
