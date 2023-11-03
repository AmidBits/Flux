using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
#if NET7_0_OR_GREATER

    /// <summary>Compute the average of all elements in <paramref name="source"/>.</summary>
    public static TSelf Avg<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.AsParallel().Aggregate(() => TSelf.Zero, (a, e, i) => a + e, (a, i) => a / TSelf.CreateChecked(i));

#else

    /// <summary>Compute the average of all elements in <paramref name="source"/>.</summary>
    public static decimal Avg(this System.Collections.Generic.IEnumerable<decimal> source)
      => source.AsParallel().Aggregate(() => 0m, (a, e, i) => a + e, (a, i) => a / (decimal)i);

    /// <summary>Compute the average of all elements in <paramref name="source"/>.</summary>
    public static double Avg(this System.Collections.Generic.IEnumerable<double> source)
      => source.AsParallel().Aggregate(() => 0d, (a, e, i) => a + e, (a, i) => a / (double)i);

    /// <summary>Compute the average of all elements in <paramref name="source"/>.</summary>
    public static System.Numerics.BigInteger Avg(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source)
      => source.AsParallel().Aggregate(() => System.Numerics.BigInteger.Zero, (a, e, i) => a + e, (a, i) => a / (System.Numerics.BigInteger)i);

    /// <summary>Compute the average of all elements in <paramref name="source"/>.</summary>
    public static int Avg(this System.Collections.Generic.IEnumerable<int> source)
      => source.AsParallel().Aggregate(() => 0, (a, e, i) => a + e, (a, i) => a / i);

    /// <summary>Compute the average of all elements in <paramref name="source"/>.</summary>
    public static long Avg(this System.Collections.Generic.IEnumerable<long> source)
      => source.AsParallel().Aggregate(() => 0L, (a, e, i) => a + e, (a, i) => a / i);

#endif
  }
}
