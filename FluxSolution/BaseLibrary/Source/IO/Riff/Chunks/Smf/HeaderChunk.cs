/// <summary>Relating to standard MIDI files.</summary>
/// <seealso cref="http://www.ccarh.org/courses/253/handout/smf/"/>
/// <seealso cref="https://www.cs.cmu.edu/~music/cmsip/readings/Standard-MIDI-file-format-updated.pdf"/>
/// <seealso cref="https://acad.carleton.edu/courses/musc108-00-f14/pages/04/04StandardMIDIFiles.html"/>
namespace Flux.Riff.Smf
{
  // Fixed size 14 bytes.
  public sealed class HeaderChunk
    : SmfChunk
  {
    public const string ID = @"MThd";

    public Format Format { get => (Format)m_buffer.ReadUInt16(8, Endianess.BigEndian); set => ((ushort)value).WriteBytes(m_buffer, 8, Endianess.BigEndian); }
    [System.CLSCompliant(false)] public ushort Tracks { get => m_buffer.ReadUInt16(10, Endianess.BigEndian); set { value.WriteBytes(m_buffer, 10, Endianess.BigEndian); } }
    public short Division { get => m_buffer.ReadInt16(12, Endianess.BigEndian); set { value.WriteBytes(m_buffer, 12, Endianess.BigEndian); } }

    public string NextChunkPreview => System.Text.Encoding.ASCII.GetString(m_buffer, 14, 4);

    public void SetMillisecondDivision() => Division = unchecked((short)FramesPerSecond.TwentyFive << 8) | 40;

    public HeaderChunk() : base(ID, 14) { }

    public HeaderChunk(byte[] buffer) : base(buffer) { }

    public override string ToString()
    {
      var sb = new System.Text.StringBuilder(base.ToString());

      sb.Insert(sb.IndexOf(0, ')') + 1, $" \"{Format}\", {Tracks} tracks, {Division}");

      return sb.ToString();
    }
  }
}
