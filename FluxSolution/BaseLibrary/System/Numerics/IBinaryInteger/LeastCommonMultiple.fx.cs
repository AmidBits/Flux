namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the least common multiple of all values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TValue Lcm<TValue>(this TValue a, params TValue[] other)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => other.Length >= 1
      ? a.LeastCommonMultiple(other.Aggregate((b, c) => b.LeastCommonMultiple(c)))
      : throw new System.ArgumentOutOfRangeException(nameof(other));

    /// <summary>Returns the least common multiple of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TValue LeastCommonMultiple<TValue>(this TValue a, TValue b)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => a / a.GreatestCommonDivisor(b) * b;
  }
}
