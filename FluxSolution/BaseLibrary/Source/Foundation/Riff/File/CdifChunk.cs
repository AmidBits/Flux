namespace Flux.Riff
{
  public class CdifChunk
    : BaseChunk
  {
    public const string ID = @"CDif";

    public CdifChunk()
      : base(ID, 8)
    {
    }
    public CdifChunk(byte[] buffer)
      : base(buffer)
    { }
  }
}
