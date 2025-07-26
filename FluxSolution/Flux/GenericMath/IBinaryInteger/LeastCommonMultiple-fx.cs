namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the least common multiple of all values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TInteger Lcm<TInteger>(this TInteger a, params TInteger[] other)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => other.Length >= 1
      ? a.LeastCommonMultiple(other.Aggregate((b, c) => b.LeastCommonMultiple(c)))
      : throw new System.ArgumentOutOfRangeException(nameof(other));

    /// <summary>Returns the least common multiple of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TInteger LeastCommonMultiple<TInteger>(this TInteger a, TInteger b)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => a / a.GreatestCommonDivisor(b) * b;
  }
}
