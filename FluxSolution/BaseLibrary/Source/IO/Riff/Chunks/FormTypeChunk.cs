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
  public abstract class FormTypeChunk
    : Chunk
  {
    public const string WaveID = @"WAVE";

    public string FormType { get => System.Text.Encoding.ASCII.GetString(m_buffer, 8, 4); set { System.Text.Encoding.ASCII.GetBytes((value ?? throw new System.ArgumentNullException(nameof(value)))[..4]).CopyTo(m_buffer, 8); } }

    public FormTypeChunk(string chunkID, string formType) : base(chunkID, 4) => FormType = formType;

    public FormTypeChunk(byte[] buffer) : base(buffer) { }

    public override string ToString()
    {
      var sm = new SpanMaker<char>();

      sm = sm.Append($"{GetType().Name} {{ \"{ChunkID}\" (8+{ChunkSize} bytes) \"{FormType}\" }}");

      return sm.ToString();
    }
  }
}
