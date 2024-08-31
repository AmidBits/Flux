namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// </summary>
    /// <remarks>See <see cref="ReverseBits{TValue}(TValue)"/> for bit reversal.</remarks>
    public static TValue ReverseBytes<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the byte reversal.

      value.WriteBigEndian(bytes); // Write as BigEndian (high-to-low).

      return TValue.ReadLittleEndian(bytes, !value.IsSignedNumber()); // Read as LittleEndian (low-to-high).
    }
  }
}
