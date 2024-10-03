using System.ComponentModel.Design;

namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static System.UInt128 ReadValueBigEndian(this System.ReadOnlySpan<byte> buffer)
    {
      var value = System.UInt128.Zero;
      for (var i = 0; i < buffer.Length; i++)
        value = (value << 8) | buffer[i];
      return value;
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static System.UInt128 ReadValueLittleEndian(this System.ReadOnlySpan<byte> buffer)
    {
      var value = System.UInt128.Zero;
      for (var i = buffer.Length - 1; i >= 0; i--)
        value = (value << 8) | buffer[i];
      return value;
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    [System.CLSCompliant(false)]
    public static System.UInt128 ReadValue(this System.ReadOnlySpan<byte> buffer, Endianess endianess)
      => endianess == Endianess.BigEndian ? ReadValueBigEndian(buffer) : endianess == Endianess.LittleEndian ? ReadValueLittleEndian(buffer) : throw new System.ArgumentOutOfRangeException(nameof(endianess));

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytesBigEndian(this System.UInt128 value, System.Span<byte> buffer)
    {
      for (var i = buffer.Length - 1; i >= 0; i--)
      {
        buffer[i] = (byte)(value & 0xff);
        value >>= 8;
      }
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    [System.CLSCompliant(false)]
    public static void WriteBytesLittleEndian(this System.UInt128 value, System.Span<byte> buffer)
    {
      for (var i = 0; i < buffer.Length; i++)
      {
        buffer[i] = (byte)(value & 0xff);
        value >>= 8;
      }
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="buffer"></param>
    /// <param name="endianess"></param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    [System.CLSCompliant(false)]
    public static void WriteBytes(this System.UInt128 value, System.Span<byte> buffer, Endianess endianess)
    {
      if (endianess == Endianess.BigEndian) WriteBytesBigEndian(value, buffer);
      else if (endianess == Endianess.LittleEndian) WriteBytesLittleEndian(value, buffer);
      else throw new System.ArgumentOutOfRangeException(nameof(endianess));
    }

    #region Read values from a buffer.

    public static System.Boolean ReadBoolean(this byte[] buffer, int offset, Endianess endianess) => System.BitConverter.ToBoolean(buffer, offset);
    public static System.Byte ReadByte(this byte[] buffer, int offset, Endianess endianess) => buffer[offset];
    public static System.Char ReadChar(this byte[] buffer, int offset, Endianess endianess) => unchecked((System.Char)ReadValue(buffer.AsReadOnlySpan().Slice(offset, 2), endianess));
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
    public static System.Int16 ReadInt16(this byte[] buffer, int offset, Endianess endianess) => unchecked((System.Int16)ReadValue(buffer.AsReadOnlySpan().Slice(offset, 2), endianess));
    public static System.Int32 ReadInt32(this byte[] buffer, int offset, Endianess endianess) => unchecked((System.Int32)ReadValue(buffer.AsReadOnlySpan().Slice(offset, 4), endianess));
    public static System.Int64 ReadInt64(this byte[] buffer, int offset, Endianess endianess) => unchecked((System.Int64)ReadValue(buffer.AsReadOnlySpan().Slice(offset, 8), endianess));
    public static System.Int128 ReadInt128(this byte[] buffer, int offset, Endianess endianess) => unchecked((System.Int128)ReadValue(buffer.AsReadOnlySpan().Slice(offset, 16), endianess));
    public static System.IntPtr ReadIntPtr(this byte[] buffer, int offset, Endianess endianess) => (System.IntPtr)ReadValue(buffer.AsReadOnlySpan().Slice(offset, System.IntPtr.Size), endianess);
    [System.CLSCompliant(false)] public static System.SByte ReadSByte(this byte[] buffer, int offset, Endianess endianess) => unchecked((sbyte)buffer[offset]);
    public static System.Single ReadSingle(this byte[] buffer, int offset, Endianess endianess) => System.BitConverter.Int32BitsToSingle(ReadInt32(buffer, offset, endianess));
    [System.CLSCompliant(false)] public static System.UInt16 ReadUInt16(this byte[] buffer, int offset, Endianess endianess) => (System.UInt16)ReadValue(buffer.AsReadOnlySpan().Slice(offset, 2), endianess);
    [System.CLSCompliant(false)] public static System.UInt32 ReadUInt32(this byte[] buffer, int offset, Endianess endianess) => (System.UInt32)ReadValue(buffer.AsReadOnlySpan().Slice(offset, 4), endianess);
    [System.CLSCompliant(false)] public static System.UInt64 ReadUInt64(this byte[] buffer, int offset, Endianess endianess) => (System.UInt64)ReadValue(buffer.AsReadOnlySpan().Slice(offset, 8), endianess);
    [System.CLSCompliant(false)] public static System.UInt128 ReadUInt128(this byte[] buffer, int offset, Endianess endianess) => (System.UInt128)ReadValue(buffer.AsReadOnlySpan().Slice(offset, 16), endianess);
    [System.CLSCompliant(false)] public static System.UIntPtr ReadUIntPtr(this byte[] buffer, int offset, Endianess endianess) => (System.UIntPtr)ReadValue(buffer.AsReadOnlySpan().Slice(offset, System.UIntPtr.Size), endianess);

    #endregion // Read values from a buffer.

    #region Write values to a buffer.

    public static void WriteBytes(this System.Boolean value, System.Span<byte> buffer, int offset, Endianess endianess) => System.BitConverter.GetBytes(value).CopyTo(buffer);
    public static void WriteBytes(this System.Byte value, System.Span<byte> buffer, int offset, Endianess endianess) => buffer[offset] = value;
    public static void WriteBytes(this System.Char value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer.Slice(offset, 2), endianess);
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
    public static void WriteBytes(this System.Int16 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer.Slice(offset, 2), endianess);
    public static void WriteBytes(this System.Int32 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer.Slice(offset, 4), endianess);
    public static void WriteBytes(this System.Int64 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer.Slice(offset, 8), endianess);
    public static void WriteBytes(this System.IntPtr value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer.Slice(offset, System.IntPtr.Size), endianess);
    public static void WriteBytes(this System.Int128 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer.Slice(offset, 16), endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.SByte value, System.Span<byte> buffer, int offset, Endianess endianess) => buffer[offset] = unchecked((byte)value);
    public static void WriteBytes(this System.Single value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(System.BitConverter.SingleToInt32Bits(value), buffer, offset, endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.UInt16 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(value, buffer.Slice(offset, 2), endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.UInt32 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(value, buffer.Slice(offset, 4), endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.UInt64 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(value, buffer.Slice(offset, 8), endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.UInt128 value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(value, buffer.Slice(offset, 16), endianess);
    [System.CLSCompliant(false)] public static void WriteBytes(this System.UIntPtr value, System.Span<byte> buffer, int offset, Endianess endianess) => WriteBytes(unchecked((System.UInt128)value), buffer.Slice(offset, System.UIntPtr.Size), endianess);

    #endregion // Write values to a buffer.
  }
}
