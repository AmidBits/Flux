/// <summary>Contains classes to read and evaluate RIFF files, which includes *.WAV files.</summary>
/// <seealso cref="http://tiny.systems/software/soundProgrammer/WavFormatDocs.pdf"/>
/// <seealso cref="http://soundfile.sapp.org/doc/WaveFormat/"/>
/// <seealso cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ee415713(v=vs.85).aspx"/>
/// <seealso cref="https://sharkysoft.com/jwave/docs/javadocs/lava/riff/wave/doc-files/riffwave-frameset.htm"/>
/// <seealso cref="https://sites.google.com/site/musicgapi/technical-documents/wav-file-format"/>
/// <seealso cref="https://joenord.com/audio-wav-file-format"/>
/// <seealso cref="https://johnloomis.org/cpe102/asgn/asgn1/riff.html"/>
/// https://www.recordingblogs.com/wiki/list-chunk-of-a-wave-file
/// https://www.recordingblogs.com/wiki/associated-data-list-chunk-of-a-wave-file
namespace Flux.Riff.Chunks
{
  //public interface IChunk // Not sure about an IChunk interface yet.
  //{
  //  System.Collections.Generic.IReadOnlyList<byte> Buffer { get; }

  //  string ChunkID { get; }
  //  int ChunkSize { get; }
  //}

  public class Chunk
  {
    protected byte[] m_buffer = [];

    public string ChunkID { get => System.Text.Encoding.ASCII.GetString(m_buffer, 0, 4); set { System.Text.Encoding.ASCII.GetBytes((value ?? throw new System.ArgumentNullException(nameof(value)))[..4]).CopyTo(m_buffer, 0); } }
    virtual public int ChunkSize { get => System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(new System.ReadOnlySpan<byte>(m_buffer, 4, 4)); set { System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(new System.Span<byte>(m_buffer, 4, 4), value); } }

    /// <summary>
    /// <para>Initialize the chunk buffer to <paramref name="bufferSize"/>. The fields ChunkID and ChunkData are included in this size.</para>
    /// </summary>
    /// <param name="bufferSize">The size of the chunk buffer, i.e. all data including the fields ChunkID and ChunkSize.</param>
    public Chunk(int bufferSize)
    {
      m_buffer = new byte[bufferSize];

      ChunkID = "????";
      ChunkSize = bufferSize - 8;
    }

    /// <summary>
    /// <para>Initialize the chunk buffer with the <paramref name="chunkID"/> and the <paramref name="chunkSize"/>. The chunk data is also allocated to the size of <paramref name="chunkSize"/>.</para>
    /// </summary>
    /// <param name="chunkID">The FOURCC identifier of the chunk.</param>
    /// <param name="chunkSize">The size of the chunk data, i.e. the data excluding the fields ChunkID and ChunkSize).</param>
    public Chunk(string chunkID, int chunkSize)
    {
      m_buffer = new byte[8 + chunkSize];

      ChunkID = chunkID;
      ChunkSize = chunkSize;
    }

    /// <summary>
    /// <para>Initialize the chunk buffer with the data in <paramref name="bytes"/>. The data is copied.</para>
    /// </summary>
    /// <param name="bytes">The chunk buffer, i.e. all data including the fields ChunkID and ChunkSize.</param>
    public Chunk(byte[] bytes)
    {
      m_buffer = new byte[bytes.Length];

      bytes.CopyTo(m_buffer, 0);
    }

    public Chunk(System.IO.Stream stream) => ReadBytes(stream, 8);

    public int ChunkSizeForFile => this is FormTypeChunk ? ChunkSize : ChunkSize + 8;

    /// <summary>
    /// <para>Sets the size of the buffer by an <paramref name="absoluteSize"/>, i.e. becomes the buffer size.</para>
    /// </summary>
    /// <param name="absoluteSize"></param>
    /// <returns>The old size.</returns>
    public int SetBufferSizeAbsolute(int absoluteSize)
    {
      var oldSize = m_buffer.Length;

      System.Array.Resize(ref m_buffer, absoluteSize);

      return oldSize;
    }

    /// <summary>
    /// <para>Sets the size of the buffer by a <paramref name="relativeSize"/>, a negative value shrinks the buffer, a positive value grows the buffer.</para>
    /// </summary>
    /// <param name="relativeSize"></param>
    /// <returns>The old size and the new size, as a 2-tuple.</returns>
    public (int OldSize, int NewSize) SetBufferSizeRelative(int relativeSize)
    {
      var oldSize = m_buffer.Length;

      System.Array.Resize(ref m_buffer, oldSize + relativeSize);

      return (oldSize, m_buffer.Length);
    }

    public void ReadBytes(System.IO.Stream stream, int count)
    {
      System.ArgumentNullException.ThrowIfNull(stream);

      int read;

      if (m_buffer.Length == 0)
      {
        SetBufferSizeAbsolute(count);

        read = stream.Read(m_buffer, 0, count);
      }
      else //if (m_buffer.Length is var offset)
      {
        var offset = SetBufferSizeRelative(count).OldSize;

        read = stream.Read(m_buffer, offset, count);
      }

      if (read == 0) throw new System.IO.EndOfStreamException();
    }

    public void FillChunk(System.IO.Stream stream)
    {
      System.ArgumentNullException.ThrowIfNull(stream);

      //var offset = m_buffer.Length;

      var offset = SetBufferSizeRelative(ChunkSize).OldSize;
      //System.Array.Resize(ref m_buffer, offset + (int)ChunkSize);

      var read = stream.Read(m_buffer, offset, (int)ChunkSize);

      if (read == 0) throw new System.IO.EndOfStreamException();
    }

    public void WriteBytes(System.IO.Stream targetStream, int offset, int count)
    {
      System.ArgumentNullException.ThrowIfNull(targetStream);

      targetStream.Write(m_buffer, offset, count);
    }
    public void WriteTo(System.IO.Stream stream)
    {
      System.ArgumentNullException.ThrowIfNull(stream);

      stream.Write(m_buffer, 0, m_buffer.Length);
    }

    public static Chunk? GetChunk(System.IO.Stream stream)
    {
      System.ArgumentNullException.ThrowIfNull(stream);

      try
      {
        var chunk = new Chunk(stream);

        switch (chunk.ChunkID)
        {
          case Riff.Wave.DataChunk.ID:
            chunk = new Riff.Wave.DataChunk(chunk.m_buffer);
            chunk.FillChunk(stream);
            break;
          case Riff.Wave.FactChunk.ID:
            chunk = new Riff.Wave.FactChunk(chunk.m_buffer);
            chunk.FillChunk(stream);
            break;
          case Riff.Wave.FormatChunk.ID:
            chunk = new Riff.Wave.FormatChunk(chunk.m_buffer);
            chunk.FillChunk(stream);
            break;
          case Riff.RiffChunk.ID:
            chunk = new Riff.RiffChunk(chunk.m_buffer);
            chunk.ReadBytes(stream, 4);
            break;
          case List.ListChunk.ID:
            chunk = new List.ListChunk(chunk.m_buffer);
            chunk.ReadBytes(stream, 4);
            break;
          case Smf.HeaderChunk.ID:
            chunk = new Smf.HeaderChunk(chunk.m_buffer);
            chunk.ReadBytes(stream, 6);
            break;
          case Smf.TrackChunk.ID:
            var tchunk = new Smf.TrackChunk(chunk.m_buffer);
            tchunk.FillChunk(stream);
            chunk = tchunk;
            break;
          default:
            chunk.FillChunk(stream);
            break;
        }

        return chunk;
      }
      catch { }

      return null;
    }

    public static System.Collections.Generic.IEnumerable<Chunk> GetChunks(System.IO.Stream stream)
    {
      System.ArgumentNullException.ThrowIfNull(stream);

      while (GetChunk(stream) is var chunk && chunk is not null)
        yield return chunk;
    }

    public override string ToString()
      => $"{GetType().Name} {{ \"{ChunkID}\" (8+{ChunkSize} bytes) }}";
  }
}
