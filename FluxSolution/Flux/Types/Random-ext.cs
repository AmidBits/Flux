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
        System.ArgumentNullException.ThrowIfNull(source);

        var buffer = new byte[count];
        source.NextBytes(buffer);
        return buffer;
      }

      #endregion

      #region NextBigInteger

      /// <summary>
      /// <para>Generates a a non-negative random <see cref="System.Numerics.BigInteger"/>, limited in size by the specified <paramref name="maxBitLength"/>.</para>
      /// <para>Since <see cref="System.Numerics.BigInteger"/> is a variable sized type of integer, one has to specify the maximum number of bits to utilize when generating (rather than specifying a maximum value.</para>
      /// <para>To generate a <see cref="System.Numerics.BigInteger"/> limited by a maximumValue (exclusive) use <see cref="NextNumber{TNumber}(Random, TNumber)"/>.</para>
      /// <para>To generate a <see cref="System.Numerics.BigInteger"/> limited by a range [minimumValue, maximumValue) use <see cref="NextNumber{TNumber}(Random, TNumber, TNumber)"/>.</para>
      /// </summary>
      /// <param name="maxBitLength"></param>
      /// <returns></returns>
      public System.Numerics.BigInteger NextBigInteger(int maxBitLength)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxBitLength);

        var byteCount = int.CreateChecked(double.Ceiling(maxBitLength / 8.0));

        var rarray = System.Buffers.ArrayPool<byte>.Shared.Rent(byteCount);
        var span = rarray.AsSpan(0, byteCount);
        source.NextBytes(span);
        var bi = new System.Numerics.BigInteger(span, true);
        System.Buffers.ArrayPool<byte>.Shared.Return(rarray);
        bi >>>= (byteCount * 8 - maxBitLength);
        return bi;
      }

      #endregion

      #region NextBoolean

      /// <summary>
      /// <para>Generates a new random boolean value.</para>
      /// </summary>
      /// <returns></returns>
      public bool NextBoolean()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.Next(2) == 0;
      }

      #endregion // NextBoolean

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
        System.ArgumentNullException.ThrowIfNull(source);
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

      #region NextNumber

      public TNumber NextNumber<TNumber>()
        where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IMinMaxValue<TNumber>
        => NextNumber(source, TNumber.MaxValue);

      /// <summary>
      /// <para>Returns a non-negative random <typeparamref name="TNumber"/> in the interval [0, <paramref name="maxValue"/>).</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public TNumber NextNumber<TNumber>(TNumber maxValue)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxValue);

        while (true)
          if (TNumber.CreateChecked(source.NextDouble() * double.CreateChecked(maxValue)) is var value && value >= TNumber.Zero && value < maxValue)
            return value;
      }

      /// <summary>
      /// <para>Returns a random <typeparamref name="TNumber"/> in the interval [<paramref name="minValue"/>, <paramref name="maxValue"/>).</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public TNumber NextNumber<TNumber>(TNumber minValue, TNumber maxValue)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minValue, maxValue);

        return minValue + source.NextNumber(maxValue - minValue);
      }

      #endregion // NextNumber

      #region NextDouble

      /// <summary>
      /// <para>Creates a new non-negative random double in the interval [0, <paramref name="maxValue"/>).</para>
      /// </summary>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public double NextDouble(double maxValue)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxValue);

        return source.NextDouble() * maxValue;
      }

      /// <summary>
      /// <para>Creates a new random double in the interval [<paramref name="minValue"/>, <paramref name="maxValue"/>).</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public double NextDouble(double minValue, double maxValue)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minValue, maxValue);

        return minValue + source.NextDouble() * (maxValue - minValue);
      }

      #endregion // NextDouble

      #region NextExponential

      /// <summary>
      /// <para>Get exponential random sample with mean = 1.</para>
      /// </summary>
      /// <returns></returns>
      public double NextExponential()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return -double.Log(NextUniform(source));
      }

      /// <summary>
      /// <para>Get exponential random sample with specified <paramref name="mu"/> (mean).</para>
      /// </summary>
      /// <param name="mu">The mean of the distribution.</param>
      /// <param name="sigma">The standard deviation.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public double NextExponential(double mu, double sigma)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sigma);

        return mu + sigma * NextExponential(source);
      }

      #endregion // NextExponential

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
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfNegative(sigma);

        var mag = sigma * double.Sqrt(-2.0 * double.Log(NextNormal(source)));

        var (sin, cos) = double.SinCos(double.Tau * source.NextDouble());

        return (mag * cos + mu, mag * sin + mu);
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
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfNegative(stdDev);

        double u, v, s;

        do
        {
          u = source.NextDouble() * 2.0 - 1.0;
          v = source.NextDouble() * 2.0 - 1.0;
          s = u * u + v * v;
        }
        while (s >= 1.0 || s == 0.0);

        s = stdDev * double.Sqrt(-2.0 * double.Log(s) / s);

        return (mean + s * u, mean + s * v);
      }

      #endregion

      #region NextGuid

      /// <summary>
      /// <para>Returns a new instance of a Guid structure by using an array of random bytes.</para>
      /// </summary>
      /// <returns></returns>
      public System.Guid NextGuid()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var rarray = System.Buffers.ArrayPool<byte>.Shared.Rent(16);
        var span = new System.Span<byte>(rarray, 0, 16);
        source.NextBytes(span);
        var guid = new System.Guid(span);
        System.Buffers.ArrayPool<byte>.Shared.Return(rarray);
        return guid;
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
        => NextUniform(source) is var u && u < 0.5 ? mean + scale * double.Log(2 * u) : mean - scale * double.Log(2 * (1 - u));

      #endregion

      #region NextLogNormal

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="mu">The mean of the distribution.</param>
      /// <param name="sigma">The standard deviation.</param>
      /// <returns></returns>
      public double NextLogNormal(double mu = 0, double sigma = 1)
        => double.Exp(NextNormal(source, mu, sigma));

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
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfNegative(sigma);

        return mu + sigma * double.Sqrt(-2 * double.Log(NextUniform(source))) * double.Sin(double.Tau * NextUniform(source));
      }

      #endregion

      /// <summary>
      /// <para>Cannot for the life of me remember where I found this... so use with caution.</para>
      /// </summary>
      /// <returns></returns>
      public double NextNormalApproximation()
      {
        var u0 = source.NextInt64();
        var u1 = source.NextInt64();

        var bd = long.PopCount(u0 & 0xffffffff) - long.PopCount(u0 >> 32);

        var a = u1 & 0xffffffff;
        var b = u1 >> 32;

        var td = a - b;

        double r = (bd << 30) + td;

        return r * 2.14731e-10;
      }

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
      /// <param name="minTimeSpan"></param>
      /// <param name="maxTimeSpan"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.TimeSpan NextTimeSpan(System.TimeSpan minTimeSpan, System.TimeSpan maxTimeSpan)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minTimeSpan, maxTimeSpan);

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
        System.ArgumentNullException.ThrowIfNull(source);

        double uniform;

        do
        {
          uniform = source.NextDouble();
        }
        while (uniform <= 0 && uniform >= 1);

        return uniform;
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
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(shape);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(scale);

        return scale * double.Pow(-double.Log(NextUniform(source)), 1.0 / shape);
      }

      #endregion
    }
  }
}
