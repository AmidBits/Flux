namespace Flux.IO.Checksum
{
  public interface IChecksum32
  {
    int ComputeChecksum32(byte[] bytes, int startAt, int count);
  }
}