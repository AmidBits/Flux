//namespace Flux
//{
//  public static partial class BitLength
//  {
//    /// <summary>
//    /// </summary>
//    /// <param name="value">The total number of items in the set. Greater than or equal to <paramref name="k"/>.</param>
//    /// <returns></returns>
//    extension<TBitLength>(TBitLength bitLength)
//      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
//    {
//      public TBitLength AssertBitLengthWithinType(string? paramName = "bitLength")
//      {
//        if (!IsBitLengthWithinType(bitLength))
//          throw new System.ArgumentOutOfRangeException(paramName);

//        return bitLength;
//      }

//      /// <summary>
//      /// <para>A bit-count is the number of storage bits for the type <typeparamref name="TBitLength"/>.</para>
//      /// </summary>
//      /// <typeparam name="TBitLength"></typeparam>
//      /// <param name="source"></param>
//      /// <returns></returns>
//      public TBitLength ConvertBitLengthToBitCount()
//        => bitLength.ConvertBitLengthToByteCount() * TBitLength.CreateChecked(8);

//      /// <summary>
//      /// <para>A byte-count is the number of storage bytes for the type <typeparamref name="TBitLength"/>.</para>
//      /// </summary>
//      /// <typeparam name="TBitLength"></typeparam>
//      /// <param name="source"></param>
//      /// <returns></returns>
//      public TBitLength ConvertBitLengthToByteCount()
//        => TBitLength.CreateChecked(double.Ceiling(double.CreateChecked(bitLength) / 8));

//      /// <summary>
//      /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TInteger"/>, based on byte-count (times 8).</para>
//      /// </summary>
//      /// <remarks>
//      /// <para>The <c>bit-length(<paramref name="value"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="value"/>)</c>, hence a zero-based bit-index is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
//      /// </remarks>
//      public int GetBitLength()
//        => TBitLength.IsNegative(bitLength)
//        ? bitLength.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
//        : bitLength.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

//      ///// <summary>
//      ///// <para>Uses the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetShortestBitLength()"/>.</para>
//      ///// </summary>
//      ///// <remarks>
//      ///// <para>The bit-length(<paramref name="source"/>) is equal to <c>(1 + log2-toward-zero(<paramref name="source"/>))</c> and is also equal to <c>log2-away-from-zero(<paramref name="source"/>)</c>.</para>
//      ///// </remarks>
//      //public static int GetShortestBitLength<TNumber>(this TNumber source)
//      //  where TNumber : System.Numerics.IBinaryInteger<TNumber>
//      //  => source.GetShortestBitLength();

//      /// <summary>
//      /// <para>Computes the max number of digits that can be represented by the specified <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether to <paramref name="accountForSignBit"/>.</para>
//      /// <code>var mdcf = (10).GetMaxDigitCount(10, false); // Yields 4, because a max value of 1023 can be represented (all bits can be used in an unsigned value).</code>
//      /// <code>var mdct = (10).GetMaxDigitCount(10, true); // Yields 3, because a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
//      /// <code>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</code>
//      /// </summary>
//      /// <typeparam name="TBitLength"></typeparam>
//      /// <typeparam name="TRadix"></typeparam>
//      /// <param name="bitLength">This is the number of bits to take into account.</param>
//      /// <param name="radix">This is the radix (base) to use.</param>
//      /// <param name="accountForSignBit">Indicates whether <paramref name="bitLength"/> use one bit for the sign.</param>
//      /// <returns></returns>
//      public int GetMaxDigitCountOfBitLength<TRadix>(TRadix radix, bool accountForSignBit)
//        where TRadix : System.Numerics.IBinaryInteger<TRadix>
//      {
//        if (TBitLength.IsNegative(bitLength))
//          return 0;

//        var swar = System.Numerics.BigInteger.CreateChecked(bitLength).CreateBitMaskLsbFromBitLength(); // Create a bit-mask representing the greatest value for the bit-length.

//        if (swar.IsSingleDigit(radix)) // If SWAR is less than radix, there is only one digit, otherwise, compute for values higher than radix, and more digits.
//          return 1;

//        if (accountForSignBit) // If accounting for a sign-bit, shift the SWAR to properly represent the max of a signed type.
//          swar >>>= 1;

//        var logc = swar.FastIntegerLog(radix, UniversalRounding.WholeAwayFromZero, out var _);

//        return int.CreateChecked(logc);
//      }

//      public bool IsBitLengthWithinType()
//        => bitLength >= TBitLength.Zero && bitLength <= TBitLength.CreateChecked(bitLength.GetBitCount());
//    }
//  }
//}
