namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Reverses the bits of an integer. The LSBs (least significant bits) becomes the MSBs (most significant bits) and vice versa, i.e. the bits are mirrored across the integer storage space. It's a reversal of all bits.</para>
    /// See <see cref="ReverseBytes{TSelf}(TSelf)"/> for byte reversal.
    /// </summary>
    /// <remarks>This method only works on built-in/primitive integer types. <see cref="System.Numerics.BigInteger"/> is an integer type, but is not built-in/primitive.</remarks>
    public static TSelf ReverseBits<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (value.GetType() is var type && !type.IsPrimitive) throw new System.NotSupportedException(type.FullName);

      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the bit reversal.

      value.WriteBigEndian(bytes); // Write as BigEndian ('left-to-right').

      for (var i = bytes.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
        ReverseBits(ref bytes[i]); // Mirror (reverse) bits in each byte.

      return TSelf.ReadLittleEndian(bytes, typeof(System.Numerics.IUnsignedNumber<>).IsSupertypeOf(value.GetType())); // Read as LittleEndian ('right-to-left').
    }

#else
    // https://stackoverflow.com/questions/33910399/reverse-the-order-of-bits-in-a-bit-array
    // http://aggregate.org/MAGIC/
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan

    private static System.Collections.Generic.IReadOnlyList<byte>? m_byteReverseBits;
    public static System.Collections.Generic.IReadOnlyList<byte> ByteReverseBits
      => m_byteReverseBits ??= System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(System.Linq.Enumerable.Range(0, 256), n => (byte)(ReverseBits((uint)n) >> 24)));

    /// <summary>Computes the reverse bit mask of a value.</summary>
    public static System.Numerics.BigInteger ReverseBits(this System.Numerics.BigInteger value)
    {
      if (value >= 0 && value <= 255)
        return ByteReverseBits[(int)value];

      var sourceArray = value.ToByteArrayEx(out var sourceIndex, out var _);

      var targetBytes = new byte[sourceIndex + 1];
      var targetIndex = 0;

      while (sourceIndex >= 0)
        targetBytes[targetIndex++] = ByteReverseBits[sourceArray[sourceIndex--]];

      return new System.Numerics.BigInteger(targetBytes);
    }

    /// <summary>Computes the reverse bit mask of a value.</summary>
    public static int ReverseBits(this int value)
      => unchecked((int)ReverseBits((uint)value));
    /// <summary>Computes the reverse bit mask of a value.</summary>
    public static long ReverseBits(this long value)
      => unchecked((long)ReverseBits((ulong)value));

#endif


    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</summary>
    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void ReverseBits(ref this byte value) => value = (byte)((value * 0x0202020202UL & 0x010884422010UL) % 1023);

    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</summary>
    [System.CLSCompliant(false)]
    public static void ReverseBits(ref this ushort value) => value = (ushort)BitSwap8(BitSwap4(BitSwap2(BitSwap1(value))));
    //{
    //  value = ((value & 0xaaaa) >> 0x01) | ((value & 0x5555) << 0x01);
    //  value = ((value & 0xcccc) >> 0x02) | ((value & 0x3333) << 0x02);
    //  value = ((value & 0xf0f0) >> 0x04) | ((value & 0x0f0f) << 0x04);
    //  value = ((value & 0xff00) >> 0x08) | ((value & 0x00ff) << 0x08);
    //}

    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void ReverseBits(ref this uint value) => value = (uint)BitSwap16(BitSwap8(BitSwap4(BitSwap2(BitSwap1(value)))));
    //{
    //  //uint tmp;
    //  //value = (value << 15) | (value >> 17);
    //  //tmp = (value ^ (value >> 10)) & 0x003f801f;
    //  //value = (tmp + (tmp << 10)) ^ value;
    //  //tmp = (value ^ (value >> 4)) & 0x0e038421;
    //  //value = (tmp + (tmp << 4)) ^ value;
    //  //tmp = (value ^ (value >> 2)) & 0x22488842;
    //  //value = (tmp + (tmp << 2)) ^ value;

    //  value = ((value & 0xaaaaaaaa) >> 0x01) | ((value & 0x55555555) << 0x01);
    //  value = ((value & 0xcccccccc) >> 0x02) | ((value & 0x33333333) << 0x02);
    //  value = ((value & 0xf0f0f0f0) >> 0x04) | ((value & 0x0f0f0f0f) << 0x04);
    //  value = ((value & 0xff00ff00) >> 0x08) | ((value & 0x00ff00ff) << 0x08);
    //  value = ((value & 0xffff0000) >> 0x10) | ((value & 0x0000ffff) << 0x10);
    //}

    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void ReverseBits(ref this ulong value) => value = BitSwap32(BitSwap16(BitSwap8(BitSwap4(BitSwap2(BitSwap1(value))))));
    //{
    //  value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01);
    //  value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02);
    //  value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04);
    //  value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08);
    //  value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10);
    //  value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20);
    //}

    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap1(ulong value) => ((value & 0xaaaaaaaaaaaaaaaaUL) >> 0x01) | ((value & 0x5555555555555555UL) << 0x01);

    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap2(ulong value) => ((value & 0xccccccccccccccccUL) >> 0x02) | ((value & 0x3333333333333333UL) << 0x02);

    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap4(ulong value) => ((value & 0xf0f0f0f0f0f0f0f0UL) >> 0x04) | ((value & 0x0f0f0f0f0f0f0f0fUL) << 0x04);

    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap8(ulong value) => ((value & 0xff00ff00ff00ff00UL) >> 0x08) | ((value & 0x00ff00ff00ff00ffUL) << 0x08);

    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap16(ulong value) => ((value & 0xffff0000ffff0000UL) >> 0x10) | ((value & 0x0000ffff0000ffffUL) << 0x10);

    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap32(ulong value) => (value >> 0x20) | (value << 0x20);
  }
}
