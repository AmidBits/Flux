namespace Flux
{
  public static partial class Fx
  {
    [System.CLSCompliant(false)]
    public static ulong BytesToValue(this byte[] buffer, int offset, int count, Endianess endianess)
    {
      System.ArgumentNullException.ThrowIfNull(buffer);

      if (offset < 0 || offset >= buffer.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || offset + count >= buffer.Length || count > 8) throw new System.ArgumentOutOfRangeException(nameof(count));

      var value = 0UL;

      switch (endianess)
      {
        case Endianess.BigEndian:
          for (var i = 0; i < count; i++)
            value = (value << 8) | buffer[offset + i];
          break;
        case Endianess.LittleEndian:
          for (var i = count - 1; i >= 0; i--)
            value = (value << 8) | buffer[offset + i];
          break;
      };

      return value;
    }

    [System.CLSCompliant(false)]
    public static void ValueToBytes(this ulong value, byte[] buffer, int offset, int count, Endianess endianess)
    {
      System.ArgumentNullException.ThrowIfNull(buffer);

      if (offset < 0 || offset >= buffer.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || offset + count >= buffer.Length || count > 8) throw new System.ArgumentOutOfRangeException(nameof(count));

      switch (endianess)
      {
        case Endianess.BigEndian:
          for (var i = count - 1; i >= 0; i--)
          {
            buffer[offset + i] = (byte)(value & 0xff);
            value >>= 8;
          }
          break;
        case Endianess.LittleEndian:
          for (var i = 0; i < count; i++)
          {
            buffer[offset + i] = (byte)(value & 0xff);
            value >>= 8;
          }
          break;
        default:
          throw new System.NotImplementedException();
      }
    }

    public static System.Int16 ToInt16(this byte[] buffer, int index, Endianess endianess) => unchecked((System.Int16)ToUInt16(buffer, index, endianess));

    public static System.Int32 ToInt32(this byte[] buffer, int index, Endianess endianess) => unchecked((System.Int32)ToUInt32(buffer, index, endianess));

    public static System.Int64 ToInt64(this byte[] buffer, int index, Endianess endianess) => unchecked((System.Int64)ToUInt64(buffer, index, endianess));

    public static System.Int128 ToInt128(this byte[] buffer, int index, Endianess endianess) => unchecked((System.Int128)ToUInt128(buffer, index, endianess));

    [System.CLSCompliant(false)]
    public static System.UInt16 ToUInt16(this byte[] buffer, int index, Endianess endianess) => (System.UInt16)BytesToValue(buffer, index, 2, endianess);

    [System.CLSCompliant(false)]
    public static System.UInt32 ToUInt32(this byte[] buffer, int index, Endianess endianess) => (System.UInt32)BytesToValue(buffer, index, 4, endianess);

    [System.CLSCompliant(false)]
    public static System.UInt64 ToUInt64(this byte[] buffer, int index, Endianess endianess) => (System.UInt64)BytesToValue(buffer, index, 8, endianess);

    [System.CLSCompliant(false)]
    public static System.UInt128 ToUInt128(this byte[] buffer, int index, Endianess endianess)
    {
      var lo = BytesToValue(buffer, index, 8, endianess);
      var hi = BytesToValue(buffer, index + 8, 8, endianess);

      switch (endianess)
      {
        case Endianess.BigEndian:
          return new System.UInt128(hi, lo);
        case Endianess.LittleEndian:
          return new System.UInt128(lo, hi);
        default:
          throw new System.NotImplementedException();
      }
    }

    public static System.Int16 ReadInt16(this System.IO.BinaryReader reader, Endianess endianess) => unchecked((System.Int16)ReadUInt16(reader, endianess));
    public static System.Int32 ReadInt32(this System.IO.BinaryReader reader, Endianess endianess) => unchecked((System.Int32)ReadUInt32(reader, endianess));
    public static System.Int64 ReadInt64(this System.IO.BinaryReader reader, Endianess endianess) => unchecked((System.Int64)ReadUInt64(reader, endianess));
    public static System.Int128 ReadInt128(this System.IO.BinaryReader reader, Endianess endianess) => unchecked((System.Int128)ReadUInt128(reader, endianess));

    [System.CLSCompliant(false)]
    public static System.UInt16 ReadUInt16(this System.IO.BinaryReader reader, Endianess endianess)
    {
      var bytes = reader.ReadBytes(2);

      return ToUInt16(bytes, 0, endianess);
    }

    [System.CLSCompliant(false)]
    public static System.UInt32 ReadUInt32(this System.IO.BinaryReader reader, Endianess endianess)
    {
      var bytes = reader.ReadBytes(4);

      return ToUInt32(bytes, 0, endianess);
    }

    [System.CLSCompliant(false)]
    public static System.UInt64 ReadUInt64(this System.IO.BinaryReader reader, Endianess endianess)
    {
      var bytes = reader.ReadBytes(8);

      return ToUInt64(bytes, 0, endianess);
    }

    [System.CLSCompliant(false)]
    public static System.UInt128 ReadUInt128(this System.IO.BinaryReader reader, Endianess endianess)
    {
      var bytes = reader.ReadBytes(16);

      return ToUInt128(bytes, 0, endianess);
    }
  }
}
