namespace Flux.Riff.Chunks.List.Info
{
  public sealed class InamChunk
    : SubChunk
  {
    public const string ID = @"INAM";

    public InamChunk() : base(ID, 8) { }

    public InamChunk(byte[] buffer) : base(buffer) { }
  }
}
