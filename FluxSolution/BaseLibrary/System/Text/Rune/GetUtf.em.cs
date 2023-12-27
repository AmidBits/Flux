namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Generate a variable number (from 1 to 2) of words (16 bits as ints) representing UTF16 encoding of the <see cref="System.Text.Rune"/>.</summary>
    public static int[] GetUtf16(this System.Text.Rune source)
    {
      var value = source.Value;

      if (value >= 0x010000 && value < 0x10FFFF && (value - 0x010000) is var bits20)
        return new int[] { BitsLs(bits20), BitsHs(bits20 >> 10) };
      if ((value >= 0x0000 && value <= 0xD7FF) || (value >= 0xE000 && value <= 0xFFFF))
        return new int[] { Bits16(value) };

      throw new System.ArgumentOutOfRangeException(nameof(source));

      static ushort BitsHs(int value)
        => (ushort)((value & 0x03FF) + 0xD800);
      static ushort BitsLs(int value)
        => (ushort)((value & 0x03FF) + 0xDC00);
      static ushort Bits16(int value)
        => (ushort)(value & 0xFFFF);
    }

    /// <summary>Generate a variable number (from 1 to 4) of bytes representing UTF8 encoding of the <see cref="System.Text.Rune"/>.</summary>
    public static byte[] GetUtf8(this System.Text.Rune source)
    {
      var value = source.Value;

      if (value >= 0x10000 && value <= 0x1FFFFF)
        return new byte[] { Bits6(value), Bits6(value >> 6), Bits6(value >> 12), Bits3(value >> 18) };
      if (value >= 0x0800 && value <= 0xFFFF)
        return new byte[] { Bits6(value), Bits6(value >> 6), Bits4(value >> 12) };
      if (value >= 0x0080 && value <= 0x07FF)
        return new byte[] { Bits6(value), Bits5(value >> 6) };
      if (value >= 0x0000 && value <= 0x007F)
        return new byte[] { Bits7(value) };

      throw new System.ArgumentOutOfRangeException(nameof(source));

      static byte Bits3(int value)
        => (byte)(0b11110000 | (value & 0b00000111));
      static byte Bits4(int value)
        => (byte)(0b11100000 | (value & 0b00001111));
      static byte Bits5(int value)
        => (byte)(0b11000000 | (value & 0b00011111));
      static byte Bits6(int value)
        => (byte)(0b10000000 | (value & 0b00111111));
      static byte Bits7(int value)
        => (byte)(value & 0b01111111);
    }
  }
}
