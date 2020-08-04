namespace Flux
{
  public static partial class Math
  {
    // To find the next larger or greater than PowerOf() simply multiply the result from PowerOf() by the radix, e.g. "PowerOf(31, 2) * 2", "PowerOf(123, 10) * 10".
    // To find the next smaller or less than PowerOf() simply divide the result from PowerOf() by the radix, e.g. "PowerOf(31, 2) / 2", "PowerOf(123, 10) / 10".

    /// <summary>Returns the power of the specified number and radix.</summary>
    public static System.Numerics.BigInteger PowerOf(System.Numerics.BigInteger value, int radix)
      => value == 0 ? value : System.Numerics.BigInteger.Pow(radix, Math.DigitCount(value, radix) - 1);

    /// <summary>Returns the power of the specified number and radix.</summary>
    public static int PowerOf(int value, int radix)
      => value != 0 ? Math.Pow(radix, Math.DigitCount(value, radix) - 1) : 1;
    /// <summary>Returns the power of the specified number and radix.</summary>
    public static long PowerOf(long value, int radix)
      => value != 0 ? Math.Pow(radix, Math.DigitCount(value, radix) - 1) : 1;

    /// <summary>Returns the power of the specified number and radix.</summary>
    [System.CLSCompliant(false)]
    public static uint PowerOf(uint value, int radix)
      => value != 0 ? Math.Pow((uint)radix, Math.DigitCount(value, radix) - 1) : 1;
    /// <summary>Returns the power of the specified number and radix.</summary>
    [System.CLSCompliant(false)]
    public static ulong PowerOf(ulong value, int radix)
      => value != 0 ? Math.Pow((ulong)radix, Math.DigitCount(value, radix) - 1) : 1;
  }
}
