namespace Flux.Riff.Smf
{
  public sealed class TrackChunk
    : BaseChunk
  {
    public const string ID = @"MTrk";

    public TrackChunk()
      : base(ID, 8)
    { }
    public TrackChunk(byte[] buffer)
      : base(buffer)
    { }
  }
}
