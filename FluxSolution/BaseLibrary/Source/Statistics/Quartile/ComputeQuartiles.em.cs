namespace Flux
{
  public static partial class Fx
  {
    public static (double q1, double q2, double q3) ComputeQuartiles<TSelf>(this System.Collections.Generic.IEnumerable<double> sample, Statistics.Quartile.QuartileAlgorithm algorithm)
      => algorithm switch
      {
        Statistics.Quartile.QuartileAlgorithm.Empirical => Statistics.Quartile.Empirical.Compute(sample),
        Statistics.Quartile.QuartileAlgorithm.Method1 => Statistics.Quartile.Method1.Compute(sample),
        Statistics.Quartile.QuartileAlgorithm.Method2 => Statistics.Quartile.Method2.Compute(sample),
        Statistics.Quartile.QuartileAlgorithm.Method3 => Statistics.Quartile.Method3.Compute(sample),
        Statistics.Quartile.QuartileAlgorithm.Method4 => Statistics.Quartile.Method4.Compute(sample),
        _ => throw new NotImplementedException(),
      };
  }
}
