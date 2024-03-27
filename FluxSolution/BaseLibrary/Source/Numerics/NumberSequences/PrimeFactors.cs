namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Results in a sequence of prime factors for the specified number.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetPrimeFactors<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      foreach (var prime in GetAscendingPrimes(TSelf.CreateChecked(2)))
        while (TSelf.IsZero(number % prime))
        {
          yield return prime;

          number /= prime;

          if (number < prime)
            yield break;
        }
    }
  }
}
