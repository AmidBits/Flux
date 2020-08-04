namespace Flux.Random
{
  public abstract class RandomUInt32
    : System.Random
  {
    /// <summary>Returns a non-negative random integer.</summary>
    /// <returns>A non-negative random integer.</returns>
    public override int Next() => (int)(SampleUInt32() & 0x7FFFFFFF);
    /// <summary>Returns a non-negative random integer that is less than the specified maximum.</summary>
    /// <returns>A non-negative random integer that is less than the specified maximum.</returns>
    public override int Next(int maxValue) => maxValue > 0 ? (int)(maxValue * Sample()) : throw new System.ArgumentOutOfRangeException(nameof(maxValue), @"Must be greater than zero.");
    /// <summary>Returns a random integer that is within a specified range.</summary>
    /// <returns>A random integer that is within a specified range, greater than or equal to specified minValue, and less than the specified maxValue.</returns>
    public override int Next(int minValue, int maxValue) => maxValue > minValue ? (int)(minValue + (maxValue - minValue) * Sample()) : throw new System.ArgumentOutOfRangeException(nameof(maxValue), @"Must be greater than minValue.");
    /// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
    public override void NextBytes(byte[] buffer)
    {
      for (var index = 0; index < buffer.Length;)
      {
        var value = SampleUInt32();

        for (var count = 0; count < 4 && index < buffer.Length; count++, value >>= 8)
        {
          buffer[index++] = (byte)(value & 0xFF);
        }
      }
    }
    /// <summary>Needs to return a value that is greater than or equal to 0.0, and less than 1.0</summary>
    /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
    public override double NextDouble() => (SampleUInt32() >> 8) / (1L << 24);
    /// <summary>Needs to return a value that is greater than or equal to 0.0, and less than 1.0</summary>
    /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
    protected override double Sample() => (SampleUInt32() >> 8) / (1L << 24);

    internal abstract uint SampleUInt32();
  }
}
