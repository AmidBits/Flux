//namespace Flux
//{
//  public static partial class BitOps
//  {

//    /// <summary>
//    /// <para>Computes the max number of digits that can be represented by the <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether it <paramref name="isSigned"/>.</para>
//    /// <code>var maxDigitCount = Flux.Bits.GetMaxDigitCount(10, 10, false); // Yields 4, because with 10 bits, a radix of 10 (the decimal system) and unsigned, a max value of 1023 can be represented (all bits can be used, because it is an unsigned value).</code>
//    /// <code>var maxDigitCount = Flux.Bits.GetMaxDigitCount(10, 10, true); // Yields 3, because with 10 bits, a radix of 10 (the decimal system) and signed, a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
//    /// </summary>
//    public static int GetMaxDigitCount2(int bitLength, int radix, bool isSigned)
//    {
//      var foldedRight = (System.Numerics.BigInteger.One << bitLength) - System.Numerics.BigInteger.One;

//      if (isSigned)
//        foldedRight >>= 1; // Shift to properly represent a most-significant-bit used for negative values.

//      return (int)Units.Radix.IntegerLogFloor(foldedRight, radix) + 1;
//    }
//  }
//}