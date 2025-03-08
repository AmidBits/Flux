//Implement this (down a bit in the article) : https://stackoverflow.com/questions/218060/random-gaussian-variables

namespace Flux
{
  public static partial class Fx
  {
    // https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netstandard-2.0

    /// <summary>
    /// <para>Generates a a non-negative random BigInteger limited in size by the specified <paramref name="maxBitLength"/> (bit-length is used since BigInteger can basically represent an unlimited sized integer).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="maxBitLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Numerics.BigInteger NextBigInteger(this System.Random source, int maxBitLength)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (maxBitLength <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxBitLength));

      var (quotient, remainder) = int.DivRem(maxBitLength, 8);

      var byteCount = quotient + (remainder > 0 ? 1 : 0);
      var bitMask = (System.Numerics.BigInteger.One << maxBitLength) - 1;

      return new System.Numerics.BigInteger(GetRandomBytes(source, byteCount), true) & bitMask;
    }

    /// <summary>
    /// <para>Returns a non-negative random BigInteger that is less than the specified <paramref name="maxValue"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Numerics.BigInteger NextBigInteger(this System.Random source, System.Numerics.BigInteger maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (maxValue <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxValue)); // Ensured positive integer, no padding byte for BigInteger.

      System.Numerics.BigInteger value;

      var bitLength = (int)maxValue.GetBitLength();

      do
      {
        value = NextBigInteger(source, bitLength);
      }
      while (value >= maxValue); // If value is greater or equal to the specified maxValue, maintain uniform distribution by re-running if value is greater-than-or-equal to maxValue (a fifty-fifty situation).

      return value;
    }

    /// <summary>
    /// <para>Returns a random BigInteger that is within a specified range, i.e. greater or equal to <paramref name="minValue"/> and less than <paramref name="maxValue"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static System.Numerics.BigInteger NextBigInteger(this System.Random source, System.Numerics.BigInteger minValue, System.Numerics.BigInteger maxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return System.Numerics.BigInteger.Min(minValue, maxValue) + source.NextBigInteger(System.Numerics.BigInteger.Abs(maxValue - minValue));
    }

    /// <summary>
    /// <para>Generates a new random boolean value.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool NextBoolean(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.Next(2) == 1;
    }

    /// <summary>
    /// <para>Generates a new boolean value with the specified <paramref name="probability"/> of being true. This function can be called a Bernoulli trial.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="probability"></param>
    /// <returns></returns>
    public static bool NextBoolean(this System.Random source, double probability = 0.5)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.NextDouble() < probability;
    }

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

      if (minValue >= maxValue) throw new System.ArgumentOutOfRangeException(nameof(minValue));

      return new(source.NextInt64(minValue.Ticks, maxValue.Ticks));
    }

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

      if (maxValue <= TNumber.Zero) throw new System.ArgumentOutOfRangeException(nameof(maxValue));

      return TNumber.CreateChecked(source.NextDouble() * double.CreateChecked(maxValue));
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

      if (minValue >= maxValue) throw new System.ArgumentOutOfRangeException(nameof(minValue));

      return minValue + source.NextNumber(maxValue - minValue);
    }

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

      if (maxValue <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxValue));

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

      if (minValue >= maxValue) throw new System.ArgumentOutOfRangeException(nameof(minValue));

      return minValue + source.NextDouble(maxValue - minValue);
    }

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

      if (sigma <= 0) throw new System.ArgumentOutOfRangeException(nameof(sigma));

      return mean + sigma * NextNormal(source);
    }

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

    /// <summary>
    /// <para>Creates a new random System.UInt32 in the interval [0, <see cref="System.UInt32.MaxValue"/>), but will never return <see cref="System.UInt32.MaxValue"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static uint NextUInt32(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var uint32 = System.UInt32.MaxValue;

      while (uint32 == System.UInt32.MaxValue)
        uint32 = System.BitConverter.ToUInt32(GetRandomBytes(source, 4), 0);

      return uint32;
    }

    /// <summary>
    /// <para>Creates a new random System.UInt64 in the interval [0, <see cref="System.UInt64.MaxValue"/>), but will never return <see cref="System.UInt64.MaxValue"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    [System.CLSCompliant(false)]
    public static ulong NextUInt64(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var uint64 = System.UInt64.MaxValue;

      while (uint64 == System.UInt64.MaxValue)
        uint64 = System.BitConverter.ToUInt64(GetRandomBytes(source, 8), 0);

      return uint64;
    }

    /// <summary>
    /// <para>Produce a uniform random sample from the open interval (0, 1), i.e. the method will not return either end point.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static double NextUniform(this System.Random source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.NextDouble() is var sample && sample > 0 && sample < 1 ? sample : double.Epsilon;
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
