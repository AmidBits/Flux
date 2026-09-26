namespace Flux
{
  public static partial class BigIntegerExtensions
  {
    extension(System.Numerics.BigInteger)
    {
      #region DigitCount

      public static int DigitCount(System.Numerics.BigInteger number, int radix)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(radix, 2);

        if (number.Sign == 0)
          return 1;

        if (number.Sign < 0)
          number = System.Numerics.BigInteger.Abs(number);

        // 1. Estimate digits using bit-length. (Full fractional bit‑length.)

        var bl = number.GetBitLength();
        var top = (bl <= 64) ? (ulong)number : (ulong)(number >> (int)(bl - 64));

        var mantissa = top / (double)(1UL << 63);
        var log2n = (bl - 1) + System.Math.Log(mantissa, 2);

        var logb = log2n / System.Math.Log(radix, 2);
        var digits = (int)logb + 1;

        // 2. Correct possible off-by-one.
        if (System.Numerics.BigInteger.Pow(radix, digits - 1) is var pow && pow > number)
          digits--;

        return digits;
      }

      #endregion

      #region FitSmallestIntegerType

      //public static object FitSmallestIntegerType(System.Numerics.BigInteger value)
      //{
      //  if (value >= sbyte.MinValue && value <= sbyte.MaxValue) return (sbyte)value;
      //  else if (value >= byte.MinValue && value <= byte.MaxValue) return (byte)value;
      //  else if (value >= short.MinValue && value <= short.MaxValue) return (short)value;
      //  else if (value >= ushort.MinValue && value <= ushort.MaxValue) return (ushort)value;
      //  else if (value >= int.MinValue && value <= int.MaxValue) return (int)value;
      //  else if (value >= uint.MinValue && value <= uint.MaxValue) return (uint)value;
      //  else if (value >= long.MinValue && value <= long.MaxValue) return (long)value;
      //  else if (value >= ulong.MinValue && value <= ulong.MaxValue) return (ulong)value;
      //  else if (value >= Int128.MinValue && value <= Int128.MaxValue) return (Int128)value;
      //  else if (value >= UInt128.MinValue && value <= UInt128.MaxValue) return (UInt128)value;
      //  else return value;
      //}

      #endregion

      #region Cbrt - Cube root using Newton method.

      /// <summary>
      /// <para>Newton-Raphson iteration with a bit‑length–based initial guess.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException"></exception>
      public static System.Numerics.BigInteger Cbrt(System.Numerics.BigInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        if (n < 2)
          return n;

        var x = System.Numerics.BigInteger.One << (int)((n.GetBitLength() + 2) / 3); // Initial guess.

        while (true)
        {
          var y = (2 * x + n / (x * x)) / 3;

          if (y >= x)
            return x;

          x = y;
        }
      }

      #endregion

      #region Log - Integer logarithm.

      public static (System.Numerics.BigInteger LogFloor, System.Numerics.BigInteger LogCeiling, bool IsExactLog) Log(System.Numerics.BigInteger n, System.Numerics.BigInteger b)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
        System.ArgumentOutOfRangeException.ThrowIfLessThan(b, 2);

        if (n < b)
          return (System.Numerics.BigInteger.Zero, System.Numerics.BigInteger.One, false);

        if (n == b)
          return (System.Numerics.BigInteger.One, System.Numerics.BigInteger.One, true);

        var log2n = n.GetBitLength() - 1; // Exact bit-length (floor log2)
        var log2b = b.GetBitLength() - 1;

        var ilogf = System.Numerics.BigInteger.Clamp(log2n / log2b, 0, int.MaxValue); // Initial exponent estimate of floor(log_b(n)), clamped to valid (int) exponent range.

        var pow = (ilogf <= 63) ? PowSmall(b, (int)ilogf) : System.Numerics.BigInteger.Pow(b, ilogf); // If the exponent (ilogf) is small, manual exponentiation-by-squaring is faster, otherwise BigInteger.Pow is faster.

        while (pow > n) // Correction: adjust downward.
        {
          pow /= b;
          ilogf--;
        }

        while (pow * b <= n) // Correction: adjust upward.
        {
          pow *= b;
          ilogf++;
        }

        var exact = pow == n;
        var ilogc = exact ? ilogf : ilogf + 1;

        return (ilogf, ilogc, exact);
      }

      public static System.Numerics.BigInteger PowSmall(System.Numerics.BigInteger b, int exp)
      {
        if (exp < 0)
          return System.Numerics.BigInteger.One;

        if (exp > 63) // If exponent is large, delegate to BigInteger.Pow()
          return System.Numerics.BigInteger.Pow(b, exp);

        var result = System.Numerics.BigInteger.One;

        for (var factor = b; exp > 0; factor *= factor)
        {
          if ((exp & 1) != 0)
            result *= factor;

          exp >>= 1;
        }

        return result;
      }

      #endregion

      #region LogE - Integer natural logarithm.

      public static (System.Numerics.BigInteger LogFloor, System.Numerics.BigInteger LogCeiling) LogE(System.Numerics.BigInteger n)
      {
        if (n <= 1)
          return (System.Numerics.BigInteger.Zero, System.Numerics.BigInteger.Zero);

        // log2(n) = bitLength - 1
        var log2 = n.GetBitLength() - 1;

        // Correction: check if e^(k+1) <= n
        // Use: n >= exp(k+1)  <=>  log(n) >= k+1
        // But log(n) = log2(n) * ln(2)
        var ln_n_est = log2 * 0.6931471805599453;

        // k ≈ log2(n) * ln(2)
        // ln(2) ≈ 0.6931471805599453
        var k = (int)ln_n_est;

        if (ln_n_est >= k + 1)
          k++;

        var ilogf = System.Numerics.BigInteger.CreateChecked(k);
        var ilogc = ilogf + 1;

        return (ilogf, ilogc);
      }

      #endregion

      #region RootN - Newton-Raphson with quadratic convergence.

      /// <summary>
      /// <para>Newton–Raphson iteration for integer nth root with quadratic convergence and a logarithmic initial guess.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      /// <exception cref="System.ArithmeticException"></exception>
      public static System.Numerics.BigInteger RootN(System.Numerics.BigInteger value, int n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        if (value == 0 || value == 1 || n == 1)
          return value;

        var x = System.Numerics.BigInteger.One << (int)(value.GetBitLength() / n); // Initial guess: 2^(bitLength/n)

        while (true)
        {
          var px = x;

          var t = System.Numerics.BigInteger.Pow(x, n - 1);

          if (t.IsZero) throw new System.ArithmeticException(); // Avoid division by zero (shouldn't happen for valid inputs).

          x = ((n - 1) * x + value / t) / n; // Newton iteration.

          if (x >= px) // Convergence check.
          { // Final correction to ensure x^n <= value < (x+1)^n
            while (System.Numerics.BigInteger.Pow(x + 1, n) <= value)
              x++;

            while (System.Numerics.BigInteger.Pow(x, n) > value)
              x--;

            return x;
          }
        }
      }

      #endregion

      #region Sqrt - Square root using Newton's method.

      /// <summary>
      /// <para>Integer square root using Newton's method.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static System.Numerics.BigInteger Sqrt(System.Numerics.BigInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        if (n <= 1)
          return n;

        var x = System.Numerics.BigInteger.One << (int)(n.GetBitLength() / 2); // Initial approximation: 2^(bitLength/2)

        while (true)
        {
          var y = (x + n / x) >> 1;

          if (y >= x)
            return x;

          x = y;
        }
      }

      #endregion
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
