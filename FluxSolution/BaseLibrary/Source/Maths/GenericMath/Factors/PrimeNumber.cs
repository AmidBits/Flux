namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Creates a new sequence of ascending potential primes, greater-or-equal than the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> GetAscendingPotentialPrimes<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var six = TSelf.CreateChecked(6);

      var (quotient, remainder) = TSelf.DivRem(startAt, six);

      var multiple = six * (quotient + (remainder > TSelf.One ? TSelf.One : TSelf.Zero));

      if (quotient == TSelf.Zero) // If startAt is less than 6.
      {
        var two = TSelf.CreateChecked(2);
        var three = TSelf.CreateChecked(3);

        if (remainder <= two) yield return two;
        if (remainder <= three) yield return three;

        multiple = six;
      }
      else if (remainder <= TSelf.One) // Or, either between two potential primes or on right of a % 6 number. E.g. 12 or 13.
      {
        yield return multiple + TSelf.One;
        multiple += six;
      }

      while (true)
      {
        yield return multiple - TSelf.One;
        yield return multiple + TSelf.One;

        multiple += six;
      }
    }

    /// <summary>Indicates whether a specified number is a prime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Primality_test"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
    public static bool IsPrimeNumber<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (number <= TSelf.CreateChecked(3))
        return number >= TSelf.CreateChecked(2);

      var six = TSelf.CreateChecked(6);

      if (number % six is var remainder && (remainder != TSelf.One && remainder != TSelf.CreateChecked(5)))
        return false;

      var limit = number.IntegerSqrt();

      for (var k = TSelf.CreateChecked(5); k <= limit; k += six)
        if (TSelf.IsZero(number % k) || TSelf.IsZero(number % (k + TSelf.One + TSelf.One)))
          return false;

      return true;
    }

#else

    /// <summary>Creates a new sequence of ascending potential primes, greater than the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetAscendingPotentialPrimes(System.Numerics.BigInteger startAt)
    {
      var quotient = System.Numerics.BigInteger.DivRem(startAt, 6, out var remainder);

      var multiple = 6 * (quotient + (remainder > 1 ? 1 : 0));

      if (quotient == 0) // If startAt is less than 6.
      {
        if (startAt <= 2) yield return 2;
        if (startAt <= 3) yield return 3;

        multiple = 6;
      }
      else if (remainder <= 1) // Or, either between two potential primes or on right of a % 6 number. E.g. 12 or 13.
      {
        yield return multiple + 1;
        multiple += 6;
      }

      while (true)
      {
        yield return multiple - 1;
        yield return multiple + 1;

        multiple += 6;
      }
    }
    
    /// <summary>Indicates whether a specified number is a prime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Primality_test"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
    public static bool IsPrimeNumber(System.Numerics.BigInteger number)
    {
      if (number <= long.MaxValue)
        return IsPrimeNumber((long)number);

      if (number <= 3)
        return number >= 2;

      if (number % 2 == 0 || number % 3 == 0)
        return false;

      var limit = number.IntegerSqrt();

      for (var k = 5; k <= limit; k += 6)
        if ((number % k) == 0 || (number % (k + 2)) == 0)
          return false;

      return true;
    }

#endif
  }
}
