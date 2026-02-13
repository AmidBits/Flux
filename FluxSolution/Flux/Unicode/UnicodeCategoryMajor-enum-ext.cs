namespace Flux
{
  /// <summary>
  /// <para>This is an aggregate derivation of the System.Globalization.UnicodeCategory (or MajorMinorCode) enum value. The values represents the character of the first letter in the major code name, e.g. 'P' for Puncuation.</para>
  /// <code><example>var allCharactersByCategoryMajorLabel = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example></code>
  /// </summary>
  public enum UnicodeCategoryMajor
  {
    /// <summary>
    /// <para>Unicode major category of "Letter" = 'L'</para>
    /// </summary>
    Letter = 'L',
    /// <summary>
    /// <para>Unicode major category of "Mark" = 'M'</para>
    /// </summary>
    Mark = 'M',
    /// <summary>
    /// <para>Unicode major category of "Number" = 'N'</para>
    /// </summary>
    Number = 'N',
    /// <summary>
    /// <para>Unicode major category of "Punctuation" = 'P'</para>
    /// </summary>
    Punctuation = 'P',
    /// <summary>
    /// <para>Unicode major category of "Symbol" = 'S'</para>
    /// </summary>
    Symbol = 'S',
    /// <summary>
    /// <para>Unicode major category of "Separator" = 'Z', likely because 'S' is occupied for "Symbol".</para>
    /// </summary>
    Separator = 'Z',
    /// <summary>
    /// <para>Unicode major category of "Other" = 'C', likely for "Control".</para>
    /// </summary>
    Other = 'C',
  }

  ///// <summary>Extended Unicode functionality.</summary>
  ///// <remarks>
  ///// <para>A code point is a Unicode number representing a defined meaning. One or more code points may be used to represent other constructs, e.g. a grapheme.</para>
  ///// <para>In .NET, the <see cref="System.Text.Rune"/> represents a Unicode code point.</para>
  ///// <para>A code unit is a unit-of-storage for encoding code points. E.g. UTF-16 is a 16-bit code unit. One or more code units may be used to represent a code point.</para>
  ///// <para>In .NET, the type <see cref="System.Char"/> is a UTF-16 code unit. Multiple <see cref="System.Char"/>s are used to represent other constructs, e.g. code points (<see cref="System.Text.Rune"/>) and graphemes (text elements).</para>
  ///// <para>A grapheme is one or more code points representing an element of writing.</para>
  ///// <para>In .NET, a grapheme is a text element represented as a sequence of <see cref="System.Char"/> instances, e.g. in a <see cref="System.String"/>.</para>
  ///// <para>A glyph is a visual "image", e.g. in a font, used to represent visual "symbols". One or more glyphs may be used to represent a other constructs.</para>
  ///// </remarks>
  public static class UnicodeCategoryMajorExtensions
  {
    extension(UnicodeCategoryMajor)
    {
      /// <summary>
      /// <para>Parses a <paramref name="unicodeCategoryMajor"/> character into a <see cref="UnicodeCategoryMajor"/> enum value.</para>
      /// </summary>
      /// <param name="unicodeCategoryMajor"></param>
      /// <returns></returns>
      public static UnicodeCategoryMajor Parse(char unicodeCategoryMajor)
        => System.Enum.Parse<UnicodeCategoryMajor>(unicodeCategoryMajor.ToString(), true);

      /// <summary>
      /// <para>Attemps to parse a <paramref name="unicodeCategoryMajor"/> character into <paramref name="result"/> as a <see cref="UnicodeCategoryMajor"/> enum value.</para>
      /// </summary>
      /// <param name="unicodeCategoryMajor"></param>
      /// <param name="result"></param>
      /// <returns></returns>
      public static bool TryParse(char unicodeCategoryMajor, out UnicodeCategoryMajor result)
      {
        try
        {
          result = Parse(unicodeCategoryMajor);
          return true;
        }
        catch { }

        result = default;
        return false;
      }
    }
  }
}
