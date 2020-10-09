using Flux.Media.Riff.Wave;
using System.Linq;

namespace Flux.Media.Riff
{
  public static class File
  {
    public static System.Collections.Generic.IEnumerable<Chunk> GetChunks(System.IO.Stream stream)
    {
      if (stream is null) throw new System.ArgumentNullException(nameof(stream));

      while (stream.Position < stream.Length)
      {
        var chunk = new Chunk(stream, 8);

        switch (chunk.ChunkID)
        {
          case RiffChunk.ID:
            chunk.ReadBytes(stream, 4);
            chunk = new RiffChunk(chunk);
            break;
          case ListChunk.ID:
            chunk.ReadBytes(stream, 4);
            chunk = new ListChunk(chunk);
            break;
          case FormatChunk.ID:
            chunk.ReadBytes(stream, (int)chunk.ChunkSize);
            chunk = new FormatChunk(chunk);
            break;
          case DataChunk.ID:
            chunk.ReadBytes(stream, (int)chunk.ChunkSize);
            chunk = new DataChunk(chunk);
            break;
          case FactChunk.ID:
            chunk.ReadBytes(stream, (int)chunk.ChunkSize);
            chunk = new FactChunk(chunk);
            break;
          default:
            chunk.ReadBytes(stream, (int)chunk.ChunkSize);
            break;
        }

        yield return chunk;
      }
    }
  }

  public class Chunk
  {
    internal byte[] m_buffer = System.Array.Empty<byte>();

    public byte[] GetBuffer() => m_buffer;

    public string ChunkID { get => System.Text.Encoding.ASCII.GetString(m_buffer, 0, 4); set { System.Text.Encoding.ASCII.GetBytes((value ?? throw new System.ArgumentNullException(nameof(value))).Substring(0, 4)).CopyTo(m_buffer, 0); } }
    [System.CLSCompliant(false)] public uint ChunkSize { get => System.BitConverter.ToUInt32(m_buffer, 4); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 4); } }

    public long PositionInStream { get; set; }

    [System.CLSCompliant(false)]
    public Chunk(uint chunkDataSize)
      => m_buffer = new byte[chunkDataSize];
    [System.CLSCompliant(false)]
    public Chunk(string chunkID, uint chunkDataSize) : this(chunkDataSize)
    {
      ChunkID = chunkID;
      ChunkSize = chunkDataSize - 8;
    }
    public Chunk(byte[] bytes)
      : this((uint)(bytes ?? throw new System.ArgumentNullException(nameof(bytes))).Length)
      => bytes.CopyTo(m_buffer, 0);
    public Chunk(System.IO.Stream stream, int count)
      => ReadBytes(stream, count);
    public Chunk(Chunk chunk)
    {
      if (chunk is null) throw new System.ArgumentNullException(nameof(chunk));

      m_buffer = chunk.m_buffer;

      PositionInStream = chunk.PositionInStream;
    }

    public void ReadBytes(System.IO.Stream stream, int count)
    {
      if (stream is null) throw new System.ArgumentNullException(nameof(stream));

      if (m_buffer.Length == 0)
      {
        PositionInStream = stream.Position;

        m_buffer = new byte[count];

        stream.Read(m_buffer, 0, count);
      }
      else if (m_buffer.Length is var offset)
      {
        System.Array.Resize(ref m_buffer, offset + (int)count);

        stream.Read(m_buffer, offset, count);
      }
    }

    public virtual void WriteTo(System.IO.Stream stream)
    {
      if (stream is null) throw new System.ArgumentNullException(nameof(stream));

      stream.Write(m_buffer, 0, m_buffer.Length);
    }

    public override string ToString()
    {
      return $"<{GetType().Name} (\"{ChunkID}\", 8+{ChunkSize} bytes)>";
    }
  }

  public enum RiffType
  {
    Wave
  }

  public class TypeChunk
    : Chunk
  {
    public string Type { get => System.Text.Encoding.ASCII.GetString(m_buffer, 8, 4); set { System.Text.Encoding.ASCII.GetBytes((value ?? throw new System.ArgumentNullException(nameof(value))).Substring(0, 4)).CopyTo(m_buffer, 8); } }

    public TypeChunk(Chunk chunk)
      : base(12)
    {
      chunk.m_buffer.CopyTo(m_buffer, 0);

      PositionInStream = chunk.PositionInStream;
    }
    public override string ToString()
      => base.ToString().Replace(">", $", \"{Type}\">", System.StringComparison.Ordinal);
  }

  public class ListChunk
    : TypeChunk
  {
    public const string ID = @"LIST";

    public ListChunk(Chunk chunk)
      : base(chunk)
    {
    }
  }

  public class RiffChunk
    : TypeChunk
  {
    public const string ID = @"RIFF";

    public const string TypeWave = @"WAVE";

    public RiffChunk(Chunk chunk)
      : base(chunk)
    {
    }
  }

  namespace Wave
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

      [System.CLSCompliant(false)]
      public ushort UpdateBlockAlign()
        => BlockAlign = (ushort)(SampleChannels * (SampleBitDepth / 8));
      [System.CLSCompliant(false)]
      public uint UpdateAvgBytesPerSec()
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

      [System.CLSCompliant(false)]
      public DataChunk(uint sampleBufferSize)
        : base(ID, 8 + sampleBufferSize)
      {
      }
      public DataChunk(Chunk chunk)
        : base(chunk)
      {
      }
    }
  }
}