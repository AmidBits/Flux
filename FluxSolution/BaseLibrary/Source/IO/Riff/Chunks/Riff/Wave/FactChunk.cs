namespace Flux.Riff
{
  public sealed class FactChunk
    : SubChunk
  {
    public const string ID = @"fact";

    public int NumberOfSamplesPerChannel { get => System.BitConverter.ToInt32(m_buffer, 8); set { System.BitConverter.GetBytes(value).CopyTo(m_buffer, 8); } }

    public FactChunk(byte[] buffer) : base(buffer) { }

    public override string ToString()
      => base.ToString().Replace(">", $", {NumberOfSamplesPerChannel}>", System.StringComparison.Ordinal);
  }
}
