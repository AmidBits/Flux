namespace Flux
{
  public static class RegularExpressionsExtensions
  {
    extension(System.Text.RegularExpressions.Regex)
    {
      /// <summary>
      /// <para>Creates a new <see cref="System.Text.RegularExpressions.Regex"/> so that it matches the last occurrence of the specified <paramref name="pattern"/>.</para>
      /// </summary>
      /// <param name="pattern"></param>
      /// <returns></returns>
      public static System.Text.RegularExpressions.Regex CreateToMatchLastOccurrence(string pattern)
        => new(@$"(?!(?s:.*){pattern})");
    }

    extension(System.Text.RegularExpressions.Match source)
    {
      /// <summary>All expressions are unanchored (for now).</summary>
      public System.Collections.Generic.IDictionary<string, string> GetNamedGroups()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var dictionary = new System.Collections.Generic.SortedDictionary<string, string>();

        for (var index = 0; index < source.Groups.Count; index++)
        {
          var group = source.Groups[index];

          if (!group.Name.Equals(index.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.InvariantCulture))
            dictionary.Add(group.Name, group.Value);
        }

        return dictionary;
      }
    }
  }
}
