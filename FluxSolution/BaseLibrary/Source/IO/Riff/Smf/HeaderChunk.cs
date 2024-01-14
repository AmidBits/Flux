/// <summary>Relating to standard MIDI files.</summary>
/// <seealso cref="http://www.ccarh.org/courses/253/handout/smf/"/>
/// <seealso cref="https://www.cs.cmu.edu/~music/cmsip/readings/Standard-MIDI-file-format-updated.pdf"/>
/// <seealso cref="https://acad.carleton.edu/courses/musc108-00-f14/pages/04/04StandardMIDIFiles.html"/>
namespace Flux.Riff.Smf
{
  public sealed class HeaderChunk
    : BaseChunk
  {
    public const int FixedSize = 14;
    public const string ID = @"MThd";

    public Format Format { get => (Format)m_buffer.ReadUInt16(8, Endianess.BigEndian); set => ((ushort)value).WriteBytes(m_buffer, 8, Endianess.BigEndian); }
    [System.CLSCompliant(false)] public ushort Tracks { get => m_buffer.ReadUInt16(10, Endianess.BigEndian); set { value.WriteBytes(m_buffer, 10, Endianess.BigEndian); } }
    public short Division { get => m_buffer.ReadInt16(12, Endianess.BigEndian); set { value.WriteBytes(m_buffer, 12, Endianess.BigEndian); } }

    public void SetMillisecondDivision() => Division = unchecked((short)FramesPerSecond.TwentyFive << 8) | 40;

    public HeaderChunk()
      : base(ID, 14)
    { }
    public HeaderChunk(byte[] buffer)
      : base(buffer)
    { }

    public override string ToString()
      => base.ToString().Replace(">", $", \"{Format}\", {Tracks} tracks, {Division}>", System.StringComparison.Ordinal);
  }
}
