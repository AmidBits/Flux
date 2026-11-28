namespace Flux
{
  public static partial class BigIntegerExtensions
  {
    extension(System.Numerics.BigInteger)
    {
      #region DigitCount

      /// <summary>
      /// <para>A fast <see cref="System.Numerics.BigInteger"/> DigitCount function that works for very large numbers.</para>
      /// </summary>
      /// <param name="number"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static int DigitCount(System.Numerics.BigInteger number, int radix)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(radix, 2);

        if (number.IsZero)
          return 1;

        number = System.Numerics.BigInteger.Abs(number);

        var bitLength = number.GetBitLength();

        var log2n = bitLength > 64
          ? (bitLength - 1) + double.Log2((ulong)(number >> (int)(bitLength - 64)) / (double)(1UL << 63))
          : double.Log2((ulong)number); // bitLength <= 64

        var digits = (int)(log2n / double.Log2(radix)) + 1;

        var lower = System.Numerics.BigInteger.Pow(radix, digits - 1);
        while (lower > number)
        {
          digits--;
          lower /= radix;
        }

        var upper = lower * radix;
        while (upper <= number)
        {
          digits++;
          upper *= radix;
        }

        return digits;
      }

      #endregion

      #region FitToSmallestIntegerType

      public static object FitToSmallestIntegerType(System.Numerics.BigInteger value)
      {
        if (value >= byte.MinValue && value <= byte.MaxValue) return (byte)value;
        else if (value >= sbyte.MinValue && value <= sbyte.MaxValue) return (sbyte)value;
        else if (value >= short.MinValue && value <= short.MaxValue) return (short)value;
        else if (value >= ushort.MinValue && value <= ushort.MaxValue) return (ushort)value;
        else if (value >= int.MinValue && value <= int.MaxValue) return (int)value;
        else if (value >= uint.MinValue && value <= uint.MaxValue) return (uint)value;
        else if (value >= long.MinValue && value <= long.MaxValue) return (long)value;
        else if (value >= ulong.MinValue && value <= ulong.MaxValue) return (ulong)value;
        else if (value >= Int128.MinValue && value <= Int128.MaxValue) return (Int128)value;
        else if (value >= UInt128.MinValue && value <= UInt128.MaxValue) return (UInt128)value;
        else return value;
      }

      #endregion

      public static System.Collections.Generic.List<System.Numerics.BigInteger> ContinuedFractionCoefficients(System.Numerics.Decimal128 value, int maxTerms = 101)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTerms);

        var coefficients = new System.Collections.Generic.List<System.Numerics.BigInteger>();

        value = System.Numerics.Decimal128.Abs(value);

        for (var i = 0; i < maxTerms; i++)
        {
          var floor = System.Numerics.Decimal128.Floor(value);

          coefficients.Add(System.Numerics.BigInteger.CreateChecked(floor));

          var fractional = value - floor;

          if (fractional == System.Numerics.Decimal128.Zero)
            break;

          value = System.Numerics.Decimal128.One / fractional;
        }

        return coefficients;
      }

      public static System.Collections.Generic.List<(System.Numerics.BigInteger, System.Numerics.BigInteger)> ContinuedFractionConvergents(System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> coefficients)
      {
        System.ArgumentNullException.ThrowIfNull(coefficients);

        var fractions = new System.Collections.Generic.List<(System.Numerics.BigInteger, System.Numerics.BigInteger)>();

        var (p0, q0) = (System.Numerics.BigInteger.Zero, System.Numerics.BigInteger.One);
        var (p1, q1) = (System.Numerics.BigInteger.One, System.Numerics.BigInteger.Zero);

        foreach (var a in coefficients)
        {
          var (p, q) = (a * p1 + p0, a * q1 + q0);

          fractions.Add((p, q));

          (p0, q0) = (p1, q1);
          (p1, q1) = (p, q);
        }

        return fractions;
      }

      public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger Coefficient, System.Numerics.BigInteger Numerator, System.Numerics.BigInteger Denominator)> ContinuedFractionSteps(System.Numerics.Decimal128 value, int maxIterations = 101)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxIterations);

        if (!System.Numerics.Decimal128.IsFinite(value)) throw new System.NotFiniteNumberException();

        var v = System.Numerics.Decimal128.Abs(value);

        var (p0, q0) = (System.Numerics.BigInteger.Zero, System.Numerics.BigInteger.One);

        var (p1, q1) = (System.Numerics.BigInteger.One, System.Numerics.BigInteger.Zero);

        for (var i = 0; i < maxIterations; i++)
        {
          var coefficient = System.Numerics.BigInteger.CreateChecked(System.Numerics.Decimal128.Floor(v));

          var (p, q) = (coefficient * p1 + p0, coefficient * q1 + q0);

          yield return (coefficient, p, q);

          (p0, q0) = (p1, q1);
          (p1, q1) = (p, q);

          var fractional = v - System.Numerics.Decimal128.CreateChecked(coefficient);

          if (fractional == System.Numerics.Decimal128.Zero)
            yield break;

          v = System.Numerics.Decimal128.One / fractional;
        }
      }
    }

    extension(System.Numerics.BigInteger source)
    {
      /// <summary>Returns either the built-in byte array, or if a zero byte padding is present, a byte array excluding the zero byte is returned.</summary>
      public byte[] ToByteArrayEx(out int length)
      {
        var byteArray = source.ToByteArray();

        length = byteArray.Length - 1;

        if (length > 0 && byteArray[length] == 0)
          System.Array.Resize(ref byteArray, length);
        else
          length++;

        return byteArray;
      }

      /// <summary>This is essentially the same as the native ToByteArray (which is even called from this extension method) with the addition of the most significant byte index and its value as out parameters.</summary>
      public byte[] ToByteArrayEx(out int msbIndex, out byte msbValue)
      {
        var byteArray = source.ToByteArray();

        msbIndex = byteArray.Length - 1;
        msbValue = byteArray[msbIndex];

        if (msbIndex > 0 && msbValue == 0)
          msbValue = byteArray[--msbIndex];

        return byteArray;
      }
    }

    #region IsPrimeNumber.. - Miller-Rabin probabilistic primality test.

    /// <summary>
    /// <para>Probabilistic Miller–Rabin primality test with parallel rounds.</para>
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k">Log(bit-length, 1.17) yields an approximately 15 iterations @ 10 bits, 30 @ 100, 44 @ 1000, 59 @ 10000, and can be lowered for a higher iteration (k) count. The lower the base, the higher the count.</param>
    /// <returns></returns>
    internal static bool IsPrimeNumberProbabilistic(System.Numerics.BigInteger n, int k)
    {
      if (n <= 3) return n == 2 || n == 3;
      if ((n % 2).IsZero) return false;

      // Write n-1 as d*2^r
      var d = n - 1;
      while (d % 2 == 0) d /= 2;

      var isPrime = true;
      var lockObj = new object();

      System.Threading.Tasks.Parallel.For(0, k, (i, state) =>
      {
        if (!isPrime) { state.Stop(); return; }

        System.Numerics.BigInteger a; // Random base in [2, n-2]

        lock (lockObj) { a = System.Random.Shared.NextInteger(2, n - 2); }

        if (!IsPrimeNumberProbabilisticTest(d, n, a))
        {
          lock (lockObj) { isPrime = false; }

          state.Stop();
        }
      });

      return isPrime;
    }

    /// <summary>
    /// <para>Miller–Rabin test for a single base.</para>
    /// </summary>
    /// <param name="d"></param>
    /// <param name="n"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    private static bool IsPrimeNumberProbabilisticTest(System.Numerics.BigInteger d, System.Numerics.BigInteger n, System.Numerics.BigInteger a)
    {
      var x = System.Numerics.BigInteger.ModPow(a, d, n);

      if (x == 1 || x == n - 1) return true;

      while (d != n - 1)
      {
        x = (x * x) % n;
        d *= 2;

        if (x == 1) return false;
        if (x == n - 1) return true;
      }

      return false;
    }

    #endregion
  }
}
