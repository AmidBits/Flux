namespace Flux
{
  public static partial class Unicode
  {
    /// <summary>
    /// <para>Creates a new sequence with all runes in a <see cref="System.Globalization.UnicodeCategory"/>.</para>
    /// </summary>
    /// <param name="unicodeCategory"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<System.Text.Rune> GetRunesInUnicodeCategory(this System.Globalization.UnicodeCategory unicodeCategory)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (var i = 0; i <= 0x10FFFF; i++)
        if (System.Text.Rune.IsValid(i) && new System.Text.Rune(i) is var rune)
          if (System.Text.Rune.GetUnicodeCategory(rune) == unicodeCategory)
            list.Add(rune);

      return list;
    }

    /// <summary>Translates a <see cref="System.Globalization.UnicodeCategory"/> enum value (<paramref name="unicodeCategory"/>) into a <see cref="UnicodeCategoryMajor"/> enum value.</summary>
    /// <example>var allCharactersByCategoryMajorLabel = Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>

    public static UnicodeCategoryMajor ToUnicodeCategoryMajor(this System.Globalization.UnicodeCategory unicodeCategory)
    {
      var unicodeCategoryName = unicodeCategory.ToString();

      return System.Enum.GetValues<UnicodeCategoryMajor>().FirstOrValue(UnicodeCategoryMajor.Other, (ucm, i) => unicodeCategoryName.EndsWith(ucm.ToString())).Item;
    }

    /// <summary>Translates a <see cref="System.Globalization.UnicodeCategory"/> enum value (<paramref name="unicodeCategory"/>) into a <see cref="UnicodeCategoryMajorMinor"/> enum value.</summary>
    public static UnicodeCategoryMajorMinor ToUnicodeCategoryMajorMinor(this System.Globalization.UnicodeCategory unicodeCategory) => (UnicodeCategoryMajorMinor)unicodeCategory;

    /// <summary>Creates a new string in a more readable format, e.g. "DecimalDigitNumber" becomes "decimal digit" (i.e. drop the ending Unicode category major, make lower case and add word spacing).</summary>
    public static string ToUnicodeCategoryMinorFriendlyString(this System.Globalization.UnicodeCategory source)
    {
      var ucsb = new SpanMaker<char>(source == System.Globalization.UnicodeCategory.OtherNotAssigned ? source.ToString()[5..] : source.ToString());
      var ucms = source.ToUnicodeCategoryMajor().ToString();

      if (ucsb.AsReadOnlySpan().IsCommonSuffix(ucms)) ucsb = ucsb.Remove(ucsb.Length - ucms.Length, ucms.Length); // Either fix the unicode category that ends with its own category major.
      else if (ucsb.AsReadOnlySpan().IsCommonPrefix(ucms)) ucsb = ucsb.Remove(0, ucms.Length); // Or fix the unicode category that starts with its own category major.

      ucsb.PrefixCapWords();

      if (source == System.Globalization.UnicodeCategory.NonSpacingMark) ucsb = ucsb.RemoveAll(char.IsWhiteSpace); // Fix "non spacing" to "nonspacing".
      if (source == System.Globalization.UnicodeCategory.PrivateUse) ucsb = ucsb.Replace(c => c == ' ', "-"); // Fix "private use" to "private-use".

      return ucsb.ToString();
    }
  }
}
