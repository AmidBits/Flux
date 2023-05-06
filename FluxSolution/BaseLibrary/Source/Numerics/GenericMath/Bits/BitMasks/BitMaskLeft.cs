namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Create a left (most-significant) bit mask with <paramref name="count"/> bits set to 1 on the LSB (Least Significant Bit) side.</summary>
    public static TSelf BitMaskLeft<TSelf>(this TSelf count) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => count.BitMaskRight() << (count.GetBitCount() - int.CreateChecked(count));

#else


#endif
  }
}
