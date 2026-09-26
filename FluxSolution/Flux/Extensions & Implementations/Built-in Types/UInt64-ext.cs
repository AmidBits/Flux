namespace Flux
{
  public static partial class UInt64Extensions
  {
    /// <summary>
    /// <para>The largest prime number that fits in an <see cref="System.UInt64"/>.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public const ulong MaxPrimeNumber = 18446744073709551557ul;

    private const ulong m_primeBitMask = 0b0010_1000_0010_0000_1000_1010_0010_0000_1010_0000_1000_1010_0010_1000_1010_1100UL;
    private static readonly ulong[] m_smallPrimes = { 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };
    private static readonly ulong[] m_primeDeterministicBases = { 2, 3, 5, 7, 11, 13, 17 };

    extension(System.UInt64)
    {
      #region BitSwaps

      /// <summary>
      /// <para>Swap adjacent 1-bits (single bits).</para>
      /// </summary>
      [System.CLSCompliant(false)] public static ulong BitSwap1(ulong value) => ((value & 0xaaaaaaaaaaaaaaaaUL) >> 0x01) | ((value & 0x5555555555555555UL) << 0x01);

      /// <summary>
      /// <para>Swap adjacent 2-bits (pairs).</para>
      /// </summary>
      [System.CLSCompliant(false)] public static ulong BitSwap2(ulong value) => ((value & 0xccccccccccccccccUL) >> 0x02) | ((value & 0x3333333333333333UL) << 0x02);

      /// <summary>
      /// <para>Swap adjacent 4-bits (nibbles).</para>
      /// </summary>
      [System.CLSCompliant(false)] public static ulong BitSwap4(ulong value) => ((value & 0xf0f0f0f0f0f0f0f0UL) >> 0x04) | ((value & 0x0f0f0f0f0f0f0f0fUL) << 0x04);

      /// <summary>
      /// <para>Swap adjacent 8-bits (bytes).</para>
      /// </summary>
      [System.CLSCompliant(false)] public static ulong BitSwap8(ulong value) => ((value & 0xff00ff00ff00ff00UL) >> 0x08) | ((value & 0x00ff00ff00ff00ffUL) << 0x08);

      /// <summary>
      /// <para>Swap adjacent 16-bits (short words).</para>
      /// </summary>
      [System.CLSCompliant(false)] public static ulong BitSwap16(ulong value) => ((value & 0xffff0000ffff0000UL) >> 0x10) | ((value & 0x0000ffff0000ffffUL) << 0x10);

      /// <summary>
      /// <para>Swap adjacent 32-bits (words).</para>
      /// </summary>
      [System.CLSCompliant(false)] public static ulong BitSwap32(ulong value) => ((value & 0xffffffff00000000UL) >> 0x20) | ((value & 0x00000000ffffffffUL) << 0x20);

      #endregion

      #region Cbrt - Integer cube root.

      /// <summary>
      /// <para>Computes the integer cube root of a 64-bit unsigned integer.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <returns></returns>
      [System.CLSCompliant(false)]
      public static ulong Cbrt(ulong n)
      {
        if (n < 8)
          return n == 0 ? 0u : 1u;

        var x = 1UL << ((System.Numerics.BitOperations.Log2(n) + 2) / 3);

        while (true)
        {
          var xp = x;

          // Overflow‑safe x²
          if (x != 0 && x > n / x)
            return xp;

          var x2 = x * x;

          // Overflow‑safe n/x²
          var div = (x2 == 0) ? 0 : n / x2;

          // Newton iteration
          x = (2 * x + div) / 3;

          if (x >= xp)
            return xp;
        }
      }

      #endregion

      #region Log - Integer logarithm.

      /// <summary>
      /// <para>Computes the integer logarithm of a 64-bit unsigned integer with a specified base.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      [System.CLSCompliant(false)]
      public static (ulong LogFloor, ulong LogCeiling, bool IsExactLog) Log(ulong n, ulong b)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(b, 2ul);

        if (n == 0)
          return (0, 0, true);

        if (n < b)
          return (0, 1, false);

        if (n == b)
          return (1, 1, true);

        var log2n = System.Numerics.BitOperations.Log2(n);
        var log2b = System.Numerics.BitOperations.Log2(b);

        var ilogf = (ulong)(log2n / log2b);

        var pow = System.UInt128.One;
        System.UInt128 factor = b;
        var remexp = ilogf;

        while (remexp != 0)
        {
          if ((remexp & 1) != 0)
            pow *= factor;

          factor *= factor;
          remexp >>= 1;
        }

        while (pow > n)
        {
          pow /= b;
          ilogf--;
        }

        while (pow * b <= n)
        {
          pow *= b;
          ilogf++;
        }

        var exact = pow == n;
        var ilogc = exact ? ilogf : ilogf + 1;

        return (ilogf, ilogc, exact);
      }

      #endregion

      #region LogE - Integer natural logarithm.

      /// <summary>
      /// <para>Computes the integer natural logarithm of a 64-bit unsigned integer.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      [System.CLSCompliant(false)]
      public static (ulong LogFloor, ulong LogCeiling) LogE(ulong n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        if (n <= 1)
          return (0, 0);

        var log2 = System.Numerics.BitOperations.Log2(n); // log2(n) = 63 - clz(n)

        var ln_n_est = log2 * 0.69314718055994530941723212145818; // ln(n) ≈ log2(n) * ln(2)

        var k = (ulong)ln_n_est; // Log-floor estimate.

        if (ln_n_est < k) // Correction for floating-point underestimation.
          k--;

        return (k, k + 1);
      }

      #endregion

      #region RootN - Integer nth root.

      /// <summary>
      /// <para>Compute the nth root of a 64-bit unsigned integer. This implementation uses Newton's method.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="degree"></param>
      /// <returns></returns>
      [System.CLSCompliant(false)]
      public static ulong RootN(ulong value, int degree)
      {
        if (degree <= 1)
          return value;

        if (value == 0)
          return 0;

        if (degree == 2) // Fast path for square root.
          return (ulong)double.Sqrt(value);

        if (degree == 3) // Fast path for cube root (integer‑safe).
          return Cbrt(value);

        var x = 1UL << (System.Numerics.BitOperations.Log2(value) / degree);

        while (true) // Newton iteration.
        {
          var xp = x;

          var pow = Pow(x, (ulong)degree - 1, value);
          var div = (pow == ulong.MaxValue) ? 0 : value / pow;

          x = ((ulong)(degree - 1) * x + div) / (ulong)degree;

          if (x >= xp)
            return xp;
        }
      }

      #endregion

      #region Sqrt - Integer square root.

      /// <summary>
      /// <para>Computes the integer square root of a 64-bit unsigned integer.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      [System.CLSCompliant(false)]
      public static ulong Sqrt(ulong n)
      {
        if (n <= 1)
          return n;

        var r = 0ul;
        var bit = 1ul << (System.Numerics.BitOperations.Log2(n) & ~1); // The highest power of four <= 2^64

        while (bit > n) // Align bit with highest non-zero pair of bits
          bit >>= 2;

        while (bit != 0)
        {
          if (n >= r + bit)
          {
            n -= r + bit;
            r = (r >> 1) + bit;
          }
          else
            r >>= 1;

          bit >>= 2;
        }

        return r;
      }

      #endregion

      #region IsPrimeNumber.. - Miller-Rabin deterministic primality test.

      ///// <summary>
      ///// <para>Deterministic Miller-Rabin prime number test.</para>
      ///// </summary>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //[System.CLSCompliant(false)]
      //public static bool IsPrime(ulong n)
      //  => MillerRabinDeterministicIsPrime(n);

      /// <summary>
      /// <para>Deterministic Miller-Rabin primality test for 64-bit integers using bases 2,3,5,7,11,13,17.</para>
      /// </summary>
      /// <remarks>Guaranteed correct for n &lt; 2^64.</remarks>
      /// <param name="n"></param>
      /// <returns></returns>
      internal static bool IsPrimeNumberDeterministic(ulong n)
      {
        if (n < 64)
          return (m_primeBitMask & (1UL << (int)n)) != 0; // Initial 64-bit-mask takes care of [0, 63].

        if (n % 2 == 0UL)
          return false; // Secondly, eliminate all even numbers (the prime number 2 was handled in the previous step).

        foreach (var p in m_smallPrimes) // Very small prime checks.
        {
          //if (n == p)
          //  return true;

          if (n % p == 0UL)
            return false;
        }

        // Write 'n - 1' as 'd * 2^s', where d is an odd number and s is the number of times you can divide n-1 by 2 before it becomes odd.

        var d = n - 1;
        var s = 0;
        while ((d & 1) == 0)
        {
          d >>= 1;
          s++;
        }

        foreach (var a in m_primeDeterministicBases)
        {
          if (a >= n)
            break;

          if (!IsPrimeNumberDeterministicPass(a, s, d, n))
            return false;
        }

        return true;
      }

      private static bool IsPrimeNumberDeterministicPass(ulong a, int s, ulong d, ulong n)
      {
        var x = PowMod(a, d, n);

        if (x == 1 || x == n - 1)
          return true;

        for (int r = 1; r < s; r++)
        {
          x = MulMod(x, x, n);

          if (x == n - 1)
            return true;
        }

        return false;
      }

      private static ulong MulMod(ulong a, ulong b, ulong mod)
        => (ulong)(((UInt128)a * (UInt128)b) % mod);

      private static ulong PowMod(ulong a, ulong d, ulong mod)
      {
        var result = 1UL;

        while (d > 0)
        {
          if ((d & 1) != 0)
            result = MulMod(result, a, mod);

          a = MulMod(a, a, mod);
          d >>>= 1;
        }

        return result;
      }

      //private static bool MillerRabinDeterministicPass(ulong a, int s, ulong d, ulong n)
      //{
      //  System.Numerics.BigInteger nBI = n;
      //  System.Numerics.BigInteger aBI = a;
      //  System.Numerics.BigInteger x = System.Numerics.BigInteger.ModPow(aBI, d, nBI);

      //  if (x == 1 || x == n - 1)
      //    return true;

      //  for (var r = 1; r < s; r++)
      //  {
      //    x = System.Numerics.BigInteger.ModPow(x, 2, nBI);

      //    if (x == n - 1)
      //      return true;
      //  }

      //  return false;
      //}

      #endregion

      /// <summary>
      /// <para>Computes base^exp for ulong using exponentiation by squaring. Throws OverflowException if result exceeds ulong.MaxValue.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      private static ulong Pow(ulong value, ulong exponent)
      {
        if (exponent == 0) return 1;
        if (value == 0) return 0;

        var result = 1UL;
        var current = value;

        while (exponent > 0)
          checked // Detect overflow.
          {
            if ((exponent & 1) != 0)
              result *= current;

            exponent >>= 1;

            if (exponent > 0)
              current *= current;
          }

        return result;
      }

      #region Pow - Overflow‑safe exponentiation.

      /// <summary>
      /// <para>Overflow‑safe exponentiation: x^k, stops early if exceeding limit.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <param name="limit"></param>
      /// <returns></returns>
      private static ulong Pow(ulong value, ulong exponent, ulong limit)
      {
        var result = 1ul;

        while (exponent > 0)
        {
          if ((exponent & 1) != 0)
          {
            if (value != 0 && result > limit / value) // Check: result * value > limit ?
              return ulong.MaxValue;

            result *= value;

            if (result > limit)
              return ulong.MaxValue;
          }

          exponent >>= 1;

          if (exponent == 0)
            break;

          if (value != 0 && value > limit / value) // Square the base, with overflow check.
            value = ulong.MaxValue;
          else
            value *= value;

          if (value > limit)
            value = ulong.MaxValue;
        }

        return result;
      }

      #endregion

      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static ulong ReverseBits(ulong value)
      {
        ReverseBitsInPlace(ref value);

        return value;
      }

      /// <summary>
      /// <para>In-place (by ref) mirror the bits (bit-reversal of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static void ReverseBitsInPlace(ref ulong value)
      {
        value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01);
        value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02);
        value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04);
        value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08);
        value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10);
        value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20);
      }

      #endregion
    }
  }
}

// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>
