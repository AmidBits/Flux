namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes the max number of digits that can be represented by the specified <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether to <paramref name="accountForSignBit"/>.</para>
    /// <code>* * * NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER-OF-BITS (to account for). * * *</code>
    /// <code>var mdcf = (10).GetMaxDigitCount(10, false); // Yields 4, because a max value of 1023 can be represented (all bits can be used, because it is an unsigned value).</code>
    /// <code>var mdct = (10).GetMaxDigitCount(10, true); // Yields 3, because a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="bitLength">This is the number of bits to take into account.</param>
    /// <param name="radix">This is the radix (base) to use.</param>
    /// <param name="accountForSignBit">Indicates whether <paramref name="bitLength"/> use one bit for the sign.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>Uses <see cref="FastIntegerLog{TNumber, TNewBase}(TNumber, TNewBase, UniversalRounding, out double)"/>.</para>
    /// </remarks>
    public static int MaxDigitCountOfBitLength<TBitLength, TRadix>(this TBitLength bitLength, TRadix radix, bool accountForSignBit)
    where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
    where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (TBitLength.IsNegative(bitLength))
        return 0;
      else if (bitLength.CreateBitMaskLsb() < TBitLength.CreateChecked(radix))
        return 1;
      else // Need to compute for values higher than radix.
      {
        var swar = System.Numerics.BigInteger.CreateChecked(bitLength).CreateBitMaskLsb();
        // If needed, shift the SWAR to properly represent the max of a signed type.
        var logc = (accountForSignBit ? swar >>> 1 : swar).FastIntegerLog(radix, UniversalRounding.WholeAwayFromZero, out var _);

        return int.CreateChecked(logc);
      }
    }
  }
}
