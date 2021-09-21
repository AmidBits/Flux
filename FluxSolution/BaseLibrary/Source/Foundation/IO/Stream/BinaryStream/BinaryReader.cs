namespace Flux.IO
{
  /// <summary>
  /// Equivalent of System.IO.BinaryReader, but with either endianness, depending on
  /// the EndianBitConverter it is constructed with. No data is buffered in the
  /// reader; the client may seek within the stream at will.
  /// </summary>
  public sealed class BinaryReader
    : System.IDisposable
  {
    /// <summary>Whether or not this reader has been disposed yet.</summary>
    private bool m_disposed = false;
    /// <summary>Decoder to use for string conversions.</summary>
    private readonly System.Text.Decoder m_decoder;
    /// <summary>Buffer used for temporary storage before conversion into primitives.</summary>
    private readonly byte[] m_buffer = new byte[16];
    /// <summary>Buffer used for temporary storage when reading a single character.</summary>
    private readonly char[] m_charBuffer = new char[1];
    /// <summary>Minimum number of bytes used to encode a character.</summary>
    private readonly int m_minBytesPerChar;

    /// <summary>Equivalent of System.IO.BinaryWriter, but with either endianness, depending on the EndianBitConverter it is constructed with.</summary>
    /// <param name="bitConverter">Converter to use when reading data</param>
    /// <param name="stream">Stream to read data from</param>
    public BinaryReader(BitConverter bitConverter, System.IO.Stream stream)
      : this(bitConverter, stream, System.Text.Encoding.UTF8)
    { }

    /// <summary>
    /// Constructs a new binary reader with the given bit converter, reading
    /// to the given stream, using the given encoding.
    /// </summary>
    /// <param name="bitConverter">Converter to use when reading data</param>
    /// <param name="stream">Stream to read data from</param>
    /// <param name="encoding">Encoding to use when reading character data</param>
    public BinaryReader(BitConverter bitConverter, System.IO.Stream stream, System.Text.Encoding encoding)
    {
      if (stream == null) throw new System.ArgumentNullException(nameof(stream));
      if (!stream.CanRead) throw new System.ArgumentException("Stream isn't writable.", nameof(stream));

      m_stream = stream;
      m_bitConverter = bitConverter ?? throw new System.ArgumentNullException(nameof(bitConverter));
      m_encoding = encoding ?? throw new System.ArgumentNullException(nameof(encoding));
      m_decoder = encoding.GetDecoder();
      m_minBytesPerChar = encoding is System.Text.UnicodeEncoding ? 2 : 1;
    }

    private readonly BitConverter m_bitConverter;
    /// <summary>The bit converter used to read values from the stream.</summary>
    public BitConverter BitConverter
      => m_bitConverter;

    private readonly System.Text.Encoding m_encoding;
    /// <summary>The encoding used to read strings.</summary>
    public System.Text.Encoding Encoding
      => m_encoding;

    private readonly System.IO.Stream m_stream;
    /// <summary>Gets the underlying stream of the EndianBinaryReader.</summary>
    public System.IO.Stream BaseStream
      => m_stream;

    /// <summary>
    /// Closes the reader, including the underlying stream..
    /// </summary>
    public void Close()
    {
      Dispose();
    }
    /// <summary>
    /// Seeks within the stream.
    /// </summary>
    /// <param name="offset">Offset to seek to.</param>
    /// <param name="origin">Origin of seek operation.</param>
    public void Seek(int offset, System.IO.SeekOrigin origin)
    {
      CheckDisposed();
      m_stream.Seek(offset, origin);
    }
    /// <summary>
    /// Reads a single byte from the stream.
    /// </summary>
    /// <returns>The byte read</returns>
    public byte ReadByte()
    {
      ReadInternal(m_buffer, 1);
      return m_buffer[0];
    }
    /// <summary>
    /// Reads a single signed byte from the stream.
    /// </summary>
    /// <returns>The byte read</returns>
    [System.CLSCompliant(false)]
    public sbyte ReadSByte()
    {
      ReadInternal(m_buffer, 1);
      return unchecked((sbyte)m_buffer[0]);
    }
    /// <summary>Reads a boolean from the stream. 1 byte is read.</summary>
    public bool ReadBoolean()
    {
      ReadInternal(m_buffer, 1);
      return m_bitConverter.ToBoolean(m_buffer, 0);
    }
    /// <summary>Reads a 16-bit signed integer from the stream, using the bit converter for this reader. 2 bytes are read.</summary>
    public short ReadInt16()
    {
      ReadInternal(m_buffer, 2);
      return m_bitConverter.ToInt16(m_buffer, 0);
    }
    /// <summary>Reads a 32-bit signed integer from the stream, using the bit converter for this reader. 4 bytes are read.</summary>
    public int ReadInt32()
    {
      ReadInternal(m_buffer, 4);
      return m_bitConverter.ToInt32(m_buffer, 0);
    }
    /// <summary>Reads a 64-bit signed integer from the stream, using the bit converter for this reader. 8 bytes are read.</summary>
    public long ReadInt64()
    {
      ReadInternal(m_buffer, 8);
      return m_bitConverter.ToInt64(m_buffer, 0);
    }
    /// <summary>Reads a 16-bit unsigned integer from the stream, using the bit converter for this reader. 2 bytes are read.</summary>
    [System.CLSCompliant(false)]
    public ushort ReadUInt16()
    {
      ReadInternal(m_buffer, 2);
      return m_bitConverter.ToUInt16(m_buffer, 0);
    }
    /// <summary>Reads a 32-bit unsigned integer from the stream, using the bit converter for this reader. 4 bytes are read.</summary>
    [System.CLSCompliant(false)]
    public uint ReadUInt32()
    {
      ReadInternal(m_buffer, 4);
      return m_bitConverter.ToUInt32(m_buffer, 0);
    }
    /// <summary>Reads a 64-bit unsigned integer from the stream, using the bit converter for this reader. 8 bytes are read.</summary>
    [System.CLSCompliant(false)]
    public ulong ReadUInt64()
    {
      ReadInternal(m_buffer, 8);
      return m_bitConverter.ToUInt64(m_buffer, 0);
    }
    /// <summary>Reads a single-precision floating-point value from the stream, using the bit converter for this reader. 4 bytes are read.</summary>
    public float ReadSingle()
    {
      ReadInternal(m_buffer, 4);
      return m_bitConverter.ToSingle(m_buffer, 0);
    }
    /// <summary>Reads a double-precision floating-point value from the stream, using the bit converter for this reader. 8 bytes are read.</summary>
    public double ReadDouble()
    {
      ReadInternal(m_buffer, 8);
      return m_bitConverter.ToDouble(m_buffer, 0);
    }
    /// <summary>Reads a decimal value from the stream, using the bit converter for this reader. 16 bytes are read.</summary>
    public decimal ReadDecimal()
    {
      ReadInternal(m_buffer, 16);
      return m_bitConverter.ToDecimal(m_buffer, 0);
    }
    /// <summary>
    /// Reads a single character from the stream, using the character encoding for
    /// this reader. If no characters have been fully read by the time the stream ends,
    /// -1 is returned.
    /// </summary>
    /// <returns>The character read, or -1 for end of stream.</returns>
    public int Read()
    {
      int charsRead = Read(m_charBuffer, 0, 1);
      if (charsRead == 0)
      {
        return -1;
      }
      else
      {
        return m_charBuffer[0];
      }
    }
    /// <summary>
    /// Reads the specified number of characters into the given buffer, starting at
    /// the given index.
    /// </summary>
    /// <param name="data">The buffer to copy data into</param>
    /// <param name="index">The first index to copy data into</param>
    /// <param name="count">The number of characters to read</param>
    /// <returns>The number of characters actually read. This will only be less than
    /// the requested number of characters if the end of the stream is reached.
    /// </returns>
    public int Read(char[] data, int index, int count)
    {
      CheckDisposed();

      if (m_buffer == null) throw new System.NullReferenceException(nameof(m_buffer));
      if (index < 0) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));
      if (count + index > data.Length) throw new System.ArgumentException("Not enough space in buffer for specified number of characters starting at specified index.");

      int read = 0;
      bool firstTime = true;

      // Use the normal buffer if we're only reading a small amount, otherwise
      // use at most 4K at a time.
      byte[] byteBuffer = m_buffer;

      if (byteBuffer.Length < count * m_minBytesPerChar)
      {
        byteBuffer = new byte[4096];
      }

      while (read < count)
      {
        int amountToRead;
        // First time through we know we haven't previously read any data
        if (firstTime)
        {
          amountToRead = count * m_minBytesPerChar;
          firstTime = false;
        }
        // After that we can only assume we need to fully read "chars left -1" characters
        // and a single byte of the character we may be in the middle of
        else
        {
          amountToRead = ((count - read - 1) * m_minBytesPerChar) + 1;
        }
        if (amountToRead > byteBuffer.Length)
        {
          amountToRead = byteBuffer.Length;
        }
        int bytesRead = TryReadInternal(byteBuffer, amountToRead);
        if (bytesRead == 0)
        {
          return read;
        }
        int decoded = m_decoder.GetChars(byteBuffer, 0, bytesRead, data, index);
        read += decoded;
        index += decoded;
      }
      return read;
    }
    /// <summary>
    /// Reads the specified number of bytes into the given buffer, starting at
    /// the given index.
    /// </summary>
    /// <param name="buffer">The buffer to copy data into</param>
    /// <param name="index">The first index to copy data into</param>
    /// <param name="count">The number of bytes to read</param>
    /// <returns>The number of bytes actually read. This will only be less than
    /// the requested number of bytes if the end of the stream is reached.
    /// </returns>
    public int Read(byte[] buffer, int index, int count)
    {
      CheckDisposed();

      if (buffer == null) throw new System.ArgumentNullException(nameof(buffer));
      if (index < 0) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (count + index > buffer.Length) throw new System.ArgumentException("Not enough space in buffer for specified number of bytes starting at specified index.");

      int read = 0;
      while (count > 0)
      {
        int block = m_stream.Read(buffer, index, count);
        if (block == 0)
        {
          return read;
        }
        index += block;
        read += block;
        count -= block;
      }
      return read;
    }
    /// <summary>
    /// Reads the specified number of bytes, returning them in a new byte array.
    /// If not enough bytes are available before the end of the stream, this
    /// method will return what is available.
    /// </summary>
    /// <param name="count">The number of bytes to read</param>
    /// <returns>The bytes read</returns>
    public byte[] ReadBytes(int count)
    {
      CheckDisposed();

      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      byte[] ret = new byte[count];
      int index = 0;
      while (index < count)
      {
        int read = m_stream.Read(ret, index, count - index);
        // Stream has finished half way through. That's fine, return what we've got.
        if (read == 0)
        {
          byte[] copy = new byte[index];
          System.Buffer.BlockCopy(ret, 0, copy, 0, index);
          return copy;
        }
        index += read;
      }
      return ret;
    }
    /// <summary>
    /// Reads the specified number of bytes, returning them in a new byte array.
    /// If not enough bytes are available before the end of the stream, this
    /// method will throw an IOException.
    /// </summary>
    /// <param name="count">The number of bytes to read</param>
    /// <returns>The bytes read</returns>
    public byte[] ReadBytesOrThrow(int count)
    {
      byte[] ret = new byte[count];
      ReadInternal(ret, count);
      return ret;
    }
    /// <summary>
    /// Reads a 7-bit encoded integer from the stream. This is stored with the least significant
    /// information first, with 7 bits of information per byte of value, and the top
    /// bit as a continuation flag. This method is not affected by the endianness
    /// of the bit converter.
    /// </summary>
    /// <returns>The 7-bit encoded integer read from the stream.</returns>
    public int Read7BitEncodedInt()
    {
      CheckDisposed();

      int ret = 0;
      for (int shift = 0; shift < 35; shift += 7)
      {
        int b = m_stream.ReadByte();
        if (b == -1)
        {
          throw new System.IO.EndOfStreamException();
        }
        ret |= (b & 0x7f) << shift;
        if ((b & 0x80) == 0)
        {
          return ret;
        }
      }
      // Still haven't seen a byte with the high bit unset? Dodgy data.
      throw new System.IO.IOException("Invalid 7-bit encoded integer in stream.");
    }
    /// <summary>
    /// Reads a 7-bit encoded integer from the stream. This is stored with the most significant
    /// information first, with 7 bits of information per byte of value, and the top
    /// bit as a continuation flag. This method is not affected by the endianness
    /// of the bit converter.
    /// </summary>
    /// <returns>The 7-bit encoded integer read from the stream.</returns>
    public int ReadBigEndian7BitEncodedInt()
    {
      CheckDisposed();

      int ret = 0;
      for (int i = 0; i < 5; i++)
      {
        int b = m_stream.ReadByte();
        if (b == -1)
        {
          throw new System.IO.EndOfStreamException();
        }
        ret = (ret << 7) | (b & 0x7f);
        if ((b & 0x80) == 0)
        {
          return ret;
        }
      }
      // Still haven't seen a byte with the high bit unset? Dodgy data.
      throw new System.IO.IOException("Invalid 7-bit encoded integer in stream.");
    }
    /// <summary>
    /// Reads a length-prefixed string from the stream, using the encoding for this reader.
    /// A 7-bit encoded integer is first read, which specifies the number of bytes 
    /// to read from the stream. These bytes are then converted into a string with
    /// the encoding for this reader.
    /// </summary>
    /// <returns>The string read from the stream.</returns>
    public string ReadString()
    {
      int bytesToRead = Read7BitEncodedInt();

      byte[] data = new byte[bytesToRead];
      ReadInternal(data, bytesToRead);
      return m_encoding.GetString(data, 0, data.Length);
    }

    /// <summary>
    /// Checks whether or not the reader has been disposed, throwing an exception if so.
    /// </summary>
    private void CheckDisposed()
    {
      if (m_disposed) throw new System.ObjectDisposedException(nameof(BinaryReader));
    }

    /// <summary>
    /// Reads the given number of bytes from the stream, throwing an exception
    /// if they can't all be read.
    /// </summary>
    /// <param name="data">Buffer to read into</param>
    /// <param name="size">Number of bytes to read</param>
    private void ReadInternal(byte[] data, int size)
    {
      CheckDisposed();
      int index = 0;
      while (index < size)
      {
        int read = m_stream.Read(data, index, size - index);
        if (read == 0)
        {
          throw new System.IO.EndOfStreamException(System.String.Format("End of stream reached with {0} byte{1} left to read.", size - index,
            size - index == 1 ? "s" : ""));
        }
        index += read;
      }
    }
    /// <summary>
    /// Reads the given number of bytes from the stream if possible, returning
    /// the number of bytes actually read, which may be less than requested if
    /// (and only if) the end of the stream is reached.
    /// </summary>
    /// <param name="data">Buffer to read into</param>
    /// <param name="size">Number of bytes to read</param>
    /// <returns>Number of bytes actually read</returns>
    private int TryReadInternal(byte[] data, int size)
    {
      CheckDisposed();
      int index = 0;
      while (index < size)
      {
        int read = m_stream.Read(data, index, size - index);
        if (read == 0)
        {
          return index;
        }
        index += read;
      }
      return index;
    }

    /// <summary>
    /// Disposes of the underlying stream.
    /// </summary>
    public void Dispose()
    {
      if (!m_disposed)
      {
        m_disposed = true;
        ((System.IDisposable)m_stream).Dispose();
      }
    }
  }
}
