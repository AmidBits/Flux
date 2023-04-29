using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
#if NET7_0_OR_GREATER

    /// <summary>Compute the sum of all elements in <paramref name="source"/>.</summary>
    public static TSelf Sum<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.AsParallel().Aggregate(TSelf.Zero, (a, e) => a + e);

#else

    /// <summary>Compute the sum of all elements in <paramref name="source"/>.</summary>
    public static decimal Sum(this System.Collections.Generic.IEnumerable<decimal> source)
      => source.AsParallel().Aggregate(0m, (a, e) => a + e);

    /// <summary>Compute the sum of all elements in <paramref name="source"/>.</summary>
    public static double Sum(this System.Collections.Generic.IEnumerable<double> source)
      => source.AsParallel().Aggregate(0d, (a, e) => a + e);

    /// <summary>Compute the sum of all elements in <paramref name="source"/>.</summary>
    public static System.Numerics.BigInteger Sum(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source)
      => source.AsParallel().Aggregate(System.Numerics.BigInteger.Zero, (a, e) => a + e);

    /// <summary>Compute the sum of all elements in <paramref name="source"/>.</summary>
    public static int Sum(this System.Collections.Generic.IEnumerable<int> source)
      => source.AsParallel().Aggregate(0, (a, e) => a + e);

    /// <summary>Compute the sum of all elements in <paramref name="source"/>.</summary>
    public static long Sum(this System.Collections.Generic.IEnumerable<long> source)
      => source.AsParallel().Aggregate(0L, (a, e) => a + e);

#endif
  }
}
