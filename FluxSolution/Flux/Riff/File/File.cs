/// <summary>Contains classes to read and evaluate RIFF files, which includes *.WAV files.</summary>
/// <seealso cref="http://tiny.systems/software/soundProgrammer/WavFormatDocs.pdf"/>
/// <seealso cref="http://soundfile.sapp.org/doc/WaveFormat/"/>
/// <seealso cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ee415713(v=vs.85).aspx"/>
/// <seealso cref="https://sharkysoft.com/jwave/docs/javadocs/lava/riff/wave/doc-files/riffwave-frameset.htm"/>
/// <seealso cref="https://sites.google.com/site/musicgapi/technical-documents/wav-file-format"/>
/// <seealso cref="https://joenord.com/audio-wav-file-format"/>
/// <seealso cref="https://johnloomis.org/cpe102/asgn/asgn1/riff.html"/>
namespace Flux.Riff.File
{
  public static class File
  {
    public static void CreateFile16BitMono(string path, Dsp.Oscillators.Oscillator oscillator, int sampleCount)
    {
      System.ArgumentNullException.ThrowIfNull(oscillator);

      var fileName = System.IO.Path.Combine(path, $"{oscillator}.wav");

      using var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);

      var rc = new Chunks.Riff.RiffChunk(Riff.Chunks.FormTypeChunk.WaveID);
      var fc = new Chunks.Riff.Wave.FormatChunk(1, (int)oscillator.SampleRate, 16);
      var dc = new Chunks.Riff.Wave.DataChunk((int)(fc.BytesPerSample * sampleCount));

      rc.ChunkSize = rc.ChunkSizeForFile + fc.ChunkSizeForFile + dc.ChunkSizeForFile;

      rc.WriteTo(fileStream);
      fc.WriteTo(fileStream);
      dc.WriteTo(fileStream);

      foreach (var amplitudeSample in oscillator.GetNext(sampleCount).Select(sample => unchecked((ushort)(short)(sample * short.MaxValue))))
      {
        fileStream.WriteByte((byte)(amplitudeSample & 0xFF));
        fileStream.WriteByte((byte)(amplitudeSample >> 0x8));
      }
    }

    public static void CreateFile16BitStereo(string path, Dsp.Oscillators.Oscillator oscillatorL, Dsp.Oscillators.Oscillator oscillatorR, int sampleCount)
    {
      System.ArgumentNullException.ThrowIfNull(oscillatorL);
      System.ArgumentNullException.ThrowIfNull(oscillatorR);

      var fileName = System.IO.Path.Combine(path, $"{oscillatorL}_{oscillatorR}.wav");

      using var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);

      var rc = new Chunks.Riff.RiffChunk(Riff.Chunks.FormTypeChunk.WaveID);
      var fc = new Chunks.Riff.Wave.FormatChunk(2, (int)oscillatorL.SampleRate, 16);
      var dc = new Chunks.Riff.Wave.DataChunk((int)(fc.BytesPerSample * sampleCount));

      rc.ChunkSize = rc.ChunkSizeForFile + fc.ChunkSizeForFile + dc.ChunkSizeForFile;

      rc.WriteTo(fileStream);
      fc.WriteTo(fileStream);
      dc.WriteTo(fileStream);

      for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
      {
        var sampleL = unchecked((ushort)(short)(oscillatorL.NextSample() * short.MaxValue));

        fileStream.WriteByte((byte)(sampleL & 0xFF));
        fileStream.WriteByte((byte)(sampleL >> 0x8));

        var sampleR = unchecked((ushort)(short)(oscillatorR.NextSample() * short.MaxValue));

        fileStream.WriteByte((byte)(sampleR & 0xFF));
        fileStream.WriteByte((byte)(sampleR >> 0x8));
      }
    }
  }
}