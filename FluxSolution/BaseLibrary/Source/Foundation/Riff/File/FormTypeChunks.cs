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
  public class FormTypeChunk
    : BaseChunk
  {
    public const string WaveID = @"WAVE";

    public string FormType { get => System.Text.Encoding.ASCII.GetString(m_buffer, 8, 4); set { System.Text.Encoding.ASCII.GetBytes((value ?? throw new System.ArgumentNullException(nameof(value)))[..4]).CopyTo(m_buffer, 8); } }

    public FormTypeChunk(string chunkID, string formType)
      : base(chunkID, 12)
      => FormType = formType;
    public FormTypeChunk(byte[] buffer)
      : base(buffer)
    { }

    public override string ToString()
    {
      var s = base.ToString();
      return s.Insert(s.Length - 1, $", \"{FormType}\"");
    }
  }

  public sealed class ListChunk
    : FormTypeChunk
  {
    public const string ID = @"LIST";

    public ListChunk(string formType)
      : base(ID, formType)
    { }
    public ListChunk(byte[] buffer)
      : base(buffer)
    { }
  }

  public sealed class RiffChunk
    : FormTypeChunk
  {
    public const string ID = @"RIFF";

    public RiffChunk(string formType)
      : base(ID, formType)
    { }
    public RiffChunk(byte[] buffer)
      : base(buffer)
    { }
  }
}
