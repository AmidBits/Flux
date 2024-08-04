namespace Flux
{
  public static partial class Unicode
  {
#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<=U\+)[0-9A-F]{4,6}", System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex RegexParseUnicodeUnotation();
#else
    private static System.Text.RegularExpressions.Regex RegexParseUnicodeUnotation() => new(@"(?<=U\+)[0-9A-F]{4,6}");
#endif

    public static System.Collections.Generic.List<char> ParseUnicodeUnotation(this string text)
    {
      var list = new System.Collections.Generic.List<char>();

      foreach (var match in RegexParseUnicodeUnotation().Matches(text).Cast<System.Text.RegularExpressions.Match>())
        if (match.Success)
          list.Add((char)int.Parse(match.Value, System.Globalization.NumberStyles.HexNumber, null));

      return list;
    }

    public static bool TryParseUnicodeUnotation(this string text, out System.Collections.Generic.List<char> result)
    {
      try
      {
        result = ParseUnicodeUnotation(text);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Convert the character to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string ToUnicodeUnotationString(this char character) => $"U+{(int)character:X4}";

    /// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string ToUnicodeUnotationString(this System.Text.Rune rune) => $"U+{rune.Value:X4}";
  }
}
