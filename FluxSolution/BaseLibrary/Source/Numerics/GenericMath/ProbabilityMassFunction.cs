#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    public static TSelf ProbabilityMassFunction<TSelf>(this TSelf k, TSelf n, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => (n / k) * TSelf.Pow(p, k) * TSelf.Pow(TSelf.One - p, n - k);
  }
}
#endif
