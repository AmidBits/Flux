//Implement this (down a bit in the article) : https://stackoverflow.com/questions/218060/random-gaussian-variables

namespace Flux
{
  public static partial class Reflection
  {
    // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netstandard-2.0

    /// <summary>Generates an array with the specified <paramref name="count"/> of random bytes.</summary>
    public static byte[] GetRandomBytes(this System.Random source, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var buffer = new byte[count];
      source.NextBytes(buffer);
      return buffer;
    }

    /// <summary>Returns a non-negative random BigInteger that is less than the specified <paramref name="maxValue"/>.</summary>
    public static System.Numerics.BigInteger NextBigInteger(this System.Random source, System.Numerics.BigInteger maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (maxValue <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxValue));

      var maxValueBytes = maxValue.ToByteArrayEx(out var msbIndex, out var msbValue); // Already checked for positive integer, so no padding byte is present.
      var maxByteBitMask = (byte)((uint)msbValue).BitFoldRight(); // (byte)((1 << msbValue.GetBitLengthEx()) - 1);
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

    /// <summary>Returns a random BigInteger that is within a specified range, i.e. greater or equal to <paramref name="minValue"/> and less than <paramref name="maxValue"/>.</summary>
    public static System.Numerics.BigInteger NextBigInteger(this System.Random source, System.Numerics.BigInteger minValue, System.Numerics.BigInteger maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (minValue >= maxValue) throw new System.ArgumentOutOfRangeException(nameof(minValue));

      return minValue + source.NextBigInteger(maxValue - minValue);
    }

    /// <summary>Generates a a non-negative random BigInteger limited in size by the specified <paramref name="maxBitLength"/> (bit length is used since BigInteger can basically represent an unlimited number).</summary>
    public static System.Numerics.BigInteger NextBigInteger(this System.Random source, int maxBitLength)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (maxBitLength <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxBitLength));

      var quotient = System.Math.DivRem(maxBitLength, 8, out var remainder);

      return new System.Numerics.BigInteger(GetRandomBytes(source, quotient + (remainder > 0 ? 1 : 0)), true) & ((System.Numerics.BigInteger.One << maxBitLength) - 1);
    }

    /// <summary>Generates a random boolean value.</summary>
    public static bool NextBoolean(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.Next(2) == 1;
    }

    /// <summary>Generates a boolean value with the specified <paramref name="probability"/> of being true. This function can be called a Bernoulli trial.</summary>
    public static bool NextBoolean(this System.Random source, double probability = 0.5)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.NextDouble() < probability;
    }

    /// <remarks>Apply inverse of the Cauchy distribution function to a random sample.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Cauchy_distribution"/>
    public static double NextCauchy(this System.Random source, double mu, double scale)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (scale <= 0) throw new System.ArgumentOutOfRangeException(nameof(scale));

      return mu + scale * System.Math.Tan(System.Math.PI * (NextUniform(source) - 0.5));
    }

    /// <summary>Returns a random System.DateTime in the interval [<see cref="System.DateTime.MinValue"/>, <see cref="System.DateTime.MaxValue"/>).</summary>
    public static System.DateTime NextDateTime(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.NextDateTime(System.DateTime.MinValue, System.DateTime.MaxValue);
    }

    /// <summary>Returns a random System.DateTime in the interval [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    public static System.DateTime NextDateTime(this System.Random source, System.DateTime minValue, System.DateTime maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (minValue >= maxValue) throw new System.ArgumentOutOfRangeException(nameof(minValue));

      return new(minValue.Ticks + source.NextInt64(maxValue.Ticks - minValue.Ticks));
    }

    /// <summary>Returns a non-negative random double in the interval [0, <paramref name="maxValue"/>).</summary>
    public static double NextDouble(this System.Random source, double maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (maxValue <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxValue));

      return source.NextDouble() * maxValue;
    }

    /// <summary>Returns a random double in the interval [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    public static double NextDouble(this System.Random source, double minValue, double maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (minValue >= maxValue) throw new System.ArgumentOutOfRangeException(nameof(minValue));

      return minValue + source.NextDouble(maxValue - minValue);
    }

    /// <summary>Get exponential random sample with mean = 1.</summary>
    public static double NextExponential(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return -System.Math.Log(NextUniform(source));
    }

    /// <summary>Get exponential random sample with specified <paramref name="mu"/> (mean).</summary>
    public static double NextExponential(this System.Random source, double mu)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (mu <= 0) throw new System.ArgumentOutOfRangeException(nameof(mu));

      return mu * NextExponential(source);
    }

    /// <summary>
    /// <para>Generates a pseudo-random number using the Box-Muller sampling method by generating pairs of independent standard normal random variables.</para>
    /// <seealso href="https://en.wikipedia.org/wiki/Box-Muller_transform"/>
    /// </summary>
    /// <param name="mu">The mean of the distribution.</param>
    /// <param name="sigma">Standard deviation.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static (double z0, double z1) NextBoxMullerTransform(this System.Random source, double mu = 0, double sigma = 1)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      double u1, u2;

      do
      {
        u1 = source.NextDouble();
        u2 = source.NextDouble();
      }
      while (u1 <= double.Epsilon || u2 <= double.Epsilon);

      var s = System.Math.Sqrt(-2 * System.Math.Log(u1));

      var z0 = s * System.Math.Cos(System.Math.Tau * u2);
      var z1 = s * System.Math.Sin(System.Math.Tau * u2);

      return (z0 * sigma + mu, z1 * sigma + mu);
    }

    /// <summary>
    /// <para>The Marsaglia polar method is a pseudo-random number sampling method for generating a pair of independent standard normal random variables.</para>
    /// <see href="https://en.wikipedia.org/wiki/Marsaglia_polar_method"/>
    /// </summary>
    /// <param name="mu">The mean of the distribution.</param>
    /// <param name="sigma">Standard deviation.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static (double z0, double z1) NextMarsagliaPolarMethod(this System.Random source, double mu = 0, double sigma = 1)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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

      return (z0 * sigma + mu, z1 * sigma + mu);
    }

    /// <summary>Returns a new instance of a Guid structure by using an array of random bytes.</summary>
    public static System.Guid NextGuid(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return new(GetRandomBytes(source, 16));
    }

    /// <summary>The Laplace distribution is also known as the double exponential distribution.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Laplace_distribution"/>
    public static double NextLaplace(this System.Random source, double mean, double scale)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return NextUniform(source) is var u && u < 0.5 ? mean + scale * System.Math.Log(2 * u) : mean - scale * System.Math.Log(2 * (1 - u));
    }

    public static double NextLogNormal(this System.Random source, double mu, double sigma)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return System.Math.Exp(NextNormal(source, mu, sigma));
    }

    /// <summary>Using the Box-Muller algorithm.</summary>
    public static double NextNormal(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return System.Math.Sqrt(-2 * System.Math.Log(NextUniform(source))) * System.Math.Sin(Maths.PiOver2 * NextUniform(source));
    }

    /// <summary>Get normal (Gaussian) random sample with specified mean and standard deviation.</summary>
    public static double NextNormal(this System.Random source, double mean, double sigma)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (sigma <= 0) throw new System.ArgumentOutOfRangeException(nameof(sigma));

      return mean + sigma * NextNormal(source);
    }

    /// <summary>Returns a random System.TimeSpan in the interval [<see cref="System.TimeSpan.MinValue"/>, <see cref="System.TimeSpan.MaxValue"/>).</summary>
    public static System.TimeSpan NextTimeSpan(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.NextTimeSpan(System.TimeSpan.MinValue, System.TimeSpan.MaxValue);
    }

    /// <summary>Returns a random System.TimeSpan in the interval [<paramref name="minValue"/>, <paramref name="maxValue"/>).</summary>
    public static System.TimeSpan NextTimeSpan(this System.Random source, System.TimeSpan minValue, System.TimeSpan maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (minValue.Ticks >= maxValue.Ticks) throw new System.ArgumentOutOfRangeException(nameof(minValue));

      return new(minValue.Ticks + source.NextInt64(maxValue.Ticks - minValue.Ticks));
    }

    /// <summary>Returns a random System.UInt32 in the interval [0, <see cref="System.UInt32.MaxValue"/>), but will never return <see cref="System.UInt32.MaxValue"/>.</summary>
    [System.CLSCompliant(false)]
    public static uint NextUInt32(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var uint32 = System.UInt32.MaxValue;

      while (uint32 == System.UInt32.MaxValue)
        uint32 = System.BitConverter.ToUInt32(GetRandomBytes(source, 4), 0);

      return uint32;
    }

    /// <summary>Returns a random System.UInt64 in the interval [0, <see cref="System.UInt64.MaxValue"/>), but will never return <see cref="System.UInt64.MaxValue"/>.</summary>
    [System.CLSCompliant(false)]
    public static ulong NextUInt64(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var uint64 = System.UInt64.MaxValue;

      while (uint64 == System.UInt64.MaxValue)
        uint64 = System.BitConverter.ToUInt64(GetRandomBytes(source, 8), 0);

      return uint64;
    }

    /// <summary>Produce a uniform random sample from the open interval (0, 1), i.e. the method will not return either end point.</summary>
    public static double NextUniform(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.NextDouble() is var sample && sample > 0 && sample < 1 ? sample : double.Epsilon;
    }

    /// <see cref="https://en.wikipedia.org/wiki/Weibull_distribution"/>
    public static double NextWeibull(this System.Random source, double shape, double scale)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (shape <= 0) throw new System.ArgumentOutOfRangeException(nameof(shape));
      if (scale <= 0) throw new System.ArgumentOutOfRangeException(nameof(scale));

      return scale * System.Math.Pow(-System.Math.Log(NextUniform(source)), 1 / shape);
    }
  }
}
