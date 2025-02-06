namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes the max number of digits that can be represented by the specified <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether to <paramref name="accountForSignBit"/>.</para>
    /// <code>var mdcf = (10).GetMaxDigitCount(10, false); // Yields 4, because a max value of 1023 can be represented (all bits can be used, because it is an unsigned value).</code>
    /// <code>var mdct = (10).GetMaxDigitCount(10, true); // Yields 3, because a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
    /// <code>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</code>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="bitLength">This is the number of bits to take into account.</param>
    /// <param name="radix">This is the radix (base) to use.</param>
    /// <param name="accountForSignBit">Indicates whether <paramref name="bitLength"/> use one bit for the sign.</param>
    /// <returns></returns>
    public static int MaxDigitCountOfBitLength<TBitLength, TRadix>(this TBitLength bitLength, TRadix radix, bool accountForSignBit)
    where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
    where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (TBitLength.IsNegative(bitLength))
        return 0;

      var swar = System.Numerics.BigInteger.CreateChecked(bitLength).CreateBitMaskLsb(); // Create a bit-mask representing the greatest value for the bit-length.

      if (swar.IsSingleDigit(radix)) // If SWAR is less than radix, there is only one digit, otherwise, compute for values higher than radix, and more digits.
        return 1;

      if (accountForSignBit) // If accounting for a sign-bit, shift the SWAR to properly represent the max of a signed type.
        swar >>>= 1;

      var logc = swar.FastIntegerLog(radix, UniversalRounding.WholeAwayFromZero, out var _);

      return int.CreateChecked(logc);
    }
  }
}
