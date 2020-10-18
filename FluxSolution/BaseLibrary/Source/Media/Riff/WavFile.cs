/// <summary>Form type 'WAVE' chunks.</summary>
/// <seealso cref="http://tiny.systems/software/soundProgrammer/WavFormatDocs.pdf"/>
/// <seealso cref="http://soundfile.sapp.org/doc/WaveFormat/"/>
/// <seealso cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ee415713(v=vs.85).aspx"/>
/// <seealso cref="https://sharkysoft.com/jwave/docs/javadocs/lava/riff/wave/doc-files/riffwave-frameset.htm"/>
/// <seealso cref="https://sites.google.com/site/musicgapi/technical-documents/wav-file-format"/>
/// <seealso cref="https://joenord.com/audio-wav-file-format"/>
/// <seealso cref="https://johnloomis.org/cpe102/asgn/asgn1/riff.html"/>
namespace Flux.Media.Riff.WavFile
{
  public class FactChunk
    : Chunk
  {
    public const string ID = @"fact";

    [System.CLSCompliant(false)] public uint NumberOfSamples { get => System.BitConverter.ToUInt32(m_buffer, 8); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 8); } }

    public FactChunk(Chunk chunk)
      : base(chunk)
    {
    }

    public override string ToString()
      => base.ToString().Replace(">", $", {NumberOfSamples}>", System.StringComparison.Ordinal);
  }

  public class FormatChunk
    : Chunk
  {
    public const string ID = @"fmt ";

    [System.CLSCompliant(false)] public ushort Format { get => System.BitConverter.ToUInt16(m_buffer, 8); set { System.BitConverter.GetBytes((ushort)value).CopyTo(m_buffer, 8); } }
    [System.CLSCompliant(false)] public ushort SampleChannels { get => System.BitConverter.ToUInt16(m_buffer, 10); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 10); UpdateBlockAlign(); } }
    [System.CLSCompliant(false)] public uint SampleRate { get => System.BitConverter.ToUInt32(m_buffer, 12); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 12); UpdateAvgBytesPerSec(); } }
    [System.CLSCompliant(false)] public uint AvgBytesPerSec { get => System.BitConverter.ToUInt32(m_buffer, 16); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 16); } }
    [System.CLSCompliant(false)] public ushort BlockAlign { get => System.BitConverter.ToUInt16(m_buffer, 20); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 20); UpdateAvgBytesPerSec(); } }
    [System.CLSCompliant(false)] public ushort SampleBitDepth { get => System.BitConverter.ToUInt16(m_buffer, 22); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 22); UpdateBlockAlign(); } }

    public bool IsExtendedFormat => (m_buffer.Length > 24 && ExtensionSize == 22);
    [System.CLSCompliant(false)] public ushort ExtensionSize { get => System.BitConverter.ToUInt16(m_buffer, 24); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 24); } }
    [System.CLSCompliant(false)] public ushort ValidBitsPerSample { get => System.BitConverter.ToUInt16(m_buffer, 26); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 26); } }
    [System.CLSCompliant(false)] public uint ChannelMask { get => System.BitConverter.ToUInt32(m_buffer, 28); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 28); } }
    public System.Guid SubFormat { get { var bytes = new byte[16]; m_buffer.CopyTo(m_buffer, 32); return new System.Guid(bytes); } set => value.ToByteArray().CopyTo(m_buffer, 32); }

    [System.CLSCompliant(false)] public uint BytesPerSample => (SampleBitDepth / 8U) * SampleChannels;

    public FormatChunk()
      : base(ID, 8 + 16) // Chunk + Structure
    {
      Format = 1; // PCM
    }
    [System.CLSCompliant(false)]
    public FormatChunk(ushort sampleChannels, uint sampleRate, ushort sampleBitDepth)
      : this()
    {
      SampleBitDepth = sampleBitDepth;
      SampleChannels = sampleChannels;
      SampleRate = sampleRate;
    }
    public FormatChunk(Chunk chunk)
      : base(chunk)
    {
    }

    //[System.CLSCompliant(false)]
    private ushort UpdateBlockAlign()
      => BlockAlign = (ushort)(SampleChannels * (SampleBitDepth / 8));
    //[System.CLSCompliant(false)]
    private uint UpdateAvgBytesPerSec()
      => AvgBytesPerSec = (SampleRate * BlockAlign);

    public static string GetFormatName(int format)
      => format switch
      {
        1 => @"PCM",
        _ => throw new System.NotImplementedException()
      };

    public override string ToString()
      => base.ToString().Replace(">", $", {GetFormatName((int)Format)}, {SampleChannels} ch., {SampleRate} Hz, {SampleBitDepth}-bit>", System.StringComparison.Ordinal);
  }

  public class DataChunk
    : Chunk
  {
    public const string ID = @"data";

    //public short[] SamplesAs16Bit
    //{
    //  get
    //  {
    //    short[] buffer = new short[(int)System.Math.Ceiling(Buffer.Length / 2.0)];

    //    System.Buffer.BlockCopy(Buffer, 0, buffer, 0, Buffer.Length);

    //    return buffer;
    //  }
    //  set
    //  {
    //    System.Array.Resize(ref Buffer, 8 + value.Length * 2);

    //    System.Buffer.BlockCopy(value, 0, Buffer, 8, Buffer.Length);

    //    ChunkDataSize = (uint)Buffer.Length;
    //  }
    //}

    public void SetSampleBuffer(FormatChunk format, int samples)
    {
      if (format is null) throw new System.ArgumentNullException(nameof(format));

      System.Array.Resize(ref m_buffer, 8 + (format.SampleBitDepth / 8 * format.SampleChannels) * samples);

      ChunkSize = (uint)m_buffer.Length - 8;
    }

    public DataChunk(int sampleBufferSize)
      : base(ID, 8 + sampleBufferSize)
    {
    }
    public DataChunk(Chunk chunk)
      : base(chunk)
    {
    }
  }
}