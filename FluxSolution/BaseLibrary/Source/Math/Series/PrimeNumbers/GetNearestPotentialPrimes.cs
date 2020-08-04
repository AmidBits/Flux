namespace Flux
{
  public static partial class Math
  {
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNearestPotentialPrimes(System.Numerics.BigInteger number)
    {
      var quotient = System.Numerics.BigInteger.DivRem(number, 6, out var remainder);

      var lo = quotient * 6;
      var hi = lo + 6;

      if (number == 3)
      {
        yield return 3;
        yield return 2;
        lo = -6;
      }
      else if (number == 2)
      {
        yield return 2;
        yield return 3;
        lo = -6;
      }

      if (quotient == 0 || remainder >= 3)
      {
        while (true)
        {
          yield return hi - 1;
          if (lo >= 6) yield return lo + 1;
          else if (lo == 0) yield return 3;
          yield return hi + 1;
          if (lo >= 6) yield return lo - 1;
          else if (lo == 0) yield return 2;
          hi += 6;
          lo -= 6;
        }
      }
      else
      {
        while (true)
        {
          if (lo > 0) yield return lo + 1;
          else if (lo == 0) yield return 3;
          yield return hi - 1;
          if (lo > 0) yield return lo - 1;
          else if (lo == 0) yield return 2;
          yield return hi + 1;
          lo -= 6;
          hi += 6;
        }
      }
    }

    public static System.Collections.Generic.IEnumerable<int> GetNearestPotentialPrimes(int number)
    {
      var quotient = System.Math.DivRem(number, 6, out var remainder);

      var lo = quotient * 6;
      var hi = lo + 6;

      if (number == 3)
      {
        yield return 3;
        yield return 2;
        lo = -6;
      }
      else if (number == 2)
      {
        yield return 2;
        yield return 3;
        lo = -6;
      }

      if (quotient == 0 || remainder >= 3)
      {
        while (true)
        {
          yield return hi - 1;
          if (lo >= 6) yield return lo + 1;
          else if (lo == 0) yield return 3;
          yield return hi + 1;
          if (lo >= 6) yield return lo - 1;
          else if (lo == 0) yield return 2;
          hi += 6;
          lo -= 6;
        }
      }
      else
      {
        while (true)
        {
          if (lo > 0) yield return lo + 1;
          else if (lo == 0) yield return 3;
          yield return hi - 1;
          if (lo > 0) yield return lo - 1;
          else if (lo == 0) yield return 2;
          yield return hi + 1;
          lo -= 6;
          hi += 6;
        }
      }
    }
    public static System.Collections.Generic.IEnumerable<long> GetNearestPotentialPrimes(long number)
    {
      var quotient = System.Math.DivRem(number, 6, out var remainder);

      var lo = quotient * 6;
      var hi = lo + 6;

      if (number == 3)
      {
        yield return 3;
        yield return 2;
        lo = -6;
      }
      else if (number == 2)
      {
        yield return 2;
        yield return 3;
        lo = -6;
      }

      if (quotient == 0 || remainder >= 3)
      {
        while (true)
        {
          yield return hi - 1;
          if (lo >= 6) yield return lo + 1;
          else if (lo == 0) yield return 3;
          yield return hi + 1;
          if (lo >= 6) yield return lo - 1;
          else if (lo == 0) yield return 2;
          hi += 6;
          lo -= 6;
        }
      }
      else
      {
        while (true)
        {
          if (lo > 0) yield return lo + 1;
          else if (lo == 0) yield return 3;
          yield return hi - 1;
          if (lo > 0) yield return lo - 1;
          else if (lo == 0) yield return 2;
          yield return hi + 1;
          lo -= 6;
          hi += 6;
        }
      }
    }
  }
}
