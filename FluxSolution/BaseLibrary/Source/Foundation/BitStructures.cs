namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct BitStructure32
    : System.IEquatable<BitStructure32>
  {
    [System.Runtime.InteropServices.FieldOffset(0)] private readonly byte m_byte0;
    [System.Runtime.InteropServices.FieldOffset(1)] private readonly byte m_byte1;
    [System.Runtime.InteropServices.FieldOffset(2)] private readonly byte m_byte2;
    [System.Runtime.InteropServices.FieldOffset(3)] private readonly byte m_byte3;
    [System.Runtime.InteropServices.FieldOffset(0)] private readonly int m_int32;
    [System.Runtime.InteropServices.FieldOffset(0)] private readonly float m_single;

    public byte Byte0 => m_byte0;
    public byte Byte1 => m_byte1;
    public byte Byte2 => m_byte2;
    public byte Byte3 => m_byte3;
    public float FloatingPoint32 => m_single;
    public int Integer32 => m_int32;

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
    public BitStructure32(float value)
      : this()
      => m_single = value;

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
      => $"<[{m_byte0:X2} {m_byte1:X2} {m_byte2:X2} {m_byte3:X2}], {m_int32}, {m_single}";
  }

  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct BitStructure64
    : System.IEquatable<BitStructure64>
  {
    [System.Runtime.InteropServices.FieldOffset(0)] private readonly byte m_byte0;
    [System.Runtime.InteropServices.FieldOffset(1)] private readonly byte m_byte1;
    [System.Runtime.InteropServices.FieldOffset(2)] private readonly byte m_byte2;
    [System.Runtime.InteropServices.FieldOffset(3)] private readonly byte m_byte3;
    [System.Runtime.InteropServices.FieldOffset(4)] private readonly byte m_byte4;
    [System.Runtime.InteropServices.FieldOffset(5)] private readonly byte m_byte5;
    [System.Runtime.InteropServices.FieldOffset(6)] private readonly byte m_byte6;
    [System.Runtime.InteropServices.FieldOffset(7)] private readonly byte m_byte7;
    [System.Runtime.InteropServices.FieldOffset(0)] private readonly double m_double;
    [System.Runtime.InteropServices.FieldOffset(0)] private readonly long m_int64;

    public byte Byte0 => m_byte0;
    public byte Byte1 => m_byte1;
    public byte Byte2 => m_byte2;
    public byte Byte3 => m_byte3;
    public byte Byte4 => m_byte4;
    public byte Byte5 => m_byte5;
    public byte Byte6 => m_byte6;
    public byte Byte7 => m_byte7;
    public double FloatingPoint64 => m_double;
    public long Integer64 => m_int64;

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
    public BitStructure64(long value)
      : this()
      => m_int64 = value;
    public BitStructure64(double value)
      : this()
      => m_double = value;

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
      => $"<[{m_byte0:X2} {m_byte1:X2} {m_byte2:X2} {m_byte3:X2} {m_byte4:X2} {m_byte5:X2} {m_byte6:X2} {m_byte7:X2}], {m_int64}, {m_double}";
  }
}
