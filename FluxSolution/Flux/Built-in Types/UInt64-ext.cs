namespace Flux
{
  public static partial class UInt64Extensions
  {
    private const ulong m_primeBitMask = 0b0010_1000_0010_0000_1000_1010_0010_0000_1010_0000_1000_1010_0010_1000_1010_1100UL;
    private static readonly ulong[] m_smallPrimes = { 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };
    private static readonly ulong[] m_primeDeterministicBases = { 2, 3, 5, 7, 11, 13, 17 };

    extension(System.UInt64)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      [System.CLSCompliant(false)] public static ulong MaxPrimeNumber => 18446744073709551557ul;

      #region IsPrime deterministic

      /// <summary>
      /// <para>This implementation uses a Miller-Rabin deterministic algorithm.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      [System.CLSCompliant(false)]
      public static bool IsPrime(ulong n)
        => MillerRabinDeterministicIsPrime(n);

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

    #region Miller-Rabin deterministic ulong primality check

    /// <summary>
    /// <para>Deterministic Miller-Rabin primality test for 64-bit integers using bases 2,3,5,7,11,13,17.</para>
    /// </summary>
    /// <remarks>Guaranteed correct for n &lt; 2^64.</remarks>
    /// <param name="n"></param>
    /// <returns></returns>
    private static bool MillerRabinDeterministicIsPrime(ulong n)
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

        if (!MillerRabinDeterministicPass(a, s, d, n))
          return false;
      }

      return true;
    }

    private static bool MillerRabinDeterministicPass(ulong a, int s, ulong d, ulong n)
    {
      System.Numerics.BigInteger nBI = n;
      System.Numerics.BigInteger aBI = a;
      System.Numerics.BigInteger x = System.Numerics.BigInteger.ModPow(aBI, d, nBI);

      if (x == 1 || x == n - 1)
        return true;

      for (var r = 1; r < s; r++)
      {
        x = System.Numerics.BigInteger.ModPow(x, 2, nBI);

        if (x == n - 1)
          return true;
      }

      return false;
    }

    #endregion
  }
}
