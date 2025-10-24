namespace Flux
{
  /// <summary>
  /// <para>Even though multiplicative functions, have its own space.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Divisor_function"/></para>
  /// </summary>
  public static class DivisorFunctions
  {
    extension<TInteger>(TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>σ0()</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// <para><see href="https://cp-algorithms.com/algebra/divisors.html"/></para>
      /// </summary>
      public TInteger CountDivisors()
      {
        var count = TInteger.One;

        for (var i = TInteger.CreateChecked(2); (i * i) <= n; i++)
        {
          if (TInteger.IsZero(n % i))
          {
            var e = TInteger.Zero;

            do
            {
              e++;

              n /= i;
            }
            while (TInteger.IsZero(n % i));

            count *= e + TInteger.One;
          }
        }

        if (n > TInteger.One)
          count <<= 1;

        return count;
      }

      public System.Collections.Generic.List<TInteger> GetDivisors()
      {
        var divisors = new System.Collections.Generic.List<TInteger>();
        GetDivisors(n, divisors);
        divisors.Sort();
        return divisors;
      }

      /// <summary>
      /// <para>Creates a new list of factors (a.k.a. divisors) for the specified number.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// </summary>
      /// <remarks>This implementaion does not order the result.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="proper"></param>
      /// <returns></returns>
      public void GetDivisors(System.Collections.Generic.ICollection<TInteger> divisors)
      {
        if (n > TInteger.Zero)
          for (var i = TInteger.One; (i * i) <= n; i++)
          {
            var (q, r) = TInteger.DivRem(n, i);

            if (TInteger.IsZero(r))
            {
              divisors.Add(i);

              if (q != i)
                divisors.Add(q);
            }
          }
      }

      /// <summary>Determines whether the <paramref name="number"/> is a deficient number.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Deficient_number"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
      public bool IsDeficientNumber()
        => SumDivisors(n).AliquotSum < n;

      /// <summary>Determines whether the <paramref name="number"/> is a perfect number.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Perfect_number"/>
      public bool IsPerfectNumber()
        => SumDivisors(n).AliquotSum == n;

      /// <summary>
      /// <para>σ1()</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// <para><see href="https://cp-algorithms.com/algebra/divisors.html"/></para>
      /// </summary>
      public (TInteger Sum, TInteger AliquotSum) SumDivisors()
      {
        var sum = TInteger.One;

        var aliquot = n;

        for (var i = TInteger.CreateChecked(2); i * i <= n; i++)
        {
          if (TInteger.IsZero(n % i))
          {
            var e = 0;

            do
            {
              e++;

              n /= i;
            }
            while (TInteger.IsZero(n % i));

            var add = TInteger.Zero;
            var pow = TInteger.One;

            do
            {
              add += pow;
              pow *= i;
            }
            while (e-- > 0);

            sum *= add;
          }
        }

        if (n > TInteger.One)
          sum *= TInteger.One + n;

        return (sum, sum - aliquot);
      }
    }
  }
}
