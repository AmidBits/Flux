namespace Flux.Random
{
  /// <summary>The 64-bit generator xoshiro256+ (XOR/shift/rotate) is our best and fastest (though not a cryptographically secure) generator.</summary>
  /// <see cref="http://xoshiro.di.unimi.it/"/>
  /// <seealso cref="http://xoshiro.di.unimi.it/xoshiro256plus.c"/>
  public sealed class Xoshiro256P
    : ARandomUInt64
  {
    //public static System.Random Default
    //  => new Xoshiro256P();

    private ulong m_state0, m_state1, m_state2, m_state3, m_t;

    [System.CLSCompliant(false)]
    public Xoshiro256P(ulong seed)
    {
      var sm64 = new SplitMix64(seed);

      m_state0 = sm64.SampleUInt64();
      m_state1 = sm64.SampleUInt64();
      m_state2 = sm64.SampleUInt64();
      m_state3 = sm64.SampleUInt64();
    }
    public Xoshiro256P(long seed)
      : this(unchecked((ulong)seed))
    { }
    public Xoshiro256P()
      : this(System.Diagnostics.Stopwatch.GetTimestamp())
    { }

    internal override ulong SampleUInt64()
    {
      unchecked
      {
        var result = m_state0 + m_state3;

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
