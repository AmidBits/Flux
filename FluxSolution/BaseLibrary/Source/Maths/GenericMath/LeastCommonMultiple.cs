using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the least common multiple of all values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TSelf Lcm<TSelf>(this TSelf[] values)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => values.Length >= 2
      ? values.Aggregate((a, b) => a.LeastCommonMultiple(b))
      : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the least common multiple of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TSelf LeastCommonMultiple<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => a / a.GreatestCommonDivisor(b) * b;

#else

    /// <summary>Returns the least common multiple of all values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Numerics.BigInteger Lcm(this System.Numerics.BigInteger[] values)
      => values.Length >= 2
      ? values.Aggregate((a, b) => a.LeastCommonMultiple(b))
      : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the least common multiple of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Numerics.BigInteger LeastCommonMultiple(this System.Numerics.BigInteger a, System.Numerics.BigInteger b)
      => a / a.GreatestCommonDivisor(b) * b;

#endif
  }
}
