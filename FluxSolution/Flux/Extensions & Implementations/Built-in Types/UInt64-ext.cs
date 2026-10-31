namespace Flux
{
  [System.CLSCompliant(false)]
  public static partial class UInt64Extensions
  {
    private static System.ReadOnlySpan<byte> SmallPrimeBases => [3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61];
    private static System.ReadOnlySpan<uint> MillerRabinBases => [2, 325, 9375, 28178, 450775, 9780504, 1795265022];

    extension(System.UInt64)
    {
      #region BitSwaps

      /// <summary>
      /// <para>Swap adjacent 1-bits (single bits).</para>
      /// </summary>
      public static ulong BitSwap1(ulong value) => ((value & 0xaaaaaaaaaaaaaaaaUL) >> 0x01) | ((value & 0x5555555555555555UL) << 0x01);

      /// <summary>
      /// <para>Swap adjacent 2-bits (pairs).</para>
      /// </summary>
      public static ulong BitSwap2(ulong value) => ((value & 0xccccccccccccccccUL) >> 0x02) | ((value & 0x3333333333333333UL) << 0x02);

      /// <summary>
      /// <para>Swap adjacent 4-bits (nibbles).</para>
      /// </summary>
      public static ulong BitSwap4(ulong value) => ((value & 0xf0f0f0f0f0f0f0f0UL) >> 0x04) | ((value & 0x0f0f0f0f0f0f0f0fUL) << 0x04);

      /// <summary>
      /// <para>Swap adjacent 8-bits (bytes).</para>
      /// </summary>
      public static ulong BitSwap8(ulong value) => ((value & 0xff00ff00ff00ff00UL) >> 0x08) | ((value & 0x00ff00ff00ff00ffUL) << 0x08);

      /// <summary>
      /// <para>Swap adjacent 16-bits (short words).</para>
      /// </summary>
      public static ulong BitSwap16(ulong value) => ((value & 0xffff0000ffff0000UL) >> 0x10) | ((value & 0x0000ffff0000ffffUL) << 0x10);

      /// <summary>
      /// <para>Swap adjacent 32-bits (words).</para>
      /// </summary>
      public static ulong BitSwap32(ulong value) => ((value & 0xffffffff00000000UL) >> 0x20) | ((value & 0x00000000ffffffffUL) << 0x20);

      #endregion

      ///// <summary>
      ///// <para>Computes base^exp for ulong using exponentiation by squaring. Throws OverflowException if result exceeds ulong.MaxValue.</para>
      ///// </summary>
      ///// <param name="value"></param>
      ///// <param name="exponent"></param>
      ///// <returns></returns>
      //private static ulong Pow(ulong value, ulong exponent)
      //{
      //  if (exponent == 0) return 1;
      //  if (value == 0) return 0;

      //  var result = 1UL;
      //  var current = value;

      //  while (exponent > 0)
      //    checked // Detect overflow.
      //    {
      //      if ((exponent & 1) != 0)
      //        result *= current;

      //      exponent >>= 1;

      //      if (exponent > 0)
      //        current *= current;
      //    }

      //  return result;
      //}

      //#region Pow - Overflow‑safe exponentiation.

      ///// <summary>
      ///// <para>Overflow‑safe exponentiation: x^k, stops early if exceeding limit.</para>
      ///// </summary>
      ///// <param name="value"></param>
      ///// <param name="exponent"></param>
      ///// <param name="limit"></param>
      ///// <returns></returns>
      //private static ulong Pow(ulong value, ulong exponent, ulong limit)
      //{
      //  var result = 1ul;

      //  while (exponent > 0)
      //  {
      //    if ((exponent & 1) != 0)
      //    {
      //      if (value != 0 && result > limit / value) // Check: result * value > limit ?
      //        return ulong.MaxValue;

      //      result *= value;

      //      if (result > limit)
      //        return ulong.MaxValue;
      //    }

      //    exponent >>= 1;

      //    if (exponent == 0)
      //      break;

      //    if (value != 0 && value > limit / value) // Square the base, with overflow check.
      //      value = ulong.MaxValue;
      //    else
      //      value *= value;

      //    if (value > limit)
      //      value = ulong.MaxValue;
      //  }

      //  return result;
      //}

      //#endregion

      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
      /// </summary>
      public static ulong ReverseBits(ulong value)
      {
        ReverseBitsInPlace(ref value);

        return value;
      }

      /// <summary>
      /// <para>In-place (by ref) mirror the bits (bit-reversal of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
      /// </summary>
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

    #region IsPrimeNumberDeterministic - Miller-Rabin deterministic primality test.

    /// <summary>
    /// <para>Deterministic Miller-Rabin primality test for 64-bit integers using bases 2,3,5,7,11,13,17.</para>
    /// <para>This implementation uses ulong native operations.</para>
    /// </summary>
    /// <remarks>Guaranteed correct for n &lt; 2^64.</remarks>
    /// <param name="n"></param>
    /// <returns></returns>
    internal static bool IsPrimeNumberDeterministic(ulong n)
    {
      if (n <= uint.MaxValue)
        return uint.IsPrimeNumberDeterministic((uint)n);

      if ((n & 1) == 0) // Eliminate all even numbers.
        return false;

      foreach (var p in SmallPrimeBases) // Small-prime trial division.
        if (n % p == 0)
          return false;

      // Write n - 1 as d * 2^s with d odd.
      var d = n - 1;
      var s = System.Numerics.BitOperations.TrailingZeroCount(d);
      d >>= s;

      foreach (var a in MillerRabinBases) // Deterministic Miller-Rabin witnesses for all ulong values.
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
      var x = PowMod(a % n, d, n);

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
        d >>= 1;
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
  }
}

// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>
