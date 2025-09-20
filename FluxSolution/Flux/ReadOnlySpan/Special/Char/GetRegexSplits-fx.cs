namespace Flux
{
  public static partial class ReadOnlySpans
  {

#if NET9_0_OR_GREATER

    /// <summary>
    /// <para>Creates a new list of ranges and sub-strings in <paramref name="source"/> that's been split by <paramref name="regexPattern"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <returns></returns>
    public static DataStructures.OrderedDictionary<Range, string> GetRegexSplits(this System.ReadOnlySpan<char> source, string regexPattern)
    {
      var dictionary = new DataStructures.OrderedDictionary<Range, string>();
      foreach (var range in new System.Text.RegularExpressions.Regex(regexPattern).EnumerateSplits(source))
        dictionary.Add(range, source[range].ToString());
      return dictionary;
    }

#endif

  }
}
