namespace Flux.Riff.Chunks.Smf
{
  public sealed class TrackChunk
    : SmfChunk
  {
    public const string ID = @"MTrk";

    public string NextChunkPreview => System.Text.Encoding.ASCII.GetString(m_buffer, 8, 4);

    public TrackChunk() : base(ID, 8) { }

    public TrackChunk(byte[] buffer) : base(buffer) { }
  }
}
