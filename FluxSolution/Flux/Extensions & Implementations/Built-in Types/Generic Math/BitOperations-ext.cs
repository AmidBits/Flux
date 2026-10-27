namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region ClearLeastSignificant1Bit

      /// <summary>
      /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
      /// </summary>
      /// <see href="https://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
      public static TInteger ClearLeastSignificant1Bit(TInteger value)
        => value & (value - TInteger.One);

      #endregion

      #region ClearMostSignificant1Bit

      /// <summary>
      /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
      /// </summary>
      /// <see href="https://aggregate.org/MAGIC/#Most%20Significant%201%20Bit"/>
      public static TInteger ClearMostSignificant1Bit(TInteger value)
        => value - value.MostSignificant1Bit;

      #endregion

      #region CreateLsbMask

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits (a.k.a. low-order or right-most bits) set to 1.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
      /// <returns></returns>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
      public static TInteger CreateLsbMask(TInteger count)
      {
        var cnt = int.CreateChecked(count);

        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(cnt, count.BitWidth);

        if (TInteger.IsZero(count))
          return count;

        return (((TInteger.One << (cnt - 1)) - TInteger.One) << 1) | TInteger.One;
      }

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits (a.k.a. low-order or right-most bits) from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from most-to-least-significant-bits over the <typeparamref name="TBitMask"/>.</para>
      /// </summary>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
      public static TInteger CreateLsbMask(TInteger bitMask, int bitMaskLength, int bitLength)
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

      #region CreateMsbMask

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="count"/> most-significant-bits (a.k.a. high-order or left-most bits) set to 1.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
      /// <returns></returns>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
      public static TInteger CreateMsbMask(TInteger count)
      {
        if (TInteger.IsZero(count))
          return count;

        return CreateLsbMask(count) << (TInteger.Zero.BitWidth - int.CreateChecked(count));
      }

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="bitLength"/> number of most-significant-bits (a.k.a. high-order or left-most bits) from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from least-to-most-significant-bits over the integer.</para>
      /// </summary>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
      public static TInteger CreateMsbMask(TInteger bitMask, int bitMaskLength, int bitLength)
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

      #endregion

      #region GrayCode

      /// <summary>
      /// <para>Converts a binary number to a reflected binary Gray code.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      public static TInteger BinaryToGrayCode(TInteger value)
        => value ^ (value >>> 1);

      /// <summary>
      /// <para>Converts a reflected binary gray code to a binary number.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      public static TInteger GrayCodeToBinary(TInteger value)
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

      #region MaxRepresentableDigitCount

      /// <summary>
      /// <para>Computes the max number of digits that can be represented by the specified <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether to <paramref name="accountForSignBit"/>.</para>
      /// <code>var mdcf = BinaryInteger.MaxRepresentableDigitCount(10, 10, false); // Yields 4, because a max value of 1023 can be represented (all bits can be used in an unsigned value).</code>
      /// <code>var mdct = BinaryInteger.MaxRepresentableDigitCount(10, 10, true); // Yields 3, because a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="bitLength">This is the number of bits to take into account.</param>
      /// <param name="radix">This is the radix (base) to use.</param>
      /// <param name="accountForSignBit">Indicates whether one bit should be used for the sign.</param>
      /// <returns></returns>
      public static int MaxRepresentableDigitCount<TRadix>(TInteger bitLength, TRadix radix, bool accountForSignBit)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bitLength);
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var effectiveBitLength = accountForSignBit ? bitLength - TInteger.One : bitLength;

        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(effectiveBitLength);

        var mask = CreateLsbMask(System.UInt128.CreateChecked(effectiveBitLength)); // Create a bit-mask representing the greatest value for the bit-length. System.UInt128 is used to ensure that the mask can be created for all bit-lengths, even those greater than 64 bits.

        return int.CreateChecked(DigitCount(mask, radix));
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

        var bytes = (stackalloc byte[count]); // Retrieve the byte size of the number, which will be the basis for the bit reversal.

        value.WriteLittleEndian(bytes); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).

        for (var i = bytes.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
          byte.ReverseBitsInPlace(ref bytes[i]); // Mirror (reverse) bits in each byte.

        return TInteger.ReadBigEndian(bytes, typeof(TInteger).IsIUnsignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).
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

        var bytes = (stackalloc byte[count]); // Retrieve the byte size of the number, which will be the basis for the bit reversal.

        // We can use either direction here, write-LE/read-BE or write-BE/read-LE, doesn't really matter, since the end result is the same.

        value.WriteLittleEndian(bytes); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).

        return TInteger.ReadBigEndian(bytes, typeof(TInteger).IsIUnsignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).
      }

      #endregion

      #region ShuffleBytes

      /// <summary>
      /// <para>Shuffles all bytes of an integer.</para>
      /// </summary>
      public static TInteger ShuffleBytes(TInteger value, System.Random? rng = null)
      {
        rng ??= System.Random.Shared;

        var bytes = (stackalloc byte[value.GetByteCount()]);

        value.WriteLittleEndian(bytes);

        rng.Shuffle(bytes);

        return TInteger.ReadLittleEndian(bytes, typeof(TInteger).IsIUnsignedNumber());
      }

      #endregion
    }

    extension<TInteger>(TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region BitFoldToLsb

      /// <summary>
      /// <para>Recursively "folds" all 1-bits, starting at the most-significant-bit, into the right-most or lower-order bits.</para>
      /// <para>Yields a bit vector with the same most-significant-bit as <paramref name="value"/>, and with all 1's below it.</para>
      /// </summary>
      /// <returns>The right-most or lower-order bits, up to the most-significant-bit of <paramref name="value"/>, set to 1. If <paramref name="value"/> is negative, -1 is returned (all bits set to 1). Zero returns 0.</returns>
      public TInteger BitFoldToLsb()
      {
        if (TInteger.IsZero(value))
          return value;

        var ms1b = value.MostSignificant1Bit;

        return ms1b | (ms1b - TInteger.One);
      }

#if INCLUDE_SCRATCH

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

      #region BitFoldToMsb

      /// <summary>
      /// <para>Recursively "folds" all 1-bits, starting at the least-significant-bit, into the left-most or higher-order bits. Yields a bit vector with the same least-significant-bit as <paramref name="value"/>, and with all 1's above it.</para>
      /// <para><see cref="System.Numerics.BigInteger"/> does not have a fixed width so the result is dependent on <paramref name="value"/>.</para>
      /// </summary>
      /// <returns>The left-most or higher-order bits, down to the least-significant-bit of <paramref name="value"/>, set to 1. If <paramref name="value"/> is negative, -1 is returned (all bits set to 1). Zero returns 0.</returns>
      public TInteger BitFoldToMsb()
        => ~(value.LeastSignificant1Bit - TInteger.One);
      //{
      //  if (TInteger.IsZero(value))
      //    return value;

      //  if (value is System.Numerics.BigInteger) // BigInteger is a special case.
      //    return CreateBitMaskRight(TInteger.CreateChecked(value.BitCount));

      //  return (~TInteger.Zero) << int.CreateChecked(TInteger.TrailingZeroCount(value));
      //}

      //var tzc = value.GetTrailingZeroCount();
      //return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;

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

#endif

      #endregion

      #region BitLength

      /// <summary>
      /// <para>Returns the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TInteger"/>, based on byte-count (times 8).</para>
      /// </summary>
      /// <remarks>
      /// <para>The bit-length yields the (1-based) position, which is one more than the 0-based index found by log2) of the most significant bit set in <paramref name="value"/>.</para>
      /// </remarks>
      public int BitLength
        => TInteger.IsNegative(value)
        ? value.BitWidth // When value is negative, return the bit-count (i.e. based on the storage strategy).
        : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

#if INCLUDE_SCRATCH

      /// <summary>
      /// <para><see href="https://aggregate.org/MAGIC/#Log2%20of%20an%20Integer"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TInteger ScratchBitLength
        => ScratchLog2(value) + TInteger.One;

#endif

      #endregion

      #region BitWidth

      /// <summary>
      /// <para>Returns the size, in number of bits, needed to store the value as <typeparamref name="TInteger"/>.</para>
      /// <para>For fixed-size types, the returned value is the type's pre-defined width (for example <see langword="int"/> = 32 and <see langword="long"/> = 64).</para>
      /// <para>For dynamic-size types, such as <see cref="System.Numerics.BigInteger"/>, the returned value is the width of the allocated representation currently used to store the value.</para>
      /// </summary>
      public int BitWidth
        => value.GetByteCount() * 8;

      #endregion

      #region ByteCount

      /// <summary>
      /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TInteger}.GetByteCount()"/>.</para>
      /// </summary>
      /// <remarks>
      /// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies.</para>
      /// </remarks>
      public int ByteCount
        => value.GetByteCount();

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

      #region LeastSignificant1Bit

      /// <summary>
      /// <para>Extracts the lowest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the least-significant-1-bit.</para>
      /// </summary>
      /// <remarks>The LS1B is the largest power of two that is also a divisor of <paramref name="value"/>.</remarks>
      /// <see href="https://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
      public TInteger LeastSignificant1Bit
        => value & ((~value) + TInteger.One);
      //=> (value ^ (value & (value - TInteger.One))); // Alternative to the above.
      //=> (value & -value); // <<< This optimized version does not work on unsigned integers, obviously since the number has to be negated.

      #endregion

      #region MostSignificant1Bit

      /// <summary>
      /// <para>Extracts the highest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the most-significant-1-bit.</para>
      /// <list type="bullet">
      /// <item>If <paramref name="value"/> equal zero, zero is returned.</item>
      /// <item>If <paramref name="value"/> is negative, min-value of the signed type is returned (i.e. the top most-significant-bit that the type is able to represent).</item>
      /// <item>Otherwise the most-significant-1-bit is returned, which also happens to be the same as Log2(<paramref name="value"/>).</item>
      /// </list>
      /// </summary>
      /// <remarks>
      /// <para>The index and position of the most significant bit set in a value, is the log2(value) and bit-length(value) respectively.</para>
      /// </remarks>
      public TInteger MostSignificant1Bit
        => TInteger.IsZero(value)
        ? value
        : TInteger.One << int.CreateChecked(TInteger.Log2(value)); // alternative: TInteger.One << (value.GetBitLength() - 1);

#if INCLUDE_SCRATCH

            public TInteger ScratchLeastSignificant1Bit
              => value & ((~value) + TInteger.One); // Works on signed or unsigned integers.
            //=> (value ^ (value & (value - TInteger.One))); // Alternative to the above.
            //=> (value & -value); // Does not work on unsigned integers.

            public TInteger ScratchMostSignificant1Bit
            {
              get
              {
                value = ScratchBitFoldRight(value);

                return value & ~(value >> 1);
              }
            }

#endif

      #endregion

      #region RoundUpToPowerOf2

      public TInteger RoundUpToPowerOf2(bool unequal)
      {
        var ms1b = TInteger.Abs(value).MostSignificant1Bit;

        if (unequal || ms1b != value)
          ms1b <<= 1;

        return TInteger.CopySign(ms1b, value);
      }

      #endregion

      #region RoundDownToPowerOf2

      public TInteger RoundDownToPowerOf2(bool unequal)
      {
        var ms1b = TInteger.Abs(value).MostSignificant1Bit;

        if (unequal && ms1b == value)
          ms1b >>>= 1;

        return TInteger.CopySign(ms1b, value);
      }

      #endregion
    }
  }
}

// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>
