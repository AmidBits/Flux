namespace Flux
{
  /// <summary>
  /// <para>The bit struct is available to access the bits of primitive types.</para>
  /// </summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public record struct BitStruct
  {
    #region Fields

    [System.Runtime.InteropServices.FieldOffset(0)] private byte m_byte_0;
    [System.Runtime.InteropServices.FieldOffset(1)] private byte m_byte_1;
    [System.Runtime.InteropServices.FieldOffset(2)] private byte m_byte_2;
    [System.Runtime.InteropServices.FieldOffset(3)] private byte m_byte_3;
    [System.Runtime.InteropServices.FieldOffset(4)] private byte m_byte_4;
    [System.Runtime.InteropServices.FieldOffset(5)] private byte m_byte_5;
    [System.Runtime.InteropServices.FieldOffset(6)] private byte m_byte_6;
    [System.Runtime.InteropServices.FieldOffset(7)] private byte m_byte_7;
    [System.Runtime.InteropServices.FieldOffset(8)] private byte m_byte_8;
    [System.Runtime.InteropServices.FieldOffset(9)] private byte m_byte_9;
    [System.Runtime.InteropServices.FieldOffset(10)] private byte m_byte_A;
    [System.Runtime.InteropServices.FieldOffset(11)] private byte m_byte_B;
    [System.Runtime.InteropServices.FieldOffset(12)] private byte m_byte_C;
    [System.Runtime.InteropServices.FieldOffset(13)] private byte m_byte_D;
    [System.Runtime.InteropServices.FieldOffset(14)] private byte m_byte_E;
    [System.Runtime.InteropServices.FieldOffset(15)] private byte m_byte_F;

    [System.Runtime.InteropServices.FieldOffset(0)] private char m_char_0;
    [System.Runtime.InteropServices.FieldOffset(2)] private char m_char_1;
    [System.Runtime.InteropServices.FieldOffset(4)] private char m_char_2;
    [System.Runtime.InteropServices.FieldOffset(6)] private char m_char_3;
    [System.Runtime.InteropServices.FieldOffset(8)] private char m_char_4;
    [System.Runtime.InteropServices.FieldOffset(10)] private char m_char_5;
    [System.Runtime.InteropServices.FieldOffset(12)] private char m_char_6;
    [System.Runtime.InteropServices.FieldOffset(14)] private char m_char_7;

    [System.Runtime.InteropServices.FieldOffset(0)] private decimal m_decimal;

    [System.Runtime.InteropServices.FieldOffset(0)] private double m_double;

    [System.Runtime.InteropServices.FieldOffset(0)] private Half m_half_0;
    [System.Runtime.InteropServices.FieldOffset(2)] private Half m_half_1;
    [System.Runtime.InteropServices.FieldOffset(4)] private Half m_half_2;
    [System.Runtime.InteropServices.FieldOffset(6)] private Half m_half_3;
    [System.Runtime.InteropServices.FieldOffset(8)] private Half m_half_4;
    [System.Runtime.InteropServices.FieldOffset(10)] private Half m_half_5;
    [System.Runtime.InteropServices.FieldOffset(12)] private Half m_half_6;
    [System.Runtime.InteropServices.FieldOffset(14)] private Half m_half_7;

    [System.Runtime.InteropServices.FieldOffset(0)] private short m_int16_0;
    [System.Runtime.InteropServices.FieldOffset(2)] private short m_int16_1;
    [System.Runtime.InteropServices.FieldOffset(4)] private short m_int16_2;
    [System.Runtime.InteropServices.FieldOffset(6)] private short m_int16_3;
    [System.Runtime.InteropServices.FieldOffset(8)] private short m_int16_4;
    [System.Runtime.InteropServices.FieldOffset(10)] private short m_int16_5;
    [System.Runtime.InteropServices.FieldOffset(12)] private short m_int16_6;
    [System.Runtime.InteropServices.FieldOffset(14)] private short m_int16_7;

    [System.Runtime.InteropServices.FieldOffset(0)] private int m_int32_0;
    [System.Runtime.InteropServices.FieldOffset(4)] private int m_int32_1;
    [System.Runtime.InteropServices.FieldOffset(8)] private int m_int32_2;
    [System.Runtime.InteropServices.FieldOffset(12)] private int m_int32_3;

    [System.Runtime.InteropServices.FieldOffset(0)] private long m_int64_0;
    [System.Runtime.InteropServices.FieldOffset(8)] private long m_int64_1;

    [System.Runtime.InteropServices.FieldOffset(0)] private Int128 m_int128;

    [System.Runtime.InteropServices.FieldOffset(0)] private float m_single;

    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyte0;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyte1;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyte2;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyte3;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyte4;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyte5;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyte6;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyte7;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyte8;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyte9;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyteA;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyteB;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyteC;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyteD;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyteE;
    [System.Runtime.InteropServices.FieldOffset(0)] private sbyte m_sbyteF;

    [System.Runtime.InteropServices.FieldOffset(0)] private ushort m_uint16_0;
    [System.Runtime.InteropServices.FieldOffset(2)] private ushort m_uint16_1;
    [System.Runtime.InteropServices.FieldOffset(4)] private ushort m_uint16_2;
    [System.Runtime.InteropServices.FieldOffset(6)] private ushort m_uint16_3;
    [System.Runtime.InteropServices.FieldOffset(8)] private ushort m_uint16_4;
    [System.Runtime.InteropServices.FieldOffset(10)] private ushort m_uint16_5;
    [System.Runtime.InteropServices.FieldOffset(12)] private ushort m_uint16_6;
    [System.Runtime.InteropServices.FieldOffset(14)] private ushort m_uint16_7;

    [System.Runtime.InteropServices.FieldOffset(0)] private uint m_uint32_0;
    [System.Runtime.InteropServices.FieldOffset(4)] private uint m_uint32_1;
    [System.Runtime.InteropServices.FieldOffset(8)] private uint m_uint32_2;
    [System.Runtime.InteropServices.FieldOffset(12)] private uint m_uint32_3;

    [System.Runtime.InteropServices.FieldOffset(0)] private ulong m_uint64_0;
    [System.Runtime.InteropServices.FieldOffset(8)] private ulong m_uint64_1;

    [System.Runtime.InteropServices.FieldOffset(0)] private UInt128 m_uint128;

    #endregion // Fields

    #region Constructors

    public BitStruct() { } // => m_byte_s = new byte[16];
    public BitStruct(byte[] bytes, int offset, int count)
      : this()
    {
      System.ArgumentNullException.ThrowIfNull(bytes);

      if (offset < 0 || offset >= bytes.Length) throw new ArgumentNullException(nameof(offset));
      if (count < 0 || offset + count >= bytes.Length || count > 16) throw new ArgumentNullException(nameof(count));

      m_byte_0 = bytes[offset + 0];
      m_byte_1 = bytes[offset + 1];
      m_byte_2 = bytes[offset + 2];
      m_byte_3 = bytes[offset + 3];
      m_byte_4 = bytes[offset + 4];
      m_byte_5 = bytes[offset + 5];
      m_byte_6 = bytes[offset + 6];
      m_byte_7 = bytes[offset + 7];
      m_byte_8 = bytes[offset + 8];
      m_byte_9 = bytes[offset + 9];
      m_byte_A = bytes[offset + 10];
      m_byte_B = bytes[offset + 11];
      m_byte_C = bytes[offset + 12];
      m_byte_D = bytes[offset + 13];
      m_byte_E = bytes[offset + 14];
      m_byte_F = bytes[offset + 15];
    }

    public BitStruct(char value) : this() => m_char_0 = value;

    public BitStruct(decimal value) : this() => m_decimal = value;

    public BitStruct(double value) : this() => m_double = value;

    public BitStruct(Half value) : this() => m_half_0 = value;

    public BitStruct(short value) : this() => m_int16_0 = value;
    public BitStruct(int value) : this() => m_int32_0 = value;
    public BitStruct(long value) : this() => m_int64_0 = value;

    public BitStruct(Int128 value) : this() => m_int128 = value;

    public BitStruct(float value) : this() => m_single = value;

    [CLSCompliant(false)] public BitStruct(sbyte value) : this() => m_sbyte0 = value;

    [CLSCompliant(false)] public BitStruct(ushort value) : this() => m_uint16_0 = value;
    [CLSCompliant(false)] public BitStruct(uint value) : this() => m_uint32_0 = value;
    [CLSCompliant(false)] public BitStruct(ulong value) : this() => m_uint64_0 = value;

    [CLSCompliant(false)] public BitStruct(UInt128 value) : this() => m_uint128 = value;

    #endregion // Constructors

    #region Properties

    public string BinaryString => m_uint128.ToBinaryString().ToString();
    public int BinaryStringLength => BinaryString.Length;
    public string DecimalString => m_uint128.ToDecimalString().ToString();
    public int DecimalStringLength => DecimalString.Length;
    public string HexadecimalString => m_uint128.ToHexadecimalString().ToString();
    public int HexadecimalStringLength => HexadecimalString.Length;
    public string OctalString => m_uint128.ToOctalString().ToString();
    public int OctalStringLength => OctalString.Length;

    public byte Byte0 { readonly get => m_byte_0; set => m_byte_0 = value; }
    public byte Byte1 { readonly get => m_byte_1; set => m_byte_1 = value; }
    public byte Byte2 { readonly get => m_byte_2; set => m_byte_2 = value; }
    public byte Byte3 { readonly get => m_byte_3; set => m_byte_3 = value; }
    public byte Byte4 { readonly get => m_byte_4; set => m_byte_4 = value; }
    public byte Byte5 { readonly get => m_byte_5; set => m_byte_5 = value; }
    public byte Byte6 { readonly get => m_byte_6; set => m_byte_6 = value; }
    public byte Byte7 { readonly get => m_byte_7; set => m_byte_7 = value; }
    public byte Byte8 { readonly get => m_byte_8; set => m_byte_8 = value; }
    public byte Byte9 { readonly get => m_byte_9; set => m_byte_9 = value; }
    public byte ByteA { readonly get => m_byte_A; set => m_byte_A = value; }
    public byte ByteB { readonly get => m_byte_B; set => m_byte_B = value; }
    public byte ByteC { readonly get => m_byte_C; set => m_byte_C = value; }
    public byte ByteD { readonly get => m_byte_D; set => m_byte_D = value; }
    public byte ByteE { readonly get => m_byte_E; set => m_byte_E = value; }
    public byte ByteF { readonly get => m_byte_F; set => m_byte_F = value; }

    public char Char0 { readonly get => m_char_0; set => m_char_0 = value; }
    public char Char1 { readonly get => m_char_1; set => m_char_1 = value; }
    public char Char2 { readonly get => m_char_2; set => m_char_2 = value; }
    public char Char3 { readonly get => m_char_3; set => m_char_3 = value; }
    public char Char4 { readonly get => m_char_4; set => m_char_4 = value; }
    public char Char5 { readonly get => m_char_5; set => m_char_5 = value; }
    public char Char6 { readonly get => m_char_6; set => m_char_6 = value; }
    public char Char7 { readonly get => m_char_7; set => m_char_7 = value; }

    public decimal Decimal { readonly get => m_decimal; set => m_decimal = value; }

    public int[] DecimalBits { readonly get => decimal.GetBits(m_decimal); set => new decimal(value); }

    public double Double { readonly get => m_double; set => m_double = value; }

    [CLSCompliant(false)] public Int64 DoubleBits { readonly get => System.BitConverter.DoubleToInt64Bits(m_double); set => m_double = System.BitConverter.Int64BitsToDouble(value); }
    [CLSCompliant(false)] public UInt64 DoubleUBits { readonly get => System.BitConverter.DoubleToUInt64Bits(m_double); set => m_double = System.BitConverter.UInt64BitsToDouble(value); }

    public Half Half0 { readonly get => m_half_0; set => m_half_0 = value; }
    public Half Half1 { readonly get => m_half_1; set => m_half_1 = value; }
    public Half Half2 { readonly get => m_half_2; set => m_half_2 = value; }
    public Half Half3 { readonly get => m_half_3; set => m_half_3 = value; }
    public Half Half4 { readonly get => m_half_4; set => m_half_4 = value; }
    public Half Half5 { readonly get => m_half_5; set => m_half_5 = value; }
    public Half Half6 { readonly get => m_half_6; set => m_half_6 = value; }
    public Half Half7 { readonly get => m_half_7; set => m_half_7 = value; }

    public Int16 Int16_0 { readonly get => m_int16_0; set => m_int16_0 = value; }
    public Int16 Int16_1 { readonly get => m_int16_1; set => m_int16_1 = value; }
    public Int16 Int16_2 { readonly get => m_int16_2; set => m_int16_2 = value; }
    public Int16 Int16_3 { readonly get => m_int16_3; set => m_int16_3 = value; }
    public Int16 Int16_4 { readonly get => m_int16_4; set => m_int16_4 = value; }
    public Int16 Int16_5 { readonly get => m_int16_5; set => m_int16_5 = value; }
    public Int16 Int16_6 { readonly get => m_int16_6; set => m_int16_6 = value; }
    public Int16 Int16_7 { readonly get => m_int16_7; set => m_int16_7 = value; }

    public Int32 Int32_0 { readonly get => m_int32_0; set => m_int32_0 = value; }
    public Int32 Int32_1 { readonly get => m_int32_1; set => m_int32_1 = value; }
    public Int32 Int32_2 { readonly get => m_int32_2; set => m_int32_2 = value; }
    public Int32 Int32_3 { readonly get => m_int32_3; set => m_int32_3 = value; }

    public Int64 Int64_0 { readonly get => m_int64_0; set => m_int64_0 = value; }
    public Int64 Int64_1 { readonly get => m_int64_1; set => m_int64_1 = value; }

    public Int128 Int128 { readonly get => m_int128; set => m_int128 = value; }

    [CLSCompliant(false)] public sbyte SByte0 { readonly get => m_sbyte0; set => m_sbyte0 = value; }
    [CLSCompliant(false)] public sbyte SByte1 { readonly get => m_sbyte1; set => m_sbyte1 = value; }
    [CLSCompliant(false)] public sbyte SByte2 { readonly get => m_sbyte2; set => m_sbyte2 = value; }
    [CLSCompliant(false)] public sbyte SByte3 { readonly get => m_sbyte3; set => m_sbyte3 = value; }
    [CLSCompliant(false)] public sbyte SByte4 { readonly get => m_sbyte4; set => m_sbyte4 = value; }
    [CLSCompliant(false)] public sbyte SByte5 { readonly get => m_sbyte5; set => m_sbyte5 = value; }
    [CLSCompliant(false)] public sbyte SByte6 { readonly get => m_sbyte6; set => m_sbyte6 = value; }
    [CLSCompliant(false)] public sbyte SByte7 { readonly get => m_sbyte7; set => m_sbyte7 = value; }
    [CLSCompliant(false)] public sbyte SByte8 { readonly get => m_sbyte8; set => m_sbyte8 = value; }
    [CLSCompliant(false)] public sbyte SByte9 { readonly get => m_sbyte9; set => m_sbyte9 = value; }
    [CLSCompliant(false)] public sbyte SByteA { readonly get => m_sbyteA; set => m_sbyteA = value; }
    [CLSCompliant(false)] public sbyte SByteB { readonly get => m_sbyteB; set => m_sbyteB = value; }
    [CLSCompliant(false)] public sbyte SByteC { readonly get => m_sbyteC; set => m_sbyteC = value; }
    [CLSCompliant(false)] public sbyte SByteD { readonly get => m_sbyteD; set => m_sbyteD = value; }
    [CLSCompliant(false)] public sbyte SByteE { readonly get => m_sbyteE; set => m_sbyteE = value; }
    [CLSCompliant(false)] public sbyte SByteF { readonly get => m_sbyteF; set => m_sbyteF = value; }

    public float Single { readonly get => m_single; set => m_single = value; }

    [CLSCompliant(false)] public Int32 SingleBits { readonly get => System.BitConverter.SingleToInt32Bits(m_single); set => m_single = System.BitConverter.Int32BitsToSingle(value); }
    [CLSCompliant(false)] public UInt32 SingleUBits { readonly get => System.BitConverter.SingleToUInt32Bits(m_single); set => m_single = System.BitConverter.UInt32BitsToSingle(value); }

    [CLSCompliant(false)] public UInt16 UInt16_0 { readonly get => m_uint16_0; set => m_uint16_0 = value; }
    [CLSCompliant(false)] public UInt16 UInt16_1 { readonly get => m_uint16_1; set => m_uint16_1 = value; }
    [CLSCompliant(false)] public UInt16 UInt16_2 { readonly get => m_uint16_2; set => m_uint16_2 = value; }
    [CLSCompliant(false)] public UInt16 UInt16_3 { readonly get => m_uint16_3; set => m_uint16_3 = value; }
    [CLSCompliant(false)] public UInt16 UInt16_4 { readonly get => m_uint16_4; set => m_uint16_4 = value; }
    [CLSCompliant(false)] public UInt16 UInt16_5 { readonly get => m_uint16_5; set => m_uint16_5 = value; }
    [CLSCompliant(false)] public UInt16 UInt16_6 { readonly get => m_uint16_6; set => m_uint16_6 = value; }
    [CLSCompliant(false)] public UInt16 UInt16_7 { readonly get => m_uint16_7; set => m_uint16_7 = value; }

    [CLSCompliant(false)] public UInt32 UInt32_0 { readonly get => m_uint32_0; set => m_uint32_0 = value; }
    [CLSCompliant(false)] public UInt32 UInt32_1 { readonly get => m_uint32_1; set => m_uint32_1 = value; }
    [CLSCompliant(false)] public UInt32 UInt32_2 { readonly get => m_uint32_2; set => m_uint32_2 = value; }
    [CLSCompliant(false)] public UInt32 UInt32_3 { readonly get => m_uint32_3; set => m_uint32_3 = value; }

    [CLSCompliant(false)] public UInt64 UInt64_0 { readonly get => m_uint64_0; set => m_uint64_0 = value; }
    [CLSCompliant(false)] public UInt64 UInt64_1 { readonly get => m_uint64_1; set => m_uint64_1 = value; }

    [CLSCompliant(false)] public UInt128 UInt128 { readonly get => m_uint128; set => m_uint128 = value; }

    #endregion // Properties
  }
}
