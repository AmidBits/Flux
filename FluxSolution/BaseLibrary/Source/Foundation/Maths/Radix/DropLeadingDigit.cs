namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Drop the leading digit of the number.</summary>
    public static System.Numerics.BigInteger DropLeadingDigit(this System.Numerics.BigInteger source, int radix)
      => IsSingleDigit(source) ? 0 : source % System.Numerics.BigInteger.Pow(radix, DigitCount(source, radix) - 1);

    /// <summary>Drop the leading digit of the number.</summary>
    public static int DropLeadingDigit(this int source, int radix)
      => IsSingleDigit(source) ? 0 : source % System.Convert.ToInt32(System.Math.Pow(radix, DigitCount(source, radix) - 1));
    /// <summary>Drop the leading digit of the number.</summary>
    public static long DropLeadingDigit(this long source, int radix)
      => IsSingleDigit(source) ? 0 : source % System.Convert.ToInt64(System.Math.Pow(radix, DigitCount(source, radix) - 1));
  }
}
