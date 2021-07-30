namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Numerics.BigInteger Lcm(params System.Numerics.BigInteger[] values)
      => System.Linq.Enumerable.Aggregate(values, LeastCommonMultiple);

    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static int Lcm(params int[] values)
      => System.Linq.Enumerable.Aggregate(values, LeastCommonMultiple);
    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static long Lcm(params long[] values)
      => System.Linq.Enumerable.Aggregate(values, LeastCommonMultiple);

    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    [System.CLSCompliant(false)]
    public static uint Lcm(params uint[] values)
      => System.Linq.Enumerable.Aggregate(values, LeastCommonMultiple);
    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    [System.CLSCompliant(false)]
    public static ulong Lcm(params ulong[] values)
      => System.Linq.Enumerable.Aggregate(values, LeastCommonMultiple);
  }
}
