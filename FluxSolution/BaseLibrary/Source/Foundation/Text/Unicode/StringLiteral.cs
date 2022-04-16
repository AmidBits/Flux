using System.Linq;

namespace Flux
{
  /// <summary>The functionality of this class relates to \uxxxx and \UXXXXXXXX style formatting.</summary>
  public static partial class Unicode
  {
    public static readonly System.Text.RegularExpressions.Regex ParseStringLiteralRegex = new(@"((?<=\\u)[0-9a-f]{4}|(?<=\\U)[0-9A-F]{8,})", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    public static System.Collections.Generic.IEnumerable<char> ParseStringLiteral(string expression)
      => ParseStringLiteralRegex.Matches(expression).Where(m => m.Success).Select(m => (char)int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null));
    public static bool TryParseStringLiteral(string text, out System.Collections.Generic.List<char> result)
    {
      try
      {
        result = ParseStringLiteral(text).ToList();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Convert the Unicode codepoint to a string literal format, i.e. "\uxxxx" (four hex characters) or "\UXXXXXXXX" (eight hex characters).</summary>
    public static string StringLiteralToString(System.Text.Rune rune)
      => rune.Value <= 0xFFFF ? $@"\u{rune.Value:X4}" : $@"\U{rune.Value:X8}";
  }
}
