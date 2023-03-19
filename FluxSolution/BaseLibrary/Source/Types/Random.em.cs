//Implement this (down a bit in the article) : https://stackoverflow.com/questions/218060/random-gaussian-variables

namespace Flux
{
  public static partial class RandomEm
  {
    // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netstandard-2.0

    /// <summary>Generates an array with the specified count of random bytes.</summary>
    public static byte[] GetRandomBytes([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var buffer = new byte[count];
      source.NextBytes(buffer);
      return buffer;
    }
    /// <summary>Generates a span with the specified count of random bytes.</summary>
    //public static System.ReadOnlySpan<byte> GetRandomByteSpan([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, int count)
    //  => GetRandomBytes(source, count);

    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetRandomBigIntegers([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, System.Numerics.BigInteger minValue, System.Numerics.BigInteger maxValue)
    {
      while (true)
        yield return source.NextBigInteger(minValue, maxValue);
    }
    public static System.Collections.Generic.IEnumerable<int> GetRandomInt32s([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, int minValue, int maxValue)
    {
      while (true)
        yield return source.Next(minValue, maxValue);
    }
    public static System.Collections.Generic.IEnumerable<long> GetRandomInt64s([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, long minValue, long maxValue)
    {
      while (true)
        yield return source.NextInt64(minValue, maxValue);
    }

    /// <summary>Returns a non-negative random BigInteger that is less than the specified maxValue.</summary>
    public static System.Numerics.BigInteger NextBigInteger([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, System.Numerics.BigInteger maxValue)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (maxValue <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxValue), $"Maximum value ({maxValue}) must be greater than 0.");

      var maxValueBytes = maxValue.ToByteArrayEx(out var msbIndex, out var msbValue); // Already checked for positive integer, so no padding byte is present.
      var maxByteBitMask = (byte)((1 << msbValue.GetBitLengthEx()) - 1);
      var maxIndex = maxValueBytes.Length - 1;
      var hasPaddingByte = msbIndex < maxIndex;

      System.Numerics.BigInteger value;

      do
      {
        source.NextBytes(maxValueBytes);

        if (hasPaddingByte)
          maxValueBytes[maxIndex] = 0; // Zero out the highest byte, if needed.

        maxValueBytes[msbIndex] &= maxByteBitMask; // Constrain the random value by masking the most significant byte of the array.

        value = new System.Numerics.BigInteger(maxValueBytes);
      }
      while (value >= maxValue); // If value is greater or equal to the specified maximum value, maintain uniform distribution by re-running if value is greater or equal to the specified maximum value (a fifty-fifty situation).

      return value;
    }
    /// <summary>Returns a random BigInteger that is within a specified range, i.e. greater or equal to minValue and less than maxValue.</summary>
    public static System.Numerics.BigInteger NextBigInteger([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, System.Numerics.BigInteger minValue, System.Numerics.BigInteger maxValue)
      => (minValue < maxValue) ? NextBigInteger(source, maxValue - minValue) + minValue : throw new System.ArgumentOutOfRangeException(nameof(minValue), $"The minValue ({minValue}) must be less than the maxValue ({maxValue}).");

    /// <summary>Generates a a non-negative random BigInteger limited in size by the specified maxBitLength (bit length is used since BigInteger can basically represent an unlimited number).</summary>
    public static System.Numerics.BigInteger NextBigInteger([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, int maxBitLength)
    {
      var quotient = System.Math.DivRem(maxBitLength, 8, out var remainder);

      return new System.Numerics.BigInteger(GetRandomBytes(source, quotient + (remainder > 0 ? 1 : 0)), true) & ((System.Numerics.BigInteger.One << maxBitLength) - 1);
    }

    /// <summary>Generates a boolean value with the specified probability of being true. This function can be called a Bernoulli trial.</summary>
    public static bool NextBoolean([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double probabilityOfTrue = 0.5)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).NextDouble() < probabilityOfTrue;

    /// <remarks>Apply inverse of the Cauchy distribution function to a random sample.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Cauchy_distribution"/>
    public static double NextCauchy([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double median, double scale)
      => (scale > 0) ? median + scale * System.Math.Tan(System.Math.PI * (NextUniform(source) - 0.5)) : throw new System.ArgumentException($"The scale ({scale}) parameter must be positive.");

    /// <summary>Returns a random System.TimeSpan in the range [System.DateTime.MinValue, System.DateTime.MaxValue].</summary>
    public static System.DateTime NextDateTime([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => new(NextInt64(source, System.DateTime.MaxValue.Ticks));
    /// <summary>Returns a random System.TimeSpan in the range [minValue, maxValue].</summary>
    public static System.DateTime NextDateTime([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, System.DateTime maxValue)
      => new(NextInt64(source, System.DateTime.MinValue.Ticks, maxValue.Ticks));
    /// <summary>Returns a random System.TimeSpan in the range [minValue, maxValue].</summary>
    public static System.DateTime NextDateTime([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, System.DateTime minValue, System.DateTime maxValue)
      => new(NextInt64(source, minValue.Ticks, maxValue.Ticks));

    /// <summary>Returns a non-negative random double that is within a specified range.</summary>
    public static double NextDouble([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double maxValue)
      => maxValue > 0 ? (source ?? throw new System.ArgumentNullException(nameof(source))).NextDouble() * maxValue : throw new System.ArgumentOutOfRangeException(nameof(maxValue), $"{maxValue} > 0");
    /// <summary>Returns a random double that is within a specified range.</summary>
    public static double NextDouble([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double minValue, double maxValue)
      => minValue < maxValue ? NextDouble(source, maxValue - minValue) + minValue : throw new System.ArgumentOutOfRangeException(nameof(maxValue), $"{minValue} < {maxValue}");

    /// <summary>Get exponential random sample with mean 1.</summary>
    public static double NextExponential([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => -System.Math.Log(NextUniform(source));
    /// <summary>Get exponential random sample with specified mean.<.summary>
    public static double NextExponential([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double mean)
      => (mean > 0) ? mean * NextExponential(source) : throw new System.ArgumentOutOfRangeException(nameof(mean), $"{mean} > 0");

    /// <summary>Generates a pseudo-random number using the Box-Muller sampling method by generating pairs of independent standard normal random variables.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Box–Muller_transform"/>
    public static (double z0, double z1) NextBoxMullerTransform([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double mean = 0, double stdDev = 1)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      double u1, u2;

      do
      {
        u1 = source.NextDouble();
        u2 = source.NextDouble();
      }
      while (u1 <= double.Epsilon || u2 <= double.Epsilon);

      var z0 = System.Math.Sqrt(-2 * System.Math.Log(u1)) * System.Math.Cos(double.Tau * u2);
      var z1 = System.Math.Sqrt(-2 * System.Math.Log(u1)) * System.Math.Sin(double.Tau * u2);

      return (z0 * stdDev + mean, z1 * stdDev + mean);
    }

    /// <summary>The Marsaglia polar method is a pseudo-random number sampling method for generating a pair of independent standard normal random variables.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Marsaglia_polar_method"/>
    public static (double z0, double z1) NextMarsagliaPolarMethod([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double mean = 0, double stdDev = 1)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      double u, v, s;

      do
      {
        u = source.NextDouble() * 2 - 1;
        v = source.NextDouble() * 2 - 1;
        s = u * u + v * v;
      }
      while (s >= 1 || s == 0);

      s = System.Math.Sqrt(-2.0 * System.Math.Log(s) / s);

      var z0 = u * s;
      var z1 = v * s;

      return (z0 * stdDev + mean, z1 * stdDev + mean);
    }

    /// <summary>Returns a new instance of a Guid structure by using an array of random bytes.</summary>
    public static System.Guid NextGuid([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => new(GetRandomBytes(source, 16));

    ///// <summary>Returns a non-negative random Int32.</summary>
    //public static int NextInt32(this System.Random source)
    //  => NextInt32(source, int.MaxValue);
    ///// <summary>Returns a non-negative random Int32 that is less than the specified maxValue.</summary>
    //public static int NextInt32(this System.Random source, int maxValue)
    //  => maxValue >= 2 ? (int)System.Math.Floor(maxValue * (source ?? throw new System.ArgumentNullException(nameof(source))).NextDouble()) : throw new System.ArgumentNullException(nameof(maxValue));
    ///// <summary>Returns a random Int32 that is within a specified range.</summary>
    //public static int NextInt32(this System.Random source, int minValue, int maxValue)
    //  => minValue < maxValue ? minValue + NextInt32(source, maxValue - minValue) : throw new System.ArgumentNullException(nameof(minValue));

    /// <summary>Returns a non-negative random Int64.</summary>
    public static long NextInt64([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => NextInt64(source, long.MaxValue);
    /// <summary>Returns a non-negative random Int64 that is less than the specified maxValue.</summary>
    public static long NextInt64([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, long maxValue)
      => maxValue >= 2 ? (long)System.Math.Floor(maxValue * (source ?? throw new System.ArgumentNullException(nameof(source))).NextDouble()) : throw new System.ArgumentOutOfRangeException(nameof(maxValue), $"{maxValue} > 0");
    /// <summary>Returns a random Int64 that is within a specified range.</summary>
    public static long NextInt64([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, long minValue, long maxValue)
      => minValue < maxValue ? minValue + NextInt64(source, maxValue - minValue) : throw new System.ArgumentOutOfRangeException(nameof(minValue), $"{minValue} < {maxValue}");

    /// <summary>Returns a random System.Net.IPAddress = IPv4.</summary>
    public static System.Net.IPAddress NextIPv4Address([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => new(GetRandomBytes(source, 4));

    /// <summary>Returns a random System.Net.IPAddress = IPv6.</summary>
    public static System.Net.IPAddress NextIPv6Address([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => new(GetRandomBytes(source, 16));

    /// <summary>The Laplace distribution is also known as the double exponential distribution.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Laplace_distribution"/>
    public static double NextLaplace([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double mean, double scale)
      => NextUniform(source) is var u && u < 0.5 ? mean + scale * System.Math.Log(2 * u) : mean - scale * System.Math.Log(2 * (1 - u));

    public static double NextLogNormal([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double mu, double sigma)
      => System.Math.Exp(NextNormal(source, mu, sigma));

    /// <summary>Using the Box-Muller algorithm.</summary>
    public static double NextNormal([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => System.Math.Sqrt(-2 * System.Math.Log(NextUniform(source))) * System.Math.Sin(GenericMath.PiOver2 * NextUniform(source));
    /// <summary>Get normal (Gaussian) random sample with specified mean and standard deviation.</summary>
    public static double NextNormal([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double mean, double standardDeviation)
      => standardDeviation > 0 ? mean + standardDeviation * NextNormal(source) : throw new System.ArgumentOutOfRangeException(nameof(standardDeviation), $"{standardDeviation} > 0");

    // Preliminary to be in place.
    public static float NextSingle([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => System.Convert.ToSingle(source.NextDouble(float.MinValue, float.MaxValue));
    // Preliminary to be in place.
    public static float NextSingle([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, float maxValue)
      => System.Convert.ToSingle(source.NextDouble(float.MinValue, maxValue));
    // Preliminary to be in place.
    public static float NextSingle([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, float minValue, float maxValue)
      => System.Convert.ToSingle(source.NextDouble(minValue, maxValue));

    /// <summary>Returns a random System.TimeSpan in the range [System.TimeSpan.MinValue, System.TimeSpan.MaxValue].</summary>
    public static System.TimeSpan NextTimeSpan([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => new(NextInt64(source));
    /// <summary>Returns a random System.TimeSpan in the range [minValue, maxValue].</summary>

    public static System.TimeSpan NextTimeSpan([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, System.TimeSpan minValue, System.TimeSpan maxValue)
      => new(NextInt64(source, minValue.Ticks, maxValue.Ticks));

    /// <summary>Returns a random System.UInt32 in the range [0, System.UInt32.MaxValue], but will never return uint.MaxValue.</summary>
    [System.CLSCompliant(false)]
    public static uint NextUInt32([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => System.BitConverter.ToUInt32(GetRandomBytes(source, 4), 0) is var v && v == System.UInt32.MaxValue ? System.UInt32.MaxValue - System.BitConverter.ToUInt32(GetRandomBytes(source, 4), 0) : v;

    /// <summary>Returns a random System.UInt64 in the range [0, System.UInt64.MaxValue], but will never return ulong.MaxValue.</summary>
    [System.CLSCompliant(false)]
    public static ulong NextUInt64([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => System.BitConverter.ToUInt64(GetRandomBytes(source, 8), 0) is var v && v == System.UInt64.MaxValue ? System.UInt64.MaxValue - System.BitConverter.ToUInt64(GetRandomBytes(source, 8), 0) : v;

    /// <summary>Produce a uniform random sample from the open interval [0, 1]. The method will not return either end point.</summary>
    public static double NextUniform([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).NextDouble() is var sample && sample > 0 && sample < 1 ? sample : sample == 0 ? double.Epsilon : sample == 1 ? 1 - double.Epsilon : throw new System.ArgumentOutOfRangeException(nameof(source), $"NextDouble() returned an invalid value ({sample}).");

    /// <see cref="https://en.wikipedia.org/wiki/Weibull_distribution"/>
    public static double NextWeibull([System.Diagnostics.CodeAnalysis.DisallowNull] this System.Random source, double shape, double scale)
      => shape > 0 ? scale > 0 ? scale * System.Math.Pow(-System.Math.Log(NextUniform(source)), 1 / shape) : throw new System.ArgumentOutOfRangeException(nameof(scale), $"{scale} > 0") : throw new System.ArgumentOutOfRangeException(nameof(shape), $"{shape} > 0");
  }
}
