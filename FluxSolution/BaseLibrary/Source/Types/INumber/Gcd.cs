#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the greatest common divisor of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static TSelf Gcd<TSelf>(this TSelf[] values)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => System.Linq.Enumerable.Aggregate(values, (a, b) => a.GreatestCommonDivisor(b));
  }
}
#endif
