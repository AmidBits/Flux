using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns a sequence of cousine primes, each of which is a pair of primes that differ by four.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cousin_prime"/>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger, int Index)> GetCousinePrimes()
    {
      var counter = 0;

      foreach (var (leading, midling, trailing) in GetAscendingPrimes(SmallestPrime).PartitionTuple(0, (leading, midling, trailing, index) => (leading, midling, trailing)))
      {
        if (midling - leading == 4)
        {
          yield return (leading, midling, counter);
        }
        else if (trailing - leading == 4)
        {
          yield return (leading, trailing, counter);
        }

        counter++;
      }
    }
  }
}
