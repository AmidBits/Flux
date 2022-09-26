namespace Flux
{
  public static partial class IntEm
  {
    public static System.Numerics.BigInteger GetMostSignificantDigit(System.Numerics.BigInteger source, int radix = 10)
      => source == 0 ? 0 : source / System.Numerics.BigInteger.Pow(radix, Maths.DigitCount(source, radix));

    public static int GetMostSignificantDigit(int source, int radix = 10)
      => source == 0 ? 0 : System.Convert.ToInt32(source / System.Math.Pow(radix, Maths.DigitCount(source, radix)));

    public static long GetMostSignificantDigit(long source, int radix = 10)
      => source == 0 ? 0 : System.Convert.ToInt64(source / System.Math.Pow(radix, Maths.DigitCount(source, radix)));
  }
}
