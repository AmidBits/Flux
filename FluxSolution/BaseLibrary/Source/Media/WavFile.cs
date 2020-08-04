using System.Linq;

namespace Flux.Media
{
  /// <summary></summary>
  /// <seealso cref="http://tiny.systems/software/soundProgrammer/WavFormatDocs.pdf"/>
  /// <seealso cref="http://soundfile.sapp.org/doc/WaveFormat/"/>
  /// <seealso cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ee415713(v=vs.85).aspx"/>
  /// <seealso cref="https://sharkysoft.com/jwave/docs/javadocs/lava/riff/wave/doc-files/riffwave-frameset.htm"/>
  /// <seealso cref="https://sites.google.com/site/musicgapi/technical-documents/wav-file-format"/>
  /// <seealso cref="https://joenord.com/audio-wav-file-format"/>
  [System.CLSCompliant(false)]
  public static class WavFile
  {
    public static uint CalculateFileSize(TypeChunk type, FormatChunk format, DataChunk data) => (uint)(type.Buffer.Length + format.Buffer.Length + data.Buffer.Length + (format.SampleChannels * data.ChunkSize * 2));

    public static System.Collections.Generic.IEnumerable<Chunk> GetChunks(System.IO.Stream stream, bool onlyAdvanceOnDataChunk)
    {
      while (stream.Position < stream.Length)
      {
        yield return ReadChunk(stream, onlyAdvanceOnDataChunk);
      }
    }

    public static Chunk ReadChunk(System.IO.Stream stream, bool onlyAdvanceOnDataChunk)
    {
      var chunk = new Chunk(stream, 8);

      if (chunk.ChunkID.Equals(RiffTypeChunk.ID, System.StringComparison.OrdinalIgnoreCase))
      {
        chunk.Read(stream, 4);

        return new RiffTypeChunk(chunk.Buffer) { PositionInStream = chunk.PositionInStream };
      }
      else if (chunk.ChunkID.Equals(FormatChunk.ID, System.StringComparison.OrdinalIgnoreCase))
      {
        chunk.Read(stream, (int)chunk.ChunkSize);

        return new FormatChunk(chunk.Buffer) { PositionInStream = chunk.PositionInStream };
      }
      else if (chunk.ChunkID.Equals(DataChunk.ID, System.StringComparison.OrdinalIgnoreCase))
      {
        if (onlyAdvanceOnDataChunk)
        {
          stream.Seek(chunk.ChunkSize, System.IO.SeekOrigin.Current);
        }
        else
        {
          chunk.Read(stream, (int)chunk.ChunkSize);
        }

        return new DataChunk(chunk.Buffer) { PositionInStream = chunk.PositionInStream };
      }
      else // Return all other chunks as base Chunk.
      {
        chunk.Read(stream, (int)chunk.ChunkSize);

        return chunk;
      }
    }

    public class Chunk
    {
      public byte[] Buffer = new byte[0];

      public string ChunkID { get => System.Text.Encoding.ASCII.GetString(Buffer, 0, 4); set { System.Text.Encoding.ASCII.GetBytes(value.Substring(0, 4)).CopyTo(Buffer, 0); } }
      public uint ChunkSize { get => System.BitConverter.ToUInt32(Buffer, 4); set { System.BitConverter.GetBytes(value).CopyTo(Buffer, 4); } }

      public long PositionInStream { get; set; }

      public Chunk(uint chunkDataSize) => Buffer = new byte[chunkDataSize];
      public Chunk(string chunkID, uint chunkDataSize) : this(chunkDataSize)
      {
        ChunkID = chunkID;
        ChunkSize = chunkDataSize - 8;
      }
      public Chunk(byte[] bytes) : this((uint)bytes.Length) => bytes.CopyTo(Buffer, 0);
      public Chunk(System.IO.Stream stream, int count) => Read(stream, count);

      public byte[] Read(System.IO.Stream stream, int byteCount)
      {
        if (Buffer.Length == 0)
        {
          PositionInStream = stream.Position;

          Buffer = new byte[byteCount];

          stream.Read(Buffer, 0, byteCount);
        }
        else if (Buffer.Length is var offset)
        {
          System.Array.Resize(ref Buffer, offset + (int)byteCount);

          stream.Read(Buffer, offset, byteCount);
        }

        return Buffer;
      }

      public virtual uint FileSize => this is TypeChunk ? ChunkSize : ChunkSize + 8;

      public virtual void WriteTo(System.IO.Stream stream) => stream.Write(Buffer, 0, Buffer.Length);
    }

    public class TypeChunk : Chunk
    {
      public string Type { get => System.Text.Encoding.ASCII.GetString(Buffer, 8, 4); set { System.Text.Encoding.ASCII.GetBytes(value.Substring(0, 4)).CopyTo(Buffer, 8); } }

      public TypeChunk(string typeChunkID) : base(typeChunkID, 12) { }
      public TypeChunk(byte[] bytes) : base(bytes) { }
    }

    public class RiffTypeChunk : TypeChunk
    {
      public const string ID = @"RIFF";
      public const string TypeWave = @"WAVE";

      public RiffTypeChunk() : base(ID) { Type = TypeWave; }
      public RiffTypeChunk(byte[] bytes) : base(bytes) { }

      public override void WriteTo(System.IO.Stream stream)
      {
        if (ChunkSize <= 4)
        {
          throw new System.Exception("The RIFF property 'ChunkSize' represents the overall file size in bytes - 8 bytes.");
        }

        stream.Write(Buffer, 0, Buffer.Length);
      }
    }

    public class FormatChunk : Chunk
    {
      public const string ID = @"fmt ";

      public ushort Format { get => System.BitConverter.ToUInt16(Buffer, 8); set { System.BitConverter.GetBytes((ushort)value).CopyTo(Buffer, 8); } }
      public ushort SampleChannels { get => System.BitConverter.ToUInt16(Buffer, 10); set { System.BitConverter.GetBytes(value).CopyTo(Buffer, 10); UpdateBlockAlign(); } }
      public uint SampleRate { get => System.BitConverter.ToUInt32(Buffer, 12); set { System.BitConverter.GetBytes(value).CopyTo(Buffer, 12); UpdateAvgBytesPerSec(); } }
      public uint AvgBytesPerSec { get => System.BitConverter.ToUInt32(Buffer, 16); set { System.BitConverter.GetBytes(value).CopyTo(Buffer, 16); } }
      public ushort BlockAlign { get => System.BitConverter.ToUInt16(Buffer, 20); set { System.BitConverter.GetBytes(value).CopyTo(Buffer, 20); UpdateAvgBytesPerSec(); } }
      public ushort SampleBitDepth { get => System.BitConverter.ToUInt16(Buffer, 22); set { System.BitConverter.GetBytes(value).CopyTo(Buffer, 22); UpdateBlockAlign(); } }

      public bool IsExtendedFormat => (Buffer.Length > 24 && ExtensionSize == 22);
      public ushort ExtensionSize { get => System.BitConverter.ToUInt16(Buffer, 24); set { System.BitConverter.GetBytes(value).CopyTo(Buffer, 24); } }
      public ushort ValidBitsPerSample { get => System.BitConverter.ToUInt16(Buffer, 26); set { System.BitConverter.GetBytes(value).CopyTo(Buffer, 26); } }
      public uint ChannelMask { get => System.BitConverter.ToUInt32(Buffer, 28); set { System.BitConverter.GetBytes(value).CopyTo(Buffer, 28); } }
      public System.Guid SubFormat { get { var bytes = new byte[16]; Buffer.CopyTo(Buffer, 32); return new System.Guid(bytes); } set => value.ToByteArray().CopyTo(Buffer, 32); }

      public uint BytesPerSample => (SampleBitDepth / 8U) * SampleChannels;

      public FormatChunk() : base(ID, 16 + 8) { Format = 1; }
      public FormatChunk(ushort sampleChannels, uint sampleRate, ushort sampleBitDepth) : this()
      {
        SampleBitDepth = sampleBitDepth;
        SampleChannels = sampleChannels;
        SampleRate = sampleRate;
      }
      public FormatChunk(byte[] bytes) : base(bytes) { }

      public ushort UpdateBlockAlign() => BlockAlign = (ushort)(SampleChannels * (SampleBitDepth / 8));
      public uint UpdateAvgBytesPerSec() => AvgBytesPerSec = (SampleRate * BlockAlign);
    }

    public class DataChunk : Chunk
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
        System.Array.Resize(ref Buffer, 8 + (format.SampleBitDepth / 8 * format.SampleChannels) * samples);

        ChunkSize = (uint)Buffer.Length - 8;
      }

      public DataChunk(uint sampleBufferSize = 0) : base(ID, sampleBufferSize + 8) { }
      public DataChunk(byte[] bytes) : base(bytes) { }
    }

    public static void CreateFile16BitMono(string path, Flux.Dsp.Oscillator oscillator, int sampleCount)
    {
      var fileName = System.IO.Path.Combine(path, $"{oscillator}.wav");

      using var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);

      var rtc = new Media.WavFile.RiffTypeChunk();
      var fc = new Media.WavFile.FormatChunk(1, (uint)oscillator.SampleRate, 16);
      var dc = new Media.WavFile.DataChunk { ChunkSize = fc.BytesPerSample * (uint)sampleCount };

      rtc.ChunkSize = rtc.FileSize + fc.FileSize + dc.FileSize;

      rtc.WriteTo(fileStream);
      fc.WriteTo(fileStream);
      dc.WriteTo(fileStream);

      foreach (var amplitudeSample in oscillator.GetNext(sampleCount).Select(sample => unchecked((ushort)(short)(sample * short.MaxValue))))
      {
        fileStream.WriteByte((byte)(amplitudeSample & 0xFF));
        fileStream.WriteByte((byte)(amplitudeSample >> 0x8));
      }
    }

    public static void CreateFile16BitStereo(string path, Flux.Dsp.Oscillator oscillatorL, Flux.Dsp.Oscillator oscillatorR, uint sampleCount)
    {
      var fileName = System.IO.Path.Combine(path, $"{oscillatorL}_{oscillatorR}.wav");

      using var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);

      var rtc = new Media.WavFile.RiffTypeChunk();
      var fc = new Media.WavFile.FormatChunk(2, (uint)oscillatorL.SampleRate, 16);
      var dc = new Media.WavFile.DataChunk { ChunkSize = fc.BytesPerSample * sampleCount };

      rtc.ChunkSize = rtc.FileSize + fc.FileSize + dc.FileSize;

      rtc.WriteTo(fileStream);
      fc.WriteTo(fileStream);
      dc.WriteTo(fileStream);

      for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
      {
        var sampleL = unchecked((ushort)(short)(oscillatorL.NextSample().FrontCenter * short.MaxValue));

        fileStream.WriteByte((byte)(sampleL & 0xFF));
        fileStream.WriteByte((byte)(sampleL >> 0x8));

        var sampleR = unchecked((ushort)(short)(oscillatorR.NextSample().FrontCenter * short.MaxValue));

        fileStream.WriteByte((byte)(sampleR & 0xFF));
        fileStream.WriteByte((byte)(sampleR >> 0x8));
      }
    }
  }
}
