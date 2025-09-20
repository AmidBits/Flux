namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// </summary>
    /// <remarks>See <see cref="ReverseBits{TInteger}(TInteger)"/> for bit reversal.</remarks>
    public static TInteger ReverseBytes<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the byte reversal.

      // We can use either direction here, write-LE/read-BE or write-BE/read-LE, doesn't really matter, since the end result is the same.

      value.WriteLittleEndian(bytes); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).
      return TInteger.ReadBigEndian(bytes, value.IsUnsignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).

      //value.WriteBigEndian(bytes); // Write as BigEndian (decreasing numeric significance in increasing memory addresses).
      //return TValue.ReadLittleEndian(bytes, value.IsUnsignedNumber()); // Read as LittleEndian (increasing numeric significance in increasing memory addresses).
    }
  }
}
