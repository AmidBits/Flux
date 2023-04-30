namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Create a bit mask with <paramref name="count1Bits"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static TSelf BitMask<TSelf>(this TSelf count1Bits)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (((TSelf.One << (int.CreateChecked(count1Bits) - 1)) - TSelf.One) << 1) | TSelf.One;

    /// <summary>Create a bit mask with <paramref name="count1Bits"/> bits set to 1 then <paramref name="shiftTowardMostSignificant"/> bits set to 0, in that order, on the LSB (Least Significant Bit) side.</summary>
    public static TSelf BitMask<TSelf>(this TSelf count1Bits, TSelf shiftTowardMostSignificant)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => BitMask(count1Bits) << int.CreateChecked(shiftTowardMostSignificant);

#else

    /// <summary>Create a bit mask with <paramref name="oneCount"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static System.Numerics.BigInteger BitMask(this int count1Bits)
      => (((System.Numerics.BigInteger.One << (count1Bits - 1)) - System.Numerics.BigInteger.One) << 1) | System.Numerics.BigInteger.One;

    /// <summary>Create a bit mask with <paramref name="count1Bits"/> bits set to 1 then <paramref name="shiftTowardMostSignificant"/> bits set to 0, in that order, on the LSB (Least Significant Bit) side.</summary>
    public static System.Numerics.BigInteger BitMask(this int count1Bits, int shiftTowardMostSignificant)
      => BitMask(count1Bits) << shiftTowardMostSignificant;

#endif
  }
}
