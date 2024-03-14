namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// </summary>
    /// <remarks>See <see cref="ReverseBits{TSelf}(TSelf)"/> for bit reversal.</remarks>
    public static TSelf ReverseBytes<TSelf>(this TSelf integer)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var bytes = new byte[integer.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the byte reversal.

      integer.WriteBigEndian(bytes); // Write as BigEndian (high-to-low).

      return TSelf.ReadLittleEndian(bytes, !integer.IsSignedNumber()); // Read as LittleEndian (low-to-high).
    }
  }
}
