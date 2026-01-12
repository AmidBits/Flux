namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region BitFold..

      /// <summary>
      /// <para>Recursively "folds" all 1-bits, starting at the least-significant-1-bit, into the left-most or higher-order bits.</para>
      /// <para>Yields a bit vector with the same least-significant-1-bit as <paramref name="value"/>, and with all 1's above it.</para>
      /// </summary>
      /// <returns>The left-most or higher-order bits, to the least-significant-1-bit of <paramref name="value"/>, set to 1. If <paramref name="value"/> is negative, -1 is returned (all bits set to 1). Zero returns 0.</returns>
      public static TInteger BitFoldLeft(TInteger value)
        => TInteger.IsZero(value)
        ? value
        : (value is System.Numerics.BigInteger ? CreateBitMaskRight(TInteger.CreateChecked(value.GetBitCount())) : ~TInteger.Zero) << int.CreateChecked(TInteger.TrailingZeroCount(value));
      //var tzc = value.GetTrailingZeroCount();
      //return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;

      /// <summary>
      /// <para>Recursively "folds" all 1-bits, starting at the most-significant-1-bit, into the right-most or lower-order bits.</para>
      /// <para>Yields a bit vector with the same most-significant-1-bit as <paramref name="value"/>, and with all 1's below it.</para>
      /// </summary>
      /// <returns>The right-most or lower-order bits, to the most-significant-1-bit of <paramref name="value"/>, set to 1. If <paramref name="value"/> is negative, -1 is returned (all bits set to 1). Zero returns 0.</returns>
      public static TInteger BitFoldRight(TInteger value)
        => TInteger.IsZero(value)
        ? value
        : (((MostSignificant1Bit(value) - TInteger.One) << 1) | TInteger.One);

#if INCLUDE_SCRATCH

      /// <summary>
      /// <para>This is the traditional SWAR algorithm that recursively "folds" the lower bits into the upper bits, i.e. folded left or towards the MSB.</para>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TInteger ScratchBitFoldLeft()
      {
        // Or loop to accomodate dynamic data types, but works like the traditional unrolled SWAR below:
        for (var shift = GetBitCount(value) >> 1; shift > 0; shift >>= 1)
          value |= value << shift;

        // value |= (value << 64); // For a 128-bit type.
        // value |= (value << 32); // For a 64-bit type.
        // value |= (value << 16); // For a 32-bit type
        // value |= (value << 8);
        // value |= (value << 4);
        // value |= (value << 2);
        // value |= (value << 1);

        return value;
      }

      /// <summary>
      /// <para>This is the traditional SWAR algorithm that recursively "folds" the upper bits into the lower bits, i.e. folded right or towards the LSB.</para>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TInteger ScratchBitFoldRight()
      {
        // Or loop to accomodate dynamic data types, but works like traditional unrolled SWAR below:
        for (var shift = GetBitCount(value); shift > 0; shift >>= 1)
          value |= value >>> shift; // Unsigned shift right.

        // value |= (value >> 64); // For a 128-bit type.
        // value |= (value >> 32); // For a 64-bit type.
        // value |= (value >> 16); // For a 32-bit type
        // value |= (value >> 8);
        // value |= (value >> 4);
        // value |= (value >> 2);
        // value |= (value >> 1);

        return value;
      }

#endif

      #endregion

      #region Contains..1Bits

      /// <summary>
      /// <para>Checks whether a <paramref name="value"/> contains all 1-bits of a <paramref name="bitMask"/>.</para>
      /// </summary>
      public static bool ContainsAll1Bits<TBitMask>(TInteger value, TBitMask bitMask)
        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
        => TInteger.IsZero(~value & TInteger.CreateChecked(bitMask));

      /// <summary>
      /// <para>Checks whether a <paramref name="value"/> contains any 1-bits of a <paramref name="bitMask"/>.</para>
      /// </summary>
      public static bool ContainsAny1Bits<TBitMask>(TInteger value, TBitMask bitMask)
        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
        => !TInteger.IsZero(value & TInteger.CreateChecked(bitMask));

      #endregion

      #region CreateBitMask..

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="count"/> most-significant-bits set to 1.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
      /// <returns></returns>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
      public static TInteger CreateBitMaskLeft(TInteger count)
      {
        var bitMaskRight = CreateBitMaskRight(count);

        return bitMaskRight << (bitMaskRight.GetBitCount() - int.CreateChecked(count));
      }

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="bitLength"/> number of most-significant-bits from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from least-to-most-significant-bits over the integer.</para>
      /// </summary>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
      public static TInteger CreateBitMaskLeft(TInteger bitMask, int bitMaskLength, int bitLength)
      {
        bitMask &= TInteger.CreateChecked((1 << bitMaskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

        var (q, r) = int.DivRem(bitLength, bitMaskLength);

        var result = bitMask;

        for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one) times, hence we skip equal-to zero in the condition.
          result = bitMask | (result << bitMaskLength); // Shift the mask count bits and | (OR) in count most-significant-bits from bit-mask.

        if (r > 0)
          result = (result << r) | (bitMask >>> (bitMaskLength - r));

        return result;
      }

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits set to 1.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
      /// <returns></returns>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
      public static TInteger CreateBitMaskRight(TInteger count)
      {
        if (TInteger.IsZero(count))
          return count;

        return (((TInteger.One << (int.CreateChecked(count) - 1)) - TInteger.One) << 1) | TInteger.One;
      }

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from most-to-least-significant-bits over the <typeparamref name="TBitMask"/>.</para>
      /// </summary>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
      public static TInteger CreateBitMaskRight(TInteger bitMask, int bitMaskLength, int bitLength)
      {
        bitMask &= TInteger.CreateChecked((1 << bitMaskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

        var (q, r) = int.DivRem(bitLength, bitMaskLength);

        var result = bitMask;

        for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one) times, hence we skip equal-to zero in the condition.
          result = bitMask | (result << bitMaskLength); // Shift the mask count bits and | (OR) in count most-significant-bits from bit-mask.

        if (r > 0)
          result |= (bitMask & TInteger.CreateChecked((1 << r) - 1)) << (bitLength - r);

        return result;
      }

      #endregion

      #region Gray code

      /// <summary>
      /// <para>Converts a binary number to a reflected binary Gray code.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      public static TInteger ConvertBinaryToGray(TInteger value)
        => value ^ (value >>> 1);

      // The binary to gray below is from Wikipedia.org, but since gray code is not uniquely defined, I am not sure how valuable it really is.
      ///// <summary>
      ///// <para>Convert a value to a Gray code with the given base and digits. Iterating through a sequence of values would result in a sequence of Gray codes in which only one digit changes at a time.</para>
      ///// <para><see href="https://en.wikipedia.org/wiki/Gray_code"/></para>
      ///// </summary>
      ///// <remarks>Experimental adaption from wikipedia.</remarks>
      ///// <exception cref="System.ArgumentNullException"></exception>
      //public static TInteger[] ConvertBinaryToGray(TInteger value, TInteger radix)
      //{
      //  if (value < TInteger.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      //  var gray = new TInteger[int.CreateChecked(Units.Radix.DigitCount(value, radix))];

      //  var baseN = new TInteger[gray.Length]; // Stores the ordinary base-N number, one digit per entry

      //  for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
      //  {
      //    baseN[index] = value % radix;

      //    value /= radix;
      //  }

      //  var shift = TInteger.Zero; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

      //  for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
      //  {
      //    gray[index] = (baseN[index] + shift) % radix;

      //    shift = shift + radix - gray[index]; // Subtract from base so shift is positive
      //  }

      //  return gray;
      //}

      /// <summary>
      /// <para>Converts a reflected binary gray code to a binary number.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      public static TInteger ConvertGrayToBinary(TInteger value)
      {
        var mask = value;

        while (!TInteger.IsZero(mask))
        {
          mask >>>= 1;
          value ^= mask;
        }

        return value;
      }

      #endregion

      #region IntegerLog2

      public static (TInteger TowardZero, TInteger AwayFromZero) IntegerLog2(TInteger value)
      {
        if (TInteger.IsNegative(value))
        {
          var (log2tz, log2afz) = IntegerLog2(TInteger.Abs(value));

          return (-log2tz, -log2afz);
        }

        if (TInteger.IsZero(value))
          return (value, value);

        var log2 = TInteger.Log2(value);

        return (log2, TInteger.IsPow2(value) ? log2 : log2 + TInteger.One);
      }

      #endregion

      #region Pow2

      /// <summary>
      /// <para>Returns the power-of-2 of value, toward-zero and away-from-zero.</para>
      /// </summary>
      /// <param name="unequal"></param>
      /// <returns></returns>
      public static (TInteger TowardZero, TInteger AwayFromZero) Pow2(TInteger value, bool unequal)
      {
        if (TInteger.IsNegative(value))
        {
          var (pow2tz, pow2afz) = Pow2(TInteger.Abs(value), unequal);

          return (-pow2tz, -pow2afz);
        }

        if (TInteger.IsPow2(value))
        {
          if (unequal)
            return (value >> 1, value << 1);

          return (value, value);
        }

        var ms1b = MostSignificant1Bit(value);

        return (ms1b, ms1b << 1);
      }

      #endregion

      #region ReverseBits

      /// <summary>
      /// <para>Reverses the bits of an integer. The LSBs (least significant bits) becomes the MSBs (most significant bits) and vice versa, i.e. the bits are mirrored across the integer storage space. It's a reversal of all storage bits.</para>
      /// </summary>
      /// <remarks>See <see cref="ReverseBytes{TInteger}(TInteger)"/> for byte reversal.</remarks>
      public static TInteger ReverseBits(TInteger value)
      {
        var count = value.GetByteCount();

        var bytes = System.Buffers.ArrayPool<byte>.Shared.Rent(count); // Retrieve the byte size of the number, which will be the basis for the bit reversal.

        var span = bytes.AsSpan(0, count);

        value.WriteLittleEndian(span); // Write as BigEndian (high-to-low).

        for (var i = span.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
          byte.ReverseBitsInPlace(ref span[i]); // Mirror (reverse) bits in each byte.

        var result = TInteger.ReadBigEndian(span, value.GetType().IsNumericsIUnsignedNumber()); // Read as LittleEndian (low-to-high).

        System.Buffers.ArrayPool<byte>.Shared.Return(bytes);

        return result;
      }

      #endregion

      #region ReverseBytes

      /// <summary>
      /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
      /// </summary>
      /// <remarks>See <see cref="ReverseBits{TInteger}(TInteger)"/> for bit reversal.</remarks>
      public static TInteger ReverseBytes(TInteger value)
      {
        var count = value.GetByteCount();

        var bytes = System.Buffers.ArrayPool<byte>.Shared.Rent(count); // Retrieve the byte size of the number, which will be the basis for the bit reversal.

        var span = bytes.AsSpan(0, count);

        // We can use either direction here, write-LE/read-BE or write-BE/read-LE, doesn't really matter, since the end result is the same.

        value.WriteLittleEndian(span); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).

        var result = TInteger.ReadBigEndian(span, value.GetType().IsNumericsIUnsignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).

        System.Buffers.ArrayPool<byte>.Shared.Return(bytes);

        return result;
      }

      #endregion

      #region ShuffleBytes

      /// <summary>
      /// <para>Shuffles all bytes of an integer.</para>
      /// </summary>
      public static TInteger ShuffleBytes(TInteger value, System.Random? rng = null)
      {
        rng ??= System.Random.Shared;

        var bytes = System.Buffers.ArrayPool<byte>.Shared.Rent(value.GetByteCount());

        value.WriteLittleEndian(bytes);

        rng.Shuffle(bytes);

        var result = TInteger.ReadLittleEndian(bytes, value.GetType().IsNumericsIUnsignedNumber());

        System.Buffers.ArrayPool<byte>.Shared.Return(bytes);

        return result;
      }

      #endregion

      #region ..Significant1Bit

      /// <summary>
      /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
      /// </summary>
      /// <see href="https://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
      public static TInteger ClearLeastSignificant1Bit(TInteger value)
        => value & (value - TInteger.One);

      /// <summary>
      /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
      /// </summary>
      /// <see href="https://aggregate.org/MAGIC/#Most%20Significant%201%20Bit"/>
      public static TInteger ClearMostSignificant1Bit(TInteger value)
        => value - MostSignificant1Bit(value);

      /// <summary>
      /// <para>Extracts the lowest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the least-significant-1-bit.</para>
      /// </summary>
      /// <remarks>The LS1B is the largest power of two that is also a divisor of <paramref name="value"/>.</remarks>
      /// <see href="https://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
      public static TInteger LeastSignificant1Bit(TInteger value)
        => value & ((~value) + TInteger.One);
      //=> (value & -value); // This optimized version does not work on unsigned integers.

      /// <summary>
      /// <para>Extracts the highest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the most-significant-1-bit.</para>
      /// <list type="bullet">
      /// <item>If <paramref name="value"/> equal zero, zero is returned.</item>
      /// <item>If <paramref name="value"/> is negative, min-value of the signed type is returned (i.e. the top most-significant-bit that the type is able to represent).</item>
      /// <item>Otherwise the most-significant-1-bit is returned, which also happens to be the same as Log2(<paramref name="value"/>).</item>
      /// </list>
      /// </summary>
      /// <remarks>Note that for dynamic types, e.g. <see cref="System.Numerics.BigInteger"/>, the number of bits depends on the storage size used for the <paramref name="value"/>.</remarks>
      public static TInteger MostSignificant1Bit(TInteger value)
        => TInteger.IsZero(value) ? value : TInteger.One << (value.GetBitLength() - 1);

#if INCLUDE_SCRATCH

            public static TInteger ScratchLeastSignificant1Bit(TInteger value)
              => value & ((~value) + TInteger.One); // Works on signed or unsigned integers.
            // => (value ^ (value & (value - TInteger.One))); // Alternative to the above.
            // => (value & -value); // Does not work on unsigned integers.

            public static TInteger ScratchMostSignificant1Bit(TInteger value)
            {
              value = ScratchBitFoldRight(value);

              return value & ~(value >> 1);
            }

#endif

      #endregion
    }

    extension<TInteger>(TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region GetBitLength

      /// <summary>
      /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TInteger"/>, based on byte-count (times 8).</para>
      /// </summary>
      /// <remarks>
      /// <para>The <c>bit-length(<paramref name="value"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="value"/>)</c>, hence a zero-based bit-index is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
      /// </remarks>
      public int GetBitLength()
        => TInteger.IsNegative(value)
        ? value.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
        : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

#if INCLUDE_SCRATCH

      /// <summary>
      /// <para><see href="https://aggregate.org/MAGIC/#Log2%20of%20an%20Integer"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TInteger ScratchBitLength()
        => ScratchLog2(value) + TInteger.One;

#endif

      #endregion

      #region Get..Count()

      /// <summary>
      /// <para>Returns the size, in number of bits, needed to store <paramref name="value"/>.</para>
      /// <para>Most types returns the underlying storage size of the type itself, e.g. <see langword="int"/> = 32 or <see langword="long"/> = 64.</para>
      /// </summary>
      /// <remarks>
      /// <para>Some data types, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetBitCount{TValue}"/> dynamic, and depends on the actual number stored.</para>
      /// </remarks>
      public int GetBitCount()
        => value.GetByteCount() * 8;

      ///// <summary>
      ///// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TInteger}.GetByteCount(TInteger)"/>.</para>
      ///// </summary>
      ///// <remarks>
      ///// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetByteCount{TValue}"/> dynamic also.</para>
      ///// </remarks>
      //public int GetByteCount()
      //  => value.GetByteCount();

      ///// <summary>
      ///// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TInteger}.PopCount(TInteger)"/>.</para>
      ///// </summary>
      ///// <returns>The population count of <paramref name="value"/>, i.e. the number of bits set to 1 in <paramref name="value"/>.</returns>
      //public int GetPopCount()
      //  => int.CreateChecked(TInteger.PopCount(value));

#if INCLUDE_SCRATCH

      public int ScratchGetPopCount()
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);

        var count = 0;

        while (value > TInteger.Zero)
        {
          count++;

          value &= value - TInteger.One; // Clear the LS1B.
        }

        return count;
      }

#endif

      #endregion
    }

    #region BitSwaps

    /// <summary>
    /// <para>Swap adjacent 1-bits (single bits).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap1(this ulong value) => ((value & 0xaaaaaaaaaaaaaaaaUL) >> 0x01) | ((value & 0x5555555555555555UL) << 0x01);

    /// <summary>
    /// <para>Swap adjacent 2-bits (pairs).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap2(this ulong value) => ((value & 0xccccccccccccccccUL) >> 0x02) | ((value & 0x3333333333333333UL) << 0x02);

    /// <summary>
    /// <para>Swap adjacent 4-bits (nibbles).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap4(this ulong value) => ((value & 0xf0f0f0f0f0f0f0f0UL) >> 0x04) | ((value & 0x0f0f0f0f0f0f0f0fUL) << 0x04);

    /// <summary>
    /// <para>Swap adjacent 8-bits (bytes).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap8(this ulong value) => ((value & 0xff00ff00ff00ff00UL) >> 0x08) | ((value & 0x00ff00ff00ff00ffUL) << 0x08);

    /// <summary>
    /// <para>Swap adjacent 16-bits.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap16(this ulong value) => ((value & 0xffff0000ffff0000UL) >> 0x10) | ((value & 0x0000ffff0000ffffUL) << 0x10);

    /// <summary>
    /// <para>Swap adjacent 32-bits.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap32(this ulong value) => ((value & 0xffffffff00000000UL) >> 0x20) | ((value & 0x00000000ffffffffUL) << 0x20);

    #endregion // BitSwaps

    #region MortonNumber

    /// <summary>
    /// <para>Interleave bits of byte <paramref name="x"/> and byte <paramref name="y"/>, so that all of the bits of <paramref name="x"/> are in the even positions and <paramref name="y"/> in the odd, resulting in a Morton Number.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static int MortonNumber(this byte x, byte y)
      => unchecked((int)(((x * 0x0101010101010101UL & 0x8040201008040201UL) * 0x0102040810204081UL >> 49) & 0x5555 | ((y * 0x0101010101010101UL & 0x8040201008040201UL) * 0x0102040810204081UL >> 48) & 0xAAAA));

    #endregion
  }
}
