namespace Flux.Randomness.Rng32
{
  /// <remarks>In earlier .net implementations, only Sample() was required to be overridden, whereas later implementations changed requirements of more overrides.</remarks>
  /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.random"/>
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
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxValue);

      return int.CreateChecked(double.Floor(maxValue * Sample()));
    }

    /// <summary>Returns a random integer that is within a specified range.</summary>
    /// <returns>A random integer that is within a specified range, greater than or equal to specified minValue, and less than the specified maxValue.</returns>
    public override int Next(int minValue, int maxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfGreaterThan(minValue, maxValue);

      return minValue + Next(maxValue - minValue);
    }

    /// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
    public override void NextBytes(byte[] buffer)
    {
      System.ArgumentNullException.ThrowIfNull(buffer);

      for (var index = 0; index < buffer.Length;)
      {
        var value = SampleUInt32();
        if (buffer.Length - index >= 4)
        {
          value.WriteBytes(buffer.AsSpan(index, 4), Endianess.LittleEndian);
          index += 4;
        }
        else
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
      => double.CreateChecked(SampleUInt32() >> 8) / double.CreateChecked(1U << 24);
    //=> System.BitConverter.UInt32BitsToSingle((0x3FFU << 23) | (SampleUInt32() >> 9)) - 1; // Right shift means upper bits bias.

    /// <summary>Returns a random signed 32-bit integer in the range [0, int.MaxValue].</summary>
    public int SampleInt32()
      => int.CreateChecked(SampleUInt32() & 0x7FFF_FFFF);

    #region Implemented interfaces

    /// <summary>Returns a random unsigned 32-bit integer.</summary>
    internal abstract uint SampleUInt32();

    #endregion Implemented interfaces
  }
}
