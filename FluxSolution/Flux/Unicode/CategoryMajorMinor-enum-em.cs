namespace Flux
{
  public static partial class Unicode
  {
    /// <summary>
    /// <para>Parses two characters as representing Unicode category <paramref name="unicodeCategoryMajor"/> and <paramref name="unicodeCategoryMinor"/>.</para>
    /// </summary>
    public static UnicodeCategoryMajorMinor ParseUnicodeCategoryMajorMinor(this char unicodeCategoryMajor, char unicodeCategoryMinor)
      => ((UnicodeCategoryMajorMinor)System.Enum.Parse<UnicodeCategoryMajorMinor>($"{unicodeCategoryMajor}{unicodeCategoryMinor}", true));

    /// <summary>
    /// <para>Attempts to parse the beginning of a string as Unicode category <paramref name="unicodeCategoryMajor"/> and <paramref name="unicodeCategoryMinor"/>.</para>
    /// </summary>
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

    /// <summary>
    /// <para>Translates a <paramref name="unicodeCategoryMajorMinor"/> enum value into a <see cref="UnicodeCategoryMajor"/> enum value.</para>
    /// </summary>
    /// <example>var allCharactersByCategoryMajorLabel = Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
    public static UnicodeCategoryMajor ToUnicodeCategoryMajor(this UnicodeCategoryMajorMinor unicodeCategoryMajorMinor)
      => ((System.Globalization.UnicodeCategory)unicodeCategoryMajorMinor).ToUnicodeCategoryMajor();

    /// <summary>
    /// <para>Translates a <paramref name="unicodeCategoryMajorMinor"/> enum value into a <see cref="System.Globalization.UnicodeCategory"/> enum value.</para>
    /// </summary>
    public static System.Globalization.UnicodeCategory ToUnicodeCategory(this UnicodeCategoryMajorMinor unicodeCategoryMajorMinor)
      => (System.Globalization.UnicodeCategory)unicodeCategoryMajorMinor;
  }
}
