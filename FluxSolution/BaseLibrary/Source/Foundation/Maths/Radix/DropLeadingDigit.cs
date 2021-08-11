namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Drop the leading digit of the number.</summary>
    public static System.Numerics.BigInteger DropLeadingDigit(this System.Numerics.BigInteger source, int radix)
      => source < 0 ? -DropLeadingDigit(-source, radix) : source % System.Numerics.BigInteger.Pow(radix, DigitCount(source, radix) - 1);

    /// <summary>Drop the leading digit of the number.</summary>
    public static int DropLeadingDigit(this int source, int radix)
      => source < 0 ? -DropLeadingDigit(-source, radix) : source % (int)System.Math.Pow(radix, DigitCount(source, radix) - 1);
    /// <summary>Drop the leading digit of the number.</summary>
    public static long DropLeadingDigit(this long source, int radix)
      => source < 0 ? -DropLeadingDigit(-source, radix) : source % (long)System.Math.Pow(radix, DigitCount(source, radix) - 1);

    /// <summary>Drop the leading digit of the number.</summary>
    [System.CLSCompliant(false)]
    public static uint DropLeadingDigit(this uint source, int radix)
      => source % (uint)System.Math.Pow(radix, DigitCount(source, radix) - 1);
    /// <summary>Drop the leading digit of the number.</summary>
    [System.CLSCompliant(false)]
    public static ulong DropLeadingDigit(this ulong source, int radix)
      => source % (ulong)System.Math.Pow(radix, DigitCount(source, radix) - 1);
  }
}
