namespace Flux
{
  //public enum NumericBitSize
  //{
  //  /// <summary>The total number of bits used by a type in order to represent a number.</summary>
  //  BitCount,
  //  /// <summary>The minimum number of bits (a.k.a. shortest-bit-length) needed to represent a number.</summary>
  //  BitLength
  //}

#if NET7_0_OR_GREATER

  /// <summary>Convert a number into a positional notation text string.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Positional_notation"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
  public static partial class PositionalNotation
  {
    public const string Base64 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";

    #region Static methods

    /// <summary>Converts a <paramref name="number"/> to a list of <paramref name="symbols"/>, based on the specified <paramref name="radix"/> and <paramref name="alphabet"/>.</summary>
    public static bool TryConvertPositionalNotationIndicesToSymbols<TSymbol>(System.Collections.Generic.IList<int> positionalNotationIndices, System.ReadOnlySpan<TSymbol> alphabet, out System.Collections.Generic.List<TSymbol> symbols)
    {
      symbols = new();

      try
      {
        for (var i = 0; i < positionalNotationIndices.Count; i++)
          symbols.Add(alphabet[positionalNotationIndices[i]]);

        return true;
      }
      catch { }

      return false;
    }

    /// <summary>Converts a list of <paramref name="symbols"/> to a <paramref name="number"/>, based on the specified <paramref name="radix"/> and <paramref name="alphabet"/>.</summary>
    public static bool TryConvertSymbolsToPositionalNotationIndices<TSymbol>(System.ReadOnlySpan<TSymbol> symbols, System.ReadOnlySpan<TSymbol> alphabet, out System.Collections.Generic.List<int> positionalNotationIndices)
    {
      positionalNotationIndices = new();

      try
      {
        for (var i = 0; i < symbols.Length; i++)
          positionalNotationIndices.Add(alphabet.IndexOf(symbols[i]));

        return true;
      }
      catch { }

      return false;
    }

    /// <summary>Converts <paramref name="number"/> to a positional notation text string with the specified <paramref name="minLength"/>.</summary>
    public static System.ReadOnlySpan<TSymbol> NumberToText<TSelf, TSymbol>(TSelf number, System.ReadOnlySpan<TSymbol> alphabet, TSymbol negativeSymbol, int minLength = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var indices = new System.Collections.Generic.List<int>();

      if (alphabet.Length == 2) // Special case for base-2 (radix).
      {
        for (var bitIndex = number.GetBitLengthEx() - 1; bitIndex >= 0; bitIndex--)
          if ((int.CreateChecked((number >> bitIndex) & TSelf.One) is var position && position > 0) || indices.Count > 0)
            indices.Add(position);
      }
      else // Otherwise use generic algorithm.
        Quantities.Radix.TryConvertNumberToPositionalNotationIndices(number, TSelf.CreateChecked(alphabet.Length), out indices);

      while (indices.Count < minLength)
        indices.Insert(0, 0); // Pad left with zeroth element.

      TryConvertPositionalNotationIndicesToSymbols(indices, alphabet, out var symbols);

      if (TSelf.IsNegative(number) && alphabet.Length == 10)
        symbols.Insert(0, negativeSymbol); // If the value is negative AND base-2 (radix) is 10 (decimal)...

      return symbols.AsSpan();
    }

    /// <summary>Convert a positional notation <paramref name="text"/> string to a number using <paramref name="alphabet"/> and <paramref name="negativeSymbol"/>.</summary>
    public static TSelf TextToNumber<TSelf, TSymbol>(System.ReadOnlySpan<TSymbol> text, System.ReadOnlySpan<TSymbol> alphabet, TSymbol negativeSymbol, out TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var isNegative = text[0]!.Equals(negativeSymbol);

      TryConvertSymbolsToPositionalNotationIndices(text[(isNegative ? 1 : 0)..], alphabet.AsSpan(), out var indices);

      Quantities.Radix.TryConvertPositionalNotationIndicesToNumber(indices, alphabet.Length, out TSelf value);

      return number = isNegative ? -value : value;
    }

    //#else
    //#endif

    #endregion // Static methods

#if NET7_0_OR_GREATER

    #region Binary strings

    /// <summary>Creates a binary (base 2) text string with <paramref name="minLength"/> and <paramref name="alphabet"/> from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 32 digit string for a 32-bit integer.</remarks>
    public static System.ReadOnlySpan<TSymbol> ToBinaryString<TSelf, TSymbol>(this TSelf value, System.ReadOnlySpan<TSymbol> alphabet, int minLength = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TSymbol : struct
    {
      var list = new System.Collections.Generic.List<TSymbol>();

      for (var bitIndex = value.GetBitLengthEx() - 1; bitIndex >= 0; bitIndex--)
        list.Add(alphabet[int.CreateChecked((value >> bitIndex) & TSelf.One)]);

      while (list.Count < minLength)
        list.Insert(0, alphabet[0]); // Pad with zero element symbol.

      return list.AsSpan();
    }

    /// <summary>Creates a binary (base 2) text string with <paramref name="minLength"/> from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 32 digit string for a 32-bit integer.</remarks>
    public static System.ReadOnlySpan<char> ToBinaryString<TSelf>(this TSelf value, int minLength)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToBinaryString(value, Base64.AsSpan()[..2], minLength);

    /// <summary>Creates a binary (base 2) text string from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 32 digit string for a 32-bit integer.</remarks>
    public static System.ReadOnlySpan<char> ToBinaryString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToBinaryString(value, value.GetBitCount());

    #endregion // Binary strings

    #region Decimal strings

    /// <summary>Creates a decimal (base 10) text string with <paramref name="minLength"/> and <paramref name="alphabet"/> from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 10 digit string for an 32-bit integer.</remarks>
    public static System.ReadOnlySpan<TSymbol> ToDecimalString<TSelf, TSymbol>(this TSelf value, System.ReadOnlySpan<TSymbol> alphabet, TSymbol negativeSymbol, int minLength = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (alphabet.Length < 10) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

      Quantities.Radix.TryConvertNumberToPositionalNotationIndices(value, 10, out var indices);

      while (indices.Count < minLength)
        indices.Insert(0, 0); // Pad left with zeroth element.

      TryConvertPositionalNotationIndicesToSymbols(indices, alphabet, out var symbols);
      //var symbols = ConvertIndicesToSymbols(indices, alphabet.ToArray());

      if (TSelf.IsNegative(value))
        symbols.Insert(0, negativeSymbol);

      return symbols.AsSpan();
    }

    /// <summary>Creates a decimal (base 10) text string with <paramref name="minLength"/> from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 10 digit string for an 32-bit integer.</remarks>
    public static System.ReadOnlySpan<char> ToDecimalString<TSelf>(this TSelf value, int minLength)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToDecimalString(value, Base64.AsSpan()[..10], '\u002D', minLength);

    /// <summary>Creates a decimal (base 10) text string from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 10 digit string for an 32-bit integer.</remarks>
    public static System.ReadOnlySpan<char> ToDecimalString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToDecimalString(value, Quantities.Radix.GetMaxDigitCount(value.GetBitCount(), 10, value.IsSignedNumber()));

    #endregion // Decimal strings

    #region Hexadecimal strings

    /// <summary>Creates a hexadecimal (base 16) text string with <paramref name="minLength"/> and <paramref name="alphabet"/> from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 8 digit string for an 32-bit integer.</remarks>
    public static System.ReadOnlySpan<TSymbol> ToHexadecimalString<TSelf, TSymbol>(this TSelf value, System.ReadOnlySpan<TSymbol> alphabet, int minLength = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (alphabet.Length < 16) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

      Quantities.Radix.TryConvertNumberToPositionalNotationIndices(value, 16, out var indices);

      while (indices.Count < minLength)
        indices.Insert(0, 0); // Pad left with zeroth element.

      TryConvertPositionalNotationIndicesToSymbols(indices, alphabet, out var symbols);

      return symbols.AsSpan();
    }

    /// <summary>Creates a hexadecimal (base 16) text string with <paramref name="minLength"/> from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 8 digit string for an 32-bit integer.</remarks>
    public static System.ReadOnlySpan<char> ToHexadecimalString<TSelf>(this TSelf value, int minLength)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToHexadecimalString(value, Base64.AsSpan()[..16], minLength);

    /// <summary>Creates a hexadecimal (base 16) text string from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 8 digit string for an 32-bit integer.</remarks>
    public static System.ReadOnlySpan<char> ToHexadecimalString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToHexadecimalString(value, Quantities.Radix.GetMaxDigitCount(value.GetBitCount(), 16, value.IsSignedNumber()));

    #endregion // Hexadecimal strings

    #region Octal strings

    /// <summary>Creates an octal (base 8) text string with <paramref name="minLength"/> and <paramref name="alphabet"/> from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 3 digit string for an 8-bit integer.</remarks>
    public static System.ReadOnlySpan<TSymbol> ToOctalString<TSelf, TSymbol>(this TSelf value, System.ReadOnlySpan<TSymbol> alphabet, int minLength = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (alphabet.Length < 8) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

      Quantities.Radix.TryConvertNumberToPositionalNotationIndices(value, 8, out var indices);

      while (indices.Count < minLength)
        indices.Insert(0, 0); // Pad left with zeroth element.

      TryConvertPositionalNotationIndicesToSymbols(indices, alphabet, out var symbols);

      return symbols.AsSpan();
    }

    /// <summary>Creates an octal (base 8) text string with <paramref name="minLength"/> from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 3 digit string for an 8-bit integer.</remarks>
    public static System.ReadOnlySpan<char> ToOctalString<TSelf>(this TSelf value, int minLength)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToOctalString(value, Base64.AsSpan()[..8], minLength);

    /// <summary>Creates an octal (base 8) text string from <paramref name="value"/>.</summary>
    /// <remarks>This function evaluates and returns the most fitting string length, e.g. a 3 digit string for an 8-bit integer.</remarks>
    public static System.ReadOnlySpan<char> ToOctalString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToOctalString(value, Quantities.Radix.GetMaxDigitCount(value.GetBitCount(), 8, value.IsSignedNumber()));

    #endregion // Octal strings

    /// <summary>Creates a base <paramref name="radix"/> text string from <paramref name="value"/>, with an optional <paramref name="minLength"/> of digits in the resulting string (padded with zeroes if needed).</summary>
    /// <remarks>By default, this function returns the shortest possible string length.</remarks>
    public static System.ReadOnlySpan<TSymbol> ToRadixSpan<TSelf, TSymbol>(this TSelf value, System.ReadOnlySpan<TSymbol> alphabet, TSymbol negativeSymbol, int minLength = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TSymbol : struct
    {
      if (alphabet.Length == 2)
        return ToBinaryString(value, alphabet, minLength);
      else if (alphabet.Length == 8)
        return ToOctalString(value, alphabet, minLength);
      else if (alphabet.Length == 10)
        return ToDecimalString(value, alphabet, negativeSymbol, minLength);
      else if (alphabet.Length == 16)
        return ToHexadecimalString(value, alphabet, minLength);

      // Otherwise use generic algorithm.

      Quantities.Radix.TryConvertNumberToPositionalNotationIndices(value, TSelf.CreateChecked(alphabet.Length), out var indices);

      while (indices.Count < minLength)
        indices.Insert(0, 0); // Pad left with zeroth element.

      TryConvertPositionalNotationIndicesToSymbols(indices, alphabet, out var symbols);

      return symbols.AsSpan();
    }

    /// <summary>Creates a base <paramref name="radix"/> text string from <paramref name="value"/>, with an optional <paramref name="minLength"/> of digits in the resulting string (padded with zeroes if needed).</summary>
    /// <remarks>By default, this function returns the shortest possible string length.</remarks>
    public static System.ReadOnlySpan<char> ToRadixString<TSelf>(this TSelf value, int radix, int minLength = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ToRadixSpan(value, Base64.AsSpan()[..radix], '\u002D', minLength);

#else

    //public static string ToBinaryString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());
    //public static string ToBinaryString(this int value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());
    //public static string ToBinaryString(this long value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());

    //public static string ToDecimalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());
    //public static string ToDecimalString(this int value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());
    //public static string ToDecimalString(this long value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());

    //public static string ToHexadecimalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);
    //public static string ToHexadecimalString(this int value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);
    //public static string ToHexadecimalString(this long value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);

    //public static string ToOctalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);
    //public static string ToOctalString(this int value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);
    //public static string ToOctalString(this long value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);

    ///// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    //public static string ToRadixString(this System.Numerics.BigInteger number, int radix, int minLength = 1) => new Text.PositionalNotation(radix).NumberToText(number, minLength).ToString();
    ///// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    //public static string ToRadixString(this int number, int radix, int minLength = 1) => new Text.PositionalNotation(radix).NumberToText(number, minLength).ToString();
    ///// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    //public static string ToRadixString(this long number, int radix, int minLength = 1) => new Text.PositionalNotation(radix).NumberToText(number, minLength).ToString();

#endif
  }

#else


    /// <summary>Convert a number into a positional notation text string.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Positional_notation"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
    public readonly ref struct PositionalNotation
    {
      public const string Base64 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";

      public const int MaxRadix = 64;

      public static PositionalNotation Base2 => new(2);
      public static PositionalNotation Base8 => new(8);
      public static PositionalNotation Base10 => new(10);
      public static PositionalNotation Base16 => new(16);

      public System.Text.Rune NegativeSign { get; init; } = (System.Text.Rune)'-';

      public System.ReadOnlySpan<System.Text.Rune> Symbols { get; }

      /// <summary>Convert a number into a positional notation text string.</summary>
      /// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
      public PositionalNotation(System.ReadOnlySpan<System.Text.Rune> symbols) => Symbols = symbols;
      public PositionalNotation(int radix) : this(Base64.AsSpan().Slice(0, radix).ToListRune().AsSpan()) { }

      /// <summary>Converts a number into a positional notation text string.</summary>
      public string NumberToText(System.Numerics.BigInteger number, int minLength = 1)
      {
        if (number < 0)
          return NegativeSign.ToString() + NumberToText(-number, minLength);

        if (number.IsZero)
          return Symbols[0].ToString();

        var radix = Symbols.Length;

        var sb = new SpanBuilder<System.Text.Rune>();

        while (number > 0)
        {
          number = System.Numerics.BigInteger.DivRem(number, radix, out var remainder);

          sb.Insert(0, Symbols[(int)remainder]);
        }

        if (minLength > sb.Count)
          sb.Insert(0, Symbols[0], minLength - sb.Count);

        return sb.ToString();
      }

      /// <summary>Tries to convert a number into a positional notation text string.</summary>
      public bool TryNumberToText(System.Numerics.BigInteger number, out string result, int minLength = 1)
      {
        try
        {
          result = NumberToText(number, minLength);
          return true;
        }
        catch { }

        result = string.Empty;
        return false;
      }

      /// <summary>Convert a positional notation text string into a number.</summary>
      public System.Numerics.BigInteger TextToNumber(System.ReadOnlySpan<System.Text.Rune> number)
      {
        var isNegative = number[0] == NegativeSign;

        var bi = System.Numerics.BigInteger.Zero;

        for (var index = isNegative ? 1 : 0; index < number.Length; index++)
        {
          bi *= Symbols.Length;

          bi += MemoryExtensions.IndexOf(Symbols, number[index]) is var position && position >= 0 ? position : throw new System.InvalidOperationException();
        }

        return isNegative ? -bi : bi;
      }
      /// <summary>Convert a positional notation text string into a number.</summary>
      public System.Numerics.BigInteger TextToNumber(System.ReadOnlySpan<char> number) => TextToNumber(number.ToListRune().AsSpan().AsReadOnlySpan());

      /// <summary>Convert a positional notation text string into a number.</summary>
      public bool TryTextToNumber(System.ReadOnlySpan<System.Text.Rune> number, out System.Numerics.BigInteger result)
      {
        try
        {
          result = TextToNumber(number);
          return true;
        }
        catch { }

        result = default;
        return false;
      }
      /// <summary>Convert a positional notation text string into a number.</summary>
      public bool TryTextToNumber(System.ReadOnlySpan<char> number, out System.Numerics.BigInteger result) => TryTextToNumber(number.ToListRune().AsSpan().AsReadOnlySpan(), out result);

      public static System.Collections.Generic.Dictionary<int, string> ToStringRadices(System.Numerics.BigInteger number)
      {
        var dictionary = new System.Collections.Generic.Dictionary<int, string>();
        for (var radix = 2; radix <= MaxRadix; radix++)
          dictionary.Add(radix, new PositionalNotation(radix).NumberToText(number).ToString());
        return dictionary;
      }
    }

#endif
}
