namespace Flux
{
  public static partial class Maths
  {
    // To find the next larger or greater than PowerOf() simply multiply the result from PowerOf() by the radix, e.g. "PowerOf(31, 2) * 2", "PowerOf(123, 10) * 10".
    // To find the next smaller or less than PowerOf() simply divide the result from PowerOf() by the radix, e.g. "PowerOf(31, 2) / 2", "PowerOf(123, 10) / 10".

    /// <summary>Returns the power of the specified number and radix.</summary>
    public static System.Numerics.BigInteger PowerOf(System.Numerics.BigInteger value, int radix)
      => value == 0 ? value : System.Numerics.BigInteger.Pow(radix, Maths.DigitCount(value, radix) - 1);

    /// <summary>Returns the power of the specified number and radix.</summary>
    public static int PowerOf(int value, int radix)
      => value != 0 ? Maths.Pow(radix, Maths.DigitCount(value, radix) - 1) : 1;
    /// <summary>Returns the power of the specified number and radix.</summary>
    public static long PowerOf(long value, int radix)
      => value != 0 ? Maths.Pow(radix, Maths.DigitCount(value, radix) - 1) : 1;

    /// <summary>Returns the power of the specified number and radix.</summary>
    [System.CLSCompliant(false)]
    public static uint PowerOf(uint value, int radix)
      => value != 0 ? Maths.Pow((uint)radix, Maths.DigitCount(value, radix) - 1) : 1;
    /// <summary>Returns the power of the specified number and radix.</summary>
    [System.CLSCompliant(false)]
    public static ulong PowerOf(ulong value, int radix)
      => value != 0 ? Maths.Pow((ulong)radix, Maths.DigitCount(value, radix) - 1) : 1;
  }
}
