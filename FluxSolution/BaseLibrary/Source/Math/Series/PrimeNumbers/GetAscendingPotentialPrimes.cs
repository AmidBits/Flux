using System.Linq;

namespace Flux
{
  public static partial class Math
  {
    /// <summary>Creates a new sequence of ascending potential primes, greater than the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetAscendingPotentialPrimes(System.Numerics.BigInteger startAt)
    {
      if (startAt <= 2) yield return 2;
      if (startAt <= 3) yield return 3;

      var quotient = System.Numerics.BigInteger.DivRem(startAt, 6, out var remainder);

      if (remainder == 5) // On a potential prime before an ascending % 6 number. E.g. 11.
      {
        yield return 6 * ++quotient + 1;
      }
      else if (remainder == 0) // Between two potential primes on a % 6 number. E.g. 12.
      {
        yield return 6 * quotient++ + 1;
      }

      for (var index = 6 * (quotient + (remainder > 0 ? 1 : 0)); true; index += 6)
      {
        yield return index - 1;
        yield return index + 1;
      }
    }

    /// <summary>Creates a new sequence of ascending potential primes, greater than the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetAscendingPotentialPrimes(int startAt)
    {
      if (startAt <= 2) yield return 2;
      if (startAt <= 3) yield return 3;

      var quotient = System.Math.DivRem(startAt, 6, out var remainder);

      if (remainder == 5) // On a potential prime before an ascending % 6 number. E.g. 11.
      {
        yield return 6 * ++quotient + 1;
      }
      else if (remainder == 0) // Between two potential primes on a % 6 number. E.g. 12.
      {
        yield return 6 * quotient++ + 1;
      }

      for (var index = 6 * (quotient + (remainder > 0 ? 1 : 0)); true; index += 6)
      {
        yield return index - 1;
        yield return index + 1;
      }
    }
    /// <summary>Creates a new sequence of ascending potential primes, greater than the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<long> GetAscendingPotentialPrimes(long startAt)
    {
      if (startAt <= 2) yield return 2;
      if (startAt <= 3) yield return 3;

      var quotient = System.Math.DivRem(startAt, 6, out var remainder);

      if (remainder == 5) // On a potential prime before an ascending % 6 number. E.g. 11.
      {
        yield return 6 * ++quotient + 1;
      }
      else if (remainder == 0) // Between two potential primes on a % 6 number. E.g. 12.
      {
        yield return 6 * quotient++ + 1;
      }

      for (var index = 6 * (quotient + (remainder > 0 ? 1 : 0)); true; index += 6)
      {
        yield return index - 1;
        yield return index + 1;
      }
    }
  }
}
