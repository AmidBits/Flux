#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    public static bool IsPowEx<TSelf, TRadix>(this TSelf value, TRadix radix)
      where TSelf : System.Numerics.INumber<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      GenericMath.AssertRadix(radix, out TSelf tradix);

      if (TSelf.IsNegative(value)) // Make it work on negative numbers.
        return IsPowEx(-value, radix);

      if (value == tradix) // If the value is equal to the radix, then it's a power of that radix.
        return true;

      if (value > TSelf.One)
        while (value % tradix is var remainder && TSelf.IsZero(remainder))
          value /= tradix;

      return value == TSelf.One;
    }

    /// <summary>PREVIEW! Determines if the number is a power of the specified radix. The sign is ignored so the function can be used on negative numbers as well.</summary>
    public static bool IsPow<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      GenericMath.AssertRadix(radix);

      if (TSelf.IsNegative(value)) // Make it work on negative numbers.
        return IsPow(-value, radix);

      if (value == radix) // If the value is equal to the radix, then it's a power of that radix.
        return true;

      if (radix == (TSelf.One + TSelf.One)) // Special case for binary numbers.
        return BitOps.IsPow2(value);

      if (value > TSelf.One)
        while (value % radix is var remainder && TSelf.IsZero(remainder))
          value /= radix;

      return value == TSelf.One;
    }
  }
}
#endif
