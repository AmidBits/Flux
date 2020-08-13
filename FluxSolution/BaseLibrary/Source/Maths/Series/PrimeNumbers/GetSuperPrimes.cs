using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns a sequence of super-primes, which is a subsequence of prime numbers that occupy prime-numbered positions within the sequence of all prime numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Super-prime"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSuperPrimes() => GetAscendingPrimes(SmallestPrime.ToBigInteger()).Where((p, i) => IsPrimeNumber(((System.Numerics.BigInteger)i + System.Numerics.BigInteger.One)));
  }
}
