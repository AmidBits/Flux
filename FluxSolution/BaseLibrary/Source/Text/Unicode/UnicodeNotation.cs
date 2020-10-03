namespace Flux.Text
{
  /// <summary>The functionality of this class relates to U+xxxxx style formatting.</summary>
  public static class UnicodeNotation
  {
    public static readonly System.Text.RegularExpressions.Regex ParseRegex = new System.Text.RegularExpressions.Regex(@"(?<=U\+)1?[0-9A-F]{4,}", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);

    public static int Parse(string expression)
      => ParseRegex.Match(expression) is var m && m.Success && int.TryParse(m.Value, System.Globalization.NumberStyles.HexNumber, null, out var number) ? number : throw new System.ArgumentException($"Could not parse \"{expression}\" as unicode notation.");
    public static bool TryParse(string text, out int result)
    {
      try
      {
        result = Parse(text);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types
      catch { }
#pragma warning restore CA1031 // Do not catch general exception types

      result = default;
      return false;
    }

    /// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string ToString(int codePoint)
      => $"U+{codePoint:X4}";
  }
}
