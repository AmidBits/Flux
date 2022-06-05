namespace Flux.Numerics
{
  public record class PrimeFactors
    : INumberSequenceable<System.Numerics.BigInteger>
  {
    public System.Numerics.BigInteger Number { get; }

    public PrimeFactors(System.Numerics.BigInteger number)
      => Number = number;

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
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetPrimeFactors(Number);

    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetNumberSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
