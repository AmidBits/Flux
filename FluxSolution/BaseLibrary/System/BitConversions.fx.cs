namespace Flux
{
  public static partial class Fx
  {
    #region Read values from a byte buffer

    /// <summary>
    /// <para>Reads a 1-byte <paramref name="buffer"/> to a <see cref="System.Boolean"/>. If 0 then false, otherwise true.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static System.Boolean ReadBoolean(this System.ReadOnlySpan<byte> buffer)
      => buffer[0] != 0;

    public static System.Byte ReadByte(this System.ReadOnlySpan<byte> buffer)
      => buffer[0];

    public static System.Char ReadChar(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Char)buffer[..2].ReadUInt128(endianess));

    /// <summary>
    /// <para>Reads a 16-byte <paramref name="buffer"/> to a <see cref="System.Decimal"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
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

    /// <summary>
    /// <para>Reads an 8-byte <paramref name="buffer"/> to a <see cref="System.DateTime"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.DateTime ReadDateTime(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => new(buffer.ReadInt64(endianess));

    /// <summary>
    /// <para>Reads a 16-byte <paramref name="buffer"/> to a <see cref="System.DateTimeOffset"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.DateTimeOffset ReadDateTimeOffset(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => new(buffer.Slice(0, 8).ReadDateTime(endianess), buffer.Slice(8, 8).ReadTimeSpan(endianess));

    /// <summary>
    /// <para>Reads an 8-byte <paramref name="buffer"/> to a <see cref="System.Double"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.Double ReadDouble(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => System.BitConverter.Int64BitsToDouble(buffer.ReadInt64(endianess));

    /// <summary>
    /// <para>Reads a 16-byte <paramref name="buffer"/> to a <see cref="System.Guid"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.Guid ReadGuid(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => new System.Guid(buffer, endianess == Endianess.BigEndian);

    /// <summary>
    /// <para>Reads a 2-byte <paramref name="buffer"/> to a <see cref="System.Int16"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.Int16 ReadInt16(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Int16)buffer[..2].ReadUInt128(endianess));

    /// <summary>
    /// <para>Reads a 4-byte <paramref name="buffer"/> to a <see cref="System.Int32"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.Int32 ReadInt32(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Int32)buffer[..4].ReadUInt128(endianess));

    /// <summary>
    /// <para>Reads an 8-byte <paramref name="buffer"/> to a <see cref="System.Int64"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.Int64 ReadInt64(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Int64)buffer[..8].ReadUInt128(endianess));

    /// <summary>
    /// <para>Reads a 16-byte <paramref name="buffer"/> to a <see cref="System.Int128"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.Int128 ReadInt128(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Int128)buffer[..16].ReadUInt128(endianess));

    /// <summary>
    /// <para>Reads a <see cref="System.IntPtr.Size"/>-byte (4-bytes in a 32-bit process, 8-bytes in a 64-bit process, etc.) <paramref name="buffer"/> to a <see cref="System.IntPtr"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.IntPtr ReadIntPtr(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.IntPtr)buffer[..System.IntPtr.Size].ReadUInt128(endianess));

    [System.CLSCompliant(false)]
    public static System.SByte ReadSByte(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((sbyte)buffer[0]);

    /// <summary>
    /// <para>Reads a 4-byte <paramref name="buffer"/> to a <see cref="System.Single"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.Single ReadSingle(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => System.BitConverter.Int32BitsToSingle(ReadInt32(buffer, endianess));

    /// <summary>
    /// <para>Reads a 8-byte <paramref name="buffer"/> to a <see cref="System.TimeSpan"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.TimeSpan ReadTimeSpan(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => new(buffer.ReadInt64(endianess));

    /// <summary>
    /// <para>Reads a 2-byte <paramref name="buffer"/> to a <see cref="System.UInt16"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static System.UInt16 ReadUInt16(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => (System.UInt16)buffer[..2].ReadUInt128(endianess);

    /// <summary>
    /// <para>Reads a 4-byte <paramref name="buffer"/> to a <see cref="System.UInt32"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static System.UInt32 ReadUInt32(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => (System.UInt32)buffer[..4].ReadUInt128(endianess);

    /// <summary>
    /// <para>Reads an 8-byte <paramref name="buffer"/> to a <see cref="System.UInt64"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static System.UInt64 ReadUInt64(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => (System.UInt64)buffer[..8].ReadUInt128(endianess);

    /// <summary>
    /// <para>Reads a 16-byte <paramref name="buffer"/> to a <see cref="System.UInt128"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
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

    /// <summary>
    /// <para>Reads a <see cref="System.UIntPtr.Size"/>-byte (4-bytes in a 32-bit process, 8-bytes in a 64-bit process, etc.) <paramref name="buffer"/> to a <see cref="System.UIntPtr"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
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

    /// <summary>
    /// <para>Writes a <see cref="System.Decimal"/> to a 16-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
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

    /// <summary>
    /// <para>Writes a <see cref="System.DateTime"/> to an 8-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.DateTime value, System.Span<byte> buffer, Endianess endianess)
      => value.Ticks.WriteBytes(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.DateTimeOffset"/> to a 16-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.DateTimeOffset value, System.Span<byte> buffer, Endianess endianess)
    {
      value.DateTime.WriteBytes(buffer.Slice(0, 8), endianess);
      value.Offset.WriteBytes(buffer.Slice(8, 8), endianess);
    }

    /// <summary>
    /// <para>Writes a <see cref="System.Double"/> to an 8-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.Double value, System.Span<byte> buffer, Endianess endianess)
      => System.BitConverter.DoubleToInt64Bits(value).WriteBytes(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.Guid"/> to a 16-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.Guid value, System.Span<byte> buffer, Endianess endianess)
    {
      if (!value.TryWriteBytes(buffer, endianess == Endianess.BigEndian, out var bytesWritten))
        throw new System.InvalidOperationException();
    }

    /// <summary>
    /// <para>Writes a <see cref="System.Int16"/> to a 2-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.Int16 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..2], endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.Int32"/> to a 4-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.Int32 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..4], endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.Int64"/> to an 8-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.Int64 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..8], endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.IntPtr"/> to a <see cref="System.IntPtr.Size"/>-byte (4-bytes in a 32-bit process, 8-bytes in a 64-bit process, etc.) <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.IntPtr value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..System.IntPtr.Size], endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.Int128"/> to a 16-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.Int128 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..16], endianess);

    [System.CLSCompliant(false)]
    public static void WriteByte(this System.SByte value, System.Span<byte> buffer)
      => buffer[0] = unchecked((byte)value);

    /// <summary>
    /// <para>Writes a <see cref="System.Single"/> to a 4-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.Single value, System.Span<byte> buffer, Endianess endianess)
      => System.BitConverter.SingleToInt32Bits(value).WriteBytes(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.TimeSpan"/> to an 8-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.TimeSpan value, System.Span<byte> buffer, Endianess endianess)
      => value.Ticks.WriteBytes(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.UInt16"/> to a 2-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UInt16 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..2], endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.UInt32"/> to a 4-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UInt32 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..4], endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.UInt64"/> to an 8-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UInt64 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..8], endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.UInt128"/> to a 16-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
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

    /// <summary>
    /// <para>Writes a <see cref="System.UIntPtr"/> to a <see cref="System.UIntPtr.Size"/>-byte (4-bytes in a 32-bit process, 8-bytes in a 64-bit process, etc.) <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UIntPtr value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBytes(buffer[..System.UIntPtr.Size], endianess);

    #endregion // Write values to a byte buffer
  }
}
