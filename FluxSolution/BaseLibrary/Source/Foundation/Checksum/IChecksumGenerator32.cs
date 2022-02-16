namespace Flux.Checksum
{
  public interface IChecksumGenerator32
  {
    /// <summary>The current checksum.</summary>
    int Checksum32 { get; }
    /// <summary>Continue generating a checksum.</summary>
    int GenerateChecksum32(byte[] bytes, int startAt, int count);
  }
}