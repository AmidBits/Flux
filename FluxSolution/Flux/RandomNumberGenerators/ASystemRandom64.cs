namespace Flux.RandomNumberGenerators
{
  /// <summary>
  /// <para>An abstract base class for 64-bit random number generators.</para>
  /// </summary>
  public abstract class ASystemRandom64
    : System.Random
  {
    private readonly double SampleScalar = double.CreateChecked(System.UInt128.One << 64); // Divide by 65th bit, to maintain less than 1.0 output.

    public override int Next() => (int)(SampleUInt64() % int.MaxValue);

    public override int Next(int maxValue) => (int)NextInt64(maxValue);

    public override int Next(int minValue, int maxValue) => (int)NextInt64(minValue, maxValue);

    public override void NextBytes(byte[] buffer) => NextBytes(buffer.AsSpan());

    public override void NextBytes(Span<byte> buffer)
    {
      for (var index = 0; index < buffer.Length;)
      {
        var length = int.Min(buffer.Length - index, 8);

        SampleUInt64().TryWriteToBuffer(buffer.Slice(index, length), Endianess.LittleEndian, out var _);

        index += length;
      }
    }

    public override long NextInt64() => (long)(SampleUInt64() % long.MaxValue);

    public override long NextInt64(long maxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(maxValue);

      if (maxValue == 0) return 0;

      return (long)(SampleUInt64() % (ulong)maxValue);
    }

    public override long NextInt64(long minValue, long maxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfLessThan(maxValue, minValue);

      if (minValue == maxValue) return minValue;

      return minValue + (long)(SampleUInt64() % (ulong)(maxValue - minValue));
    }

    protected override double Sample() => SampleUInt64() / SampleScalar;

    internal abstract ulong SampleUInt64();
  }
}
