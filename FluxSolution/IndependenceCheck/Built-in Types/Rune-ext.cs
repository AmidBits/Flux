namespace Flux
{
  public static partial class RuneExtensions
  {
    extension(System.Text.Rune)
    {
      /// <summary>
      /// <para>The longest UTF-8 encoded byte sequence.</para>
      /// </summary>
      public static int Utf8MaxEncodedSequenceLength => 4;

      /// <summary>
      /// <para>The URI percent-encoded (%xx) octet length.</para>
      /// </summary>
      public static int UriPercentEncodedOctetLength => 3;

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

      #region GetUnicodeRange

      /// <summary>
      /// <para>Locates the Unicode range and block name of the <paramref name="rune"/>.</para>
      /// </summary>
      public static System.Collections.Generic.KeyValuePair<string, System.Text.Unicode.UnicodeRange> GetUnicodeRange(System.Text.Rune rune)
        => System.Text.Unicode.UnicodeRange.GetUnicodeRanges().InfimumSupremum(t2 => t2.Value.FirstCodePoint, rune.Value, false).InfimumItem;

      #endregion

      #region IsUriReserved

      /// <summary>
      /// <para>URIs include components and subcomponents that are delimited by characters in the "reserved" set. These characters are called "reserved" because they may(or may not) be defined as delimiters by the generic syntax, by each scheme-specific syntax, or by the implementation-specific syntax of a URI's dereferencing algorithm.</para>
      /// <para>If data for a URI component would conflict with a reserved character's purpose as a delimiter, then the conflicting data must be percent-encoded before the URI is formed.</para>
      /// <para>reserved = <c>gen-delims</c> and <c>sub-delims</c></para>
      /// <para>gen-delims = "<c>:/?#[]@</c>"</para>
      /// <para>sub-delims = "<c>!$&amp;&apos;()*+,;=</c>"</para>
      /// <para>The purpose of reserved characters is to provide a set of delimiting characters that are distinguishable from other data within a URI.</para>
      /// <para>URIs that differ in the replacement of a reserved character with its corresponding percent-encoded octet are not equivalent. Percent-encoding a reserved character, or decoding a percent-encoded octet that corresponds to a reserved character, will change how the URI is interpreted by most applications.Thus, characters in the reserved set are protected from normalization and are therefore safe to be used by scheme-specific and producer-specific algorithms for delimiting data subcomponents within a URI.</para>
      /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsUriReserved(System.Text.Rune source)
        => source.IsAscii && char.IsUriReserved((char)source.Value);

      #endregion

      #region IsUriUnreserved

      /// <summary>
      /// <para>Characters that are allowed in a URI but do not have a reserved purpose are called unreserved. These include uppercase and lowercase letters, decimal digits, hyphen, period, underscore, and tilde.</para>
      /// <para>unreserved = <c>ALPHA</c>, <c>DIGIT</c> or "<c>-._~</c>"</para>
      /// <para>URIs that differ in the replacement of an unreserved character with its corresponding percent-encoded US-ASCII octet are equivalent: they identify the same resource. However, URI comparison implementations not always perform normalization prior to comparison (see Section 6).</para>
      /// <para>For consistency, percent-encoded octets in the ranges of ALPHA (%41-%5A and %61-%7A), DIGIT (%30-%39), hyphen (%2D), period (%2E), underscore (%5F), or tilde (%7E) should not be created by URI producers and, when found in a URI, should be decoded to their corresponding unreserved characters by URI normalizers.</para>
      /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsUriUnreserved(System.Text.Rune source)
        => source.IsAscii && char.IsUriUnreserved((char)source.Value);

      #endregion

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

      #region ToCsharpUtf16Literal

      /// <summary>
      /// <para>Creates a new string with a rune represented as <code>\u0000</code> (a C# UTF-16 literal string).</para>
      /// </summary>
      /// <returns></returns>
      public static string ToCsharpUtf16Literal(System.Text.Rune rune)
      {
        System.Span<char> utf16 = stackalloc char[rune.Utf16SequenceLength];
        rune.EncodeToUtf16(utf16);

        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < utf16.Length; i++)
          sb.Append($"\\u{rune.Value:X4}");
        return sb.ToString();
      }

      #endregion

      #region ToCsharpUtf32Literal

      /// <summary>
      /// <para>Creates a new string with a rune represented as <code>\U00000000</code> (a C# UTF-32 literal string).</para>
      /// </summary>
      /// <returns></returns>
      public static string ToCsharpUtf32Literal(System.Text.Rune rune)
        => $"\\U{rune.Value:X8}";

      #endregion

      #region ToCsharpVariableHexLiteral

      /// <summary>
      /// <para>Creates a new string with a rune represented as <code>\x000</code> (a C# variable hex literal string).</para>
      /// </summary>
      /// <returns></returns>
      public static string ToCsharpVariableHexLiteral(System.Text.Rune rune)
        => $"\\x{rune.Value:X1}";

      #endregion

      #region ToDecimalNumericCharacterReference

      /// <summary>
      /// <para>A decimal numeric character reference refers to a character by its Universal Coded Character Set/Unicode code point, and uses the format: <code>&amp;#nnnn;</code> where nnnn is the code point in decimal form.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Numeric_character_reference"/></para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static string ToDecimalNumericCharacterReference(System.Text.Rune rune)
        => $"&#{rune.Value};";

      #endregion

      #region ToHexadecimalNumericCharacterReference

      /// <summary>
      /// <para>A hexadecimal numeric character reference refers to a character by its Universal Coded Character Set/Unicode code point, and uses the format: <code>&amp;#xhhhh;</code> where the x must be lowercase in XML documents, hhhh is the code point in hexadecimal form.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Numeric_character_reference"/></para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static string ToHexadecimalNumericCharacterReference(System.Text.Rune rune)
        => $"&#x{rune.Value:X1};";

      #endregion

      #region ToUnicodeUnotation

      /// <summary>
      /// <para>Creates a new string of a rune (Unicode codepoint) in Unicode U-notation format: <code>U+XXXX</code> (at least 4 hex characters, but more if needed).</para>
      /// </summary>
      /// <returns></returns>
      public static string ToUnicodeUnotation(System.Text.Rune rune)
        => $"U+{rune.Value:X4}";

      #endregion

      #region ToUriPercentEncoding

      /// <summary>
      /// <para>Creates a new string of a rune URI percent-encoded: <code>%E2%88%86</code> (where the encoding can be from one up to three "percent-codes").</para>
      /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static string ToUriPercentEncoding(System.Text.Rune rune)
      {
        if (rune.Value is var utf32 && utf32 <= 0x7F)
          return byte.ToUriPercentEncoding((byte)utf32);

        System.Span<byte> bytes = stackalloc byte[System.Text.Rune.Utf8MaxEncodedSequenceLength];
        System.Span<char> chars = stackalloc char[System.Text.Rune.Utf8MaxEncodedSequenceLength * System.Text.Rune.UriPercentEncodedOctetLength];

        var length = rune.EncodeToUtf8(bytes);

        for (var index = 0; index < length; index++)
          byte.ToUriPercentEncoding((byte)bytes[index]).CopyTo(chars.Slice(index * System.Text.Rune.UriPercentEncodedOctetLength, System.Text.Rune.UriPercentEncodedOctetLength));

        return chars[..(length * System.Text.Rune.UriPercentEncodedOctetLength)].ToString();
      }

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

      #region ToVerboseString

      public string ToVerboseString()
        => $"{ToUnicodeUnotation(source)} '{source}' {System.Text.Rune.GetUnicodeCategory(source)}";

      #endregion
    }
  }
}
