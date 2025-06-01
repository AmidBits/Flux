namespace Flux.Randomness.NumberGenerators
{
  /// <summary>
  /// <para>The 32-bit random number generator xoshiro128** (XOR/shift/rotate) is an all-purpose, rock-solid (though not a cryptographically secure) generator. It has excellent speed, a state size (128 bits) that is large enough for mild parallelism, and it passes all tests we are aware of.</para>
  /// <para><see cref="http://xoshiro.di.unimi.it/"/></para>
  /// <para><seealso cref="	/// <see cref="http://xoshiro.di.unimi.it/splitmix64.c"/></para>
  /// </summary>
  public sealed class Xoshiro128SS
    : ARandom32
  {
    private static readonly System.Threading.ThreadLocal<System.Random> m_shared = new(() => new Xoshiro128SS());
    new public static System.Random Shared => m_shared.Value!;

    private uint m_state0, m_state1, m_state2, m_state3, m_t;

    [System.CLSCompliant(false)]
    public Xoshiro128SS(ulong seed)
    {
      var sm64 = new SplitMix64(seed);

      var state01 = sm64.SampleUInt64();
      var state23 = sm64.SampleUInt64();

      m_state0 = (uint)(state01 & 0xFFFF_FFFF);
      m_state1 = (uint)(state01 >> 32);
      m_state2 = (uint)(state23 & 0xFFFF_FFFF);
      m_state3 = (uint)(state23 >> 32);
    }
    public Xoshiro128SS(long seed) : this(unchecked((ulong)seed)) { }
    public Xoshiro128SS() : this(System.Diagnostics.Stopwatch.GetTimestamp()) { }

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

    //public override int Next()
    //  => int.CreateChecked(SampleUInt32() & 0x7FFF_FFFF);

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
    //    var length = int.Min(buffer.Length - index, 4);

    //    SampleUInt32().TryWriteToBuffer(buffer.Slice(index, length), Endianess.LittleEndian, out var _);

    //    index += length;
    //  }
    //}

    //protected override double Sample() => double.CreateChecked(SampleUInt32() >> 8) / double.CreateChecked(1U << 24);
  }
}
