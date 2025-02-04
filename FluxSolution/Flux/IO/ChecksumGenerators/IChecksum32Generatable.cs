namespace Flux.IO.ChecksumGenerators
{
  public interface IChecksum32Generatable
  {
    /// <summary>The current checksum.</summary>
    int Checksum32 { get; }
    /// <summary>Continue generating a checksum.</summary>
    int GenerateChecksum32(byte[] bytes, int offset, int count);
  }
}