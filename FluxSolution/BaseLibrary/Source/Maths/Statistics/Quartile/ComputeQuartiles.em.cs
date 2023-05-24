namespace Flux
{
  public static partial class StatisticsExtensionMethods
  {
    public static (double q1, double q2, double q3) ComputeQuartiles<TSelf>(this System.Collections.Generic.IEnumerable<double> sample, QuartileAlgorithm algorithm)
      => algorithm switch
      {
        QuartileAlgorithm.Empirical => Maths.QuartileEmpirical.Compute(sample),
        QuartileAlgorithm.Method1 => Maths.QuartileMethod1.Compute(sample),
        QuartileAlgorithm.Method2 => Maths.QuartileMethod2.Compute(sample),
        QuartileAlgorithm.Method3 => Maths.QuartileMethod3.Compute(sample),
        QuartileAlgorithm.Method4 => Maths.QuartileMethod4.Compute(sample),
        _ => throw new NotImplementedException(),
      };
  }
}
