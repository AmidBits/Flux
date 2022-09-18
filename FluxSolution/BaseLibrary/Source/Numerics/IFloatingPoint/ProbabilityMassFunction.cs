namespace Flux
{
  public static partial class FloatingPoint
  {
    public static TSelf ProbabilityMassFunction<TSelf>(this TSelf k, TSelf n, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => (n / k) * TSelf.Pow(p, k) * TSelf.Pow(TSelf.One - p, n - k);
  }
}
