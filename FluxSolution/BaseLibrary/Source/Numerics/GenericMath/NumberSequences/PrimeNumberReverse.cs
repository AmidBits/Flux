#if NET7_0_OR_GREATER
namespace Flux.NumberSequencing
{
  public record class PrimeNumberReverse
    : INumericSequence<System.Numerics.BigInteger>
  {
    public System.Numerics.BigInteger StartAt { get; }

    public PrimeNumberReverse(System.Numerics.BigInteger startAt)
      => StartAt = startAt;

    #region Static members
    /// <summary>Creates a new sequence of descending potential primes, less than the specified number.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDescendingPotentialPrimes(System.Numerics.BigInteger startAt)
    {
      var quotient = System.Numerics.BigInteger.DivRem(startAt, 6, out var remainder);

      if (remainder == 1) // On a potential prime before a descending % 6 number. E.g. 13.
        yield return 6 * quotient-- - 1;
      else if (remainder == 0) // Between two potential primes on a % 6 number. E.g. 12.
        yield return 6 * quotient-- - 1;

      for (var index = 6 * quotient; index > 0; index -= 6)
      {
        yield return index + 1;
        yield return index - 1;
      }

      yield return 3;
      yield return 2;
    }
    /// <summary>Creates a new sequence descending prime numbers, less than the specified number.</summary>
    /// <see cref="https://math.stackexchange.com/questions/164767/prime-number-generator-how-to-make"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDescendingPrimes(System.Numerics.BigInteger startAt)
      => GetDescendingPotentialPrimes(startAt).AsParallel().AsOrdered().Where(PrimeNumber.IsPrimeNumber);
    #endregion Static members

    #region Implemented interfaces
    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
      => GetDescendingPrimes(StartAt);
    // IEnumerable
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
#endif
