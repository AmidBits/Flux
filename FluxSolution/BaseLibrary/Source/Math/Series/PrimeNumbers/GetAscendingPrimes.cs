using System.Linq;

namespace Flux
{
  public static partial class Math
  {
    /// <summary>Creates a new sequence ascending prime numbers, greater than the specified number.</summary>
    /// <see cref="https://math.stackexchange.com/questions/164767/prime-number-generator-how-to-make"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetAscendingPrimes(System.Numerics.BigInteger startAt)
      => GetAscendingPotentialPrimes(startAt).Where(IsPrimeNumber);

    /// <summary>Creates a new sequence ascending prime numbers, greater than the specified number.</summary>
    /// <see cref="https://math.stackexchange.com/questions/164767/prime-number-generator-how-to-make"/>
    public static System.Collections.Generic.IEnumerable<int> GetAscendingPrimes(int startAt)
      => GetAscendingPotentialPrimes(startAt).Where(IsPrimeNumber);
    /// <summary>Creates a new sequence ascending prime numbers, greater than the specified number.</summary>
    /// <see cref="https://math.stackexchange.com/questions/164767/prime-number-generator-how-to-make"/>
    public static System.Collections.Generic.IEnumerable<long> GetAscendingPrimes(long startAt)
      => GetAscendingPotentialPrimes(startAt).Where(IsPrimeNumber);
  }
}
