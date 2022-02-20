namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Determines if the number is a power of the specified radix. The sign is ignored so the function can be used on negative numbers as well.</summary>
    public static bool IsPowerOf(this System.Numerics.BigInteger value, int radix)
    {
      AssertRadix(radix);

      if (value < 0) // Make it work on negative numbers.
        value = -value;

      if (value == radix) // If the value is equal to the radix, then it's a power of that radix.
        return true;

      if (radix == 2) // Special case for binary numbers.
        return value != 0 && (value & (value - 1)) == 0;

      if (value > 1)
        while (System.Numerics.BigInteger.DivRem(value, radix, out var remainder) is var quotient && remainder == 0)
          value = quotient;

      return value == 1;
    }

    /// <summary>Determines if the number is a power of the specified radix. The sign is ignored so the function can be used on negative numbers as well.</summary>
    public static bool IsPowerOf(this int value, int radix)
    {
      AssertRadix(radix);

      if (value < 0) // Make it work on negative numbers.
        value = -value;

      if (value == radix) // If the value is equal to the radix, then it's a power of that radix.
        return true;

      if (radix == 2) // Special case for binary numbers.
        return value != 0 && (value & (value - 1)) == 0;

      if (value > 1)
        while (value % radix == 0)
          value /= radix;

      return value == 1;
    }

    /// <summary>Determines if the number is a power of the specified radix. The sign is ignored so the function can be used on negative numbers as well.</summary>
    public static bool IsPowerOf(this long value, int radix)
    {
      AssertRadix(radix);

      if (value < 0) // Make it work on negative numbers.
        value = -value;

      if (value == radix) // If the value is equal to the radix, then it's a power of that radix.
        return true;

      if (radix == 2) // Special case for binary numbers.
        return value != 0 && (value & (value - 1)) == 0;

      if (value > 1)
        while (value % radix == 0)
          value /= radix;

      return value == 1;
    }
  }
}
