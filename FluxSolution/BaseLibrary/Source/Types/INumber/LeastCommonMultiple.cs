#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TSelf Lcm<TSelf>(this TSelf[] values)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => System.Linq.Enumerable.Aggregate(values, (a, b) => a.LeastCommonMultiple(b));

    /// <summary>PREVIEW! Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static TSelf LeastCommonMultiple<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => a / a.GreatestCommonDivisor(b) * b;
  }
}
#endif
