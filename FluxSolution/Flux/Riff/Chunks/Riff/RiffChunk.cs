/// <summary>Contains classes to read and evaluate RIFF files, which includes *.WAV files.</summary>
/// <seealso href="http://tiny.systems/software/soundProgrammer/WavFormatDocs.pdf"/>
/// <seealso href="http://soundfile.sapp.org/doc/WaveFormat/"/>
/// <seealso href="https://msdn.microsoft.com/en-us/library/windows/desktop/ee415713(v=vs.85).aspx"/>
/// <seealso href="https://sharkysoft.com/jwave/docs/javadocs/lava/riff/wave/doc-files/riffwave-frameset.htm"/>
/// <seealso href="https://sites.google.com/site/musicgapi/technical-documents/wav-file-format"/>
/// <seealso href="https://joenord.com/audio-wav-file-format"/>
/// <seealso href="https://johnloomis.org/cpe102/asgn/asgn1/riff.html"/>
/// <seealso href="https://mmsp.ece.mcgill.ca/Documents/AudioFormats/WAVE/WAVE.html"/>
namespace Flux.Riff.Chunks.Riff
{
  public sealed class RiffChunk
    : FormTypeChunk
  {
    public const string ID = @"RIFF";

    public RiffChunk(string formType) : base(ID, formType) { }

    public RiffChunk(byte[] buffer) : base(buffer) { }
  }
}
