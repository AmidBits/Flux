namespace Flux
{
  namespace Text
  {
    /// <summary>The functionality of this class relates to U+xxxxx style formatting.</summary>
    public static partial class HtmlEntityNumber
    {
#if NET7_0_OR_GREATER
      [System.Text.RegularExpressions.GeneratedRegex(@"(?<=&#)\d+(?=;)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)] private static partial System.Text.RegularExpressions.Regex RegexParse();
#else
    private static System.Text.RegularExpressions.Regex RegexParse() => new(@"(?<=&#)\d+(?=;)");
#endif

      public static System.Collections.Generic.IEnumerable<System.Text.Rune> Parse(string text)
        => System.Linq.Enumerable.Select(System.Linq.Enumerable.Where(RegexParse().Matches(text), m => m.Success), m => new System.Text.Rune(int.Parse(m.Value, System.Globalization.NumberStyles.Number)));

      public static bool TryParse(string text, out System.Collections.Generic.List<System.Text.Rune> result)
      {
        try
        {
          result = System.Linq.Enumerable.ToList(Parse(text));
          return true;
        }
        catch { }

        result = default!;
        return false;
      }

      /// <summary>Convert the Unicode codepoint to the string representation format "U+XXXX" (at least 4 hex characters, more if needed).</summary>
      public static string ToString(this System.Text.Rune rune)
        => $"&#{rune.Value};";
    }
  }
}
