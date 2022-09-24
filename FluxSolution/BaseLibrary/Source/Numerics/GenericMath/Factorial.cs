#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public static TSelf Factorial<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value < TSelf.Zero
      ? -Factorial(-value)
      : value <= TSelf.One
      ? TSelf.One
      : Loop.Range(TSelf.One + TSelf.One, value - TSelf.One, TSelf.One).AsParallel().Aggregate(TSelf.One, (a, b) => a * b);
  }
}
#endif
