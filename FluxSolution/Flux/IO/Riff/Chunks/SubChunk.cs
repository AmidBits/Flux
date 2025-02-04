/// <summary>Contains classes to read and evaluate RIFF files, which includes *.WAV files.</summary>
/// <seealso cref="http://tiny.systems/software/soundProgrammer/WavFormatDocs.pdf"/>
/// <seealso cref="http://soundfile.sapp.org/doc/WaveFormat/"/>
/// <seealso cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ee415713(v=vs.85).aspx"/>
/// <seealso cref="https://sharkysoft.com/jwave/docs/javadocs/lava/riff/wave/doc-files/riffwave-frameset.htm"/>
/// <seealso cref="https://sites.google.com/site/musicgapi/technical-documents/wav-file-format"/>
/// <seealso cref="https://joenord.com/audio-wav-file-format"/>
/// <seealso cref="https://johnloomis.org/cpe102/asgn/asgn1/riff.html"/>
namespace Flux.IO.Riff.Chunks
{
  public class SubChunk
    : Chunk
  {
    public System.Span<byte> ChunkData => m_buffer.AsSpan()[8..];

    public SubChunk(System.ReadOnlySpan<byte> bytes) : base(bytes.Length) => bytes.CopyTo(m_buffer);

    public SubChunk(byte[] bytes) : this(bytes.AsReadOnlySpan()) { }

    public SubChunk(string chunkID, int chunkSize) : base(chunkID, chunkSize) { }
  }
}
