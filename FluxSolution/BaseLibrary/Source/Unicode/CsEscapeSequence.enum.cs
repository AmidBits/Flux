namespace Flux
{
  ///// <summary></summary>
  ///// <remarks>
  ///// <para>A code point is a Unicode number representing a defined meaning. One or more code points may be used to represent higher order constructs, e.g. a grapheme.</para>
  ///// <para>In .NET, the <see cref="System.Text.Rune"/> represents a Unicode code point.</para>
  ///// <para>A code unit is a unit of storage for encoding code points. E.g. UTF-16 is a 16-bit code unit. One or more code units may be used to represent a code point.</para>
  ///// <para>In .NET, the type <see cref="System.Char"/> is a UTF-16 code unit. Multiple <see cref="System.Char"/>s are used to represent larger constructs, e.g. code points (<see cref="System.Text.Rune"/>) and graphemes (text elements).</para>
  ///// <para>A grapheme is one or more code points representing an element of writing.</para>
  ///// <para>In .NET, a grapheme is a text element represented as a sequence of <see cref="System.Char"/>, e.g. in a <see cref="System.String"/>.</para>
  ///// <para>A glyph is a visual "image", e.g. in a font, used to represent visual "symbols". One or more glyphs may be used to represent a larger construct.</para>
  ///// </remarks>
  public static partial class Unicode
  {
#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"((?<=\\u)[0-9a-f]{4}|(?<=\\U)[0-9A-F]{8}|(?<=\\x)[0-9A-F]{1,8})", System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex RegexParseUnicodeCsEscapeSequence();
#else
    private static System.Text.RegularExpressions.Regex RegexParseUnicodeCsEscapeSequence() => new(@"((?<=\\u)[0-9a-f]{4}|(?<=\\U)[0-9A-F]{8}|(?<=\\x)[0-9A-F]{1,8})");
#endif

    public static System.Collections.Generic.List<char> ParseUnicodeCsEscapeSequence(this string text)
    {
      var list = new System.Collections.Generic.List<char>();

      foreach (var match in RegexParseUnicodeCsEscapeSequence().Matches(text).Cast<System.Text.RegularExpressions.Match>())
        if (match.Success)
          list.Add((char)int.Parse(match.Value, System.Globalization.NumberStyles.HexNumber, null));

      return list;
    }

    public static bool TryParseUnicodeCsEscapeSequence(this string text, out System.Collections.Generic.List<char> result)
    {
      try
      {
        result = ParseUnicodeCsEscapeSequence(text).ToList();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Convert the character to a string literal format, i.e. "\uhhhh" (four hex characters, for UTF-16 size), "\U00HHHHHH" (eight hex characters, for UTF-32 size), or "\x[H][H][H][H].</summary>
    public static string ToUnicodeCsEscapeSequenceString(this char character, CsEscapeSequence format)
      => format switch
      {
        CsEscapeSequence.UTF16 => $@"\u{(int)character:X4}",
        CsEscapeSequence.UTF32 => $@"\U{(int)character:X8}",
        CsEscapeSequence.Variable => $@"\x{(int)character:X2}",
        _ => throw new NotImplementedException(),
      };

    /// <summary>Convert the Unicode codepoint to a string literal format, i.e. "\uhhhh" (four hex characters, for UTF-16 size), "\U00HHHHHH" (eight hex characters, for UTF-32 size), or "\x[H][H][H][H].</summary>
    public static string ToUnicodeCsEscapeSequenceString(this System.Text.Rune rune, CsEscapeSequence format)
      => format switch
      {
        CsEscapeSequence.UTF16 => $@"\u{rune.Value:X4}",
        CsEscapeSequence.UTF32 => $@"\U{rune.Value:X8}",
        CsEscapeSequence.Variable => $@"\x{rune.Value:X2}",
        _ => throw new NotImplementedException(),
      };
  }

  public enum CsEscapeSequence
  {
    /// <summary>\u = Unicode escape sequence (UTF-16) \uHHHH (range: 0000 - FFFF; example: \u00E7 = "ç")</summary>
    UTF16,
    /// <summary>\U = Unicode escape sequence (UTF-32) \U00HHHHHH (range: 000000 - 10FFFF; example: \U0001F47D)</summary>
    UTF32,
    /// <summary>\x = Unicode escape sequence similar to "\u" except with variable length \xH[H][H][H] (range: 0 - FFFF; example: \x00E7 or \x0E7 or \xE7 = "ç")</summary>
    Variable
  }
}
