namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Creates a new list of factors (a.k.a. divisors) for the specified <paramref name="value"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
    /// </summary>
    /// <remarks>This implementaion does not order the result.</remarks>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="proper"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<TInteger> Factors<TInteger>(this TInteger value, bool proper)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var list = new System.Collections.Generic.List<TInteger>();

      if (value > TInteger.Zero)
      {
        var sqrt = value.IntegerSqrt();

        for (var counter = TInteger.One; counter <= sqrt; counter++)
          if (TInteger.IsZero(value % counter))
          {
            list.Add(counter);

            if (value / counter is var quotient && quotient != counter)
              list.Add(quotient);
          }
      }

      if (proper) list.Remove(value);

      return list;
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
    public static System.Collections.Generic.List<TInteger> PrimeFactors<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (value <= TInteger.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      var list = new System.Collections.Generic.List<TInteger>();

      var two = TInteger.CreateChecked(2);
      var three = TInteger.CreateChecked(3);
      var four = TInteger.CreateChecked(4);
      var five = TInteger.CreateChecked(5);
      var six = TInteger.CreateChecked(6);
      var seven = TInteger.CreateChecked(7);

      var m_primeFactorWheelIncrements = new TInteger[] { four, two, four, two, four, six, two, six };

      while (TInteger.IsZero(value % two))
      {
        list.Add(two);
        value /= two;
      }

      while (TInteger.IsZero(value % three))
      {
        list.Add(three);
        value /= three;
      }

      while (TInteger.IsZero(value % five))
      {
        list.Add(five);
        value /= five;
      }

      TInteger k = seven, k2 = k * k;

      var index = 0;

      while (k2 <= value)
      {
        if (TInteger.IsZero(value % k))
        {
          list.Add(k);
          value /= k;
        }
        else
        {
          k += m_primeFactorWheelIncrements[index++];
          k2 = k * k;

          if (index >= m_primeFactorWheelIncrements.Length)
            index = 0;
        }
      }

      if (value > TInteger.One)
        list.Add(value);

      return list;
    }
  }
}
