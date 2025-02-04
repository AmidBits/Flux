namespace Flux.Randomness.Rng64
{
  /// <see cref="http://xoshiro.di.unimi.it/"/>
  /// <seealso cref="	/// <see cref="http://xoshiro.di.unimi.it/splitmix64.c"/>
  public sealed class SplitMix64
    : ARandomUInt64
  {
    new public static System.Random Shared { get; } = new SplitMix64();

    private ulong m_state;

    [System.CLSCompliant(false)]
    public SplitMix64(ulong seed)
      => m_state = seed;
    public SplitMix64(long seed)
      : this(unchecked((ulong)seed))
    { }
    public SplitMix64()
      : this(System.Diagnostics.Stopwatch.GetTimestamp())
    { }

    internal override ulong SampleUInt64()
    {
      unchecked
      {
        var z = m_state += 0x9e3779b97f4a7c15;

        z = (z ^ (z >> 30)) * 0xbf58476d1ce4e5b9;
        z = (z ^ (z >> 27)) * 0x94d049bb133111eb;

        return z ^ (z >> 31);
      }
    }
  }
}
