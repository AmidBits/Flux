namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct BitStructure32
    : System.IEquatable<BitStructure32>
  {
    [System.Runtime.InteropServices.FieldOffset(0)]
    private byte m_byte0;
    [System.Runtime.InteropServices.FieldOffset(1)]
    private byte m_byte1;
    [System.Runtime.InteropServices.FieldOffset(2)]
    private byte m_byte2;
    [System.Runtime.InteropServices.FieldOffset(3)]
    private byte m_byte3;
    [System.Runtime.InteropServices.FieldOffset(0)]
    private int m_int32;
    [System.Runtime.InteropServices.FieldOffset(0)]
    private uint m_uint32;
    [System.Runtime.InteropServices.FieldOffset(0)]
    private float m_single;

    public BitStructure32(byte[] bytes, int offset = 0)
      : this()
    {
      if (bytes is null) throw new System.ArgumentNullException(nameof(bytes));

      m_byte0 = bytes[offset + 0];
      m_byte1 = bytes[offset + 1];
      m_byte2 = bytes[offset + 2];
      m_byte3 = bytes[offset + 3];
    }
    public BitStructure32(int value)
      : this()
      => m_int32 = value;
    [System.CLSCompliant(false)]
    public BitStructure32(uint value)
      : this()
      => m_uint32 = value;
    public BitStructure32(float value)
      : this()
      => m_single = value;

    public byte Byte0
    { get => m_byte0; set => m_byte0 = value; }
    public byte Byte1
    { get => m_byte1; set => m_byte1 = value; }
    public byte Byte2
    { get => m_byte2; set => m_byte2 = value; }
    public byte Byte3
    { get => m_byte3; set => m_byte3 = value; }
    public int Int32
    { get => m_int32; set => m_int32 = value; }
    [System.CLSCompliant(false)]
    public uint UInt32
    { get => m_uint32; set => m_uint32 = value; }
    public float Single
    { get => m_single; set => m_single = value; }

    // Operators
    public static bool operator ==(BitStructure32 a, BitStructure32 b)
      => a.Equals(b);
    public static bool operator !=(BitStructure32 a, BitStructure32 b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(BitStructure32 other)
      => m_int32 == other.m_int32;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is BitStructure32 o && Equals(o);
    public override int GetHashCode()
      => m_int32.GetHashCode();
    public override string? ToString()
      => $"{GetType().Name} {{ [{m_byte0:X2} {m_byte1:X2} {m_byte2:X2} {m_byte3:X2}], {m_int32}, {m_single} }}";
  }
}
