namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static TSelf EstimateQuantile<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> sample, TSelf p, QuantileAlgorithm algorithm)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => algorithm switch
      {
        QuantileAlgorithm.EDF => Numerics.QuantileEdf.Estimate(sample, p),
        QuantileAlgorithm.R1 => Numerics.QuantileR1.Estimate(sample, p),
        QuantileAlgorithm.R2 => Numerics.QuantileR2.Estimate(sample, p),
        QuantileAlgorithm.R3 => Numerics.QuantileR3.Estimate(sample, p),
        QuantileAlgorithm.R4 => Numerics.QuantileR4.Estimate(sample, p),
        QuantileAlgorithm.R5 => Numerics.QuantileR5.Estimate(sample, p),
        QuantileAlgorithm.R6 => Numerics.QuantileR6.Estimate(sample, p),
        QuantileAlgorithm.R7 => Numerics.QuantileR7.Estimate(sample, p),
        QuantileAlgorithm.R8 => Numerics.QuantileR8.Estimate(sample, p),
        QuantileAlgorithm.R9 => Numerics.QuantileR9.Estimate(sample, p),
        _ => throw new NotImplementedException(),
      };
  }
}
