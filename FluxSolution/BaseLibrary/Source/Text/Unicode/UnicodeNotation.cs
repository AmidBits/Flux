namespace Flux.Text
{
  /// <summary>The functionality of this class relates to U+hhhh style formatting.</summary>
  public static class UnicodeNotation
  {
    public static readonly System.Text.RegularExpressions.Regex ParseRegex = new System.Text.RegularExpressions.Regex(@"(?<=U\+)[0-9A-Fa-f]{4,}");

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
