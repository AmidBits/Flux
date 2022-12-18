namespace Flux
{
  /// <summary>The functionality here relates to \uxxxx, \UXXXXXXXX and \xVARIABLE style formatting.</summary>
  public static partial class ExtensionMethods
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"((?<=\\u)[0-9a-f]{4}|(?<=\\U)[0-9A-F]{8}|(?<=\\x)[0-9A-F]{1,8})", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex ParseUnicodeCsEscapeSequenceRegex();

    public static System.Collections.Generic.IEnumerable<char> ParseUnicodeCsEscapeSequence(this string text)
      => ParseUnicodeCsEscapeSequenceRegex().Matches(text).Where(m => m.Success).Select(m => (char)int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null));
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

    /// <summary>Convert the Unicode codepoint to a string literal format, i.e. "\uhhhh" (four hex characters, for UTF-16 size), "\U00HHHHHH" (eight hex characters, for UTF-32 size), or "\x[H][H][H][H].</summary>
    public static string ToUnicodeCsEscapeSequenceString(this System.Text.Rune rune, Unicode.CsEscapeSequenceFormat format)
      => format switch
      {
        Unicode.CsEscapeSequenceFormat.UTF16 => $@"\u{rune.Value:X4}",
        Unicode.CsEscapeSequenceFormat.UTF32 => $@"\U{rune.Value:X8}",
        Unicode.CsEscapeSequenceFormat.Variable => $@"\x{rune.Value:X1}",
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
