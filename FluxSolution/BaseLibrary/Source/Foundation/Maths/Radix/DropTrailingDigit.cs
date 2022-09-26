namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Drop the trailing digit of the number.</summary>
    public static System.Numerics.BigInteger DropTrailingDigit(System.Numerics.BigInteger source, int radix)
      => source / AssertRadix(radix);

    /// <summary>Drop the trailing digit of the number.</summary>
    public static int DropTrailingDigit(int source, int radix)
      => source / AssertRadix(radix);
    /// <summary>Drop the trailing digit of the number.</summary>
    public static long DropTrailingDigit(long source, int radix)
      => source / AssertRadix(radix);
  }
}
