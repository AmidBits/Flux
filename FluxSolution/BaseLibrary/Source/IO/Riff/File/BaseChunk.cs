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
  public class BaseChunk
    : Chunk
  {
    public override string ChunkID { get => System.Text.Encoding.ASCII.GetString(m_buffer, 0, 4); set { System.Text.Encoding.ASCII.GetBytes((value ?? throw new System.ArgumentNullException(nameof(value)))[..4]).CopyTo(m_buffer, 0); } }
    public override int ChunkSize { get => m_buffer.ReadInt32(4, Endianess.LittleEndian); set { value.WriteBytes(m_buffer, 4, Endianess.LittleEndian); } }

    public BaseChunk(string chunkID, int chunkDataSize)
      : base(chunkID, chunkDataSize)
    { }
    public BaseChunk(byte[] buffer)
       : base(buffer)
    { }
    public BaseChunk(System.IO.Stream chunk)
       : base(chunk)
    { }

    public static Chunk? GetChunk(System.IO.Stream stream)
    {
      System.ArgumentNullException.ThrowIfNull(stream);

      try
      {
        var chunk = new BaseChunk(stream);

        switch (chunk.ChunkID)
        {
          case CdifChunk.ID:
            chunk = new CdifChunk(chunk.m_buffer);
            chunk.FillChunk(stream);
            break;
          case DataChunk.ID:
            chunk = new DataChunk(chunk.m_buffer);
            chunk.FillChunk(stream);
            break;
          case FactChunk.ID:
            chunk = new FactChunk(chunk.m_buffer);
            chunk.FillChunk(stream);
            break;
          case FormatChunk.ID:
            chunk = new FormatChunk(chunk.m_buffer);
            chunk.FillChunk(stream);
            break;
          case ListChunk.ID:
            chunk = new ListChunk(chunk.m_buffer);
            chunk.ReadBytes(stream, 4);
            break;
          case RiffChunk.ID:
            chunk = new RiffChunk(chunk.m_buffer);
            chunk.ReadBytes(stream, 4);
            break;
          default:
            chunk.FillChunk(stream);
            break;
        }

        return chunk;
      }
      catch (System.IO.EndOfStreamException)
      {
        return null;
      }
    }
    public static System.Collections.Generic.IEnumerable<Chunk> GetChunks(System.IO.Stream stream)
    {
      System.ArgumentNullException.ThrowIfNull(stream);

      while (GetChunk(stream) is var chunk && chunk is not null)
        yield return chunk;
    }
  }
}
