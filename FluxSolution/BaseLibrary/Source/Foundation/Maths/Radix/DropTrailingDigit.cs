namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Drop the trailing digit of the number.</summary>
    public static System.Numerics.BigInteger DropTrailingDigit(this System.Numerics.BigInteger source, int radix)
      => radix >= 2 ? source / radix : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Drop the trailing digit of the number.</summary>
    public static int DropTrailingDigit(this int source, int radix)
      => radix >= 2 ? source / radix : throw new System.ArgumentOutOfRangeException(nameof(radix));
    /// <summary>Drop the trailing digit of the number.</summary>
    public static long DropTrailingDigit(this long source, int radix)
      => radix >= 2 ? source / radix : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Drop the trailing digit of the number.</summary>
    [System.CLSCompliant(false)]
    public static uint DropTrailingDigit(this uint source, int radix)
      => radix >= 2 ? source / (uint)radix : throw new System.ArgumentOutOfRangeException(nameof(radix));
    /// <summary>Drop the trailing digit of the number.</summary>
    [System.CLSCompliant(false)]
    public static ulong DropTrailingDigit(this ulong source, int radix)
      => radix >= 2 ? source / (ulong)radix : throw new System.ArgumentOutOfRangeException(nameof(radix));
  }
}
