namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Numerics.BigInteger GetMostSignificantDigit(this System.Numerics.BigInteger source, int radix = 10)
      => source == 0 ? 0 : radix >= 2 ? source / System.Numerics.BigInteger.Pow(radix, Maths.DigitCountImpl(source, radix)) : throw new System.ArgumentOutOfRangeException(nameof(radix));

    public static int GetMostSignificantDigit(this int source, int radix = 10)
      => source == 0 ? 0 : radix >= 2 ? System.Convert.ToInt32(source / System.Math.Pow(radix, Maths.DigitCountImpl(source, radix))) : throw new System.ArgumentOutOfRangeException(nameof(radix));

    public static long GetMostSignificantDigit(this long source, int radix = 10)
      => source == 0 ? 0 : radix >= 2 ? System.Convert.ToInt64(source / System.Math.Pow(radix, Maths.DigitCountImpl(source, radix))) : throw new System.ArgumentOutOfRangeException(nameof(radix));
  }
}
