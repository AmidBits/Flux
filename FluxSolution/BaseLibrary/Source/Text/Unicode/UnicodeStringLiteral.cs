namespace Flux.Text
{
  /// <summary>The functionality of this class relates to U+hhhh style formatting.</summary>
  public static class UnicodeStringLiteral
  {
    public static readonly System.Text.RegularExpressions.Regex ParseRegex = new System.Text.RegularExpressions.Regex(@"((?<=\\u)[0-9a-f]{4}|(?<=\\U)[0-9A-F]{8,})", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

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

    /// <summary>Convert the Unicode codepoint to a string literal format, i.e. "\uxxxx" (four hex characters) or "\UXXXXXXXX" (eight hex characters).</summary>
    public static string ToString(int codePoint)
      => codePoint < 0 ? throw new System.ArgumentOutOfRangeException(nameof(codePoint)) : codePoint <= 0xFFFF ? $@"\u{codePoint:x4}" : $@"\U{codePoint:X8}";
  }
}
