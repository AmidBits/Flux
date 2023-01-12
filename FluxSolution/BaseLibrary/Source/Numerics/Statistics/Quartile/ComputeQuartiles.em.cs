namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static (TSelf q1, TSelf q2, TSelf q3) ComputeQuartiles<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> sample, QuartileAlgorithm algorithm)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => algorithm switch
      {
        QuartileAlgorithm.Empirical => Numerics.QuartileEmpirical.Compute(sample),
        QuartileAlgorithm.Method1 => Numerics.QuartileMethod1.Compute(sample),
        QuartileAlgorithm.Method2 => Numerics.QuartileMethod2.Compute(sample),
        QuartileAlgorithm.Method3 => Numerics.QuartileMethod3.Compute(sample),
        QuartileAlgorithm.Method4 => Numerics.QuartileMethod4.Compute(sample),
        _ => throw new NotImplementedException(),
      };
  }
}
