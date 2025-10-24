namespace Flux
{
  public static partial class BitConversions
  {
    public static System.Numerics.BigInteger ReadBigInteger(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => new System.Numerics.BigInteger(buffer, false, endianess == Endianess.BigEndian);

    public static int WriteBytes(this System.Numerics.BigInteger value, System.Span<byte> buffer, Endianess endianess)
    {
      var count = value.GetByteCount();

      if (!value.TryWriteBytes(buffer[..count], out var bytesWritten, false, endianess == Endianess.BigEndian))
        throw new System.InvalidOperationException();

      return count;
    }

    #region Read values from a byte buffer

    /// <summary>
    /// <para>Reads a 1-byte <paramref name="buffer"/> to a <see cref="System.Boolean"/>. If 0 then false, otherwise true.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static bool ReadBoolean(this System.ReadOnlySpan<byte> buffer)
      => buffer[0] != 0;

    /// <summary>
    /// <para>Reads a 1-byte <paramref name="buffer"/> to a <see cref="System.Byte"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static byte ReadByte(this System.ReadOnlySpan<byte> buffer)
      => buffer[0];

    /// <summary>
    /// <para>Reads a 2-byte <paramref name="buffer"/> to a <see cref="System.Char"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static char ReadChar(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((char)buffer.ReadUInt16(endianess));

    /// <summary>
    /// <para>Reads a 16-byte <paramref name="buffer"/> to a <see cref="System.Decimal"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static decimal ReadDecimal(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
    {
      var parts = decimal.GetBits(decimal.Zero);

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
      => new(buffer.ReadDateTime(endianess), buffer[8..].ReadTimeSpan(endianess));

    /// <summary>
    /// <para>Reads an 8-byte <paramref name="buffer"/> to a <see cref="System.Double"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static double ReadDouble(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => System.BitConverter.Int64BitsToDouble(buffer.ReadInt64(endianess));

    /// <summary>
    /// <para>Reads a 16-byte <paramref name="buffer"/> to a <see cref="System.Guid"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.Guid ReadGuid(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => new(buffer, endianess == Endianess.BigEndian);

    /// <summary>
    /// <para>Reads a 2-byte <paramref name="buffer"/> to a <see cref="System.Int16"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static short ReadInt16(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((short)buffer.ReadUInt16(endianess));

    /// <summary>
    /// <para>Reads a 4-byte <paramref name="buffer"/> to a <see cref="System.Int32"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static int ReadInt32(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((int)buffer.ReadUInt32(endianess));

    /// <summary>
    /// <para>Reads an 8-byte <paramref name="buffer"/> to a <see cref="System.Int64"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static long ReadInt64(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((long)buffer.ReadUInt64(endianess));

    /// <summary>
    /// <para>Reads a 16-byte <paramref name="buffer"/> to a <see cref="System.Int128"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static System.Int128 ReadInt128(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((System.Int128)buffer.ReadUInt128(endianess));

    /// <summary>
    /// <para>Reads a <see cref="System.IntPtr.Size"/>-byte (4-bytes in a 32-bit process, 8-bytes in a 64-bit process, etc.) <paramref name="buffer"/> to a <see cref="System.IntPtr"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <remarks>This is machine dependent, so it is NOT universal. Be mindful.</remarks>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static nint ReadIntPtr(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => unchecked((nint)buffer.ReadUIntPtr(endianess));

    /// <summary>
    /// <para>Reads a 1-byte <paramref name="buffer"/> to a <see cref="System.SByte"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static sbyte ReadSByte(this System.ReadOnlySpan<byte> buffer)
      => unchecked((sbyte)buffer[0]);

    /// <summary>
    /// <para>Reads a 4-byte <paramref name="buffer"/> to a <see cref="System.Single"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    public static float ReadSingle(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
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
    public static ushort ReadUInt16(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
    {
      if (buffer.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(buffer));

      if (endianess == Endianess.BigEndian)
        return (ushort)((buffer[0] << 8) | buffer[1]);
      else if (endianess == Endianess.LittleEndian)
        return (ushort)((buffer[1] << 8) | buffer[0]);
      else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    }

    /// <summary>
    /// <para>Reads a 4-byte <paramref name="buffer"/> to a <see cref="System.UInt32"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static uint ReadUInt32(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
    {
      if (buffer.Length < 4) throw new System.ArgumentOutOfRangeException(nameof(buffer));

      if (endianess == Endianess.BigEndian)
        return ((uint)buffer[0] << 24) | ((uint)buffer[1] << 16) | ((uint)buffer[2] << 8) | buffer[3];
      else if (endianess == Endianess.LittleEndian)
        return ((uint)buffer[3] << 24) | ((uint)buffer[2] << 16) | ((uint)buffer[1] << 8) | buffer[0];
      else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    }

    /// <summary>
    /// <para>Reads an 8-byte <paramref name="buffer"/> to a <see cref="System.UInt64"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static ulong ReadUInt64(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
    {
      if (buffer.Length < 8) throw new System.ArgumentOutOfRangeException(nameof(buffer));

      if (endianess == Endianess.BigEndian)
        return (ulong)buffer[0] << 56 | (ulong)buffer[1] << 48 | (ulong)buffer[2] << 40 | (ulong)buffer[3] << 32 | (ulong)buffer[4] << 24 | (ulong)buffer[5] << 16 | (ulong)buffer[6] << 8 | (ulong)buffer[7];
      else if (endianess == Endianess.LittleEndian)
        return (ulong)buffer[7] << 56 | (ulong)buffer[6] << 48 | (ulong)buffer[5] << 40 | (ulong)buffer[4] << 32 | (ulong)buffer[3] << 24 | (ulong)buffer[2] << 16 | (ulong)buffer[1] << 8 | (ulong)buffer[0];
      else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    }

    /// <summary>
    /// <para>Reads a 16-byte <paramref name="buffer"/> to a <see cref="System.UInt128"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static System.UInt128 ReadUInt128(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
    {
      if (buffer.Length < 16) throw new System.ArgumentOutOfRangeException(nameof(buffer));

      if (endianess == Endianess.BigEndian)
        return (System.UInt128)buffer[0] << 120 | (System.UInt128)buffer[1] << 112 | (System.UInt128)buffer[2] << 104 | (System.UInt128)buffer[3] << 96 | (System.UInt128)buffer[4] << 88 | (System.UInt128)buffer[5] << 80 | (System.UInt128)buffer[6] << 72 | (System.UInt128)buffer[7] << 64 | (System.UInt128)buffer[8] << 56 | (System.UInt128)buffer[9] << 48 | (System.UInt128)buffer[10] << 40 | (System.UInt128)buffer[11] << 32 | (System.UInt128)buffer[12] << 24 | (System.UInt128)buffer[13] << 16 | (System.UInt128)buffer[14] << 8 | (System.UInt128)buffer[15];
      else if (endianess == Endianess.LittleEndian)
        return (System.UInt128)buffer[15] << 120 | (System.UInt128)buffer[14] << 112 | (System.UInt128)buffer[13] << 104 | (System.UInt128)buffer[12] << 96 | (System.UInt128)buffer[11] << 88 | (System.UInt128)buffer[10] << 80 | (System.UInt128)buffer[9] << 72 | (System.UInt128)buffer[8] << 64 | (System.UInt128)buffer[7] << 56 | (System.UInt128)buffer[6] << 48 | (System.UInt128)buffer[5] << 40 | (System.UInt128)buffer[4] << 32 | (System.UInt128)buffer[3] << 24 | (System.UInt128)buffer[2] << 16 | (System.UInt128)buffer[1] << 8 | (System.UInt128)buffer[0];
      else throw new System.ArgumentOutOfRangeException(nameof(endianess));

    }

    /// <summary>
    /// <para>Reads a <see cref="System.UIntPtr.Size"/>-byte (4-bytes in a 32-bit process, 8-bytes in a 64-bit process, etc.) <paramref name="buffer"/> to a <see cref="System.UIntPtr"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <remarks>This is machine dependent, so it is NOT universal. Be mindful.</remarks>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static nuint ReadUIntPtr(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => (nuint)ReadBuffer(buffer[..nuint.Size], endianess);

    /// <summary>
    /// <para>Reads <paramref name="buffer"/>.Length into a value (i.e. 4 bytes = the lower 32-bits, 8 bytes = the lower 64-bits, etc.).</para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    private static System.UInt128 ReadBuffer(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
    {
      var value = System.UInt128.Zero;

      if (endianess == Endianess.BigEndian)
        for (var i = 0; i < buffer.Length; i++)
          value = (value << 8) | buffer[i];
      else if (endianess == Endianess.LittleEndian)
        for (var i = buffer.Length - 1; i >= 0; i--)
          value = (value << 8) | buffer[i];
      else throw new System.ArgumentOutOfRangeException(nameof(endianess));

      return value;
    }

    ///// <summary>
    ///// <para>Reads <paramref name="buffer"/>.Length into <paramref name="bytes"/>.</para>
    ///// </summary>
    ///// <param name="buffer"></param>
    ///// <param name="endianess"></param>
    ///// <param name="bytes"></param>
    ///// <returns></returns>
    //private static int ReadBytes(this System.ReadOnlySpan<byte> buffer, Endianess endianess, out byte[] bytes)
    //{
    //  bytes = new byte[buffer.Length];

    //  if (endianess == Endianess.BigEndian)
    //    for (var i = 0; i < buffer.Length; i++)
    //      bytes[bytes.Length - i - 1] = buffer[i];
    //  else if (endianess == Endianess.LittleEndian)
    //    for (var i = buffer.Length - 1; i >= 0; i--)
    //      bytes[bytes.Length - i - 1] = buffer[i];
    //  else throw new System.ArgumentOutOfRangeException(nameof(endianess));

    //  return bytes.Length;
    //}

    #endregion // Read values from a byte buffer

    #region Write values to a byte buffer

    /// <summary>
    /// <para>Writes a <see cref="System.Boolean"/> to a 1-byte <paramref name="buffer"/>.</para>
    /// <para> If true then 1, otherwise 0.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    public static void WriteByte(this bool value, System.Span<byte> buffer)
      => buffer[0] = (byte)(value ? 0x01 : 0x00);

    /// <summary>
    /// <para>Writes a <see cref="System.Byte"/> to a 1-byte <paramref name="buffer"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    public static void WriteByte(this byte value, System.Span<byte> buffer)
      => buffer[0] = value;

    /// <summary>
    /// <para>Writes a <see cref="System.Char"/> to a 2-byte <paramref name="buffer"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this char value, System.Span<byte> buffer, Endianess endianess)
      => System.UInt128.CreateChecked(unchecked((ushort)value)).WriteBuffer(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.Decimal"/> to a 16-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this decimal value, System.Span<byte> buffer, Endianess endianess)
    {
      var parts = decimal.GetBits(value);

      switch (endianess)
      {
        case Endianess.BigEndian:
          for (var i = parts.Length - 1; i >= 0; i--)
            parts[i].WriteBytes(buffer[(i * 4)..], endianess);
          break;
        case Endianess.LittleEndian:
          for (var i = 0; i < parts.Length; i++)
            parts[i].WriteBytes(buffer[(i * 4)..], endianess);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(endianess));
      }
    }

    ///// <summary>
    ///// <para>Writes a <see cref="System.DateTime"/> to an 8-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    ///// </summary>
    ///// <param name="value"></param>
    ///// <param name="buffer"></param>
    ///// <param name="endianess"></param>
    //public static void WriteBytes(this System.DateTime value, System.Span<byte> buffer, Endianess endianess)
    //  => value.Ticks.WriteBytes(buffer, endianess);

    ///// <summary>
    ///// <para>Writes a <see cref="System.DateTimeOffset"/> to a 16-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    ///// </summary>
    ///// <param name="value"></param>
    ///// <param name="buffer"></param>
    ///// <param name="endianess"></param>
    //public static void WriteBytes(this System.DateTimeOffset value, System.Span<byte> buffer, Endianess endianess)
    //{
    //  value.DateTime.WriteBytes(buffer[..8], endianess);
    //  value.Offset.WriteBytes(buffer.Slice(8, 8), endianess);
    //}

    /// <summary>
    /// <para>Writes a <see cref="System.Double"/> to an 8-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this double value, System.Span<byte> buffer, Endianess endianess)
      => System.UInt128.CreateChecked(System.BitConverter.DoubleToUInt64Bits(value)).WriteBuffer(buffer, endianess);

    ///// <summary>
    ///// <para>Writes a <see cref="System.Guid"/> to a 16-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    ///// </summary>
    ///// <param name="value"></param>
    ///// <param name="buffer"></param>
    ///// <param name="endianess"></param>
    //public static void WriteBytes(this System.Guid value, System.Span<byte> buffer, Endianess endianess)
    //{
    //  if (!value.TryWriteBytes(buffer, endianess == Endianess.BigEndian, out var bytesWritten) || bytesWritten != 16)
    //    throw new System.InvalidOperationException();
    //}

    /// <summary>
    /// <para>Writes a <see cref="System.Int16"/> to a 2-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this short value, System.Span<byte> buffer, Endianess endianess)
      => System.UInt128.CreateChecked(unchecked((ushort)value)).WriteBuffer(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.Int32"/> to a 4-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this int value, System.Span<byte> buffer, Endianess endianess)
      => System.UInt128.CreateChecked(unchecked((uint)value)).WriteBuffer(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.Int64"/> to an 8-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this long value, System.Span<byte> buffer, Endianess endianess)
      => System.UInt128.CreateChecked(unchecked((ulong)value)).WriteBuffer(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.IntPtr"/> to a <see cref="System.IntPtr.Size"/>-byte (4-bytes in a 32-bit process, 8-bytes in a 64-bit process, etc.) <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this nint value, System.Span<byte> buffer, Endianess endianess)
      => System.UInt128.CreateChecked(unchecked((nuint)value)).WriteBuffer(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.Int128"/> to a 16-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this System.Int128 value, System.Span<byte> buffer, Endianess endianess)
      => unchecked((System.UInt128)value).WriteBuffer(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.SByte"/> to a 1-byte <paramref name="buffer"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    [System.CLSCompliant(false)]
    public static void WriteByte(this sbyte value, System.Span<byte> buffer)
      => buffer[0] = unchecked((byte)value);

    /// <summary>
    /// <para>Writes a <see cref="System.Single"/> to a 4-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    public static void WriteBytes(this float value, System.Span<byte> buffer, Endianess endianess)
      => System.UInt128.CreateChecked(System.BitConverter.SingleToUInt32Bits(value)).WriteBuffer(buffer[int.Min(buffer.Length, 4)..], endianess);

    //public static void WriteBytes<T>(this T value, System.Span<byte> buffer, Endianess endianess)
    //  where T : System.Numerics.IFloatingPoint<T>
    //{
    //  ((5) as System.Numerics.IFloatingPoint<T>)?.Try(,)
    //  value.WriteLittleEndian(buffer);
    //}

    ///// <summary>
    ///// <para>Writes a <see cref="System.TimeSpan"/> to an 8-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    ///// </summary>
    ///// <param name="value"></param>
    ///// <param name="buffer"></param>
    ///// <param name="endianess"></param>
    //public static void WriteBytes(this System.TimeSpan value, System.Span<byte> buffer, Endianess endianess)
    //  => value.Ticks.WriteBytes(buffer, endianess);

    /// <summary>
    /// <para>Writes a <see cref="System.UInt16"/> to a 2-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytes(this ushort value, System.Span<byte> buffer, Endianess endianess)
      => WriteBuffer(value, buffer[..int.Min(buffer.Length, 2)], endianess);
    //{
    //  if (buffer.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(buffer));

    //  if (endianess == Endianess.BigEndian)
    //  {
    //    buffer[1] = (byte)value;
    //    buffer[0] = (byte)(value >> 8);
    //  }
    //  else if (endianess == Endianess.LittleEndian)
    //  {
    //    buffer[0] = (byte)value;
    //    buffer[1] = (byte)(value >> 8);
    //  }
    //  else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    //}

    /// <summary>
    /// <para>Writes a <see cref="System.UInt32"/> to a 4-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytes(this uint value, System.Span<byte> buffer, Endianess endianess)
      => WriteBuffer(value, buffer[..int.Min(buffer.Length, 4)], endianess);
    //{
    //  if (buffer.Length < 4) throw new System.ArgumentOutOfRangeException(nameof(buffer));

    //  if (endianess == Endianess.BigEndian)
    //  {
    //    buffer[3] = (byte)value;
    //    buffer[2] = (byte)(value >> 8);
    //    buffer[1] = (byte)(value >> 16);
    //    buffer[0] = (byte)(value >> 24);
    //  }
    //  else if (endianess == Endianess.LittleEndian)
    //  {
    //    buffer[0] = (byte)value;
    //    buffer[1] = (byte)(value >> 8);
    //    buffer[2] = (byte)(value >> 16);
    //    buffer[3] = (byte)(value >> 24);
    //  }
    //  else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    //}

    /// <summary>
    /// <para>Writes a <see cref="System.UInt64"/> to an 8-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytes(this ulong value, System.Span<byte> buffer, Endianess endianess)
      => WriteBuffer(value, buffer[..int.Min(buffer.Length, 8)], endianess);
    //{
    //  if (buffer.Length < 8) throw new System.ArgumentOutOfRangeException(nameof(buffer));

    //  if (endianess == Endianess.BigEndian)
    //  {
    //    buffer[7] = (byte)value;
    //    buffer[6] = (byte)(value >> 8);
    //    buffer[5] = (byte)(value >> 16);
    //    buffer[4] = (byte)(value >> 24);
    //    buffer[3] = (byte)(value >> 32);
    //    buffer[2] = (byte)(value >> 40);
    //    buffer[1] = (byte)(value >> 48);
    //    buffer[0] = (byte)(value >> 56);
    //  }
    //  else if (endianess == Endianess.LittleEndian)
    //  {
    //    buffer[0] = (byte)value;
    //    buffer[1] = (byte)(value >> 8);
    //    buffer[2] = (byte)(value >> 16);
    //    buffer[3] = (byte)(value >> 24);
    //    buffer[4] = (byte)(value >> 32);
    //    buffer[5] = (byte)(value >> 40);
    //    buffer[6] = (byte)(value >> 48);
    //    buffer[7] = (byte)(value >> 56);
    //  }
    //  else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    //}

    /// <summary>
    /// <para>Writes a <see cref="System.UInt128"/> to a 16-byte <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UInt128 value, System.Span<byte> buffer, Endianess endianess)
      => WriteBuffer(value, buffer, endianess);
    //{
    //  if (buffer.Length < 16) throw new System.ArgumentOutOfRangeException(nameof(buffer));

    //  if (endianess == Endianess.BigEndian)
    //  {
    //    buffer[15] = (byte)value;
    //    buffer[14] = (byte)(value >> 8);
    //    buffer[13] = (byte)(value >> 16);
    //    buffer[12] = (byte)(value >> 24);
    //    buffer[11] = (byte)(value >> 32);
    //    buffer[10] = (byte)(value >> 40);
    //    buffer[9] = (byte)(value >> 48);
    //    buffer[8] = (byte)(value >> 56);
    //    buffer[7] = (byte)(value >> 64);
    //    buffer[6] = (byte)(value >> 72);
    //    buffer[5] = (byte)(value >> 80);
    //    buffer[4] = (byte)(value >> 88);
    //    buffer[3] = (byte)(value >> 96);
    //    buffer[2] = (byte)(value >> 104);
    //    buffer[1] = (byte)(value >> 112);
    //    buffer[0] = (byte)(value >> 120);
    //  }
    //  else if (endianess == Endianess.LittleEndian)
    //  {
    //    buffer[0] = (byte)value;
    //    buffer[1] = (byte)(value >> 8);
    //    buffer[2] = (byte)(value >> 16);
    //    buffer[3] = (byte)(value >> 24);
    //    buffer[4] = (byte)(value >> 32);
    //    buffer[5] = (byte)(value >> 40);
    //    buffer[6] = (byte)(value >> 48);
    //    buffer[7] = (byte)(value >> 56);
    //    buffer[8] = (byte)(value >> 64);
    //    buffer[9] = (byte)(value >> 72);
    //    buffer[10] = (byte)(value >> 80);
    //    buffer[11] = (byte)(value >> 88);
    //    buffer[12] = (byte)(value >> 96);
    //    buffer[13] = (byte)(value >> 104);
    //    buffer[14] = (byte)(value >> 112);
    //    buffer[15] = (byte)(value >> 120);
    //  }
    //  else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    //}

    /// <summary>
    /// <para>Writes a <see cref="System.UIntPtr"/> to a <see cref="System.UIntPtr.Size"/>-byte (4-bytes in a 32-bit process, 8-bytes in a 64-bit process, etc.) <paramref name="buffer"/> using the specified <paramref name="endianess"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytes(this nuint value, System.Span<byte> buffer, Endianess endianess)
      => WriteBuffer(value, buffer[..nuint.Size], endianess);

    /// <summary>
    /// <para>Writes a <paramref name="value"/> of <paramref name="buffer"/>.Length bytes, i.e. dynamically (i.e. 4 bytes = the lower 32-bits of <paramref name="value"/>, or 8 bytes = the lower 64-bits of <paramref name="value"/>).</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    [System.CLSCompliant(false)]
    public static void WriteBuffer(this System.UInt128 value, System.Span<byte> buffer, Endianess endianess)
    {
      var maxIndex = buffer.Length - 1;

      if (endianess == Endianess.BigEndian)
      {
        var bitsBelow = maxIndex * 8;
        for (var i = maxIndex; i >= 0; i--)
          buffer[i] = (byte)((value >> (bitsBelow - (i * 8))) & 0xff);
      }
      else if (endianess == Endianess.LittleEndian)
        for (var i = maxIndex; i >= 0; i--)
          buffer[i] = (byte)((value >> (i * 8)) & 0xff);
      else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    }

    #endregion // Write values to a byte buffer

    public static bool TryReadFromBuffer(this System.Span<byte> buffer, Endianess endianess, out System.Decimal value)
    {
      try
      {
        var byteParts = new byte[16].AsSpan();

        buffer.CopyTo(byteParts);

        if (endianess == Endianess.BigEndian)
          System.MemoryExtensions.Reverse(byteParts);

        var intParts = System.Runtime.InteropServices.MemoryMarshal.Cast<byte, int>(byteParts);

        value = new System.Decimal(intParts);
        return true;
      }
      catch { }

      value = default!;
      return false;
    }

    public static bool TryReadFromBuffer(this System.Span<byte> buffer, Endianess endianess, out System.Double value)
    {
      try
      {
        if (TryReadFromBuffer(buffer, endianess, out ulong uint64))
        {
          value = System.BitConverter.UInt64BitsToDouble(uint64);
          return true;
        }
      }
      catch { }

      value = default;
      return false;
    }

    public static bool TryReadFromBuffer(this System.Span<byte> buffer, Endianess endianess, out System.Single value)
    {
      try
      {
        if (TryReadFromBuffer(buffer, endianess, out uint uint32))
        {
          value = System.BitConverter.UInt32BitsToSingle(uint32);
          return true;
        }
      }
      catch { }

      value = default;
      return false;
    }

    public static bool TryReadFromBuffer<T>(this System.ReadOnlySpan<byte> buffer, Endianess endianess, out T value)
      where T : System.Numerics.IBinaryInteger<T>
    {
      if (endianess == Endianess.LittleEndian)
        return T.TryReadLittleEndian(buffer, typeof(T).IsNumericTypeUnsigned(), out value);
      else if (endianess == Endianess.BigEndian)
        return T.TryReadBigEndian(buffer, typeof(T).IsNumericTypeUnsigned(), out value);
      else
      {
        value = default!;
        return false;
      }
    }

    public static bool TryWriteToBuffer(this System.Decimal value, System.Span<byte> buffer, Endianess endianess, out int bytesWritten)
    {
      try
      {
        var intParts = System.Decimal.GetBits(value);

        var byteParts = System.Runtime.InteropServices.MemoryMarshal.AsBytes<int>(intParts).AsSpan();

        if (endianess == Endianess.BigEndian)
          System.MemoryExtensions.Reverse(byteParts);

        byteParts.CopyTo(buffer);

        bytesWritten = byteParts.Length;
        return true;
      }
      catch { }

      bytesWritten = 0;
      return false;
    }

    public static bool TryWriteToBuffer(this System.Double value, System.Span<byte> buffer, Endianess endianess, out int bytesWritten)
      => System.BitConverter.DoubleToUInt64Bits(value).TryWriteToBuffer(buffer, endianess, out bytesWritten);

    public static bool TryWriteToBuffer(this System.Single value, System.Span<byte> buffer, Endianess endianess, out int bytesWritten)
      => System.BitConverter.SingleToUInt32Bits(value).TryWriteToBuffer(buffer, endianess, out bytesWritten);

    public static bool TryWriteToBuffer<T>(this T value, System.Span<byte> buffer, Endianess endianess, out int bytesWritten)
      where T : System.Numerics.IBinaryInteger<T>
    {
      if (endianess == Endianess.LittleEndian)
        return value.TryWriteLittleEndian(buffer, out bytesWritten);
      else if (endianess == Endianess.BigEndian)
        return value.TryWriteBigEndian(buffer, out bytesWritten);
      else
      {
        bytesWritten = 0;
        return false;
      }
    }
  }
}
