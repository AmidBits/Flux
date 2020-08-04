namespace Flux
{
  public static partial class Math
  {
    /// <summary>Drop the trailing digit of the number.</summary>
    public static System.Numerics.BigInteger DropTrailingDigit(this System.Numerics.BigInteger source, int radix)
      => source / (radix >= 2 ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix)));

    /// <summary>Drop the trailing digit of the number.</summary>
    public static System.Numerics.BigInteger DropTrailingDigit(this int source, int radix)
      => source / (radix >= 2 ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix)));
    /// <summary>Drop the trailing digit of the number.</summary>
    public static System.Numerics.BigInteger DropTrailingDigit(this long source, int radix)
      => source / (radix >= 2 ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix)));

    /// <summary>Drop the trailing digit of the number.</summary>
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger DropTrailingDigit(this uint source, int radix)
      => radix >= 2 ? source / (uint)radix : throw new System.ArgumentOutOfRangeException(nameof(radix));
    /// <summary>Drop the trailing digit of the number.</summary>
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger DropTrailingDigit(this ulong source, int radix)
      => radix >= 2 ? source / (ulong)radix : throw new System.ArgumentOutOfRangeException(nameof(radix));
  }
}
