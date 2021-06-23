/// <summary>Relating to standard MIDI files.</summary>
/// <seealso cref="http://www.ccarh.org/courses/253/handout/smf/"/>
/// <seealso cref="https://www.cs.cmu.edu/~music/cmsip/readings/Standard-MIDI-file-format-updated.pdf"/>
/// <seealso cref="https://acad.carleton.edu/courses/musc108-00-f14/pages/04/04StandardMIDIFiles.html"/>
namespace Flux.Riff.Smf
{
  public class HeaderChunk
    : BaseChunk
  {
    public const int FixedSize = 14;
    public const string ID = @"MThd";

    public Format Format { get => (Format)BitConverter.BigEndian.ToUInt16(m_buffer, 8); set => BitConverter.BigEndian.GetBytes((ushort)value).CopyTo(m_buffer, 8); }
    [System.CLSCompliant(false)] public ushort Tracks { get => BitConverter.BigEndian.ToUInt16(m_buffer, 10); set { BitConverter.BigEndian.GetBytes(value).CopyTo(m_buffer, 10); } }
    public short Division { get => BitConverter.BigEndian.ToInt16(m_buffer, 12); set { BitConverter.BigEndian.GetBytes(value).CopyTo(m_buffer, 12); } }

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
