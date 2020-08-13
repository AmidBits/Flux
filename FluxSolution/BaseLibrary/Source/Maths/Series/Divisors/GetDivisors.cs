using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Results in a sequence of divisors for the specified number, with the option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <remarks>This implementaion does not order the result, and the first element contains the number itself, so if only proper divsors are needed, skip the first element.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDivisors(System.Numerics.BigInteger number)
    {
      var sqrt = number.Sqrt();

      for (int counter = 1; counter <= sqrt; counter++)
      {
        if (number % counter == 0)
        {
          yield return counter;

          if (number / counter is var quotient && quotient != counter) yield return quotient;
        }
      }
    }

    /// <summary>Results in a sequence of divisors for the specified number, with the option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <remarks>This implementaion does not order the result, and the first element contains the number itself, so if only proper divsors are needed, skip the first element.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<int> GetDivisors(int number)
    {
      var sqrt = System.Math.Sqrt(number);

      for (int counter = 1; counter <= sqrt; counter++)
      {
        if (number % counter == 0)
        {
          yield return counter;

          if (number / counter is var quotient && quotient != counter) yield return quotient;
        }
      }
    }
    /// <summary>Results in a sequence of divisors for the specified number, with the option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <remarks>This implementaion does not order the result, and the first element contains the number itself, so if only proper divsors are needed, skip the first element.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<long> GetDivisors(long number)
    {
      var sqrt = System.Math.Sqrt(number);

      for (long counter = 1; counter <= sqrt; counter++)
      {
        if (number % counter == 0)
        {
          yield return counter;

          if (number / counter is var quotient && quotient != counter) yield return quotient;
        }
      }
    }

    /// <summary>Results in a sequence of divisors for the specified number, with the option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <remarks>This implementaion does not order the result, and the first element contains the number itself, so if only proper divsors are needed, skip the first element.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<uint> GetDivisors(uint number)
    {
      var sqrt = System.Math.Sqrt(number);

      for (uint counter = 1; counter <= sqrt; counter++)
      {
        if (number % counter == 0)
        {
          yield return counter;

          if (number / counter is var quotient && quotient != counter) yield return quotient;
        }
      }
    }
    /// <summary>Results in a sequence of divisors for the specified number, with the option of only proper divisors (divisors including 1 but not itself).</summary>
    /// <remarks>This implementaion does not order the result, and the first element contains the number itself, so if only proper divsors are needed, skip the first element.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<ulong> GetDivisors(ulong number)
    {
      var sqrt = System.Math.Sqrt(number);

      for (ulong counter = 1; counter <= sqrt; counter++)
      {
        if (number % counter == 0)
        {
          yield return counter;

          if (number / counter is var quotient && quotient != counter) yield return quotient;
        }
      }
    }
  }
}
