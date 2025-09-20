//namespace Flux
//{
//  public static partial class IntegerMath
//  {
//    extension<TInteger>(TInteger value)
//    {
//      /// <summary>
//      /// <para>Creates a new list of factors (a.k.a. divisors) for the specified <paramref name="value"/>.</para>
//      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
//      /// </summary>
//      /// <remarks>This implementaion does not order the result.</remarks>
//      /// <typeparam name="TInteger"></typeparam>
//      /// <param name="value"></param>
//      /// <param name="proper"></param>
//      /// <returns></returns>
//      public System.Collections.Generic.List<TInteger> Factors(bool proper)
//      {
//        var list = new System.Collections.Generic.List<TInteger>();

//        if (value > TInteger.Zero)
//        {
//          var sqrt = value.IntegerSqrt();

//          for (var counter = TInteger.One; counter <= sqrt; counter++)
//            if (TInteger.IsZero(value % counter))
//            {
//              list.Add(counter);

//              if (value / counter is var quotient && quotient != counter)
//                list.Add(quotient);
//            }
//        }

//        if (proper) list.Remove(value);

//        return list;
//      }

//      /// <summary>
//      /// <para>Creates a list of prime factors (a.k.a. divisors) for the <paramref name="value"/> using wheel factorization.</para>
//      /// <para><see href="https://en.wikipedia.org/wiki/Factorization"/></para>
//      /// <para><see href="https://en.wikipedia.org/wiki/Wheel_factorization"/></para>
//      /// <para><see href="https://en.wikipedia.org/wiki/Integer_factorization"/></para>
//      /// <para><seealso href="https://en.wikipedia.org/wiki/Divisor"/></para>
//      /// </summary>
//      /// <typeparam name="TInteger"></typeparam>
//      /// <param name="value"></param>
//      /// <returns></returns>
//      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
//      public System.Collections.Generic.List<TInteger> PrimeFactors()
//      {
//        if (value <= TInteger.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

//        var list = new System.Collections.Generic.List<TInteger>();

//        var two = TInteger.CreateChecked(2);
//        var four = TInteger.CreateChecked(4);
//        var six = TInteger.CreateChecked(6);

//        var m_primeFactorWheelIncrements = new TInteger[] { four, two, four, two, four, six, two, six };

//        while (TInteger.IsZero(value % two))
//        {
//          list.Add(two);
//          value /= two;
//        }

//        var three = TInteger.CreateChecked(3);

//        while (TInteger.IsZero(value % three))
//        {
//          list.Add(three);
//          value /= three;
//        }

//        var five = TInteger.CreateChecked(5);

//        while (TInteger.IsZero(value % five))
//        {
//          list.Add(five);
//          value /= five;
//        }

//        TInteger k = TInteger.CreateChecked(7), k2 = k * k;

//        var index = 0;

//        while (k2 <= value)
//        {
//          if (TInteger.IsZero(value % k))
//          {
//            list.Add(k);
//            value /= k;
//          }
//          else
//          {
//            k += m_primeFactorWheelIncrements[index++];
//            k2 = k * k;

//            if (index >= m_primeFactorWheelIncrements.Length)
//              index = 0;
//          }
//        }

//        if (value > TInteger.One)
//          list.Add(value);

//        return list;
//      }

//    }
//    ///// <summary>
//    ///// <para>Asserts that a <paramref name="value"/> is a non-negative integer, i.e. greater than or equal to zero. Throws an exception if not.</para>
//    ///// <para><see href="https://en.wikipedia.org/wiki/Natural_number"/></para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="value"></param>
//    ///// <param name="paramName"></param>
//    ///// <returns></returns>
//    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
//    //public static TInteger AssertNonNegativeInteger<TInteger>(this TInteger value, string? paramName = null)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //  => IsNonNegativeInteger(value) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "Must be greater than or equal to zero.");

//    ///// <summary>
//    ///// <para>Returns whether a <paramref name="value"/> is a non-negative integer, i.e. greater than or equal to zero.</para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="value"></param>
//    ///// <returns></returns>
//    //public static bool IsNonNegativeInteger<TInteger>(this TInteger value)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //  => value >= TInteger.Zero;

//    ///// <summary>
//    ///// <para>Asserts that a <paramref name="value"/> is a non-positive integer, i.e. less than or equal to zero. Throws an exception if not.</para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="value"></param>
//    ///// <param name="paramName"></param>
//    ///// <returns></returns>
//    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
//    //public static TInteger AssertNonPositiveInteger<TInteger>(this TInteger value, string? paramName = null)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //  => IsNonPositiveInteger(value) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be less than or equal to zero.");

//    ///// <summary>
//    ///// <para>Returns whether a <paramref name="value"/> is a non-positive integer, i.e. less than or equal to zero.</para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="value"></param>
//    ///// <returns></returns>
//    //public static bool IsNonPositiveInteger<TInteger>(this TInteger value)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //  => value <= TInteger.Zero;

//    ///// <summary>
//    ///// <para>Asserts that a <paramref name="value"/> is a positive integer, i.e. greater than zero. Throws an exception if not.</para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="value"></param>
//    ///// <param name="paramName"></param>
//    ///// <returns></returns>
//    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
//    //public static TInteger AssertPositiveInteger<TInteger>(this TInteger value, string? paramName = null)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //  => IsPositiveInteger(value) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), "Must be greater than zero.");

//    ///// <summary>
//    ///// <para>Returns whether a <paramref name="value"/> is a positive integer, i.e. greater than zero.</para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="value"></param>
//    ///// <returns></returns>
//    //public static bool IsPositiveInteger<TInteger>(this TInteger value)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //  => value > TInteger.Zero;
//  }
//}
