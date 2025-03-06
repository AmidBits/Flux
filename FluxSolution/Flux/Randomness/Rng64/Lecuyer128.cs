namespace Flux.Randomness.Rng64
{
  /// <summary>The Lecuyer128 is a 64-bit generator.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Lehmer_random_number_generator"/>
  /// <see cref="https://en.wikipedia.org/wiki/Permuted_congruential_generator"/>
  /// <see cref="https://en.wikipedia.org/wiki/Combined_linear_congruential_generator"/>
  public sealed class Lecuyer128
    : ARandomUInt64
  {
    new public static System.Random Shared { get; } = new Lecuyer128();

    private readonly static System.UInt128 m_multiplier = System.UInt128.Parse("12e15e35b500f16e2e714eb2b37916a5", System.Globalization.NumberStyles.HexNumber);

    private System.UInt128 m_state;

    private Lecuyer128(System.UInt128 seed) => m_state = (seed << 1) | 1;
    public Lecuyer128() : this(((System.UInt128)System.Diagnostics.Stopwatch.GetTimestamp() << 64) | (System.UInt128)System.Diagnostics.Stopwatch.GetTimestamp()) { }

    internal override ulong SampleUInt64() => unchecked((ulong)((m_state *= m_multiplier) >> 64));
  }
}
