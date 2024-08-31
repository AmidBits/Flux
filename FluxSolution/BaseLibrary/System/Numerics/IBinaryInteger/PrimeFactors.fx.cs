namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new list of factors (a.k.a. divisors) for the specified <paramref name="value"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
    /// </summary>
    /// <remarks>This implementaion does not order the result.</remarks>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="proper"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<TValue> Factors<TValue>(this TValue value, bool proper)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      var list = new System.Collections.Generic.List<TValue>();

      if (value > TValue.Zero)
      {
        var sqrt = value.IntegerSqrt();

        for (var counter = TValue.One; counter <= sqrt; counter++)
          if (TValue.IsZero(value % counter))
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
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.List<TValue> PrimeFactors<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      if (value <= TValue.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      var list = new System.Collections.Generic.List<TValue>();

      var two = TValue.CreateChecked(2);
      var three = TValue.CreateChecked(3);
      var four = TValue.CreateChecked(4);
      var five = TValue.CreateChecked(5);
      var six = TValue.CreateChecked(6);
      var seven = TValue.CreateChecked(7);

      var m_primeFactorWheelIncrements = new TValue[] { four, two, four, two, four, six, two, six };

      while (TValue.IsZero(value % two))
      {
        list.Add(two);
        value /= two;
      }

      while (TValue.IsZero(value % three))
      {
        list.Add(three);
        value /= three;
      }

      while (TValue.IsZero(value % five))
      {
        list.Add(five);
        value /= five;
      }

      TValue k = seven, k2 = k * k;

      var index = 0;

      while (k2 <= value)
      {
        if (TValue.IsZero(value % k))
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

      if (value > TValue.One)
        list.Add(value);

      return list;
    }
  }
}
