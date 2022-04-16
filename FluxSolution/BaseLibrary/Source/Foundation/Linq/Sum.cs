namespace Flux
{
  public static partial class Enumerable
  {
    public static System.Numerics.BigInteger Sum(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source)
      => System.Linq.ParallelEnumerable.Aggregate(System.Linq.ParallelEnumerable.AsParallel(source), System.Numerics.BigInteger.Zero, (a, b) => a + b);
  }
}
