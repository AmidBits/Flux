namespace Flux.Randomness.NumberGenerators
{
  /// <summary>
  /// <para>A simple 32-bit random number generator based on George Marsaglia's MWC (multiply with carry) generator.</para>
  /// </summary>
  /// <remarks>Although it is very simple, it passes Marsaglia's DIEHARD series of random number generator tests.</remarks>
  public abstract class ARandom32
    : System.Random
  {
    internal abstract uint SampleUInt32();

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

    protected override double Sample()
    //=> double.CreateChecked(Next()) / double.CreateChecked(int.MaxValue);
    => double.CreateChecked(SampleUInt32() >> 8) / double.CreateChecked(1U << 24);
  }
}
