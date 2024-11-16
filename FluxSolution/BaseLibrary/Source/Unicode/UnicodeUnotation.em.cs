namespace Flux
{
  public static partial class Unicode
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Prefix>U\+)(?<Codepoint>[0-9A-F]{4,6})", System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex RegexParseUnicodeUnotation();

    /// <summary>
    /// <para>Search for all Unicode u-notation in <paramref name="text"/> using the <paramref name="replacer"/>.</para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="replacer"></param>
    /// <returns></returns>
    public static string ReplaceUnicodeUnotation(this string text, System.Func<string, System.Text.Rune, string> replacer)
      => RegexParseUnicodeUnotation().Replace(text, m => replacer(m.Value, new System.Text.Rune(int.Parse(m.Value.AsSpan().Slice(2, m.Value.Length - 3), System.Globalization.NumberStyles.Number))));

    /// <summary>
    /// <para>Search for all Unicode u-notation in <paramref name="text"/>.</para>
    /// </summary>
    /// <param name="text"></param>
    /// <returns>The HTML entity, the rune, the index and count, for each found HTML entity number.</returns>
    public static System.Collections.Generic.IEnumerable<(string UnicodeNotation, System.Text.Rune Rune, int Index, int Count)> SearchUnicodeUnotation(this string text)
      => RegexParseUnicodeUnotation().Matches(text).Where(m => m.Success).Select(m => (m.Value, new System.Text.Rune(int.Parse(m.Value.AsSpan().Slice(2, m.Value.Length - 2), System.Globalization.NumberStyles.Number)), m.Index, m.Length));

    /// <summary>Convert the character to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string ToUnicodeUnotationString(this char character) => $"U+{(int)character:X4}";

    /// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    public static string ToUnicodeUnotationString(this System.Text.Rune rune) => $"U+{rune.Value:X4}";

    /// <summary>
    /// <para>Attempts to search for all Unicode u-notation in <paramref name="text"/>.</para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="result"></param>
    /// <returns>True if at least one HTML entity number was found.</returns>
    public static bool TrySearchUnicodeUnotation(this string text, out System.Collections.Generic.List<(string UnicodeNotation, System.Text.Rune Rune, int Index, int Count)> result)
    {
      try
      {
        result = SearchUnicodeUnotation(text).ToList();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
