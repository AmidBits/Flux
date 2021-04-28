//using System.Linq;

//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>Returns a sequence of teim primes, each of which is a pair of primes that differ by two.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Twin_prime"/>
//    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger)> GetTwinPrimes()
//      => GetAscendingPrimes(SmallestPrime.ToBigInteger()).PartitionTuple2(false, (leading, trailing, index) => (leading, trailing)).Where((t) => t.trailing - t.leading == 2);
//  }
//}
