namespace Flux
{
  public static partial class BitOps
  {
    #region BitMasks

    /// <summary>
    /// <para>Get/check the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static bool BitMaskCheckAll<TInteger, TBitMask>(this TInteger value, TBitMask bitMask)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
      => TInteger.IsZero(~value & TInteger.CreateChecked(bitMask));

    /// <summary>
    /// <para>Get/check the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static bool BitMaskCheckAny<TInteger, TBitMask>(this TInteger value, TBitMask bitMask)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
      => !TInteger.IsZero(value & TInteger.CreateChecked(bitMask));

    /// <summary>
    /// <para>Clear the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static TInteger BitMaskClear<TInteger, TBitMask>(this TInteger value, TBitMask bitMask)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
      => value & ~TInteger.CreateChecked(bitMask);

    /// <summary>
    /// <para>Flip the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static TInteger BitMaskFlip<TInteger, TBitMask>(this TInteger value, TBitMask bitMask)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
      => value ^ TInteger.CreateChecked(bitMask);

    /// <summary>
    /// <para>Flip the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    public static TInteger BitMaskSet<TInteger, TBitMask>(this TInteger value, TBitMask bitMask)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
      => value | TInteger.CreateChecked(bitMask);

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="count"/> most-significant-bits set to 1.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
    /// <returns></returns>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
    public static TInteger CreateBitMaskLeft<TInteger>(this TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var bitMaskRight = CreateBitMaskRight(count);

      return bitMaskRight << (bitMaskRight.GetBitCount() - int.CreateChecked(count));
    }

    ///// <summary>
    ///// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits set to 1, and the number of <paramref name="trailingZeroBitCount"/> (least-significant-bits set to zero).</para>
    ///// </summary>
    ///// <typeparam name="TInteger"></typeparam>
    ///// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
    ///// <param name="trailingZeroBitCount">The number of least-significant-bits set to zero after the most-significant-1-bits set by <paramref name="count"/>.</param>
    ///// <returns></returns>
    ///// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/> which has a dynamic bit-storage.</remarks>
    ///// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
    //public static TInteger CreateBitMaskRight<TInteger>(this TInteger count, int trailingZeroBitCount)
    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
    //  => CreateBitMaskRight(count) << trailingZeroBitCount;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits set to 1.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
    /// <returns></returns>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
    public static TInteger CreateBitMaskRight<TInteger>(this TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (TInteger.IsZero(count))
        return count;

      return (((TInteger.One << (int.CreateChecked(count) - 1)) - TInteger.One) << 1) | TInteger.One;
    }

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from least-to-most-significant-bits over the <typeparamref name="TBitMask"/>.</para>
    /// </summary>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
    public static TBitMask FillBitMaskLeft<TBitMask>(this TBitMask bitMask, int bitMaskLength, int bitLength)
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
    {
      bitMask &= TBitMask.CreateChecked((1 << bitMaskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

      var (q, r) = int.DivRem(bitLength, bitMaskLength);

      var result = bitMask;

      for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one) times, hence we skip equal-to zero in the condition.
        result = bitMask | (result << bitMaskLength); // Shift the mask count bits and | (OR) in count most-significant-bits from bit-mask.

      if (r > 0)
        result = (result << r) | (bitMask >>> (bitMaskLength - r));

      return result;
    }

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from most-to-least-significant-bits over the <typeparamref name="TBitMask"/>.</para>
    /// </summary>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
    public static TBitMask FillBitMaskRight<TBitMask>(this TBitMask bitMask, int bitMaskLength, int bitLength)
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
    {
      bitMask &= TBitMask.CreateChecked((1 << bitMaskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

      var (q, r) = int.DivRem(bitLength, bitMaskLength);

      var result = bitMask;

      for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one) times, hence we skip equal-to zero in the condition.
        result = bitMask | (result << bitMaskLength); // Shift the mask count bits and | (OR) in count most-significant-bits from bit-mask.

      if (r > 0)
        result |= (bitMask & TBitMask.CreateChecked((1 << r) - 1)) << (bitLength - r);

      return result;
    }

    #endregion
  }
}
