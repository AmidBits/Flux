namespace Flux.Randomness.NumberGenerators
{
  /// <summary>
  /// <para>This is simply a <see cref="System.Random"/> wrapper for the <see cref="System.Security.Cryptography.RandomNumberGenerator"/>.</para>
  /// </summary>
  public sealed class SscRng
    : System.Random
  {
    private static readonly System.Threading.ThreadLocal<System.Random> m_shared = new(() => new SscRng());
    new public static System.Random Shared => m_shared.Value!;

    public override int Next() => System.Security.Cryptography.RandomNumberGenerator.GetInt32(int.MaxValue);

    public override int Next(int maxValue) => System.Security.Cryptography.RandomNumberGenerator.GetInt32(int.Max(maxValue, 1)); // For some reason .GetInt32(toExclusive) does not work like Random.Next(toExclusive), zero appears not allowed.

    public override int Next(int minValue, int maxValue) => System.Security.Cryptography.RandomNumberGenerator.GetInt32(minValue, maxValue);

    public override void NextBytes(byte[] buffer) => System.Security.Cryptography.RandomNumberGenerator.Fill(buffer);

    public override void NextBytes(Span<byte> buffer) => System.Security.Cryptography.RandomNumberGenerator.Fill(buffer);

    protected override double Sample() => double.CreateChecked(System.Security.Cryptography.RandomNumberGenerator.GetInt32(int.MaxValue) >> 7) / double.CreateChecked(1U << 24); // Only right shift 7 since the type is an int, not uint.
  }
}
