// https://codeforces.com/blog/entry/22229

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Generates an array of divisor counts of all numbers less than or equal to the specified number. This is done as with the sum of divisors, only increase by 1 instead of by the divisor.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static int[] GenerateCountOfDivisors(int number)
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
    public static int[] GenerateSumOfDivisors(int number)
    {
      var sums = new int[number + 1];
      for (var i = 1; i <= number; i++)
        for (var j = i; j <= number; j += i)
          sums[j] += i;
      return sums;
    }
  }
}
