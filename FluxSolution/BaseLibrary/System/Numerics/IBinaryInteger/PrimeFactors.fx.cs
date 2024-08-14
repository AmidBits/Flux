namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new list of factors (a.k.a. divisors) for the specified <paramref name="number"/>.</summary>
    /// <remarks>This implementaion does not order the result.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Divisor"/>
    public static System.Collections.Generic.List<TSelf> Factors<TSelf>(this TSelf number, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var list = new System.Collections.Generic.List<TSelf>();

      if (number > TSelf.Zero)
      {
        var sqrt = number.IntegerSqrt();

        for (var counter = TSelf.One; counter <= sqrt; counter++)
          if (TSelf.IsZero(number % counter))
          {
            list.Add(counter);

            if (number / counter is var quotient && quotient != counter)
              list.Add(quotient);
          }
      }

      if (proper) list.Remove(number);

      return list;
    }

    /// <summary>
    /// <para>Creates a list of prime factors for the <paramref name="number"/> using wheel factorization.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Factorization"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Wheel_factorization"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Integer_factorization"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Divisor"/></para>
    /// </summary>
    public static System.Collections.Generic.List<TSelf> PrimeFactors<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (number <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(number));

      var list = new System.Collections.Generic.List<TSelf>();

      var two = TSelf.CreateChecked(2);
      var three = TSelf.CreateChecked(3);
      var four = TSelf.CreateChecked(4);
      var five = TSelf.CreateChecked(5);
      var six = TSelf.CreateChecked(6);
      var seven = TSelf.CreateChecked(7);

      var m_primeFactorWheelIncrements = new TSelf[] { four, two, four, two, four, six, two, six };

      while (TSelf.IsZero(number % two))
      {
        list.Add(two);
        number /= two;
      }

      while (TSelf.IsZero(number % three))
      {
        list.Add(three);
        number /= three;
      }

      while (TSelf.IsZero(number % five))
      {
        list.Add(five);
        number /= five;
      }

      TSelf k = seven, k2 = k * k;

      var index = 0;

      while (k2 <= number)
      {
        if (TSelf.IsZero(number % k))
        {
          list.Add(k);
          number /= k;
        }
        else
        {
          k += m_primeFactorWheelIncrements[index++];
          k2 = k * k;

          if (index >= m_primeFactorWheelIncrements.Length)
            index = 0;
        }
      }

      if (number > TSelf.One)
        list.Add(number);

      return list;
    }

    //public static TSelf CountOfPrimeFactors<TSelf>(this TSelf number)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //{
    //  if (number <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(number));

    //  var count = TSelf.Zero;

    //  var two = TSelf.CreateChecked(2);
    //  var three = TSelf.CreateChecked(3);
    //  var four = TSelf.CreateChecked(4);
    //  var five = TSelf.CreateChecked(5);
    //  var six = TSelf.CreateChecked(6);
    //  var seven = TSelf.CreateChecked(7);

    //  var m_primeFactorWheelIncrements = new TSelf[] { four, two, four, two, four, six, two, six };

    //  while (TSelf.IsZero(number % two))
    //  {
    //    count++;
    //    number /= two;
    //  }

    //  while (TSelf.IsZero(number % three))
    //  {
    //    count++;
    //    number /= three;
    //  }

    //  while (TSelf.IsZero(number % five))
    //  {
    //    count++;
    //    number /= five;
    //  }

    //  TSelf k = seven, k2 = k * k;

    //  var index = 0;

    //  while (k2 <= number)
    //  {
    //    if (TSelf.IsZero(number % k))
    //    {
    //      count++;
    //      number /= k;
    //    }
    //    else
    //    {
    //      k += m_primeFactorWheelIncrements[index++];
    //      k2 = k * k;

    //      if (index >= m_primeFactorWheelIncrements.Length)
    //        index = 0;
    //    }
    //  }

    //  if (number > TSelf.One)
    //    count++;

    //  return count;
    //}

    //public static TSelf SumOfPrimeFactors<TSelf>(this TSelf number)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //{
    //  if (number <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(number));

    //  var sum = TSelf.Zero;

    //  var two = TSelf.CreateChecked(2);
    //  var three = TSelf.CreateChecked(3);
    //  var four = TSelf.CreateChecked(4);
    //  var five = TSelf.CreateChecked(5);
    //  var six = TSelf.CreateChecked(6);
    //  var seven = TSelf.CreateChecked(7);

    //  var m_primeFactorWheelIncrements = new TSelf[] { four, two, four, two, four, six, two, six };

    //  while (TSelf.IsZero(number % two))
    //  {
    //    sum += two;
    //    number /= two;
    //  }

    //  while (TSelf.IsZero(number % three))
    //  {
    //    sum += three;
    //    number /= three;
    //  }

    //  while (TSelf.IsZero(number % five))
    //  {
    //    sum += five;
    //    number /= five;
    //  }

    //  TSelf k = seven, k2 = k * k;

    //  var index = 0;

    //  while (k2 <= number)
    //  {
    //    if (TSelf.IsZero(number % k))
    //    {
    //      sum += k;
    //      number /= k;
    //    }
    //    else
    //    {
    //      k += m_primeFactorWheelIncrements[index++];
    //      k2 = k * k;

    //      if (index >= m_primeFactorWheelIncrements.Length)
    //        index = 0;
    //    }
    //  }

    //  if (number > TSelf.One)
    //    sum += number;

    //  return sum;
    //}
  }
}
