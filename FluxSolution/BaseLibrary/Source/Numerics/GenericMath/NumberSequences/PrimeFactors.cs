#if NET7_0_OR_GREATER
namespace Flux.NumberSequencing
{
  public sealed class PrimeFactors
    : INumberSubset<System.Numerics.BigInteger>
  {
    #region Static methods
    /// <summary>Results in a sequence of prime factors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPrimeFactors(System.Numerics.BigInteger number)
    {
      foreach (var prime in Flux.Numerics.PrimeNumber.GetAscendingPrimes(2))
        while ((number % prime) == 0)
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
    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSubset(System.Numerics.BigInteger number)
      => GetPrimeFactors(number);
    #endregion Implemented interfaces
  }
}
#endif