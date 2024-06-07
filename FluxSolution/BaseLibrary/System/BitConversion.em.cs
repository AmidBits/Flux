namespace Flux
{
  public static partial class Fx
  {
    [System.CLSCompliant(false)]
    public static System.UInt128 ReadValue(this System.ReadOnlySpan<byte> buffer, int offset, int count, Endianess endianess)
    {
      if (offset < 0 || offset >= buffer.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || offset + count > buffer.Length || count > 16) throw new System.ArgumentOutOfRangeException(nameof(count));

      var value = System.UInt128.Zero;

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
    public static void WriteBytes(this System.UInt128 value, System.Span<byte> buffer, int offset, int count, Endianess endianess)
    {
      if (offset < 0 || offset >= buffer.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || offset + count > buffer.Length || count > 16) throw new System.ArgumentOutOfRangeException(nameof(count));

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

    public static System.Boolean ReadBoolean(this System.ReadOnlySpan<byte> buffer, int offset, Endianess endianess) => System.BitConverter.ToBoolean(buffer);
    public static System.Byte ReadByte(this System.ReadOnlySpan<byte> buffer, int offset, Endianess endianess) => buffer[offset];
    public static System.Char ReadChar(this byte[] buffer, int offset, Endianess endianess) => unchecked((System.Char)ReadValue(buffer, offset, 2, endianess));
    public static System.Decimal ReadDecimal(this byte[] buffer, int offset, Endianess endianess)
    {
      var parts = System.Decimal.GetBits(System.Decimal.Zero);

      switch (endianess)
      {
        case Endianess.BigEndian:
          for (var i = parts.Length - 1; i >= 0; i--)
            parts[i] = ReadInt32(buffer, offset + i * 4, endianess);
          break;
        case Endianess.LittleEndian:
          for (var i = 0; i < parts.Length; i++)
            parts[i] = ReadInt32(buffer, offset + i * 4, endianess);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(endianess));
      }

      return new(parts);
    }
    public static System.Double ReadDouble(this byte[] buffer, int offset, Endianess endianess) => System.BitConverter.Int64BitsToDouble(ReadInt64(buffer, offset, endianess));
    public static System.Int16 ReadInt16(this byte[] buffer, int offset, Endianess endianess) => unchecked((System.Int16)ReadValue(buffer, offset, 2, endianess));
    public static System.Int32 ReadInt32(this byte[] buffer, int offset, Endianess endianess) => unchecked((System.Int32)ReadValue(buffer, offset, 4, endianess));
    public static System.Int64 ReadInt64(this byte[] buffer, int offset, Endianess endianess) => unchecked((System.Int64)ReadValue(buffer, offset, 8, endianess));
    public static System.Int128 ReadInt128(this System.ReadOnlySpan<byte> buffer, int offset, Endianess endianess) => unchecked((System.Int128)ReadValue(buffer, offset, 16, endianess));
    public static System.IntPtr ReadIntPtr(this System.ReadOnlySpan<byte> buffer, int offset, Endianess endianess) => (System.IntPtr)ReadValue(buffer, offset, System.IntPtr.Size, endianess);
    [System.CLSCompliant(false)] public static System.SByte ReadSByte(this byte[] buffer, int offset, Endianess endianess) => unchecked((sbyte)buffer[offset]);
    public static System.Single ReadSingle(this byte[] buffer, int offset, Endianess endianess) => System.BitConverter.Int32BitsToSingle(ReadInt32(buffer, offset, endianess));
    [System.CLSCompliant(false)] public static System.UInt16 ReadUInt16(this byte[] buffer, int offset, Endianess endianess) => (System.UInt16)ReadValue(buffer, offset, 2, endianess);
    [System.CLSCompliant(false)] public static System.UInt32 ReadUInt32(this byte[] buffer, int offset, Endianess endianess) => (System.UInt32)ReadValue(buffer, offset, 4, endianess);
    [System.CLSCompliant(false)] public static System.UInt64 ReadUInt64(this byte[] buffer, int offset, Endianess endianess) => (System.UInt64)ReadValue(buffer, offset, 8, endianess);
    [System.CLSCompliant(false)] public static System.UInt128 ReadUInt128(this System.ReadOnlySpan<byte> buffer, int offset, Endianess endianess) => (System.UInt128)ReadValue(buffer, offset, 16, endianess);
    [System.CLSCompliant(false)] public static System.UIntPtr ReadUIntPtr(this System.ReadOnlySpan<byte> buffer, int offset, Endianess endianess) => (System.UIntPtr)ReadValue(buffer, offset, System.UIntPtr.Size, endianess);

    public static void WriteBytes(this System.Boolean value, System.Span<byte> buffer, int offset, Endianess endianess) => System.BitConverter.GetBytes(value).CopyTo(buffer);
    public static void WriteBytes(this System.Byte value, System.Span<byte> buffer, int offset, Endianess endianess) => buffer[offset] = value;
    public static void WriteBytes(this System.Char value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer, offset, 2, endianess);
    public static void WriteBytes(this System.Decimal value, System.Span<byte> buffer, int offset, Endianess endianess)
    {
      var parts = System.Decimal.GetBits(value);

      switch (endianess)
      {
        case Endianess.BigEndian:
          for (var i = parts.Length - 1; i >= 0; i--)
            parts[i].WriteBytes(buffer, i * 4, endianess);
          break;
        case Endianess.LittleEndian:
          for (var i = 0; i < parts.Length; i++)
            parts[i].WriteBytes(buffer, i * 4, endianess);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(endianess));
      }
    }
    public static void WriteBytes(this System.Double value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(System.BitConverter.DoubleToInt64Bits(value), buffer, offset, endianess);
    public static void WriteBytes(this System.Int16 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer, offset, 2, endianess);
    public static void WriteBytes(this System.Int32 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer, offset, 4, endianess);
    public static void WriteBytes(this System.Int64 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer, offset, 8, endianess);
    public static void WriteBytes(this System.IntPtr value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer, offset, System.IntPtr.Size, endianess);
    public static void WriteBytes(this System.Int128 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer, offset, 16, endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.SByte value, System.Span<byte> buffer, int offset, Endianess endianess) => buffer[offset] = unchecked((byte)value);
    public static void WriteBytes(this System.Single value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(System.BitConverter.SingleToInt32Bits(value), buffer, offset, endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.UInt16 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(value, buffer, offset, 2, endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.UInt32 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(value, buffer, offset, 4, endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.UInt64 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(value, buffer, offset, 8, endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.UInt128 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(value, buffer, offset, 16, endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.UIntPtr value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer, offset, System.UIntPtr.Size, endianess);
  }
}
