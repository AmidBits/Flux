namespace Flux.HashGenerators
{
  public interface ISimpleHash32Generatable
  {
    /// <summary>The current hash value.</summary>
    int SimpleHash32 { get; }
    /// <summary>Continue generating a simple hash value.</summary>
    int GenerateSimpleHash32(byte[] bytes, int offset, int count);
  }
}
