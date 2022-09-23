#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Determines if the number is a power of the specified radix. The sign is ignored so the function can be used on negative numbers as well.</summary>
    public static bool IsPow<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      if (value < TSelf.Zero) // Make it work on negative numbers.
        return IsPow(-value, radix);

      if (value == radix) // If the value is equal to the radix, then it's a power of that radix.
        return true;

      if (radix == (TSelf.One + TSelf.One)) // Special case for binary numbers.
        return value != TSelf.Zero && (value & (value - TSelf.One)) == TSelf.Zero;

      if (value > TSelf.One)
        while (value % radix is var remainder && remainder == TSelf.Zero)
          value /= radix;

      return value == TSelf.One;
    }
  }
}
#endif
