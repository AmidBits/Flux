namespace Flux
{
  public static class NumericFormatting
  {
    #region EngineeringNotation

    /// <summary>
    /// <para>Gets a prefix and value representing the engineering notation of <paramref name="value"/> and optionally whether to <paramref name="restrictToTriplets"/>.</para>
    /// <example><code>var (p, v) = (1803).GetEngineeringNotationProperties(); // (p: "k", v: 1.803)</code></example>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="restrictToTriplets"></param>
    /// <returns></returns>
    private static (Units.MetricPrefix EngineeringNotationPrefix, decimal EngineeringNotationValue) GetEngineeringNotationProperties(decimal value, bool restrictToTriplets = true)
    {
      var engineeringNotationPrefix = Units.MetricPrefix.Unprefixed;

      var engineeringNotationValue = value;

      if (engineeringNotationValue != 0)
      {
        engineeringNotationPrefix = double.Log10(double.Abs(double.CreateChecked(engineeringNotationValue))) is var log10 && restrictToTriplets
        ? (Units.MetricPrefix)int.CreateChecked(double.Floor(log10 / 3) * 3)
        : System.Enum.GetValues<Units.MetricPrefix>().GetInfimumAndSupremum(mp => (int)mp, int.CreateChecked(double.Floor(log10)), true).InfimumItem;

        engineeringNotationValue *= (decimal)double.Pow(10, -(int)engineeringNotationPrefix);
      }

      return (engineeringNotationPrefix, engineeringNotationValue);
    }

    public static string ToEngineeringNotationString<TNumber>(this TNumber value, System.Text.StringBuilder? stringBuilder = null, string? unit = null, string? format = null, System.IFormatProvider? formatProvider = null, bool restrictToTriplets = true)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      stringBuilder ??= new System.Text.StringBuilder();

      if (!string.IsNullOrWhiteSpace(unit))
        stringBuilder.Insert(0, unit);

      var (engineeringNotationPrefix, engineeringNotationValue) = GetEngineeringNotationProperties(decimal.CreateChecked(value), restrictToTriplets);

      var symbol = engineeringNotationPrefix.GetMetricPrefixSymbol(false);

      if (!string.IsNullOrWhiteSpace(symbol))
        stringBuilder.Insert(0, symbol);

      if (stringBuilder.Length > 0)
        stringBuilder.Insert(0, ' ');

      stringBuilder.Insert(0, TNumber.CreateChecked(engineeringNotationValue).ToString(format, formatProvider));

      return stringBuilder.ToString();
    }
    //extension<TNumber>(TNumber value)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //{
    ///// <summary>
    ///// <para>Gets a prefix and value representing the engineering notation of <paramref name="value"/> and optionally whether to <paramref name="restrictToTriplets"/>.</para>
    ///// <example><code>var (p, v) = (1803).GetEngineeringNotationProperties(); // (p: "k", v: 1.803)</code></example>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <param name="value"></param>
    ///// <param name="restrictToTriplets"></param>
    ///// <returns></returns>
    //public (Units.MetricPrefix EngineeringNotationPrefix, double EngineeringNotationValue) GetEngineeringNotationProperties(bool restrictToTriplets = true)
    //{
    //  var prefix = Units.MetricPrefix.Unprefixed;

    //  var number = double.CreateChecked(value);

    //  if (!TNumber.IsZero(value))
    //  {
    //    prefix = double.Log10(double.Abs(number)) is var log10 && restrictToTriplets
    //    ? (Units.MetricPrefix)int.CreateChecked(double.Floor(log10 / 3) * 3)
    //    : System.Enum.GetValues<Units.MetricPrefix>().GetInfimumAndSupremum(mp => (int)mp, int.CreateChecked(double.Floor(log10)), true).InfimumItem;

    //    number *= double.Pow(10, -(int)prefix);
    //  }

    //  return (prefix, number);
    //}

    //  /// <summary>
    //  /// <para>Creates a string representation of the <paramref name="value"/> number in engineering notation, with options of <paramref name="format"/>, <paramref name="formatProvider"/>, <paramref name="spacing"/> and <paramref name="restrictToTriplets"/>.</para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Engineering_notation"/></para>
    //  /// <example><code>var v = (1803).ToEngineeringNotationString("g"); // v == "1.803 kg"</code></example>
    //  /// </summary>
    //  /// <typeparam name="TNumber"></typeparam>
    //  /// <param name="value"></param>
    //  /// <param name="unit"></param>
    //  /// <param name="format"></param>
    //  /// <param name="formatProvider"></param>
    //  /// <param name="spacing"></param>
    //  /// <param name="restrictToTriplets"></param>
    //  /// <returns></returns>
    //  public string ToEngineeringNotationString(string? unit = null, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.ThinSpace, bool restrictToTriplets = true)
    //  {
    //    if (TNumber.IsZero(value))
    //      return '0'.ToString();

    //    var sb = new System.Text.StringBuilder();

    //    var (enp, env) = value.GetEngineeringNotationProperties(restrictToTriplets);

    //    sb.Append(env.ToString(format, formatProvider));

    //    var symbol = enp.GetMetricPrefixSymbol(false);

    //    var hasSymbol = symbol.Length > 0;
    //    var hasUnit = !string.IsNullOrWhiteSpace(unit);

    //    if (hasSymbol || hasUnit)
    //    {
    //      sb.Append(spacing.ToSpacingString());

    //      if (hasSymbol)
    //        sb.Append(symbol);

    //      if (hasUnit)
    //        sb.Append(unit);
    //    }

    //    return sb.ToString();
    //  }
    //}

    #endregion

    extension<TInteger>(TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region OrdinalColumnNames

      /// <summary>
      /// <para>Returns a generic <paramref name="columnNamePrefix"/> for the <paramref name="value"/> as if it was an index of a 0-based column-structure.</para>
      /// <para>+1 is added to the <paramref name="value"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="numericWidth"></param>
      /// <param name="columnNamePrefix"></param>
      /// <returns></returns>
      public string ToSingleOrdinalColumnName(int numericWidth, string columnNamePrefix = "Column")
        => columnNamePrefix + (value + TInteger.One).ToString($"D{numericWidth}", null);

      /// <summary>
      /// <para>Returns a generic <paramref name="columnNamePrefix"/> for the <paramref name="value"/> as if it was an index of a 0-based column-structure.</para>
      /// <para>+1 is added to the <paramref name="value"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="columnNamePrefix"></param>
      /// <returns></returns>
      public string ToSingleOrdinalColumnName(string columnNamePrefix = "Column")
        => value.ToSingleOrdinalColumnName(int.CreateChecked(Units.Radix.DigitCount(value, TInteger.CreateChecked(10))), columnNamePrefix);

      /// <summary>
      /// <para>Creates an array of generic column-<paramref name="columnNamePrefix"/>s for <paramref name="value"/> amount of columns.</para>
      /// <example><paramref name="value"/> = 3, returns <c>["Column1", "Column2", "Column3"]</c></example>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="columnNamePrefix"></param>
      /// <returns></returns>
      public string[] ToMultipleOrdinalColumnNames(string columnNamePrefix = "Column")
      {
        var maxWidth = int.CreateChecked(Units.Radix.DigitCount(value, 10));

        return [.. System.Linq.Enumerable.Range(1, int.CreateChecked(value)).Select(i => i.ToSingleOrdinalColumnName(maxWidth, columnNamePrefix))];
      }

      #endregion

      #region OrdinalIndicator

      /// <summary>
      /// <para>Gets the ordinal indicator suffix for <paramref name="value"/>. E.g. "st" for 1 and "nd" for 122.</para>
      /// </summary>
      /// <remarks>The suffixes "st", "nd" and "rd" are consistent for all numbers ending in 1, 2 and 3, resp., except for 11, 12 and 13, which, with all other numbers, ends with the suffix "th".</remarks>
      public string GetOrdinalIndicatorSuffix()
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

      /// <summary>
      /// <para>Creates a new string with <paramref name="value"/> and its ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</para>
      /// </summary>
      public string ToOrdinalIndicatorString()
        => value.ToString() + GetOrdinalIndicatorSuffix(value);

      #endregion

      #region To..String (Binary, Decimal, Hexadecimal, Octal, Radix, Subscript, Superscript, etc.)

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a binary (base 2) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.ReadOnlySpan<char> ToBinaryString(int minLength = 1, string alphabet = Units.Radix.Base64)
      {
        if (minLength <= 0) minLength = value.GetBitCount();

        alphabet ??= Units.Radix.Base64;

        if (alphabet.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        var indices = new System.Collections.Generic.List<int>();

        for (var bitIndex = int.Min(int.Max(value.GetBitLength(), minLength), value.GetBitCount()) - 1; bitIndex >= 0; bitIndex--)
        {
          var bitValue = int.CreateChecked((value >>> bitIndex) & TInteger.One);

          if (bitValue > 0 || indices.Count > 0 || bitIndex < minLength)
            indices.Add(bitValue);
        }

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan();
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a decimal (base 10) string based on <paramref name="minLength"/>, <paramref name="negativeSymbol"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="minLength"></param>
      /// <param name="negativeSymbol"></param>
      /// <param name="alphabet"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.ReadOnlySpan<char> ToDecimalString(int minLength = 1, char negativeSymbol = '\u002D', string alphabet = Units.Radix.Base64)
      {
        if (minLength <= 0) minLength = value.GetBitCount().BitLengthToMaxDigitCount<int, int>(10, value.IsNumericTypeSigned());

        alphabet ??= Units.Radix.Base64;

        if (alphabet.Length < 10) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        var abs = TInteger.Abs(value);

        abs.TryConvertNumberToPositionalNotationIndices(10, out var indices);

        while (indices.Count < minLength)
          indices.Insert(0, 0); // Pad left with zeroth element.

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        if (TInteger.IsNegative(value))
          symbols.Insert(0, negativeSymbol); // If the value is negative AND base-2 (radix) is 10 (decimal)...

        return symbols.AsSpan();
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a hexadecimal (base 16) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.ReadOnlySpan<char> ToHexadecimalString(int minLength = 1, string alphabet = Units.Radix.Base64)
      {
        if (minLength <= 0) minLength = value.GetBitCount().BitLengthToMaxDigitCount<int, int>(16, value.IsNumericTypeSigned());

        alphabet ??= Units.Radix.Base64;

        if (alphabet.Length < 16) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        var indices = new System.Collections.Generic.List<int>();

        for (var nibbleIndex = (value.GetByteCount() << 1) - 1; nibbleIndex >= 0; nibbleIndex--)
        {
          var nibbleValue = int.CreateChecked((value >>> (nibbleIndex << 2)) & TInteger.CreateChecked(0xF));

          if (nibbleValue > 0 || indices.Count > 0 || nibbleIndex < minLength)
            indices.Add(nibbleValue);
        }

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan();
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a octal (base 8) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.ReadOnlySpan<char> ToOctalString(int minLength = 1, string alphabet = Units.Radix.Base64)
      {
        if (minLength <= 0) minLength = value.GetBitCount().BitLengthToMaxDigitCount<int, int>(8, value.IsNumericTypeSigned());

        alphabet ??= Units.Radix.Base64;

        if (alphabet.Length < 8) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        value.TryConvertNumberToPositionalNotationIndices(8, out var indices);

        while (indices.Count < minLength)
          indices.Insert(0, 0); // Pad left with zeroth element.

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan();
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to text based on <paramref name="radix"/>, <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.ReadOnlySpan<char> ToRadixString<TRadix>(TRadix radix, int minLength = 1, string alphabet = Units.Radix.Base64)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var rdx = int.CreateChecked(Units.Radix.AssertMember(radix));

        if (rdx == 2)
          return value.ToBinaryString(minLength, alphabet);
        else if (rdx == 8)
          return value.ToOctalString(minLength, alphabet);
        else if (rdx == 10)
          return value.ToDecimalString(minLength, alphabet: alphabet);
        else if (rdx == 16)
          return value.ToHexadecimalString(minLength, alphabet);
        else
        {
          if (minLength <= 0) minLength = value.GetBitCount().BitLengthToMaxDigitCount<int, int>(rdx, value.IsNumericTypeSigned());

          alphabet ??= Units.Radix.Base64;

          if (alphabet.Length < rdx) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

          value.TryConvertNumberToPositionalNotationIndices(radix, out var indices);

          while (indices.Count < minLength)
            indices.Insert(0, 0); // Pad left with zeroth element.

          indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

          return symbols.AsSpan();
        }
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to subscript text using <paramref name="radix"/> (base) and a <paramref name="minLength"/>.</para>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <param name="minLength"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<char> ToSubscriptString<TRadix>(TRadix radix, int minLength = 1)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var alphabet = "\u2080\u2081\u2082\u2083\u2084\u2085\u2086\u2087\u2088\u2089";

        return ToRadixString(value, Units.Radix.AssertMember(radix, TRadix.CreateChecked(alphabet.Length)), minLength, alphabet); // Extra top-limit to radix (only 10 characters in subscript alphabet).
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to superscript text using <paramref name="radix"/> (base) and a <paramref name="minLength"/>.</para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <param name="minLength"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<char> ToSuperscriptString<TRadix>(TRadix radix, int minLength = 1, bool upperCase = false)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var alphabet = "\u2070\u00B9\u00B2\u00B3\u2074\u2075\u2076\u2077\u2078\u2079";

        alphabet += upperCase ? "\u1D2C\u1D2E\uA7F2\u1D30\u1D31\uA7F3" : "\u1D43\u1D47\u1D9C\u1D48\u1D49\u1DA0";

        return ToRadixString(value, Units.Radix.AssertMember(radix, TRadix.CreateChecked(alphabet.Length)), minLength, alphabet); // Extra top-limit to radix (only 16 characters in superscript alphabet, but choice of lower/upper case).
      }

      #endregion
    }

    #region WithCountDecimals

    /// <summary>
    /// <para>Returns a string format for a specified number of fractional digits.</para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static string GetFormatWithCountDecimals<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfLessThan(value, TInteger.One);
      System.ArgumentOutOfRangeException.ThrowIfGreaterThan(value, TInteger.CreateChecked(339));

      return "0." + new string('#', int.CreateChecked(value));
    }

    public static string ToStringWithCountDecimals<TFloat>(this TFloat value, int numberOfDecimals = 339)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => value.ToString(numberOfDecimals.GetFormatWithCountDecimals(), null);

    #endregion
  }
}
