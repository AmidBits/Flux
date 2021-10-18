namespace Flux.Numerics
{
  public class Factors
    : ASequencedNumbers<System.Numerics.BigInteger>
  {
    public System.Numerics.BigInteger Number { get; }

    public Factors(System.Numerics.BigInteger number)
      => Number = number;

    private System.Numerics.BigInteger? m_aliquotSum;
    public System.Numerics.BigInteger AliquotSum
    {
      get
      {
        if (!m_aliquotSum.HasValue)
          Compute();

        return m_aliquotSum!.Value;
      }
    }

    private System.Numerics.BigInteger? m_count;
    public System.Numerics.BigInteger Count
    {
      get
      {
        if (!m_count.HasValue)
          Compute();

        return m_count!.Value;
      }
    }

    private System.Numerics.BigInteger? m_sum;
    public System.Numerics.BigInteger Sum
    {
      get
      {
        if (!m_sum.HasValue)
          Compute();

        return m_sum!.Value;
      }
    }

    private void Compute()
    {
      foreach (var divisor in GetNumberSequence())
      {
        m_sum = m_sum.HasValue ? m_sum + divisor : divisor;
        m_count = m_count.HasValue ? m_count + 1 : 1;
      }

      m_aliquotSum = m_sum - Number;
    }

    // INumberSequence
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
    {
      foreach (var divisor in GetFactors(Number))
        yield return divisor;
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
    public static System.Numerics.BigInteger GetCountOfFactors(System.Numerics.BigInteger number)
    {
      var count = System.Numerics.BigInteger.Zero;
      var sqrt = Maths.ISqrt(number);
      for (var counter = System.Numerics.BigInteger.One; counter <= sqrt; counter++)
        if (number % counter == 0)
          count += (number / counter == counter ? 1 : 2);
      return count;
    }
    /// <summary>Results in a sequence of divisors for the specified number, with the option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <remarks>This implementaion does not order the result.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetFactors(System.Numerics.BigInteger number)
    {
      yield return 1;
      yield return number;

      var sqrt = Maths.ISqrt(number);
      for (var counter = 2; counter <= sqrt; counter++)
        if (number % counter == 0)
        {
          yield return counter;
          if (number / counter is var quotient && quotient != counter)
            yield return quotient;
        }
    }
    /// <summary>Results in a sequence of divisors for the specified number, with option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static System.Numerics.BigInteger GetSumOfFactors(System.Numerics.BigInteger number)
    {
      var sum = System.Numerics.BigInteger.Zero;
      var sqrt = Maths.ISqrt(number);
      for (var counter = System.Numerics.BigInteger.One; counter <= sqrt; counter++)
        if (number % counter == 0)
        {
          sum += counter;
          if (number / counter is var quotient && quotient != counter)
            sum += quotient;
        }
      return sum;
    }

    /// <summary>Determines whether the number is a deficient number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Deficient_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static bool IsDeficientNumber(System.Numerics.BigInteger number)
      => GetSumOfFactors(number) - number < number;
    /// <summary>Determines whether the number is a perfect number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Perfect_number"/>
    public static bool IsPerfectNumber(System.Numerics.BigInteger number)
      => GetSumOfFactors(number) - number == number;
    #endregion Static methods
  }
}
