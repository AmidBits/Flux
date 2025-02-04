namespace Flux.Randomness.Rng32
{
  /// <summary>The 32-bit generator xoshiro128** (XOR/shift/rotate) is an all-purpose, rock-solid (though not a cryptographically secure) generator. It has excellent speed, a state size (128 bits) that is large enough for mild parallelism, and it passes all tests we are aware of.</summary>
  /// <see cref="http://xoshiro.di.unimi.it/"/>
  /// <seealso cref="http://xoshiro.di.unimi.it/xoshiro128starstar.c"/>
  public sealed class Xoshiro128SS
    : ARandomUInt32
  {
    new public static System.Random Shared { get; } = new Xoshiro128SS();

    private uint m_state0, m_state1, m_state2, m_state3, m_t;

    [System.CLSCompliant(false)]
    public Xoshiro128SS(ulong seed)
    {
      var sm64 = new Rng64.SplitMix64(seed);

      var state01 = sm64.SampleUInt64();
      var state23 = sm64.SampleUInt64();

      m_state0 = (uint)(state01 & 0xFFFF_FFFF);
      m_state1 = (uint)(state01 >> 32);
      m_state2 = (uint)(state23 & 0xFFFF_FFFF);
      m_state3 = (uint)(state23 >> 32);
    }
    public Xoshiro128SS(long seed)
      : this(unchecked((ulong)seed))
    { }
    public Xoshiro128SS()
      : this(System.Diagnostics.Stopwatch.GetTimestamp())
    { }

    internal override uint SampleUInt32()
    {
      unchecked
      {
        var result = m_state1 * 5;

        result = ((result << 7) | (result >> 25)) * 9;

        m_t = m_state1 << 9;

        m_state2 ^= m_state0;
        m_state3 ^= m_state1;
        m_state1 ^= m_state2;
        m_state0 ^= m_state3;

        m_state2 ^= m_t;

        m_state3 = (m_state3 << 11) | (m_state3 >> 21);

        return result;
      }
    }
  }
}
