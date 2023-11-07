namespace Flux
{
  public static partial class Bits
  {

    /// <summary>Computes the max number of digits that can be represented by the number of <paramref name="bitCount"/> in base <paramref name="radix"/> and whether it <paramref name="isSigned"/>.</summary>
    public static int GetMaxDigitCount(int bitLength, int radix, bool isSigned)
    {
      var digits = (System.Numerics.BigInteger.One << bitLength) - System.Numerics.BigInteger.One;

      if (isSigned)
        digits >>= 1; // Shift to indicate most-significant-bit for negative values.

      return (int)digits.IntegerLog(radix) + 1;
    }
  }
}
