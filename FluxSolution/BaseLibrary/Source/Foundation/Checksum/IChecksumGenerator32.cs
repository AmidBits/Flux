namespace Flux.Checksum
{
  public interface IChecksumGenerator32
  {
    int Checksum32 { get; }
    int GenerateChecksum32(byte[] bytes, int startAt, int count);
  }
}