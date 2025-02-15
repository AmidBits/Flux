namespace Flux
{
  public static partial class Fx
  {

#if NET9_0_OR_GREATER

    /// <summary>
    /// <para>Creates a new list of ranges and sub-strings in <paramref name="source"/> that's been split by <paramref name="regexPattern"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <returns></returns>
    public static DataStructures.OrderedDictionary<Slice, string> RegexSplits(this System.ReadOnlySpan<char> source, string regexPattern)
    {
      var dictionary = new DataStructures.OrderedDictionary<Slice, string>();
      foreach (var r in new System.Text.RegularExpressions.Regex(regexPattern).EnumerateSplits(source))
        if (new Slice(r, source.Length) is var slice)
          dictionary.Add(slice, source.Slice(slice).ToString());
      return dictionary;
    }

#endif

  }
}
