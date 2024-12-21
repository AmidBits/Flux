namespace Flux
{
  public static partial class Fx
  {
    #region Read values from a byte buffer

    /// <summary>
    /// <para>Creates a new boolean from the first byte in <paramref name="buffer"/>. If zero then false, otherwise true.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static System.Boolean ReadBoolean(this System.ReadOnlySpan<byte> buffer)
      => buffer[0] != 0;

    public static System.Byte ReadByte(this System.ReadOnlySpan<byte> buffer)
      => buffer[0];

    public static System.Char ReadChar(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Char)buffer[..2].ReadUInt128(endianess));

    public static System.Decimal ReadDecimal(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
    {
      var parts = System.Decimal.GetBits(System.Decimal.Zero);

      switch (endianess)
      {
        case Endianess.BigEndian:
          for (var i = parts.Length - 1; i >= 0; i--)
            parts[i] = ReadInt32(buffer[(i * 4)..], endianess);
          break;
        case Endianess.LittleEndian:
          for (var i = 0; i < parts.Length; i++)
            parts[i] = ReadInt32(buffer[(i * 4)..], endianess);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(endianess));
      }

      return new(parts);
    }

    public static System.Double ReadDouble(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => System.BitConverter.Int64BitsToDouble(buffer.ReadInt64(endianess));

    public static System.Int16 ReadInt16(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Int16)buffer[..2].ReadUInt128(endianess));

    public static System.Int32 ReadInt32(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Int32)buffer[..4].ReadUInt128(endianess));

    public static System.Int64 ReadInt64(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Int64)buffer[..8].ReadUInt128(endianess));

    public static System.Int128 ReadInt128(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Int128)buffer[..16].ReadUInt128(endianess));

    public static System.IntPtr ReadIntPtr(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.IntPtr)buffer[..System.IntPtr.Size].ReadUInt128(endianess));

    [System.CLSCompliant(false)]
    public static System.SByte ReadSByte(this byte[] buffer, Endianess endianess)
      => unchecked((sbyte)buffer[0]);

    public static System.Single ReadSingle(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => System.BitConverter.Int32BitsToSingle(ReadInt32(buffer, endianess));

    [System.CLSCompliant(false)]
    public static System.UInt16 ReadUInt16(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => (System.UInt16)buffer[..2].ReadUInt128(endianess);

    [System.CLSCompliant(false)]
    public static System.UInt32 ReadUInt32(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => (System.UInt32)buffer[..4].ReadUInt128(endianess);

    [System.CLSCompliant(false)]
    public static System.UInt64 ReadUInt64(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => (System.UInt64)buffer[..8].ReadUInt128(endianess);

    [System.CLSCompliant(false)]
    public static System.UInt128 ReadUInt128(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
    {
      if (endianess == Endianess.BigEndian)
      {
        var value = System.UInt128.Zero;
        for (var i = 0; i < buffer.Length; i++)
          value = (value << 8) | buffer[i];
        return value;
      }
      else if (endianess == Endianess.LittleEndian)
      {
        var value = System.UInt128.Zero;
        for (var i = buffer.Length - 1; i >= 0; i--)
          value = (value << 8) | buffer[i];
        return value;
      }
      else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    }

    [System.CLSCompliant(false)]
    public static System.UIntPtr ReadUIntPtr(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => (System.UIntPtr)buffer[..System.UIntPtr.Size].ReadUInt128(endianess);

    #endregion // Read values from a byte buffer

    #region Write values to a byte buffer

    public static void WriteByte(this System.Boolean value, System.Span<byte> buffer)
      => buffer[0] = (byte)(value ? 0x01 : 0x00);

    public static void WriteByte(this System.Byte value, System.Span<byte> buffer)
      => buffer[0] = value;

    public static void WriteBytes(this System.Char value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..2], endianess);

    public static void WriteBytes(this System.Decimal value, System.Span<byte> buffer, Endianess endianess)
    {
      var parts = System.Decimal.GetBits(value);

      switch (endianess)
      {
        case Endianess.BigEndian:
          for (var i = parts.Length - 1; i >= 0; i--)
            parts[i].WriteBytes(buffer.Slice(i * 4), endianess);
          break;
        case Endianess.LittleEndian:
          for (var i = 0; i < parts.Length; i++)
            parts[i].WriteBytes(buffer.Slice(i * 4), endianess);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(endianess));
      }
    }

    public static void WriteBytes(this System.Double value, System.Span<byte> buffer, Endianess endianess)
      => System.BitConverter.DoubleToInt64Bits(value).WriteBytes(buffer, endianess);

    public static void WriteBytes(this System.Int16 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..2], endianess);

    public static void WriteBytes(this System.Int32 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..4], endianess);

    public static void WriteBytes(this System.Int64 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..8], endianess);

    public static void WriteBytes(this System.IntPtr value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..System.IntPtr.Size], endianess);

    public static void WriteBytes(this System.Int128 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..16], endianess);

    [System.CLSCompliant(false)]
    public static void WriteByte(this System.SByte value, System.Span<byte> buffer)
      => buffer[0] = unchecked((byte)value);

    public static void WriteBytes(this System.Single value, System.Span<byte> buffer, Endianess endianess)
      => System.BitConverter.SingleToInt32Bits(value).WriteBytes(buffer, endianess);

    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UInt16 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..2], endianess);

    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UInt32 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..4], endianess);

    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UInt64 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..8], endianess);

    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UInt128 value, System.Span<byte> buffer, Endianess endianess)
    {
      if (endianess == Endianess.BigEndian)
        for (var i = buffer.Length - 1; i >= 0; i--)
        {
          buffer[i] = (byte)(value & 0xff);
          value >>= 8;
        }
      else if (endianess == Endianess.LittleEndian)
        for (var i = 0; i < buffer.Length; i++)
        {
          buffer[i] = (byte)(value & 0xff);
          value >>= 8;
        }
      else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    }

    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UIntPtr value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..System.UIntPtr.Size], endianess);

    #endregion // Write values to a byte buffer
  }
}
