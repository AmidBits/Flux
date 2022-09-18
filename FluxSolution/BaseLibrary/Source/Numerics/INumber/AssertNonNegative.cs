#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Number
  {
    /// <summary>PREVIEW! Determines if the number is a power of 2. A non-negative binary integer value x is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</summary>
    /// <remarks>The implementation is extremely fast for huge BigInteger values.</remarks>
    public static TSelf AssertNonNegativeValue<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsNonNegativeValue(value) ? value : throw new System.ArgumentOutOfRangeException(nameof(value), "Non-negative value required.");

    public static bool IsNonNegativeValue<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => value >= TSelf.Zero;
  }
}
#endif
