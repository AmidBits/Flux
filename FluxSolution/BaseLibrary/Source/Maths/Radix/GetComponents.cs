using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetComponents(System.Numerics.BigInteger value, int radix, bool skipZeroes = false)
    {
      var scalar = System.Numerics.BigInteger.One;
      foreach (var digit in GetDigitsReversed(value, radix))
      {
        if (digit != 0 || !skipZeroes)
          yield return digit * scalar;
        scalar *= radix;
      }
    }

    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetComponents(int value, int radix, bool skipZeroes = false)
    {
      var scalar = 1;
      foreach (var digit in GetDigitsReversed(value, radix))
      {
        if (digit != 0 || !skipZeroes)
          yield return digit * scalar;
        scalar *= radix;
      }
    }
    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
    public static System.Collections.Generic.IEnumerable<long> GetComponents(long value, int radix, bool skipZeroes = false)
    {
      var scalar = 1L;
      foreach (var digit in GetDigitsReversed(value, radix))
      {
        if (digit != 0 || !skipZeroes)
          yield return digit * scalar;
        scalar *= radix;
      }
    }

    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<uint> GetComponents(uint value, int radix, bool skipZeroes = false)
    {
      var scalar = 1U;
      foreach (var digit in GetDigitsReversed(value, radix))
      {
        if (digit != 0 || !skipZeroes)
          yield return (digit * scalar);
        scalar *= (uint)radix;
      }
    }
    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<ulong> GetComponents(ulong value, int radix, bool skipZeroes = false)
    {
      var scalar = 1UL;
      foreach (var digit in GetDigitsReversed(value, radix))
      {
        if (digit != 0 || !skipZeroes)
          yield return (digit * scalar);
        scalar *= (ulong)radix;
      }
    }
  }
}
