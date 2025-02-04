namespace Flux.IO.Riff.Chunks.List.Info
{
  public sealed class IengChunk
    : SubChunk
  {
    public const string ID = @"IENG";

    public IengChunk() : base(ID, 8) { }

    public IengChunk(byte[] buffer) : base(buffer) { }
  }
}
