namespace Flux
{
  public static partial class Bits
  {

    /// <summary>Computes the max number of digits that can be represented by the number of <paramref name="bitCount"/> in base <paramref name="radix"/> and whether it <paramref name="isSigned"/>.</summary>
    public static int MaxDigitCount(int bitCount, int radix, bool isSigned)
      => (int)(((System.Numerics.BigInteger.One << bitCount) - 1) >> (isSigned ? 1 : 0)).IntegerLog(radix) + 1;

  }
}
