//Implement this (down a bit in the article) : https://stackoverflow.com/questions/218060/random-gaussian-variables

using System.Numerics;

namespace Flux
{
  public static partial class RandomExtensions
  {
    // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netstandard-2.0

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

      return new System.Numerics.BigInteger(GetRandomBytes(source, byteCount), true) >> (byteCount * 8 - maxBitLength);
    }

    #region NextBoolean

    /// <summary>
    /// <para>Generates a new random boolean value.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool NextBoolean(this System.Random source)
      => source.NextBoolean(0.5);

    /// <summary>
    /// <para>Generates a new boolean value with the specified <paramref name="probability"/> of being true. This function can be called a Bernoulli trial.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="probability"></param>
    /// <returns></returns>
    public static bool NextBoolean(this System.Random source, double probability)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
      System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, 1);

      return source.NextDouble() < probability;
    }

    #endregion // NextBoolean

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

      if (scale <= 0) throw new System.ArgumentOutOfRangeException(nameof(scale));

      return mu + scale * double.TanPi(NextUniform(source) - 0.5);
    }

    #region NextDateTime

    /// <summary>
    /// <para>Returns a new random System.DateTime in the interval [<see cref="System.DateTime.MinValue"/>, <see cref="System.DateTime.MaxValue"/>).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.DateTime NextDateTime(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.NextDateTime(System.DateTime.MinValue, System.DateTime.MaxValue);
    }

    /// <summary>
    /// <para>Returns a new random System.DateTime in the interval [<paramref name="minValue"/>, <paramref name="maxValue"/>).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.DateTime NextDateTime(this System.Random source, System.DateTime minValue, System.DateTime maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minValue, maxValue);

      return new(source.NextInt64(minValue.Ticks, maxValue.Ticks));
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
        if (TNumber.CreateChecked(source.NextDouble() * double.CreateChecked(maxValue)) is var value && TNumber.IsPositive(value) && value < maxValue)
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

      return minValue + source.NextDouble(maxValue - minValue);
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
    public static double NextExponential(this System.Random source, double mu)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (mu <= 0) throw new System.ArgumentOutOfRangeException(nameof(mu));

      return mu * NextExponential(source);
    }

    #endregion // NextExponential

    /// <summary>
    /// <para>Generates a pseudo-random number using the Box-Muller sampling method by generating pairs of independent standard normal random variables.</para>
    /// <seealso href="https://en.wikipedia.org/wiki/Box-Muller_transform"/>
    /// </summary>
    /// <param name="source"></param>
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

      var s = double.Sqrt(-2 * double.Log(u1));

      var z0 = s * double.Cos(double.Tau * u2);
      var z1 = s * double.Sin(double.Tau * u2);

      return (z0 * sigma + mu, z1 * sigma + mu);
    }

    /// <summary>
    /// <para>The Marsaglia polar method is a pseudo-random number sampling method for generating a pair of independent standard normal random variables.</para>
    /// <see href="https://en.wikipedia.org/wiki/Marsaglia_polar_method"/>
    /// </summary>
    /// <param name="source"></param>
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

      s = double.Sqrt(-2.0 * double.Log(s) / s);

      var z0 = u * s;
      var z1 = v * s;

      return (z0 * sigma + mu, z1 * sigma + mu);
    }

    /// <summary>
    /// <para>Returns a new instance of a Guid structure by using an array of random bytes.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Guid NextGuid(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return new(GetRandomBytes(source, 16));
    }

    /// <summary>
    /// <para>The Laplace distribution is also known as the double exponential distribution.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Laplace_distribution"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="mean"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static double NextLaplace(this System.Random source, double mean, double scale)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return NextUniform(source) is var u && u < 0.5 ? mean + scale * double.Log(2 * u) : mean - scale * double.Log(2 * (1 - u));
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="mu"></param>
    /// <param name="sigma"></param>
    /// <returns></returns>
    public static double NextLogNormal(this System.Random source, double mu, double sigma)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return double.Exp(NextNormal(source, mu, sigma));
    }

    /// <summary>
    /// <para>Using the Box-Muller algorithm.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static double NextNormal(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return double.Sqrt(-2 * double.Log(NextUniform(source))) * double.Sin((double.Pi / 2) * NextUniform(source));
    }

    /// <summary>
    /// <para>Get normal (Gaussian) random sample with specified mean and standard deviation.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="mean"></param>
    /// <param name="sigma"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static double NextNormal(this System.Random source, double mean, double sigma)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sigma);

      return mean + sigma * NextNormal(source);
    }

    #region NextTimeSpan

    /// <summary>
    /// <para>Creates a new random System.TimeSpan in the interval [<see cref="System.TimeSpan.MinValue"/>, <see cref="System.TimeSpan.MaxValue"/>).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.TimeSpan NextTimeSpan(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.NextTimeSpan(System.TimeSpan.MinValue, System.TimeSpan.MaxValue);
    }

    /// <summary>
    /// <para>Creates a new random System.TimeSpan in the interval [<paramref name="minValue"/>, <paramref name="maxValue"/>).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.TimeSpan NextTimeSpan(this System.Random source, System.TimeSpan minValue, System.TimeSpan maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (minValue.Ticks >= maxValue.Ticks) throw new System.ArgumentOutOfRangeException(nameof(minValue));

      return new(source.NextInt64(minValue.Ticks, maxValue.Ticks));
    }

    #endregion // NextTimeSpan

    ///// <summary>
    ///// <para>Creates a new random System.UInt32 in the interval [0, <see cref="System.UInt32.MaxValue"/>), but will never return <see cref="System.UInt32.MaxValue"/>.</para>
    ///// </summary>
    ///// <param name="source"></param>
    ///// <returns></returns>
    //[System.CLSCompliant(false)]
    //public static uint NextUInt32(this System.Random source)
    //{
    //  System.ArgumentNullException.ThrowIfNull(source);

    //  return System.BitConverter.ToUInt32(GetRandomBytes(source, 4), 0) is var next && next != System.UInt32.MaxValue ? next : NextUInt32(source);
    //}

    ///// <summary>
    ///// <para>Creates a new random System.UInt64 in the interval [0, <see cref="System.UInt64.MaxValue"/>), but will never return <see cref="System.UInt64.MaxValue"/>.</para>
    ///// </summary>
    ///// <param name="source"></param>
    ///// <returns></returns>
    //[System.CLSCompliant(false)]
    //public static ulong NextUInt64(this System.Random source)
    //{
    //  System.ArgumentNullException.ThrowIfNull(source);

    //  return System.BitConverter.ToUInt64(GetRandomBytes(source, 8), 0) is var next && next != System.UInt64.MaxValue ? next : NextUInt64(source);
    //}

    ///// <summary>
    ///// <para>Creates a new random System.UInt128 in the interval [0, <see cref="System.UInt128.MaxValue"/>), but will never return <see cref="System.UInt128.MaxValue"/>.</para>
    ///// </summary>
    ///// <param name="source"></param>
    ///// <returns></returns>
    //[System.CLSCompliant(false)]
    //public static System.UInt128 NextUInt128(this System.Random source)
    //{
    //  System.ArgumentNullException.ThrowIfNull(source);

    //  return System.BitConverter.ToUInt128(GetRandomBytes(source, 16), 0) is var next && next != System.UInt128.MaxValue ? next : NextUInt128(source);
    //}

    /// <summary>
    /// <para>Produce a uniform random sample from the open interval (0, 1), i.e. the method will not return either end point.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static double NextUniform(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      while (true)
        if (source.NextDouble() is var value && value > 0 && value < 1)
          return value;
    }

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

      if (shape <= 0) throw new System.ArgumentOutOfRangeException(nameof(shape));
      if (scale <= 0) throw new System.ArgumentOutOfRangeException(nameof(scale));

      return scale * double.Pow(-double.Log(NextUniform(source)), 1 / shape);
    }
  }
}
