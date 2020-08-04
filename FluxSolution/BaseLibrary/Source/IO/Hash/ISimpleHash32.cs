namespace Flux.IO.Hash
{
  public interface ISimpleHash32
  {
    int ComputeSimpleHash32(byte[] bytes, int startAt, int count);
  }
}
