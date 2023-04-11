namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER
    /// <summary>Create a bit mask with <paramref name="oneCount"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static TSelf CreateBitMask<TSelf>(this TSelf oneCount)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (((TSelf.One << (int.CreateChecked(oneCount) - 1)) - TSelf.One) << 1) | TSelf.One;

    /// <summary>Create a bit mask with <paramref name="oneCount"/> bits set to 1 then <paramref name="trailingZeroCount"/> bits set to 0, in that order, on the LSB (Least Significant Bit) side.</summary>
    public static TSelf CreateBitMask<TSelf>(this TSelf oneCount, TSelf trailingZeroCount)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => CreateBitMask(oneCount) << int.CreateChecked(trailingZeroCount);
#else
    /// <summary>Create a bit mask with <paramref name="oneCount"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static System.Numerics.BigInteger CreateBitMask(this int oneCount)
      => (((System.Numerics.BigInteger.One << (oneCount - 1)) - System.Numerics.BigInteger.One) << 1) | System.Numerics.BigInteger.One;

    /// <summary>Create a bit mask with <paramref name="oneCount"/> bits set to 1 then <paramref name="trailingZeroCount"/> bits set to 0, in that order, on the LSB (Least Significant Bit) side.</summary>
    public static System.Numerics.BigInteger CreateBitMask(this int oneCount, int trailingZeroCount)
      => CreateBitMask(oneCount) << trailingZeroCount;
#endif
  }
}
