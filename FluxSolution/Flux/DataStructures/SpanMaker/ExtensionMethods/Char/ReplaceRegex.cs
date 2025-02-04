namespace Flux
{
  public static partial class Em
  {
#if NET9_0_OR_GREATER

    /// <summary>
    /// <para>Replaces all occurences in <paramref name="source"/> matching <paramref name="regexPattern"/> with the result from <paramref name="replacementSelector"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <param name="replacementSelector"></param>
    /// <returns></returns>
    public static SpanMaker<char> ReplaceRegex(this SpanMaker<char> source, string regexPattern, System.Func<System.ReadOnlySpan<char>, System.ReadOnlySpan<char>> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      var mrs = source.AsReadOnlySpan().RegexMatches(regexPattern);

      for (var i = mrs.Count - 1; i >= 0; i--)
      {
        var (range, text) = mrs[i];

        var replacement = replacementSelector(text);

        source = source.Remove(range);
        source = source.Insert(range.Start.Value, 1, replacement);
      }

      return source;
    }

#else

    public static SpanMaker<char> ReplaceRegex( this SpanMaker<char> source, string pattern, System.Func<string, string> replacementSelector)
    {
      var vms = source.AsReadOnlySpan().RegexMatches(pattern);

      for (var i = vms.Count - 1; i >= 0; i--)
      {
        var (range, text) = vms[i];

        var replacement = replacementSelector(source.AsReadOnlySpan()[range].ToString());

        source = source.Remove(range);
        source = source.Insert(range.Start.Value, 1, replacement);
      }

      return source;
    }

#endif

    /// <summary>
    /// <para>Replaces all occurences in <paramref name="source"/> matching <paramref name="regexPattern"/> with <paramref name="replacement"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public static SpanMaker<char> ReplaceRegex(this SpanMaker<char> source, string regexPattern, System.ReadOnlySpan<char> replacement)
    {
      var sm = source;
      var mrs = sm.AsReadOnlySpan().RegexMatches(regexPattern);
      for (var i = mrs.Count - 1; i >= 0; i--)
      {
        var range = mrs[i].Range;
        sm = sm.Remove(range);
        sm = sm.Insert(range.Start.Value, 1, replacement);
      }
      return sm;
    }
  }
}
