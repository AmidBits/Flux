namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Reverses the bits of an integer. The LSBs (least significant bits) becomes the MSBs (most significant bits) and vice versa, i.e. the bits are mirrored across the integer storage space. It's a reversal of all bits.</para>
    /// See <see cref="ReverseBytes{TSelf}(TSelf)"/> for byte reversal.
    /// </summary>
    public static TSelf ReverseBits<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the bit reversal.
      value.WriteBigEndian(bytes); // Write as BigEndian ('left-to-right').

      for (var i = bytes.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
        bytes[i] = MirrorBits(bytes[i]); // Mirror (reverse) bits in each byte.

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

    /// <summary>Computes the reverse bit mask of a value.</summary>
    /// <remark>Using Knuth's algorithm from http://www.hackersdelight.org/revisions.pdf. Retrieved 8/19/2015.</remark>
    [System.CLSCompliant(false)]
    public static uint ReverseBits(this uint value)
    {
      uint tmp;
      value = (value << 15) | (value >> 17);
      tmp = (value ^ (value >> 10)) & 0x003f801f;
      value = (tmp + (tmp << 10)) ^ value;
      tmp = (value ^ (value >> 4)) & 0x0e038421;
      value = (tmp + (tmp << 4)) ^ value;
      tmp = (value ^ (value >> 2)) & 0x22488842;
      value = (tmp + (tmp << 2)) ^ value;

      // Traditional bit hack.
      //  value = ((value & 0xFFFF0000) >> 0x10) | ((value & 0x0000FFFF) << 0x10); // Swap 16-bit pairs.
      //  value = ((value & 0xFF00FF00) >> 0x08) | ((value & 0x00FF00FF) << 0x08); // Swap 8-bit pairs.
      //  value = ((value & 0xF0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F) << 0x04); // Swap 4-bit pairs.
      //  value = ((value & 0xCCCCCCCC) >> 0x02) | ((value & 0x33333333) << 0x02); // Swap 2-bit pairs.
      //  value = ((value & 0xAAAAAAAA) >> 0x01) | ((value & 0x55555555) << 0x01); // Swap 1-bit pairs.

      return value;
    }
    /// <summary>Computes the reverse bit mask of the value.</summary>
    [System.CLSCompliant(false)]
    public static ulong ReverseBits(this ulong value)
    {
      value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20); // Swap 32-bit pair.
      value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10); // Swap 16-bit pairs.
      value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08); // Swap 8-bit pairs.
      value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04); // Swap 4-bit pairs.
      value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02); // Swap 2-bit pairs.
      value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01); // Swap 1-bit pairs.

      return value;
    }

#endif
  }
}
