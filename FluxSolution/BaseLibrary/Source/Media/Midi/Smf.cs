/// <summary></summary>
/// <seealso cref="http://www.ccarh.org/courses/253/handout/smf/"/>
/// <seealso cref="https://www.cs.cmu.edu/~music/cmsip/readings/Standard-MIDI-file-format-updated.pdf"/>
/// <seealso cref="https://acad.carleton.edu/courses/musc108-00-f14/pages/04/04StandardMIDIFiles.html"/>
namespace Flux.Media.Midi.Smf
{
  public enum Format
  {
    SingleTrack = 0,
    MultipleParallelTracks = 1,
    MultipleSequentialTracks = 2
  }

  public enum FramesPerSecond
  {
    TwentyFour = -24,
    TwentyFive = -25,
    TwentyNine = -29,
    Thirty = -30,
  }

  public class Chunk
  {
    public byte[] Buffer = System.Array.Empty<byte>();

    public string ChunkID { get => System.Text.Encoding.ASCII.GetString(Buffer, 0, 4); set { System.Text.Encoding.ASCII.GetBytes((value ?? throw new System.ArgumentNullException(nameof(value))).Substring(0, 4)).CopyTo(Buffer, 0); } }
    [System.CLSCompliant(false)]
    public uint ChunkSize { get => System.BitConverter.ToUInt32(Buffer, 4); set { System.BitConverter.GetBytes(value).CopyTo(Buffer, 4); } }

    public long PositionInStream { get; set; }

    [System.CLSCompliant(false)]
    public Chunk(uint chunkSize)
      => Buffer = new byte[chunkSize];
    [System.CLSCompliant(false)]
    public Chunk(string chunkID, uint chunkSize)
      : this(chunkSize)
    {
      ChunkID = chunkID;
      ChunkSize = chunkSize - 8;
    }
    public Chunk(byte[] bytes)
      : this((uint)(bytes ?? throw new System.ArgumentNullException(nameof(bytes))).Length)
      => bytes.CopyTo(Buffer, 0);
    public Chunk(System.IO.Stream stream, int count)
      => Read(stream, count);

    public byte[] Read(System.IO.Stream stream, int byteCount)
    {
      if (stream is null) throw new System.ArgumentNullException(nameof(stream));

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

    public virtual void WriteTo(System.IO.Stream stream) => (stream ?? throw new System.ArgumentNullException(nameof(stream))).Write(Buffer, 0, Buffer.Length);
  }

  public class HeaderChunk
    : Chunk
  {
    public const string ID = @"MThd";

    public Format Format { get => (Format)Buffer[9]; set => Buffer[9] = (byte)value; }
    [System.CLSCompliant(false)]
    public ushort NumTracks { get => System.BitConverter.ToUInt16(Buffer, 10); set { System.BitConverter.GetBytes((ushort)value).CopyTo(Buffer, 10); } }
    public short Division { get => System.BitConverter.ToInt16(Buffer, 12); set { System.BitConverter.GetBytes((short)value).CopyTo(Buffer, 12); } }

    public FramesPerSecond FramesPerSecond { get => (FramesPerSecond)Buffer[12]; set => Buffer[12] = (byte)value; }
    public byte FrameResolution { get => Buffer[13]; set => Buffer[13] = value; }

    [System.CLSCompliant(false)]
    public ushort PulsesPerQuarterNote { get => System.BitConverter.ToUInt16(Buffer, 12); set => System.BitConverter.GetBytes((ushort)(value & 0x7FFF)).CopyTo(Buffer, 12); }

    public void SetMillisecondDivision() => Division = unchecked((short)FramesPerSecond.TwentyFive << 8) | 40;

    public HeaderChunk() : base(ID, 6) { }
  }

  public class TrackChunk
    : Chunk
  {
    public const string ID = @"MTrk";

    public TrackChunk() : base(ID, 0) { }
    public TrackChunk(byte[] bytes) : base(bytes) { }
  }
}
