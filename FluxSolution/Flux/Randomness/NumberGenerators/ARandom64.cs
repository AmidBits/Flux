namespace Flux.Randomness.NumberGenerators
{
  /// <summary>
  /// <para>An abstract base class for 64-bit random number generators.</para>
  /// </summary>
  abstract public class ARandom64
    : System.Random
  {
    #region System.Random overrides

    public override int Next()
      => Next(int.MaxValue);

    public override int Next(int maxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(maxValue);

      while (true)
        if (int.CreateChecked(Sample() * maxValue) is var value && value != maxValue)
          return value;
    }

    public override int Next(int minValue, int maxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThan(minValue, maxValue);

      return minValue + Next(maxValue - minValue);
    }

    public override void NextBytes(byte[] buffer)
      => NextBytes(buffer.AsSpan());

    public override void NextBytes(Span<byte> buffer)
    {
      for (var index = 0; index < buffer.Length;)
      {
        var length = int.Min(buffer.Length - index, 8);

        SampleUInt64().TryWriteToBuffer(buffer.Slice(index, length), Endianess.LittleEndian, out var _);

        index += length;
      }
    }

    public override long NextInt64()
      => NextInt64(long.MaxValue);

    public override long NextInt64(long maxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(maxValue);

      while (true)
        if (long.CreateChecked(Sample() * maxValue) is var value && value != maxValue)
          return value;
    }

    public override long NextInt64(long minValue, long maxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThan(minValue, maxValue);

      return minValue + NextInt64(maxValue - minValue);
    }

    #endregion // System.Random overrides

    //internal static ulong GenerateSeedUInt64()
    //{
    //  unchecked
    //  {
    //    var t = (ulong)System.Diagnostics.Stopwatch.GetTimestamp();

    //    var tr = t.ReverseBits();

    //    t = (t ^ tr) | t;

    //    return ~t;
    //  }
    //}

    //internal static long GenerateSeedInt64()
    //  => unchecked((long)(GenerateSeedUInt64() & 0x7FFF_FFFF_FFFF_FFFFUL));

    internal abstract ulong SampleUInt64();

    private readonly double SampleFactor = 1.0 / double.CreateChecked(new System.UInt128(1UL, 0UL));

    protected override double Sample()
      => SampleUInt64() * SampleFactor;
    //{
    //  while (true)
    //    if (double.CreateChecked(SampleUInt64()) / MaxSample is var sample && sample < 1d) // Ensure that the resulting sample does not evaluate to 1.
    //      return sample;
    //}
    //=> double.CreateChecked(NextInt64()) / double.CreateChecked(long.MaxValue);
    //=> double.CreateChecked(SampleUInt64() >> 11) / double.CreateChecked(1UL << 53);
  }
}
