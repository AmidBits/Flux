namespace Flux
{
  /// <para><see href="https://en.wikipedia.org/wiki/Prime_omega_function"/></para>
  public static class PrimeOmegaFunctions
  {
    extension<TInteger>(TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>The number of prime factors that make up a number.</para>
      /// </summary>
      /// <returns></returns>
      public (int TotalCount, int DistinctCount) CountPrimeFactors()
      {
        var pf = GetPrimeFactors(n);

        return (pf.Count, pf.Distinct().Count());
      }

      public System.Collections.Generic.List<TInteger> GetPrimeFactors()
      {
        var primeFactors = new System.Collections.Generic.List<TInteger>();
        GetPrimeFactors(n, primeFactors);
        return (primeFactors);
      }

      /// <summary>
      /// <para>Creates a list of prime factors (a.k.a. divisors) for the <paramref name="value"/> using wheel factorization.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Factorization"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Wheel_factorization"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Integer_factorization"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public void GetPrimeFactors(System.Collections.Generic.ICollection<TInteger> primeFactors)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        var two = TInteger.CreateChecked(2);
        var four = TInteger.CreateChecked(4);
        var six = TInteger.CreateChecked(6);

        var m_primeFactorWheelIncrements = new TInteger[] { four, two, four, two, four, six, two, six };

        while (TInteger.IsZero(n % two))
        {
          primeFactors.Add(two);
          n /= two;
        }

        var three = TInteger.CreateChecked(3);

        while (TInteger.IsZero(n % three))
        {
          primeFactors.Add(three);
          n /= three;
        }

        var five = TInteger.CreateChecked(5);

        while (TInteger.IsZero(n % five))
        {
          primeFactors.Add(five);
          n /= five;
        }

        TInteger k = TInteger.CreateChecked(7), k2 = k * k;

        var index = 0;

        while (k2 <= n)
        {
          if (TInteger.IsZero(n % k))
          {
            primeFactors.Add(k);
            n /= k;
          }
          else
          {
            k += m_primeFactorWheelIncrements[index++];
            k2 = k * k;

            if (index >= m_primeFactorWheelIncrements.Length)
              index = 0;
          }
        }

        if (n > TInteger.One)
          primeFactors.Add(n);
      }
    }

    public static string ToPrimeFactorString<TInteger>(this System.Collections.Generic.List<TInteger> primeFactors)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => string.Join('\u00d7', primeFactors.GroupAdjacent(pf => pf).Select(g => $"{g.Key}{g.Count().ToSuperscriptString(10).ToString()}"));
  }
}
