using System.Linq;

namespace Flux.Numerics
{
  public class AbundantNumber
    : ANumberSequenceable<System.Numerics.BigInteger>
  {
    // INumberSequence
    [System.Diagnostics.Contracts.Pure]
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => System.Linq.Enumerable.Select(GetAbundantNumbers(), t => t.n);

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger n, System.Numerics.BigInteger sum)> GetAbundantNumbers()
      => Flux.Linq.Enumerable.Range((System.Numerics.BigInteger)3, ulong.MaxValue, 1).AsParallel().AsOrdered().Select(n => (n, sum: Numerics.Factors.GetSumOfDivisors(n) - n)).Where(x => x.sum > x.n);

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Highly_abundant_number"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>> GetHighlyAbundantNumbers()
    {
      var largestSumOfDivisors = System.Numerics.BigInteger.Zero;
      for (var index = System.Numerics.BigInteger.One; ; index++)
        if (Factors.GetSumOfDivisors(index) is var sumOfDivisors && sumOfDivisors > largestSumOfDivisors)
        {
          yield return new System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>(index, sumOfDivisors);
          largestSumOfDivisors = sumOfDivisors;
        }
    }

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Superabundant_number"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>> GetSuperAbundantNumbers()
    {
      var largestValue = 0.0;
      foreach (var kvp in GetHighlyAbundantNumbers())
        if ((double)kvp.Value / (double)kvp.Key is var value && value > largestValue)
        {
          yield return kvp;
          largestValue = value;
        }
    }

    /// <summary>Determines whether the number is an abundant number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Abundant_number"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsAbundantNumber(System.Numerics.BigInteger value)
      => Factors.GetSumOfDivisors(value) - value > value;
    #endregion Static methods
  }
}
