namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Parses two characters as representing Unicode category <paramref name="unicodeCategoryMajor"/> and <paramref name="unicodeCategoryMinor"/>.</para>
    /// </summary>
    public static Unicode.UnicodeCategoryMajorMinor ParseUnicodeCategoryMajorMinor(this char unicodeCategoryMajor, char unicodeCategoryMinor)
      => ((Unicode.UnicodeCategoryMajorMinor)System.Enum.Parse<Unicode.UnicodeCategoryMajorMinor>($"{unicodeCategoryMajor}{unicodeCategoryMinor}", true));

    /// <summary>
    /// <para>Attempts to parse the beginning of a string as Unicode category <paramref name="unicodeCategoryMajor"/> and <paramref name="unicodeCategoryMinor"/>.</para>
    /// </summary>
    public static bool TryParseUnicodeCategoryMajorMinor(this char unicodeCategoryMajor, char unicodeCategoryMinor, out Unicode.UnicodeCategoryMajorMinor result)
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

    /// <summary>
    /// <para>Translates a <paramref name="unicodeCategoryMajorMinor"/> enum value into a <see cref="UnicodeCategoryMajor"/> enum value.</para>
    /// </summary>
    /// <example>var allCharactersByCategoryMajorLabel = Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
    public static Unicode.UnicodeCategoryMajor ToUnicodeCategoryMajor(this Unicode.UnicodeCategoryMajorMinor unicodeCategoryMajorMinor)
      => ((System.Globalization.UnicodeCategory)unicodeCategoryMajorMinor).ToUnicodeCategoryMajor();

    /// <summary>
    /// <para>Translates a <paramref name="unicodeCategoryMajorMinor"/> enum value into a <see cref="System.Globalization.UnicodeCategory"/> enum value.</para>
    /// </summary>
    public static System.Globalization.UnicodeCategory ToUnicodeCategory(this Unicode.UnicodeCategoryMajorMinor unicodeCategoryMajorMinor)
      => (System.Globalization.UnicodeCategory)unicodeCategoryMajorMinor;
  }
}
