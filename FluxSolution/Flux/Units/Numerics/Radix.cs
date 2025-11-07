namespace Flux.Units
{
  /// <summary>
  /// <para>Radix is in the range of the closed interval [<see cref="Radix.MinRadix"/> = 2, <see cref="int.MaxValue"/>].</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Radix"/></para>
  /// </summary>
  public readonly record struct Radix
    : System.IComparable, System.IComparable<Radix>, System.IFormattable, IValueQuantifiable<int>
  {
    public static Radix Binary { get; } = new(2);
    public static Radix Octal { get; } = new(8);
    public static Radix Decimal { get; } = new(10);
    public static Radix Hexadecimal { get; } = new(16);

    public const string Base64 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";
    //public const string Base62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public const int MinValue = 2;

    private readonly int m_value;

    public Radix(int radix) => m_value = AssertMember(radix, IntervalNotation.Closed, nameof(radix));

    #region Static methods

    /// <summary>
    /// <para>Asserts that a <paramref name="radix"/> is a member of a radix interval, i.e. in the range <see cref="MinValue"/>..<paramref name="maxRadix"/>, with the specified <paramref name="intervalNotation"/>.</para>
    /// <para>Throws an exception if <paramref name="radix"/> is not a member.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TInteger AssertMember<TInteger>(TInteger radix, TInteger maxRadix, IntervalNotation intervalNotation = IntervalNotation.Closed, string? paramName = null)
      where TInteger : System.Numerics.INumber<TInteger>
      => intervalNotation.IsMember(radix, TInteger.CreateChecked(MinValue), maxRadix)
      ? radix
      : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"The radix ({radix}) is out of range: {intervalNotation.ToIntervalNotationString(TInteger.CreateChecked(MinValue), maxRadix)}.");

    /// <summary>
    /// <para>Asserts that a <paramref name="radix"/> is a member of a radix interval, i.e. in the range <see cref="MinValue"/>..&#x221E;, with the specified <paramref name="intervalNotation"/>.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TInteger AssertMember<TInteger>(TInteger radix, IntervalNotation intervalNotation = IntervalNotation.Closed, string? paramName = null)
      where TInteger : System.Numerics.INumber<TInteger>
      => AssertMember(radix, radix + TInteger.One, intervalNotation, paramName);

    /// <summary>
    /// <para>Gets the count, the sum, whether it is jumbled, is a power of, the number reversed, the place values, and the reverse digits, of <paramref name="value"/> using base <paramref name="radix"/>.</para>
    /// </summary>
    public static (TInteger DigitCount, TInteger DigitSum, bool IsJumbled, bool IsPowOf, TInteger NumberReversed, System.Collections.Generic.List<TInteger> PlaceValues, System.Collections.Generic.List<TInteger> ReverseDigits) Digits<TInteger, TRadix>(TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
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

    /// <summary>
    /// <para>Gets the count of all digits in a number using the specified <paramref name="radix"/>.</para>
    /// </summary>
    /// <remarks>DigitCount is log-floor + 1.</remarks>
    public static TInteger DigitCount<TInteger, TRadix>(TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value.IntegerLog(radix).TowardZero + TInteger.One;
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

    /// <summary>
    /// <para>Returns the sum of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Digit_sum"/></para>
    /// </summary>
    public static TInteger DigitSum<TInteger, TRadix>(TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
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

    /// <summary>
    /// <para>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="value"/> using base <paramref name="radix"/>.</para>
    /// </summary>
    public static TInteger DropLeastSignificantDigits<TInteger, TRadix>(TInteger value, TRadix radix, TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TInteger.CreateChecked(Units.Radix.AssertMember(radix).IntegerPow(count));

    /// <summary>
    /// <para>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="value"/> using base <paramref name="radix"/>.</para>
    /// </summary>
    public static TInteger DropMostSignificantDigits<TInteger, TRadix>(TInteger value, TRadix radix, TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TInteger.CreateChecked(radix.IntegerPow(DigitCount(value, radix) - count));

    /// <summary>
    /// <para>Creates a new list with the digit place value components of <paramref name="value"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<TInteger> GetDigitPlaceValues<TInteger, TRadix>(TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
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

    /// <summary>
    /// <para>Creates a new list digits (in reverse order) representing the <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<TInteger> GetDigits<TInteger, TRadix>(TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
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

    /// <summary>
    /// <para>Creates a new list digits (in reverse order) representing the <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    public static System.Collections.Generic.List<TInteger> GetDigitsReversed<TInteger, TRadix>(TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
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

    public bool IsBalanced<TInteger, TRadix>(TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var digits = GetDigits(value, radix);

      var ceilingHalf = digits.Count.EnvelopedDivRem(2).Quotient;

      var left = digits[..ceilingHalf].Sum();
      var right = digits[^ceilingHalf..].Sum();

      var rgt = SumLeastSignificantDigits(value, radix, TInteger.CreateChecked(ceilingHalf));

      return left == right;
    }

    /// <summary>
    /// <para>Indicates whether <paramref name="value"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</para>
    /// <para><see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/></para>
    /// </summary>
    public static bool IsJumbled<TInteger, TRadix>(TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
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

    /// <summary>
    /// <para>Indicates whether a <paramref name="radix"/> is a member of a radix interval, i.e. in the range <see cref="MinValue"/>..<paramref name="maxRadix"/>, with the specified <paramref name="intervalNotation"/>.</para>
    /// </summary>
    public static bool IsMember<TInteger>(TInteger radix, TInteger maxRadix, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where TInteger : System.Numerics.INumber<TInteger>
      => TInteger.CreateChecked(MinValue) is var minRadix && maxRadix >= minRadix && intervalNotation.IsMember(radix, minRadix, maxRadix);

    /// <summary>
    /// <para>Indicates whether a <paramref name="radix"/> is a member of a radix interval, i.e. in the range <see cref="MinValue"/>..&#x221E;, with the specified <paramref name="intervalNotation"/>.</para>
    /// </summary>
    public static bool IsMember<TInteger>(TInteger radix, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where TInteger : System.Numerics.INumber<TInteger>
      => IsMember(radix, radix + TInteger.One, intervalNotation);

    /// <summary>
    /// <para>A self number in a given number base b is a natural number that cannot be written as the sum of any other natural number n and the individual digits of number n.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static bool IsSelfNumber<TInteger, TRadix>(TInteger number, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      for (var n = number - TInteger.One; n > TInteger.Zero; n--)
        if (number == n + DigitSum(n, radix))
          return false;

      return true;
    }

    /// <summary>
    /// <para>Indicates whether the <paramref name="value"/> is single digit using the base <paramref name="radix"/>, i.e. in the interval (-<paramref name="radix"/>, +<paramref name="radix"/>).</para>
    /// </summary>
    public static bool IsSingleDigit<TInteger, TRadix>(TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TInteger.Abs(value) < TInteger.CreateChecked(Units.Radix.AssertMember(radix));

    /// <summary>
    /// <para>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</para>
    /// </summary>
    public static TInteger KeepLeastSignificantDigits<TInteger, TRadix>(TInteger value, TRadix radix, TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value % TInteger.CreateChecked(Units.Radix.AssertMember(radix).IntegerPow(count));

    /// <summary>
    /// <para>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</para>
    /// </summary>
    public static TInteger KeepMostSignificantDigits<TInteger, TRadix>(TInteger value, TRadix radix, TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value / TInteger.CreateChecked(radix.IntegerPow(Units.Radix.DigitCount(value, radix) - count));

    /// <summary>
    /// <para>Reverse the digits a <paramref name="value"/> in base <paramref name="radix"/>, obtaining a new number.</para>
    /// </summary>
    public static TInteger ReverseDigits<TInteger, TRadix>(TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
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

    public TInteger SumLeastSignificantDigits<TInteger, TRadix>(TInteger value, TRadix radix, TInteger count)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      var sum = TInteger.Zero;

      for (var i = count - TInteger.One; i >= TInteger.Zero; i--)
      {
        sum += value % rdx;

        value /= rdx;
      }

      return sum;
    }

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Radix a, Radix b) => a.CompareTo(b) < 0;
    public static bool operator <=(Radix a, Radix b) => a.CompareTo(b) <= 0;
    public static bool operator >(Radix a, Radix b) => a.CompareTo(b) > 0;
    public static bool operator >=(Radix a, Radix b) => a.CompareTo(b) >= 0;

    public static Radix operator -(Radix v) => new(-v.m_value);
    public static Radix operator +(Radix a, int b) => new(a.m_value + b);
    public static Radix operator +(Radix a, Radix b) => a + b.m_value;
    public static Radix operator /(Radix a, int b) => new(a.m_value / b);
    public static Radix operator /(Radix a, Radix b) => a / b.m_value;
    public static Radix operator *(Radix a, int b) => new(a.m_value * b);
    public static Radix operator *(Radix a, Radix b) => a * b.m_value;
    public static Radix operator %(Radix a, int b) => new(a.m_value % b);
    public static Radix operator %(Radix a, Radix b) => a % b.m_value;
    public static Radix operator -(Radix a, int b) => new(a.m_value - b);
    public static Radix operator -(Radix a, Radix b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Radix other) => m_value.CompareTo(other.m_value);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Radix o ? CompareTo(o) : -1;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The <see cref="Radix.Value"/> property is an integer radix in the range <see cref="MinRadix"/>..&#x221E;.</para>
    /// </summary>
    public int Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
