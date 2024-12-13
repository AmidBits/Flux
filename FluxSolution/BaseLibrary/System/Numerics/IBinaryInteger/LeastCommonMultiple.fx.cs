namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the least common multiple of all values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TNumber Lcm<TNumber>(this TNumber a, params TNumber[] other)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => other.Length >= 1
      ? a.LeastCommonMultiple(other.Aggregate((b, c) => b.LeastCommonMultiple(c)))
      : throw new System.ArgumentOutOfRangeException(nameof(other));

    /// <summary>Returns the least common multiple of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TNumber LeastCommonMultiple<TNumber>(this TNumber a, TNumber b)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => a / a.GreatestCommonDivisor(b) * b;
  }
}
