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
    /// <para>Create a bit-mask with <paramref name="length"/> number of least-significant-bits from <paramref name="mask"/> of <paramref name="maskLength"/> filled repeatedly from right-to-left over the <typeparamref name="TSelf"/>.</para>
    /// </summary>
    public static TSelf BitMaskFillLeft<TSelf>(this TSelf mask, int maskLength, int length)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      mask &= TSelf.CreateChecked((1 << maskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

      var (q, r) = int.DivRem(length, maskLength);

      var result = mask;

      for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one, hence we skip equal-to zero in the condition) times.
        result = mask | (result << maskLength); // Shift the mask count bits and | (or) in count most-significant-bits from bit-mask.

      if (r > 0)
        result = (result << r) | (mask >>> (maskLength - r));

      return result;
    }

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="length"/> number of least-significant-bits from <paramref name="mask"/> of <paramref name="maskLength"/> filled repeatedly from left-to-right over the <typeparamref name="TSelf"/>.</para>
    /// </summary>
    public static TSelf BitMaskFillRight<TSelf>(this TSelf mask, int maskLength, int length)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      mask &= TSelf.CreateChecked((1 << maskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

      var (q, r) = int.DivRem(length, maskLength);

      var result = mask;

      for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one, hence we skip equal-to zero in the condition) times.
        result = mask | (result << maskLength); // Shift the mask count bits and | (or) in count most-significant-bits from bit-mask.

      if (r > 0)
        result = result | ((mask & TSelf.CreateChecked((1 << r) - 1)) << (length - r));

      return result;
    }

    /// <summary>
    /// <para>Flip the bits in <paramref name="source"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static TSelf BitMaskFlip<TSelf>(this TSelf source, TSelf bitMask)
      where TSelf : System.Numerics.IBitwiseOperators<TSelf, TSelf, TSelf>
      => source ^= bitMask;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="count"/> most-significant-bits set to 1.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="count">Can be up to the number of storage bits available in <typeparamref name="TSelf"/>.</param>
    /// <returns></returns>
    public static TSelf BitMaskLeft<TSelf>(this TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => count.BitMaskRight() << (count.GetBitCount() - int.CreateChecked(count));

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits set to 1, and the number of <paramref name="trailingZeroCount"/> (least-significant-bits set to zero).</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="count">Can be up to the number of storage bits available in <typeparamref name="TSelf"/>.</param>
    /// <param name="trailingZeroCount">The number of least-significant-bits set to zero after the most-significant-1-bits set by <paramref name="count"/>.</param>
    /// <returns></returns>
    /// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/> which has a dynamic bit-storage.</remarks>
    public static System.Numerics.BigInteger BitMaskLeft(this System.Numerics.BigInteger count, int trailingZeroCount)
      => count.BitMaskRight() << trailingZeroCount;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="count"/> most-significant-bits set to 1.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="count">Can be up to the number of storage bits available in <typeparamref name="TSelf"/>.</param>
    /// <returns></returns>
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
