namespace Flux.Randomness.NumberGenerators
{
  /// <summary>
  /// <para>The 64-bit random number generator xoshiro256+ (XOR/shift/rotate) is our best and fastest (though not a cryptographically secure) generator.</para>
  /// <para><see cref="http://xoshiro.di.unimi.it/"/></para>
  /// <para><seealso cref="	/// <see cref="http://xoshiro.di.unimi.it/splitmix64.c"/></para>
  /// </summary>
  public sealed class Xoshiro256P
    : ARandom64
  {
    private static readonly System.Threading.ThreadLocal<System.Random> m_shared = new(() => new Xoshiro256P());
    new public static System.Random Shared => m_shared.Value!;

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

    //public override int Next()
    //  => int.CreateChecked(SampleUInt64() & 0x7FFF_FFFF);

    //public override int Next(int maxValue)
    //{
    //  System.ArgumentOutOfRangeException.ThrowIfNegative(maxValue);

    //  return int.CreateChecked(Sample() * maxValue);
    //}

    //public override int Next(int minValue, int maxValue)
    //  => minValue + Next(maxValue - minValue);

    //public override void NextBytes(byte[] buffer)
    //  => NextBytes(buffer.AsSpan());

    //public override void NextBytes(Span<byte> buffer)
    //{
    //  for (var index = 0; index < buffer.Length;)
    //  {
    //    var length = int.Min(buffer.Length - index, 8);

    //    SampleUInt64().TryWriteToBuffer(buffer.Slice(index, length), Endianess.LittleEndian, out var _);

    //    index += length;
    //  }
    //}

    //public override long NextInt64()
    //  => long.CreateChecked(SampleUInt64() & 0x7FFF_FFFF_FFFF_FFFF);

    //public override long NextInt64(long maxValue)
    //{
    //  System.ArgumentOutOfRangeException.ThrowIfNegative(maxValue);

    //  return long.CreateChecked(Sample() * maxValue);
    //}

    //public override long NextInt64(long minValue, long maxValue)
    //  => minValue + NextInt64(maxValue - minValue);

    //protected override double Sample() => double.CreateChecked(SampleUInt64() >> 11) / double.CreateChecked(1U << 53);
  }
}
