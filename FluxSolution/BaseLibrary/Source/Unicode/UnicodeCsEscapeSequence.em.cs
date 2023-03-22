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

  public static partial class ExtensionMethodsUnicode
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"((?<=\\u)[0-9a-f]{4}|(?<=\\U)[0-9A-F]{8}|(?<=\\x)[0-9A-F]{1,8})", System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex RegexParseUnicodeCsEscapeSequence();

    public static System.Collections.Generic.IEnumerable<char> ParseUnicodeCsEscapeSequence(this string text)
      => RegexParseUnicodeCsEscapeSequence().Matches(text).Where(m => m.Success).Select(m => (char)int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null));
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
    public static string ToUnicodeCsEscapeSequenceString(this char character, Unicode.CsEscapeSequenceFormat format)
      => ToUnicodeCsEscapeSequenceString((System.Text.Rune)character, format);

    /// <summary>Convert the Unicode codepoint to a string literal format, i.e. "\uhhhh" (four hex characters, for UTF-16 size), "\U00HHHHHH" (eight hex characters, for UTF-32 size), or "\x[H][H][H][H].</summary>
    public static string ToUnicodeCsEscapeSequenceString(this System.Text.Rune rune, Unicode.CsEscapeSequenceFormat format)
      => format switch
      {
        Unicode.CsEscapeSequenceFormat.UTF16 => $@"\u{rune.Value:X4}",
        Unicode.CsEscapeSequenceFormat.UTF32 => $@"\U{rune.Value:X8}",
        Unicode.CsEscapeSequenceFormat.Variable => $@"\x{rune.Value:X2}",
        _ => throw new NotImplementedException(),
      };
  }

  namespace Unicode
  {
    public enum CsEscapeSequenceFormat
    {
      /// <summary>\u = Unicode escape sequence (UTF-16) \uHHHH (range: 0000 - FFFF; example: \u00E7 = "ç")</summary>
      UTF16,
      /// <summary>\U = Unicode escape sequence (UTF-32) \U00HHHHHH (range: 000000 - 10FFFF; example: \U0001F47D)</summary>
      UTF32,
      /// <summary>\x = Unicode escape sequence similar to "\u" except with variable length \xH[H][H][H] (range: 0 - FFFF; example: \x00E7 or \x0E7 or \xE7 = "ç")</summary>
      Variable
    }
  }
}
