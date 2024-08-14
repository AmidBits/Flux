namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the least common multiple of all values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TSelf Lcm<TSelf>(this TSelf a, params TSelf[] other)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => other.Length >= 1
      ? a.LeastCommonMultiple(other.Aggregate((b, c) => b.LeastCommonMultiple(c)))
      : throw new System.ArgumentOutOfRangeException(nameof(other));

    /// <summary>Returns the least common multiple of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TSelf LeastCommonMultiple<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => a / a.GreatestCommonDivisor(b) * b;
  }
}
