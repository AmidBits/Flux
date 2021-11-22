namespace Flux.Randomization
{
  /// <summary>The 64-bit generator xoshiro256** (XOR/shift/rotate) is our all-purpose, rock-solid generator (not a cryptographically secure generator, though, like all PRNGs in these pages). It has excellent (sub-ns) speed, a state space (256 bits) that is large enough for any parallel application, and it passes all tests we are aware of.</summary>
  /// <see cref="http://xoshiro.di.unimi.it/"/>
  /// <seealso cref="http://xoshiro.di.unimi.it/xoshiro256starstar.c"/>
  public sealed class Xoshiro256SS
    : ARandomUInt64
  {
    public static System.Random Default
      => new Xoshiro256SS();

    private ulong m_state0, m_state1, m_state2, m_state3, m_t;

    [System.CLSCompliant(false)]
    public Xoshiro256SS(ulong seed)
    {
      var sm64 = new SplitMix64(seed);

      m_state0 = sm64.SampleUInt64();
      m_state1 = sm64.SampleUInt64();
      m_state2 = sm64.SampleUInt64();
      m_state3 = sm64.SampleUInt64();
    }
    public Xoshiro256SS(long seed)
      : this(unchecked((ulong)seed))
    { }
    public Xoshiro256SS()
      : this(System.Diagnostics.Stopwatch.GetTimestamp())
    { }

    internal override ulong SampleUInt64()
    {
      unchecked
      {
        var result = m_state1 * 5;

        result = ((result << 7) | (result >> 57)) * 9;

        m_t = m_state1 << 17;

        m_state2 ^= m_state0;
        m_state3 ^= m_state1;
        m_state1 ^= m_state2;
        m_state0 ^= m_state3;

        m_state2 ^= m_t;

        m_state3 = (m_state3 << 45) | (m_state3 >> 19);

        return result;
      }
    }
  }
}
