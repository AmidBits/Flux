//Implement this (down a bit in the article) : https://stackoverflow.com/questions/218060/random-gaussian-variables

namespace Flux
{
  public static partial class RandomExtensions
  {
    // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netstandard-2.0

    extension(System.Random source)
    {
      #region GetNextBytes

      /// <summary>
      /// <para>Generates an array with the specified <paramref name="count"/> of random bytes.</para>
      /// </summary>
      public byte[] GetNextBytes(int count)
      {
        var buffer = new byte[count];
        source.NextBytes(buffer);
        return buffer;
      }

      #endregion

      #region NextNBitBigInteger

      /// <summary>
      /// <para>NextBigInteger only exists with a parameter of <paramref name="bitCount"/>, i.e. kind of like maxValue for fixed-size integers, but by specifying the number of bits instead. Using maxValue or minValue/maxValue with BigInteger is still possible with NextInteger.</para>
      /// </summary>
      /// <param name="bitCount"></param>
      /// <returns></returns>
      public System.Numerics.BigInteger NextNBitBigInteger(int bitCount)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bitCount);

        var byteCount = (bitCount + 7) >> 3;

        var bytes = (stackalloc byte[byteCount]);

        source.NextBytes(bytes);

        var excessBits = (byteCount << 3) - bitCount;

        bytes[0] &= (byte)(0xFF >> excessBits);

        return new(bytes, true, true);
      }

      //public System.Numerics.BigInteger NextBigInteger(System.Numerics.BigInteger maxValue)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxValue);

      //  var bitCount = (int)System.Numerics.BigInteger.Log2(maxValue - 1) + 1; // How many bits are needed to represent (maxValue - 1).

      //  System.Numerics.BigInteger value;

      //  do
      //  {
      //    value = NextNBitBigInteger(source, bitCount); // uniform in [0, 2^bitCount)
      //  }
      //  while (value >= maxValue);

      //  return value;
      //}

      //public System.Numerics.BigInteger NextBigInteger(System.Numerics.BigInteger minValue, System.Numerics.BigInteger maxValue)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxValue, minValue);

      //  return minValue + NextBigInteger(source, maxValue - minValue);
      //}

      #endregion

      #region NextBoolean

      /// <summary>
      /// <para>Generates a new random boolean value.</para>
      /// </summary>
      /// <returns></returns>
      public bool NextBoolean()
        => source.Next(2) == 0;

      #endregion

      #region NextCauchy

      /// <summary>
      /// <para><remarks>Apply inverse of the Cauchy distribution function to a random sample.</remarks></para>
      /// <para>The special case when <paramref name="mu"/> (x0) = 0 and <paramref name="scale"/> (gamma) = 1 is called the standard Cauchy distribution.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Cauchy_distribution"/></para>
      /// </summary>
      /// <param name="mu">The mean of the distribution.</param>
      /// <param name="scale">The standard deviation.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public double NextCauchy(double mu = 0, double scale = 1)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(scale);

        return mu + scale * double.TanPi(NextUniform(source) - 0.5);
      }

      #endregion

      #region NextDateTime

      /// <summary>
      /// <para>Returns a new random System.DateTime in the interval [<see cref="System.DateTime.MinValue"/>, <see cref="System.DateTime.MaxValue"/>).</para>
      /// </summary>
      /// <returns></returns>
      public System.DateTime NextDateTime()
        => NextDateTime(source, System.DateTime.MinValue, System.DateTime.MaxValue);

      /// <summary>
      /// <para>Returns a new random System.DateTime in the interval [<paramref name="minDateTime"/>, <paramref name="maxDateTime"/>).</para>
      /// </summary>
      /// <param name="minDateTime"></param>
      /// <param name="maxDateTime"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.DateTime NextDateTime(System.DateTime minDateTime, System.DateTime maxDateTime)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minDateTime, maxDateTime);

        return new(source.NextInt64(minDateTime.Ticks, maxDateTime.Ticks));
      }

      #endregion // NextDateTime

      #region NextDecimal

      public decimal NextDecimal(decimal maxValue)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(maxValue);

        var int96 = NextNBitBigInteger(source, 96); // Generate a uniform integer in [0, 2^96)

        var scale = (decimal)System.Numerics.BigInteger.Pow(2, 96); // Convert 2^96 to decimal (fits exactly)

        return (decimal)int96 / scale * maxValue; // Scale into [0, maxValue)
      }

      public decimal NextDecimal(decimal minValue, decimal maxValue)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(maxValue, minValue);

        return minValue + NextDecimal(source, maxValue - minValue);
      }

      #endregion

      #region NextDouble

      public double NextDouble(double maxValue)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(maxValue);

        return source.NextDouble() * maxValue;
      }

      public double NextDouble(double minValue, double maxValue)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(maxValue, minValue);

        return minValue + source.NextDouble() * (maxValue - minValue);
      }

      #endregion

      #region NextInteger

      public TInteger NextInteger<TInteger>()
        where TInteger : System.Numerics.IBinaryInteger<TInteger>, System.Numerics.IMinMaxValue<TInteger>
        => NextInteger(source, TInteger.MaxValue);

      public TInteger NextInteger<TInteger>(TInteger maxValue)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxValue);

        var bitCount = int.CreateChecked(TInteger.Log2(maxValue - TInteger.One) + TInteger.One); // Compute bit width needed.

        TInteger value;

        do
        {
          value = TInteger.CreateTruncating(NextNBitBigInteger(source, bitCount));
        }
        while (value >= maxValue);

        return value;
      }

      public TInteger NextInteger<TInteger>(TInteger minValue, TInteger maxValue)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxValue, minValue);

        return minValue + NextInteger(source, maxValue - minValue);
      }

      #endregion

      #region NextExponential

      /// <summary>
      /// <para>Get exponential random sample with mean = 1.</para>
      /// </summary>
      /// <returns></returns>
      public double NextExponential()
        => -double.Log(NextUniform(source));

      /// <summary>
      /// <para>Get exponential random sample with specified <paramref name="mu"/> (mean).</para>
      /// </summary>
      /// <param name="mu">The mean of the distribution.</param>
      /// <param name="sigma">The standard deviation.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public double NextExponential(double mu, double sigma)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sigma);

        return mu + sigma * NextExponential(source);
      }

      #endregion 

      #region NextBoxMullerTransform

      /// <summary>
      /// <para>Generates a pseudo-random number using the Box-Muller sampling method by generating pairs of independent standard normal random variables.</para>
      /// <seealso href="https://en.wikipedia.org/wiki/Box-Muller_transform"/>
      /// </summary>
      /// <param name="mu">The mean of the distribution.</param>
      /// <param name="sigma">The standard deviation.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      public (double Z0, double Z1) NextBoxMullerTransform(double mu = 0, double sigma = 1)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sigma);

        var u1 = NextUniform(source); // (0,1)
        var u2 = NextUniform(source); // (0,1)

        var mag = sigma * double.Sqrt(-2.0 * double.Log(u1));

        var (sin, cos) = double.SinCos(double.Tau * u2);

        return (mu + mag * cos, mu + mag * sin);
      }

      #endregion

      #region NextMarsagliaPolarMethod

      /// <summary>
      /// <para>The Marsaglia polar method is a pseudo-random number sampling method for generating a pair of independent standard normal random variables.</para>
      /// <see href="https://en.wikipedia.org/wiki/Marsaglia_polar_method"/>
      /// </summary>
      /// <param name="mean">The mean of the distribution.</param>
      /// <param name="stdDev">The standard deviation.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      public (double Z0, double Z1) NextMarsagliaPolarMethod(double mean = 0, double stdDev = 1)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(stdDev);

        double u, v, s;

        do
        {
          u = 2.0 * NextUniform(source) - 1.0;
          v = 2.0 * NextUniform(source) - 1.0;

          s = u * u + v * v;
        }
        while (s >= 1.0 || s == 0.0);

        var m = double.Sqrt(-2.0 * double.Log(s) / s) * stdDev;

        return (mean + m * u, mean + m * v);
      }

      #endregion

      #region NextGuid

      /// <summary>
      /// <para>Returns a new instance of a Guid structure by using an array of random bytes.</para>
      /// </summary>
      /// <returns></returns>
      public System.Guid NextGuid()
      {
        var span = (stackalloc byte[16]);
        source.NextBytes(span);
        return new System.Guid(span);
      }

      #endregion

      #region NextLaplace

      /// <summary>
      /// <para>The Laplace distribution is also known as the double exponential distribution.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Laplace_distribution"/></para>
      /// </summary>
      /// <param name="mean">The mean of the distribution.</param>
      /// <param name="scale">The standard deviation.</param>
      /// <returns></returns>
      public double NextLaplace(double mean, double scale)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(scale);

        var u = NextUniform(source); // uniform in (0,1)
        var v = u - 0.5; // centered uniform
        var s = double.Sign(v); // ±1

        return mean - scale * s * double.Log(1 - 2 * double.Abs(v));
      }

      #endregion

      #region NextLogNormal

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="mu">The mean of the distribution.</param>
      /// <param name="sigma">The standard deviation.</param>
      /// <returns></returns>
      public double NextLogNormal(double mu = 0, double sigma = 1)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sigma);

        return double.Exp(NextNormal(source, mu, sigma));
      }

      #endregion

      #region NextNormal

      /// <summary>
      /// <para>Using the Box-Muller algorithm.</para>
      /// </summary>
      /// <param name="mu">The mean of the distribution.</param>
      /// <param name="sigma">The standard deviation.</param>
      /// <returns></returns>
      public double NextNormal(double mu = 0, double sigma = 1)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sigma);

        return mu + sigma * double.Sqrt(-2 * double.Log(NextUniform(source))) * double.Sin(double.Tau * NextUniform(source));
      }

      #endregion

      #region NextNormalApproximation

      /// <summary>
      /// <para>Cannot for the life of me remember where I found this... so use with caution.</para>
      /// </summary>
      /// <returns></returns>
      public double NextNormalApproximation()
      {
        var u0 = source.NextInt64();
        var u1 = source.NextInt64();

        var pcd = long.PopCount(u0 & 0xffffffff) - long.PopCount((u0 >>> 32) & 0xffffffff); // Popcount difference of low/high halves → approx N(0, 16)

        // Uniform difference of two 32-bit halves → approx N(0, 2^65/12)

        var a = (uint)u1;
        var b = (uint)(u1 >>> 32);

        var ud = (long)a - (long)b;

        var r = (pcd << 30) + ud; // Combine the two components.

        return r * 2.1570e-10; // Correct variance-normalizing constant.
      }

      #endregion

      #region NextTimeSpan

      /// <summary>
      /// <para>Creates a new random System.TimeSpan in the interval [<see cref="System.TimeSpan.MinValue"/>, <see cref="System.TimeSpan.MaxValue"/>).</para>
      /// </summary>
      /// <returns></returns>
      public System.TimeSpan NextTimeSpan()
        => NextTimeSpan(source, System.TimeSpan.MinValue, System.TimeSpan.MaxValue);

      /// <summary>
      /// <para>Creates a new random System.TimeSpan in the interval [<paramref name="minTimeSpan"/>, <paramref name="maxTimeSpan"/>).</para>
      /// </summary>
      /// <param name="maxTimeSpan">Exclusive.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.TimeSpan NextTimeSpan(System.TimeSpan maxTimeSpan)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTimeSpan.Ticks);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(maxTimeSpan.Ticks, System.TimeSpan.MaxValue.Ticks);

        return new(source.NextInt64(maxTimeSpan.Ticks));
      }

      /// <summary>
      /// <para>Creates a new random System.TimeSpan in the interval [<paramref name="minTimeSpan"/>, <paramref name="maxTimeSpan"/>).</para>
      /// </summary>
      /// <param name="minTimeSpan">Inclusive.</param>
      /// <param name="maxTimeSpan">Exclusive.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.TimeSpan NextTimeSpan(System.TimeSpan minTimeSpan, System.TimeSpan maxTimeSpan)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minTimeSpan, maxTimeSpan);

        System.ArgumentOutOfRangeException.ThrowIfLessThan(minTimeSpan.Ticks, System.TimeSpan.MinValue.Ticks);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(maxTimeSpan.Ticks, System.TimeSpan.MaxValue.Ticks);

        return new(source.NextInt64(minTimeSpan.Ticks, maxTimeSpan.Ticks));
      }

      #endregion // NextTimeSpan

      #region NextUniform

      /// <summary>
      /// <para>Produce a uniform random sample from the open interval (0, 1), i.e. the method will not return either end point.</para>
      /// </summary>
      /// <returns></returns>
      public double NextUniform()
      {
        var bits = (ulong)source.NextInt64() >> 11; // keep top 53 bits

        return (bits + 0.5) * double.SignificandScale;
      }

      #endregion

      #region NextWeibull

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Weibull_distribution"/></para>
      /// </summary>
      /// <param name="shape"></param>
      /// <param name="scale">The standard deviation.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public double NextWeibull(double shape, double scale)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(shape);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(scale);

        return scale * double.Pow(-double.Log(NextUniform(source)), 1.0 / shape);
      }

      #endregion
    }
  }
}
