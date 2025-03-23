namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Creates a new list of factors (a.k.a. divisors) for the specified <paramref name="value"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
    /// </summary>
    /// <remarks>This implementaion does not order the result.</remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="proper"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<TNumber> Factors<TNumber>(this TNumber value, bool proper)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var list = new System.Collections.Generic.List<TNumber>();

      if (value > TNumber.Zero)
      {
        var sqrt = value.IntegerSqrt();

        for (var counter = TNumber.One; counter <= sqrt; counter++)
          if (TNumber.IsZero(value % counter))
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
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.List<TNumber> PrimeFactors<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (value <= TNumber.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      var list = new System.Collections.Generic.List<TNumber>();

      var two = TNumber.CreateChecked(2);
      var three = TNumber.CreateChecked(3);
      var four = TNumber.CreateChecked(4);
      var five = TNumber.CreateChecked(5);
      var six = TNumber.CreateChecked(6);
      var seven = TNumber.CreateChecked(7);

      var m_primeFactorWheelIncrements = new TNumber[] { four, two, four, two, four, six, two, six };

      while (TNumber.IsZero(value % two))
      {
        list.Add(two);
        value /= two;
      }

      while (TNumber.IsZero(value % three))
      {
        list.Add(three);
        value /= three;
      }

      while (TNumber.IsZero(value % five))
      {
        list.Add(five);
        value /= five;
      }

      TNumber k = seven, k2 = k * k;

      var index = 0;

      while (k2 <= value)
      {
        if (TNumber.IsZero(value % k))
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

      if (value > TNumber.One)
        list.Add(value);

      return list;
    }
  }
}
