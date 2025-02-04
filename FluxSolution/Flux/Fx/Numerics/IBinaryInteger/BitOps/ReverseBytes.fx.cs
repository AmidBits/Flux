namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// </summary>
    /// <remarks>See <see cref="ReverseBits{TValue}(TValue)"/> for bit reversal.</remarks>
    public static TNumber ReverseBytes<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var bytes = new byte[source.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the byte reversal.

      // We can use either direction here, write-LE/read-BE or write-BE/read-LE, doesn't really matter, since the end result is the same.

      source.WriteLittleEndian(bytes); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).
      return TNumber.ReadBigEndian(bytes, !source.IsSignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).

      //value.WriteBigEndian(bytes); // Write as BigEndian (decreasing numeric significance in increasing memory addresses).
      //return TValue.ReadLittleEndian(bytes, !value.IsSignedNumber()); // Read as LittleEndian (increasing numeric significance in increasing memory addresses).
    }

    //public static TValue ReverseBytesOwnLoop<TValue>(this TValue value)
    //  where TValue : System.Numerics.IBinaryInteger<TValue>
    //{
    //  var reversed = TValue.Zero;

    //  var bc = value.GetBitCount();

    //  var mh = TValue.CreateChecked(0xFF) << (value.GetBitCount() / 2);
    //  var ml = mh >>> 8;

    //  for (var vs = 8; vs < bc; vs += 16)
    //  {
    //    reversed |= ((value >>> vs) & ml) | ((value << vs) & mh);

    //    mh <<= 8;
    //    ml >>>= 8;
    //  }

    //  return reversed;
    //}
  }
}
