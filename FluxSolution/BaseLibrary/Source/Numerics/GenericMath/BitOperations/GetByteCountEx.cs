namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// See <see cref="ReverseBits{TSelf}(TSelf)"/> for bit reversal.
    /// </summary>
    public static int GetByteCountEx<TSelf>(this TSelf value, out int actualByteCount, out int bitCount)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      bitCount = value.GetShortestBitLength();

      var (quotient, remainder) = int.DivRem(bitCount, 8);

      actualByteCount = quotient + (remainder > 0 ? 1 : 0);

      return value.GetByteCount();
    }
  }
}
