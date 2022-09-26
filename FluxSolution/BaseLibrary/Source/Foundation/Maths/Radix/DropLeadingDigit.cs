namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Drop the leading digit of the number.</summary>
    public static System.Numerics.BigInteger DropLeadingDigit(System.Numerics.BigInteger source, int radix)
      => DigitCount(source, radix) is var dc && dc <= 1 ? 0 : source % System.Numerics.BigInteger.Pow(radix, dc - 1);

    /// <summary>Drop the leading digit of the number.</summary>
    public static int DropLeadingDigit(int source, int radix)
      => DigitCount(source, radix) is var dc && dc <= 1 ? 0 : source % System.Convert.ToInt32(System.Math.Pow(radix, dc - 1));
    /// <summary>Drop the leading digit of the number.</summary>
    public static long DropLeadingDigit(long source, int radix)
      => DigitCount(source, radix) is var dc && dc <= 1 ? 0 : source % System.Convert.ToInt64(System.Math.Pow(radix, dc - 1));
  }
}
