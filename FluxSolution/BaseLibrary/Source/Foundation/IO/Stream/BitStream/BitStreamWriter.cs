namespace Flux.IO
{
  public class BitStreamWriter
    : System.IO.Stream
  {
    private readonly System.IO.Stream m_baseStream;

    private ulong m_bitBuffer;
    /// <summary>The intermediate 64-bit storage buffer.</summary>
    [System.CLSCompliant(false)]
    public ulong BitBuffer => m_bitBuffer & ((1UL << m_bitCount) - 1UL);

    private int m_bitCount;
    /// <summary>The number of bits currently stored in the bit buffer.</summary>
    public int BitCount => m_bitCount;

    /// <summary>The total number of bits written through the bit stream.</summary>
    public int TotalBits { get; private set; }

    public BitStreamWriter(System.IO.Stream output)
      => m_baseStream = output;

    private void WriteBytes()
    {
      while (m_bitCount >= 8)
      {
        m_bitCount -= 8;
        m_baseStream.WriteByte((byte)(m_bitBuffer >> m_bitCount));
      }

      if (m_bitCount > 0)
      {
        m_bitBuffer &= (ulong.MaxValue >> (64 - m_bitCount)); // Ensure clean 'spill' bits. (Fill zeros in the MSB's.)
      }
    }

    public virtual void WriteBits(int bits, int count)
      => WriteBits(unchecked((uint)bits), count);
    public virtual void WriteBits(long bits, int count)
      => WriteBits(unchecked((ulong)bits), count);

    [System.CLSCompliant(false)]
    public virtual void WriteBits(uint bits, int count)
    {
      if (count < 0 || count > 32) throw new System.ArgumentOutOfRangeException(nameof(count));

      if (m_bitCount + count > 64) WriteBytes();

      TotalBits += count;

      m_bitBuffer = (m_bitBuffer << count) | (bits & ((1UL << count) - 1UL));
      m_bitCount += count;
    }
    [System.CLSCompliant(false)]
    public virtual void WriteBits(ulong bits, int count)
    {
      if (count < 0 || count > 64) throw new System.ArgumentOutOfRangeException(nameof(count));

      if (count > 32)
      {
        count -= 32;
        WriteBits((uint)(bits >> count), 32);
      }

      WriteBits((uint)bits, count);
    }

    // System.IO.Stream
    public override bool CanRead
      => false;
    public override bool CanSeek
      => false;
    public override bool CanWrite
      => m_baseStream.CanWrite;
    public override void Flush()
    {
      if (m_bitCount > 0 && 8 - (m_bitCount % 8) is var shift && shift < 8)
      {
        m_bitBuffer <<= shift;
        m_bitCount += shift;
      }

      WriteBytes();

      m_baseStream.Flush();
    }
    public override long Length
      => m_baseStream.Length;
    public override long Position
    { get => m_baseStream.Position; set => throw new System.NotImplementedException(); }
    public override int Read(byte[] buffer, int offset, int count)
      => throw new System.NotImplementedException();
    public override long Seek(long offset, System.IO.SeekOrigin origin)
      => throw new System.NotImplementedException();
    public override void SetLength(long value)
      => throw new System.NotImplementedException();
    /// <summary>Implements writing bulk bytes of bits.</summary>
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (buffer is null) throw new System.ArgumentNullException(nameof(buffer));

      for (int index = offset, limit = offset + count; index < limit; index++)
      {
        WriteByte(buffer[index]);
      }
    }
    /// <summary>Implements writing a single byte of bits.</summary>
    public override void WriteByte(byte value)
      => WriteBits((uint)value, 8);
  }
}