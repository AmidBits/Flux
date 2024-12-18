/// <summary>Contains classes to read and evaluate RIFF files, which includes *.WAV files.</summary>
/// <seealso cref="http://tiny.systems/software/soundProgrammer/WavFormatDocs.pdf"/>
/// <seealso cref="http://soundfile.sapp.org/doc/WaveFormat/"/>
/// <seealso cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ee415713(v=vs.85).aspx"/>
/// <seealso cref="https://sharkysoft.com/jwave/docs/javadocs/lava/riff/wave/doc-files/riffwave-frameset.htm"/>
/// <seealso cref="https://sites.google.com/site/musicgapi/technical-documents/wav-file-format"/>
/// <seealso cref="https://joenord.com/audio-wav-file-format"/>
/// <seealso cref="https://johnloomis.org/cpe102/asgn/asgn1/riff.html"/>
namespace Flux.Riff.Smf
{
  public class SmfChunk
    : Chunk
  {
    /// <summary>
    /// <para>The chunk size for Smf uses BigEndian as opposed to the riff's.</para>
    /// </summary>
    override public int ChunkSize { get => m_buffer.AsReadOnlySpan(4).ReadInt32(Endianess.BigEndian); set { value.WriteBytes(m_buffer.AsSpan(4), Endianess.BigEndian); } }

    public System.Span<byte> ChunkData => m_buffer.AsSpan().Slice(8);

    public SmfChunk(System.ReadOnlySpan<byte> bytes) : base(bytes.Length) => bytes.CopyTo(m_buffer);
    public SmfChunk(byte[] bytes) : this(bytes.AsReadOnlySpan()) { }

    public SmfChunk(string chunkID, int chunkSize) : base(chunkID, chunkSize) { }
  }
}
