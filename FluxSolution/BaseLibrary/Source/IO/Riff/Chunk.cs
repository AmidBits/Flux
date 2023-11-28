/// <summary>Contains classes to read and evaluate RIFF files, which includes *.WAV files.</summary>
/// <seealso cref="http://tiny.systems/software/soundProgrammer/WavFormatDocs.pdf"/>
/// <seealso cref="http://soundfile.sapp.org/doc/WaveFormat/"/>
/// <seealso cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ee415713(v=vs.85).aspx"/>
/// <seealso cref="https://sharkysoft.com/jwave/docs/javadocs/lava/riff/wave/doc-files/riffwave-frameset.htm"/>
/// <seealso cref="https://sites.google.com/site/musicgapi/technical-documents/wav-file-format"/>
/// <seealso cref="https://joenord.com/audio-wav-file-format"/>
/// <seealso cref="https://johnloomis.org/cpe102/asgn/asgn1/riff.html"/>
namespace Flux.Riff
{
  public interface IChunk // Not sure about an IChunk interface yet.
  {
    System.Collections.Generic.IReadOnlyList<byte> Buffer { get; }

    string ChunkID { get; }
    int ChunkSize { get; }
  }

  public abstract class Chunk
    : IChunk
  {
    internal byte[] m_buffer = System.Array.Empty<byte>();
    public System.Collections.Generic.IReadOnlyList<byte> Buffer => m_buffer;

    public abstract string ChunkID { get; set; }
    public abstract int ChunkSize { get; set; }

    public long PositionInStream { get; set; }

    public Chunk(int totalBufferSize)
      => m_buffer = new byte[totalBufferSize];
    public Chunk(string chunkID, int chunkDataSize)
      : this(chunkDataSize - 8)
    {
      ChunkID = chunkID;
      ChunkSize = m_buffer.Length; // This is the chunk size as represented in the file.
    }
    public Chunk(byte[] bytes)
      : this((bytes ?? throw new System.ArgumentNullException(nameof(bytes))).Length)
      => bytes.CopyTo(m_buffer, 0);
    public Chunk(System.IO.Stream stream)
      => ReadBytes(stream, 8);

    public int ChunkSizeForFile => this is FormTypeChunk ? ChunkSize : ChunkSize + 8;

    public void ReadBytes(System.IO.Stream stream, int count)
    {
      System.ArgumentNullException.ThrowIfNull(stream);

      var read = -1;

      if (m_buffer is null || m_buffer.Length == 0)
      {
        PositionInStream = stream.Position;

        m_buffer = new byte[count];

        read = stream.Read(m_buffer, 0, count);
      }
      else if (m_buffer.Length is var offset)
      {
        System.Array.Resize(ref m_buffer, offset + (int)count);

        read = stream.Read(m_buffer, offset, count);
      }

      if (read == 0) throw new System.IO.EndOfStreamException();
    }

    public void FillChunk(System.IO.Stream stream)
    {
      System.ArgumentNullException.ThrowIfNull(stream);

      var offset = m_buffer.Length;

      System.Array.Resize(ref m_buffer, offset + (int)ChunkSize);

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

    public override string ToString()
      => $"{GetType().Name} {{ \"{ChunkID}\", 8+{ChunkSize} bytes }}";
  }
}
