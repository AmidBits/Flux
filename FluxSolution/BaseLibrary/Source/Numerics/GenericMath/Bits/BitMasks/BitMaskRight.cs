namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Create a bit mask with <paramref name="count1Bits"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static TSelf BitMaskRight<TSelf>(this TSelf count) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (((TSelf.One << (int.CreateChecked(count) - 1)) - TSelf.One) << 1) | TSelf.One;

#else


#endif
  }
}
