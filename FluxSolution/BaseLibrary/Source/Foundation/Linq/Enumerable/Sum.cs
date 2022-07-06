namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Sum for BigInteger.</summary>
    public static System.Numerics.BigInteger Sum(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source)
      => source.AsParallel().Aggregate(System.Numerics.BigInteger.Zero, (a, e) => a + e);
  }
}
