#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public static TSelf Factorial<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x < TSelf.Zero
      ? -Factorial(-x)
      : x <= TSelf.One
      ? TSelf.One
      : Loop.Range(TSelf.One + TSelf.One, x - TSelf.One, TSelf.One).AsParallel().Aggregate(TSelf.One, (a, b) => a * b);
  }
}
#endif
