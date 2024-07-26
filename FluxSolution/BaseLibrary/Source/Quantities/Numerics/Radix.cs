namespace Flux.Quantities
{
  /// <summary>
  /// <para>Radix, unit of natural number, in the range [<see cref="Radix.MinRadix"/>, <see cref="Radix.MaxRadix"/>].</para>
  /// <para><seealso cref="https://en.wikipedia.org/wiki/Radix"/></para>
  /// </summary>
  public readonly record struct Radix
    : System.IComparable, System.IComparable<Radix>, System.IFormattable, System.Numerics.IMinMaxValue<int>, IValueQuantifiable<int>
  {
    //public const int MinRadix = 2;
    //public const int MaxRadix = 256;

    private readonly int m_value;

    public Radix(int radix) => m_value = IntervalNotation.Closed.AssertValidMember(radix, MinRadix, MaxRadix, nameof(radix));

    #region Static methods

    ///// <summary>Asserts the number is a valid <paramref name="radix"/> (throws an exception if not).</summary>
    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
    //public static TSelf AssertMember<TSelf>(TSelf radix, string? paramName = null)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  => IntervalNotation.Closed.AssertValidMember(radix, TSelf.CreateChecked(MinRadix), TSelf.CreateChecked(MaxRadix), paramName ?? nameof(radix));

    /// <summary>Asserts that <paramref name="radix"/> is valid, with an <paramref name="alernativeMaxRadix"/> (throws an exception, if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertMember<TSelf>(TSelf radix, TSelf alernativeMaxRadix, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IntervalNotation.Closed.AssertValidMember(radix, TSelf.CreateChecked(MinRadix), TSelf.Min(alernativeMaxRadix, TSelf.CreateChecked(MaxRadix)), paramName ?? nameof(radix));

    /// <summary>
    /// <para>Convert a value to a Gray code with the given base and digits. Iterating through a sequence of values would result in a sequence of Gray codes in which only one digit changes at a time.</para>
    /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
    /// </summary>
    /// <remarks>Experimental adaption from wikipedia.</remarks>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static TSelf[] BinaryToGray<TSelf>(TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>, System.Numerics.IUnsignedNumber<TSelf>
    {
      var gray = new TSelf[int.CreateChecked(DigitCount(value, radix))];

      var baseN = new TSelf[gray.Length]; // Stores the ordinary base-N number, one digit per entry

      for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
      {
        baseN[index] = value % radix;

        value /= radix;
      }

      var shift = TSelf.Zero; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

      for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
      {
        gray[index] = (baseN[index] + shift) % radix;

        shift = shift + radix - gray[index]; // Subtract from base so shift is positive
      }

      return gray;
    }

    /// <summary>Returns the count of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DigitCount<TSelf>(TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertMember(radix);

      var count = TSelf.Zero;

      while (!TSelf.IsZero(value))
      {
        count++;

        value /= radix;
      }

      return count;
    }

    /// <summary>Returns the sum of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static TSelf DigitSum<TSelf>(TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertMember(radix);

      var sum = TSelf.Zero;

      while (!TSelf.IsZero(value))
      {
        sum += value % radix;

        value /= radix;
      }

      return sum;
    }

    /// <summary>Drop the trailing (least significant) digit of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropLeastSignificantDigit<TSelf>(TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number / AssertMember(radix);

    /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropLeastSignificantDigits<TSelf>(TSelf number, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number / Maths.IntegerPow(AssertMember(radix), count);

    /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DropMostSignificantDigits<TSelf>(TSelf number, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number % Maths.IntegerPow(radix, DigitCount(number, radix) - count);

    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TSelf"/>) of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static System.Collections.Generic.List<TSelf> GetDigits<TSelf>(TSelf number, TSelf radix, int count = int.MaxValue)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var reversed = GetDigitsReversed(number, radix, count);
      reversed.Reverse();
      return reversed;
    }

    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TSelf"/>) of <paramref name="number"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public static System.Collections.Generic.List<TSelf> GetDigitsReversed<TSelf>(TSelf number, TSelf radix, int count = int.MaxValue)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertMember(radix);

      if (TSelf.IsNegative(number))
        number = TSelf.Abs(number);

      var list = new System.Collections.Generic.List<TSelf>();

      if (TSelf.IsZero(number))
        list.Add(TSelf.Zero);
      else
        while (!TSelf.IsZero(number) && list.Count < count)
        {
          list.Add(number % radix);
          number /= radix;
        }

      return list;
    }

    /// <summary>Returns the digit place value components of <paramref name="number"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</summary>
    public static System.Collections.Generic.List<TSelf> GetPlaceValues<TSelf>(TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var list = GetDigitsReversed(number, radix); // Already asserts radix.

      var power = TSelf.One;

      for (int index = 0; index < list.Count; index++)
      {
        list[index] *= power;

        power *= radix;
      }

      return list;
    }

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log floor of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TSelf IntegerLogTowardZero<TSelf>(TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertMember(radix);

      var v = TSelf.Abs(value);

      var ilog = TSelf.Zero;

      if (!TSelf.IsZero(v))
        while (v >= radix)
        {
          v /= radix;

          ilog++;
        }

      return TSelf.CopySign(ilog, value);
    }

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log ceiling of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TSelf IntegerLogAwayFromZero<TSelf>(TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.CopySign(TSelf.Abs(IntegerLogTowardZero(value, radix)) is var tz && IsPowOf(value, radix) ? tz : tz + TSelf.One, value);

    /// <summary>Indicates whether <paramref name="number"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public static bool IsJumbled<TSelf>(TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertMember(radix);

      while (!TSelf.IsZero(number))
      {
        var remainder = number % radix;

        number /= radix;

        if (TSelf.IsZero(number))
          break;
        else if (TSelf.Abs((number % radix) - remainder) > TSelf.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }

    /// <summary>
    /// <para>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</para>
    /// </summary>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns false.</remarks>
    public static bool IsPowOf<TSelf>(TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertMember(radix);

      value = TSelf.Abs(value);

      if (radix == TSelf.CreateChecked(2)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TSelf.IsPow2(value);

      if (value >= radix)
        while (TSelf.IsZero(value % radix))
          value /= radix;

      return value == TSelf.One;
    }

    /// <summary>Returns whether <paramref name="number"/> using base <paramref name="radix"/> is a self number. A self number, Colombian number or Devlali number, in a given number base (radix) is a natural number that cannot be written as the sum of any other natural number n and the individual digits of n.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Self_number"/>
    public static bool IsSelfNumber<TSelf>(TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var value = SelfNumberLowBound(number, radix); value < number; value++)
        if (DigitSum(value, radix) + value == number)
          return false;

      return true;
    }

    /// <summary>Indicates whether the <paramref name="number"/> is single digit using the base <paramref name="radix"/>, i.e. in the range [-<paramref name="radix"/>, <paramref name="radix"/>].</summary>
    public static bool IsSingleDigit<TSelf>(TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertMember(radix);

      return TSelf.IsZero(number) || (TSelf.IsPositive(number) && number < radix) || (TSelf.IsNegative(number) && number > -radix);
    }

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigit<TSelf>(TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value % AssertMember(radix);

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepLeastSignificantDigits<TSelf>(TSelf value, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value % Maths.IntegerPow(AssertMember(radix), count);

    /// <summary>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TSelf KeepMostSignificantDigits<TSelf>(TSelf value, TSelf radix, TSelf count)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value / Maths.IntegerPow(radix, DigitCount(value, radix) - count);

    /// <summary>
    /// <para>Computes the max number of digits that can be represented by the <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether it <paramref name="isSigned"/>.</para>
    /// <code>var maxDigitCount = Flux.Bits.GetMaxDigitCount(10, 10, false); // Yields 4, because with 10 bits, a radix of 10 (the decimal system) and unsigned, a max value of 1023 can be represented (all bits can be used, because it is an unsigned value).</code>
    /// <code>var maxDigitCount = Flux.Bits.GetMaxDigitCount(10, 10, true); // Yields 3, because with 10 bits, a radix of 10 (the decimal system) and signed, a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
    /// </summary>
    public static int MaxDigitCount<TSelf>(TSelf bitLength, TSelf radix, bool isSigned)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var foldedRight = (TSelf.One << int.CreateChecked(bitLength)) - TSelf.One;

      if (isSigned)
        foldedRight >>= 1; // Shift to properly represent a most-significant-bit used for negative values.

      return int.CreateChecked(Quantities.Radix.IntegerLogTowardZero(foldedRight, radix) + TSelf.One);
    }

    /// <summary>Computes the away-from-zero power-of-<paramref name="radix"/> of <paramref name="value"/>.</summary>
    /// <param name="value">The value for which the toward-zero (floor if positive) power-of-<paramref name="radix"/> will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="unequal">Whether the power-of-<paramref name="radix"/> must be unequal to <paramref name="value"/>.</param>
    /// <returns>The away-from-zero (ceiling, if <paramref name="value"/> is positive) power-of-<paramref name="radix"/> of <paramref name="value"/>.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf PowOfAwayFromZero<TSelf>(TSelf value, TSelf radix, bool unequal)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(value) ? value : TSelf.CopySign(TSelf.Abs(value) is var v && radix.IntegerPow(IntegerLogTowardZero(v, radix)) is var ipow && (ipow == v ? ipow : ipow * radix) is var afz && unequal && afz == v ? afz * radix : afz, value);

    /// <summary>Computes the toward-zero power-of-<paramref name="radix"/> of <paramref name="value"/>.</summary>
    /// <param name="value">The value for which the toward-zero (floor if positive) power-of-<paramref name="radix"/> will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="unequal">Whether the power-of-<paramref name="radix"/> must be unequal to <paramref name="value"/>.</param>
    /// <returns>The toward-zero (floor, if <paramref name="value"/> is positive) power-of-<paramref name="radix"/> of <paramref name="value"/>.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf PowOfTowardZero<TSelf>(TSelf value, TSelf radix, bool unequal)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(value) ? value : TSelf.CopySign(TSelf.Abs(value) is var v && radix.IntegerPow(IntegerLogTowardZero(v, radix)) is var ipow && unequal && ipow == v ? ipow / radix : ipow, value);

    /// <summary>Reverse the digits of <paramref name="number"/> using base <paramref name="radix"/>, obtaining a new number.</summary>
    public static TSelf ReverseDigits<TSelf>(TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertMember(radix);

      var reversed = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        reversed = (reversed * radix) + (number % radix);

        number /= radix;
      }

      return reversed;
    }

    /// <summary>Returns the minimum possible number that can make <paramref name="value"/> a self number using base <paramref name="radix"/>.</summary>
    public static TSelf SelfNumberLowBound<TSelf>(TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (value <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      var logRadix = TSelf.CreateChecked(IntegerLogTowardZero(value, radix));
      var maxDistinct = (TSelf.CreateChecked(9) * logRadix) + (value / Maths.IntegerPow(radix, logRadix));

      return TSelf.Max(value - maxDistinct, TSelf.Zero);
    }

    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSubscriptString<TSelf>(TSelf number, int radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => PositionalNotation.NumberToText(number, "\u2080\u2081\u2082\u2083\u2084\u2085\u2086\u2087\u2088\u2089".AsSpan()[..AssertMember(radix, 10)], '\u002D').ToString();

    /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToSuperscriptString<TSelf>(TSelf number, int radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => PositionalNotation.NumberToText(number, "\u2070\u00B9\u00B2\u00B3\u2074\u2075\u2076\u2077\u2078\u2079".AsSpan()[..AssertMember(radix, 10)], '\u002D').ToString();

    /// <summary>Converts a <paramref name="number"/> to a list of <paramref name="positionalNotationIndices"/>, based on the specified <paramref name="radix"/>.</summary>
    public static bool TryConvertNumberToPositionalNotationIndices<TValue, TRadix>(TValue number, TRadix radix, out System.Collections.Generic.List<int> positionalNotationIndices)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (TValue.IsNegative(number))
        return TryConvertNumberToPositionalNotationIndices(TValue.Abs(number), radix, out positionalNotationIndices);

      positionalNotationIndices = new();

      try
      {
        AssertMember(radix);

        while (!TValue.IsZero(number))
        {
          (number, var remainder) = TValue.DivRem(number, TValue.CreateChecked(radix));

          positionalNotationIndices.Insert(0, int.CreateChecked(remainder));
        }

        return true;
      }
      catch { }

      return false;
    }

    /// <summary>Converts a list of <paramref name="positionalNotationIndices"/> to a <paramref name="number"/>, based on the specified <paramref name="radix"/>.</summary>
    public static bool TryConvertPositionalNotationIndicesToNumber<TRadix, TValue>(System.Collections.Generic.IList<int> positionalNotationIndices, TRadix radix, out TValue number)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      number = TValue.Zero;

      try
      {
        AssertMember(radix);

        for (var index = 0; index < positionalNotationIndices.Count; index++)
        {
          number *= TValue.CreateChecked(radix);

          number += TValue.CreateChecked(positionalNotationIndices[index]);
        }

        return true;
      }
      catch { }

      return false;
    }

    /// <summary>Returns whether the number is a valid <paramref name="radix"/>.</summary>
    public static bool VerifyMember<TSelf>(TSelf radix)
      where TSelf : System.Numerics.INumber<TSelf>
      => IntervalNotation.Closed.IsValidMember(radix, TSelf.CreateChecked(MinRadix), TSelf.CreateChecked(MaxRadix));

    /// <summary>Returns whether the number is a valid <paramref name="radix"/>, with an <paramref name="alernativeMaxRadix"/>.</summary>
    public static bool VerifyMember<TSelf>(TSelf radix, TSelf alernativeMaxRadix)
      where TSelf : System.Numerics.INumber<TSelf>
      => IntervalNotation.Closed.IsValidMember(radix, TSelf.CreateChecked(MinRadix), alernativeMaxRadix);

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
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, $"{{0{(format is null ? string.Empty : $":{format}")}}}", m_value);

    public readonly int MinRadix => 2;
    public readonly int MaxRadix => 256;

    // IQuantifiable<>
    /// <summary>
    /// <para>The <see cref="Radix.Value"/> property is a radix in the closed interval [<see cref="MinRadix"/>, <see cref="MaxRadix"/>].</para>
    /// </summary>
    public int Value => m_value;

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
