namespace Flux.Numerics
{
  public sealed class PrimeFactors
    : ASequencedNumbers<System.Numerics.BigInteger>
  {
    public System.Numerics.BigInteger Number { get; }

    public PrimeFactors(System.Numerics.BigInteger number)
      => Number = number;

    // INumberSequence
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
    {
      foreach (var primeFactor in GetPrimeFactors(Number))
        yield return primeFactor;
    }

    #region Static methods
    /// <summary>Results in a sequence of prime factors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
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
  }
}
