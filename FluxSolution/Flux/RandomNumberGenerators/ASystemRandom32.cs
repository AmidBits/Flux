namespace Flux.RandomNumberGenerators
{
  /// <summary>
  /// <para>An abstract base class for 32-bit random number generators.</para>
  /// </summary>
  public abstract class ASystemRandom32
    : System.Random
  {
    private const double SampleScalar = (1UL << 32); // Divide by 33rd bit, to maintain less than 1.0 operations.

    public override int Next() => (int)(SampleUInt32() % int.MaxValue);

    public override int Next(int maxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(maxValue);

      if (maxValue == 0) return 0;

      return (int)(SampleUInt32() % maxValue);
    }

    public override int Next(int minValue, int maxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfLessThan(maxValue, minValue);

      if (minValue == maxValue) return minValue;

      return minValue + (int)(SampleUInt32() % (maxValue - minValue));
    }

    public override void NextBytes(byte[] buffer) => NextBytes((System.Span<byte>)buffer);

    public override void NextBytes(Span<byte> buffer)
    {
      var index = 0;

      while (index < buffer.Length)
      {
        var sample = SampleUInt32();

        switch (buffer.Length - index)
        {
          case >= 4:
            System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(buffer[index..], (uint)sample);
            index += 4;
            break;
          case >= 2:
            System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(buffer[index..], (ushort)sample);
            index += 2;
            break;
          case 1:
            buffer[index++] = (byte)sample;
            break;
          default:
            throw new System.InvalidOperationException();
        }
      }

      //for (var index = 0; index < buffer.Length;)
      //{
      //  var length = int.Min(buffer.Length - index, 4);

      //  //SampleUInt32().TryWriteToBuffer(buffer.Slice(index, length), Endianess.LittleEndian, out var _);
      //  System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(buffer.Slice(index, length), SampleUInt32());

      //  index += length;
      //}
    }

    protected override double Sample() => SampleUInt32() / SampleScalar;

    internal abstract uint SampleUInt32();
  }
}
