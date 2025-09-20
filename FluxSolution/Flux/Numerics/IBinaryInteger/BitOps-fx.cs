//namespace Flux
//{
//  /// <summary>
//  /// <para><see href="http://aggregate.org/MAGIC/"/></para>
//  /// <para><see href="http://graphics.stanford.edu/~seander/bithacks.html"/></para>
//  /// </summary>
//  /// <remarks>
//  /// <para>Clear a bit: <code><![CDATA[value & ~(1 << index)]]></code></para>
//  /// <para>Toggle/Flip a bit: <code><![CDATA[value ^ (1 << index)]]></code></para>
//  /// <para>Set a bit: <code><![CDATA[value | (1 << index)]]></code></para>
//  /// 
//  /// </remarks>
//  public static partial class BitOps
//  {
//    #region MortonNumber

//    /// <summary>
//    /// <para>Interleave bits of byte <paramref name="x"/> and byte <paramref name="y"/>, so that all of the bits of <paramref name="x"/> are in the even positions and <paramref name="y"/> in the odd, resulting in a Morton Number.</para>
//    /// </summary>
//    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
//    public static int MortonNumber(this byte x, byte y)
//      => unchecked((int)(((x * 0x0101010101010101UL & 0x8040201008040201UL) * 0x0102040810204081UL >> 49) & 0x5555 | ((y * 0x0101010101010101UL & 0x8040201008040201UL) * 0x0102040810204081UL >> 48) & 0xAAAA));

//    #endregion // MortonNumber

//    #region ReverseBits

//    /// <summary>
//    /// <para>Reverses the bits of an integer. The LSBs (least significant bits) becomes the MSBs (most significant bits) and vice versa, i.e. the bits are mirrored across the integer storage space. It's a reversal of all storage bits.</para>
//    /// </summary>
//    /// <remarks>See <see cref="ReverseBytes{TInteger}(TInteger)"/> for byte reversal.</remarks>
//    public static TInteger ReverseBits<TInteger>(this TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>, System.Numerics.ISignedNumber<TInteger>
//    {
//      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the bit reversal.

//      value.WriteBigEndian(bytes); // Write as BigEndian (high-to-low).

//      for (var i = bytes.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
//        ReverseBitsInPlace(ref bytes[i]); // Mirror (reverse) bits in each byte.

//      return TInteger.ReadLittleEndian(bytes, !value.IsSignedNumber()); // Read as LittleEndian (low-to-high).
//    }

//    /// <summary>
//    /// <para>Bit-reversal of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
//    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
//    /// </summary>
//    public static byte ReverseBits(this byte value)
//    {
//      ReverseBitsInPlace(ref value);

//      return value;
//    }

//    /// <summary>
//    /// <para>Bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
//    /// </summary>
//    [System.CLSCompliant(false)]
//    public static ushort ReverseBits(this ushort value)
//    {
//      ReverseBitsInPlace(ref value);

//      return value;
//    }

//    /// <summary>
//    /// <para>Bit-reversal of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</para>
//    /// </summary>
//    [System.CLSCompliant(false)]
//    public static uint ReverseBits(this uint value)
//    {
//      ReverseBitsInPlace(ref value);

//      return value;
//    }

//    /// <summary>
//    /// <para>Bit-reversal of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
//    /// </summary>
//    [System.CLSCompliant(false)]
//    public static ulong ReverseBits(this ulong value)
//    {
//      ReverseBitsInPlace(ref value);

//      return value;
//    }

//    /// <summary>
//    /// <para>In-place (by ref) mirror the bits (bit-reversal) of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
//    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
//    /// </summary>
//    public static void ReverseBitsInPlace(ref this byte value)
//      => value = (byte)(((value * 0x0202020202UL) & 0x010884422010UL) % 1023);

//    /// <summary>
//    /// <para>In-place (by ref) mirror the bits (bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
//    /// </summary>
//    [System.CLSCompliant(false)]
//    public static void ReverseBitsInPlace(ref this ushort value)
//      => value = (ushort)(((((((byte)(value & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023) << 8) | ((((((byte)((value >> 8) & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023)));

//    // Alternatively:
//    // {
//    //  value = (ushort)(((value & 0xAAAAu) >> 0x01) | ((value & 0x5555u) << 0x01));
//    //  value = (ushort)(((value & 0xCCCCu) >> 0x02) | ((value & 0x3333u) << 0x02));
//    //  value = (ushort)(((value & 0xF0F0u) >> 0x04) | ((value & 0x0F0Fu) << 0x04));
//    //  value = (ushort)(((value & 0xFF00u) >> 0x08) | ((value & 0x00FFu) << 0x08));
//    // }

//    /// <summary>
//    /// <para>In-place (by ref) mirror the bits (bit-reversal of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</para>
//    /// </summary>
//    [System.CLSCompliant(false)]
//    public static void ReverseBitsInPlace(ref this uint value)
//    {
//      value = (value << 15) | (value >> 17);
//      var tmp = (value ^ (value >> 10)) & 0x003f801f;
//      value = (tmp + (tmp << 10)) ^ value;
//      tmp = (value ^ (value >> 4)) & 0x0e038421;
//      value = (tmp + (tmp << 4)) ^ value;
//      tmp = (value ^ (value >> 2)) & 0x22488842;
//      value = (tmp + (tmp << 2)) ^ value;

//      // Alternatively:
//      // value = ((value & 0xAAAAAAAAu) >> 0x01) | ((value & 0x55555555u) << 0x01);
//      // value = ((value & 0xCCCCCCCCu) >> 0x02) | ((value & 0x33333333u) << 0x02);
//      // value = ((value & 0xF0F0F0F0u) >> 0x04) | ((value & 0x0F0F0F0Fu) << 0x04);
//      // value = ((value & 0xFF00FF00u) >> 0x08) | ((value & 0x00FF00FFu) << 0x08);
//      // value = ((value & 0xFFFF0000u) >> 0x10) | ((value & 0x0000FFFFu) << 0x10);
//    }

//    /// <summary>
//    /// <para>In-place (by ref) mirror the bits (bit-reversal of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
//    /// </summary>
//    [System.CLSCompliant(false)]
//    public static void ReverseBitsInPlace(ref this ulong value)
//    {
//      value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01);
//      value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02);
//      value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04);
//      value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08);
//      value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10);
//      value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20);
//    }

//    #endregion // ReverseBits

//    extension<TBitLength>(TBitLength bitLength)
//      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
//    {
//      #region BitLengthWithinType

//      public TBitLength AssertBitLengthWithinType(string? paramName = "bitLength")
//      {
//        if (!IsBitLengthWithinType(bitLength))
//          throw new System.ArgumentOutOfRangeException(paramName);

//        return bitLength;
//      }

//      public bool IsBitLengthWithinType()
//        => bitLength >= TBitLength.Zero && bitLength <= TBitLength.CreateChecked(GetBitCount(bitLength));

//      #endregion

//      #region Conversions

//      /// <summary>
//      /// <para>A bit-count is the number of storage bits for the type <typeparamref name="TBitLength"/>.</para>
//      /// </summary>
//      /// <typeparam name="TBitLength"></typeparam>
//      /// <param name="source"></param>
//      /// <returns></returns>
//      public int ConvertBitLengthToBitCount()
//        => bitLength.ConvertBitLengthToByteCount() * int.CreateChecked(8);

//      /// <summary>
//      /// <para>A byte-count is the number of storage bytes for the type <typeparamref name="TBitLength"/>.</para>
//      /// </summary>
//      /// <typeparam name="TBitLength"></typeparam>
//      /// <param name="source"></param>
//      /// <returns></returns>
//      public int ConvertBitLengthToByteCount()
//        => int.CreateChecked(double.Ceiling(double.CreateChecked(bitLength) / 8));

//      #endregion // Conversions

//      #region FillBitMask

//      /// <summary>
//      /// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from most-to-least-significant-bits over the <typeparamref name="TBitMask"/>.</para>
//      /// </summary>
//      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
//      public TBitMask FillBitMaskLsb<TBitMask>(TBitMask bitMask, int bitMaskLength)
//        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
//      {
//        bitMask &= TBitMask.CreateChecked((1 << bitMaskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

//        var (q, r) = int.DivRem(int.CreateChecked(bitLength), bitMaskLength);

//        var result = bitMask;

//        for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one) times, hence we skip equal-to zero in the condition.
//          result = bitMask | (result << bitMaskLength); // Shift the mask count bits and | (OR) in count most-significant-bits from bit-mask.

//        if (r > 0)
//          result |= (bitMask & TBitMask.CreateChecked((1 << r) - 1)) << (int.CreateChecked(bitLength) - r);

//        return result;
//      }

//      /// <summary>
//      /// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from least-to-most-significant-bits over the <typeparamref name="TBitMask"/>.</para>
//      /// </summary>
//      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
//      public TBitMask FillBitMaskMsb<TBitMask>(TBitMask bitMask, int bitMaskLength)
//        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
//      {
//        bitMask &= TBitMask.CreateChecked((1 << bitMaskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

//        var (q, r) = int.DivRem(int.CreateChecked(bitLength), bitMaskLength);

//        var result = bitMask;

//        for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one) times, hence we skip equal-to zero in the condition.
//          result = bitMask | (result << bitMaskLength); // Shift the mask count bits and | (OR) in count most-significant-bits from bit-mask.

//        if (r > 0)
//          result = (result << r) | (bitMask >>> (bitMaskLength - r));

//        return result;
//      }

//      #endregion // FillBitMask

//      #region BitMasks

//      /// <summary>
//      /// <para>Create a bit-mask with <paramref name="bitLength"/> least-significant-bits set to 1, and the number of <paramref name="trailingZeroBitCount"/> (least-significant-bits set to zero).</para>
//      /// </summary>
//      /// <typeparam name="TBitLength"></typeparam>
//      /// <param name="bitLength">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitLength"/>.</param>
//      /// <param name="trailingZeroBitCount">The number of least-significant-bits set to zero after the most-significant-1-bits set by <paramref name="bitLength"/>.</param>
//      /// <returns></returns>
//      /// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/> which has a dynamic bit-storage.</remarks>
//      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
//      public TBitLength CreateBitMaskLsbFromBitLength(int trailingZeroBitCount)
//        => CreateBitMaskLsbFromBitLength(bitLength) << trailingZeroBitCount;

//      /// <summary>
//      /// <para>Create a bit-mask with <paramref name="bitLength"/> least-significant-bits set to 1.</para>
//      /// </summary>
//      /// <typeparam name="TBitLength"></typeparam>
//      /// <param name="bitLength">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitLength"/>.</param>
//      /// <returns></returns>
//      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
//      public TBitLength CreateBitMaskLsbFromBitLength()
//        => TBitLength.IsZero(bitLength)
//        ? bitLength
//        : bitLength > TBitLength.CreateChecked(bitLength.GetBitCount())
//        ? throw new System.ArgumentOutOfRangeException(nameof(bitLength))
//        : (((TBitLength.One << (int.CreateChecked(bitLength) - 1)) - TBitLength.One) << 1) | TBitLength.One;

//      /// <summary>
//      /// <para>Create a bit-mask with <paramref name="bitLength"/> most-significant-bits set to 1.</para>
//      /// </summary>
//      /// <typeparam name="TBitLength"></typeparam>
//      /// <param name="bitLength">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitLength"/>.</param>
//      /// <returns></returns>
//      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
//      public TBitLength CreateBitMaskMsbFromBitLength()
//        => CreateBitMaskLsbFromBitLength(bitLength) << (bitLength.GetBitCount() - int.CreateChecked(bitLength));

//      #endregion // BitMasks
//    }

//    /// <summary>
//    /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TInteger"/>, based on byte-count (times 8).</para>
//    /// </summary>
//    /// <remarks>
//    /// <para>The <c>bit-length(<paramref name="value"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="value"/>)</c>, hence a zero-based bit-index is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
//    /// </remarks>
//    //public static int GetBitLength<TInteger>(this TInteger value)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //  => TInteger.IsNegative(value)
//    //  ? value.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
//    //  : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

//    ///// <summary>
//    ///// <para>Uses the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetShortestBitLength()"/>.</para>
//    ///// </summary>
//    ///// <remarks>
//    ///// <para>The bit-length(<paramref name="source"/>) is equal to <c>(1 + log2-toward-zero(<paramref name="source"/>))</c> and is also equal to <c>log2-away-from-zero(<paramref name="source"/>)</c>.</para>
//    ///// </remarks>
//    //public static int GetShortestBitLength<TNumber>(this TNumber source)
//    //  where TNumber : System.Numerics.IBinaryInteger<TNumber>
//    //  => source.GetShortestBitLength();

//    extension<TInteger>(TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      #region BitFold

//      /// <summary>
//      /// <para>Recursively "folds" all 1-bits into lower (right) bits, by taking the most-significant-1-bits (MS1B) and OR it with (MS1B - 1), ending with bottom (right) bits (from MS1B on) set to 1.</para>
//      /// <para>The process yields a bit vector with the same most-significant-1-bit as <paramref name="value"/>, and all 1's below it.</para>
//      /// </summary>
//      /// <returns>All bits set from MS1B down, or -1 (all bits) if the value is less than zero.</returns>
//      public TInteger BitFoldLsb()
//        => TInteger.IsZero(value)
//        ? value
//        : (((value.MostSignificant1Bit() - TInteger.One) << 1) | TInteger.One);

//      /// <summary>
//      /// <para>Recursively "folds" all 1-bits into upper (left) bits, ending with top (left) bits (from LS1B on) set to 1.</para>
//      /// <para>The process yields a bit vector with the same least-significant-1-bit as <paramref name="value"/>, and all 1's above it.</para>
//      /// </summary>
//      /// <returns>All bits set from LS1B up, or -1 if the value is less than zero.</returns>
//      public TInteger BitFoldMsb()
//        => TInteger.IsZero(value)
//        ? value
//        : (value is System.Numerics.BigInteger ? TInteger.CreateChecked(value.GetBitCount()).CreateBitMaskLsbFromBitLength() : ~TInteger.Zero) << value.GetTrailingZeroCount();
//      //var tzc = value.GetTrailingZeroCount();
//      //return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;

//      #endregion

//      #region GetBitLength

//      /// <summary>
//      /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TInteger"/>, based on byte-count (times 8).</para>
//      /// </summary>
//      /// <remarks>
//      /// <para>The <c>bit-length(<paramref name="value"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="value"/>)</c>, hence a zero-based bit-index is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
//      /// </remarks>
//      public int GetBitLength()
//        => TInteger.IsNegative(value)
//        ? value.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
//        : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

//      ///// <summary>
//      ///// <para>Uses the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetShortestBitLength()"/>.</para>
//      ///// </summary>
//      ///// <remarks>
//      ///// <para>The bit-length(<paramref name="source"/>) is equal to <c>(1 + log2-toward-zero(<paramref name="source"/>))</c> and is also equal to <c>log2-away-from-zero(<paramref name="source"/>)</c>.</para>
//      ///// </remarks>
//      //public static int GetShortestBitLength<TInteger>(this TInteger source)
//      //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      //  => source.GetShortestBitLength();

//      #endregion

//      #region BitManipulation

//      /// <summary>
//      /// <para>Gets the bit-index of a power-of-2 number.</para>
//      /// </summary>
//      /// <typeparam name="TInteger"></typeparam>
//      /// <param name="value"></param>
//      /// <returns></returns>
//      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
//      public TInteger BitIndexOfPow2()
//        => TInteger.IsPow2(value) ? TInteger.Log2(value) : throw new System.ArgumentOutOfRangeException(nameof(value), "Must be a value that is a power-of-2.");

//      /// <summary>
//      /// <para>Clear a bit in <paramref name="value"/> based on the 0-based <paramref name="index"/>.</para>
//      /// </summary>
//      /// <typeparam name="TInteger"></typeparam>
//      /// <param name="index"></param>
//      /// <returns></returns>
//      public TInteger ClearBit(int index)
//        => value & ~(TInteger.One << index);

//      /// <summary>
//      /// <para>Flip a bit of <paramref name="value"/> based on the zero-based <paramref name="index"/>.</para>
//      /// </summary>
//      /// <typeparam name="TValue"></typeparam>
//      /// <param name="value"></param>
//      /// <param name="index"></param>
//      /// <returns></returns>
//      public TInteger FlipBit(int index)
//        => value ^ (TInteger.One << index);

//      /// <summary>
//      /// <para>Determine the state of a bit in <paramref name="value"/> based on the 0-based <paramref name="index"/>.</para>
//      /// </summary>
//      /// <typeparam name="TValue"></typeparam>
//      /// <param name="index"></param>
//      /// <returns></returns>
//      public bool GetBit(int index)
//        => !TInteger.IsZero(value & (TInteger.One << index));

//      /// <summary>
//      /// <para>Set a bit in <paramref name="value"/> based on the 0-based <paramref name="index"/>.</para>
//      /// </summary>
//      /// <typeparam name="TInteger"></typeparam>
//      /// <param name="index"></param>
//      /// <returns></returns>
//      public TInteger SetBit(int index)
//        => value | (TInteger.One << index);

//      #endregion

//      #region BitMasks

//      /// <summary>
//      /// <para>Get/check the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
//      /// </summary>
//      public bool BitMaskCheckAll<TBitMask>(TBitMask bitMask)
//        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
//        => TInteger.IsZero(~value & TInteger.CreateChecked(bitMask));

//      /// <summary>
//      /// <para>Get/check the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
//      /// </summary>
//      public bool BitMaskCheckAny<TBitMask>(TBitMask bitMask)
//        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
//        => !TInteger.IsZero(value & TInteger.CreateChecked(bitMask));

//      /// <summary>
//      /// <para>Clear the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
//      /// </summary>
//      public TInteger BitMaskClear<TBitMask>(TBitMask bitMask)
//        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
//        => value & ~TInteger.CreateChecked(bitMask);

//      /// <summary>
//      /// <para>Flip the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
//      /// </summary>
//      public TInteger BitMaskFlip<TBitMask>(TBitMask bitMask)
//        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
//        => value ^ TInteger.CreateChecked(bitMask);

//      /// <summary>
//      /// <para>Flip the bits of <paramref name="value"/> corresponding with the 1-bits in <paramref name="bitMask"/>.</para>
//      /// </summary>
//      public TInteger BitMaskSet<TBitMask>(TBitMask bitMask)
//        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
//        => value | TInteger.CreateChecked(bitMask);

//      ///// <summary>
//      ///// <para>Create a bit-mask with <paramref name="bitLength"/> least-significant-bits set to 1, and the number of <paramref name="trailingZeroBitCount"/> (least-significant-bits set to zero).</para>
//      ///// </summary>
//      ///// <typeparam name="TBitLength"></typeparam>
//      ///// <param name="bitLength">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitLength"/>.</param>
//      ///// <param name="trailingZeroBitCount">The number of least-significant-bits set to zero after the most-significant-1-bits set by <paramref name="bitLength"/>.</param>
//      ///// <returns></returns>
//      ///// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/> which has a dynamic bit-storage.</remarks>
//      ///// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
//      //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//      //public static TBitLength CreateBitMaskLsbFromBitLength<TBitLength>(this TBitLength bitLength, int trailingZeroBitCount)
//      //  where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
//      //  => CreateBitMaskLsbFromBitLength(bitLength) << trailingZeroBitCount;

//      ///// <summary>
//      ///// <para>Create a bit-mask with <paramref name="bitLength"/> least-significant-bits set to 1.</para>
//      ///// </summary>
//      ///// <typeparam name="TBitLength"></typeparam>
//      ///// <param name="bitLength">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitLength"/>.</param>
//      ///// <returns></returns>
//      ///// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
//      //public static TBitLength CreateBitMaskLsbFromBitLength<TBitLength>(this TBitLength bitLength)
//      //  where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
//      //  => TBitLength.IsZero(bitLength)
//      //  ? bitLength
//      //  : bitLength > TBitLength.CreateChecked(bitLength.GetBitCount())
//      //  ? throw new System.ArgumentOutOfRangeException(nameof(bitLength))
//      //  : (((TBitLength.One << (int.CreateChecked(bitLength) - 1)) - TBitLength.One) << 1) | TBitLength.One;

//      ///// <summary>
//      ///// <para>Create a bit-mask with <paramref name="bitLength"/> most-significant-bits set to 1.</para>
//      ///// </summary>
//      ///// <typeparam name="TBitLength"></typeparam>
//      ///// <param name="bitLength">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TBitLength"/>.</param>
//      ///// <returns></returns>
//      ///// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
//      //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//      //public static TBitLength CreateBitMaskMsbFromBitLength<TBitLength>(this TBitLength bitLength)
//      //  where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
//      //  => CreateBitMaskLsbFromBitLength(bitLength) << (bitLength.GetBitCount() - int.CreateChecked(bitLength));

//      ///// <summary>
//      ///// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from most-to-least-significant-bits over the <typeparamref name="TBitMask"/>.</para>
//      ///// </summary>
//      ///// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
//      //public static TBitMask FillBitMaskLsb<TBitMask>(this TBitMask bitMask, int bitMaskLength, int bitLength)
//      //  where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
//      //{
//      //  bitMask &= TBitMask.CreateChecked((1 << bitMaskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

//      //  var (q, r) = int.DivRem(bitLength, bitMaskLength);

//      //  var result = bitMask;

//      //  for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one) times, hence we skip equal-to zero in the condition.
//      //    result = bitMask | (result << bitMaskLength); // Shift the mask count bits and | (OR) in count most-significant-bits from bit-mask.

//      //  if (r > 0)
//      //    result |= (bitMask & TBitMask.CreateChecked((1 << r) - 1)) << (bitLength - r);

//      //  return result;
//      //}

//      ///// <summary>
//      ///// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from least-to-most-significant-bits over the <typeparamref name="TBitMask"/>.</para>
//      ///// </summary>
//      ///// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
//      //public static TBitMask FillBitMaskMsb<TBitMask>(this TBitMask bitMask, int bitMaskLength, int bitLength)
//      //  where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
//      //{
//      //  bitMask &= TBitMask.CreateChecked((1 << bitMaskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

//      //  var (q, r) = int.DivRem(bitLength, bitMaskLength);

//      //  var result = bitMask;

//      //  for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one) times, hence we skip equal-to zero in the condition.
//      //    result = bitMask | (result << bitMaskLength); // Shift the mask count bits and | (OR) in count most-significant-bits from bit-mask.

//      //  if (r > 0)
//      //    result = (result << r) | (bitMask >>> (bitMaskLength - r));

//      //  return result;
//      //}

//      /////// <summary>
//      /////// <para>Create a bit-mask with <paramref name="count"/> most-significant-bits set to 1.</para>
//      /////// </summary>
//      /////// <typeparam name="TValue"></typeparam>
//      /////// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TValue"/>.</param>
//      /////// <returns></returns>
//      ////public static TValue BitMaskLeft<TValue>(this TValue count)
//      ////  where TValue : System.Numerics.IBinaryInteger<TValue>
//      ////  => count.BitMaskRight() << (count.GetBitCount() - int.CreateChecked(count));

//      /////// <summary>
//      /////// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits set to 1, and the number of <paramref name="trailingZeroCount"/> (least-significant-bits set to zero).</para>
//      /////// </summary>
//      /////// <typeparam name="TValue"></typeparam>
//      /////// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TValue"/>.</param>
//      /////// <param name="trailingZeroCount">The number of least-significant-bits set to zero after the most-significant-1-bits set by <paramref name="count"/>.</param>
//      /////// <returns></returns>
//      /////// <remarks>This is a specialized version for <see cref="System.Numerics.BigInteger"/> which has a dynamic bit-storage.</remarks>
//      ////public static TValue BitMaskRight<TValue>(this TValue count, int trailingZeroCount)
//      ////  where TValue : System.Numerics.IBinaryInteger<TValue>
//      ////  => count.BitMaskRight() << trailingZeroCount;

//      /////// <summary>
//      /////// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits set to 1.</para>
//      /////// </summary>
//      /////// <typeparam name="TValue"></typeparam>
//      /////// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TValue"/>.</param>
//      /////// <returns></returns>
//      ////public static TValue BitMaskRight<TValue>(this TValue count)
//      ////  where TValue : System.Numerics.IBinaryInteger<TValue>
//      ////  => (((TValue.One << (int.CreateChecked(count) - 1)) - TValue.One) << 1) | TValue.One;

//      #endregion // BitMasks

//      #region GetBitCount

//      /// <summary>
//      /// <para>Returns the size, in number of bits, needed to store <paramref name="value"/>.</para>
//      /// <para>Most types returns the underlying storage size of the type itself, e.g. <see langword="int"/> = 32 or <see langword="long"/> = 64.</para>
//      /// </summary>
//      /// <remarks>
//      /// <para>Some data types, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetBitCount{TValue}"/> dynamic, and depends on the actual number stored.</para>
//      /// </remarks>
//      public int GetBitCount()
//        => value.GetByteCount() * 8;

//      #endregion

//      #region GetByteCount

//      /// <summary>
//      /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetByteCount(TValue)"/>.</para>
//      /// </summary>
//      /// <remarks>
//      /// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetByteCount{TValue}"/> dynamic also.</para>
//      /// </remarks>
//      public int GetByteCount()
//        => value.GetByteCount();

//      #endregion

//      #region GetPopCount

//      /// <summary>
//      /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.PopCount(TValue)"/>.</para>
//      /// </summary>
//      /// <returns>The population count of <paramref name="value"/>, i.e. the number of bits set to 1 in <paramref name="value"/>.</returns>
//      public int GetPopCount()
//        => int.CreateChecked(TInteger.PopCount(value));

//      #endregion

//      #region GrayCode

//      /// <summary>
//      /// <para>Converts a binary number to a reflected binary Gray code.</para>
//      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
//      /// </summary>
//      public TInteger BinaryToGray()
//        => value ^ (value >>> 1);

//      /// <summary>
//      /// <para>Convert a value to a Gray code with the given base and digits. Iterating through a sequence of values would result in a sequence of Gray codes in which only one digit changes at a time.</para>
//      /// <para><see href="https://en.wikipedia.org/wiki/Gray_code"/></para>
//      /// </summary>
//      /// <remarks>Experimental adaption from wikipedia.</remarks>
//      /// <exception cref="System.ArgumentNullException"></exception>
//      public TInteger[] BinaryToGrayCode(TInteger radix)
//      {
//        if (value < TInteger.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

//        var gray = new TInteger[int.CreateChecked(value.DigitCount(radix))];

//        var baseN = new TInteger[gray.Length]; // Stores the ordinary base-N number, one digit per entry

//        for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
//        {
//          baseN[index] = value % radix;

//          value /= radix;
//        }

//        var shift = TInteger.Zero; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

//        for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
//        {
//          gray[index] = (baseN[index] + shift) % radix;

//          shift = shift + radix - gray[index]; // Subtract from base so shift is positive
//        }

//        return gray;
//      }

//      /// <summary>
//      /// <para>Converts a reflected binary gray code to a binary number.</para>
//      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
//      /// </summary>
//      public TInteger GrayToBinary()
//      {
//        var mask = value;

//        while (!TInteger.IsZero(mask))
//        {
//          mask >>>= 1;
//          value ^= mask;
//        }

//        return value;
//      }

//      #endregion

//      #region Log2

//      /// <summary>
//      /// <para>Computes the ceiling integer-log-2 (a.k.a. ceiling-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index + 1 of a power-of-2 <paramref name="value"/>.</para>
//      /// </summary>
//      /// <param name="value">The value of which to find the log.</param>
//      /// <returns>The ceiling integer log2 of <paramref name="value"/>.</returns>
//      /// <remarks>
//      /// <para>The <c>log2-away-from-zero(<paramref name="value"/>)</c> is equal to <c>bit-length(<paramref name="value"/>)</c>.</para>
//      /// </remarks>
//      public TInteger Log2AwayFromZero()
//        => TInteger.IsZero(value)
//        ? value
//        : TInteger.CopySign(TInteger.Abs(value) is var abs && TInteger.Log2(abs) is var log2 && TInteger.IsPow2(abs) ? log2 : log2 + TInteger.One, value);

//      /// <summary>
//      /// <para>Computes the floor integer-log-2 (a.k.a. floor-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index of a power-of-2 <paramref name="value"/>.</para>
//      /// </summary>
//      /// <param name="value">The value of which to find the log.</param>
//      /// <returns>The floor integer-log-2 of <paramref name="value"/>.</returns>
//      /// <remarks>
//      /// <para>The ceiling log2 of <paramref name="value"/>: <code>(<paramref name="value"/> > 1 ? Log2TowardZero(<paramref name="value"/> - 1) + 1 : 0)</code></para>
//      /// <para>The <c>log2-toward-zero(<paramref name="value"/>)</c> is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
//      /// </remarks>
//      public TInteger Log2TowardZero()
//        => TInteger.IsZero(value)
//        ? value
//        : TInteger.CopySign(TInteger.Log2(TInteger.Abs(value)), value);

//#if INCLUDE_SWAR

//    public static TValue SwarLog2<TValue>(this TValue value)
//      where TValue : System.Numerics.IBinaryInteger<TValue>
//      => TValue.PopCount(SwarFoldRight(value) >> 1);

//#endif

//      #endregion

//      #region Pow2

//      /// <summary>
//      /// <para>Computes the closest power-of-2 in the direction away-from-zero, optionally <paramref name="unequal"/> if <paramref name="value"/> is already a power-of-2.</para>
//      /// <example>
//      /// <para>In order to process for example floating point types:</para>
//      /// <code>TValue.CreateChecked(PowOf2AwayFromZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; TValue.IsInteger(value)))</code>
//      /// </example>
//      /// </summary>
//      /// <typeparam name="TInteger"></typeparam>
//      /// <param name="value"></param>
//      /// <param name="unequal"></param>
//      /// <returns></returns>
//      public TInteger Pow2AwayFromZero(bool unequal)
//        => TInteger.CopySign(TInteger.Abs(value) is var abs && TInteger.IsPow2(abs) ? (unequal ? abs << 1 : abs) : abs.MostSignificant1Bit() << 1, value);

//      /// <summary>
//      /// <para>Computes the closest power-of-2 in the direction toward-zero, optionally <paramref name="unequal"/> if <paramref name="value"/> is already a power-of-2.</para>
//      /// <example>
//      /// <para>In order to process for example a double:</para>
//      /// <code><see cref="double"/>.CreateChecked(PowOf2TowardZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; <see cref="double"/>.IsInteger(value)))</code>
//      /// </example>
//      /// </summary>
//      /// <typeparam name="TInteger"></typeparam>
//      /// <param name="value"></param>
//      /// <param name="unequal"></param>
//      /// <returns></returns>
//      public TInteger Pow2TowardZero(bool unequal)
//         => TInteger.CopySign(TInteger.Abs(value) is var abs && TInteger.IsPow2(abs) && unequal ? abs >> 1 : abs.MostSignificant1Bit(), value);

//#if INCLUDE_SWAR

//    public static TValue SwarNextLargestPow2<TValue>(this TValue value)
//      where TValue : System.Numerics.IBinaryInteger<TValue>
//      => SwarFoldRight(value) + TValue.One;

//#endif

//      #endregion

//      #region ReverseBytes

//      /// <summary>
//      /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
//      /// </summary>
//      /// <remarks>See <see cref="ReverseBits{TValue}(TValue)"/> for bit reversal.</remarks>
//      public TInteger ReverseBytes()
//      {
//        var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the byte reversal.

//        // We can use either direction here, write-LE/read-BE or write-BE/read-LE, doesn't really matter, since the end result is the same.

//        value.WriteLittleEndian(bytes); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).
//        return TInteger.ReadBigEndian(bytes, value.IsUnsignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).

//        //value.WriteBigEndian(bytes); // Write as BigEndian (decreasing numeric significance in increasing memory addresses).
//        //return TValue.ReadLittleEndian(bytes, value.IsUnsignedNumber()); // Read as LittleEndian (increasing numeric significance in increasing memory addresses).
//      }

//      //public static TValue ReverseBytesOwnLoop<TValue>(this TValue value)
//      //  where TValue : System.Numerics.IBinaryInteger<TValue>
//      //{
//      //  var reversed = TValue.Zero;

//      //  var bc = value.GetBitCount();

//      //  var mh = TValue.CreateChecked(0xFF) << (value.GetBitCount() / 2);
//      //  var ml = mh >>> 8;

//      //  for (var vs = 8; vs < bc; vs += 16)
//      //  {
//      //    reversed |= ((value >>> vs) & ml) | ((value << vs) & mh);

//      //    mh <<= 8;
//      //    ml >>>= 8;
//      //  }

//      //  return reversed;
//      //}

//      #endregion

//      #region ShuffleBytes

//      /// <summary>
//      /// <para>Shuffles all bytes of an integer.</para>
//      /// </summary>
//      public TInteger ShuffleBytes()
//      {
//        var bytes = new byte[value.GetByteCount()];
//        value.WriteLittleEndian(bytes);
//        System.Security.Cryptography.RandomNumberGenerator.Shuffle<byte>(bytes);
//        return TInteger.ReadLittleEndian(bytes, value.GetType().IsUnsignedNumericType());
//      }

//      #endregion

//      #region Significant1Bits

//      /// <summary>
//      /// <para>Extracts the lowest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the least-significant-1-bit.</para>
//      /// </summary>
//      /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
//      public TInteger LeastSignificant1Bit()
//        => value & ((~value) + TInteger.One);
//      //=> (value & -value); // This optimized version does not work on unsigned integers.

//      /// <summary>
//      /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
//      /// </summary>
//      /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
//      public TInteger LeastSignificant1BitClear()
//        => value & (value - TInteger.One);

//      /// <summary>
//      /// <para>Extracts the highest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the most-significant-1-bit.</para>
//      /// <list type="bullet">
//      /// <item>If <paramref name="value"/> equal zero, zero is returned.</item>
//      /// <item>If <paramref name="value"/> is negative, min-value of the signed type is returned (i.e. the top most-significant-bit that the type is able to represent).</item>
//      /// <item>Otherwise the most-significant-1-bit is returned, which also happens to be the same as Log2(<paramref name="value"/>).</item>
//      /// </list>
//      /// </summary>
//      /// <remarks>Note that for dynamic types, e.g. <see cref="System.Numerics.BigInteger"/>, the number of bits depends on the storage size used for the <paramref name="value"/>.</remarks>
//      public TInteger MostSignificant1Bit()
//        => TInteger.IsZero(value) ? value : TInteger.One << (value.GetBitLength() - 1);

//      /// <summary>
//      /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
//      /// </summary>
//      /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
//      public TInteger MostSignificant1BitClear()
//        => value - value.MostSignificant1Bit();

//#if INCLUDE_SWAR

//    public static TValue SwarMostSignificant1Bit<TValue>(this TValue source)
//      where TValue : System.Numerics.IBinaryInteger<TValue>
//    {
//      source = SwarFoldRight(source);

//      return (source & ~(source >> 1));
//    }

//#endif

//      #endregion

//      #region ZeroCounts

//      /// <summary>
//      /// <para>A.k.a. count-leading-zero's (clz), counts the number of zero bits preceding the most-significant-1-bit in <paramref name="value"/>. I.e. the number of most-significant-0-bits.</para>
//      /// </summary>
//      /// <remarks>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.LeadingZeroCount(TValue)"/>.</remarks>
//      public int GetLeadingZeroCount()
//        => int.CreateChecked(TInteger.LeadingZeroCount(value));

//      /// <summary>
//      /// <para>A.k.a. called count-trailing-zero's (ctz), counts the number of zero bits trailing the least-significant-1-bit in <paramref name="value"/>. I.e. the number of least-significant-0-bits.</para>
//      /// </summary>
//      /// <remarks>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.TrailingZeroCount(TValue)"/>.</remarks>
//      public int GetTrailingZeroCount()
//        => int.CreateChecked(TInteger.TrailingZeroCount(value));

//#if INCLUDE_SWAR

//    public static int SwarLeadingZeroCount<TValue>(this TValue value)
//      where TValue : System.Numerics.IBinaryInteger<TValue>
//      => GetBitCount(value) - GetPopCount(SwarFoldRight(value));

//#endif

//      #endregion
//    }
//  }
//}
