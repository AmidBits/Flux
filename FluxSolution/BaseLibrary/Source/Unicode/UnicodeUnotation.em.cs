namespace Flux
{
  public static partial class Unicode
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Prefix>U\+)(?<Codepoint>[0-9A-Fa-f]{4,6})", System.Text.RegularExpressions.RegexOptions.Compiled)]
    public static partial System.Text.RegularExpressions.Regex RegexParseUnicodeUnotation();

    public static SpanMaker<char> UnicodeUnotationDecode(this System.ReadOnlySpan<char> source)
    {
      var sm = new SpanMaker<char>();

      var evm = RegexParseUnicodeUnotation().EnumerateMatches(source);

      var lastEnd = 0;

      foreach (var vm in evm)
      {
        var index = vm.Index;
        var length = vm.Length;

        sm.Append(source.Slice(lastEnd, index - lastEnd)); // Append any in-between characters.

        sm.Append(new System.Text.Rune(int.Parse(source.Slice(index + 2, length - 2), System.Globalization.NumberStyles.HexNumber)).ToString()); // Append the rune string.

        lastEnd = index + length;
      }

      return sm;
    }

    /// <summary>
    /// <para>Convert the character to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="asHexadecimal"></param>
    /// <returns></returns>
    public static string UnicodeUnotationEncode(this char source)
      => $"U+{(int)source:X4}";

    /// <summary>
    /// <para>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="asHexadecimal"></param>
    /// <returns></returns>
    public static string UnicodeUnotationEncode(this System.Text.Rune source)
      => $"U+{source.Value:X4}";

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static SpanMaker<char> UnicodeUnotationEncode(this System.ReadOnlySpan<char> source)
    {
      var sm = new SpanMaker<char>();
      foreach (var rune in source.EnumerateRunes())
        sm.Append(rune.UnicodeUnotationEncode());
      return sm;
    }

    ///// <summary>
    ///// <para>Search for all Unicode u-notation in <paramref name="text"/> using the <paramref name="replacer"/>.</para>
    ///// </summary>
    ///// <param name="text"></param>
    ///// <param name="replacer"></param>
    ///// <returns></returns>
    //public static string ReplaceUnicodeUnotation(this string text, System.Func<string, System.Text.Rune, string> replacer)
    //  => RegexParseUnicodeUnotation().Replace(text, m => replacer(m.Value, new System.Text.Rune(int.Parse(m.Value.AsSpan().Slice(2, m.Value.Length - 3), System.Globalization.NumberStyles.Number))));

    ///// <summary>
    ///// <para>Search for all Unicode u-notation in <paramref name="text"/>.</para>
    ///// </summary>
    ///// <param name="text"></param>
    ///// <returns>The HTML entity, the rune, the index and count, for each found HTML entity number.</returns>
    //public static System.Collections.Generic.IEnumerable<(string UnicodeNotation, System.Text.Rune Rune, int Index, int Count)> SearchUnicodeUnotation(this string text)
    //  => RegexParseUnicodeUnotation().Matches(text).Where(m => m.Success).Select(m => (m.Value, new System.Text.Rune(int.Parse(m.Value.AsSpan().Slice(2, m.Value.Length - 2), System.Globalization.NumberStyles.HexNumber)), m.Index, m.Length));

    ///// <summary>Convert the character to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    //public static string ToUnicodeUnotationString(this char character)
    //  => $"U+{(int)character:X4}";

    ///// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
    //public static string ToUnicodeUnotationString(this System.Text.Rune rune)
    //  => $"U+{rune.Value:X4}";

    ///// <summary>
    ///// <para>Attempts to search for all Unicode u-notation in <paramref name="text"/>.</para>
    ///// </summary>
    ///// <param name="text"></param>
    ///// <param name="result"></param>
    ///// <returns>True if at least one HTML entity number was found.</returns>
    //public static bool TrySearchUnicodeUnotation(this string text, out System.Collections.Generic.List<(string UnicodeNotation, System.Text.Rune Rune, int Index, int Count)> result)
    //{
    //  try
    //  {
    //    result = SearchUnicodeUnotation(text).ToList();
    //    return true;
    //  }
    //  catch { }

    //  result = default!;
    //  return false;
    //}
  }
}
