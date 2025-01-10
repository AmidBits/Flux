namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Converts a <paramref name="value"/> to <paramref name="positionalNotationIndices"/> based on <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="positionalNotationIndices"></param>
    /// <returns></returns>
    public static bool TryConvertNumberToPositionalNotationIndices<TNumber, TRadix>(this TNumber value, TRadix radix, out System.Collections.Generic.List<int> positionalNotationIndices)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      positionalNotationIndices = new();

      try
      {
        var rdx = TNumber.CreateChecked(Quantities.Radix.AssertMember(radix));

        while (!TNumber.IsZero(value))
        {
          (value, var remainder) = TNumber.DivRem(value, rdx);

          positionalNotationIndices.Insert(0, int.CreateChecked(remainder));
        }

        return true;
      }
      catch { }

      return false;
    }

    /// <summary>
    /// <para>Converts <paramref name="positionalNotationIndices"/> to a <paramref name="value"/> based on <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TRadix"></typeparam>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="positionalNotationIndices"></param>
    /// <param name="radix"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool TryConvertPositionalNotationIndicesToNumber<TRadix, TNumber>(this System.Collections.Generic.List<int> positionalNotationIndices, TRadix radix, out TNumber value)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      value = TNumber.Zero;

      try
      {
        var rdx = TNumber.CreateChecked(Quantities.Radix.AssertMember(radix));

        for (var index = 0; index < positionalNotationIndices.Count; index++)
        {
          value *= rdx;

          value += TNumber.CreateChecked(positionalNotationIndices[index]);
        }

        return true;
      }
      catch { }

      return false;
    }

    /// <summary>
    /// <para>Converts <paramref name="positionalNotationIndices"/> to <paramref name="symbols"/> using the specified <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="positionalNotationIndices"></param>
    /// <param name="alphabet"></param>
    /// <param name="symbols"></param>
    /// <returns></returns>
    public static bool TryTransposePositionalNotationIndicesToSymbols<TSymbol>(this System.Collections.Generic.List<int> positionalNotationIndices, System.ReadOnlySpan<TSymbol> alphabet, out System.Collections.Generic.List<TSymbol> symbols)
    {
      try
      {
        symbols = new System.Collections.Generic.List<TSymbol>(positionalNotationIndices.Count);

        for (var i = 0; i < positionalNotationIndices.Count; i++)
          symbols.Add(alphabet[positionalNotationIndices[i]]);

        return true;
      }
      catch { }

      symbols = new();
      return false;
    }

    /// <summary>
    /// <para>Converts <paramref name="symbols"/> to <paramref name="positionalNotationIndices"/> using the specified <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="symbols"></param>
    /// <param name="alphabet"></param>
    /// <param name="positionalNotationIndices"></param>
    /// <returns></returns>
    public static bool TryTransposeSymbolsToPositionalNotationIndices<TSymbol>(this System.Collections.Generic.List<TSymbol> symbols, System.ReadOnlySpan<TSymbol> alphabet, out System.Collections.Generic.List<int> positionalNotationIndices)
    {
      try
      {
        positionalNotationIndices = new(symbols.Count);

        for (var i = 0; i < symbols.Count; i++)
          positionalNotationIndices[i] = alphabet.IndexOf(symbols[i]);

        return true;
      }
      catch { }

      positionalNotationIndices = new();
      return false;
    }

    public static readonly char[] Base64Alphabet = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '+', '/' };

    /// <summary>
    /// <para>Converts a <paramref name="value"/> to a binary (base 2) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minLength"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.ReadOnlySpan<char> ToBinaryString<TNumber>(this TNumber value, int minLength = 1, char[]? alphabet = null)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (minLength <= 0) minLength = value.GetBitCount();

      alphabet ??= Base64Alphabet;

      if (alphabet.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

      var abs = TNumber.Abs(value);

      var indices = new System.Collections.Generic.List<int>();

      for (var bitIndex = int.Min(int.Max(abs.GetBitLength(), minLength), abs.GetBitCount()) - 1; bitIndex >= 0; bitIndex--)
      {
        var bitValue = int.CreateChecked((abs >>> bitIndex) & TNumber.One);

        if (bitValue > 0 || indices.Count > 0 || bitIndex < minLength)
          indices.Add(bitValue);
      }

      TryTransposePositionalNotationIndicesToSymbols(indices, alphabet, out System.Collections.Generic.List<char> symbols);

      return symbols.AsSpan();
    }

    /// <summary>
    /// <para>Converts a <paramref name="value"/> to a decimal (base 10) string based on <paramref name="minLength"/>, <paramref name="negativeSymbol"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minLength"></param>
    /// <param name="negativeSymbol"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.ReadOnlySpan<char> ToDecimalString<TNumber>(this TNumber value, int minLength = 1, char negativeSymbol = '\u002D', char[]? alphabet = null)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (minLength <= 0) minLength = value.GetBitCount().MaxDigitCountOfBitLength(10, value.IsSignedNumber());

      alphabet ??= Base64Alphabet;

      if (alphabet.Length < 10) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

      var abs = TNumber.Abs(value);

      abs.TryConvertNumberToPositionalNotationIndices(10, out var indices);

      while (indices.Count < minLength)
        indices.Insert(0, 0); // Pad left with zeroth element.

      TryTransposePositionalNotationIndicesToSymbols(indices, alphabet, out System.Collections.Generic.List<char> symbols);

      if (TNumber.IsNegative(value))
        symbols.Insert(0, negativeSymbol); // If the value is negative AND base-2 (radix) is 10 (decimal)...

      return symbols.AsSpan();
    }

    /// <summary>
    /// <para>Converts a <paramref name="value"/> to a hexadecimal (base 16) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minLength"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.ReadOnlySpan<char> ToHexadecimalString<TNumber>(this TNumber value, int minLength = 1, char[]? alphabet = null)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (minLength <= 0) minLength = value.GetBitCount().MaxDigitCountOfBitLength(16, value.IsSignedNumber());

      alphabet ??= Base64Alphabet;

      if (alphabet.Length < 16) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

      var indices = new System.Collections.Generic.List<int>();

      for (var nibbleIndex = (value.GetByteCount() << 1) - 1; nibbleIndex >= 0; nibbleIndex--)
      {
        var nibbleValue = int.CreateChecked((value >>> (nibbleIndex << 2)) & TNumber.CreateChecked(0xF));

        if (nibbleValue > 0 || indices.Count > 0 || nibbleIndex < minLength)
          indices.Add(nibbleValue);
      }

      TryTransposePositionalNotationIndicesToSymbols(indices, alphabet, out System.Collections.Generic.List<char> symbols);

      return symbols.AsSpan();
    }

    /// <summary>
    /// <para>Converts a <paramref name="value"/> to a octal (base 8) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minLength"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.ReadOnlySpan<char> ToOctalString<TNumber>(this TNumber value, int minLength = 1, char[]? alphabet = null)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (minLength <= 0) minLength = value.GetBitCount().MaxDigitCountOfBitLength(8, value.IsSignedNumber());

      alphabet ??= Base64Alphabet;

      if (alphabet.Length < 8) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

      value.TryConvertNumberToPositionalNotationIndices(8, out var indices);

      while (indices.Count < minLength)
        indices.Insert(0, 0); // Pad left with zeroth element.

      TryTransposePositionalNotationIndicesToSymbols(indices, alphabet, out System.Collections.Generic.List<char> symbols);

      return symbols.AsSpan();
    }

    /// <summary>
    /// <para>Converts a <paramref name="value"/> to text based on <paramref name="radix"/>, <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="minLength"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.ReadOnlySpan<char> ToRadixString<TNumber, TRadix>(this TNumber value, TRadix radix, int minLength = 1, char[]? alphabet = null)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = int.CreateChecked(Quantities.Radix.AssertMember(radix));

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
        if (minLength <= 0) minLength = value.GetBitCount().MaxDigitCountOfBitLength(rdx, value.IsSignedNumber());

        alphabet ??= Base64Alphabet;

        if (alphabet.Length < rdx) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        value.TryConvertNumberToPositionalNotationIndices(radix, out var indices);

        while (indices.Count < minLength)
          indices.Insert(0, 0); // Pad left with zeroth element.

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan();
      }
    }

    public static readonly char[] SubscriptNumerals = new char[] { '\u2080', '\u2081', '\u2082', '\u2083', '\u2084', '\u2085', '\u2086', '\u2087', '\u2088', '\u2089' };

    /// <summary>
    /// <para>Converts a <paramref name="value"/> to subscript text using <paramref name="radix"/> (base) and a <paramref name="minLength"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="minLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<char> ToSubscriptString<TValue, TRadix>(this TValue value, TRadix radix, int minLength = 1)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value.ToRadixString(Quantities.Radix.AssertMember(radix, TRadix.CreateChecked(10)), minLength, SubscriptNumerals); // Extra top-limit to radix (only 10 characters in subscript alphabet).

    public static readonly char[] SuperscriptNumerals = new char[] { '\u2070', '\u00B9', '\u00B2', '\u00B3', '\u2074', '\u2075', '\u2076', '\u2077', '\u2078', '\u2079' };

    /// <summary>
    /// <para>Converts a <paramref name="value"/> to superscript text using <paramref name="radix"/> (base) and a <paramref name="minLength"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="minLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<char> ToSuperscriptString<TSelf, TRadix>(this TSelf value, TRadix radix, int minLength = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value.ToRadixString(Quantities.Radix.AssertMember(radix, TRadix.CreateChecked(10)), minLength, SuperscriptNumerals); // Extra top-limit to radix (only 10 characters in superscript alphabet).
  }
}
