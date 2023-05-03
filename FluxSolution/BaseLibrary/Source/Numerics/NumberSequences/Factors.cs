namespace Flux.NumberSequences
{
  public record class Factors
    : INumericSequence<System.Numerics.BigInteger>, INumberSubset<System.Numerics.BigInteger>
  {
    private System.Numerics.BigInteger? m_count;
    private System.Numerics.BigInteger? m_sum;

    public System.Numerics.BigInteger Number { get; }

    public Factors(System.Numerics.BigInteger number) => Number = number;

    public System.Numerics.BigInteger AliquotSum => Sum - Number;

    public System.Numerics.BigInteger Count => m_count.HasValue ? m_count.Value : Compute().count;
    public System.Numerics.BigInteger Sum => m_sum.HasValue ? m_sum.Value : Compute().sum;

    private (System.Numerics.BigInteger sum, System.Numerics.BigInteger count) Compute()
    {
      m_sum = 0;
      m_count = 0;

      foreach (var divisor in GetSequence())
      {
        m_sum = m_sum.Value + divisor;
        m_count = m_count.Value + 1;
      }

      return (m_sum.Value, m_count.Value);
    }

    #region Static methods

    // https://codeforces.com/blog/entry/22229
    /// <summary>Generates an array of divisor counts of all numbers less than or equal to the specified number. This is done as with the sum of divisors, only increase by 1 instead of by the divisor.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static int[] GenerateCountOfFactors(int number)
    {
      var counts = new int[number + 1];

      for (var i = 1; i <= number; i++)
        for (var j = i; j <= number; j += i)
          counts[j]++;

      return counts;
    }
    /// <summary>Generates am array of Euler totient values for numbers up to the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static int[] GenerateEulerTotient(int number)
    {
      var totient = new int[number + 1];

      for (var i = 1; i <= number; i++)
        totient[i] = i;
      for (var i = 2; i <= number; i++)
        if (totient[i] == i)
          for (var j = i; j <= number; j += i)
            totient[j] -= totient[j] / i;

      return totient;
    }

    /// <summary>Generates am array of the largest prime factors for numbers up to the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static int[] GenerateLargestPrimeFactor(int number)
    {
      var factor = new int[number + 1];

      System.Array.Fill(factor, 1);
      for (var i = 1; i <= number; i++)
        if (factor[i] == 1)
          for (var j = i; j <= number; j += i)
            factor[j] = i;

      return factor;
    }

    /// <summary>Generates an array of divisor sums of all numbers less than or equal to the specified number. This is done as the count of divisors, only we increase by the divisor instead of by 1.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static int[] GenerateSumOfFactors(int number)
    {
      var sums = new int[number + 1];

      for (var i = 1; i <= number; i++)
        for (var j = i; j <= number; j += i)
          sums[j] += i;

      return sums;
    }

    /// <summary>Returns the count of divisors in the sequence for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Numerics.BigInteger GetCountOfDivisors(System.Numerics.BigInteger number) => System.Linq.Enumerable.Count(GetDivisors(number));

    /// <summary>Returns the count of proper divisors in the sequence for the specified number (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Numerics.BigInteger GetCountOfProperDivisors(System.Numerics.BigInteger number) => System.Linq.Enumerable.Count(GetProperDivisors(number));

    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <remarks>This implementaion does not order the result.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDivisors(System.Numerics.BigInteger number)
    {
      if (number > 0)
      {
        var sqrt = GenericMath.IntegerSqrt(number);

        for (var counter = 1; counter <= sqrt; counter++)
          if (number % counter == 0)
          {
            yield return counter;

            if (number / counter is var quotient && quotient != counter)
              yield return quotient;
          }
      }
    }

    /// <summary>Results in a sequence of proper divisors for the specified number (divisors including 1 but not itself).</summary>
    /// <remarks>This implementaion does not order the result.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetProperDivisors(System.Numerics.BigInteger number) => System.Linq.Enumerable.Where(GetDivisors(number), bi => bi != number);

    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static System.Numerics.BigInteger GetSumOfDivisors(System.Numerics.BigInteger number) => Enumerable.Sum(GetDivisors(number));

    /// <summary>Results in a sequence of proper divisors for the specified number (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static System.Numerics.BigInteger GetSumOfProperDivisors(System.Numerics.BigInteger number) => Enumerable.Sum(GetProperDivisors(number));

    /// <summary>Determines whether the number is a deficient number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Deficient_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static bool IsDeficientNumber(System.Numerics.BigInteger number) => GetSumOfDivisors(number) - number < number;

    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perfect_number"/>
    public static bool IsPerfectNumber(System.Numerics.BigInteger number) => GetSumOfDivisors(number) - number == number;

    #endregion Static methods

    #region Implemented interfaces

    // INumericSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
    {
      foreach (var divisor in GetDivisors(Number))
        yield return divisor;
    }

    // INumberSubset
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSubset(System.Numerics.BigInteger number)
    {
      foreach (var divisor in GetDivisors(Number))
        yield return divisor;
    }

    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator() => GetSequence().GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion Implemented interfaces
  }
}
