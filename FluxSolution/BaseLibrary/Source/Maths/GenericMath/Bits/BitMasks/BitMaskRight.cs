namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Create a bit mask with <paramref name="bitCount"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static TSelf BitMaskRight<TSelf>(this TSelf bitCount)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (((TSelf.One << (int.CreateChecked(bitCount) - 1)) - TSelf.One) << 1) | TSelf.One;

#else

    /// <summary>Create a bit mask with <paramref name="count1Bits"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static System.Numerics.BigInteger BitMaskRight(this System.Numerics.BigInteger count)
      => (((System.Numerics.BigInteger.One << ((int)count - 1)) - 1) << 1) | System.Numerics.BigInteger.One;

    /// <summary>Create a bit mask with <paramref name="count1Bits"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static int BitMaskRight(this int count)
      => unchecked((int)((uint)count).BitFoldRight());

    /// <summary>Create a bit mask with <paramref name="count1Bits"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static long BitMaskRight(this long count)
      => unchecked((long)((ulong)count).BitFoldRight());

    /// <summary>Create a bit mask with <paramref name="count1Bits"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    [System.CLSCompliant(false)]
    public static uint BitMaskRight(this uint count)
      => (((1U << ((int)count - 1)) - 1) << 1) | 1U;

    /// <summary>Create a bit mask with <paramref name="count1Bits"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    [System.CLSCompliant(false)]
    public static ulong BitMaskRight(this ulong count)
      => (((1UL << ((int)count - 1)) - 1) << 1) | 1UL;

#endif
  }
}
