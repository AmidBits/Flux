namespace Flux.IO
{
  /// <summary>
  /// Equivalent of System.IO.BinaryWriter, but with either endianness, depending on
  /// the EndianBitConverter it is constructed with.
  /// </summary>
  public sealed class BinaryWriter
    : Disposable
  {
    /// <summary>
    /// Buffer used for temporary storage during conversion from primitives
    /// </summary>
    private readonly byte[] m_buffer = new byte[16];
    /// <summary>
    /// Buffer used for Write(char)
    /// </summary>
    private readonly char[] m_charBuffer = new char[1];

    /// <summary>
    /// Constructs a new binary writer with the given bit converter, writing
    /// to the given stream, using UTF-8 encoding.
    /// </summary>
    /// <param name="bitConverter">Converter to use when writing data</param>
    /// <param name="stream">Stream to write data to</param>
    public BinaryWriter(BitConverter bitConverter, System.IO.Stream stream)
      : this(bitConverter, stream, System.Text.Encoding.UTF8)
    { }
    /// <summary>
    /// Constructs a new binary writer with the given bit converter, writing
    /// to the given stream, using the given encoding.
    /// </summary>
    /// <param name="bitConverter">Converter to use when writing data</param>
    /// <param name="stream">Stream to write data to</param>
    /// <param name="encoding">Encoding to use when writing character data</param>
    public BinaryWriter(BitConverter bitConverter, System.IO.Stream stream, System.Text.Encoding encoding)
    {
      System.ArgumentNullException.ThrowIfNull(bitConverter);
      System.ArgumentNullException.ThrowIfNull(stream);
      System.ArgumentNullException.ThrowIfNull(encoding);

      m_stream = stream;
      m_bitConverter = bitConverter;
      m_encoding = encoding;

      if (!stream.CanWrite) throw new System.ArgumentException("Stream isn't writable", nameof(stream));
    }

    private readonly BitConverter m_bitConverter;
    /// <summary>The bit converter used to write values to the stream.</summary>
    public BitConverter BitConverter
      => m_bitConverter;

    private readonly System.Text.Encoding m_encoding;
    /// <summary>The encoding used to write strings.</summary>
    public System.Text.Encoding Encoding
      => m_encoding;

    private readonly System.IO.Stream m_stream;
    /// <summary>Gets the underlying stream of the BinaryWriter.</summary>
    public System.IO.Stream BaseStream
      => m_stream;

    /// <summary>Closes the writer, including the underlying stream.</summary>
    public void Close()
    {
      Dispose();
    }
    /// <summary>Flushes the underlying stream.</summary>
    public void Flush()
    {
      CheckDisposed();
      m_stream.Flush();
    }
    /// <summary>Seeks within the stream.</summary>
    /// <param name="offset">Offset to seek to.</param>
    /// <param name="origin">Origin of seek operation.</param>
    public void Seek(int offset, System.IO.SeekOrigin origin)
    {
      CheckDisposed();
      m_stream.Seek(offset, origin);
    }
    /// <summary>Writes a boolean value to the stream. 1 byte is written.</summary>
    public void Write(bool value)
    {
      m_bitConverter.CopyBytes(value, m_buffer, 0);
      WriteInternal(m_buffer, 1);
    }
    /// <summary>Writes a 16-bit signed integer to the stream, using the bit converter for this writer. 2 bytes are written.</summary>
    public void Write(short value)
    {
      m_bitConverter.CopyBytes(value, m_buffer, 0);
      WriteInternal(m_buffer, 2);
    }
    /// <summary>Writes a 32-bit signed integer to the stream, using the bit converter for this writer. 4 bytes are written.</summary>
    public void Write(int value)
    {
      m_bitConverter.CopyBytes(value, m_buffer, 0);
      WriteInternal(m_buffer, 4);
    }
    /// <summary>Writes a 64-bit signed integer to the stream, using the bit converter for this writer. 8 bytes are written.</summary>
    public void Write(long value)
    {
      m_bitConverter.CopyBytes(value, m_buffer, 0);
      WriteInternal(m_buffer, 8);
    }
    /// <summary>Writes a 16-bit unsigned integer to the stream, using the bit converter for this writer. 2 bytes are written.</summary>
    [System.CLSCompliant(false)]
    public void Write(ushort value)
    {
      m_bitConverter.CopyBytes(value, m_buffer, 0);
      WriteInternal(m_buffer, 2);
    }
    /// <summary>Writes a 32-bit unsigned integer to the stream, using the bit converter for this writer. 4 bytes are written.</summary>
    [System.CLSCompliant(false)]
    public void Write(uint value)
    {
      m_bitConverter.CopyBytes(value, m_buffer, 0);
      WriteInternal(m_buffer, 4);
    }
    /// <summary>Writes a 64-bit unsigned integer to the stream, using the bit converter for this writer. 8 bytes are written.</summary>
    [System.CLSCompliant(false)]
    public void Write(ulong value)
    {
      m_bitConverter.CopyBytes(value, m_buffer, 0);
      WriteInternal(m_buffer, 8);
    }
    /// <summary>Writes a single-precision floating-point value to the stream, using the bit converter for this writer. 4 bytes are written.</summary>
    public void Write(float value)
    {
      m_bitConverter.CopyBytes(value, m_buffer, 0);
      WriteInternal(m_buffer, 4);
    }
    /// <summary>Writes a double-precision floating-point value to the stream, using the bit converter for this writer. 8 bytes are written.</summary>
    public void Write(double value)
    {
      m_bitConverter.CopyBytes(value, m_buffer, 0);
      WriteInternal(m_buffer, 8);
    }
    /// <summary>Writes a decimal value to the stream, using the bit converter for this writer. 16 bytes are written.</summary>
    public void Write(decimal value)
    {
      m_bitConverter.CopyBytes(value, m_buffer, 0);
      WriteInternal(m_buffer, 16);
    }
    /// <summary>Writes a signed byte to the stream.</summary>
    public void Write(byte value)
    {
      m_buffer[0] = value;
      WriteInternal(m_buffer, 1);
    }
    /// <summary>Writes an unsigned byte to the stream.</summary>
    [System.CLSCompliant(false)]
    public void Write(sbyte value)
    {
      m_buffer[0] = unchecked((byte)value);
      WriteInternal(m_buffer, 1);
    }
    /// <summary>Writes an array of bytes to the stream.</summary>
    public void Write(byte[] value)
    {
      if (value is null) throw new System.ArgumentNullException(nameof(value));

      WriteInternal(value, value.Length);
    }
    /// <summary>Writes a portion of an array of bytes to the stream.</summary>
    /// <param name="value">An array containing the bytes to write</param>
    /// <param name="offset">The index of the first byte to write within the array</param>
    /// <param name="count">The number of bytes to write</param>
    public void Write(byte[] value, int offset, int count)
    {
      CheckDisposed();
      m_stream.Write(value, offset, count);
    }
    /// <summary>Writes a single character to the stream, using the encoding for this writer.</summary>
    public void Write(char value)
    {
      m_charBuffer[0] = value;
      Write(m_charBuffer);
    }
    /// <summary>Writes an array of characters to the stream, using the encoding for this writer.</summary>
    public void Write(char[] value)
    {
      if (value is null) throw new System.ArgumentNullException(nameof(value));

      CheckDisposed();
      byte[] data = Encoding.GetBytes(value, 0, value.Length);
      WriteInternal(data, data.Length);
    }
    /// <summary>
    /// Writes a string to the stream, using the encoding for this writer.
    /// </summary>
    /// <param name="value">The value to write. Must not be null.</param>
    /// <exception cref="ArgumentNullException">value is null</exception>
    public void Write(string value)
    {
      if (value == null) throw new System.ArgumentNullException(nameof(value));

      CheckDisposed();
      byte[] data = Encoding.GetBytes(value);
      Write7BitEncodedInt(data.Length);
      WriteInternal(data, data.Length);
    }
    /// <summary>Writes a 7-bit encoded integer from the stream. This is stored with the least significant information first, with 7 bits of information per byte of value, and the top bit as a continuation flag.</summary>
    public void Write7BitEncodedInt(int value)
    {
      CheckDisposed();
      if (value < 0) throw new System.ArgumentOutOfRangeException(nameof(value), "Value must be greater than or equal to 0.");
      int index = 0;
      while (value >= 128)
      {
        m_buffer[index++] = (byte)((value & 0x7f) | 0x80);
        value >>= 7;
        index++;
      }
      m_buffer[index++] = (byte)value;
      m_stream.Write(m_buffer, 0, index);
    }

    /// <summary>Checks whether or not the writer has been disposed, throwing an exception if so.</summary>
    private void CheckDisposed()
    {
      if (IsDisposed) throw new System.ObjectDisposedException("EndianBinaryWriter");
    }

    /// <summary>Writes the specified number of bytes from the start of the given byte array, after checking whether or not the writer has been disposed.</summary>
    /// <param name="bytes">The array of bytes to write from</param>
    /// <param name="length">The number of bytes to write</param>
    private void WriteInternal(byte[] bytes, int length)
    {
      CheckDisposed();
      m_stream.Write(bytes, 0, length);
    }

    protected override void DisposeManaged()
    {
      Flush();

      ((System.IDisposable)m_stream).Dispose();

      base.DisposeManaged();
    }
  }
}
