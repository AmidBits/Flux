using System.Linq;

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
    private static System.Collections.Generic.IList<(string name, System.Text.Unicode.UnicodeRange range)> GetUnicodeNamesAndRanges()
    {
      var list = new System.Collections.Generic.List<(string name, System.Text.Unicode.UnicodeRange range)>();

      foreach (var pi in Flux.Fx.GetPropertyInfos(typeof(System.Text.Unicode.UnicodeRanges)).Where(pi => pi.Name != nameof(System.Text.Unicode.UnicodeRanges.All) && pi.Name != nameof(System.Text.Unicode.UnicodeRanges.None)))
        if (pi.GetValue(null, null) is System.Text.Unicode.UnicodeRange ur)
          list.Add((pi.Name, ur));

      return list;
    }

    /// <summary>Returns a readonly list with the names and corresponding <see cref="System.Text.Unicode.UnicodeRange"/> objects.</summary>
    public static readonly System.Collections.Generic.IReadOnlyList<(string name, System.Text.Unicode.UnicodeRange range)> UnicodeNamesAndRanges
      = (System.Collections.Generic.IReadOnlyList<(string name, System.Text.Unicode.UnicodeRange range)>)GetUnicodeNamesAndRanges();

    /// <summary>Creates a new sequence with all runes in the <paramref name="unicodeRange"/>.</summary>
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> GetRunes(this System.Text.Unicode.UnicodeRange unicodeRange)
    {
      for (int codePoint = unicodeRange.FirstCodePoint, length = unicodeRange.Length; length > 0; codePoint++, length--)
        if (System.Text.Rune.IsValid(codePoint))
          yield return new System.Text.Rune(codePoint);
    }

    /// <summary>Locates the Unicode range and block name of the <paramref name="character"/>.</summary>
    public static (string name, System.Text.Unicode.UnicodeRange range) FindUnicodeRange(this System.Char character)
      => FindUnicodeRange((System.Text.Rune)character);

    /// <summary>Locates the Unicode range and block name of the <paramref name="rune"/>.</summary>
    public static (string name, System.Text.Unicode.UnicodeRange range) FindUnicodeRange(this System.Text.Rune rune)
    {
      for (var index = 0; index < UnicodeNamesAndRanges.Count; index++)
        if (UnicodeNamesAndRanges[index] is var unar && unar.range is var ur && rune.Value >= ur.FirstCodePoint && rune.Value < ur.FirstCodePoint + ur.Length)
          return unar;

      return (nameof(System.Text.Unicode.UnicodeRanges.None), System.Text.Unicode.UnicodeRanges.None);
    }
  }
}
