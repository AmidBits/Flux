namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    public static int DigitCount(this System.Numerics.BigInteger value, int radix)
      => value == 0
      ? 0
      : System.Convert.ToInt32(System.Math.Floor(System.Numerics.BigInteger.Log(System.Numerics.BigInteger.Abs(value), AssertRadix(radix))) + 1);

    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    public static int DigitCount(this int value, int radix)
      => value == 0
      ? 0
      : System.Convert.ToInt32(System.Math.Floor(System.Math.Log(System.Math.Abs(value), AssertRadix(radix))) + 1);
    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    public static int DigitCount(this long value, int radix)
      => value == 0
      ? 0
      : System.Convert.ToInt32(System.Math.Floor(System.Math.Log(System.Math.Abs(value), AssertRadix(radix))) + 1);
  }
}
