namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <remarks>This implementaion does not order the result.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetDivisors<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (number > TSelf.Zero)
      {
        var sqrt = Maths.IntegerSqrt(number);

        for (var counter = TSelf.One; counter <= sqrt; counter++)
        {
          var (quotient, reminder) = TSelf.DivRem(number, counter);

          if (TSelf.IsZero(reminder))
          {
            yield return counter;

            if (quotient != counter)
              yield return quotient;
          }
        }
      }
    }

#else


    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <remarks>This implementaion does not order the result.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDivisors(System.Numerics.BigInteger number)
    {
      if (number > 0)
      {
        var sqrt = Maths.IntegerSqrt(number);

        for (var counter = 1; counter <= sqrt; counter++)
          if (number % counter == 0)
          {
            yield return counter;

            if (number / counter is var quotient && quotient != counter)
              yield return quotient;
          }
      }
    }

#endif
  }
}
