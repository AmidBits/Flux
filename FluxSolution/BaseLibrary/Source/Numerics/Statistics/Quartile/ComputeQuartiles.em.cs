namespace Flux
{
  public static partial class StatisticsExtensionMethods
  {
    public static (double q1, double q2, double q3) ComputeQuartiles<TSelf>(this System.Collections.Generic.IEnumerable<double> sample, QuartileAlgorithm algorithm)
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
