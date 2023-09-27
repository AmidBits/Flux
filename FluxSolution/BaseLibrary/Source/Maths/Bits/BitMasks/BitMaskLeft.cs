namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Create a left (most-significant) bit mask with <paramref name="bitCount"/> bits set to 1 on the MSB (Most Significant Bit) or left side.</summary>
    public static TSelf BitMaskLeft<TSelf>(this TSelf bitCount)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => bitCount.BitMaskRight() << (bitCount.GetBitCount() - int.CreateChecked(bitCount));

    /// <summary>Create a left (most-significant) bit mask with <paramref name="bitCount"/> bits set to 1 on the MSB (Most Significant Bit) or left side.</summary>
    /// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/>.</remarks>
    public static System.Numerics.BigInteger BitMask(this System.Numerics.BigInteger bitCount, int trailingZeroCount)
      => bitCount.BitMaskRight() << trailingZeroCount;

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
