namespace Flux
{
  public static partial class RadixExtension
  {
    extension<TRadix>(TRadix radix)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      /// <summary>
      /// <para>Asserts that a <paramref name="radix"/> is a radix, i.e. greater than or equal to 2. Throws an exception if not.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Natural_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="radix"></param>
      /// <param name="paramName"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public TRadix AssertRadix(string? paramName = null)
        => IsRadix(radix) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), "Must be greater than or equal to 2.");

      /// <summary>
      /// <para>Asserts that a <paramref name="radix"/> is a radix, i.e. greater than or equal to 2. Throws an exception if not.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Natural_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="radix"></param>
      /// <param name="paramName"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public TRadix AssertRadix(TRadix upperLimit, string? paramName = null)
        => IsRadix(radix, upperLimit) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"Must be greater than or equal to 2 or less than or equal to {upperLimit}.");

      /// <summary>
      /// <para>Returns whether a <paramref name="radix"/> is valid, i.e. greater than or equal to 2 and less than or equal to 256.</para>
      /// <para>For a radix with custom upper limit use <see cref="IsRadix{TInteger}(TInteger, TInteger)"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="radix"></param>
      /// <returns></returns>
      public bool IsRadix()
        => radix > TRadix.One && radix <= TRadix.CreateChecked(byte.MaxValue);

      /// <summary>
      /// <para>Returns whether a <paramref name="radix"/> is valid, i.e. greater than or equal to 2 and less than or equal to a custom <paramref name="upperLimit"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="radix"></param>
      /// <returns></returns>
      public bool IsRadix(TRadix upperLimit)
        => radix > TRadix.One && radix <= upperLimit;
    }

    extension<TInteger>(TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>Converts an integer <paramref name="value"/> using base <paramref name="radix"/> to a decimal fraction, e.g. "123 => 0.123".</summary>
      public double ConvertToDecimalFraction<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var fdc = value.FastDigitCount(radix);

        var fip = radix.FastIntegerPow(fdc, out var _).IpowTz;

        return double.CreateChecked(value) / double.CreateChecked(fip);
      }

      /// <summary>Gets the count of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public TInteger DigitCount<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

        var count = TInteger.Zero;

        while (!TInteger.IsZero(value))
        {
          count++;

          value /= rdx;
        }

        return count;
      }

      /// <summary>Returns the sum of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Digit_sum"/>
      public TInteger DigitSum<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

        var sum = TInteger.Zero;

        while (!TInteger.IsZero(value))
        {
          sum += value % rdx;

          value /= rdx;
        }

        return sum;
      }

      /// <summary>Drop the trailing (least significant) digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public TInteger DropLeastSignificantDigit<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value / TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public TInteger DropLeastSignificantDigits<TRadix>(TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value / TInteger.CreateChecked(Units.Radix.AssertMember(radix).FastIntegerPow(count, out var _).IpowTz);

      /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public TInteger DropMostSignificantDigits<TRadix>(TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value % TInteger.CreateChecked(radix.FastIntegerPow(DigitCount(value, radix) - count, out var _).IpowTz);

      /// <summary>Returns the digit place value components of <paramref name="value"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</summary>
      public System.Collections.Generic.List<TInteger> GetDigitPlaceValues<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var list = GetDigitsReversed(value, radix); // Already asserts radix.

        var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

        var power = TInteger.One;

        for (int index = 0; index < list.Count; index++)
        {
          list[index] *= power;

          power *= rdx;
        }

        return list;
      }

      /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TInteger"/>) of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public System.Collections.Generic.List<TInteger> GetDigits<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

        if (TInteger.IsNegative(value))
          value = TInteger.Abs(value);

        var list = new System.Collections.Generic.List<TInteger>();

        if (TInteger.IsZero(value))
          list.Add(TInteger.Zero);
        else
          while (!TInteger.IsZero(value))
          {
            list.Insert(0, value % rdx);

            value /= rdx;
          }

        return list;
      }

      /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TInteger"/>) of <paramref name="value"/> using base <paramref name="radix"/>, in reverse order.</summary>
      public System.Collections.Generic.List<TInteger> GetDigitsReversed<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

        if (TInteger.IsNegative(value))
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

      //public bool IsBalanced<TRadix>(TRadix radix)
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //{
      //  var digits = GetDigits(value, radix);

      //  var ceilingHalf = digits.Count.DivRemEnveloped(2).QuotientEnveloped;

      //  var left = digits[..ceilingHalf].Sum();
      //  var right = digits[^ceilingHalf..].Sum();

      //  var rgt = value.SumLeastSignificantDigits(radix, TInteger.CreateChecked(ceilingHalf));

      //  return left == right;
      //}

      /// <summary>Indicates whether <paramref name="value"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
      /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
      public bool IsJumbled<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

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

      /// <summary>Returns whether <paramref name="value"/> using base <paramref name="radix"/> is a self value. A self value, Colombian value or Devlali value, in a given value base (radix) is a natural value that cannot be written as the sum of any other natural value n and the individual digits of n.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Self_number"/>
      public bool IsSelfNumber<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        for (var sn = SelfNumberLowBound(value, radix); sn < value; sn++)
          if (DigitSum(sn, radix) + sn == value)
            return false;

        return true;
      }

      /// <summary>Indicates whether the <paramref name="value"/> is single digit using the base <paramref name="radix"/>, i.e. in the interval (-<paramref name="radix"/>, +<paramref name="radix"/>).</summary>
      public bool IsSingleDigit<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => TInteger.Abs(value) < TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public TInteger KeepLeastSignificantDigit<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value % TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public TInteger KeepLeastSignificantDigits<TRadix>(TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value % TInteger.CreateChecked(Units.Radix.AssertMember(radix).FastIntegerPow(count, out var _).IpowTz);

      /// <summary>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public TInteger KeepMostSignificantDigits<TRadix>(TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value / TInteger.CreateChecked(radix.FastIntegerPow(value.DigitCount(radix) - count, out var _).IpowTz);

      /// <summary>
      /// <para>Gets the count of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public (TInteger DigitCount, TInteger DigitSum, bool IsJumbled, bool IsPowOf, TInteger NumberReversed, System.Collections.Generic.List<TInteger> PlaceValues, System.Collections.Generic.List<TInteger> ReverseDigits) MagicalDigits<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

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

      /// <summary>Reverse the digits of the <paramref name="x"/> using base <paramref name="radix"/>, obtaining a new number.</summary>
      public TInteger ReverseDigits<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

        var reversed = TInteger.Zero;

        while (!TInteger.IsZero(value))
        {
          reversed = (reversed * rdx) + (value % rdx);

          value /= rdx;
        }

        return reversed;
      }

      /// <summary>Returns the minimum possible value that can make <paramref name="value"/> a self value using base <paramref name="radix"/>.</summary>
      public TInteger SelfNumberLowBound<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);

        var logRadix = TInteger.CreateChecked(value.IntegerLogTowardZero(radix));
        var maxDistinct = (TInteger.CreateChecked(9) * logRadix) + (value / TInteger.CreateChecked(radix.IntegerPow(logRadix)));

        return TInteger.Max(value - maxDistinct, TInteger.Zero);
      }

      //public TInteger SumLeastSignificantDigits<TRadix>(TRadix radix, TInteger count)
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //{
      //  var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      //  var sum = TInteger.Zero;

      //  for (var i = count - TInteger.One; i >= TInteger.Zero; i--)
      //  {
      //    sum += value % rdx;
      //    value /= rdx;
      //  }

      //  return sum;
      //}

      //public TInteger SumMostSignificantDigits<TRadix>(TRadix radix, TInteger count)
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //{
      //}
    }
  }
}
