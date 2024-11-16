namespace Flux
{
  namespace Text
  {
    /// <summary>The functionality of this class relates to U+xxxxx style formatting.</summary>
    public static partial class HtmlEntityNumber
    {
      [System.Text.RegularExpressions.GeneratedRegex(@"(?<Prefix>&#)(?<Codepoint>\d+)(?<Suffix>;)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)]
      private static partial System.Text.RegularExpressions.Regex RegexParseHtmlEntityNumbers();

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="text"></param>
      /// <param name="replacer"></param>
      /// <returns></returns>
      public static string ReplaceHtmlEntityNumber(this string text, System.Func<string, System.Text.Rune, string> replacer)
        => RegexParseHtmlEntityNumbers().Replace(text, m => replacer(m.Value, new System.Text.Rune(int.Parse(m.Value.AsSpan().Slice(2, m.Value.Length - 3), System.Globalization.NumberStyles.Number))));

      /// <summary>
      /// <para>Search for all HTML entity numbers in <paramref name="text"/>.</para>
      /// </summary>
      /// <param name="text"></param>
      /// <returns>The HTML entity, the rune, the index and count, for each found HTML entity number.</returns>
      public static System.Collections.Generic.IEnumerable<(string HtmlEntity, System.Text.Rune Rune, int Index, int Count)> SearchHtmlEntityNumber(this string text)
        => RegexParseHtmlEntityNumbers().Matches(text).Where(m => m.Success).Select(m => (m.Value, new System.Text.Rune(int.Parse(m.Value.AsSpan().Slice(2, m.Value.Length - 3), System.Globalization.NumberStyles.Number)), m.Index, m.Length));

      /// <summary>
      /// <para>Convert the <paramref name="character"/> to an HTML <c>&amp;#<i>entity_number</i>;</c> string.</para>
      /// </summary>
      /// <param name="character"></param>
      /// <returns></returns>
      public static string ToHtmlEntityNumberString(this char character) => $"&#{character};";

      /// <summary>
      /// <para>Convert the <paramref name="rune"/> (Unicode codepoint) to an HTML <c>&amp;#<i>entity_number</i>;</c> string.</para>
      /// </summary>
      /// <param name="rune"></param>
      /// <returns></returns>
      public static string ToHtmlEntityNumberString(this System.Text.Rune rune) => $"&#{rune.Value};";

      /// <summary>
      /// <para>Attempts to search for all HTML entity numbers in <paramref name="text"/>.</para>
      /// </summary>
      /// <param name="text"></param>
      /// <param name="result"></param>
      /// <returns>True if at least one HTML entity number was found.</returns>
      public static bool TrySearchHtmlEntityNumber(this string text, out System.Collections.Generic.List<(string HtmlEntity, System.Text.Rune Rune, int Index, int Count)> result)
      {
        try
        {
          result = SearchHtmlEntityNumber(text).ToList();
          return result.Count > 0;
        }
        catch { }

        result = default!;
        return false;
      }
    }
  }
}
