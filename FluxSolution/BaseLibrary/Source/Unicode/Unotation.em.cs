namespace Flux
{
  /// <summary>The functionality here relates to U+xxxxx style formatting.</summary>
  public static partial class ExtensionMethods
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<=U\+)[0-9A-F]{4,6}", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex ParseUnicodeUnotationRegex();

    public static System.Collections.Generic.IEnumerable<System.Text.Rune> ParseUnicodeUnotation(this string text)
      => ParseUnicodeUnotationRegex().Matches(text).Where(m => m.Success).Select(m => new System.Text.Rune(int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null)));
    public static bool TryParseUnicodeUnotation(this string text, out System.Collections.Generic.List<System.Text.Rune> result)
    {
      try
      {
        result = ParseUnicodeUnotation(text).ToList();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string ToUnicodeUnotationString(this System.Text.Rune rune)
      => $"U+{rune.Value:X4}";
  }
}
