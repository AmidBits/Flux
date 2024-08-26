namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes the max number of digits that can be represented by the specified source <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether it <paramref name="accountForSignBit"/>.</para>
    /// <code>var maxDigitCount = Flux.Bits.GetMaxDigitCount(10, 10, false); // Yields 4, because with 10 bits, a radix of 10 (the decimal system) and unsigned, a max value of 1023 can be represented (all bits can be used, because it is an unsigned value).</code>
    /// <code>var maxDigitCount = Flux.Bits.GetMaxDigitCount(10, 10, true); // Yields 3, because with 10 bits, a radix of 10 (the decimal system) and signed, a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <param name="bitLength">This is the number of bits to take into account.</param>
    /// <param name="radix">This is the radix (base) to use.</param>
    /// <param name="accountForSignBit">Indicates whether <paramref name="bitLength"/> use one bit for the sign.</param>
    /// <returns></returns>
    /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (source for extension method) IS THE BITLENGTH.</c></remarks>
    public static int MaxDigitsOfBitLength<TBitLength, TRadix>(this TBitLength bitLength, TRadix radix, bool accountForSignBit)
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      Quantities.Radix.AssertMember(radix);

      var ms1b = System.Numerics.BigInteger.One << (int.CreateChecked(bitLength) - 1);

      var swar = ms1b.BitFoldRight();

      if (accountForSignBit)
        swar >>>= 1; // Shift to properly represent a most-significant-bit used for negative values.

      var logc = double.Ceiling(System.Numerics.BigInteger.Log(swar, double.CreateChecked(radix))); // Integer log is way too slow for high bit-lengths.

      return System.Convert.ToInt32(logc);
    }
  }
}
