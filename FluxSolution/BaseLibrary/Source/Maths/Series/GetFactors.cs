namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetFactors(System.Numerics.BigInteger value)
    {
      yield return System.Numerics.BigInteger.One;
      yield return value;

      for (System.Numerics.BigInteger divisor = 2, partner = value; divisor < partner; divisor++)
      {
        if (value % divisor == 0)
        {
          yield return divisor;

          if ((partner = value / divisor) != divisor) yield return partner;
        }
      }
    }

    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<int> GetFactors(int value)
    {
      yield return 1;
      yield return value;

      for (int factor = 2, cutoff = value; factor < cutoff; factor++)
      {
        if (value % factor == 0)
        {
          yield return factor;

          cutoff = value / factor;

          if (cutoff != factor)
          {
            yield return cutoff;
          }
        }
      }
    }
    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.IEnumerable<long> GetFactors(long value)
    {
      yield return 1;
      yield return value;

      for (long factor = 2, cutoff = value; factor < cutoff; factor++)
      {
        if (value % factor == 0)
        {
          yield return factor;

          cutoff = value / factor;

          if (cutoff != factor)
          {
            yield return cutoff;
          }
        }
      }
    }

    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<uint> GetFactors(uint value)
    {
      yield return 1;
      yield return value;

      for (uint factor = 2, cutoff = value; factor < cutoff; factor++)
      {
        if (value % factor == 0)
        {
          yield return factor;

          cutoff = value / factor;

          if (cutoff != factor)
          {
            yield return cutoff;
          }
        }
      }
    }
    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<ulong> GetFactors(ulong value)
    {
      yield return 1;
      yield return value;

      for (ulong factor = 2, cutoff = value; factor < cutoff; factor++)
      {
        if (value % factor == 0)
        {
          yield return factor;

          cutoff = value / factor;

          if (cutoff != factor)
          {
            yield return cutoff;
          }
        }
      }
    }
  }
}
