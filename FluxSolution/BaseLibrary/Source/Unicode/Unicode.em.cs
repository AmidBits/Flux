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
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> GetRunes(this System.Text.Unicode.UnicodeRange unicodeRange)
    {
      var length = unicodeRange.Length;

      for (var i = unicodeRange.FirstCodePoint; length > 0; i++)
        if (System.Text.Rune.IsValid(i))
        {
          yield return new System.Text.Rune(i);

          length--;
        }
    }

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
    public static UnicodeCategoryMajorMinor ToUnicodeCategoryMajorMinor(this char unicodeCategoryMajor, char unicodeCategoryMinor)
      => ((UnicodeCategoryMajorMinor)System.Enum.Parse(typeof(UnicodeCategoryMajorMinor), $"{unicodeCategoryMajor}{unicodeCategoryMinor}"));

    /// <summary>Parses a beginning of a string as Unicode category major and minor.</summary>
    public static UnicodeCategoryMajorMinor ParseUnicodeCategoryMajorMinor(this string unicodeCategoryMajorMinor)
      => (unicodeCategoryMajorMinor ?? throw new System.ArgumentNullException(nameof(unicodeCategoryMajorMinor))).Length >= 2
      ? ToUnicodeCategoryMajorMinor(unicodeCategoryMajorMinor[0], unicodeCategoryMajorMinor[1])
      : throw new System.ArgumentOutOfRangeException(nameof(unicodeCategoryMajorMinor));

    /// <summary>Tries to parse the beginning of a string as Unicode category major and minor.</summary>
    public static bool TryParseUnicodeCategoryMajorMinor(this string unicodeCategoryMajorMinor, out UnicodeCategoryMajorMinor result)
    {
      try
      {
        result = ParseUnicodeCategoryMajorMinor(unicodeCategoryMajorMinor);
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
  }
}
