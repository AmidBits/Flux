namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Removes all occurrences of <paramref name="regexPattern"/> in <paramref name="source"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <returns></returns>
    public static SpanMaker<char> RemoveRegex(this SpanMaker<char> source, string regexPattern)
    {
      var sm = source;
      var mrs = sm.AsReadOnlySpan().RegexMatches(regexPattern);
      for (var i = mrs.Count - 1; i >= 0; i--)
        sm = sm.Remove(mrs[i].Range);
      return sm;
    }
  }
}
