namespace FluxNet.Numerics
{
  public static partial class BitOps
  {
    #region BitMasks

    /// <summary>
    /// <para>Get/check the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool BitMaskCheckAll<TNumber, TBitMask>(this TNumber value, TBitMask bitMask)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
      => TNumber.IsZero(~value & TNumber.CreateChecked(bitMask));

    /// <summary>
    /// <para>Get/check the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool BitMaskCheckAny<TNumber, TBitMask>(this TNumber value, TBitMask bitMask)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
      => !TNumber.IsZero(value & TNumber.CreateChecked(bitMask));

    /// <summary>
    /// <para>Clear the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber BitMaskClear<TNumber, TBitMask>(this TNumber value, TBitMask bitMask)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
      => value & ~TNumber.CreateChecked(bitMask);

    /// <summary>
    /// <para>Flip the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber BitMaskFlip<TNumber, TBitMask>(this TNumber value, TBitMask bitMask)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
      => value ^ TNumber.CreateChecked(bitMask);

    /// <summary>
    /// <para>Flip the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber BitMaskSet<TNumber, TBitMask>(this TNumber value, TBitMask bitMask)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
      => value | TNumber.CreateChecked(bitMask);

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="bitLength"/> least-significant-bits set to 1, and the number of <paramref name="trailingZeroBitCount"/> (least-significant-bits set to zero).</para>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <param name="bitLength">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitLength"/>.</param>
    /// <param name="trailingZeroBitCount">The number of least-significant-bits set to zero after the most-significant-1-bits set by <paramref name="bitLength"/>.</param>
    /// <returns></returns>
    /// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/> which has a dynamic bit-storage.</remarks>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TBitLength CreateBitMaskLsbFromBitLength<TBitLength>(this TBitLength bitLength, int trailingZeroBitCount)
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
      => CreateBitMaskLsbFromBitLength(bitLength) << trailingZeroBitCount;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="bitLength"/> least-significant-bits set to 1.</para>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <param name="bitLength">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitLength"/>.</param>
    /// <returns></returns>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
    public static TBitLength CreateBitMaskLsbFromBitLength<TBitLength>(this TBitLength bitLength)
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
      => TBitLength.IsZero(bitLength)
      ? bitLength
      : bitLength > TBitLength.CreateChecked(bitLength.GetBitCount())
      ? throw new System.ArgumentOutOfRangeException(nameof(bitLength))
      : (((TBitLength.One << (int.CreateChecked(bitLength) - 1)) - TBitLength.One) << 1) | TBitLength.One;

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="bitLength"/> most-significant-bits set to 1.</para>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <param name="bitLength">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitLength"/>.</param>
    /// <returns></returns>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TBitLength CreateBitMaskMsbFromBitLength<TBitLength>(this TBitLength bitLength)
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
      => CreateBitMaskLsbFromBitLength(bitLength) << (bitLength.GetBitCount() - int.CreateChecked(bitLength));

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from most-to-least-significant-bits over the <typeparamref name="TBitMask"/>.</para>
    /// </summary>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
    public static TBitMask FillBitMaskLsb<TBitMask>(this TBitMask bitMask, int bitMaskLength, int bitLength)
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

    /// <summary>
    /// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from least-to-most-significant-bits over the <typeparamref name="TBitMask"/>.</para>
    /// </summary>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
    public static TBitMask FillBitMaskMsb<TBitMask>(this TBitMask bitMask, int bitMaskLength, int bitLength)
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

    ///// <summary>
    ///// <para>Create a bit-mask with <paramref name="count"/> most-significant-bits set to 1.</para>
    ///// </summary>
    ///// <typeparam name="TValue"></typeparam>
    ///// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TValue"/>.</param>
    ///// <returns></returns>
    //public static TValue BitMaskLeft<TValue>(this TValue count)
    //  where TValue : System.Numerics.IBinaryInteger<TValue>
    //  => count.BitMaskRight() << (count.GetBitCount() - int.CreateChecked(count));

    ///// <summary>
    ///// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits set to 1, and the number of <paramref name="trailingZeroCount"/> (least-significant-bits set to zero).</para>
    ///// </summary>
    ///// <typeparam name="TValue"></typeparam>
    ///// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TValue"/>.</param>
    ///// <param name="trailingZeroCount">The number of least-significant-bits set to zero after the most-significant-1-bits set by <paramref name="count"/>.</param>
    ///// <returns></returns>
    ///// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/> which has a dynamic bit-storage.</remarks>
    //public static TValue BitMaskRight<TValue>(this TValue count, int trailingZeroCount)
    //  where TValue : System.Numerics.IBinaryInteger<TValue>
    //  => count.BitMaskRight() << trailingZeroCount;

    ///// <summary>
    ///// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits set to 1.</para>
    ///// </summary>
    ///// <typeparam name="TValue"></typeparam>
    ///// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TValue"/>.</param>
    ///// <returns></returns>
    //public static TValue BitMaskRight<TValue>(this TValue count)
    //  where TValue : System.Numerics.IBinaryInteger<TValue>
    //  => (((TValue.One << (int.CreateChecked(count) - 1)) - TValue.One) << 1) | TValue.One;

    #endregion
  }
}
