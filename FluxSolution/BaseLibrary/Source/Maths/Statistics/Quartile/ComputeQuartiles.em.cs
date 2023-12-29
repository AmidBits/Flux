namespace Flux
{
  public static partial class Em
  {
    public static (double q1, double q2, double q3) ComputeQuartiles<TSelf>(this System.Collections.Generic.IEnumerable<double> sample, Statistics.QuartileAlgorithm algorithm)
      => algorithm switch
      {
        Statistics.QuartileAlgorithm.Empirical => Statistics.QuartileEmpirical.Compute(sample),
        Statistics.QuartileAlgorithm.Method1 => Statistics.QuartileMethod1.Compute(sample),
        Statistics.QuartileAlgorithm.Method2 => Statistics.QuartileMethod2.Compute(sample),
        Statistics.QuartileAlgorithm.Method3 => Statistics.QuartileMethod3.Compute(sample),
        Statistics.QuartileAlgorithm.Method4 => Statistics.QuartileMethod4.Compute(sample),
        _ => throw new NotImplementedException(),
      };
  }
}
