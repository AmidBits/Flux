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
#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<=U\+)[0-9A-F]{4,6}", System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex RegexParseUnicodeUnotation();
#else
    private static System.Text.RegularExpressions.Regex RegexParseUnicodeUnotation() => new(@"(?<=U\+)[0-9A-F]{4,6}");
#endif

    public static System.Collections.Generic.List<char> ParseUnicodeUnotation(this string text)
    {
      var list = new System.Collections.Generic.List<char>();

      foreach (var match in RegexParseUnicodeUnotation().Matches(text).Cast<System.Text.RegularExpressions.Match>())
        if (match.Success)
          list.Add((char)int.Parse(match.Value, System.Globalization.NumberStyles.HexNumber, null));

      return list;
    }

    public static bool TryParseUnicodeUnotation(this string text, out System.Collections.Generic.List<char> result)
    {
      try
      {
        result = ParseUnicodeUnotation(text);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Convert the character to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string ToUnicodeUnotationString(this char character) => $"U+{(int)character:X4}";

    /// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string ToUnicodeUnotationString(this System.Text.Rune rune) => $"U+{rune.Value:X4}";
  }
}
