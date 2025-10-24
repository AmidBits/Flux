namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension(System.ReadOnlySpan<char> source)
    {
      #region Regex

      /// <summary>
      /// <para>Creates a new list of ranges from matching <paramref name="regexPattern"/> in <paramref name="source"/>.</para>
      /// </summary>
      /// <param name="regexPattern"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<Range> GetRegexMatches(string regexPattern)
      {
        var ranges = new System.Collections.Generic.List<Range>();
        foreach (var vm in new System.Text.RegularExpressions.Regex(regexPattern).EnumerateMatches(source))
          ranges.Add(System.Range.FromOffsetAndLength(vm.Index, vm.Length));
        return ranges;
      }

#if NET9_0_OR_GREATER

      /// <summary>
      /// <para>Creates a new list of ranges from splitting by <paramref name="regexPattern"/> in <paramref name="source"/>.</para>
      /// </summary>
      /// <param name="regexPattern"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<Range> GetRegexSplits(string regexPattern)
      {
        var ranges = new System.Collections.Generic.List<Range>();
        foreach (var range in new System.Text.RegularExpressions.Regex(regexPattern).EnumerateSplits(source))
          ranges.Add(range);
        return ranges;
      }

#endif

      #endregion
    }
  }
}
