using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Creates a new sequence descending prime numbers, less than the specified number.</summary>
    /// <see cref="https://math.stackexchange.com/questions/164767/prime-number-generator-how-to-make"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDescendingPrimes(System.Numerics.BigInteger startAt) 
      => GetDescendingPotentialPrimes(startAt).Where(IsPrimeNumber);

    /// <summary>Creates a new sequence descending prime numbers, less than the specified number.</summary>
    /// <see cref="https://math.stackexchange.com/questions/164767/prime-number-generator-how-to-make"/>
    public static System.Collections.Generic.IEnumerable<int> GetDescendingPrimes(int startAt)
      => GetDescendingPotentialPrimes(startAt).Where(IsPrimeNumber);
    /// <summary>Creates a new sequence descending prime numbers, less than the specified number.</summary>
    /// <see cref="https://math.stackexchange.com/questions/164767/prime-number-generator-how-to-make"/>
    public static System.Collections.Generic.IEnumerable<long> GetDescendingPrimes(long startAt) 
      => GetDescendingPotentialPrimes(startAt).Where(IsPrimeNumber);
  }
}
