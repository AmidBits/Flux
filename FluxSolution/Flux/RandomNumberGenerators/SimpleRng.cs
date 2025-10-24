namespace Flux.RandomNumberGenerators
{
  /// <summary>
  /// <para>A simple 32-bit random number generator based on George Marsaglia's MWC (multiply with carry) generator.</para>
  /// </summary>
  /// <remarks>Although it is very simple, it passes Marsaglia's DIEHARD series of random number generator tests.</remarks>
  public sealed class SimpleRng
    : ASystemRandom32
  {
    private static readonly System.Threading.ThreadLocal<System.Random> m_shared = new(() => new SimpleRng());
    new public static System.Random Shared => m_shared.Value!;

    public enum SeedEnum
    {
      MarsagliaDefault,
      TimerMechanism
    }

    private uint m_w;
    private uint m_z;

    [System.CLSCompliant(false)]
    public SimpleRng(uint seed1, uint seed2)
    {
      m_w = seed1;
      m_z = seed2;
    }

    public SimpleRng(int seed1, int seed2)
      : this(unchecked((uint)seed1), unchecked((uint)seed2))
    { }

    public SimpleRng(SeedEnum seed)
    {
      switch (seed)
      {
        case SeedEnum.MarsagliaDefault:
          m_w = 521288629;
          m_z = 362436069;
          break;
        case SeedEnum.TimerMechanism:
          var t = (uint)System.Diagnostics.Stopwatch.GetTimestamp();
          m_w = t;
          m_z = t.ReverseBits();
          break;
      }
    }

    public SimpleRng()
      : this(SeedEnum.TimerMechanism)
    { }

    internal override uint SampleUInt32()
    {
      m_z = 36969 * (m_z & 65535) + (m_z >> 16);
      m_w = 18000 * (m_w & 65535) + (m_w >> 16);

      return unchecked((m_z << 16) + m_w);
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
