namespace Flux
{
  public static partial class Convert
  {

    /// <summary>Computes the max number of digits that can be represented by the number of <paramref name="bitCount"/> in base <paramref name="radix"/>.</summary>
    public static int BitCountToMaxDigitCount(int bitCount, int radix, bool isSigned)
      => (int)(((System.Numerics.BigInteger.One << bitCount) - 1) >> (isSigned ? 1 : 0)).IntegerLog(radix) + 1;

  }
}
