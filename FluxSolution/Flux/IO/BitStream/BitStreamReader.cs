namespace Flux.IO.BitStream
{
  public sealed class BitStreamReader(System.IO.Stream baseStream)
    : System.IO.Stream
  {
    private ulong m_bitBuffer;

    private int m_bitCount;

    /// <summary>The intermediate 64-bit storage buffer.</summary>
    [System.CLSCompliant(false)]
    public ulong BitBuffer => m_bitBuffer & ((1UL << m_bitCount) - 1UL);

    /// <summary>The number of bits currently stored in the bit buffer.</summary>
    public int BitCount => m_bitCount;

    /// <summary>The total number of bits read through the bit stream.</summary>
    public int TotalBits { get; private set; }

    /// <summary>
    /// <para>Read bytes until at least <paramref name="bitCount"/> bits are in the bit-buffer.</para>
    /// </summary>
    /// <param name="bitCount">The minimum number of bits to buffer.</param>
    /// <exception cref="System.IO.EndOfStreamException"></exception>
    private void ReadBytes(int bitCount)
    {
      while (m_bitCount < bitCount)
      {
        if (baseStream.ReadByte() is int read && read == -1)
          throw new System.IO.EndOfStreamException();

        m_bitCount += 8;
        m_bitBuffer = (m_bitBuffer << 8) | (byte)read;
      }
    }

    public int ReadBitsInt32(int bitCount) => unchecked((int)ReadBitsUInt32(bitCount));
    public long ReadBitsInt64(int bitCount) => unchecked((long)ReadBitsUInt64(bitCount));

    [System.CLSCompliant(false)]
    public uint ReadBitsUInt32(int bitCount)
    {
      if (bitCount < 0 || bitCount > 32) throw new System.ArgumentOutOfRangeException(nameof(bitCount));

      ReadBytes(bitCount);

      TotalBits += bitCount;

      m_bitCount -= bitCount;

      return (uint)((m_bitBuffer >> m_bitCount) & ((1UL << bitCount) - 1UL));
    }
    [System.CLSCompliant(false)]
    public ulong ReadBitsUInt64(int bitCount)
    {
      if (bitCount < 0 || bitCount > 64) throw new System.ArgumentOutOfRangeException(nameof(bitCount));

      var bits = 0UL;

      if (bitCount > 32)
      {
        bitCount -= 32;

        bits = ((ulong)ReadBitsUInt32(32)) << bitCount;
      }

      return bits | ReadBitsUInt32(bitCount);
    }

    #region Overridden inheritance

    // System.IO.Stream
    public override bool CanRead => baseStream.CanRead;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override void Flush() { }
    public override long Length => baseStream.Length;
    public override long Position { get => baseStream.Position; set => throw new System.NotImplementedException(); }

    /// <summary>Implements reading bulk bytes of bits.</summary>
    public override int Read(byte[] buffer, int offset, int count)
    {
      System.ArgumentNullException.ThrowIfNull(buffer);

      int index = offset, limit = offset + count;

      for (; index < limit; index++)
      {
        try { buffer[index] = (byte)ReadByte(); }
        catch { break; }
      }

      return limit - index - 1;
    }

    /// <summary>Implements reading a single byte of bits.</summary>
    public override int ReadByte() => (byte)ReadBitsUInt32(8);

    public override long Seek(long offset, System.IO.SeekOrigin origin) => throw new System.NotImplementedException();
    public override void SetLength(long value) => throw new System.NotImplementedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new System.NotImplementedException();

    #endregion Overridden inheritance
  }
}
