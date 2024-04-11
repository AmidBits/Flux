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

  public static partial class Unicode
  {
    /// <summary>Returns a readonly list with the names and corresponding <see cref="System.Text.Unicode.UnicodeRange"/> objects.</summary>
    private static System.Collections.Generic.Dictionary<System.Text.Unicode.UnicodeRange, string> GetUnicodeRangeAndNames()
    {
      var dictionary = new System.Collections.Generic.Dictionary<System.Text.Unicode.UnicodeRange, string>();

      foreach (var pi in Flux.Fx.GetPropertyInfos(typeof(System.Text.Unicode.UnicodeRanges)).Where(pi => pi.Name != nameof(System.Text.Unicode.UnicodeRanges.All) && pi.Name != nameof(System.Text.Unicode.UnicodeRanges.None)))
        if (pi.GetValue(null, null) is System.Text.Unicode.UnicodeRange ur)
          dictionary.Add(ur, pi.Name);

      return dictionary;
    }

    /// <summary>Creates a new sequence with all runes in the <paramref name="unicodeRange"/>.</summary>
    public static System.Collections.Generic.List<System.Text.Rune> GetRunes(this System.Text.Unicode.UnicodeRange unicodeRange)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (int codePoint = unicodeRange.FirstCodePoint, length = unicodeRange.Length; length > 0; codePoint++, length--)
        if (System.Text.Rune.IsValid(codePoint))
          list.Add(new System.Text.Rune(codePoint));

      return list;
    }

    /// <summary>Locates the Unicode range and block name of the <paramref name="character"/>.</summary>
    public static System.Collections.Generic.KeyValuePair<System.Text.Unicode.UnicodeRange, string> FindUnicodeRange(this System.Char character) => FindUnicodeRange((System.Text.Rune)character);

    /// <summary>Locates the Unicode range and block name of the <paramref name="rune"/>.</summary>
    public static System.Collections.Generic.KeyValuePair<System.Text.Unicode.UnicodeRange, string> FindUnicodeRange(this System.Text.Rune rune)
    {
      foreach (var kvp in GetUnicodeRangeAndNames())
        if (rune.Value >= kvp.Key.FirstCodePoint && rune.Value < (kvp.Key.FirstCodePoint + kvp.Key.Length))
          return kvp;

      return new(System.Text.Unicode.UnicodeRanges.None, nameof(System.Text.Unicode.UnicodeRanges.None));
    }
  }
}
