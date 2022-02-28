namespace Flux
{
  public static partial class BitOps
  {
    // https://stackoverflow.com/questions/33910399/reverse-the-order-of-bits-in-a-bit-array
    // http://aggregate.org/MAGIC/
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan

    private static System.Collections.Generic.IReadOnlyList<byte>? m_byteReverseBits;
    public static System.Collections.Generic.IReadOnlyList<byte> ByteReverseBits
      => m_byteReverseBits ??= System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(System.Linq.Enumerable.Range(0, 256), n => (byte)(ReverseBits((uint)n) >> 24)));

    /// <summary>Computes the reverse bit mask of a value.</summary>
    public static System.Numerics.BigInteger ReverseBits(System.Numerics.BigInteger value)
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
    public static int ReverseBits(int value)
      => unchecked((int)ReverseBits((uint)value));
    /// <summary>Computes the reverse bit mask of a value.</summary>
    public static long ReverseBits(long value)
      => unchecked((long)ReverseBits((ulong)value));

    /// <summary>Computes the reverse bit mask of a value.</summary>
    /// <remark>Using Knuth's algorithm from http://www.hackersdelight.org/revisions.pdf. Retrieved 8/19/2015.</remark>
    [System.CLSCompliant(false)]
    public static uint ReverseBits(uint value)
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
    public static ulong ReverseBits(ulong value)
    {
      value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20); // Swap 32-bit pair.
      value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10); // Swap 16-bit pairs.
      value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08); // Swap 8-bit pairs.
      value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04); // Swap 4-bit pairs.
      value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02); // Swap 2-bit pairs.
      value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01); // Swap 1-bit pairs.

      return value;
    }
  }
}
