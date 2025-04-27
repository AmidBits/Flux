namespace Flux.Riff.Chunks.Riff.Wave
{
  public sealed class FactChunk
    : SubChunk
  {
    public const string ID = @"fact";

    public FactChunk(byte[] buffer) : base(buffer) { }

    public int NumberOfSamplesPerChannel { get => System.BitConverter.ToInt32(m_buffer, 8); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 8); } }

    public override string ToString()
      => base.ToString().Replace(">", $", {NumberOfSamplesPerChannel}>", System.StringComparison.Ordinal);
  }
}
