namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Results in the mersenne number for the specified number.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public static TSelf GetMersenneNumber<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (TSelf.One + TSelf.One).IntegerPow(number) - TSelf.One;

    /// <summary>Results in a sequence of mersenne numbers.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetMersenneNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var number = TSelf.One; ; number++)
        yield return GetMersenneNumber(number);
    }

    /// <summary>Results in a sequence of mersenne primes.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Mersenne_prime"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetMersennePrimes<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => System.Linq.Enumerable.Where(GetMersenneNumbers<TSelf>(), IsPrimeNumber);
  }
}
