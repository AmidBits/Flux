namespace Flux.IO.Riff.Chunks.List.Info
{
  public sealed class IartChunk
    : SubChunk
  {
    public const string ID = @"IART";

    public IartChunk() : base(ID, 8) { }

    public IartChunk(byte[] buffer) : base(buffer) { }
  }
}
