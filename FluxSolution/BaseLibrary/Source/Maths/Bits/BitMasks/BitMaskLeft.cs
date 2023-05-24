namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Create a left (most-significant) bit mask with <paramref name="count"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static TSelf BitMaskLeft<TSelf>(this TSelf count) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => count.BitMaskRight() << (count.GetBitCount() - int.CreateChecked(count));

#else

    /// <summary>Create a left (most-significant) bit mask with <paramref name="count"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static System.Numerics.BigInteger BitMaskLeft(this System.Numerics.BigInteger count)
      => count.BitMaskRight() << (count.GetBitCount() - (int)count);

    /// <summary>Create a left (most-significant) bit mask with <paramref name="count"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static int BitMaskLeft(this int count)
      => unchecked((int)((uint)count).BitMaskLeft());

    /// <summary>Create a left (most-significant) bit mask with <paramref name="count"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static long BitMaskLeft(this long count)
      => unchecked((long)((ulong)count).BitMaskLeft());

    /// <summary>Create a left (most-significant) bit mask with <paramref name="count"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    [System.CLSCompliant(false)]
    public static uint BitMaskLeft(this uint count)
      => count.BitMaskRight() << (count.GetBitCount() - (int)count);

    /// <summary>Create a left (most-significant) bit mask with <paramref name="count"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    [System.CLSCompliant(false)]
    public static ulong BitMaskLeft(this ulong count)
      => count.BitMaskRight() << (count.GetBitCount() - (int)count);

#endif
  }
}
