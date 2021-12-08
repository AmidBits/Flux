using System.Linq;

namespace Flux.Text
{
  /// <summary>The functionality of this class relates to U+xxxxx style formatting.</summary>
  public static partial class Unicode
  {
    public static readonly System.Text.RegularExpressions.Regex ParseNotationRegex = new(@"(?<=U\+)[0-9A-F]{4,6}", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);

    public static System.Collections.Generic.IEnumerable<System.Text.Rune> ParseNotation(string expression)
      => ParseNotationRegex.Matches(expression).Where(m => m.Success).Select(m => new System.Text.Rune(int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null)));
    public static bool TryParseNotation(string text, out System.Collections.Generic.List<System.Text.Rune> result)
    {
      try
      {
        result = ParseNotation(text).ToList();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string NotationToString(System.Text.Rune rune)
      => $"U+{rune.Value:X4}";
  }
}
