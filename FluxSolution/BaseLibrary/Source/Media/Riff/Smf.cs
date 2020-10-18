/// <summary>Relating to standard MIDI files.</summary>
/// <seealso cref="http://www.ccarh.org/courses/253/handout/smf/"/>
/// <seealso cref="https://www.cs.cmu.edu/~music/cmsip/readings/Standard-MIDI-file-format-updated.pdf"/>
/// <seealso cref="https://acad.carleton.edu/courses/musc108-00-f14/pages/04/04StandardMIDIFiles.html"/>
namespace Flux.Media.Riff.Smf
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

  public class HeaderChunk
    : Chunk
  {
    public const string ID = @"MThd";

    public Format Format { get => (Format)Buffer[9]; set => m_buffer[9] = (byte)value; }
    [System.CLSCompliant(false)]
    public ushort NumTracks { get => System.BitConverter.ToUInt16(m_buffer, 10); set { System.BitConverter.GetBytes((ushort)value).CopyTo(m_buffer, 10); } }
    public short Division { get => System.BitConverter.ToInt16(m_buffer, 12); set { System.BitConverter.GetBytes((short)value).CopyTo(m_buffer, 12); } }

    public FramesPerSecond FramesPerSecond { get => (FramesPerSecond)Buffer[12]; set => m_buffer[12] = (byte)value; }
    public byte FrameResolution { get => Buffer[13]; set => m_buffer[13] = value; }

    [System.CLSCompliant(false)]
    public ushort PulsesPerQuarterNote { get => System.BitConverter.ToUInt16(m_buffer, 12); set => System.BitConverter.GetBytes((ushort)(value & 0x7FFF)).CopyTo(m_buffer, 12); }

    public void SetMillisecondDivision() => Division = unchecked((short)FramesPerSecond.TwentyFive << 8) | 40;

    public HeaderChunk()
      : base(ID, 14)
    {
    }
    public HeaderChunk(Chunk chunk)
      : base(chunk)
    {
    }
  }

  public class TrackChunk
    : Chunk
  {
    public const string ID = @"MTrk";

    public TrackChunk(byte[] bytes)
      : base(bytes)
    {
    }
    public TrackChunk(Chunk chunk)
      : base(chunk)
    {
    }
  }
}
