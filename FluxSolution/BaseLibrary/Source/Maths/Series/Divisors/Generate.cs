using System.Linq;

namespace Flux
{
  // https://codeforces.com/blog/entry/22229
  public static partial class Maths
  {
    /// <summary>Creates a sieve of divisors up to the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static int[] GenerateCountOfDivisors(int number)
    {
      var counts = new int[number + 1];
      for (var i = 1; i <= number; i++)
        for (var j = i; j <= number; j += i)
          counts[j]++;
      return counts;
    }

    /// <summary>Creates a sieve of divisors up to the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static int[] GenerateSumOfDivisors(int number)
    {
      var sums = new int[number + 1];
      for (var i = 1; i <= number; i++)
        for (var j = i; j <= number; j += i)
          sums[j] += i;
      return sums;
    }

    /// <summary>Creates a sieve of divisors up to the specified number.</summary>
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
  }
}
