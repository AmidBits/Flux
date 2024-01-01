namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Tries to match a <paramref name="name"/> to a culture in the <paramref name="source"/> hierarchy. Also returns the <paramref name="hierarchyLevel"/> where a match was found, -1 if not found.</para>
    /// </summary>
    /// <param name="source">The culture hierarchy to compare against.</param>
    /// <param name="name">The name to match.</param>
    /// <param name="hierarchyLevel">How far up the chain the match was found.</param>
    /// <returns>Whether a match was found and also the 'distance' (<paramref name="hierarchyLevel"/>) from the <paramref name="source"/> the match was found (-1 if not found).</returns>
    public static bool TryMatchCulture(this System.Globalization.CultureInfo source, string name, out int hierarchyLevel)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      for (hierarchyLevel = 0; source != System.Globalization.CultureInfo.InvariantCulture; hierarchyLevel++)
      {
        if (System.Text.RegularExpressions.Regex.IsMatch(name, @"(?<=(^|[^\p{L}]))" + source.Name.Replace('-', '.') + @"(?=([^\p{L}]|$))", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
          return true;

        source = source.Parent;
      }

      hierarchyLevel = -1;
      return false;
    }
  }
}
