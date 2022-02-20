namespace Flux
{
  public static partial class Maths
  {
    // To find the next larger or greater than PowerOf() simply multiply the result from PowerOf() by the radix, e.g. "PowerOf(31, 2) * 2", "PowerOf(123, 10) * 10".
    // To find the next smaller or less than PowerOf() simply divide the result from PowerOf() by the radix, e.g. "PowerOf(31, 2) / 2", "PowerOf(123, 10) / 10".

    /// <summary>Returns the power of the specified number and radix.</summary>
    public static System.Numerics.BigInteger PowerOf(this System.Numerics.BigInteger value, int radix)
      => value == 0 ? 1 : System.Numerics.BigInteger.Pow(radix, DigitCount(value, radix) - 1);

    /// <summary>Returns the power of the specified number and radix.</summary>
    public static int PowerOf(this int value, int radix)
      => value == 0 ? 1 : Pow(radix, DigitCount(value, radix) - 1);

    /// <summary>Returns the power of the specified number and radix.</summary>
    public static long PowerOf(this long value, int radix)
      => value == 0 ? 1 : Pow(radix, DigitCount(value, radix) - 1);
  }
}
