namespace Flux
{
  public static class RegularExpressionsExtensions
  {
    extension(System.Text.RegularExpressions.Regex)
    {
      /// <summary>
      /// <para>Creates a balanced expression [<paramref name="openPattern"/>][<paramref name="matchPattern"/>][<paramref name="closePattern"/>]</para>
      /// </summary>
      /// <param name="openPattern">A pattern matched on the left side of <paramref name="matchPattern"/>.</param>
      /// <param name="matchPattern">A pattern matched between the <paramref name="openPattern"/> and <paramref name="closePattern"/>.</param>
      /// <param name="closePattern">A pattern matched on the right side of <paramref name="matchPattern"/>.</param>
      /// <returns></returns>
      public static System.Text.RegularExpressions.Regex CreateBalancedConstruct(System.ReadOnlySpan<char> openPattern, System.ReadOnlySpan<char> matchPattern, System.ReadOnlySpan<char> closePattern)
        => new($"^{matchPattern}*(?>(?>(?'balance'{openPattern}){matchPattern}*)+(?>(?'-balance'{closePattern}){matchPattern}*)+)+(?(balance)(?!))$");

      /// <summary>
      /// <para>Creates a new <see cref="System.Text.RegularExpressions.Regex"/> so that it matches the last occurrence of the specified <paramref name="pattern"/>.</para>
      /// </summary>
      /// <param name="pattern"></param>
      /// <returns></returns>
      public static System.Text.RegularExpressions.Regex CreateAsMatchingLastOccurrence(string pattern)
        => new(@$"(?!(?s:.*){pattern})");
    }

    extension(System.Text.RegularExpressions.Regex regex)
    {
      /// <summary>
      /// <para>Creates a list of <see cref="System.Range"/>s of all matches of a <see cref="System.Text.RegularExpressions.Regex"/> in an <paramref name="input"/>. Optionally in <paramref name="reverse"/> order.</para>
      /// </summary>
      /// <param name="regexPattern"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<Range> FindMatches(System.ReadOnlySpan<char> input, bool reverse = false)
      {
        var ranges = new System.Collections.Generic.List<Range>();
        foreach (var vm in regex.EnumerateMatches(input))
          ranges.Add(System.Range.FromOffsetAndLength(vm.Index, vm.Length));
        if (reverse)
          ranges.Reverse();
        return ranges;
      }

      /// <summary>
      /// <para>Creates a list of <see cref="System.Range"/>s of all splits of a <see cref="System.Text.RegularExpressions.Regex"/> in am <paramref name="input"/>. Optionally in <paramref name="reverse"/> order.</para>
      /// </summary>
      /// <param name="regexPattern"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<Range> FindSplits(System.ReadOnlySpan<char> input, bool reverse = false)
      {
        var ranges = new System.Collections.Generic.List<Range>();
        foreach (var range in regex.EnumerateSplits(input))
          ranges.Add(range);
        if (reverse)
          ranges.Reverse();
        return ranges;
      }
    }

    extension(System.Text.RegularExpressions.Match match)
    {
      /// <summary>
      /// <para>Creates a <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/> with explicitly named groups in a <see cref="System.Text.RegularExpressions.Match"/>.</para>
      /// </summary>
      /// <remarks>A <see cref="System.Text.RegularExpressions.Regex"/> pattern cannot contain groups with names that consist entirely of digits. Numeric group names therefore represent unnamed groups and are excluded.</remarks>
      /// <returns></returns>
      public System.Collections.Generic.IDictionary<string, System.Text.RegularExpressions.Group> GetNamedGroups()
      {
        System.ArgumentNullException.ThrowIfNull(match);

        var dictionary = new System.Collections.Generic.OrderedDictionary<string, System.Text.RegularExpressions.Group>();

        foreach (var group in match.Groups.Cast<System.Text.RegularExpressions.Group>().Skip(1))
          if (!int.TryParse(group.Name, out _))
            dictionary.Add(group.Name, group);

        return dictionary;
      }
    }
  }
}
