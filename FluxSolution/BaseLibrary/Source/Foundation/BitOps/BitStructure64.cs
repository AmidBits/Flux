namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct BitStructure64
    : System.IEquatable<BitStructure64>
  {
    [System.Runtime.InteropServices.FieldOffset(0)]
    private byte m_byte0;
    [System.Runtime.InteropServices.FieldOffset(1)]
    private byte m_byte1;
    [System.Runtime.InteropServices.FieldOffset(2)]
    private byte m_byte2;
    [System.Runtime.InteropServices.FieldOffset(3)]
    private byte m_byte3;
    [System.Runtime.InteropServices.FieldOffset(4)]
    private byte m_byte4;
    [System.Runtime.InteropServices.FieldOffset(5)]
    private byte m_byte5;
    [System.Runtime.InteropServices.FieldOffset(6)]
    private byte m_byte6;
    [System.Runtime.InteropServices.FieldOffset(7)]
    private byte m_byte7;
    [System.Runtime.InteropServices.FieldOffset(0)]
    private double m_double;
    [System.Runtime.InteropServices.FieldOffset(0)]
    private long m_int64;
    [System.Runtime.InteropServices.FieldOffset(0)]
    private ulong m_uint64;

    public BitStructure64(byte[] bytes, int offset = 0)
      : this()
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      m_byte0 = bytes[offset + 0];
      m_byte1 = bytes[offset + 1];
      m_byte2 = bytes[offset + 2];
      m_byte3 = bytes[offset + 3];
      m_byte4 = bytes[offset + 4];
      m_byte5 = bytes[offset + 5];
      m_byte6 = bytes[offset + 6];
      m_byte7 = bytes[offset + 7];
    }
    public BitStructure64(double value)
      : this()
      => m_double = value;
    public BitStructure64(long value)
      : this()
      => m_int64 = value;
    [System.CLSCompliant(false)]
    public BitStructure64(ulong value)
      : this()
      => m_uint64 = value;

    public byte Byte0
    { get => m_byte0; set => m_byte0 = value; }
    public byte Byte1
    { get => m_byte1; set => m_byte1 = value; }
    public byte Byte2
    { get => m_byte2; set => m_byte2 = value; }
    public byte Byte3
    { get => m_byte3; set => m_byte3 = value; }
    public byte Byte4
    { get => m_byte4; set => m_byte4 = value; }
    public byte Byte5
    { get => m_byte5; set => m_byte5 = value; }
    public byte Byte6
    { get => m_byte6; set => m_byte6 = value; }
    public byte Byte7
    { get => m_byte7; set => m_byte7 = value; }
    public double Double
    { get => m_double; set => m_double = value; }
    public long Int64
    { get => m_int64; set => m_int64 = value; }
    [System.CLSCompliant(false)]
    public ulong UInt64
    { get => m_uint64; set => m_uint64 = value; }

    // Operators
    public static bool operator ==(BitStructure64 a, BitStructure64 b)
      => a.Equals(b);
    public static bool operator !=(BitStructure64 a, BitStructure64 b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(BitStructure64 other)
      => m_int64 == other.m_int64;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is BitStructure64 o && Equals(o);
    public override int GetHashCode()
      => m_int64.GetHashCode();
    public override string? ToString()
      => $"{GetType().Name} {{ [{m_byte0:X2} {m_byte1:X2} {m_byte2:X2} {m_byte3:X2} {m_byte4:X2} {m_byte5:X2} {m_byte6:X2} {m_byte7:X2}], {m_int64}, {m_double} }}";
  }
}
