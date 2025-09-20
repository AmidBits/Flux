namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Returns the least common multiple of all values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TInteger Lcm<TInteger>(this TInteger a, params TInteger[] other)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(other.Length);

      return a.LeastCommonMultiple(other.Aggregate(LeastCommonMultiple));
    }

    /// <summary>Returns the least common multiple of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TInteger LeastCommonMultiple<TInteger>(this TInteger a, TInteger b)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => a / a.GreatestCommonDivisor(b) * b;
  }
}
