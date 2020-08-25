namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Creates a new sequence of descending potential primes, less than the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDescendingPotentialPrimes(System.Numerics.BigInteger startAt)
    {
      var quotient = System.Numerics.BigInteger.DivRem(startAt, 6, out var remainder);

      if (remainder == 1) // On a potential prime before a descending % 6 number. E.g. 13.
      {
        yield return 6 * quotient-- - 1;
      }
      else if (remainder == 0) // Between two potential primes on a % 6 number. E.g. 12.
      {
        yield return 6 * quotient-- - 1;
      }

      for (var index = 6 * quotient; index > 0; index -= 6)
      {
        yield return index + 1;
        yield return index - 1;
      }

      yield return 3;
      yield return 2;
    }

    /// <summary>Creates a new sequence of descending potential primes, less than the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetDescendingPotentialPrimes(int startAt)
    {
      var quotient = System.Math.DivRem(startAt, 6, out var remainder);

      if (remainder == 1) // On a potential prime before a descending % 6 number. E.g. 13.
      {
        yield return 6 * quotient-- - 1;
      }
      else if (remainder == 0) // Between two potential primes on a % 6 number. E.g. 12.
      {
        yield return 6 * quotient-- - 1;
      }

      for (var index = 6 * quotient; index > 0; index -= 6)
      {
        yield return index + 1;
        yield return index - 1;
      }

      yield return 3;
      yield return 2;
    }
    /// <summary>Creates a new sequence of descending potential primes, less than the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<long> GetDescendingPotentialPrimes(long startAt)
    {
      var quotient = System.Math.DivRem(startAt, 6, out var remainder);

      if (remainder == 1) // On a potential prime before a descending % 6 number. E.g. 13.
      {
        yield return 6 * quotient-- - 1;
      }
      else if (remainder == 0) // Between two potential primes on a % 6 number. E.g. 12.
      {
        yield return 6 * quotient-- - 1;
      }

      for (var index = 6 * quotient; index > 0; index -= 6)
      {
        yield return index + 1;
        yield return index - 1;
      }

      yield return 3;
      yield return 2;
    }
  }
}
