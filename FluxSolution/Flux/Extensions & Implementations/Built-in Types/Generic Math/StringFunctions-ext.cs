namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region ToBinaryString

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a binary (base 2) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToBinaryString(int minLength = 1, string? alphabet = null)
      {
        if (minLength <= 0) minLength = GetBitCount(value);

        alphabet ??= System.Text.Encoding.Base62;

        if (alphabet.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        var indices = new System.Collections.Generic.List<int>();

        for (var bitIndex = int.Min(int.Max(value.GetBitLength(), minLength), GetBitCount(value)) - 1; bitIndex >= 0; bitIndex--)
        {
          var bitValue = int.CreateChecked((value >>> bitIndex) & TInteger.One);

          if (bitValue > 0 || indices.Count > 0 || bitIndex < minLength)
            indices.Add(bitValue);
        }

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan().ToString();
      }

      #endregion

      #region ToCardinalNumeralString

      public string ToCardinalNumeralString() => NumeralComposition.ToCardinalNumeralString(value);

      #endregion

      #region ToDecimalString

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a decimal (base 10) string based on <paramref name="minLength"/>, <paramref name="negativeSymbol"/> and an <paramref name="alphabet"/> (<see cref="string.Base62"/> if null).</para>
      /// </summary>
      /// <param name="minLength"></param>
      /// <param name="negativeSymbol"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="string.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToDecimalString(int minLength = 1, string? alphabet = null, char negativeSymbol = '\u002D')
      {
        if (minLength <= 0) minLength = GetMaxRepresentableDigitCount(GetBitCount(value), 10, value.GetType().IsNumericsISignedNumber());

        alphabet ??= System.Text.Encoding.Base62;

        if (alphabet.Length < 10) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        var abs = TInteger.Abs(value);

        abs.TryConvertNumberToPositionalNotationIndices(10, out var indices);

        while (indices.Count < minLength)
          indices.Insert(0, 0); // Pad left with zeroth element.

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        if (TInteger.IsNegative(value))
          symbols.Insert(0, negativeSymbol); // If the value is negative AND base-2 (radix) is 10 (decimal)...

        return symbols.AsSpan().ToString();
      }

      #endregion

      #region ToHexadecimalString

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a hexadecimal (base 16) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="string.Base62"/> if null).</para>
      /// </summary>
      /// <param name="minLength"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="string.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToHexadecimalString(int minLength = 1, string? alphabet = null)
      {
        if (minLength <= 0) minLength = GetMaxRepresentableDigitCount(GetBitCount(value), 16, value.GetType().IsNumericsISignedNumber());

        alphabet ??= System.Text.Encoding.Base62;

        if (alphabet.Length < 16) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        var indices = new System.Collections.Generic.List<int>();

        for (var nibbleIndex = (value.GetByteCount() << 1) - 1; nibbleIndex >= 0; nibbleIndex--)
        {
          var nibbleValue = int.CreateChecked((value >>> (nibbleIndex << 2)) & TInteger.CreateChecked(0xF));

          if (nibbleValue > 0 || indices.Count > 0 || nibbleIndex < minLength)
            indices.Add(nibbleValue);
        }

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan().ToString();
      }

      #endregion

      #region ToOctalString

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a octal (base 8) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="string.Base62"/> if null).</para>
      /// </summary>
      /// <param name="minLength"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="string.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToOctalString(int minLength = 1, string? alphabet = null)
      {
        if (minLength <= 0) minLength = GetMaxRepresentableDigitCount(GetBitCount(value), 8, value.GetType().IsNumericsISignedNumber());

        alphabet ??= System.Text.Encoding.Base62;

        if (alphabet.Length < 8) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        value.TryConvertNumberToPositionalNotationIndices(8, out var indices);

        while (indices.Count < minLength)
          indices.Insert(0, 0); // Pad left with zeroth element.

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan().ToString();
      }

      #endregion

      #region ToOrdinalIndicatorString

      /// <summary>
      /// <para>Creates a new string with <paramref name="value"/> and its ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public string ToOrdinalIndicatorString()
        => value.ToString() + GetOrdinalIndicatorSuffix(value);

      #endregion

      #region ToOrdinalNumeralString

      public string ToOrdinalNumeralString() => NumeralComposition.ToOrdinalNumeralString(value);

      #endregion

      #region ToRadixString

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to text based on <paramref name="radix"/>, <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="string.Base62"/> if null).</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="radix"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="string.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToRadixString<TRadix>(TRadix radix, int minLength = 1, string? alphabet = null)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var rdx = int.CreateChecked(radix);

        if (rdx == 2)
          return value.ToBinaryString(minLength, alphabet);
        else if (rdx == 8)
          return value.ToOctalString(minLength, alphabet);
        else if (rdx == 10)
          return value.ToDecimalString(minLength, alphabet);
        else if (rdx == 16)
          return value.ToHexadecimalString(minLength, alphabet);
        else
        {
          if (minLength <= 0) minLength = GetMaxRepresentableDigitCount(GetBitCount(value), rdx, value.GetType().IsNumericsISignedNumber());

          alphabet ??= System.Text.Encoding.Base62;

          if (alphabet.Length < rdx) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

          value.TryConvertNumberToPositionalNotationIndices(radix, out var indices);

          while (indices.Count < minLength)
            indices.Insert(0, 0); // Pad left with zeroth element.

          indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

          return symbols.AsSpan().ToString();
        }
      }

      #endregion

      #region ToSubscriptString

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to subscript text using <paramref name="radix"/> (base) and a <paramref name="minLength"/>.</para>
      /// </summary>
      /// <remarks>Subscript can operate with up to base-10 (<paramref name="radix"/>).</remarks>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="radix"></param>
      /// <param name="minLength"></param>
      /// <returns></returns>
      public string ToSubscriptString<TRadix>(TRadix radix, int minLength = 1)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var alphabet = "\u2080\u2081\u2082\u2083\u2084\u2085\u2086\u2087\u2088\u2089";

        return ToRadixString(value, radix, minLength, alphabet); // Extra top-limit to radix (only 10 characters in subscript alphabet).
      }

      #endregion

      #region ToSuperscriptString

      /// <summary>
      /// <para>Creates a new superscript string from an integer in the specified <paramref name="radix"/> (base) and <paramref name="minLength"/>.</para>
      /// </summary>
      /// <remarks>Superscript can operate with up to base-16 (<paramref name="radix"/>).</remarks>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="radix"></param>
      /// <param name="minLength"></param>
      /// <param name="upperCase"></param>
      /// <returns></returns>
      public string ToSuperscriptString<TRadix>(TRadix radix, int minLength = 1, bool upperCase = false)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var alphabet = "\u2070\u00B9\u00B2\u00B3\u2074\u2075\u2076\u2077\u2078\u2079";

        alphabet += upperCase // Add six letters to accomodate up to radix-16 (hexadecimal).
          ? "\u1D2C\u1D2E\uA7F2\u1D30\u1D31\uA7F3" // Upper-case letters.
          : "\u1D43\u1D47\u1D9C\u1D48\u1D49\u1DA0"; // Lower-case letters.

        return ToRadixString(value, radix, minLength, alphabet); // Extra top-limit to radix (only 16 characters in superscript alphabet, but choice of lower/upper case).
      }

      #endregion
    }
  }

  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region ToCardinalNumeralString

      public string ToCardinalNumeralString() => NumeralComposition.ToCardinalNumeralString(value);

      #endregion

      #region ToStringWithCustomDecimals

      public string ToStringWithCustomDecimals(int numberOfDecimals = 339)
        => value.ToString(BinaryInteger.CreateFormatStringWithCountDecimals(numberOfDecimals), null);

      #endregion
    }
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber value)
    where TNumber : System.Numerics.INumber<TNumber>
    {
      #region ToEngineeringNotationString

      /// <summary>
      /// <para>Creates a new string in engineering notation, a version of scientific notation in which the exponent of ten is always selected to be divisible by three to match the common metric prefixes (<see cref="Units.MetricPrefix"/>).</para>
      /// </summary>
      /// <param name="stringBuilder"></param>
      /// <param name="unit"></param>
      /// <param name="format"></param>
      /// <param name="formatProvider"></param>
      /// <param name="restrictToTriplets"></param>
      /// <returns></returns>
      public string ToEngineeringNotationString(string? unit = null, string? format = null, System.IFormatProvider? formatProvider = null, bool restrictToTriplets = true, UnicodeSpacing spacing = UnicodeSpacing.Space, System.Text.StringBuilder? stringBuilder = null)
      {
        stringBuilder ??= new System.Text.StringBuilder();

        if (!string.IsNullOrWhiteSpace(unit))
          stringBuilder.Insert(0, unit);

        var engineeringNotationPrefix = Units.MetricPrefix.Unprefixed;

        var engineeringNotationValue = decimal.CreateChecked(value);

        if (engineeringNotationValue != 0)
          checked
          {
            engineeringNotationPrefix = double.Log10(double.Abs(double.CreateChecked(engineeringNotationValue))) is var log10 && restrictToTriplets
              ? (Units.MetricPrefix)int.CreateChecked(double.Floor(log10 / 3) * 3)
              : System.Enum.GetValues<Units.MetricPrefix>().InfimumSupremum(int.CreateChecked(double.Floor(log10)), mp => (int)mp, true).InfimumElement;

            engineeringNotationValue *= (decimal)double.Pow(10, -(int)engineeringNotationPrefix);
          }

        var symbol = engineeringNotationPrefix.GetMetricPrefixSymbol(false);

        if (!string.IsNullOrWhiteSpace(symbol))
          stringBuilder.Insert(0, symbol);

        if (stringBuilder.Length > 0)
          stringBuilder.Insert(0, spacing.ToSpacingString());

        stringBuilder.Insert(0, TNumber.CreateChecked(engineeringNotationValue).ToString(format, formatProvider));

        return stringBuilder.ToString();
      }

      #endregion
    }
  }
}
