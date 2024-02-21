namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Clear the bits of <paramref name="source"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static TSelf BitMaskClear<TSelf>(this TSelf source, TSelf bitMask)
      where TSelf : System.Numerics.IBitwiseOperators<TSelf, TSelf, TSelf>
      => source &= ~bitMask;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="count"/> number of least-significant-bits from <paramref name="bitMask"/> filled repeatedly from left to right across the <typeparamref name="TSelf"/>.</para>
    /// </summary>
    public static TSelf BitMaskFillLeft<TSelf>(this TSelf bitMask, int count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      bitMask &= TSelf.CreateChecked((1 << count) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

      var mask = bitMask;

      for (var i = bitMask.GetBitCount() / count; i > 0; i--) // Loop bit-count divided by count (minus one, hence we skip equal-to zero in the condition) times.
        mask = (mask << count) | bitMask; // Shift the mask count bits and | (or) in count least-significant-bits from bit-mask.

      return mask;
    }

    public static TSelf BitMaskFillRight<TSelf>(this TSelf bitMask, int count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      bitMask &= TSelf.CreateChecked((1 << count) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

      bitMask <<= bitMask.GetBitCount() - count; // Shift the template bit-mask into the most-significant-bits.

      var mask = bitMask;

      for (var i = bitMask.GetBitCount() / count; i > 0; i--) // Loop bit-count divided by count (minus one, hence we skip equal-to zero in the condition) times.
        mask = bitMask | (mask >>> count); // Shift the mask count bits and | (or) in count most-significant-bits from bit-mask.

      return mask;
    }

    /// <summary>
    /// <para>Flip the bits of <paramref name="source"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static TSelf BitMaskFlip<TSelf>(this TSelf source, TSelf bitMask)
      where TSelf : System.Numerics.IBitwiseOperators<TSelf, TSelf, TSelf>
      => source ^= bitMask;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="count"/> most-significant-bits set to 1.</para>
    /// </summary>
    public static TSelf BitMaskLeft<TSelf>(this TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => count.BitMaskRight() << (count.GetBitCount() - int.CreateChecked(count));

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits set to 1, and the number of <paramref name="trailingZeroCount"/> (least-significant-bits set to zero).</para>
    /// </summary>
    /// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/>.</remarks>
    public static System.Numerics.BigInteger BitMaskLeft(this System.Numerics.BigInteger count, int trailingZeroCount)
      => count.BitMaskRight() << trailingZeroCount;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits set to 1.</para>
    /// </summary>
    public static TSelf BitMaskRight<TSelf>(this TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (((TSelf.One << (int.CreateChecked(count) - 1)) - TSelf.One) << 1) | TSelf.One;

    /// <summary>
    /// <para>Set the bits of <paramref name="source"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static TSelf BitMaskSet<TSelf>(this TSelf source, TSelf bitMask)
      where TSelf : System.Numerics.IBitwiseOperators<TSelf, TSelf, TSelf>
      => source |= bitMask;
  }
}
