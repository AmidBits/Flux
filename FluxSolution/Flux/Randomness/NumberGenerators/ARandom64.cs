namespace Flux.Randomness.NumberGenerators
{
  /// <summary>
  /// <para>A 64-bit random number generator.</para>
  /// <para><see cref="	/// <see cref="http://xoshiro.di.unimi.it/splitmix64.c"/></para>
  /// <para><seealso cref="http://xoshiro.di.unimi.it/"/></para>
  /// </summary>
  abstract public class ARandom64
    : System.Random
  {
    internal abstract ulong SampleUInt64();

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

    protected override double Sample()
    //=> double.CreateChecked(NextInt64()) / double.CreateChecked(long.MaxValue);
    => double.CreateChecked(SampleUInt64() >> 11) / double.CreateChecked(1UL << 53);
  }
}
