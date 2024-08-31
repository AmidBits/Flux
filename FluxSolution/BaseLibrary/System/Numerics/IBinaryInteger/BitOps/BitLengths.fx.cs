namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TValue"/>, based on byte-count (times 8).</para>
    /// </summary>
    /// <remarks>
    /// <para>The <c>bit-length(<paramref name="value"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="value"/>)</c>, hence a zero-based bit-index is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
    /// </remarks>
    public static int GetBitLengthEx<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.IsNegative(value)
      ? value.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
      : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

    /// <summary>
    /// <para>Computes the max number of digits that can be represented by the specified <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether to <paramref name="accountForSignBit"/>.</para>
    /// <code>var maxDigitCount = Flux.Bits.GetMaxDigitCount(10, 10, false); // Yields 4, because with 10 bits, a radix of 10 (the decimal system) and unsigned, a max value of 1023 can be represented (all bits can be used, because it is an unsigned value).</code>
    /// <code>var maxDigitCount = Flux.Bits.GetMaxDigitCount(10, 10, true); // Yields 3, because with 10 bits, a radix of 10 (the decimal system) and signed, a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="bitLength">This is the number of bits to take into account.</param>
    /// <param name="radix">This is the radix (base) to use.</param>
    /// <param name="accountForSignBit">Indicates whether <paramref name="bitLength"/> use one bit for the sign.</param>
    /// <returns></returns>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
    public static int GetMaxDigitCountOfBitLength<TBitLength, TRadix>(this TBitLength bitLength, TRadix radix, bool accountForSignBit)
    where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
    where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var ms1b = System.Numerics.BigInteger.One << (int.CreateChecked(bitLength) - 1); // Create the most-significant-1-bit of the bit-length.

      var swar = ms1b.BitFoldRight();

      if (accountForSignBit) swar >>>= 1; // If needed, shift the swar to properly represent a negative value.

      var logc = double.Ceiling(System.Numerics.BigInteger.Log(swar, double.CreateChecked(Quantities.Radix.AssertMember(radix)))); // Integer log is way too slow for high bit-lengths.

      return System.Convert.ToInt32(logc);
    }

    /// <summary>
    /// <para>Uses the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetShortestBitLength()"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>The bit-length(<paramref name="value"/>) is equal to <c>(1 + log2-toward-zero(<paramref name="value"/>))</c> and is also equal to <c>log2-away-from-zero(<paramref name="value"/>)</c>.</para>
    /// </remarks>
    public static int GetShortestBitLength<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.
  }
}
