/// <summary>Relating to standard MIDI files.</summary>
/// <seealso cref="http://www.ccarh.org/courses/253/handout/smf/"/>
/// <seealso cref="https://www.cs.cmu.edu/~music/cmsip/readings/Standard-MIDI-file-format-updated.pdf"/>
/// <seealso cref="https://acad.carleton.edu/courses/musc108-00-f14/pages/04/04StandardMIDIFiles.html"/>
namespace Flux.Riff.Chunks.Smf
{
  // Fixed size 14 bytes.
  public sealed class HeaderChunk
    : SmfChunk
  {
    public const string ID = @"MThd";

    public Format Format { get => (Format)System.Buffers.Binary.BinaryPrimitives.ReadUInt16BigEndian(new System.ReadOnlySpan<byte>(m_buffer, 8, 2)); set => System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(new System.Span<byte>(m_buffer, 8, 2), (ushort)value); }
    [System.CLSCompliant(false)] public ushort Tracks { get => System.Buffers.Binary.BinaryPrimitives.ReadUInt16BigEndian(new System.ReadOnlySpan<byte>(m_buffer, 10, 2)); set { System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(new System.Span<byte>(m_buffer, 10, 2), value); } }
    public short Division { get => System.Buffers.Binary.BinaryPrimitives.ReadInt16BigEndian(new System.ReadOnlySpan<byte>(m_buffer, 12, 2)); set { System.Buffers.Binary.BinaryPrimitives.WriteInt16BigEndian(new System.Span<byte>(m_buffer, 12, 2), value); } }

    public string NextChunkPreview => System.Text.Encoding.ASCII.GetString(m_buffer, 14, 4);

    public void SetMillisecondDivision() => Division = unchecked((short)FramesPerSecond.TwentyFive << 8) | 40;

    public HeaderChunk() : base(ID, 14) { }

    public HeaderChunk(byte[] buffer) : base(buffer) { }

    public override string ToString()
    {
      var sb = new System.Text.StringBuilder(base.ToString());

      sb.Insert(sb.ToString().AsSpan().IndexOf(')') + 1, $" \"{Format}\", {Tracks} tracks, {Division}");

      return sb.ToString();
    }
  }
}
