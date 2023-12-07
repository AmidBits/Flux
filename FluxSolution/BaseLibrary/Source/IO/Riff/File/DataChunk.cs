namespace Flux.Riff
{
  public sealed class DataChunk
    : BaseChunk
  {
    public const string ID = @"data";

    //public short[] SamplesAs16Bit
    //{
    //  get
    //  {
    //    short[] buffer = new short[(int)System.Math.Ceiling(Buffer.Length / 2.0)];

    //    System.Buffer.BlockCopy(Buffer, 0, buffer, 0, Buffer.Length);

    //    return buffer;
    //  }
    //  set
    //  {
    //    System.Array.Resize(ref Buffer, 8 + value.Length * 2);

    //    System.Buffer.BlockCopy(value, 0, Buffer, 8, Buffer.Length);

    //    ChunkDataSize = (uint)Buffer.Length;
    //  }
    //}

    public void SetSampleBuffer(FormatChunk format, int samples)
    {
      System.ArgumentNullException.ThrowIfNull(format);

      System.Array.Resize(ref m_buffer, 8 + (format.SampleBitDepth / 8 * format.SampleChannels) * samples);

      ChunkSize = m_buffer.Length - 8;
    }

    public DataChunk(int sampleBufferSize)
      : base(ID, 8 + sampleBufferSize)
    {
    }
    public DataChunk(byte[] buffer)
      : base(buffer)
    { }
  }
}
