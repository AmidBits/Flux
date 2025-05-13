namespace Flux.Randomness.NumberGenerators
{
  /// <summary>
  /// <para>A 64-bit random number generator.</para>
  /// <para><see cref="	/// <see cref="http://xoshiro.di.unimi.it/splitmix64.c"/></para>
  /// <para><seealso cref="http://xoshiro.di.unimi.it/"/></para>
  /// </summary>
  public sealed class SplitMix64
    : ARandom64
  {
    private static readonly System.Threading.ThreadLocal<System.Random> m_shared = new(() => new SplitMix64());
    new public static System.Random Shared => m_shared.Value!;

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
