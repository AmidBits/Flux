namespace Flux
{
  public static partial class Math
  {
    /// <summary>Drop the leading digit of the number.</summary>
    public static System.Numerics.BigInteger DropLeadingDigit(this System.Numerics.BigInteger source, int radix)
      => radix >= 2 ? source % (System.Numerics.BigInteger)System.Math.Pow(radix, System.Math.Floor(System.Numerics.BigInteger.Log(source, radix))) : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Drop the leading digit of the number.</summary>
    public static int DropLeadingDigit(this int source, int radix)
      => radix >= 2 ? source % (int)System.Math.Pow(radix, System.Math.Floor(System.Math.Log(source, radix))) : throw new System.ArgumentOutOfRangeException(nameof(radix));
    /// <summary>Drop the leading digit of the number.</summary>
    public static long DropLeadingDigit(this long source, int radix)
      => radix >= 2 ? source % (long)System.Math.Pow(radix, System.Math.Floor(System.Math.Log(source, radix))) : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Drop the leading digit of the number.</summary>
    [System.CLSCompliant(false)]
    public static uint DropLeadingDigit(this uint source, int radix)
      => radix >= 2 ? source % (uint)System.Math.Pow(radix, System.Math.Floor(System.Math.Log(source, radix))) : throw new System.ArgumentOutOfRangeException(nameof(radix));
    /// <summary>Drop the leading digit of the number.</summary>
    [System.CLSCompliant(false)]
    public static ulong DropLeadingDigit(this ulong source, int radix)
      => radix >= 2 ? source % (ulong)System.Math.Pow(radix, System.Math.Floor(System.Math.Log(source, radix))) : throw new System.ArgumentOutOfRangeException(nameof(radix));
  }
}
