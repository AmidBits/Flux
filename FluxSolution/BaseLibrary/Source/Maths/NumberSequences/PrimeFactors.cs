#if NET7_0_OR_GREATER
namespace Flux.NumberSequences
{
  public sealed class PrimeFactors
    : INumberSubset<System.Numerics.BigInteger>
  {
    #region Static methods

    /// <summary>Results in a sequence of prime factors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetPrimeFactors<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      foreach (var prime in PrimeNumber.GetAscendingPrimes(TSelf.CreateChecked(2)))
        while (TSelf.IsZero(number % prime))
        {
          yield return prime;

          number /= prime;

          if (number < prime)
            yield break;
        }
    }

    #endregion Static methods

    #region Implemented interfaces

    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSubset(System.Numerics.BigInteger number) => GetPrimeFactors(number);

    #endregion Implemented interfaces
  }
}
#endif
