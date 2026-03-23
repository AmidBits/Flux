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

    extension(System.Text.RegularExpressions.Match match)
    {
      /// <summary>All expressions are unanchored (for now).</summary>
      public System.Collections.Generic.IDictionary<string, string> GetNamedGroups()
      {
        System.ArgumentNullException.ThrowIfNull(match);

        var dictionary = new System.Collections.Generic.SortedDictionary<string, string>();

        for (var index = 0; index < match.Groups.Count; index++)
        {
          var group = match.Groups[index];

          if (!group.Name.Equals(index.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.InvariantCulture))
            dictionary.Add(group.Name, group.Value);
        }

        return dictionary;
      }
    }
  }
}
