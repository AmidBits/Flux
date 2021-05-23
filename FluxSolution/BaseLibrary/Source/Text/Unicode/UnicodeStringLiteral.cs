using System.Linq;

namespace Flux.Text
{
  /// <summary>The functionality of this class relates to \uxxxx and \UXXXXXXXX style formatting.</summary>
  public static class UnicodeStringLiteral
  {
    public static readonly System.Text.RegularExpressions.Regex ParseRegex = new System.Text.RegularExpressions.Regex(@"((?<=\\u)[0-9a-f]{4}|(?<=\\U)[0-9A-F]{8,})", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    public static System.Collections.Generic.IEnumerable<System.Text.Rune> Parse(string expression)
      => ParseRegex.Matches(expression).Where(m => m.Success).Select(m => new System.Text.Rune(int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null)));
    public static bool TryParse(string text, out System.Collections.Generic.List<System.Text.Rune> result)
    {
      try
      {
        result = Parse(text).ToList();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Convert the Unicode codepoint to a string literal format, i.e. "\uxxxx" (four hex characters) or "\UXXXXXXXX" (eight hex characters).</summary>
    public static string ToString(System.Text.Rune rune)
      => rune.Value <= 0xFFFF ? $@"\u{rune.Value:x4}" : $@"\U{rune.Value:X8}";
  }
}
