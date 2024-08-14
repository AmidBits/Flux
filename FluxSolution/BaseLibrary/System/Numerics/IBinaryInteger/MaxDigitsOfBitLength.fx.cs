namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes the max number of digits that can be represented by the specified source <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether it <paramref name="isSigned"/>.</para>
    /// <code>var maxDigitCount = Flux.Bits.GetMaxDigitCount(10, 10, false); // Yields 4, because with 10 bits, a radix of 10 (the decimal system) and unsigned, a max value of 1023 can be represented (all bits can be used, because it is an unsigned value).</code>
    /// <code>var maxDigitCount = Flux.Bits.GetMaxDigitCount(10, 10, true); // Yields 3, because with 10 bits, a radix of 10 (the decimal system) and signed, a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="bitLength">This is the number of bits to take into account.</param>
    /// <param name="radix">This is the radix (base) to use.</param>
    /// <param name="isSigned">Indicates whether <paramref name="bitLength"/> use one bit for the sign.</param>
    /// <returns></returns>
    public static int MaxDigitsOfBitLength<TSelf>(this TSelf bitLength, TSelf radix, bool isSigned)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var foldedRight = (TSelf.One << int.CreateChecked(bitLength)) - TSelf.One;

      if (isSigned)
        foldedRight >>= 1; // Shift to properly represent a most-significant-bit used for negative values.

      return int.CreateChecked(foldedRight.IntegerLogTowardZero(radix) + TSelf.One);
    }
  }
}
