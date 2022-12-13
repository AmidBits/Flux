namespace Flux
{
  /// <summary>The functionality here relates to U+xxxxx style formatting.</summary>
  public static partial class Unicode
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<=U\+)[0-9A-F]{4,6}", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled)]
    public static partial System.Text.RegularExpressions.Regex ParseUnotationRegex();

    public static System.Collections.Generic.IEnumerable<System.Text.Rune> ParseUnotation(this string text)
      => ParseUnotationRegex().Matches(text).Where(m => m.Success).Select(m => new System.Text.Rune(int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber, null)));
    public static bool TryParseUnotation(this string text, out System.Collections.Generic.List<System.Text.Rune> result)
    {
      try
      {
        result = ParseUnotation(text).ToList();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string ToUnotationString(this System.Text.Rune rune)
      => $"U+{rune.Value:X4}";
  }
}
