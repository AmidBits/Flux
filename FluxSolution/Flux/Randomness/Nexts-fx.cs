//Implement this (down a bit in the article) : https://stackoverflow.com/questions/218060/random-gaussian-variables

namespace Flux
{
  public static partial class RandomExtensions
  {
    // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netstandard-2.0

    #region NextBigInteger

    /// <summary>
    /// <para>Generates a a non-negative random <see cref="System.Numerics.BigInteger"/>, limited in size by the specified <paramref name="maxBitLength"/>.</para>
    /// <para>Since <see cref="System.Numerics.BigInteger"/> is a variable sized type of integer, one has to specify the maximum number of bits to utilize when generating (rather than specifying a maximum value.</para>
    /// <para>To generate a <see cref="System.Numerics.BigInteger"/> limited by a maximumValue (exclusive) use <see cref="NextNumber{TNumber}(Random, TNumber)"/>.</para>
    /// <para>To generate a <see cref="System.Numerics.BigInteger"/> limited by a range [minimumValue, maximumValue) use <see cref="NextNumber{TNumber}(Random, TNumber, TNumber)"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="maxBitLength"></param>
    /// <returns></returns>
    public static System.Numerics.BigInteger NextBigInteger(this System.Random source, int maxBitLength)
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
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool NextBoolean(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.Next(2) == 0;
    }

    #endregion // NextBoolean

    #region NextCauchy

    /// <summary>
    /// <para><remarks>Apply inverse of the Cauchy distribution function to a random sample.</remarks></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Cauchy_distribution"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="mu"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static double NextCauchy(this System.Random source, double mu, double scale)
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
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.DateTime NextDateTime(this System.Random source)
      => NextDateTime(source, System.DateTime.MinValue, System.DateTime.MaxValue);

    /// <summary>
    /// <para>Returns a new random System.DateTime in the interval [<paramref name="minDateTime"/>, <paramref name="maxDateTime"/>).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="minDateTime"></param>
    /// <param name="maxDateTime"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.DateTime NextDateTime(this System.Random source, System.DateTime minDateTime, System.DateTime maxDateTime)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minDateTime, maxDateTime);

      return new(source.NextInt64(minDateTime.Ticks, maxDateTime.Ticks));
    }

    #endregion // NextDateTime

    #region NextNumber

    public static TNumber NextNumber<TNumber>(this System.Random source)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IMinMaxValue<TNumber>
      => NextNumber(source, TNumber.MaxValue);

    /// <summary>
    /// <para>Returns a non-negative random <typeparamref name="TNumber"/> in the interval [0, <paramref name="maxValue"/>).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber NextNumber<TNumber>(this System.Random source, TNumber maxValue)
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
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber NextNumber<TNumber>(this System.Random source, TNumber minValue, TNumber maxValue)
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
    /// <param name="source"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static double NextDouble(this System.Random source, double maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxValue);

      return source.NextDouble() * maxValue;
    }

    /// <summary>
    /// <para>Creates a new random double in the interval [<paramref name="minValue"/>, <paramref name="maxValue"/>).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static double NextDouble(this System.Random source, double minValue, double maxValue)
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
    /// <param name="source"></param>
    /// <returns></returns>
    public static double NextExponential(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return -double.Log(NextUniform(source));
    }

    /// <summary>
    /// <para>Get exponential random sample with specified <paramref name="mu"/> (mean).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="mu"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static double NextExponential(this System.Random source, double mu, double sigma)
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
    /// <param name="source"></param>
    /// <param name="mu">The mean of the distribution.</param>
    /// <param name="sigma">Standard deviation.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static (double Z0, double Z1) NextBoxMullerTransform(this System.Random source, double mu = 0, double sigma = 1)
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
    /// <param name="source"></param>
    /// <param name="mean">The mean of the distribution.</param>
    /// <param name="stdDev">Standard deviation.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static (double Z0, double Z1) NextMarsagliaPolarMethod(this System.Random source, double mean = 0, double stdDev = 1)
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
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Guid NextGuid(this System.Random source)
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
    /// <param name="source"></param>
    /// <param name="mean"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static double NextLaplace(this System.Random source, double mean, double scale)
      => NextUniform(source) is var u && u < 0.5 ? mean + scale * double.Log(2 * u) : mean - scale * double.Log(2 * (1 - u));

    #endregion

    #region NextLogNormal

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="mu"></param>
    /// <param name="sigma"></param>
    /// <returns></returns>
    public static double NextLogNormal(this System.Random source, double mu, double sigma)
      => double.Exp(NextNormal(source, mu, sigma));

    #endregion

    #region NextNormal

    /// <summary>
    /// <para>Using the Box-Muller algorithm.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static double NextNormal(this System.Random source, double mu = 0, double sigma = 1)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfNegative(sigma);

      return mu + sigma * double.Sqrt(-2 * double.Log(NextUniform(source))) * double.Sin(double.Tau * NextUniform(source));
    }

    #endregion

    //public static double NextNormalApproximation(this System.Random source)
    //{
    //  var u0 = source.NextInt64();
    //  var u1 = source.NextInt64();

    //  var bd = long.PopCount(u0 & 0xffffffff) - long.PopCount(u0 >> 32);

    //  var a = u1 & 0xffffffff;
    //  var b = u1 >> 32;

    //  var td = a - b;

    //  double r = (bd << 30) + td;

    //  return r * 2.14731e-10;
    //}

    ///// <summary>
    ///// <para>Get normal (Gaussian) random sample with specified mean and standard deviation.</para>
    ///// </summary>
    ///// <param name="source"></param>
    ///// <param name="mu"></param>
    ///// <param name="sigma"></param>
    ///// <returns></returns>
    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
    //public static double NextNormal(this System.Random source, double mu, double sigma)
    //{
    //  System.ArgumentNullException.ThrowIfNull(source);
    //  System.ArgumentOutOfRangeException.ThrowIfNegative(sigma);

    //  return mu + sigma * NextNormal(source);
    //}

    #region NextTimeSpan

    /// <summary>
    /// <para>Creates a new random System.TimeSpan in the interval [<see cref="System.TimeSpan.MinValue"/>, <see cref="System.TimeSpan.MaxValue"/>).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.TimeSpan NextTimeSpan(this System.Random source)
      => NextTimeSpan(source, System.TimeSpan.MinValue, System.TimeSpan.MaxValue);

    /// <summary>
    /// <para>Creates a new random System.TimeSpan in the interval [<paramref name="minTimeSpan"/>, <paramref name="maxTimeSpan"/>).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="minTimeSpan"></param>
    /// <param name="maxTimeSpan"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.TimeSpan NextTimeSpan(this System.Random source, System.TimeSpan minTimeSpan, System.TimeSpan maxTimeSpan)
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
    /// <param name="source"></param>
    /// <returns></returns>
    public static double NextUniform(this System.Random source)
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
    /// <param name="source"></param>
    /// <param name="shape"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static double NextWeibull(this System.Random source, double shape, double scale)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(shape);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(scale);

      return scale * double.Pow(-double.Log(NextUniform(source)), 1 / shape);
    }

    #endregion
  }
}
