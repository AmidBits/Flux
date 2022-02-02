namespace Flux
{
  public static partial class BitOps
  {
    // https://stackoverflow.com/questions/33910399/reverse-the-order-of-bits-in-a-bit-array
    // http://aggregate.org/MAGIC/
    // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan

    private static System.Collections.Generic.IReadOnlyList<byte>? m_byteReverseBits;
    /// <summary></summary>
    /// <example>System.Console.WriteLine(string.Join(@",", System.Linq.Enumerable.Range(0, 256).Select(n => @"0b" + ((int)((uint)Flux.Math.ReverseBits(n) >> 24)).ToRadixString(2).PadLeft(8, '0'))));</example>
    //public static System.Collections.Generic.IReadOnlyList<int> ByteReverseBits = new System.Collections.Generic.List<int> { 0b00000000, 0b10000000, 0b01000000, 0b11000000, 0b00100000, 0b10100000, 0b01100000, 0b11100000, 0b00010000, 0b10010000, 0b01010000, 0b11010000, 0b00110000, 0b10110000, 0b01110000, 0b11110000, 0b00001000, 0b10001000, 0b01001000, 0b11001000, 0b00101000, 0b10101000, 0b01101000, 0b11101000, 0b00011000, 0b10011000, 0b01011000, 0b11011000, 0b00111000, 0b10111000, 0b01111000, 0b11111000, 0b00000100, 0b10000100, 0b01000100, 0b11000100, 0b00100100, 0b10100100, 0b01100100, 0b11100100, 0b00010100, 0b10010100, 0b01010100, 0b11010100, 0b00110100, 0b10110100, 0b01110100, 0b11110100, 0b00001100, 0b10001100, 0b01001100, 0b11001100, 0b00101100, 0b10101100, 0b01101100, 0b11101100, 0b00011100, 0b10011100, 0b01011100, 0b11011100, 0b00111100, 0b10111100, 0b01111100, 0b11111100, 0b00000010, 0b10000010, 0b01000010, 0b11000010, 0b00100010, 0b10100010, 0b01100010, 0b11100010, 0b00010010, 0b10010010, 0b01010010, 0b11010010, 0b00110010, 0b10110010, 0b01110010, 0b11110010, 0b00001010, 0b10001010, 0b01001010, 0b11001010, 0b00101010, 0b10101010, 0b01101010, 0b11101010, 0b00011010, 0b10011010, 0b01011010, 0b11011010, 0b00111010, 0b10111010, 0b01111010, 0b11111010, 0b00000110, 0b10000110, 0b01000110, 0b11000110, 0b00100110, 0b10100110, 0b01100110, 0b11100110, 0b00010110, 0b10010110, 0b01010110, 0b11010110, 0b00110110, 0b10110110, 0b01110110, 0b11110110, 0b00001110, 0b10001110, 0b01001110, 0b11001110, 0b00101110, 0b10101110, 0b01101110, 0b11101110, 0b00011110, 0b10011110, 0b01011110, 0b11011110, 0b00111110, 0b10111110, 0b01111110, 0b11111110, 0b00000001, 0b10000001, 0b01000001, 0b11000001, 0b00100001, 0b10100001, 0b01100001, 0b11100001, 0b00010001, 0b10010001, 0b01010001, 0b11010001, 0b00110001, 0b10110001, 0b01110001, 0b11110001, 0b00001001, 0b10001001, 0b01001001, 0b11001001, 0b00101001, 0b10101001, 0b01101001, 0b11101001, 0b00011001, 0b10011001, 0b01011001, 0b11011001, 0b00111001, 0b10111001, 0b01111001, 0b11111001, 0b00000101, 0b10000101, 0b01000101, 0b11000101, 0b00100101, 0b10100101, 0b01100101, 0b11100101, 0b00010101, 0b10010101, 0b01010101, 0b11010101, 0b00110101, 0b10110101, 0b01110101, 0b11110101, 0b00001101, 0b10001101, 0b01001101, 0b11001101, 0b00101101, 0b10101101, 0b01101101, 0b11101101, 0b00011101, 0b10011101, 0b01011101, 0b11011101, 0b00111101, 0b10111101, 0b01111101, 0b11111101, 0b00000011, 0b10000011, 0b01000011, 0b11000011, 0b00100011, 0b10100011, 0b01100011, 0b11100011, 0b00010011, 0b10010011, 0b01010011, 0b11010011, 0b00110011, 0b10110011, 0b01110011, 0b11110011, 0b00001011, 0b10001011, 0b01001011, 0b11001011, 0b00101011, 0b10101011, 0b01101011, 0b11101011, 0b00011011, 0b10011011, 0b01011011, 0b11011011, 0b00111011, 0b10111011, 0b01111011, 0b11111011, 0b00000111, 0b10000111, 0b01000111, 0b11000111, 0b00100111, 0b10100111, 0b01100111, 0b11100111, 0b00010111, 0b10010111, 0b01010111, 0b11010111, 0b00110111, 0b10110111, 0b01110111, 0b11110111, 0b00001111, 0b10001111, 0b01001111, 0b11001111, 0b00101111, 0b10101111, 0b01101111, 0b11101111, 0b00011111, 0b10011111, 0b01011111, 0b11011111, 0b00111111, 0b10111111, 0b01111111, 0b11111111 };
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
        targetBytes[targetIndex++] = (byte)ByteReverseBits[sourceArray[sourceIndex--]];

      return new System.Numerics.BigInteger(targetBytes);
    }

    /// <summary>Computes the reverse bit mask of a value.</summary>
    public static int ReverseBits(int value)
      => unchecked((int)ReverseBits((uint)value));
    /// <summary>Computes the reverse bit mask of a value.</summary>
    public static long ReverseBits(long value)
      => unchecked((long)ReverseBits((ulong)value));

    /// <summary>Knuth's algorithm from http://www.hackersdelight.org/revisions.pdf. Retrieved 8/19/2015.</summary>
    [System.CLSCompliant(false)]
    public static void ReverseBits(ref uint value)
    {
      uint tmp;
      value = (value << 15) | (value >> 17);
      tmp = (value ^ (value >> 10)) & 0x003f801f;
      value = (tmp + (tmp << 10)) ^ value;
      tmp = (value ^ (value >> 4)) & 0x0e038421;
      value = (tmp + (tmp << 4)) ^ value;
      tmp = (value ^ (value >> 2)) & 0x22488842;
      value = (tmp + (tmp << 2)) ^ value;
    }
    /// <summary>Computes the reverse bit mask of a value.</summary>
    [System.CLSCompliant(false)]
    public static uint ReverseBits(uint value)
    {
      ReverseBits(ref value);
      return value;
    }
    //{
    //  value = ((value & 0xFFFF0000) >> 0x10) | ((value & 0x0000FFFF) << 0x10);
    //  value = ((value & 0xFF00FF00) >> 0x08) | ((value & 0x00FF00FF) << 0x08);
    //  value = ((value & 0xF0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F) << 0x04);
    //  value = ((value & 0xCCCCCCCC) >> 0x02) | ((value & 0x33333333) << 0x02);
    //  value = ((value & 0xAAAAAAAA) >> 0x01) | ((value & 0x55555555) << 0x01);
    //  return value;
    //}
    /// <summary>Computes the reverse bit mask of the value.</summary>
    [System.CLSCompliant(false)]
    public static void ReverseBits(ref ulong value)
    {
      value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20);
      value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10);
      value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08);
      value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04);
      value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02);
      value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01);
    }
    [System.CLSCompliant(false)]
    public static ulong ReverseBits(ulong value)
    {
      ReverseBits(ref value);
      return value;
    }
  }
}
