namespace Flux.IO.BitStream
{
  public sealed class BitStreamReader
    : System.IO.Stream
  {
    private readonly System.IO.Stream m_baseStream;

    private int m_bitCount;
    private ulong m_bitField;

    public BitStreamReader(System.IO.Stream baseStream)
    {
      m_baseStream = baseStream;

      LoadBytes();
    }

    /// <summary>The number of bits currently stored in the bit buffer.</summary>
    public int BitCount => m_bitCount;

    /// <summary>The intermediate 64-bit storage buffer.</summary>
    public long BitField => unchecked((long)(m_bitField & BitOps.CreateBitMaskRight((ulong)m_bitCount)));

    /// <summary>The total number of bits read through the bit stream.</summary>
    public int TotalReadBits { get; private set; }

    private int m_lastReadByte = 0;

    /// <summary>
    /// <para>Fill the internal buffer with as many bits as possible (not necessarily 64 bits).</para>
    /// </summary>
    /// <remarks>Less than 64 bits does not automatically mean end-of-stream.</remarks>
    /// <returns>The current number of bits in the internal buffer.</returns>
    private int FillBuffer()
    {
      while (m_bitCount <= 56) // Load up with data from the base-stream.
      {
        m_lastReadByte = m_baseStream.ReadByte();

        if (m_lastReadByte == -1)
          break;

        m_bitCount += 8;
        m_bitField = (m_bitField << 8) | (byte)m_lastReadByte;
      }

      return m_bitCount;
    }

    /// <summary>
    /// <para>Read a number of bits from the underlying stream.</para>
    /// </summary>
    /// <param name="bitCount"></param>
    /// <param name="actualBitCount"></param>
    /// <returns></returns>
    public System.Numerics.BigInteger ReadBits(int bitCount, out int actualBitCount)
    {
      var bits = System.Numerics.BigInteger.Zero;

      actualBitCount = 0;

      while (actualBitCount < bitCount)
      {
        var neededBitCount = bitCount - actualBitCount;

        if (neededBitCount == 0)
          break;

        var bufferBitCount = FillBuffer();

        var movingBitCount = int.Min(neededBitCount, bufferBitCount);

        var mask = BitOps.CreateBitMaskRight((ulong)movingBitCount);

        bits <<= movingBitCount; // Make room for moving bits.

        m_bitCount -= movingBitCount;

        bits &= (m_bitField >> m_bitCount) & mask; // And in moving bits.
      }

      TotalReadBits += actualBitCount;

      return bits;
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    private void LoadBytes()
    {
      while (m_bitCount <= 56) // Load up with data from the base-stream.
      {
        m_lastReadByte = m_baseStream.ReadByte();

        if (m_lastReadByte == -1)
          break;

        m_bitCount += 8;
        m_bitField = (m_bitField << 8) | (byte)m_lastReadByte;
      }
    }

    /// <summary>
    /// <para>Returns <paramref name="bitCount"/> bits and returns the number of <paramref name="actualBitCount"/> as an out parameter.</para>
    /// </summary>
    /// <param name="bitCount"></param>
    /// <returns>The number of bits actually read.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    private long GetBits(int bitCount, out int actualBitCount)
    {
      if (m_bitCount < bitCount) LoadBytes();

      actualBitCount = int.Min(bitCount, m_bitCount); // Can only present as many bits as available in base-stream.

      m_bitCount -= actualBitCount;

      var bitField = unchecked((long)((m_bitField >> m_bitCount) & BitOps.CreateBitMaskRight((ulong)actualBitCount)));

      TotalReadBits += actualBitCount;

      return bitField;
    }

    public int ReadBitsInt32(int bitCount, out int actualBitCount)
      => unchecked((int)ReadBitsUInt32(bitCount, out actualBitCount));
    public long ReadBitsInt64(int bitCount, out int actualBitCount)
      => unchecked((long)ReadBitsUInt64(bitCount, out actualBitCount));

    [System.CLSCompliant(false)]
    public uint ReadBitsUInt32(int bitCount, out int actualBitCount)
    {
      if (bitCount < 0 || bitCount > 32) throw new System.ArgumentOutOfRangeException(nameof(bitCount));

      return (uint)GetBits(bitCount, out actualBitCount);
    }

    [System.CLSCompliant(false)]
    public ulong ReadBitsUInt64(int bitCount, out int actualBitCount)
    {
      if (bitCount < 0 || bitCount > 64) throw new System.ArgumentOutOfRangeException(nameof(bitCount));

      return (ulong)GetBits(bitCount, out actualBitCount);
    }

    #region Overridden inheritance

    // System.IO.Stream
    public override bool CanRead => m_bitCount > 0;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override void Flush() { }
    public override long Length => m_baseStream.Length;
    public override long Position { get => m_baseStream.Position; set => throw new System.NotImplementedException(); }

    /// <summary>Implements reading bulk bytes of bits.</summary>
    public override int Read(byte[] buffer, int offset, int count)
    {
      System.ArgumentNullException.ThrowIfNull(buffer);

      var index = offset;
      var limit = offset + count;

      for (; index < limit; index++)
      {
        if (ReadByte() is var read && read == -1) break;

        buffer[index] = (byte)read;
      }

      return limit - index - 1;
    }

    /// <summary>Implements reading a single byte of bits.</summary>
    public override int ReadByte()
    {
      var bits = GetBits(8, out var actualBitCount);

      return (actualBitCount == 0) ? -1 : (byte)bits;
    }

    public override long Seek(long offset, System.IO.SeekOrigin origin) => throw new System.NotImplementedException();
    public override void SetLength(long value) => throw new System.NotImplementedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new System.NotImplementedException();

    #endregion Overridden inheritance
  }
}
