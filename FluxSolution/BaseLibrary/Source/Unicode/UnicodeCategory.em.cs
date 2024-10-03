namespace Flux
{
  public static partial class Unicode
  {
    public static System.Collections.Generic.IEnumerable<char> GetRunes(this System.Globalization.UnicodeCategory unicodeCategory)
    {
      for (var i = 0; i <= 0x10FFFF; i++)
        if (System.Text.Rune.IsValid(i))
          if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(i) == unicodeCategory)
            yield return (char)i;
    }

    /// <summary>Translates a <see cref="System.Globalization.UnicodeCategory"/> enum value (<paramref name="unicodeCategory"/>) into a <see cref="UnicodeCategoryMajor"/> enum value.</summary>
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

    /// <summary>Translates a <see cref="System.Globalization.UnicodeCategory"/> enum value (<paramref name="unicodeCategory"/>) into a <see cref="UnicodeCategoryMajorMinor"/> enum value.</summary>
    public static UnicodeCategoryMajorMinor ToUnicodeCategoryMajorMinor(this System.Globalization.UnicodeCategory unicodeCategory) => (UnicodeCategoryMajorMinor)unicodeCategory;

    /// <summary>Creates a new string in a more readable format, e.g. "DecimalDigitNumber" becomes "decimal digit" (i.e. drop the ending Unicode category major, make lower case and add word spacing).</summary>
    public static string ToUnicodeCategoryMinorFriendlyString(this System.Globalization.UnicodeCategory source)
    {
      var ucsb = new System.Text.StringBuilder(source == System.Globalization.UnicodeCategory.OtherNotAssigned ? source.ToString()[5..] : source.ToString());
      var ucms = source.ToUnicodeCategoryMajor().ToString();

      if (ucsb.EndsWith(ucms)) ucsb.Remove(ucsb.Length - ucms.Length, ucms.Length); // Either fix the unicode category that ends with its own category major.
      else if (ucsb.StartsWith(ucms)) ucsb.Remove(0, ucms.Length); // Or fix the unicode category that starts with its own category major.

      ucsb.PrefixCapWords();

      if (source == System.Globalization.UnicodeCategory.NonSpacingMark) ucsb.RemoveAll(char.IsWhiteSpace); // Fix "non spacing" to "nonspacing".
      if (source == System.Globalization.UnicodeCategory.PrivateUse) ucsb.Replace(' ', '-'); // Fix "private use" to "private-use".

      return ucsb.ToString();
    }
  }
}
