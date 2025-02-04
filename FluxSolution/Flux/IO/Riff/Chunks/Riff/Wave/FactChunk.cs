namespace Flux.IO.Riff.Chunks.Riff.Wave
{
  public sealed class FactChunk(byte[] buffer)
        : SubChunk(buffer)
  {
    public const string ID = @"fact";

    public int NumberOfSamplesPerChannel { get => System.BitConverter.ToInt32(m_buffer, 8); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 8); } }

    public override string ToString()
      => base.ToString().Replace(">", $", {NumberOfSamplesPerChannel}>", System.StringComparison.Ordinal);
  }
}
