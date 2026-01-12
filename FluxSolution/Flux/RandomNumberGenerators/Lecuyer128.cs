namespace Flux.RandomNumberGenerators
{
  /// <summary>A 64-bit random number generator.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Lehmer_random_number_generator"/>
  /// <see cref="https://en.wikipedia.org/wiki/Permuted_congruential_generator"/>
  /// <see cref="https://en.wikipedia.org/wiki/Combined_linear_congruential_generator"/>
  public sealed class Lecuyer128
    : ASystemRandom64
  {
    private static readonly System.Threading.ThreadLocal<System.Random> m_shared = new(() => new Lecuyer128());
    new public static System.Random Shared => m_shared.Value!;

    private readonly static System.UInt128 m_multiplier = System.UInt128.Parse("12e15e35b500f16e2e714eb2b37916a5", System.Globalization.NumberStyles.HexNumber);

    private System.UInt128 m_state;

    private Lecuyer128(System.UInt128 seed) => m_state = unchecked((seed << 1) | 1);
    public Lecuyer128() : this(BitOps.ReverseBits((System.UInt128)System.Diagnostics.Stopwatch.GetTimestamp()) | (System.UInt128)System.Diagnostics.Stopwatch.GetTimestamp()) { }

    internal override ulong SampleUInt64()
    {
      unchecked
      {
        m_state *= m_multiplier;

        return (ulong)(m_state >> 64);
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
