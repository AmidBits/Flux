namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Drop the leading digit of the number.</summary>
    public static System.Numerics.BigInteger DropLeadingDigit(this System.Numerics.BigInteger source, int radix)
      => radix >= 2 ? source % System.Numerics.BigInteger.Pow(radix, DigitCount(source, radix) - 1) * source.Sign : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Drop the leading digit of the number.</summary>
    public static int DropLeadingDigit(this int source, int radix)
      => (int)DropLeadingDigit((uint)System.Math.Abs(source), radix) * System.Math.Sign(source);
    /// <summary>Drop the leading digit of the number.</summary>
    public static long DropLeadingDigit(this long source, int radix)
      => (long)DropLeadingDigit((ulong)System.Math.Abs(source), radix) * System.Math.Sign(source);

    /// <summary>Drop the leading digit of the number.</summary>
    [System.CLSCompliant(false)]
    public static uint DropLeadingDigit(this uint source, int radix)
      => radix >= 2 ? source % System.Convert.ToUInt32(System.Math.Pow(radix, DigitCount(source, radix) - 1)) : throw new System.ArgumentOutOfRangeException(nameof(radix));
    /// <summary>Drop the leading digit of the number.</summary>
    [System.CLSCompliant(false)]
    public static ulong DropLeadingDigit(this ulong source, int radix)
      => radix >= 2 ? source % System.Convert.ToUInt64(System.Math.Pow(radix, DigitCount(source, radix) - 1)) : throw new System.ArgumentOutOfRangeException(nameof(radix));
  }
}
