namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Drop the trailing digit of the number.</summary>
    public static System.Numerics.BigInteger DropTrailingDigit(this System.Numerics.BigInteger source, int radix)
      => source / AssertRadix(radix);

    /// <summary>Drop the trailing digit of the number.</summary>
    public static int DropTrailingDigit(this int source, int radix)
      => source / AssertRadix(radix);
    /// <summary>Drop the trailing digit of the number.</summary>
    public static long DropTrailingDigit(this long source, int radix)
      => source / AssertRadix(radix);
  }
}
