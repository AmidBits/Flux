namespace Flux.Random
{
  /// <summary>The 32-bit generator xoshiro128+ (XOR/shift/rotate) our best and fastest (though not a cryptographically secure) generator.</summary>
  /// <see cref="http://xoshiro.di.unimi.it/"/>
  /// <seealso cref="http://xoshiro.di.unimi.it/xoshiro128plus.c"/>
  public sealed class Xoshiro128P
    : ARandomUInt32
  {
    private uint m_state0, m_state1, m_state2, m_state3, m_t;

    [System.CLSCompliant(false)]
    public Xoshiro128P(ulong seed)
    {
      var sm64 = new SplitMix64(seed);

      var state01 = sm64.SampleUInt64();
      var state23 = sm64.SampleUInt64();

      m_state0 = (uint)(state01 & 0xFFFF_FFFF);
      m_state1 = (uint)(state01 >> 32);
      m_state2 = (uint)(state23 & 0xFFFF_FFFF);
      m_state3 = (uint)(state23 >> 32);
    }
    public Xoshiro128P(long seed)
      : this(unchecked((ulong)seed))
    { }
    public Xoshiro128P()
      : this(System.Diagnostics.Stopwatch.GetTimestamp())
    { }

    internal override uint SampleUInt32()
    {
      unchecked
      {
        var result = m_state0 + m_state3;

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
