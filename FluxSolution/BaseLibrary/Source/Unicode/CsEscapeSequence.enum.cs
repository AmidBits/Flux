namespace Flux
{
  public static partial class Unicode
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Prefix>\\u|\\x)(?<Number>[0-9a-f]{1,8})", System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
    private static partial System.Text.RegularExpressions.Regex RegexParseCsEscapeSequence();

    /// <summary>
    /// <para>Replace all C# escape sequences with the result of the <paramref name="replaceSelector"/>.</para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="replaceSelector"></param>
    /// <returns></returns>
    public static string ReplaceCsEscapeSequence(this string text, System.Func<string, System.Text.Rune, string> replaceSelector)
      => RegexParseCsEscapeSequence().Replace(text, m => replaceSelector(m.Value, new System.Text.Rune(int.Parse(m.Value.AsSpan()[2..m.Value.Length], m.Value.StartsWith(@"\x") ? System.Globalization.NumberStyles.HexNumber : System.Globalization.NumberStyles.Number))));

    /// <summary>
    /// <para>Search for all C# escape sequences in <paramref name="text"/>.</para>
    /// </summary>
    /// <param name="text"></param>
    /// <returns>The HTML entity, the rune, the index and count, for each found HTML entity number.</returns>
    public static System.Collections.Generic.IEnumerable<(string CsEscapeSequence, System.Text.Rune Rune, int Index, int Count)> SearchCsEscapeSequence(this string text)
      => RegexParseCsEscapeSequence().Matches(text).Where(m => m.Success).Select(m => (m.Value, new System.Text.Rune(int.Parse(m.Value.AsSpan()[2..m.Value.Length], m.Value.StartsWith(@"\x") ? System.Globalization.NumberStyles.HexNumber : System.Globalization.NumberStyles.Number)), m.Index, m.Length));

    /// <summary>
    /// <para>Convert the <paramref name="character"/> to a C# escape sequence, i.e. "\uhhhh" (four hex characters, for UTF-16 size), "\U00HHHHHH" (eight hex characters, for UTF-32 size), or "\x[H][H][H][H].</para>
    /// </summary>
    /// <param name="character"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string ToCsEscapeSequenceString(this char character, CsEscapeSequence format)
      => format switch
      {
        CsEscapeSequence.UTF16 => $@"\u{(int)character:X4}",
        CsEscapeSequence.UTF32 => $@"\U{(int)character:X8}",
        CsEscapeSequence.Variable => $@"\x{(int)character:X2}",
        _ => throw new NotImplementedException(),
      };

    /// <summary>
    /// <para>Convert the <paramref name="rune"/> (Unicode codepoint) to a C# escape sequence, i.e. "\uhhhh" (four hex characters, for UTF-16 size), "\U00HHHHHH" (eight hex characters, for UTF-32 size), or "\x[H][H][H][H].</para>
    /// </summary>
    /// <param name="rune"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string ToCsEscapeSequenceString(this System.Text.Rune rune, CsEscapeSequence format)
      => format switch
      {
        CsEscapeSequence.UTF16 => $@"\u{rune.Value:X4}",
        CsEscapeSequence.UTF32 => $@"\U{rune.Value:X8}",
        CsEscapeSequence.Variable => $@"\x{rune.Value:X2}",
        _ => throw new NotImplementedException(),
      };

    /// <summary>
    /// <para>Attempts to search for all C# escape sequences in <paramref name="text"/>.</para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="result"></param>
    /// <returns>True if at least one HTML entity number was found.</returns>
    public static bool TrySearchCsEscapeSequence(this string text, out System.Collections.Generic.List<(string CsEscapeSequence, System.Text.Rune Rune, int Index, int Count)> result)
    {
      try
      {
        result = SearchCsEscapeSequence(text).ToList();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }

  public enum CsEscapeSequence
  {
    /// <summary>\u = Unicode escape sequence (UTF-16) \uHHHH (range: 0000 - FFFF; example: \u00E7 = "ç")</summary>
    UTF16,
    /// <summary>\U = Unicode escape sequence (UTF-32) \U00HHHHHH (range: 000000 - 10FFFF; example: \U0001F47D)</summary>
    UTF32,
    /// <summary>\x = Unicode escape sequence similar to "\u" except with variable length \xH[H][H][H] (range: 0 - FFFF; example: \x00E7 or \x0E7 or \xE7 = "ç")</summary>
    Variable
  }
}
