namespace Flux
{
  public static partial class BitOps
  {
    #region ReverseBits

    /// <summary>
    /// <para>Reverses the bits of an integer. The LSBs (least significant bits) becomes the MSBs (most significant bits) and vice versa, i.e. the bits are mirrored across the integer storage space. It's a reversal of all storage bits.</para>
    /// </summary>
    /// <remarks>See <see cref="ReverseBytes{TInteger}(TInteger)"/> for byte reversal.</remarks>
    public static TInteger ReverseBits<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>, System.Numerics.ISignedNumber<TInteger>
    {
      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the bit reversal.

      value.WriteBigEndian(bytes); // Write as BigEndian (high-to-low).

      for (var i = bytes.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
        ReverseBitsInPlace(ref bytes[i]); // Mirror (reverse) bits in each byte.

      return TInteger.ReadLittleEndian(bytes, !value.IsSignedNumber()); // Read as LittleEndian (low-to-high).
    }

    /// <summary>
    /// <para>Bit-reversal of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
    /// </summary>
    public static byte ReverseBits(this byte value)
    {
      ReverseBitsInPlace(ref value);

      return value;
    }

    /// <summary>
    /// <para>Bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ushort ReverseBits(this ushort value)
    {
      ReverseBitsInPlace(ref value);

      return value;
    }

    /// <summary>
    /// <para>Bit-reversal of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static uint ReverseBits(this uint value)
    {
      ReverseBitsInPlace(ref value);

      return value;
    }

    /// <summary>
    /// <para>Bit-reversal of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong ReverseBits(this ulong value)
    {
      ReverseBitsInPlace(ref value);

      return value;
    }

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal) of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
    /// </summary>
    public static void ReverseBitsInPlace(ref this byte value)
      => value = (byte)(((value * 0x0202020202UL) & 0x010884422010UL) % 1023);

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static void ReverseBitsInPlace(ref this ushort value)
      => value = (ushort)(((((((byte)(value & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023) << 8) | ((((((byte)((value >> 8) & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023)));

    // Alternatively:
    // {
    //  value = (ushort)(((value & 0xAAAAu) >> 0x01) | ((value & 0x5555u) << 0x01));
    //  value = (ushort)(((value & 0xCCCCu) >> 0x02) | ((value & 0x3333u) << 0x02));
    //  value = (ushort)(((value & 0xF0F0u) >> 0x04) | ((value & 0x0F0Fu) << 0x04));
    //  value = (ushort)(((value & 0xFF00u) >> 0x08) | ((value & 0x00FFu) << 0x08));
    // }

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static void ReverseBitsInPlace(ref this uint value)
    {
      value = (value << 15) | (value >> 17);
      var tmp = (value ^ (value >> 10)) & 0x003f801f;
      value = (tmp + (tmp << 10)) ^ value;
      tmp = (value ^ (value >> 4)) & 0x0e038421;
      value = (tmp + (tmp << 4)) ^ value;
      tmp = (value ^ (value >> 2)) & 0x22488842;
      value = (tmp + (tmp << 2)) ^ value;

      // Alternatively:
      // value = ((value & 0xAAAAAAAAu) >> 0x01) | ((value & 0x55555555u) << 0x01);
      // value = ((value & 0xCCCCCCCCu) >> 0x02) | ((value & 0x33333333u) << 0x02);
      // value = ((value & 0xF0F0F0F0u) >> 0x04) | ((value & 0x0F0F0F0Fu) << 0x04);
      // value = ((value & 0xFF00FF00u) >> 0x08) | ((value & 0x00FF00FFu) << 0x08);
      // value = ((value & 0xFFFF0000u) >> 0x10) | ((value & 0x0000FFFFu) << 0x10);
    }

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static void ReverseBitsInPlace(ref this ulong value)
    {
      value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01);
      value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02);
      value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04);
      value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08);
      value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10);
      value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20);
    }

    #endregion // ReverseBits
  }
}
