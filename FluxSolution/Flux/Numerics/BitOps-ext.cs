namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
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
    }

    //    extension<TInteger>(TInteger value)
    //      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    //    {
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

    //#if INCLUDE_SCRATCH

    //      /// <summary>
    //      /// <para><see href="https://aggregate.org/MAGIC/#Log2%20of%20an%20Integer"/></para>
    //      /// </summary>
    //      /// <typeparam name="TInteger"></typeparam>
    //      /// <param name="value"></param>
    //      /// <returns></returns>
    //      public TInteger ScratchBitLength()
    //        => ScratchLog2(value) + TInteger.One;

    //#endif

    //      #endregion

    //      #region Get..Count()

    //      /// <summary>
    //      /// <para>Returns the size, in number of bits, needed to store <paramref name="value"/>.</para>
    //      /// <para>Most types returns the underlying storage size of the type itself, e.g. <see langword="int"/> = 32 or <see langword="long"/> = 64.</para>
    //      /// </summary>
    //      /// <remarks>
    //      /// <para>Some data types, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetBitCount{TValue}"/> dynamic, and depends on the actual number stored.</para>
    //      /// </remarks>
    //      public int GetBitCount()
    //        => value.GetByteCount() * 8;

    //      ///// <summary>
    //      ///// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TInteger}.GetByteCount(TInteger)"/>.</para>
    //      ///// </summary>
    //      ///// <remarks>
    //      ///// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetByteCount{TValue}"/> dynamic also.</para>
    //      ///// </remarks>
    //      //public int GetByteCount()
    //      //  => value.GetByteCount();

    //      ///// <summary>
    //      ///// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TInteger}.PopCount(TInteger)"/>.</para>
    //      ///// </summary>
    //      ///// <returns>The population count of <paramref name="value"/>, i.e. the number of bits set to 1 in <paramref name="value"/>.</returns>
    //      //public int GetPopCount()
    //      //  => int.CreateChecked(TInteger.PopCount(value));

    //#if INCLUDE_SCRATCH

    //      public int ScratchGetPopCount()
    //      {
    //        System.ArgumentOutOfRangeException.ThrowIfNegative(value);

    //        var count = 0;

    //        while (value > TInteger.Zero)
    //        {
    //          count++;

    //          value &= value - TInteger.One; // Clear the LS1B.
    //        }

    //        return count;
    //      }

    //#endif

    //      #endregion
    //    }

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
    /// <para>Swap adjacent 16-bits (short words).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap16(this ulong value) => ((value & 0xffff0000ffff0000UL) >> 0x10) | ((value & 0x0000ffff0000ffffUL) << 0x10);

    /// <summary>
    /// <para>Swap adjacent 32-bits (words).</para>
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
