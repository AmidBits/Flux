using System.Linq;

namespace Flux.Riff
{
  public sealed class CdifChunk
    : BaseChunk
  {
    public const string ID = @"CDif";

    public CdifChunk()
      : base(ID, 8)
    { }
    public CdifChunk(byte[] buffer)
      : base(buffer)
    { }
  }
}
