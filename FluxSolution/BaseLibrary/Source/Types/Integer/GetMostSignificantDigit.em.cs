namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Numerics.BigInteger GetMostSignificantDigit(this System.Numerics.BigInteger source, int radix = 10)
      => source == 0 ? 0 : source / System.Numerics.BigInteger.Pow(radix, Maths.DigitCount(source, radix));

    public static int GetMostSignificantDigit(this int source, int radix = 10)
      => source == 0 ? 0 : System.Convert.ToInt32(source / System.Math.Pow(radix, Maths.DigitCount(source, radix)));

    public static long GetMostSignificantDigit(this long source, int radix = 10)
      => source == 0 ? 0 : System.Convert.ToInt64(source / System.Math.Pow(radix, Maths.DigitCount(source, radix)));
  }
}
