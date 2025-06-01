namespace Flux.Randomness.NumberGenerators
{
  /// <summary>
  /// <para>An abstract base class for 32-bit random number generators.</para>
  /// </summary>
  public abstract class ARandom32
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
        var length = int.Min(buffer.Length - index, 4);

        SampleUInt32().TryWriteToBuffer(buffer.Slice(index, length), Endianess.LittleEndian, out var _);

        index += length;
      }
    }

    #endregion // System.Random overrides

    //internal static uint GenerateSeedUInt32()
    //{
    //  unchecked
    //  {
    //    var t = (ulong)System.Diagnostics.Stopwatch.GetTimestamp();

    //    var tr = t.ReverseBits();

    //    t = (t ^ tr) | t;

    //    return (uint)(t >> 16);
    //  }
    //}

    //internal static int GenerateSeedInt32()
    //  => unchecked((int)(GenerateSeedUInt32() & 0x7FFF_FFFF));

    internal abstract uint SampleUInt32();

    private const double SampleFactor = 1.0 / (1UL << 32);

    protected override double Sample()
      => SampleUInt32() * SampleFactor;
    //{
    //  while (true)
    //    if (double.CreateChecked(SampleUInt32()) / MaxSample is var sample && sample < 1d)
    //      return sample;
    //}
    //return double.CreateChecked(SampleUInt32() >> 1) / double.CreateChecked(int.MaxValue);
    //=> double.CreateChecked(SampleUInt32() >> 8) / double.CreateChecked(1U << 23);
  }
}
