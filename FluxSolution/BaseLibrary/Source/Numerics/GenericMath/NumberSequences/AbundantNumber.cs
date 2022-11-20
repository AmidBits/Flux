namespace Flux.NumberSequences
{
  public sealed class AbundantNumber
    : INumericSequence<System.Numerics.BigInteger>
  {
    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger n, System.Numerics.BigInteger sum)> GetAbundantNumbers()
      => Enumerable.Loop(() => (System.Numerics.BigInteger)3, e => true, e => e + 1, e => e).AsParallel().AsOrdered().Select(n => (n, sum: NumberSequences.Factors.GetSumOfDivisors(n) - n)).Where(x => x.sum > x.n);

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

    #region Implemented interfaces
    // INumberSequence
    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
      => System.Linq.Enumerable.Select(GetAbundantNumbers(), t => t.n);

    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
