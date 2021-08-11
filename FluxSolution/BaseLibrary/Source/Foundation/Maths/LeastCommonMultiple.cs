namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Numerics.BigInteger LeastCommonMultiple(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
      => a / System.Numerics.BigInteger.GreatestCommonDivisor(a, b) * b;

    /// <summary>Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static int LeastCommonMultiple(int a, int b)
      => a / GreatestCommonDivisor(a, b) * b;
    /// <summary>Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static long LeastCommonMultiple(long a, long b)
      => a / GreatestCommonDivisor(a, b) * b;

    /// <summary>Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    [System.CLSCompliant(false)]
    public static uint LeastCommonMultiple(uint a, uint b)
      => a / GreatestCommonDivisor(a, b) * b;
    /// <summary>Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    [System.CLSCompliant(false)]
    public static ulong LeastCommonMultiple(ulong a, ulong b)
      => a / GreatestCommonDivisor(a, b) * b;
  }
}
