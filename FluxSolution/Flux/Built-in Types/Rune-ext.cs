namespace Flux
{
  public static partial class RuneExtensions
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Prefix>&#x?)(?<Number>[0-9A-Fa-f]{1,8})(?<Suffix>;)", System.Text.RegularExpressions.RegexOptions.Compiled)]
    public static partial System.Text.RegularExpressions.Regex RegexParseNumericCharacterReferences();

    [System.Text.RegularExpressions.GeneratedRegex(@"((?<Prefix>\\u)(?<Number>[0-9A-Fa-f]{4})|(?<Prefix>\\U)(?<Number>[0-9A-Fa-f]{8})|(?<Prefix>\\x)(?<Number>[0-9A-Fa-f]{1,8}))", System.Text.RegularExpressions.RegexOptions.Compiled)]
    public static partial System.Text.RegularExpressions.Regex RegexParseCsUnicodeLiterals();

    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Prefix>U\+)(?<Codepoint>[0-9A-Fa-f]{4,6})", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex RegexParseUnicodeUnotations();

    extension(System.Text.Rune)
    {
      public static bool IsRepresentableAsSingleChar(int value)
        => value <= 0xFFFF && System.Text.Rune.IsValid(value);

      #region AssertValid

      public static int AssertValid(int value, string? paramName = "value")
        => System.Text.Rune.IsValid(value)
        ? value
        : throw new System.ArgumentException($"Invalid codepoint value.", paramName);

      #endregion

      #region ..BasicLatinLetterY

      /// <summary>
      /// <para>Indicates whether the character is the letter 'Y' or 'y', i.e. ignore case.</para>
      /// </summary>
      /// <param name="rune">The <see cref="System.Text.Rune"/> to check.</param>
      /// <returns></returns>
      /// <remarks>Provided for consistent check against consonants and vowels in English.</remarks>
      public static bool IsBasicLatinLetterY(System.Text.Rune rune)
        => rune.Value is 'y' or 'Y';

      #endregion

      #region ..Consonant

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Text.Rune"/> is a consonant in the specified <paramref name="culture"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">If null, then <see cref="System.Globalization.CultureInfo.CurrentCulture"/></param>
      /// <returns></returns>
      public static bool IsConsonant(System.Text.Rune rune, System.Globalization.CultureInfo culture)
        => System.Text.Rune.IsLetter(rune) && culture.GetConsonantsAsRunes().Contains(rune);

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Text.Rune"/> is a consonant in the current culture.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool IsConsonant(System.Text.Rune rune)
        => IsConsonant(rune, System.Globalization.CultureInfo.CurrentCulture);

      #endregion

      #region ..ControlPicture

      /// <summary>
      /// <para>Control Pictures is a Unicode block containing characters for graphically representing the C0 control codes, and other control characters.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Control_Pictures"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Control_character"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/C0_and_C1_control_codes"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Space_(punctuation)"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Whitespace_character"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Newline"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Delete_character"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Substitute_character"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Delete_character"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Specials_(Unicode_block)#Replacement_character"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static System.Text.Rune GetControlPicture(System.Text.Rune value)
        => (System.Text.Rune)System.Char.GetControlPicture((char)value.Value);

      /// <summary>
      /// <para>Control Pictures is a Unicode block containing characters for graphically representing the C0 control codes, and other control characters.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Control_Pictures"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Control_character"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/C0_and_C1_control_codes"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Space_(punctuation)"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Whitespace_character"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Newline"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Delete_character"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Substitute_character"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Delete_character"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Specials_(Unicode_block)#Replacement_character"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="picture"></param>
      /// <returns></returns>
      public static bool TryGetControlPicture(System.Text.Rune value, out System.Text.Rune picture)
      {
        try { picture = GetControlPicture(value); }
        catch { picture = value; }

        return picture != value;
      }

      #endregion

      #region FoldToAscii

      /// <summary>
      /// <para>Folds runes representing characters above ASCII as a reasonable ASCII equivalent. Only characters from certain blocks are converted.</para>
      /// </summary>
      public static string FoldToAscii(System.Text.Rune value)
        => System.Char.FoldToAscii((char)value.Value);

      #endregion

      /// <summary>
      /// <para>Locates the Unicode range and block name of the <paramref name="rune"/>.</para>
      /// </summary>
      public static System.Collections.Generic.KeyValuePair<string, System.Text.Unicode.UnicodeRange> GetUnicodeRange(System.Text.Rune rune)
        => System.Text.Unicode.UnicodeRange.GetUnicodeRanges().InfimumSupremum(t2 => t2.Value.FirstCodePoint, rune.Value, false).InfimumItem;

      #region ..LatinStroke..

      /// <summary>
      /// <para>Indicates whether a rune is a latin diacritical stroke.</para>
      /// </summary>
      /// <param name="rune">The rune to evaluate.</param>
      /// <returns></returns>
      public static bool IsLatinStroke(System.Text.Rune rune)
        => IsRepresentableAsSingleChar(rune.Value) && char.IsLatinStroke((char)rune.Value);

      /// <summary>
      /// <para>Replaces a latin stroke letter with a plain letter, i.e. a letter without a diacritic is returned in its place. Characters that are not latin stroke letters are returned as-is.</para>
      /// </summary>
      /// <param name="rune">The rune to evaluate.</param>
      /// <returns></returns>
      public static System.Text.Rune GetLatinStrokeReplacement(System.Text.Rune rune)
        => IsRepresentableAsSingleChar(rune.Value) ? new(char.GetLatinStrokeReplacement((char)rune.Value)) : rune;

      /// <summary>
      /// <para>Attempts to replace a latin stroke letter with a plain letter and indicates whether a replacement was made.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <param name="replacementCharacter"></param>
      /// <returns></returns>
      public static bool TryGetLatinStrokeReplacement(System.Text.Rune rune, out System.Text.Rune replacementRune)
        => rune != (replacementRune = GetLatinStrokeReplacement(rune));

      #endregion

      #region ParseCsUnicodeLiterals

      public static System.Collections.Generic.List<(System.Range, System.Text.Rune)> ParseCsUnicodeLiterals(System.ReadOnlySpan<char> source)
      {
        var list = new System.Collections.Generic.List<(System.Range, System.Text.Rune)>();

        var evm = RegexParseCsUnicodeLiterals().EnumerateMatches(source);

        foreach (var vm in evm)
        {
          var index = vm.Index;
          var length = vm.Length;

          list.Add((System.Range.FromOffsetAndLength(index, length), new System.Text.Rune(int.Parse(source.Slice(index + 2, length - 2), System.Globalization.NumberStyles.HexNumber))));
        }

        return list;
      }

      #endregion

      #region ParseMlNumericCharacterReferences

      public static System.Collections.Generic.List<(System.Range, System.Text.Rune)> ParseMlNumericCharacterReferences(System.ReadOnlySpan<char> source)
      {
        var list = new System.Collections.Generic.List<(System.Range, System.Text.Rune)>();

        var evm = RegexParseNumericCharacterReferences().EnumerateMatches(source);

        foreach (var vm in evm)
        {
          var index = vm.Index;
          var length = vm.Length;

          var range = System.Range.FromOffsetAndLength(index, length);

          var value = (source[index + 2] == 'x')
            ? int.Parse(source.Slice(index + 3, length - 4), System.Globalization.NumberStyles.HexNumber)
            : int.Parse(source.Slice(index + 2, length - 3), System.Globalization.NumberStyles.Integer);

          list.Add((range, new System.Text.Rune(value)));
        }

        return list;
      }

      #endregion

      #region ParseUnicodeUnotations

      public static System.Collections.Generic.List<(System.Range Range, System.Text.Rune Rune)> ParseUnicodeUnotations(System.ReadOnlySpan<char> source)
      {
        var list = new System.Collections.Generic.List<(System.Range, System.Text.Rune)>();

        var evm = RegexParseUnicodeUnotations().EnumerateMatches(source);

        foreach (var vm in evm)
        {
          var index = vm.Index;
          var length = vm.Length;

          list.Add((System.Range.FromOffsetAndLength(index, length), new System.Text.Rune(int.Parse(source.Slice(index + 2, length - 2), System.Globalization.NumberStyles.HexNumber))));
        }

        return list;
      }

      #endregion

      #region ..Printable..

      /// <summary>
      /// <para>The set of Unicode character categories containing non-rendering, unknown, or incomplete characters.</para>
      /// <para>Unicode.Format and Unicode.PrivateUse can NOT be included in this set, because they may (private-use) or do (format) contain at least *some* rendering characters.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsNonRenderingCategory(System.Text.Rune rune, bool nonRenderCategoryFormat = false, bool nonRenderCategoryPrivateUse = false)
        => System.Text.Rune.GetUnicodeCategory(rune) is var uc
        && uc is System.Globalization.UnicodeCategory.Control or System.Globalization.UnicodeCategory.OtherNotAssigned or System.Globalization.UnicodeCategory.Surrogate
        || (nonRenderCategoryFormat && uc == System.Globalization.UnicodeCategory.Format)
        || (nonRenderCategoryPrivateUse && uc == System.Globalization.UnicodeCategory.PrivateUse);

      /// <summary>
      /// <para>Returns whether a character is a printable from the ASCII table, i.e. any character from U+0020 (32 = ' ' space) to U+007E (126 = '~' tilde), both inclusive.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsPrintableAscii(System.Text.Rune rune)
        => rune.Value is >= '\u0020' and <= '\u007E';

      /// <summary>
      /// <para>Returns whether a rune (Unicode codepoint) is a "printable" character. There is no simple solution, so please read the entire blurb of this method.</para>
      /// <para>The following code assumes that all characters in Unicode categories PrivateUse and Format are printable, which means that at least some characters will be misclassified.</para>
      /// <para><see href="https://stackoverflow.com/a/45928048/3178666"/></para>
      /// <para>IsWhiteSpace() includes the whitespace characters that are categorized as control characters. Any other character is printable, unless it falls into the non-rendering categories.</para>
      /// <para>Exclude Unicode categories containing non-rendering, unknown, or incomplete characters. Unicode categories Format and PrivateUse can NOT be included in this set, because they may (private-use) or do (format) contain at least *some* rendering characters.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsPrintableUnicode(System.Text.Rune value)
        => System.Text.Rune.IsWhiteSpace(value) || !IsNonRenderingCategory(value);

      #endregion

      #region ..Subscript

      /// <summary>
      /// <para>Indicates whether a specified rune is a subscript rune.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool IsSubscript(System.Text.Rune rune)
        => IsRepresentableAsSingleChar(rune.Value) && char.IsSubscript((char)rune.Value);

      /// <summary>
      /// <para>Indicates whether a rune is a subscript equivalent rune.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool IsSubscriptEquivalent(System.Text.Rune rune)
        => IsRepresentableAsSingleChar(rune.Value) && char.IsSubscriptEquivalent((char)rune.Value);

      /// <summary>
      /// <para>Converts a rune to its subscript equivalent, if possible, otherwise the input rune is returned.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static System.Text.Rune ConvertToSubscript(System.Text.Rune rune)
        => IsRepresentableAsSingleChar(rune.Value) ? new(char.ConvertToSubscript((char)rune.Value)) : rune;

      /// <summary>
      /// <para>Attemps to convert a rune to subscript, if possible, otherwise the input rune is returned as an out parameter, and returns an indication whether a conversion was made.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <param name="subscriptRune"></param>
      /// <returns></returns>
      public static bool TryConvertToSubscript(System.Text.Rune rune, out System.Text.Rune subscriptRune)
        => rune != (subscriptRune = ConvertToSubscript(rune));

      #endregion

      #region ..Superscript

      /// <summary>
      /// <para>Indicates whether a rune is a superscript rune.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool IsSuperscript(System.Text.Rune rune)
        => IsRepresentableAsSingleChar(rune.Value) && char.IsSuperscript((char)rune.Value);

      /// <summary>
      /// <para>Indicates whether a rune is a superscript equivalent rune.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool IsSuperscriptEquivalent(System.Text.Rune rune)
        => IsRepresentableAsSingleChar(rune.Value) && char.IsSuperscriptEquivalent((char)rune.Value);

      /// <summary>
      /// <para>Converts a rune to its superscript equivalent, if possible, otherwise the input rune is returned.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static System.Text.Rune ConvertToSuperscript(System.Text.Rune rune)
        => IsRepresentableAsSingleChar(rune.Value) ? new(char.ConvertToSuperscript((char)rune.Value)) : rune;

      /// <summary>
      /// <para>Attemps to convert a rune to superscript, if possible, otherwise the specified rune is returned as an out parameter.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <param name="superscriptRune"></param>
      /// <returns>An indication whether a conversion was made.</returns>
      public static bool TryConvertToSuperscript(System.Text.Rune rune, out System.Text.Rune superscriptRune)
        => rune != (superscriptRune = ConvertToSuperscript(rune));

      #endregion

      #region ..Vowel

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Text.Rune"/> is a vowel in the specified <paramref name="cultureInfo"/>.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <param name="cultureInfo">The culture to use.</param>
      /// <returns></returns>
      public static bool IsVowel(System.Text.Rune rune, System.Globalization.CultureInfo cultureInfo)
        => System.Text.Rune.IsLetter(rune) && cultureInfo.GetVowelsAsRunes().Contains(rune);

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Text.Rune"/> is a vowel in the current culture.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static bool IsVowel(System.Text.Rune rune)
        => IsVowel(rune, System.Globalization.CultureInfo.CurrentCulture);

      #endregion
    }

    extension(System.Text.Rune source)
    {
      #region ToCsharpUtf16LiteralString

      public string ToCsharpUtf16LiteralString()
      {
        System.Span<char> utf16 = stackalloc char[source.Utf16SequenceLength];
        source.EncodeToUtf16(utf16);

        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < utf16.Length; i++)
          sb.Append(utf16[i].ToCsharpUtf16LiteralString());
        return sb.ToString();
      }

      public string ToCsharpUtf32LiteralString()
        => $"\\U{source.Value:X8}";

      #endregion

      #region ToCsharpVariableHexLiteralString

      public string ToCsharpVariableHexLiteralString()
        => $"\\x{source.Value:X1}";

      #endregion

      #region ToDecimalNumericCharacterReferenceString

      /// <summary>
      /// <para>a numeric character reference refers to a character by its Universal Coded Character Set/Unicode code point, and uses the format: <code>&amp;#nnnn;</code> where nnnn is the code point in decimal form.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public string ToDecimalNumericCharacterReferenceString()
        => $"&#{source.Value};";

      #endregion

      #region ToHexadecimalNumericCharacterReferenceString

      /// <summary>
      /// <para>a numeric character reference refers to a character by its Universal Coded Character Set/Unicode code point, and uses the format: <code>&amp;#xhhhh;</code> where the x must be lowercase in XML documents, hhhh is the code point in hexadecimal form.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public string ToHexadecimalNumericCharacterReferenceString()
        => $"&#x{source.Value:X1};";

      #endregion

      #region GetUtfSequences

      /// <summary>Generate a variable number (from 1 to 2) of words (16 bits as ints) representing UTF16 encoding of the <see cref="System.Text.Rune"/>.</summary>
      public char[] GetUtf16Sequence()
      {
        var chars = new char[source.Utf16SequenceLength];
        source.EncodeToUtf16(chars);
        return chars;

        //var value = source.Value;

        //if (value >= 0x010000 && value < 0x10FFFF && (value - 0x010000) is var bits20)
        //  return new int[] { BitsLs(bits20), BitsHs(bits20 >> 10) };
        //if ((value >= 0x0000 && value <= 0xD7FF) || (value >= 0xE000 && value <= 0xFFFF))
        //  return new int[] { Bits16(value) };

        //throw new System.ArgumentOutOfRangeException(nameof(source));

        //static ushort BitsHs(int value)
        //  => (ushort)((value & 0x03FF) + 0xD800);
        //static ushort BitsLs(int value)
        //  => (ushort)((value & 0x03FF) + 0xDC00);
        //static ushort Bits16(int value)
        //  => (ushort)(value & 0xFFFF);
      }

      /// <summary>Generate a variable number (from 1 to 4) of bytes representing UTF8 encoding of the <see cref="System.Text.Rune"/>.</summary>
      public byte[] GetUtf8Sequence()
      {
        var bytes = new byte[source.Utf8SequenceLength];
        source.EncodeToUtf8(bytes);
        return bytes;

        //var value = source.Value;

        //if (value >= 0x10000 && value <= 0x1FFFFF)
        //  return new byte[] { Bits6(value), Bits6(value >> 6), Bits6(value >> 12), Bits3(value >> 18) };
        //if (value >= 0x0800 && value <= 0xFFFF)
        //  return new byte[] { Bits6(value), Bits6(value >> 6), Bits4(value >> 12) };
        //if (value >= 0x0080 && value <= 0x07FF)
        //  return new byte[] { Bits6(value), Bits5(value >> 6) };
        //if (value >= 0x0000 && value <= 0x007F)
        //  return new byte[] { Bits7(value) };

        //throw new System.ArgumentOutOfRangeException(nameof(source));

        //static byte Bits3(int value)
        //  => (byte)(0b11110000 | (value & 0b00000111));
        //static byte Bits4(int value)
        //  => (byte)(0b11100000 | (value & 0b00001111));
        //static byte Bits5(int value)
        //  => (byte)(0b11000000 | (value & 0b00011111));
        //static byte Bits6(int value)
        //  => (byte)(0b10000000 | (value & 0b00111111));
        //static byte Bits7(int value)
        //  => (byte)(value & 0b01111111);
      }

      #endregion

      #region ToUnicodeUnotationString

      /// <summary>
      /// <para>Convert the Unicode codepoint (rune) to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</para>
      /// </summary>
      /// <returns></returns>
      public string ToUnicodeUnotationString()
        => $"U+{source.Value:X4}";

      #endregion

      #region ToVerboseString

      public string ToVerboseString()
        => $"{ToUnicodeUnotationString(source)} '{source}' {System.Text.Rune.GetUnicodeCategory(source)}";

      #endregion
    }
  }
}
