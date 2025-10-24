namespace Flux.Units
{
  /// <summary>
  /// <para>Radix is in the range of the closed interval [<see cref="Radix.MinRadix"/> = 2, <see cref="int.MaxValue"/>].</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Radix"/></para>
  /// </summary>
  public readonly record struct Radix
    : System.IComparable, System.IComparable<Radix>, System.IFormattable, IValueQuantifiable<int>
  {
    public const string Base64 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";
    //public const string Base62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public const int MinValue = 2;

    public static Radix Binary { get; } = new(2);
    public static Radix Octal { get; } = new(8);
    public static Radix Decimal { get; } = new(10);
    public static Radix Hexadecimal { get; } = new(16);

    private readonly int m_value;

    public Radix(int radix) => m_value = AssertMember(radix, IntervalNotation.Closed, nameof(radix));

    #region Radix methods

    /// <summary>
    /// <para>Gets the count of all single digits in <paramref name="value"/>.</para>
    /// </summary>
    public TNumber DigitCount<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var rdx = TNumber.CreateChecked(Units.Radix.AssertMember(m_value));

      var count = TNumber.Zero;

      while (!TNumber.IsZero(value))
      {
        count++;

        value /= rdx;
      }

      return count;
    }

    /// <summary>
    /// <para>Returns the sum of all single digits in <paramref name="value"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Digit_sum"/></para>
    /// </summary>
    public TNumber DigitSum<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var rdx = TNumber.CreateChecked(Units.Radix.AssertMember(m_value));

      var sum = TNumber.Zero;

      while (!TNumber.IsZero(value))
      {
        sum += value % rdx;

        value /= rdx;
      }

      return sum;
    }

    /// <summary>Drop the trailing (least significant) digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public TNumber DropLeastSignificantDigit<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value / TNumber.CreateChecked(m_value);

    /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public TNumber DropLeastSignificantDigits<TNumber>(TNumber value, TNumber count)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value / TNumber.CreateChecked(m_value.FastIntegerPow(count, out var _).IpowTz);

    /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public TNumber DropMostSignificantDigits<TNumber>(TNumber value, TNumber count)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value % TNumber.CreateChecked(m_value.FastIntegerPow(DigitCount(value) - count, out var _).IpowTz);

    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TNumber"/>) of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public System.Collections.Generic.List<TNumber> GetDigits<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var rdx = TNumber.CreateChecked(m_value);

      if (TNumber.IsNegative(value))
        value = TNumber.Abs(value);

      var list = new System.Collections.Generic.List<TNumber>();

      if (TNumber.IsZero(value))
        list.Add(TNumber.Zero);
      else
        while (!TNumber.IsZero(value))
        {
          list.Insert(0, value % rdx);

          value /= rdx;
        }

      return list;
    }

    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TNumber"/>) of <paramref name="value"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public System.Collections.Generic.List<TNumber> GetDigitsReversed<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var rdx = TNumber.CreateChecked(m_value);

      if (TNumber.IsNegative(value))
        value = TNumber.Abs(value);

      var list = new System.Collections.Generic.List<TNumber>();

      if (TNumber.IsZero(value))
        list.Add(TNumber.Zero);
      else
        while (!TNumber.IsZero(value))
        {
          list.Add(value % rdx);

          value /= rdx;
        }

      return list;
    }

    /// <summary>Returns the digit place value components of <paramref name="value"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</summary>
    public System.Collections.Generic.List<TNumber> GetDigitPlaceValues<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var list = GetDigitsReversed(value); // Already asserts radix.

      var rdx = TNumber.CreateChecked(m_value);

      var power = TNumber.One;

      for (int index = 0; index < list.Count; index++)
      {
        list[index] *= power;

        power *= rdx;
      }

      return list;
    }

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log ceiling of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public TNumber IntegerLogAwayFromZero<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.CopySign(TNumber.Abs(IntegerLogTowardZero(value)) is var tz && IsIntegerPowOf(value) ? tz : tz + TNumber.One, value);

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log floor of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public TNumber IntegerLogTowardZero<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (value.TryFastIntegerLog(m_value, out TNumber ilog, out TNumber _, out var _)) // Testing!
        return ilog;

      var rdx = TNumber.CreateChecked(m_value);

      if (!TNumber.IsZero(value)) // If not zero...
      {
        var log = TNumber.Zero;

        for (var val = TNumber.Abs(value); val >= rdx; val /= rdx)
          log++;

        return TNumber.CopySign(log, value);
      }
      else return value; // ...otherwise return zero.
    }

    /// <summary>
    /// <para>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</para>
    /// </summary>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero return as false.</remarks>
    public bool IsIntegerPowOf<TNumber>(TNumber number)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (m_value == 2) // Special case for binary numbers, we can use dedicated IsPow2().
        return TNumber.IsPow2(number);

      try
      {
        var powOfRadix = TNumber.CreateChecked(m_value);

        while (powOfRadix < number)
          powOfRadix = TNumber.CreateChecked(powOfRadix * powOfRadix);

        return powOfRadix == number;
      }
      catch { }

      return false;
    }

    /// <summary>Indicates whether <paramref name="value"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public bool IsJumbled<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var rdx = TNumber.CreateChecked(Units.Radix.AssertMember(m_value));

      while (!TNumber.IsZero(value))
      {
        var remainder = value % rdx;

        value /= rdx;

        if (TNumber.IsZero(value))
          break;
        else if (TNumber.Abs((value % rdx) - remainder) > TNumber.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }

    /// <summary>Indicates whether the <paramref name="value"/> is single digit using the base <paramref name="radix"/>, i.e. in the interval (-<paramref name="radix"/>, +<paramref name="radix"/>).</summary>
    public bool IsSingleDigit<TValue>(TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.Abs(value) < TValue.CreateChecked(m_value);

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public TNumber KeepLeastSignificantDigit<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value % TNumber.CreateChecked(m_value);

    /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public TNumber KeepLeastSignificantDigits<TNumber>(TNumber value, TNumber count)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value % TNumber.CreateChecked(m_value.FastIntegerPow(count, out var _).IpowTz);

    /// <summary>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public TNumber KeepMostSignificantDigits<TNumber>(TNumber value, TNumber count)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value / TNumber.CreateChecked(m_value.FastIntegerPow(DigitCount(value) - count, out var _).IpowTz);

    /// <summary>
    /// <para>Computes the max number of digits that can be represented by the specified <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether to <paramref name="accountForSignBit"/>.</para>
    /// <code>var mdcf = (10).GetMaxDigitCount(10, false); // Yields 4, because a max value of 1023 can be represented (all bits can be used in an unsigned value).</code>
    /// <code>var mdct = (10).GetMaxDigitCount(10, true); // Yields 3, because a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
    /// <code>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitLength"/> for extension method) IS THE NUMBER OF BITS (to account for).</code>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="bitLength">This is the number of bits to take into account.</param>
    /// <param name="radix">This is the radix (base) to use.</param>
    /// <param name="accountForSignBit">Indicates whether <paramref name="bitLength"/> use one bit for the sign.</param>
    /// <returns></returns>
    public int MaxDigitCountOfBitLength<TBitLength>(TBitLength bitLength, bool accountForSignBit)
    where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
    {
      if (TBitLength.IsNegative(bitLength))
        return 0;

      var swar = System.Numerics.BigInteger.CreateChecked(bitLength).CreateBitMaskRight(); // Create a bit-mask representing the greatest value for the bit-length.

      if (swar.IsSingleDigit(m_value)) // If SWAR is less than radix, there is only one digit, otherwise, compute for values higher than radix, and more digits.
        return 1;

      if (accountForSignBit) // If accounting for a sign-bit, shift the SWAR to properly represent the max of a signed type.
        swar >>>= 1;

      var (logc, _) = swar.FastIntegerLog(m_value, out var _);

      return int.CreateChecked(logc);
    }

    /// <summary>Reverse the digits of the <paramref name="value"/> obtaining a new number.</summary>
    public TNumber ReverseDigits<TNumber>(TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var rdx = TNumber.CreateChecked(m_value);

      var reversed = TNumber.Zero;

      while (!TNumber.IsZero(value))
      {
        reversed = (reversed * rdx) + (value % rdx);

        value /= rdx;
      }

      return reversed;
    }

    /// <summary>Converts the <paramref name="source"/> integer using base <paramref name="radix"/> to a decimal fraction, e.g. "123 => 0.123".</summary>
    public double ToDecimalFraction<TNumber>(TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var fil = source.FastDigitCount(m_value);

      var fip = m_value.FastIntegerPow(fil, out var _).IpowTz;

      return double.CreateChecked(source) / double.CreateChecked(fip);
    }

    ///// <summary>
    ///// <para>Converts a <paramref name="value"/> to text based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base62"/> if null).</para>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <param name="value"></param>
    ///// <param name="minLength"></param>
    ///// <param name="alphabet"></param>
    ///// <returns></returns>
    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
    //public System.ReadOnlySpan<char> ToRadixString<TNumber>(TNumber value, int minLength = 1, string alphabet = Base64)
    //  where TNumber : System.Numerics.IBinaryInteger<TNumber>
    //{
    //  var rdx = int.CreateChecked(m_value);

    //  if (rdx == 2)
    //    return value.ToBinaryString(minLength, alphabet);
    //  else if (rdx == 8)
    //    return value.ToOctalString(minLength, alphabet);
    //  else if (rdx == 10)
    //    return value.ToDecimalString(minLength, alphabet: alphabet);
    //  else if (rdx == 16)
    //    return value.ToHexadecimalString(minLength, alphabet);
    //  else
    //  {
    //    if (minLength <= 0) minLength = value.GetBitCount().MaxDigitCountOfBitLength(rdx, value.IsNumericTypeSigned());

    //    alphabet ??= Units.Radix.Base62;

    //    if (alphabet.Length < rdx) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

    //    value.TryConvertNumberToPositionalNotationIndices(m_value, out var indices);

    //    while (indices.Count < minLength)
    //      indices.Insert(0, 0); // Pad left with zeroth element.

    //    indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

    //    return symbols.AsSpan();
    //  }
    //}

    #endregion // Radix methods

    #region Static methods

    /// <summary>
    /// <para>Asserts that the <paramref name="radix"/> with an <paramref name="maxRadix"/> is valid, i.e. an integer in the range [<see cref="MinValue"/>, <paramref name="maxRadix"/>], and throws an exception if it's not.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber AssertMember<TNumber>(TNumber radix, TNumber maxRadix, IntervalNotation intervalNotation = IntervalNotation.Closed, string? paramName = null)
      where TNumber : System.Numerics.INumber<TNumber>
      => intervalNotation.IsMember(radix, TNumber.CreateChecked(MinValue), maxRadix)
      ? radix
      : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"The radix ({radix}) is out of range: {intervalNotation.ToIntervalNotationString(TNumber.CreateChecked(MinValue), maxRadix)}.");

    /// <summary>
    /// <para>Asserts that the <paramref name="radix"/> is valid, i.e. an integer in the range [<see cref="MinValue"/>, <see cref="MaxValue"/>], and throws an exception if it's not.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber AssertMember<TNumber>(TNumber radix, IntervalNotation notation = IntervalNotation.Closed, string? paramName = null)
      where TNumber : System.Numerics.INumber<TNumber>
      => AssertMember(radix, radix + TNumber.One, notation, paramName);

    /// <summary>
    /// <para>Gets the count, the sum, whether it is jumbled, is a power of, the number reversed, the place values, and the reverse digits, of <paramref name="value"/> using base <paramref name="radix"/>.</para>
    /// </summary>
    public static (TNumber DigitCount, TNumber DigitSum, bool IsJumbled, bool IsPowOf, TNumber NumberReversed, System.Collections.Generic.List<TNumber> PlaceValues, System.Collections.Generic.List<TNumber> ReverseDigits) Digits<TNumber, TRadix>(TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TNumber.CreateChecked(Units.Radix.AssertMember(radix));

      var count = TNumber.Zero;
      var isJumbled = true;
      var numberReversed = TNumber.Zero;
      var placeValues = new System.Collections.Generic.List<TNumber>();
      var reverseDigits = new System.Collections.Generic.List<TNumber>();
      var sum = TNumber.Zero;

      var power = TNumber.One;

      while (!TNumber.IsZero(value))
      {
        var rem = value % rdx;

        count++;
        numberReversed = (numberReversed * rdx) + rem;
        placeValues.Add(rem * power);
        reverseDigits.Add(rem);
        sum += rem;

        power *= rdx;

        value /= rdx;

        if (isJumbled && (TNumber.Abs((value % rdx) - rem) > TNumber.One))
          isJumbled = false;
      }

      if (TNumber.IsZero(count))
      {
        placeValues.Add(count);
        reverseDigits.Add(count);
      }

      return (count, sum, isJumbled, sum == TNumber.One, numberReversed, placeValues, reverseDigits);
    }

    /// <summary>
    /// <para>Determines whether the <paramref name="radix"/> is within <see cref="Radix"/> constrained by the specified <paramref name="intervalNotation"/> and the <paramref name="maxRadix"/>.</para>
    /// </summary>
    public static bool IsMember<TNumber>(TNumber radix, TNumber maxRadix, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.IsInteger(radix) && TNumber.CreateChecked(MinValue) is var minRadix && maxRadix >= minRadix && intervalNotation.IsMember(radix, minRadix, maxRadix);

    /// <summary>
    /// <para>Determines whether the <paramref name="radix"/> is within <see cref="Radix"/> constrained by the specified <paramref name="notation"/>.</para>
    /// </summary>
    public static bool IsMember<TNumber>(TNumber radix, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where TNumber : System.Numerics.INumber<TNumber>
      => IsMember(radix, radix + TNumber.One, intervalNotation);

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
    /// <para>The <see cref="Radix.Value"/> property is a radix in the closed interval [<see cref="MinRadix"/>, <see cref="MaxRadix"/>].</para>
    /// </summary>
    public int Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
