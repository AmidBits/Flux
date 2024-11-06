namespace Flux.Riff
{
  public sealed class IsrcChunk
    : SubChunk
  {
    public const string ID = @"ISRC";

    public IsrcChunk() : base(ID, 8) { }

    public IsrcChunk(byte[] buffer) : base(buffer) { }
  }
}
