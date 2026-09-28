namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      public static void AssertRadix<TRadix>(TRadix radix, out TInteger rdx)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(radix, TRadix.CreateChecked(2));

        rdx = TInteger.CreateChecked(radix);
      }

      #region ConvertToFractionalPart

      /// <summary>
      /// <para>Converts an integer value to a decimal fraction, e.g. "123 => 0.123".</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static decimal ConvertToFractionalPart<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var digitCount = DigitCount(value, radix); // Digit count of the "integer part", e.g. an integer 123 = 3 digits.

        var fractionalPart = Pow(radix, digitCount); // With the digit count we can create a power-of-radix of the same magnitude as the digit count, e.g. 3 digits = 1000 (radix = 10).

        return decimal.CreateChecked(value) / decimal.CreateChecked(fractionalPart); // E.g. 123. / 1000 = .123 
      }

      #endregion

      #region DigitCount

      /// <summary>
      /// <para>Gets the count of all digits in a number using the specified <paramref name="radix"/>.</para>
      /// </summary>
      /// <remarks>DigitCount is log-floor + 1.</remarks>
      public static TInteger DigitCount<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (IsSingleDigit(value, radix))
          return TInteger.One;

        return Log(value, radix).LogAwayFromZero;
      }

      //{
      //  var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      //  var count = TInteger.Zero;

      //  while (!TInteger.IsZero(value))
      //  {
      //    count++;

      //    value /= rdx;
      //  }

      //  return count;
      //}

      public static TInteger DigitCount2(TInteger n, TInteger b)
      {
        n = TInteger.Abs(n);

        TInteger count = TInteger.One;

        while (n >= b)
        {
          n /= b;

          count++;
        }

        return count;
      }

      #endregion

      #region DigitPlaceValues

      /// <summary>
      /// <para>Creates a new list with the digit place value components of <paramref name="value"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static List<TInteger> DigitPlaceValues(TInteger n, TInteger b)
      {
        var result = new List<TInteger>();

        var place = TInteger.One;
        while (n > TInteger.Zero)
        {
          var digit = n % b;
          result.Add(digit * place);

          n /= b;
          place *= b;
        }

        return result;
      }

      #endregion

      #region DigitProduct

      public static TInteger DigitProduct<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        value = TInteger.Abs(value);

        if (value < rdx)
          return value;

        var product = TInteger.One;

        while (!TInteger.IsZero(value))
        {
          var digit = value % rdx;

          if (TInteger.IsZero(digit))
            return digit;

          product *= digit;

          value /= rdx;
        }

        return product;
      }

      #endregion

      #region DigitSum

      /// <summary>
      /// <para>Returns the sum of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Digit_sum"/></para>
      /// </summary>
      public static TInteger DigitSum<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        var sum = TInteger.Zero;

        while (!TInteger.IsZero(value))
        {
          sum += value % rdx;

          value /= rdx;
        }

        return sum;
      }

      #endregion

      #region DropLeastSignificantDigits

      /// <summary>
      /// <para>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public static TInteger DropLeastSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger _);

        return value / TInteger.CreateChecked(Pow(radix, count));
      }

      #endregion

      #region DropMostSignificantDigits

      /// <summary>
      /// <para>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public static TInteger DropMostSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value % TInteger.CreateChecked(Pow(radix, DigitCount(value, radix) - count)); // DigitCount() already checks lower radix bound.

      #endregion

      #region GetDigits

      /// <summary>
      /// <para>Creates a new list of digits representing the <paramref name="value"/> in base <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<TInteger> GetDigits<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var list = GetDigitsReversed(value, radix); // GetDigitsReversed() already check the lower radix bound.
        list.Reverse();
        return list;
      }

      #endregion

      #region GetDigitsReversed

      /// <summary>
      /// <para>Creates a new list of digits, in reverse order, representing the <paramref name="value"/> in base <paramref name="radix"/>.</para>
      /// </summary>
      public static System.Collections.Generic.List<TInteger> GetDigitsReversed<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        value = TInteger.Abs(value);

        var list = new System.Collections.Generic.List<TInteger>();

        if (TInteger.IsZero(value))
          list.Add(TInteger.Zero);
        else
          while (!TInteger.IsZero(value))
          {
            list.Add(value % rdx);

            value /= rdx;
          }

        return list;
      }

      #endregion

      #region GetOrdinalIndicatorSuffix

      /// <summary>
      /// <para>Gets the ordinal indicator suffix for <paramref name="value"/>. E.g. "st" for 1 and "nd" for 122.</para>
      /// </summary>
      /// <remarks>The suffixes "st", "nd" and "rd" are consistent for all numbers ending in 1, 2 and 3, resp., except numbers ending with 11, 12 and 13, which instead uses the suffix "th".</remarks>
      public static string GetOrdinalIndicatorSuffix(TInteger value)
      {
        var hundreds = int.CreateChecked(TInteger.Abs(value) % TInteger.CreateChecked(100)); // Trim the value (to 2 digits) before making it fit in an int (since the value could be larger).

        var (tens, ones) = int.DivRem(hundreds, 10); // ones only needs "% 10", but tens need "/ 10"..

        tens %= 10; // ..and also a "% 10".

        if (tens != 1) // If tens = 1 then variations are possible, if tens != 1 there are no variations.
          switch (ones)
          {
            case 1: return "st";
            case 2: return "nd";
            case 3: return "rd";
          }

        return "th";
      }

      #endregion

      #region IsBalanced

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static bool IsBalanced<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var digits = GetDigits(value, radix); // Already checks lower radix bound.

        var ceilingHalf = int.IntegerDivRemEnveloped(digits.Count, 2).Quotient;

        var left = digits[..ceilingHalf].Sum();
        var right = digits[^ceilingHalf..].Sum();

        //var rgt = SumLeastSignificantDigits(value, radix, TInteger.CreateChecked(ceilingHalf));

        return left == right;
      }

      #endregion

      #region IsJumbled

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</para>
      /// <para><see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/></para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static bool IsJumbled<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        while (!TInteger.IsZero(value))
        {
          var remainder = value % rdx;

          value /= rdx;

          if (TInteger.IsZero(value))
            break;
          else if (TInteger.Abs((value % rdx) - remainder) > TInteger.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
            return false;
        }

        return true;
      }

      #endregion

      #region IsSelfNumber

      /// <summary>
      /// <para>A self number in a given number base b is a natural number that cannot be written as the sum of any other natural number n and the individual digits of number n.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="number"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static bool IsSelfNumber<TRadix>(TInteger number, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger _);

        for (var n = number - TInteger.One; n > TInteger.Zero; n--)
          if (number == n + DigitSum(n, radix))
            return false;

        return true;
      }

      #endregion

      #region IsSingleDigit

      /// <summary>
      /// <para>Indicates whether the <paramref name="value"/> is single digit using the base <paramref name="radix"/>, i.e. in the interval [2, <paramref name="radix"/>).</para>
      /// </summary>
      public static bool IsSingleDigit<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        return TInteger.Abs(value) < rdx;
      }

      #endregion

      #region KeepLeastSignificantDigits

      /// <summary>
      /// <para>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public static TInteger KeepLeastSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger _);

        return value % TInteger.CreateChecked(Pow(radix, count));
      }

      #endregion

      #region KeepMostSignificantDigits

      /// <summary>
      /// <para>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public static TInteger KeepMostSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger _);

        return value / TInteger.CreateChecked(Pow(radix, DigitCount(value, radix) - count));
      }

      #endregion

      #region ProcessDigits

      /// <summary>
      /// <para>Gets the count, the sum, whether it is jumbled, is a power of, the number reversed, the place values, and the reverse digits, of <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public static (TInteger DigitCount, TInteger DigitSum, bool IsJumbled, bool IsPowOf, TInteger NumberReversed, System.Collections.Generic.List<TInteger> PlaceValues, System.Collections.Generic.List<TInteger> ReverseDigits) ProcessDigits<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        var count = TInteger.Zero;
        var isJumbled = true;
        var numberReversed = TInteger.Zero;
        var placeValues = new System.Collections.Generic.List<TInteger>();
        var reverseDigits = new System.Collections.Generic.List<TInteger>();
        var sum = TInteger.Zero;

        var power = TInteger.One;

        while (!TInteger.IsZero(value))
        {
          var rem = value % rdx;

          count++;
          numberReversed = (numberReversed * rdx) + rem;
          placeValues.Add(rem * power);
          reverseDigits.Add(rem);
          sum += rem;

          power *= rdx;

          value /= rdx;

          if (isJumbled && (TInteger.Abs((value % rdx) - rem) > TInteger.One))
            isJumbled = false;
        }

        if (TInteger.IsZero(count))
        {
          placeValues.Add(count);
          reverseDigits.Add(count);
        }

        return (count, sum, isJumbled, sum == TInteger.One, numberReversed, placeValues, reverseDigits);
      }

      #endregion

      #region ReverseDigits

      /// <summary>
      /// <para>Reverse the digits a <paramref name="value"/> in base <paramref name="radix"/>, obtaining a new number.</para>
      /// </summary>
      public static TInteger ReverseDigits<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        var reversed = TInteger.Zero;

        while (!TInteger.IsZero(value))
        {
          reversed = (reversed * rdx) + (value % rdx);

          value /= rdx;
        }

        return reversed;
      }

      #endregion

      #region RotateDigits

      /// <summary>
      /// 
      /// </summary>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <param name="radix"></param>
      /// <param name="left"></param>
      /// <returns></returns>
      public static TInteger RotateDigits<TRadix>(TInteger n, TInteger k, TRadix radix, bool left = true)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        if (n < rdx) // If n is a single digit in the given radix, rotation has no effect.
          return n;

        var digitCount = TInteger.Zero;
        var t = n;
        while (t > TInteger.Zero)
        {
          t /= rdx;

          digitCount++;
        }

        k %= digitCount;

        if (TInteger.IsZero(k))
          return n;

        if (!left)
          k = digitCount - k; // Reverse rotation.

        var powK = TInteger.One; // Compute b^k and b^(digits-k)
        for (var i = TInteger.Zero; i < k; i++)
          powK *= rdx;

        var powRest = TInteger.One;
        for (var i = TInteger.Zero; i < digitCount - k; i++)
          powRest *= rdx;

        var hiSplit = n / powRest; // first k digits
        var loSplit = n % powRest; // remaining digits

        return loSplit * powK + hiSplit; // Recombine.
      }

      #endregion

      #region SumLeastSignificantDigits

      /// <summary>
      /// <para>Sum <paramref name="count"/> least significant digits of <paramref name="value"/> in the given <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public static TInteger SumLeastSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        value = TInteger.Abs(value);

        var sum = TInteger.Zero;

        for (var i = TInteger.Zero; i < count && value > TInteger.Zero; i++)
        {
          sum += value % rdx;

          value /= rdx;
        }

        return sum;
      }

      #endregion

      #region SumMostSignificantDigits

      /// <summary>
      /// <para>Sum <paramref name="count"/> most significant digits of <paramref name="value"/> in the given <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public static TInteger SumMostSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        value = TInteger.Abs(value);
        var digits = DigitCount(value, radix);
        value /= Pow(rdx, digits - TInteger.Min(count, digits));

        var sum = TInteger.Zero;

        while (value > TInteger.Zero)
        {
          sum += value % rdx;

          value /= rdx;
        }

        return sum;
      }

      #endregion
    }
  }
}

// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>
