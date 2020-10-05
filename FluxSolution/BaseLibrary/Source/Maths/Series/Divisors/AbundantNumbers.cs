namespace Flux
{
  public static partial class Maths
  {
    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Highly_abundant_number"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>> GetHighlyAbundantNumbers()
    {
      var largestSumOfDivisors = System.Numerics.BigInteger.Zero;
      for (var index = System.Numerics.BigInteger.One; ; index++)
        if (GetSumOfDivisors(index) is var sumOfDivisors && sumOfDivisors > largestSumOfDivisors)
        {
          yield return new System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, System.Numerics.BigInteger>(index, sumOfDivisors);
          largestSumOfDivisors = sumOfDivisors;
        }
    }

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Superabundant_number"/>
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
    public static bool IsAbundantNumber(System.Numerics.BigInteger value) => GetSumOfDivisors(value) - value > value;
    /// <summary>Determines whether the number is an abundant number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Abundant_number"/>
    public static bool IsAbundantNumber(int value) => GetSumOfDivisors(value) - value > value;
    /// <summary>Determines whether the number is an abundant number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Abundant_number"/>
    public static bool IsAbundantNumber(long value) => GetSumOfDivisors(value) - value > value;
  }
}
