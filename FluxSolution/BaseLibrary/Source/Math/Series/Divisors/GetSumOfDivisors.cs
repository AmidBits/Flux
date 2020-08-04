using System.Linq;

namespace Flux
{
  public static partial class Math
  {
    /// <summary>Results in a sequence of divisors for the specified number, with option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static System.Numerics.BigInteger GetSumOfDivisors(System.Numerics.BigInteger number)
    {
      var sum = System.Numerics.BigInteger.Zero;

      var sqrt = number.Sqrt();

      for (int counter = 1; counter <= sqrt; counter++)
      {
        if (number % counter == 0)
        {
          sum += counter;

          if (number / counter is var quotient && quotient != counter) sum += quotient;
        }
      }

      return sum;
    }

    /// <summary>Results in a sequence of divisors for the specified number, with option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static int GetSumOfDivisors(int number)
    {
      var sum = 0;

      var sqrt = System.Math.Sqrt(number);

      for (int counter = 1; counter <= sqrt; counter++)
      {
        if (number % counter == 0)
        {
          sum += counter;

          if (number / counter is var quotient && quotient != counter) sum += quotient;
        }
      }

      return sum;
    }
    /// <summary>Results in a sequence of divisors for the specified number, with option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
    public static long GetSumOfDivisors(long number)
    {
      var sum = 0L;

      var sqrt = System.Math.Sqrt(number);

      for (long counter = 1; counter <= sqrt; counter++)
      {
        if (number % counter == 0)
        {
          sum += counter;

          if (number / counter is var quotient && quotient != counter) sum += quotient;
        }
      }

      return sum;
    }

    /// <summary>Results in a sequence of divisors for the specified number, with option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    [System.CLSCompliant(false)]
    public static uint GetSumOfDivisors(uint number)
    {
      var sum = 0U;

      var sqrt = System.Math.Sqrt(number);

      for (uint counter = 1; counter <= sqrt; counter++)
      {
        if (number % counter == 0)
        {
          sum += counter;

          if (number / counter is var quotient && quotient != counter) sum += quotient;
        }
      }

      return sum;
    }
    /// <summary>Results in a sequence of divisors for the specified number, with option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    [System.CLSCompliant(false)]
    public static ulong GetSumOfDivisors(ulong number)
    {
      var sum = 0UL;

      var sqrt = System.Math.Sqrt(number);

      for (ulong counter = 1; counter <= sqrt; counter++)
      {
        if (number % counter == 0)
        {
          sum += counter;

          if (number / counter is var quotient && quotient != counter) sum += quotient;
        }
      }

      return sum;
    }
  }
}
