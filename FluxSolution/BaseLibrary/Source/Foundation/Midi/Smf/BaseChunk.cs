/// <summary>Relating to standard MIDI files.</summary>
/// <seealso cref="http://www.ccarh.org/courses/253/handout/smf/"/>
/// <seealso cref="https://www.cs.cmu.edu/~music/cmsip/readings/Standard-MIDI-file-format-updated.pdf"/>
/// <seealso cref="https://acad.carleton.edu/courses/musc108-00-f14/pages/04/04StandardMIDIFiles.html"/>
namespace Flux.Riff.Smf
{
  public class BaseChunk
    : Chunk
  {
    public override string ChunkID { get => System.Text.Encoding.ASCII.GetString(m_buffer, 0, 4); set { System.Text.Encoding.ASCII.GetBytes((value ?? throw new System.ArgumentNullException(nameof(value))).Substring(0, 4)).CopyTo(m_buffer, 0); } }
    public override int ChunkSize { get => Numerics.BitConverter.BigEndian.ToInt32(m_buffer, 4); set { Numerics.BitConverter.BigEndian.GetBytes(value).CopyTo(m_buffer, 4); } }

    public BaseChunk(string chunkID, int chunkDataSize)
      : base(chunkID, chunkDataSize)
    { }
    public BaseChunk(byte[] buffer)
      : base(buffer)
    { }
    public BaseChunk(System.IO.Stream chunk)
      : base(chunk)
    { }

    public static BaseChunk? GetChunk(System.IO.Stream stream)
    {
      if (stream is null) throw new System.ArgumentNullException(nameof(stream));

      try
      {
        var chunk = new BaseChunk(stream);

        switch (chunk.ChunkID)
        {
          case HeaderChunk.ID:
            chunk = new HeaderChunk(chunk.m_buffer);
            break;
          case TrackChunk.ID:
            chunk = new TrackChunk(chunk.m_buffer);
            break;
          default:
            break;
        }

        chunk.FillChunk(stream);

        return chunk;
      }
      catch (System.IO.EndOfStreamException)
      {
        return null;
      }
    }
    public static System.Collections.Generic.IEnumerable<BaseChunk> GetChunks(System.IO.Stream stream)
    {
      if (stream is null) throw new System.ArgumentNullException(nameof(stream));

      while (GetChunk(stream) is var chunk && !(chunk is null))
        yield return chunk;
    }

    public override string ToString()
      => base.ToString();
  }
}
