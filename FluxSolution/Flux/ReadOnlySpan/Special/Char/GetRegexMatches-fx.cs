namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Creates a new list of ranges and sub-strings in <paramref name="source"/> matching <paramref name="regexPattern"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <returns></returns>
    public static DataStructures.OrderedDictionary<Range, string> GetRegexMatches(this System.ReadOnlySpan<char> source, string regexPattern)
    {
      var dictionary = new DataStructures.OrderedDictionary<Range, string>();
      foreach (var vm in new System.Text.RegularExpressions.Regex(regexPattern).EnumerateMatches(source))
        if (System.Range.FromOffsetAndLength(vm.Index, vm.Length) is var range)
          dictionary.Add(range, source[range].ToString());
      return dictionary;
    }
  }
}
