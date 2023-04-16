namespace Flux
{
#if NET7_0_OR_GREATER

  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public record struct BitStruct
  {
    #region Fields

    [System.Runtime.InteropServices.FieldOffset(0)] private byte m_byte0;
    [System.Runtime.InteropServices.FieldOffset(1)] private byte m_byte1;
    [System.Runtime.InteropServices.FieldOffset(2)] private byte m_byte2;
    [System.Runtime.InteropServices.FieldOffset(3)] private byte m_byte3;
    [System.Runtime.InteropServices.FieldOffset(4)] private byte m_byte4;
    [System.Runtime.InteropServices.FieldOffset(5)] private byte m_byte5;
    [System.Runtime.InteropServices.FieldOffset(6)] private byte m_byte6;
    [System.Runtime.InteropServices.FieldOffset(7)] private byte m_byte7;
    [System.Runtime.InteropServices.FieldOffset(8)] private byte m_byte8;
    [System.Runtime.InteropServices.FieldOffset(9)] private byte m_byte9;
    [System.Runtime.InteropServices.FieldOffset(10)] private byte m_byteA;
    [System.Runtime.InteropServices.FieldOffset(11)] private byte m_byteB;
    [System.Runtime.InteropServices.FieldOffset(12)] private byte m_byteC;
    [System.Runtime.InteropServices.FieldOffset(13)] private byte m_byteD;
    [System.Runtime.InteropServices.FieldOffset(14)] private byte m_byteE;
    [System.Runtime.InteropServices.FieldOffset(15)] private byte m_byteF;

    [System.Runtime.InteropServices.FieldOffset(0)] private decimal m_decimal;

    [System.Runtime.InteropServices.FieldOffset(0)] private double m_double;

    [System.Runtime.InteropServices.FieldOffset(0)] private int m_int32;
    [System.Runtime.InteropServices.FieldOffset(0)] private long m_int64;
    [System.Runtime.InteropServices.FieldOffset(0)] private Int128 m_int128;

    [System.Runtime.InteropServices.FieldOffset(0)] private float m_single;

    [System.Runtime.InteropServices.FieldOffset(0)] private uint m_uint32;
    [System.Runtime.InteropServices.FieldOffset(0)] private ulong m_uint64;
    [System.Runtime.InteropServices.FieldOffset(0)] private UInt128 m_uint128;

    #endregion // Fields

    #region Constructors

    public BitStruct(byte[] bytes, int offset, int count)
    {
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));
      if (offset < 0 || offset >= bytes.Length) throw new ArgumentNullException(nameof(offset));
      if (count < 0 || offset + count >= bytes.Length || count > 16) throw new ArgumentNullException(nameof(count));

      m_byte0 = bytes[offset + 0];
      m_byte1 = bytes[offset + 1];
      m_byte2 = bytes[offset + 2];
      m_byte3 = bytes[offset + 3];
      m_byte4 = bytes[offset + 4];
      m_byte5 = bytes[offset + 5];
      m_byte6 = bytes[offset + 6];
      m_byte7 = bytes[offset + 7];
      m_byte8 = bytes[offset + 8];
      m_byte9 = bytes[offset + 9];
      m_byteA = bytes[offset + 10];
      m_byteB = bytes[offset + 11];
      m_byteC = bytes[offset + 12];
      m_byteD = bytes[offset + 13];
      m_byteE = bytes[offset + 14];
      m_byteF = bytes[offset + 15];
    }

    public BitStruct(decimal value) => m_decimal = value;

    public BitStruct(double value) => m_double = value;

    public BitStruct(int value) => m_int32 = value;
    public BitStruct(long value) => m_int64 = value;
    public BitStruct(Int128 value) => m_int128 = value;

    public BitStruct(float value) => m_single = value;

    [CLSCompliant(false)] public BitStruct(uint value) => m_uint32 = value;
    [CLSCompliant(false)] public BitStruct(ulong value) => m_uint64 = value;
    [CLSCompliant(false)] public BitStruct(UInt128 value) => m_uint128 = value;

    #endregion // Constructors

    #region Properties

    public byte Byte0 { get => m_byte0; set => m_byte0 = value; }
    public byte Byte1 { get => m_byte1; set => m_byte1 = value; }
    public byte Byte2 { get => m_byte2; set => m_byte2 = value; }
    public byte Byte3 { get => m_byte3; set => m_byte3 = value; }
    public byte Byte4 { get => m_byte4; set => m_byte4 = value; }
    public byte Byte5 { get => m_byte5; set => m_byte5 = value; }
    public byte Byte6 { get => m_byte6; set => m_byte6 = value; }
    public byte Byte7 { get => m_byte7; set => m_byte7 = value; }
    public byte Byte8 { get => m_byte8; set => m_byte8 = value; }
    public byte Byte9 { get => m_byte9; set => m_byte9 = value; }
    public byte ByteA { get => m_byteA; set => m_byteA = value; }
    public byte ByteB { get => m_byteB; set => m_byteB = value; }
    public byte ByteC { get => m_byteC; set => m_byteC = value; }
    public byte ByteD { get => m_byteD; set => m_byteD = value; }
    public byte ByteE { get => m_byteE; set => m_byteE = value; }
    public byte ByteF { get => m_byteF; set => m_byteF = value; }

    public decimal Decimal { get => m_decimal; set => m_decimal = value; }

    public double Double { get => m_double; set => m_double = value; }

    public int Int32 { get => m_int32; set => m_int32 = value; }
    public long Int64 { get => m_int64; set => m_int64 = value; }
    public Int128 Int128 { get => m_int128; set => m_int128 = value; }

    public float Single { get => m_single; set => m_single = value; }

    [CLSCompliant(false)] public uint UInt32 { get => m_uint32; set => m_uint32 = value; }
    [CLSCompliant(false)] public ulong UInt64 { get => m_uint64; set => m_uint64 = value; }
    [CLSCompliant(false)] public UInt128 UInt128 { get => m_uint128; set => m_uint128 = value; }

    #endregion // Properties
  }

#endif
}
