namespace Flux.Randomization
{
  /// <remarks>In earlier .net implementations, only Sample() was required to be overridden, whereas later implementations changed requirements of more overrides.</remarks>
  /// <see cref="https://docs.microsoft.com/en-us/dotnet/api/system.random"/>
  public abstract class ARandomUInt32
    : System.Random, IRandomSamplingInt32
  {
    /// <summary>Returns a non-negative random integer.</summary>
    /// <returns>A non-negative random integer.</returns>
    public override int Next()
      => Next(int.MaxValue);
    /// <summary>Returns a non-negative random integer that is less than the specified maximum.</summary>
    /// <returns>A non-negative random integer that is less than the specified maximum.</returns>
    public override int Next(int maxValue)
      => maxValue > 0 ? (int)System.Math.Floor(maxValue * Sample()) : throw new System.ArgumentOutOfRangeException(nameof(maxValue), @"The maximum value must be greater than zero.");
    /// <summary>Returns a random integer that is within a specified range.</summary>
    /// <returns>A random integer that is within a specified range, greater than or equal to specified minValue, and less than the specified maxValue.</returns>
    public override int Next(int minValue, int maxValue)
      => minValue < maxValue ? minValue + Next(maxValue - minValue) : throw new System.ArgumentOutOfRangeException(nameof(minValue), @"The minimum value must be less than the maximum value.");
    /// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
    public override void NextBytes(byte[] buffer)
    {
      if (buffer is null) throw new System.ArgumentNullException(nameof(buffer));

      for (var index = 0; index < buffer.Length;)
      {
        var value = SampleUInt32();

        for (var count = 0; count < 4 && index < buffer.Length; count++, value >>= 8)
          buffer[index++] = unchecked((byte)value);
      }
    }
    /// <summary>Needs to return a value that is greater than or equal to 0.0, and less than 1.0</summary>
    /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
    public override double NextDouble()
      => Sample();

    /// <summary>Needs to return a value that is greater than or equal to 0.0, and less than 1.0</summary>
    /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
    protected override double Sample()
      => (double)(SampleUInt32() >> 8) / (double)(1 << 24);

    internal abstract uint SampleUInt32();

    public int SampleInt32()
      => unchecked((int)SampleUInt32());
  }
}
