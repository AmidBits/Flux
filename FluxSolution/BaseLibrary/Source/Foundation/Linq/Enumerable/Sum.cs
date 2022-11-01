namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Sum for BigInteger.</summary>
    public static System.Numerics.BigInteger Sum(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source)
      => source.AsParallel().Aggregate(System.Numerics.BigInteger.Zero, (a, e) => a + e);

#if NET7_0_OR_GREATER
    /// <summary>Sum for BigInteger.</summary>
    public static TSelf Sum<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.AsParallel().Aggregate(TSelf.Zero, (a, e) => a + e);
#endif
  }
}
