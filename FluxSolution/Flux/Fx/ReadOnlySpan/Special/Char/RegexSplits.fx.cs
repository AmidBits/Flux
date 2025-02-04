namespace Flux
{
  public static partial class Fx
  {

#if NET9_0_OR_GREATER

    /// <summary>
    /// <para>Creates a new list of ranges and sub-strings in <paramref name="source"/> split by <paramref name="regexPattern"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<(System.Range Range, string Text)> RegexSplits(this System.ReadOnlySpan<char> source, string regexPattern)
    {
      var list = new System.Collections.Generic.List<(System.Range Range, string Text)>();
      foreach (var range in new System.Text.RegularExpressions.Regex(regexPattern).EnumerateSplits(source))
        list.Add((range, source[range].ToString()));
      return list;
    }

#endif

  }
}
