namespace Flux
{
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
  public static partial class Em
  {
    /// <summary>
    /// <para>Parses a <paramref name="unicodeCategoryMajor"/> character into a <see cref="UnicodeCategoryMajor"/> enum value.</para>
    /// </summary>
    /// <param name="unicodeCategoryMajor"></param>
    /// <returns></returns>
    public static Unicode.UnicodeCategoryMajor ParseUnicodeCategoryMajor(this char unicodeCategoryMajor)
      => System.Enum.Parse<Unicode.UnicodeCategoryMajor>(unicodeCategoryMajor.ToString(), true);

    /// <summary>
    /// <para>Attemps to parse a <paramref name="unicodeCategoryMajor"/> character into <paramref name="result"/> as a <see cref="UnicodeCategoryMajor"/> enum value.</para>
    /// </summary>
    /// <param name="unicodeCategoryMajor"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryParseUnicodeCategoryMajor(this char unicodeCategoryMajor, out Unicode.UnicodeCategoryMajor result)
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
  }
}
