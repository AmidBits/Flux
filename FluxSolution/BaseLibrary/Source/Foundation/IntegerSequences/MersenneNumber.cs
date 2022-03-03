namespace Flux.Numerics
{
  public sealed class MersenneNumber
  : ANumberSequenceable<System.Numerics.BigInteger>
  {
    // INumberSequence
    /// <summary>Results in a sequence of mersenne numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetMersenneNumbers();

    #region Static methods
    /// <summary>Results in the mersenne number for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public static System.Numerics.BigInteger GetMersenneNumber(int number)
      => System.Numerics.BigInteger.Pow(2, number) - 1;
    /// <summary>Results in a sequence of mersenne numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
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
  }
}
