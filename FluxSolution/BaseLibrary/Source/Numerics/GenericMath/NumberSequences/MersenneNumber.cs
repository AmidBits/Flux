#if NET7_0_OR_GREATER
namespace Flux.NumberSequences
{
  /// <summary>Results in a sequence of mersenne numbers.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
  public sealed class MersenneNumber
    : INumericSequence<System.Numerics.BigInteger>
  {
    #region Static methods
    /// <summary>Results in the mersenne number for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public static System.Numerics.BigInteger GetMersenneNumber(int number)
      => System.Numerics.BigInteger.Pow(2, number) - 1;
    /// <summary>Results in a sequence of mersenne numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetMersenneNumbers()
    {
      for (var number = 1; number <= int.MaxValue; number++)
        yield return GetMersenneNumber(number);
    }
    /// <summary>Results in a sequence of mersenne primes.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_prime"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetMersennePrimes()
      => System.Linq.Enumerable.Where(GetMersenneNumbers(), PrimeNumber.IsPrimeNumber);
    #endregion Static methods

    #region Implemented interfaces
    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
      => GetMersenneNumbers();

    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
#endif
