namespace Flux.IO.Checksum
{
  public interface IChecksum32
  {
    //int Code { get; }
    int ComputeChecksum32(byte[] bytes, int startAt, int count);
  }
}