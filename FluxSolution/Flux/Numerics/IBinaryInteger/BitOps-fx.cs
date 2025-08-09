namespace Flux
{
  public static partial class BitOps
  {
    #region BitFold

    /// <summary>
    /// <para>Recursively "folds" all 1-bits into lower (right) bits, by taking the most-significant-1-bits (MS1B) and OR it with (MS1B - 1), ending with bottom (right) bits (from MS1B on) set to 1.</para>
    /// <para>The process yields a bit vector with the same most-significant-1-bit as <paramref name="value"/>, and all 1's below it.</para>
    /// </summary>
    /// <returns>All bits set from MS1B down, or -1 (all bits) if the value is less than zero.</returns>
    public static TNumber BitFoldLsb<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsZero(value)
      ? value
      : (((value.MostSignificant1Bit() - TNumber.One) << 1) | TNumber.One);

    /// <summary>
    /// <para>Recursively "folds" all 1-bits into upper (left) bits, ending with top (left) bits (from LS1B on) set to 1.</para>
    /// <para>The process yields a bit vector with the same least-significant-1-bit as <paramref name="value"/>, and all 1's above it.</para>
    /// </summary>
    /// <returns>All bits set from LS1B up, or -1 if the value is less than zero.</returns>
    public static TNumber BitFoldMsb<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsZero(value)
      ? value
      : (value is System.Numerics.BigInteger ? TNumber.CreateChecked(value.GetBitCount()).CreateBitMaskLsbFromBitLength() : ~TNumber.Zero) << value.GetTrailingZeroCount();
    //var tzc = value.GetTrailingZeroCount();
    //return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;

#if INCLUDE_SWAR

    /// <summary>
    /// <para>This is the traditional SWAR algorithm that recursively "folds" the upper bits into the lower bits.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TValue SwarFoldLeft<TValue>(this TValue source)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      // Loop to accomodate dynamic data types, but works like traditional unrolled 32-bit SWAR:
      //source |= (source << 16);
      //source |= (source << 8);
      //source |= (source << 4);
      //source |= (source << 2);
      //source |= (source << 1);

      for (var shift = GetBitCount(source); shift > 0; shift >>= 1)
        source |= source << shift;

      return source;
    }

    /// <summary>
    /// <para>This is the traditional SWAR algorithm that recursively "folds" the upper bits into the lower bits.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TValue SwarFoldRight<TValue>(this TValue source)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      // Loop to accomodate dynamic data types, but works like traditional unrolled 32-bit SWAR:
      //source |= (source >> 16);
      //source |= (source >> 8);
      //source |= (source >> 4);
      //source |= (source >> 2);
      //source |= (source >> 1);

      for (var shift = GetBitCount(source); shift > 0; shift >>= 1)
        source |= source >>> shift; // Unsigned shift right.

      return source;
    }

#endif

    #endregion

    #region BitLength

    /// <summary>
    /// <para>A bit-count is the number of storage bits for the type <typeparamref name="TBitLength"/>.</para>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TBitLength ConvertBitLengthToBitCount<TBitLength>(this TBitLength source)
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
      => source.ConvertBitLengthToByteCount() * TBitLength.CreateChecked(8);

    /// <summary>
    /// <para>A byte-count is the number of storage bytes for the type <typeparamref name="TBitLength"/>.</para>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TBitLength ConvertBitLengthToByteCount<TBitLength>(this TBitLength source)
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
      => TBitLength.CreateChecked(double.Ceiling(double.CreateChecked(source) / 8));

    /// <summary>
    /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TNumber"/>, based on byte-count (times 8).</para>
    /// </summary>
    /// <remarks>
    /// <para>The <c>bit-length(<paramref name="value"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="value"/>)</c>, hence a zero-based bit-index is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
    /// </remarks>
    public static int GetBitLength<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsNegative(value)
      ? value.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
      : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

    ///// <summary>
    ///// <para>Uses the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetShortestBitLength()"/>.</para>
    ///// </summary>
    ///// <remarks>
    ///// <para>The bit-length(<paramref name="source"/>) is equal to <c>(1 + log2-toward-zero(<paramref name="source"/>))</c> and is also equal to <c>log2-away-from-zero(<paramref name="source"/>)</c>.</para>
    ///// </remarks>
    //public static int GetShortestBitLength<TNumber>(this TNumber source)
    //  where TNumber : System.Numerics.IBinaryInteger<TNumber>
    //  => source.GetShortestBitLength();

    #endregion

    #region BitLengthWithinType

    public static TBitLength AssertBitLengthWithinType<TBitLength>(this TBitLength bitLength, string? paramName = "bitLength")
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
    {
      if (!IsBitLengthWithinType(bitLength))
        throw new System.ArgumentOutOfRangeException(paramName);

      return bitLength;
    }

    public static bool IsBitLengthWithinType<TBitLength>(this TBitLength bitLength)
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
    => bitLength >= TBitLength.Zero && bitLength <= TBitLength.CreateChecked(GetBitCount(bitLength));

    #endregion

    #region BitManipulation

    /// <summary>
    /// <para>Clear a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TValue BitClear<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value & ~(TValue.One << index);

    /// <summary>
    /// <para>Flip a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TValue BitFlip<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value ^ TValue.One << index;

    /// <summary>
    /// <para>Determine the state of a bit in <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool BitGet<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => !TValue.IsZero(value & TValue.One << index);

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> carries the LSB (of bit-count, not bit-length).</para>
    /// <para>E.g. if <see cref="BitCheckLsb{TValue}(TValue)"/> returns true, the <paramref name="value"/> is odd, otherwise it's even.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool BitGetLsb<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => !TNumber.IsZero(value & TNumber.One);

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> carries the MSB (of bit-count, not bit-length).</para>
    /// <para>E.g. if <paramref name="value"/> is a signed integer, and <see cref="BitCheckMsb{TValue}(TValue)"/> returns true, the <paramref name="value"/> is negative, otherwise positive.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool BitGetMsb<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value.BitGet(value.GetBitCount() - 1);

    /// <summary>
    /// <para>Gets the bit-index of a power-of-2 number.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber BitIndexOfPow2<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsPow2(value) ? TNumber.Log2(value) : throw new System.ArgumentOutOfRangeException(nameof(value), "Not a power-of-2 number.");

    /// <summary>
    /// <para>Set a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TValue BitSet<TValue>(this TValue value, int index)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value | TValue.One << index;

    #endregion

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

    #region BitSwaps

    /// <summary>
    /// <para>Swap adjacent 1-bits (single bits).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap1(this ulong value) => ((value & 0xaaaaaaaaaaaaaaaaUL) >> 0x01) | ((value & 0x5555555555555555UL) << 0x01);

    /// <summary>
    /// <para>Swap adjacent 2-bits (pairs).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap2(this ulong value) => ((value & 0xccccccccccccccccUL) >> 0x02) | ((value & 0x3333333333333333UL) << 0x02);

    /// <summary>
    /// <para>Swap adjacent 4-bits (nibbles).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap4(this ulong value) => ((value & 0xf0f0f0f0f0f0f0f0UL) >> 0x04) | ((value & 0x0f0f0f0f0f0f0f0fUL) << 0x04);

    /// <summary>
    /// <para>Swap adjacent 8-bits (bytes).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap8(this ulong value) => ((value & 0xff00ff00ff00ff00UL) >> 0x08) | ((value & 0x00ff00ff00ff00ffUL) << 0x08);

    /// <summary>
    /// <para>Swap adjacent 16-bits.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap16(this ulong value) => ((value & 0xffff0000ffff0000UL) >> 0x10) | ((value & 0x0000ffff0000ffffUL) << 0x10);

    /// <summary>
    /// <para>Swap adjacent 32-bits.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap32(this ulong value) => ((value & 0xffffffff00000000UL) >> 0x20) | ((value & 0x00000000ffffffffUL) << 0x20);

    #endregion

    #region GetBitCount

    /// <summary>
    /// <para>Returns the size, in number of bits, needed to store <paramref name="value"/>.</para>
    /// <para>Most types returns the underlying storage size of the type itself, e.g. <see langword="int"/> = 32 or <see langword="long"/> = 64.</para>
    /// </summary>
    /// <remarks>
    /// <para>Some data types, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetBitCount{TValue}"/> dynamic, and depends on the actual number stored.</para>
    /// </remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int GetBitCount<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value.GetByteCount() * 8;

    #endregion

    #region GetByteCount

    /// <summary>
    /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetByteCount(TValue)"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetByteCount{TValue}"/> dynamic also.</para>
    /// </remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int GetByteCount<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value.GetByteCount();

    #endregion

    #region GetPopCount

    /// <summary>
    /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.PopCount(TValue)"/>.</para>
    /// </summary>
    /// <returns>The population count of <paramref name="value"/>, i.e. the number of bits set to 1 in <paramref name="value"/>.</returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int GetPopCount<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => int.CreateChecked(TNumber.PopCount(value));

    #endregion

    #region GrayCode

    /// <summary>
    /// <para>Converts a binary number to a reflected binary Gray code.</para>
    /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber BinaryToGray<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value ^ (value >>> 1);

    /// <summary>
    /// <para>Convert a value to a Gray code with the given base and digits. Iterating through a sequence of values would result in a sequence of Gray codes in which only one digit changes at a time.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Gray_code"/></para>
    /// </summary>
    /// <remarks>Experimental adaption from wikipedia.</remarks>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static TNumber[] BinaryToGrayCode<TNumber>(this TNumber value, TNumber radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (value < TNumber.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      var gray = new TNumber[int.CreateChecked(value.DigitCount(radix))];

      var baseN = new TNumber[gray.Length]; // Stores the ordinary base-N number, one digit per entry

      for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
      {
        baseN[index] = value % radix;

        value /= radix;
      }

      var shift = TNumber.Zero; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

      for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
      {
        gray[index] = (baseN[index] + shift) % radix;

        shift = shift + radix - gray[index]; // Subtract from base so shift is positive
      }

      return gray;
    }

    /// <summary>
    /// <para>Converts a reflected binary gray code to a binary number.</para>
    /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    /// </summary>
    public static TNumber GrayToBinary<TNumber>(this TNumber value)
        where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var mask = value;

      while (!TNumber.IsZero(mask))
      {
        mask >>>= 1;
        value ^= mask;
      }

      return value;
    }

    #endregion

    #region Log2

    /// <summary>
    /// <para>Computes the ceiling integer-log-2 (a.k.a. ceiling-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index + 1 of a power-of-2 <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The ceiling integer log2 of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>The <c>log2-away-from-zero(<paramref name="value"/>)</c> is equal to <c>bit-length(<paramref name="value"/>)</c>.</para>
    /// </remarks>
    public static TNumber Log2AwayFromZero<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsZero(value)
      ? value
      : TNumber.CopySign(TNumber.Abs(value) is var abs && TNumber.Log2(abs) is var log2 && TNumber.IsPow2(abs) ? log2 : log2 + TNumber.One, value);

    /// <summary>
    /// <para>Computes the floor integer-log-2 (a.k.a. floor-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index of a power-of-2 <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The floor integer-log-2 of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>The ceiling log2 of <paramref name="value"/>: <code>(<paramref name="value"/> > 1 ? Log2TowardZero(<paramref name="value"/> - 1) + 1 : 0)</code></para>
    /// <para>The <c>log2-toward-zero(<paramref name="value"/>)</c> is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
    /// </remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber Log2TowardZero<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsZero(value)
      ? value
      : TNumber.CopySign(TNumber.Log2(TNumber.Abs(value)), value);

#if INCLUDE_SWAR

    public static TValue SwarLog2<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.PopCount(SwarFoldRight(value) >> 1);

#endif

    #endregion

    #region MortonNumber

    /// <summary>
    /// <para>Interleave bits of byte <paramref name="x"/> and byte <paramref name="y"/>, so that all of the bits of <paramref name="x"/> are in the even positions and <paramref name="y"/> in the odd, resulting in a Morton Number.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static int MortonNumber(this byte x, byte y)
      => unchecked((int)(((x * 0x0101010101010101UL & 0x8040201008040201UL) * 0x0102040810204081UL >> 49) & 0x5555 | ((y * 0x0101010101010101UL & 0x8040201008040201UL) * 0x0102040810204081UL >> 48) & 0xAAAA));

    #endregion

    #region Pow2

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction away-from-zero, optionally <paramref name="unequal"/> if <paramref name="value"/> is already a power-of-2.</para>
    /// <example>
    /// <para>In order to process for example floating point types:</para>
    /// <code>TValue.CreateChecked(PowOf2AwayFromZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; TValue.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber Pow2AwayFromZero<TNumber>(this TNumber value, bool unequal)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.CopySign(TNumber.Abs(value) is var abs && TNumber.IsPow2(abs) ? (unequal ? abs << 1 : abs) : abs.MostSignificant1Bit() << 1, value);

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction toward-zero, optionally <paramref name="unequal"/> if <paramref name="value"/> is already a power-of-2.</para>
    /// <example>
    /// <para>In order to process for example a double:</para>
    /// <code><see cref="double"/>.CreateChecked(PowOf2TowardZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; <see cref="double"/>.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber Pow2TowardZero<TNumber>(this TNumber value, bool unequal)
       where TNumber : System.Numerics.IBinaryInteger<TNumber>
       => TNumber.CopySign(TNumber.Abs(value) is var abs && TNumber.IsPow2(abs) && unequal ? abs >> 1 : abs.MostSignificant1Bit(), value);

#if INCLUDE_SWAR

    public static TValue SwarNextLargestPow2<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => SwarFoldRight(value) + TValue.One;

#endif

    #endregion

    #region ReverseBits

    /// <summary>
    /// <para>Reverses the bits of an integer. The LSBs (least significant bits) becomes the MSBs (most significant bits) and vice versa, i.e. the bits are mirrored across the integer storage space. It's a reversal of all storage bits.</para>
    /// </summary>
    /// <remarks>See <see cref="ReverseBytes{TValue}(TValue)"/> for byte reversal.</remarks>
    public static TNumber ReverseBits<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>, System.Numerics.ISignedNumber<TNumber>
    {
      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the bit reversal.

      value.WriteBigEndian(bytes); // Write as BigEndian (high-to-low).

      for (var i = bytes.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
        ReverseBitsInPlace(ref bytes[i]); // Mirror (reverse) bits in each byte.

      return TNumber.ReadLittleEndian(bytes, !value.IsSignedNumber()); // Read as LittleEndian (low-to-high).
    }

    /// <summary>
    /// <para>Bit-reversal of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static byte ReverseBits(this byte value)
      => (byte)(((value * 0x0202020202UL) & 0x010884422010UL) % 1023);

    /// <summary>
    /// <para>Bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ushort ReverseBits(this ushort value)
      => (ushort)(((((((byte)(value & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023) << 8) | ((((((byte)((value >> 8) & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023)));
    #region Alternative snippets
    //=> (ushort)((((byte)(value & 0xFF)).ReverseBits() << 8) | (((byte)((value >> 8) & 0xFF)).ReverseBits()));
    //=> value = (ushort)BitSwap8(BitSwap4(BitSwap2(BitSwap1(value))));
    //{
    //value = (ushort)(((value & 0xAAAAu) >> 0x01) | ((value & 0x5555u) << 0x01));
    //value = (ushort)(((value & 0xCCCCu) >> 0x02) | ((value & 0x3333u) << 0x02));
    //value = (ushort)(((value & 0xF0F0u) >> 0x04) | ((value & 0x0F0Fu) << 0x04));
    //value = (ushort)(((value & 0xFF00u) >> 0x08) | ((value & 0x00FFu) << 0x08));
    //}
    #endregion

    /// <summary>
    /// <para>Bit-reversal of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static uint ReverseBits(this uint value)
    {
      uint tmp;
      value = (value << 15) | (value >> 17);
      tmp = (value ^ (value >> 10)) & 0x003f801f;
      value = (tmp + (tmp << 10)) ^ value;
      tmp = (value ^ (value >> 4)) & 0x0e038421;
      value = (tmp + (tmp << 4)) ^ value;
      tmp = (value ^ (value >> 2)) & 0x22488842;
      value = (tmp + (tmp << 2)) ^ value;
      return tmp;
    }
    #region Alternative snippets
    //{
    //  value = ((value & 0xAAAAAAAAu) >> 0x01) | ((value & 0x55555555u) << 0x01);
    //  value = ((value & 0xCCCCCCCCu) >> 0x02) | ((value & 0x33333333u) << 0x02);
    //  value = ((value & 0xF0F0F0F0u) >> 0x04) | ((value & 0x0F0F0F0Fu) << 0x04);
    //  value = ((value & 0xFF00FF00u) >> 0x08) | ((value & 0x00FF00FFu) << 0x08);
    //  value = ((value & 0xFFFF0000u) >> 0x10) | ((value & 0x0000FFFFu) << 0x10);
    //}
    //=> value = (uint)BitSwap16(BitSwap8(BitSwap4(BitSwap2(BitSwap1(value)))));
    #endregion

    /// <summary>
    /// <para>Bit-reversal of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong ReverseBits(this ulong value)
    {
      value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01);
      value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02);
      value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04);
      value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08);
      value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10);
      value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20);
      return value;
    }
    #region Alternative snippets
    // => value = BitSwap32(BitSwap16(BitSwap8(BitSwap4(BitSwap2(BitSwap1(value))))));
    #endregion

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal) of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void ReverseBitsInPlace(ref this byte value) => value = value.ReverseBits();
    //=> value = (byte)(((value * 0x0202020202UL) & 0x010884422010UL) % 1023);

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static void ReverseBitsInPlace(ref this ushort value) => value = value.ReverseBits();

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static void ReverseBitsInPlace(ref this uint value) => value = value.ReverseBits();

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static void ReverseBitsInPlace(ref this ulong value) => value = value.ReverseBits();

    #endregion

    #region ReverseBytes

    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// </summary>
    /// <remarks>See <see cref="ReverseBits{TValue}(TValue)"/> for bit reversal.</remarks>
    public static TNumber ReverseBytes<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the byte reversal.

      // We can use either direction here, write-LE/read-BE or write-BE/read-LE, doesn't really matter, since the end result is the same.

      value.WriteLittleEndian(bytes); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).
      return TNumber.ReadBigEndian(bytes, value.IsUnsignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).

      //value.WriteBigEndian(bytes); // Write as BigEndian (decreasing numeric significance in increasing memory addresses).
      //return TValue.ReadLittleEndian(bytes, value.IsUnsignedNumber()); // Read as LittleEndian (increasing numeric significance in increasing memory addresses).
    }

    //public static TValue ReverseBytesOwnLoop<TValue>(this TValue value)
    //  where TValue : System.Numerics.IBinaryInteger<TValue>
    //{
    //  var reversed = TValue.Zero;

    //  var bc = value.GetBitCount();

    //  var mh = TValue.CreateChecked(0xFF) << (value.GetBitCount() / 2);
    //  var ml = mh >>> 8;

    //  for (var vs = 8; vs < bc; vs += 16)
    //  {
    //    reversed |= ((value >>> vs) & ml) | ((value << vs) & mh);

    //    mh <<= 8;
    //    ml >>>= 8;
    //  }

    //  return reversed;
    //}

    #endregion

    #region ShuffleBytes

    /// <summary>
    /// <para>Shuffles all bytes of an integer.</para>
    /// </summary>
    public static TNumber ShuffleBytes<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var bytes = new byte[value.GetByteCount()];
      value.WriteLittleEndian(bytes);
      System.Security.Cryptography.RandomNumberGenerator.Shuffle<byte>(bytes);
      return TNumber.ReadLittleEndian(bytes, value.GetType().IsUnsignedNumericType());
    }

    #endregion

    #region Significant1Bits

    /// <summary>
    /// <para>Extracts the lowest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the least-significant-1-bit.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber LeastSignificant1Bit<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value & ((~value) + TNumber.One);
    //=> (value & -value); // This optimized version does not work on unsigned integers.

    /// <summary>
    /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber LeastSignificant1BitClear<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value & (value - TNumber.One);

    /// <summary>
    /// <para>Extracts the highest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the most-significant-1-bit.</para>
    /// <list type="bullet">
    /// <item>If <paramref name="value"/> equal zero, zero is returned.</item>
    /// <item>If <paramref name="value"/> is negative, min-value of the signed type is returned (i.e. the top most-significant-bit that the type is able to represent).</item>
    /// <item>Otherwise the most-significant-1-bit is returned, which also happens to be the same as Log2(<paramref name="value"/>).</item>
    /// </list>
    /// </summary>
    /// <remarks>Note that for dynamic types, e.g. <see cref="System.Numerics.BigInteger"/>, the number of bits depends on the storage size used for the <paramref name="value"/>.</remarks>
    public static TNumber MostSignificant1Bit<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsZero(value) ? value : TNumber.One << (value.GetBitLength() - 1);

    /// <summary>
    /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber MostSignificant1BitClear<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value - value.MostSignificant1Bit();

#if INCLUDE_SWAR

    public static TValue SwarMostSignificant1Bit<TValue>(this TValue source)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      source = SwarFoldRight(source);

      return (source & ~(source >> 1));
    }

#endif

    #endregion

    #region ZeroCounts

    /// <summary>
    /// <para>A.k.a. count-leading-zero's (clz), counts the number of zero bits preceding the most-significant-1-bit in <paramref name="value"/>. I.e. the number of most-significant-0-bits.</para>
    /// </summary>
    /// <remarks>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.LeadingZeroCount(TValue)"/>.</remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int GetLeadingZeroCount<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => int.CreateChecked(TNumber.LeadingZeroCount(value));

    /// <summary>
    /// <para>A.k.a. called count-trailing-zero's (ctz), counts the number of zero bits trailing the least-significant-1-bit in <paramref name="value"/>. I.e. the number of least-significant-0-bits.</para>
    /// </summary>
    /// <remarks>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.TrailingZeroCount(TValue)"/>.</remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int GetTrailingZeroCount<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => int.CreateChecked(TNumber.TrailingZeroCount(value));

#if INCLUDE_SWAR

    public static int SwarLeadingZeroCount<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => GetBitCount(value) - GetPopCount(SwarFoldRight(value));

#endif

    #endregion
  }
}
