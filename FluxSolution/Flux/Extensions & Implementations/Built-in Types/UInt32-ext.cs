namespace Flux
{
  public static partial class UInt32Extensions
  {
    /// <summary>
    /// <para>The largest prime number that fits in an <see cref="System.UInt32"/>.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public const uint MaxPrimeNumber = 4294967291u;

    extension(System.UInt32)
    {
      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static uint ReverseBits(uint value)
      {
        ReverseBitsInPlace(ref value);

        return value;
      }

      /// <summary>
      /// <para>In-place (by ref) mirror the bits (bit-reversal of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static void ReverseBitsInPlace(ref uint value)
      {
        value = (value << 15) | (value >> 17);
        var tmp = (value ^ (value >> 10)) & 0x003f801f;
        value = (tmp + (tmp << 10)) ^ value;
        tmp = (value ^ (value >> 4)) & 0x0e038421;
        value = (tmp + (tmp << 4)) ^ value;
        tmp = (value ^ (value >> 2)) & 0x22488842;
        value = (tmp + (tmp << 2)) ^ value;

        // Alternatively:
        // value = ((value & 0xAAAAAAAAu) >> 0x01) | ((value & 0x55555555u) << 0x01);
        // value = ((value & 0xCCCCCCCCu) >> 0x02) | ((value & 0x33333333u) << 0x02);
        // value = ((value & 0xF0F0F0F0u) >> 0x04) | ((value & 0x0F0F0F0Fu) << 0x04);
        // value = ((value & 0xFF00FF00u) >> 0x08) | ((value & 0x00FF00FFu) << 0x08);
        // value = ((value & 0xFFFF0000u) >> 0x10) | ((value & 0x0000FFFFu) << 0x10);
      }

      #endregion

      #region IsPrime - Deterministic straightforward deterministic 6k ± 1 test.

      /// <summary>
      /// <para>Deterministic 6k ± 1 prime number test. This is the most efficient algorithm for testing primality of numbers within the range of an unsigned 32-bit integer.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      [System.CLSCompliant(false)]
      public static bool IsPrime(uint n)
      {
        if (n <= 3) return n > 1;
        if ((n & 1) == 0 || n % 3 == 0) return false;

        var i = 5u;
        var w = 2u;

        while (i * i <= n)
        {
          if (n % i == 0)
            return false;

          i += w;
          w = 6u - w; // 2,4,2,4...
        }

        return true;
      }

      #endregion
    }
  }
}
