namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Reverses the bits of an integer. The LSBs (least significant bits) becomes the MSBs (most significant bits) and vice versa, i.e. the bits are mirrored across the integer storage space. It's a reversal of all storage bits.</para>
    /// </summary>
    /// <remarks>See <see cref="ReverseBytes{TValue}(TValue)"/> for byte reversal.</remarks>
    public static TNumber ReverseBits<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var bytes = new byte[source.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the bit reversal.

      source.WriteBigEndian(bytes); // Write as BigEndian (high-to-low).

      for (var i = bytes.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
        ReverseBits(ref bytes[i]); // Mirror (reverse) bits in each byte.

      return TNumber.ReadLittleEndian(bytes, !source.IsSignedNumber()); // Read as LittleEndian (low-to-high).
    }

    //[System.CLSCompliant(false)]
    //public static void ReverseBits(ref this sbyte value) => value = unchecked((sbyte)BitSwap32(BitSwap16(BitSwap8(BitSwap4(BitSwap2(BitSwap1((byte)value)))))));
    //public static void ReverseBits(ref this short value) => value = unchecked((short)BitSwap32(BitSwap16(BitSwap8(BitSwap4(BitSwap2(BitSwap1((ushort)value)))))));
    //public static void ReverseBits(ref this int value) => value = unchecked((int)BitSwap32(BitSwap16(BitSwap8(BitSwap4(BitSwap2(BitSwap1((uint)value)))))));
    //public static void ReverseBits(ref this long value) => value = unchecked((long)BitSwap32(BitSwap16(BitSwap8(BitSwap4(BitSwap2(BitSwap1((ulong)value)))))));

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal) of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void ReverseBits(ref this byte source) => source = (byte)(((source * 0x0202020202UL) & 0x010884422010UL) % 1023);

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal) of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static void ReverseBits(ref this ushort source) => source = (ushort)BitSwap8(BitSwap4(BitSwap2(BitSwap1(source))));
    //{
    //  value = ((value & 0xaaaa) >> 0x01) | ((value & 0x5555) << 0x01);
    //  value = ((value & 0xcccc) >> 0x02) | ((value & 0x3333) << 0x02);
    //  value = ((value & 0xf0f0) >> 0x04) | ((value & 0x0f0f) << 0x04);
    //  value = ((value & 0xff00) >> 0x08) | ((value & 0x00ff) << 0x08);
    //}

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal) of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void ReverseBits(ref this uint source) => source = (uint)BitSwap16(BitSwap8(BitSwap4(BitSwap2(BitSwap1(source)))));
    //{
    //  //uint tmp;
    //  //value = (value << 15) | (value >> 17);
    //  //tmp = (value ^ (value >> 10)) & 0x003f801f;
    //  //value = (tmp + (tmp << 10)) ^ value;
    //  //tmp = (value ^ (value >> 4)) & 0x0e038421;
    //  //value = (tmp + (tmp << 4)) ^ value;
    //  //tmp = (value ^ (value >> 2)) & 0x22488842;
    //  //value = (tmp + (tmp << 2)) ^ value;
    //}
    //{
    //  value = ((value & 0xaaaaaaaa) >> 0x01) | ((value & 0x55555555) << 0x01);
    //  value = ((value & 0xcccccccc) >> 0x02) | ((value & 0x33333333) << 0x02);
    //  value = ((value & 0xf0f0f0f0) >> 0x04) | ((value & 0x0f0f0f0f) << 0x04);
    //  value = ((value & 0xff00ff00) >> 0x08) | ((value & 0x00ff00ff) << 0x08);
    //  value = ((value & 0xffff0000) >> 0x10) | ((value & 0x0000ffff) << 0x10);
    //}

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal) of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void ReverseBits(ref this ulong source) => source = BitSwap32(BitSwap16(BitSwap8(BitSwap4(BitSwap2(BitSwap1(source))))));
    //{
    //  value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01);
    //  value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02);
    //  value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04);
    //  value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08);
    //  value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10);
    //  value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20);
    //}
  }
}
