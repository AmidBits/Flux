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
    /// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits from <paramref name="bitMaskTemplate"/> repeated across the <typeparamref name="TSelf"/>.</para>
    /// </summary>
    public static TSelf BitMaskFill<TSelf>(this TSelf bitMaskTemplate, int bitLength)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      bitMaskTemplate &= TSelf.CreateChecked((1 << bitLength) - 1); // Ensure only bit-length number of bits in bitMask.

      var newMask = bitMaskTemplate;

      for (var i = bitMaskTemplate.GetBitCount() / bitLength; i > 0; i--) // Loop bit-count minus the offset divided by bit-length (minus one, because we skip equal-to zero in the condition) times.
        newMask = (newMask << bitLength) | bitMaskTemplate; // Move new bit-mask a bit-length amount and | (or) in the bitMask.

      return newMask;
    }

    /// <summary>
    /// <para>Flip the bits of <paramref name="source"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static TSelf BitMaskFlip<TSelf>(this TSelf source, TSelf bitMask)
      where TSelf : System.Numerics.IBitwiseOperators<TSelf, TSelf, TSelf>
      => source ^= bitMask;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="bitLength"/> most-significant-bits set to 1.</para>
    /// </summary>
    public static TSelf BitMaskLeft<TSelf>(this TSelf bitLength)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => bitLength.BitMaskRight() << (bitLength.GetBitCount() - int.CreateChecked(bitLength));

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="bitLength"/> least-significant-bits set to 1, and the number of <paramref name="trailingZeroCount"/> (least-significant-bits set to zero).</para>
    /// </summary>
    /// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/>.</remarks>
    public static System.Numerics.BigInteger BitMaskLeft(this System.Numerics.BigInteger bitLength, int trailingZeroCount)
      => bitLength.BitMaskRight() << trailingZeroCount;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="bitLength"/> least-significant-bits set to 1.</para>
    /// </summary>
    public static TSelf BitMaskRight<TSelf>(this TSelf bitLength)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (((TSelf.One << (int.CreateChecked(bitLength) - 1)) - TSelf.One) << 1) | TSelf.One;

    /// <summary>
    /// <para>Set the bits of <paramref name="source"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static TSelf BitMaskSet<TSelf>(this TSelf source, TSelf bitMask)
      where TSelf : System.Numerics.IBitwiseOperators<TSelf, TSelf, TSelf>
      => source |= bitMask;
  }
}
