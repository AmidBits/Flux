namespace Flux.Riff
{
  public sealed class IkeyChunk
    : SubChunk
  {
    public const string ID = @"IKEY";

    public IkeyChunk() : base(ID, 8) { }

    public IkeyChunk(byte[] buffer) : base(buffer) { }
  }
}