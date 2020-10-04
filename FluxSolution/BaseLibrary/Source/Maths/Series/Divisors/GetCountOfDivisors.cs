namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Numerics.BigInteger GetDivisorCount(System.Numerics.BigInteger value)
    {
      System.Numerics.BigInteger count = 0;
      var sqrt = value.Sqrt();
      for (System.Numerics.BigInteger counter = 1; counter <= sqrt; counter++)
        if (value % counter == 0)
          count += (value / counter == counter ? 1 : 2);
      return count;
    }

    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static int GetCountOfDivisors(int value)
    {
      var count = 0;
      var sqrt = System.Math.Sqrt(value);
      for (int counter = 1; counter <= sqrt; counter++)
        if (value % counter == 0)
          count += (value / counter == counter ? 1 : 2);
      return count;
    }
    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static long GetCountOfDivisors(long value)
    {
      var count = 0L;
      var sqrt = System.Math.Sqrt(value);
      for (var counter = 1L; counter <= sqrt; counter++)
        if (value % counter == 0)
          count += (value / counter == counter ? 1 : 2);
      return count;
    }

    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    [System.CLSCompliant(false)]
    public static uint GetCountOfDivisors(uint value)
    {
      var count = 0U;
      var sqrt = System.Math.Sqrt(value);
      for (var counter = 1U; counter <= sqrt; counter++)
        if (value % counter == 0)
          count += (value / counter == counter ? 1U : 2U);
      return count;
    }
    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    [System.CLSCompliant(false)]
    public static ulong GetCountOfDivisors(ulong value)
    {
      var count = 0UL;
      var sqrt = System.Math.Sqrt(value);
      for (ulong counter = 1; counter <= sqrt; counter++)
        if (value % counter == 0)
          count += (value / counter == counter ? 1UL : 2UL);
      return count;
    }
  }
}
