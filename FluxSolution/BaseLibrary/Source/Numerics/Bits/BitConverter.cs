//namespace Flux
//{
//  //#if NET7_0_OR_GREATER

//  public sealed class BitConverter
//  {
//    public static BitConverter BigEndian
//      => new(Endianess.BigEndian);
//    public static BitConverter LittleEndian
//      => new(Endianess.LittleEndian);

//    private readonly Endianess m_endianess;

//    private BitConverter(Endianess endianess)
//      => m_endianess = endianess;
//    private BitConverter()
//      : this(System.BitConverter.IsLittleEndian ? Endianess.LittleEndian : Endianess.BigEndian)
//    { }

//    public Endianess Endianess
//      => m_endianess;

//    /// <summary>Copies the given number of bytes from the LSB end of the specified value into the specified byte array, beginning at the specified index.</summary>
//    public void CopyBytes(long value, int bytes, byte[] buffer, int startAt)
//    {
//      if (buffer is null) throw new System.ArgumentNullException(nameof(buffer));
//      if (buffer.Length < startAt + bytes) throw new System.ArgumentOutOfRangeException(nameof(buffer));

//      switch (m_endianess)
//      {
//        case Endianess.BigEndian:
//          var endOffset = startAt + bytes - 1;
//          for (int i = 0; i < bytes; i++)
//          {
//            buffer[endOffset - i] = unchecked((byte)(value & 0xff));
//            value >>= 8;
//          }
//          break;
//        case Endianess.LittleEndian:
//          for (var i = 0; i < bytes; i++)
//          {
//            buffer[i + startAt] = unchecked((byte)(value & 0xff));
//            value >>= 8;
//          }
//          break;
//        default:
//          throw new System.NotImplementedException();
//      }
//    }

//    #region CopyBytes From Types

//#if NOTHAPPENINGNOW
//    public void CopyBytesBool(bool value, byte[] buffer, int startAt)
//      => buffer[startAt] = value ? (byte)1 : (byte)0;
//    public int CopyBytesBinaryInteger<TSelf>(TSelf value, byte[] buffer, int startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      switch (m_endianess)
//      {
//        case Endianess.BigEndian:
//          value.WriteBigEndian(buffer, startAt);
//          break;
//        case Endianess.LittleEndian:
//          value.WriteLittleEndian(buffer, startAt);
//          break;
//        default:
//          throw new System.ArgumentException(nameof(m_endianess));
//      }

//      return value.GetByteCount();
//    }
//    public int CopyBytesFloatingPoint<TSelf>(TSelf value, byte[] buffer, int startAt)
//      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
//    {
//      var boolCount = 1;
//      var exponentCount = value.GetExponentByteCount();
//      var significandCount = value.GetSignificandByteCount();

//      switch (m_endianess)
//      {
//        case Endianess.BigEndian:
//          CopyBytesBool(TSelf.IsNegative(value), buffer, startAt);
//          value.WriteExponentBigEndian(buffer, startAt + boolCount);
//          value.WriteSignificandBigEndian(buffer, startAt + boolCount + exponentCount);
//          break;
//        case Endianess.LittleEndian:
//          CopyBytesBool(TSelf.IsNegative(value), buffer, startAt);
//          value.WriteExponentLittleEndian(buffer, startAt + boolCount);
//          value.WriteSignificandLittleEndian(buffer, startAt + boolCount + exponentCount);
//          break;
//        default:
//          throw new System.ArgumentException(nameof(m_endianess));
//      }

//      return boolCount + exponentCount + significandCount;
//    }
//#endif
//    /// <summary>Copies the specified boolean value into the specified byte array, beginning at the specified index.</summary>
//    public void CopyBytes(bool value, byte[] buffer, int startAt)
//      => CopyBytes(value ? 1 : 0, 1, buffer, startAt);
//    /// <summary>Copies the specified character value into the specified byte array, beginning at the specified index.</summary>
//    public void CopyBytes(char value, byte[] buffer, int startAt)
//      => CopyBytes(value, 2, buffer, startAt);
//    /// <summary>Copies the specified decimal value into the specified byte array, beginning at the specified index.</summary>
//    public void CopyBytes(decimal value, byte[] buffer, int startAt)
//    {
//      var bytes = decimal.GetBits(value);
//      for (var i = 0; i < 4; i++)
//        CopyBytes(bytes[i], 4, buffer, i * 4 + startAt);
//    }
//    /// <summary>Copies the specified double-precision floating point value into the specified byte array, beginning at the specified index.</summary>
//    public void CopyBytes(double value, byte[] buffer, int startAt)
//      => CopyBytes(System.BitConverter.DoubleToInt64Bits(value), 8, buffer, startAt);
//    /// <summary>Copies the specified 16-bit signed integer value into the specified byte array, beginning at the specified index.</summary>
//    public void CopyBytes(short value, byte[] buffer, int startAt)
//      => CopyBytes(value, 2, buffer, startAt);
//    /// <summary>Copies the specified 32-bit signed integer value into the specified byte array, beginning at the specified index.</summary>
//    public void CopyBytes(int value, byte[] buffer, int startAt)
//      => CopyBytes(value, 4, buffer, startAt);
//    /// <summary>Copies the specified 64-bit signed integer value into the specified byte array, beginning at the specified index.</summary>
//    public void CopyBytes(long value, byte[] buffer, int startAt)
//      => CopyBytes(value, 8, buffer, startAt);
//    /// <summary>Copies the specified single-precision floating point value into the specified byte array, beginning at the specified index.</summary>
//    public void CopyBytes(float value, byte[] buffer, int startAt)
//      => CopyBytes(new BitStruct(value).Int32, 4, buffer, startAt);
//    /// <summary>Copies the specified 16-bit unsigned integer value into the specified byte array, beginning at the specified index.</summary>
//    [System.CLSCompliant(false)]
//    public void CopyBytes(ushort value, byte[] buffer, int startAt)
//      => CopyBytes(value, 2, buffer, startAt);
//    /// <summary>Copies the specified 32-bit unsigned integer value into the specified byte array, beginning at the specified index.</summary>
//    [System.CLSCompliant(false)]
//    public void CopyBytes(uint value, byte[] buffer, int startAt)
//      => CopyBytes(value, 4, buffer, startAt);
//    /// <summary>Copies the specified 64-bit unsigned integer value into the specified byte array, beginning at the specified index.</summary>
//    [System.CLSCompliant(false)]
//    public void CopyBytes(ulong value, byte[] buffer, int index)
//      => CopyBytes(unchecked((long)value), 8, buffer, index);
//    #endregion CopyBytes

//    /// <summary>Returns an array with the given number of bytes formed from the least significant bytes of the specified value. This is used to implement the other GetBytes methods.</summary>
//    private byte[] GetBytes(long value, int bytes)
//    {
//      var buffer = new byte[bytes];
//      CopyBytes(value, bytes, buffer, 0);
//      return buffer;
//    }

//    #region GetBytes From Types
//    /// <summary>Returns the specified Boolean value as an array of 1 byte.</summary>
//    public static byte[] GetBytes(bool value)
//      => System.BitConverter.GetBytes(value);
//    /// <summary>Returns the specified Unicode character value as an array of 2 bytes.</summary>
//    public byte[] GetBytes(char value)
//      => GetBytes(value, 2);
//    /// <summary>Returns the specified decimal value as an array of 16 bytes.</summary>
//    public byte[] GetBytes(decimal value)
//    {
//      var bytes = new byte[16];
//      var parts = decimal.GetBits(value);
//      for (var i = 0; i < 4; i++)
//        CopyBytes(parts[i], 4, bytes, i * 4);
//      return bytes;
//    }
//    /// <summary>Returns the specified double-precision floating point value as an array of 8 bytes.</summary>
//    public byte[] GetBytes(double value)
//      => GetBytes(new BitStruct(value).Int64, 8);
//    /// <summary>Returns the specified 16-bit signed integer value as an array of 2 bytes.</summary>
//    public byte[] GetBytes(short value)
//      => GetBytes(value, 2);
//    /// <summary>Returns the specified 32-bit signed integer value as an array of bytes.</summary>
//    /// <param name="value">The number to convert.</param>
//    /// <returns>An array of bytes with length 4.</returns>
//    public byte[] GetBytes(int value)
//      => GetBytes(value, 4);
//    /// <summary>Returns the specified 64-bit signed integer value as an array of 8 bytes.</summary>
//    public byte[] GetBytes(long value)
//      => GetBytes(value, 8);
//    /// <summary>Returns the specified single-precision floating point value as an array of 4 bytes.</summary>
//    public byte[] GetBytes(float value)
//      => GetBytes(new BitStruct(value).Int32, 4);
//    /// <summary>Returns the specified 16-bit unsigned integer value as an array of 2 bytes.</summary>
//    [System.CLSCompliant(false)]
//    public byte[] GetBytes(ushort value)
//      => GetBytes(value, 2);
//    /// <summary>Returns the specified 32-bit unsigned integer value as an array of 4 bytes.</summary>
//    [System.CLSCompliant(false)]
//    public byte[] GetBytes(uint value)
//      => GetBytes(value, 4);
//    /// <summary>Returns the specified 64-bit unsigned integer value as an array of 8 bytes.</summary>
//    [System.CLSCompliant(false)]
//    public byte[] GetBytes(ulong value)
//      => GetBytes(unchecked((long)value), 8);
//    #endregion GetBytes

//    #region FromBytes ToTypes
//    /// <summary>Convert the given number of bytes from the given array, from the given start position, into a long, using the bytes as the least significant part of the long.</summary>
//    public long FromBytes(byte[] buffer, int startIndex, int bytesToConvert)
//    {
//      if (buffer is null) throw new System.ArgumentNullException(nameof(buffer));
//      if (startIndex < 0 && startIndex >= buffer.Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
//      if (bytesToConvert < 0 && startIndex + bytesToConvert >= buffer.Length) throw new System.ArgumentOutOfRangeException(nameof(bytesToConvert));

//      var value = 0L;
//      switch (m_endianess)
//      {
//        case Endianess.BigEndian:
//          for (var i = 0; i < bytesToConvert; i++)
//            value = unchecked((value << 8) | buffer[startIndex + i]);
//          break;
//        case Endianess.LittleEndian:
//          for (var i = 0; i < bytesToConvert; i++)
//            value = unchecked((value << 8) | buffer[startIndex + bytesToConvert - 1 - i]);
//          break;
//        default:
//          throw new System.NotImplementedException();
//      }
//      return value;
//    }

//    /// <summary>Returns a Boolean value converted from one byte at a specified position in a byte array.</summary>
//    public bool ToBoolean(byte[] value, int startIndex)
//    {
//      var _ = FromBytes(value, startIndex, 1);
//      return System.BitConverter.ToBoolean(value, startIndex);
//    }
//    /// <summary>Returns a Unicode character converted from two bytes at a specified position in a byte array.</summary>
//    public char ToChar(byte[] value, int startIndex)
//      => unchecked((char)FromBytes(value, startIndex, 2));
//    /// <summary>Returns a decimal value converted from sixteen bytes at a specified position in a byte array.</summary>
//    public decimal ToDecimal(byte[] value, int startIndex)
//    {
//      // HACK: This always assumes four parts, each in their own endianness, starting with the first part at the start of the byte array.
//      // On the other hand, there's no real format specified...
//      var bytes = new int[4];
//      for (int i = 0; i < 4; i++)
//        bytes[i] = ToInt32(value, startIndex + i * 4);
//      return new System.Decimal(bytes);
//    }
//    /// <summary>Returns a double-precision floating point number converted from eight bytes at a specified position in a byte array.</summary>
//    public double ToDouble(byte[] value, int startIndex)
//      => System.BitConverter.Int64BitsToDouble(ToInt64(value, startIndex));
//    /// <summary>Returns a 16-bit signed integer converted from two bytes at a specified position in a byte array.</summary>
//    public short ToInt16(byte[] value, int startIndex)
//      => unchecked((short)(FromBytes(value, startIndex, 2)));
//    /// <summary>Returns a 32-bit signed integer converted from four bytes at a specified position in a byte array.</summary>
//    public int ToInt32(byte[] value, int startIndex)
//      => unchecked((int)(FromBytes(value, startIndex, 4)));
//    /// <summary>Returns a 64-bit signed integer converted from eight bytes at a specified position in a byte array.</summary>
//    public long ToInt64(byte[] value, int startIndex)
//      => FromBytes(value, startIndex, 8);
//    /// <summary>Returns a single-precision floating point number converted from four bytes at a specified position in a byte array.</summary>
//    public float ToSingle(byte[] value, int startIndex)
//      => new BitStruct(ToInt32(value, startIndex)).Single;
//    /// <summary>Returns a 16-bit unsigned integer converted from two bytes at a specified position in a byte array.</summary>
//    [System.CLSCompliant(false)]
//    public ushort ToUInt16(byte[] value, int startIndex)
//      => unchecked((ushort)(FromBytes(value, startIndex, 2)));
//    /// <summary>Returns a 32-bit unsigned integer converted from four bytes at a specified position in a byte array.</summary>
//    [System.CLSCompliant(false)]
//    public uint ToUInt32(byte[] value, int startIndex)
//      => unchecked((uint)(FromBytes(value, startIndex, 4)));
//    /// <summary>Returns a 64-bit unsigned integer converted from eight bytes at a specified position in a byte array.</summary>
//    [System.CLSCompliant(false)]
//    public ulong ToUInt64(byte[] value, int startIndex)
//      => unchecked((ulong)(FromBytes(value, startIndex, 8)));
//    #endregion

//    #region Static methods
//    /// <summary>Returns a string converted from the elements of a byte array.</summary>
//    public static string ToString(byte[] value)
//      => System.BitConverter.ToString(value);
//    /// <summary>Returns a string converted from the elements of a byte array starting at a specified array position.</summary>
//    public static string ToString(byte[] value, int startIndex)
//      => System.BitConverter.ToString(value, startIndex);
//    /// <summary>Returns a string converted from a specified number of bytes at a specified position in a byte array.</summary>
//    public static string ToString(byte[] value, int startIndex, int length)
//      => System.BitConverter.ToString(value, startIndex, length);
//    #endregion Static methods
//  }

//  //#endif
//}

////namespace Flux
////{
////  /// <summary>Equivalent of System.BitConverter, but with either endianness.</summary>
////  public abstract class BitConverter
////  {
////    /// <summary>Returns a little-endian bit converter instance. The same instance is always returned.</summary>
////    public static BitConverter LittleEndian
////      => new LittleEndianBitConverter();

////    /// <summary>Returns a big-endian bit converter instance. The same instance is always returned.</summary>
////    public static BitConverter BigEndian
////      => new BigEndianBitConverter();

////    #region CopyBytes
////    /// <summary>Copies the given number of bytes from the least-specific end of the specified value into the specified byte array, beginning at the specified index. This is used to implement the other CopyBytes methods.</summary>
////    /// <param name="value">The value to copy bytes for</param>
////    /// <param name="bytes">The number of significant bytes to copy</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    void CopyBytes(long value, int bytes, byte[] buffer, int index)
////    {
////      if (buffer is null) throw new System.ArgumentNullException(nameof(buffer), @"Byte array must not be null.");
////      if (buffer.Length < index + bytes) throw new System.ArgumentOutOfRangeException(nameof(buffer), @"Buffer not big enough for value.");
////      CopyBytesImpl(value, bytes, buffer, index);
////    }

////    /// <summary>Copies the given number of bytes from the least-specific end of the specified value into the specified byte array, beginning at the specified index. This must be implemented in concrete derived classes, but the implementation may assume that the value will fit into the buffer.</summary>
////    /// <param name="value">The value to copy bytes for</param>
////    /// <param name="bytes">The number of significant bytes to copy</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    protected abstract void CopyBytesImpl(long value, int bytes, byte[] buffer, int index);

////    /// <summary>Copies the specified Boolean value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">A Boolean value.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    public void CopyBytes(bool value, byte[] buffer, int index)
////      => CopyBytes(value ? 1 : 0, 1, buffer, index);
////    /// <summary>Copies the specified Unicode character value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">A character to convert.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    public void CopyBytes(char value, byte[] buffer, int index)
////      => CopyBytes(value, 2, buffer, index);
////    /// <summary>Copies the specified decimal value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">A character to convert.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    public void CopyBytes(decimal value, byte[] buffer, int index)
////    {
////      var bytes = decimal.GetBits(value);
////      for (var i = 0; i < 4; i++)
////        CopyBytesImpl(bytes[i], 4, buffer, i * 4 + index);
////    }
////    /// <summary>Copies the specified double-precision floating point value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    public void CopyBytes(double value, byte[] buffer, int index)
////      => CopyBytes(System.BitConverter.DoubleToInt64Bits(value), 8, buffer, index);
////    /// <summary>Copies the specified 16-bit signed integer value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    public void CopyBytes(short value, byte[] buffer, int index)
////      => CopyBytes(value, 2, buffer, index);
////    /// <summary>Copies the specified 32-bit signed integer value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    public void CopyBytes(int value, byte[] buffer, int index)
////      => CopyBytes(value, 4, buffer, index);
////    /// <summary>Copies the specified 64-bit signed integer value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    public void CopyBytes(long value, byte[] buffer, int index)
////      => CopyBytes(value, 8, buffer, index);
////    /// <summary>Copies the specified single-precision floating point value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    public void CopyBytes(float value, byte[] buffer, int index)
////      => CopyBytes(new BitStructure32(value).Integer32, 4, buffer, index);
////    /// <summary>Copies the specified 16-bit unsigned integer value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    internal void CopyBytes(ushort value, byte[] buffer, int index)
////      => CopyBytes(value, 2, buffer, index);
////    /// <summary>Copies the specified 32-bit unsigned integer value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    internal void CopyBytes(uint value, byte[] buffer, int index)
////      => CopyBytes(value, 4, buffer, index);
////    /// <summary>Copies the specified 64-bit unsigned integer value into the specified byte array, beginning at the specified index.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <param name="buffer">The byte array to copy the bytes into</param>
////    /// <param name="index">The first index into the array to copy the bytes into</param>
////    internal void CopyBytes(ulong value, byte[] buffer, int index)
////      => CopyBytes(unchecked((long)value), 8, buffer, index);
////    #endregion CopyBytes

////    #region GetBytes
////    /// <summary>Returns an array with the given number of bytes formed from the least significant bytes of the specified value. This is used to implement the other GetBytes methods.</summary>
////    /// <param name="value">The value to get bytes for</param>
////    /// <param name="bytes">The number of significant bytes to return</param>
////    private byte[] GetBytes(long value, int bytes)
////    {
////      var buffer = new byte[bytes];
////      CopyBytes(value, bytes, buffer, 0);
////      return buffer;
////    }

////    /// <summary>Returns the specified Boolean value as an array of bytes.</summary>
////    /// <param name="value">A Boolean value.</param>
////    /// <returns>An array of bytes with length 1.</returns>
////    public static byte[] GetBytes(bool value)
////      => System.BitConverter.GetBytes(value);
////    /// <summary>Returns the specified Unicode character value as an array of bytes.</summary>
////    /// <param name="value">A character to convert.</param>
////    /// <returns>An array of bytes with length 2.</returns>
////    public byte[] GetBytes(char value)
////      => GetBytes(value, 2);
////    /// <summary>Returns the specified decimal value as an array of bytes.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <returns>An array of bytes with length 16.</returns>
////    public byte[] GetBytes(decimal value)
////    {
////      var bytes = new byte[16];
////      var parts = decimal.GetBits(value);
////      for (var i = 0; i < 4; i++)
////        CopyBytesImpl(parts[i], 4, bytes, i * 4);
////      return bytes;
////    }
////    /// <summary>Returns the specified double-precision floating point value as an array of bytes.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <returns>An array of bytes with length 8.</returns>
////    public byte[] GetBytes(double value)
////      => GetBytes(new BitStructure64(value).Integer64, 8);
////    /// <summary>Returns the specified 16-bit signed integer value as an array of bytes.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <returns>An array of bytes with length 2.</returns>
////    public byte[] GetBytes(short value)
////      => GetBytes(value, 2);
////    /// <summary>Returns the specified 32-bit signed integer value as an array of bytes.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <returns>An array of bytes with length 4.</returns>
////    public byte[] GetBytes(int value)
////      => GetBytes(value, 4);
////    /// <summary>Returns the specified 64-bit signed integer value as an array of bytes.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <returns>An array of bytes with length 8.</returns>
////    public byte[] GetBytes(long value)
////      => GetBytes(value, 8);
////    /// <summary>Returns the specified single-precision floating point value as an array of bytes.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <returns>An array of bytes with length 4.</returns>
////    public byte[] GetBytes(float value)
////      => GetBytes(new BitStructure32(value).Integer32, 4);
////    /// <summary>Returns the specified 16-bit unsigned integer value as an array of bytes.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <returns>An array of bytes with length 2.</returns>
////    internal byte[] GetBytes(ushort value)
////      => GetBytes(value, 2);
////    /// <summary>Returns the specified 32-bit unsigned integer value as an array of bytes.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <returns>An array of bytes with length 4.</returns>
////    internal byte[] GetBytes(uint value)
////      => GetBytes(value, 4);
////    /// <summary>Returns the specified 64-bit unsigned integer value as an array of bytes.</summary>
////    /// <param name="value">The number to convert.</param>
////    /// <returns>An array of bytes with length 8.</returns>
////    internal byte[] GetBytes(ulong value)
////      => GetBytes(unchecked((long)value), 8);
////    #endregion GetBytes

////    #region ToString
////    /// <summary>Returns a String converted from the elements of a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <remarks>All the elements of value are converted.</remarks>
////    /// <returns>
////    /// A String of hexadecimal pairs separated by hyphens, where each pair 
////    /// represents the corresponding element in value; for example, "7F-2C-4A".
////    /// </returns>
////    public static string ToString(byte[] value)
////      => System.BitConverter.ToString(value);
////    /// <summary>Returns a String converted from the elements of a byte array starting at a specified array position.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <remarks>The elements from array position startIndex to the end of the array are converted.</remarks>
////    /// <returns>
////    /// A String of hexadecimal pairs separated by hyphens, where each pair 
////    /// represents the corresponding element in value; for example, "7F-2C-4A".
////    /// </returns>
////    public static string ToString(byte[] value, int startIndex)
////      => System.BitConverter.ToString(value, startIndex);
////    /// <summary>Returns a String converted from a specified number of bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <param name="length">The number of bytes to convert.</param>
////    /// <remarks>The length elements from array position startIndex are converted.</remarks>
////    /// <returns>
////    /// A String of hexadecimal pairs separated by hyphens, where each pair 
////    /// represents the corresponding element in value; for example, "7F-2C-4A".
////    /// </returns>
////    public static string ToString(byte[] value, int startIndex, int length)
////      => System.BitConverter.ToString(value, startIndex, length);
////    #endregion ToString

////    #region ToType
////    /// <summary>Checks the arguments for validity before calling FromBytes (which can therefore assume the arguments are valid).</summary>
////    /// <param name="value">The bytes to convert after checking</param>
////    /// <param name="startIndex">The index of the first byte to convert</param>
////    /// <param name="bytesToConvert">The number of bytes to convert</param>
////    /// <returns></returns>
////    long CheckedFromBytes(byte[] value, int startIndex, int bytesToConvert)
////    {
////      if (value is null) throw new System.ArgumentNullException(nameof(value));
////      if (startIndex < 0 || startIndex > value.Length - bytesToConvert) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
////      return FromBytes(value, startIndex, bytesToConvert);
////    }

////    /// <summary>Convert the given number of bytes from the given array, from the given start position, into a long, using the bytes as the least significant part of the long. By the time this is called, the arguments have been checked for validity.</summary>
////    /// <param name="value">The bytes to convert</param>
////    /// <param name="startIndex">The index of the first byte to convert</param>
////    /// <param name="bytesToConvert">The number of bytes to use in the conversion</param>
////    /// <returns>The converted number</returns>
////    protected abstract long FromBytes(byte[] value, int startIndex, int bytesToConvert);


////    /// <summary>Returns a Boolean value converted from one byte at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>true if the byte at startIndex in value is nonzero; otherwise, false.</returns>
////    //public static bool ToBoolean(byte[] value, int startIndex)
////    //{
////    //  if (value is null) throw new System.ArgumentNullException(nameof(value));
////    //  if (startIndex < 0 || startIndex > value.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
////    //  return System.BitConverter.ToBoolean(value, startIndex);
////    //}
////    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
////    public bool ToBoolean(byte[] value, int startIndex)
////      => System.BitConverter.ToBoolean(value, startIndex);
////    /// <summary>Returns a Unicode character converted from two bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>A character formed by two bytes beginning at startIndex.</returns>
////    public char ToChar(byte[] value, int startIndex)
////      => unchecked((char)(CheckedFromBytes(value, startIndex, 2)));
////    /// <summary>Returns a decimal value converted from sixteen bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>A decimal  formed by sixteen bytes beginning at startIndex.</returns>
////    public decimal ToDecimal(byte[] value, int startIndex)
////    {
////      // HACK: This always assumes four parts, each in their own endianness, starting with the first part at the start of the byte array.
////      // On the other hand, there's no real format specified...
////      var bytes = new int[4];
////      for (int i = 0; i < 4; i++)
////        bytes[i] = ToInt32(value, startIndex + i * 4);
////      return new System.Decimal(bytes);
////    }
////    /// <summary>Returns a double-precision floating point number converted from eight bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>A double precision floating point number formed by eight bytes beginning at startIndex.</returns>
////    public double ToDouble(byte[] value, int startIndex)
////      => System.BitConverter.Int64BitsToDouble(ToInt64(value, startIndex));
////    /// <summary>Returns a 16-bit signed integer converted from two bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>A 16-bit signed integer formed by two bytes beginning at startIndex.</returns>
////    public short ToInt16(byte[] value, int startIndex)
////      => unchecked((short)(CheckedFromBytes(value, startIndex, 2)));
////    /// <summary>Returns a 32-bit signed integer converted from four bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>A 32-bit signed integer formed by four bytes beginning at startIndex.</returns>
////    public int ToInt32(byte[] value, int startIndex)
////      => unchecked((int)(CheckedFromBytes(value, startIndex, 4)));
////    /// <summary>Returns a 64-bit signed integer converted from eight bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>A 64-bit signed integer formed by eight bytes beginning at startIndex.</returns>
////    public long ToInt64(byte[] value, int startIndex)
////      => CheckedFromBytes(value, startIndex, 8);
////    /// <summary>Returns a single-precision floating point number converted from four bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>A single precision floating point number formed by four bytes beginning at startIndex.</returns>
////    public float ToSingle(byte[] value, int startIndex)
////      => new BitStructure32(ToInt32(value, startIndex)).FloatingPoint32;
////    /// <summary>Returns a 16-bit unsigned integer converted from two bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>A 16-bit unsigned integer formed by two bytes beginning at startIndex.</returns>
////    internal ushort ToUInt16(byte[] value, int startIndex)
////      => unchecked((ushort)(CheckedFromBytes(value, startIndex, 2)));
////    /// <summary>Returns a 32-bit unsigned integer converted from four bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>A 32-bit unsigned integer formed by four bytes beginning at startIndex.</returns>
////    internal uint ToUInt32(byte[] value, int startIndex)
////      => unchecked((uint)(CheckedFromBytes(value, startIndex, 4)));
////    /// <summary>Returns a 64-bit unsigned integer converted from eight bytes at a specified position in a byte array.</summary>
////    /// <param name="value">An array of bytes.</param>
////    /// <param name="startIndex">The starting position within value.</param>
////    /// <returns>A 64-bit unsigned integer formed by eight bytes beginning at startIndex.</returns>
////    internal ulong ToUInt64(byte[] value, int startIndex)
////      => unchecked((ulong)(CheckedFromBytes(value, startIndex, 8)));
////    #endregion ToType

////    /// <summary>Implementation of EndianBitConverter which converts to/from big-endian byte arrays.</summary>
////    private sealed class BigEndianBitConverter
////      : BitConverter
////    {
////      /// <summary>Copies the specified number of bytes from value to buffer, starting at index.</summary>
////      /// <param name="value">The value to copy</param>
////      /// <param name="bytes">The number of bytes to copy</param>
////      /// <param name="buffer">The buffer to copy the bytes into</param>
////      /// <param name="index">The index to start at</param>
////      protected override void CopyBytesImpl(long value, int bytes, byte[] buffer, int index)
////      {
////        var endOffset = index + bytes - 1;
////        for (int i = 0; i < bytes; i++)
////        {
////          buffer[endOffset - i] = unchecked((byte)(value & 0xff));
////          value >>= 8;
////        }
////      }

////      /// <summary>Returns a value built from the specified number of bytes from the given buffer, starting at index.</summary>
////      /// <param name="buffer">The data in byte array format</param>
////      /// <param name="startIndex">The first index to use</param>
////      /// <param name="bytesToConvert">The number of bytes to use</param>
////      /// <returns>The value built from the given bytes</returns>
////      protected override long FromBytes(byte[] buffer, int startIndex, int bytesToConvert)
////      {
////        var value = 0L;
////        for (var i = 0; i < bytesToConvert; i++)
////          value = unchecked((value << 8) | buffer[startIndex + i]);
////        return value;
////      }
////    }

////    /// <summary>Implementation of EndianBitConverter which converts to/from little-endian byte arrays.</summary>
////    private sealed class LittleEndianBitConverter
////      : BitConverter
////    {
////      /// <summary>Copies the specified number of bytes from value to buffer, starting at index.</summary>
////      /// <param name="value">The value to copy</param>
////      /// <param name="bytes">The number of bytes to copy</param>
////      /// <param name="buffer">The buffer to copy the bytes into</param>
////      /// <param name="index">The index to start at</param>
////      protected override void CopyBytesImpl(long value, int bytes, byte[] buffer, int index)
////      {
////        for (var i = 0; i < bytes; i++)
////        {
////          buffer[i + index] = unchecked((byte)(value & 0xff));
////          value >>= 8;
////        }
////      }

////      /// <summary>Returns a value built from the specified number of bytes from the given buffer, starting at index.</summary>
////      /// <param name="buffer">The data in byte array format</param>
////      /// <param name="startIndex">The first index to use</param>
////      /// <param name="bytesToConvert">The number of bytes to use</param>
////      /// <returns>The value built from the given bytes</returns>
////      protected override long FromBytes(byte[] buffer, int startIndex, int bytesToConvert)
////      {
////        var value = 0L;
////        for (var i = 0; i < bytesToConvert; i++)
////          value = unchecked((value << 8) | buffer[startIndex + bytesToConvert - 1 - i]);
////        return value;
////      }
////    }
////  }
////}
