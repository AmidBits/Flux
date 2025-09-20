namespace FluxNet.Numerics.Statistics
{
  public static partial class Extensions
  {
    public static (double q1, double q2, double q3) ComputeQuartiles<TSelf>(this System.Collections.Generic.IEnumerable<double> sample, Statistics.Quartile.QuartileAlgorithm algorithm)
      => algorithm switch
      {
        Quartile.QuartileAlgorithm.Empirical => Quartile.Empirical.Compute(sample),
        Quartile.QuartileAlgorithm.Method1 => Quartile.Method1.Compute(sample),
        Quartile.QuartileAlgorithm.Method2 => Quartile.Method2.Compute(sample),
        Quartile.QuartileAlgorithm.Method3 => Quartile.Method3.Compute(sample),
        Quartile.QuartileAlgorithm.Method4 => Quartile.Method4.Compute(sample),
        _ => throw new NotImplementedException(),
      };
  }
}
