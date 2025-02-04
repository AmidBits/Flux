namespace Flux.Randomness.Rng64
{
  /// <summary>
  /// <para>This is simply a way to enable <see cref="System.Security.Cryptography.RandomNumberGenerator"/> as a System.Random generator.</para>
  /// </summary>
  public sealed class SscRng
    : ARandomUInt64
  {
    new public static System.Random Shared { get; } = new SscRng();

    internal override ulong SampleUInt64() => System.Security.Cryptography.RandomNumberGenerator.GetBytes(8).AsReadOnlySpan().ReadUInt64(Endianess.LittleEndian);
  }
}
