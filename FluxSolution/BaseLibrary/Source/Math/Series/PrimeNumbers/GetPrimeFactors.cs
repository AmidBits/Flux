using System.Linq;

namespace Flux
{
  public static partial class Math
  {
    private static readonly int[] m_primeFactorWheelIncrements = new int[] { 4, 2, 4, 2, 4, 6, 2, 6 };

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Factorization"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Wheel_factorization"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPrimeFactors(System.Numerics.BigInteger number)
    {
      if (number < 1) throw new System.ArgumentOutOfRangeException(nameof(number));

      while (number % 2 == 0)
      {
        yield return 2;
        number /= 2;
      }

      while (number % 3 == 0)
      {
        yield return 3;
        number /= 3;
      }

      while (number % 5 == 0)
      {
        yield return 5;
        number /= 5;
      }

      System.Numerics.BigInteger k = 7, k2 = k * k;
      var index = 0;

      while (k2 <= number)
      {
        if (number % k == 0)
        {
          yield return k;
          number /= k;
        }
        else
        {
          k += (uint)m_primeFactorWheelIncrements[index++];
          k2 = k * k;

          if (index >= m_primeFactorWheelIncrements.Length) index = 0;
        }
      }

      if (number > 1) yield return number;
    }

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Factorization"/>
    /// <seealso cref="https://stackoverflow.com/questions/5872962/prime-factors-in-c-sharp"/>
    public static System.Collections.Generic.IEnumerable<int> GetPrimeFactors(int number)
    {
      if (number < 1) throw new System.ArgumentOutOfRangeException(nameof(number));

      while (number % 2 == 0)
      {
        yield return 2;
        number /= 2;
      }

      while (number % 3 == 0)
      {
        yield return 3;
        number /= 3;
      }

      while (number % 5 == 0)
      {
        yield return 5;
        number /= 5;
      }

      int k = 7, k2 = k * k;
      var index = 0;

      while (k2 <= number)
      {
        if (number % k == 0)
        {
          yield return k;
          number /= k;
        }
        else
        {
          k += m_primeFactorWheelIncrements[index++];
          k2 = k * k;

          if (index >= m_primeFactorWheelIncrements.Length) index = 0;
        }
      }

      if (number > 1) yield return number;
    }
    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Factorization"/>
    /// <seealso cref="https://stackoverflow.com/questions/5872962/prime-factors-in-c-sharp"/>
    public static System.Collections.Generic.IEnumerable<long> GetPrimeFactors(long number)
    {
      if (number < 1) throw new System.ArgumentOutOfRangeException(nameof(number));

      while (number % 2 == 0)
      {
        yield return 2;
        number /= 2;
      }

      while (number % 3 == 0)
      {
        yield return 3;
        number /= 3;
      }

      while (number % 5 == 0)
      {
        yield return 5;
        number /= 5;
      }

      long k = 7, k2 = k * k;
      var index = 0;

      while (k2 <= number)
      {
        if (number % k == 0)
        {
          yield return k;
          number /= k;
        }
        else
        {
          k += m_primeFactorWheelIncrements[index++];
          k2 = k * k;

          if (index >= m_primeFactorWheelIncrements.Length) index = 0;
        }
      }

      if (number > 1) yield return number;
    }

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Factorization"/>
    /// <seealso cref="https://stackoverflow.com/questions/5872962/prime-factors-in-c-sharp"/>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<uint> GetPrimeFactors(uint number)
    {
      if (number < 1) throw new System.ArgumentOutOfRangeException(nameof(number));

      while (number % 2 == 0)
      {
        yield return 2;
        number /= 2;
      }

      while (number % 3 == 0)
      {
        yield return 3;
        number /= 3;
      }

      while (number % 5 == 0)
      {
        yield return 5;
        number /= 5;
      }

      uint k = 7, k2 = k * k;
      var index = 0;

      while (k2 <= number)
      {
        if (number % k == 0)
        {
          yield return k;
          number /= k;
        }
        else
        {
          k += (uint)m_primeFactorWheelIncrements[index++];
          k2 = k * k;

          if (index >= m_primeFactorWheelIncrements.Length) index = 0;
        }
      }

      if (number > 1) yield return number;
    }
    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Factorization"/>
    /// <seealso cref="https://stackoverflow.com/questions/5872962/prime-factors-in-c-sharp"/>
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<ulong> GetPrimeFactors(ulong number)
    {
      if (number < 1) throw new System.ArgumentOutOfRangeException(nameof(number));

      while (number % 2 == 0)
      {
        yield return 2;
        number /= 2;
      }

      while (number % 3 == 0)
      {
        yield return 3;
        number /= 3;
      }

      while (number % 5 == 0)
      {
        yield return 5;
        number /= 5;
      }

      ulong k = 7, k2 = k * k;
      var index = 0;

      while (k2 <= number)
      {
        if (number % k == 0)
        {
          yield return k;
          number /= k;
        }
        else
        {
          k += (ulong)m_primeFactorWheelIncrements[index++];
          k2 = k * k;

          if (index >= m_primeFactorWheelIncrements.Length) index = 0;
        }
      }

      if (number > 1) yield return number;
    }
  }
}
