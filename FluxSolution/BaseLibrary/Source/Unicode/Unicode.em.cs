namespace Flux
{
  ///// <summary></summary>
  ///// <remarks>
  ///// <para>A code point is a Unicode number representing a defined meaning. One or more code points may be used to represent higher order constructs, e.g. a grapheme.</para>
  ///// <para>In .NET, the <see cref="System.Text.Rune"/> represents a Unicode code point.</para>
  ///// <para>A code unit is a unit of storage for encoding code points. E.g. UTF-16 is a 16-bit code unit. One or more code units may be used to represent a code point.</para>
  ///// <para>In .NET, the type <see cref="char"/> is a code unit identified as UTF-16. Multiple <see cref="char"/>s are used to represent larger constructs, e.g. code points (<see cref="System.Text.Rune"/>) and graphemes (text elements).</para>
  ///// <para>A grapheme is one or more code points representing an element of writing.</para>
  ///// <para>In .NET, a grapheme is a text element represented as a sequence of <see cref="char"/> instances, e.g. in a <see cref="string"/>.</para>
  ///// <para>A glyph is a visual "image", e.g. in a font, used to represent visual "symbols". One or more glyphs may be used to represent a grapheme.</para>
  ///// </remarks>

  public static partial class ExtensionMethods
  {
    #region UnicodeCategory

    public static System.Collections.Generic.IEnumerable<System.Text.Rune> GetRunes(this System.Globalization.UnicodeCategory unicodeCategory)
    {
      for (var i = 0; i <= 0x10FFFF; i++)
        if (System.Text.Rune.IsValid(i))
          if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(i) == unicodeCategory)
            yield return new System.Text.Rune(i);
    }

    /// <summary>Translates a System.Globalization.UnicodeCategory enum value into a MajorLabel enum value.</summary>
    /// <example>var allCharactersByCategoryMajorLabel = Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
    public static UnicodeCategoryMajor ToUnicodeCategoryMajor(this System.Globalization.UnicodeCategory unicodeCategory)
      => unicodeCategory switch
      {
        System.Globalization.UnicodeCategory.LowercaseLetter or System.Globalization.UnicodeCategory.ModifierLetter or System.Globalization.UnicodeCategory.OtherLetter or System.Globalization.UnicodeCategory.TitlecaseLetter or System.Globalization.UnicodeCategory.UppercaseLetter => UnicodeCategoryMajor.Letter,
        System.Globalization.UnicodeCategory.SpacingCombiningMark or System.Globalization.UnicodeCategory.EnclosingMark or System.Globalization.UnicodeCategory.NonSpacingMark => UnicodeCategoryMajor.Mark,
        System.Globalization.UnicodeCategory.DecimalDigitNumber or System.Globalization.UnicodeCategory.LetterNumber or System.Globalization.UnicodeCategory.OtherNumber => UnicodeCategoryMajor.Number,
        System.Globalization.UnicodeCategory.ConnectorPunctuation or System.Globalization.UnicodeCategory.DashPunctuation or System.Globalization.UnicodeCategory.ClosePunctuation or System.Globalization.UnicodeCategory.FinalQuotePunctuation or System.Globalization.UnicodeCategory.InitialQuotePunctuation or System.Globalization.UnicodeCategory.OtherPunctuation or System.Globalization.UnicodeCategory.OpenPunctuation => UnicodeCategoryMajor.Punctuation,
        System.Globalization.UnicodeCategory.CurrencySymbol or System.Globalization.UnicodeCategory.ModifierSymbol or System.Globalization.UnicodeCategory.MathSymbol or System.Globalization.UnicodeCategory.OtherSymbol => UnicodeCategoryMajor.Symbol,
        System.Globalization.UnicodeCategory.LineSeparator or System.Globalization.UnicodeCategory.ParagraphSeparator or System.Globalization.UnicodeCategory.SpaceSeparator => UnicodeCategoryMajor.Separator,
        System.Globalization.UnicodeCategory.Control or System.Globalization.UnicodeCategory.Format or System.Globalization.UnicodeCategory.OtherNotAssigned or System.Globalization.UnicodeCategory.PrivateUse or System.Globalization.UnicodeCategory.Surrogate => UnicodeCategoryMajor.Other,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unicodeCategory)),
      };

    public static UnicodeCategoryMajorMinor ToUnicodeCategoryMajorMinor(this System.Globalization.UnicodeCategory unicodeCategory)
      => (UnicodeCategoryMajorMinor)unicodeCategory;

    #endregion // UnicodeCategory

    #region UnicodeCategoryMajor

    public static UnicodeCategoryMajor ParseUnicodeCategoryMajor(this char unicodeCategoryMajor)
      => (UnicodeCategoryMajor)System.Enum.Parse(typeof(UnicodeCategoryMajor), unicodeCategoryMajor.ToString(), true);

    public static bool TryParseUnicodeCategoryMajor(this char unicodeCategoryMajor, out UnicodeCategoryMajor result)
    {
      try
      {
        result = ParseUnicodeCategoryMajor(unicodeCategoryMajor);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    /// <summary>Translates a System.Globalization.UnicodeCategory enum value into a MajorLabel enum value.</summary>
    /// <example>var allCharactersByCategoryMajorLabel = Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
    public static UnicodeCategoryMajor ToUnicodeCategoryMajor(this UnicodeCategoryMajorMinor majorMinor)
      => ((System.Globalization.UnicodeCategory)majorMinor).ToUnicodeCategoryMajor();

    #endregion // UnicodeCategoryMajor

    #region UnicodeCategoryMajorMinor

    /// <summary>Parses two characters as representing Unicode category major and minor.</summary>
    public static UnicodeCategoryMajorMinor ParseUnicodeCategoryMajorMinor(this char unicodeCategoryMajor, char unicodeCategoryMinor)
      => ((UnicodeCategoryMajorMinor)System.Enum.Parse(typeof(UnicodeCategoryMajorMinor), $"{unicodeCategoryMajor}{unicodeCategoryMinor}"));

    /// <summary>Tries to parse the beginning of a string as Unicode category major and minor.</summary>
    public static bool TryParseUnicodeCategoryMajorMinor(this char unicodeCategoryMajor, char unicodeCategoryMinor, out UnicodeCategoryMajorMinor result)
    {
      try
      {
        result = ParseUnicodeCategoryMajorMinor(unicodeCategoryMajor, unicodeCategoryMinor);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    /// <summary>Converts a MajorMinorCode enum to a UnicodeCategory enum.</summary>
    public static System.Globalization.UnicodeCategory ToUnicodeCategory(this UnicodeCategoryMajorMinor unicodeCategoryMajorMinor)
      => (System.Globalization.UnicodeCategory)unicodeCategoryMajorMinor;

    #endregion // UnicodeCategoryMajorMinor

    #region UnicodeRange

    public static System.Collections.Generic.List<System.Text.Rune> GetRunes(this System.Text.Unicode.UnicodeRange unicodeRange)
    {
      var collection = new System.Collections.Generic.List<System.Text.Rune>(unicodeRange.Length);

      for (int codePoint = unicodeRange.FirstCodePoint, length = unicodeRange.Length; length > 0; codePoint++, length--)
        if (System.Text.Rune.IsValid(codePoint))
          collection.Add(new System.Text.Rune(codePoint));

      return collection;
    }

    #endregion // UnicodeRange

    #region UnicodeUnotation

    [System.Text.RegularExpressions.GeneratedRegex(@"(?<=U\+)[0-9A-F]{4,6}", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex ParseUnicodeUnotationRegex();

    public static System.Collections.Generic.IEnumerable<System.Text.Rune> ParseUnicodeUnotation(this string text)
      => ParseUnicodeUnotationRegex().Matches(text).Where(m => m.Success).Select(m => new System.Text.Rune(int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null)));
    public static bool TryParseUnicodeUnotation(this string text, out System.Collections.Generic.List<System.Text.Rune> result)
    {
      try
      {
        result = ParseUnicodeUnotation(text).ToList();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string ToUnicodeUnotationString(this System.Text.Rune rune)
      => $"U+{rune.Value:X4}";

    #endregion // UnicodeUnotation

    #region CsEscapeSequence

    [System.Text.RegularExpressions.GeneratedRegex(@"((?<=\\u)[0-9a-f]{4}|(?<=\\U)[0-9A-F]{8}|(?<=\\x)[0-9A-F]{1,8})", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex ParseUnicodeCsEscapeSequenceRegex();

    public static System.Collections.Generic.IEnumerable<char> ParseUnicodeCsEscapeSequence(this string text)
      => ParseUnicodeCsEscapeSequenceRegex().Matches(text).Where(m => m.Success).Select(m => (char)int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null));
    public static bool TryParseUnicodeCsEscapeSequence(this string text, out System.Collections.Generic.List<char> result)
    {
      try
      {
        result = ParseUnicodeCsEscapeSequence(text).ToList();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Convert the Unicode codepoint to a string literal format, i.e. "\uhhhh" (four hex characters, for UTF-16 size), "\U00HHHHHH" (eight hex characters, for UTF-32 size), or "\x[H][H][H][H].</summary>
    public static string ToUnicodeCsEscapeSequenceString(this System.Text.Rune rune, Unicode.CsEscapeSequenceFormat format)
      => format switch
      {
        Unicode.CsEscapeSequenceFormat.UTF16 => $@"\u{rune.Value:X4}",
        Unicode.CsEscapeSequenceFormat.UTF32 => $@"\U{rune.Value:X8}",
        Unicode.CsEscapeSequenceFormat.Variable => $@"\x{rune.Value:X1}",
        _ => throw new NotImplementedException(),
      };

    #endregion // CsEscapeSequence
  }

  namespace Unicode
  {
    public enum CsEscapeSequenceFormat
    {
      /// <summary>\u = Unicode escape sequence (UTF-16) \uHHHH (range: 0000 - FFFF; example: \u00E7 = "ç")</summary>
      UTF16,
      /// <summary>\U = Unicode escape sequence (UTF-32) \U00HHHHHH (range: 000000 - 10FFFF; example: \U0001F47D)</summary>
      UTF32,
      /// <summary>\x = Unicode escape sequence similar to "\u" except with variable length \xH[H][H][H] (range: 0 - FFFF; example: \x00E7 or \x0E7 or \xE7 = "ç")</summary>
      Variable
    }
  }
}
