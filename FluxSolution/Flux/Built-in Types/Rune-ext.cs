namespace Flux
{
  public static partial class RuneExtensions
  {
    extension(System.Text.Rune)
    {
      #region Control Picture

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
      /// <para>Indicates whether the character is the letter 'Y' or 'y', i.e. ignore case.</para>
      /// </summary>
      /// <param name="rune">The <see cref="System.Text.Rune"/> to check.</param>
      /// <returns></returns>
      /// <remarks>Provided for consistent check against consonants and vowels in English.</remarks>
      public static bool IsBasicLatinLetterY(System.Text.Rune rune)
        => rune.Value is 'y' or 'Y';

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Text.Rune"/> is a consonant in the specified <paramref name="culture"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">If null, then <see cref="System.Globalization.CultureInfo.CurrentCulture"/></param>
      /// <returns></returns>
      public static bool IsConsonant(System.Text.Rune source, System.Globalization.CultureInfo? culture = null)
        => char.IsConsonant((char)source.Value, culture);

      #region IsPrintable

      /// <summary>
      /// <para>The set of Unicode character categories containing non-rendering, unknown, or incomplete characters.</para>
      /// <para>Unicode.Format and Unicode.PrivateUse can NOT be included in this set, because they may (private-use) or do (format) contain at least *some* rendering characters.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsNonRenderingCategory(System.Text.Rune value, bool nonRenderCategoryFormat = false, bool nonRenderCategoryPrivateUse = false)
        => System.Text.Rune.GetUnicodeCategory(value) is var uc
        && uc is System.Globalization.UnicodeCategory.Control or System.Globalization.UnicodeCategory.OtherNotAssigned or System.Globalization.UnicodeCategory.Surrogate
        || (nonRenderCategoryFormat && uc == System.Globalization.UnicodeCategory.Format)
        || (nonRenderCategoryPrivateUse && uc == System.Globalization.UnicodeCategory.PrivateUse);

      /// <summary>
      /// <para>Returns whether a character is a printable from the ASCII table, i.e. any character from U+0020 (32 = ' ' space) to U+007E (126 = '~' tilde), both inclusive.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static bool IsPrintableAscii(System.Text.Rune value)
        => char.IsPrintableAscii((char)value.Value);

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

      /// <summary>
      /// <para>Indicates whether a <see cref="System.Text.Rune"/> is a vowel in the specified <paramref name="culture"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">If null, then <see cref="System.Globalization.CultureInfo.CurrentCulture"/></param>
      /// <returns></returns>
      public static bool IsVowel(System.Text.Rune source, System.Globalization.CultureInfo? culture = null)
        => char.IsVowel((char)source.Value, culture);

      #region ..Subscript

      /// <summary>
      /// <para>Indicates whether the specified Unicode character is categorized as a subscript character.</para>
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public static bool IsSubscript(System.Text.Rune rune)
        => System.Char.IsSubscript((char)rune.Value);

      /// <summary>
      /// <para>Converts the value of a Unicode character to its subscript equivalent.</para>
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public static System.Text.Rune ToSubscript(System.Text.Rune rune)
        => System.Char.TryConvertToSubscript((char)rune.Value, out var subscriptCharacter) ? (System.Text.Rune)subscriptCharacter : rune;

      public static bool TryConvertToSubscript(System.Text.Rune rune, out System.Text.Rune subscriptCharacter)
        => (subscriptCharacter = ToSubscript(rune)) == rune;

      #endregion

      #region ..Superscript

      /// <summary>
      /// <para>Indicates whether the specified Unicode character is categorized as a superscript character.</para>
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public static bool IsSuperscript(System.Text.Rune rune)
        => System.Char.IsSuperscript((char)rune.Value);

      /// <summary>
      /// <para>Converts the value of a Unicode character to its superscript equivalent.</para>
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public static System.Text.Rune ToSuperscript(System.Text.Rune rune)
        => System.Char.TryConvertToSuperscript((char)rune.Value, out var superscriptCharacter) ? (System.Text.Rune)superscriptCharacter : rune;

      public static bool TryConvertToSuperscript(System.Text.Rune rune, out System.Text.Rune superscriptCharacter)
        => (superscriptCharacter = ToSuperscript(rune)) == rune;

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
      {
        var uc = System.Text.Rune.GetUnicodeCategory(source);

        return $"{source.UnicodeUnotationEncode()} '{source}' ({uc}, {uc.ToUnicodeCategoryMajorMinor()})";
      }

      #endregion
    }
  }
}
