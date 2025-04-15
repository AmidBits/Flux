namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Tries to match a <paramref name="text"/> to a culture in the <paramref name="source"/> hierarchy. Also returns the <paramref name="hierarchyLevel"/> where a match was found, -1 if not found.</para>
    /// </summary>
    /// <param name="source">The culture hierarchy to compare against.</param>
    /// <param name="text">The name to match.</param>
    /// <param name="hierarchyLevel">How far up the chain the match was found.</param>
    /// <returns>Whether a match was found and also the 'distance' (<paramref name="hierarchyLevel"/>) from the <paramref name="source"/> the match was found (-1 if not found).</returns>
    public static bool TryMatchCulture(this System.Globalization.CultureInfo source, string text, out int hierarchyLevel)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      for (hierarchyLevel = 0; source != System.Globalization.CultureInfo.InvariantCulture; hierarchyLevel++)
      {
        if (System.Text.RegularExpressions.Regex.IsMatch(text, @"(?<=(^|[^\p{L}]))" + source.Name.Replace('-', '.') + @"(?=([^\p{L}]|$))", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
          return true;

        source = source.Parent;
      }

      hierarchyLevel = -1;
      return false;
    }

    /// <summary>
    /// <para>Creates a <paramref name="dataTable"/> with text, hierarchy-level and index (within the array) for any successful matches of <paramref name="source"/> in an array of <paramref name="texts"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="dataTable"></param>
    /// <param name="texts"></param>
    /// <returns></returns>
    public static bool TryLocateCulture(this System.Globalization.CultureInfo source, out System.Data.DataTable dataTable, params string[] texts)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      dataTable = new System.Data.DataTable("CultureLocator");

      dataTable.Columns.Add("Text", typeof(string));
      dataTable.Columns.Add("Level", typeof(int));
      dataTable.Columns.Add("Index", typeof(int));

      dataTable.DefaultView.Sort = "Level ASC, Index ASC";

      var index = 0;

      foreach (var text in texts)
      {
        if (source.TryMatchCulture(text, out var hierarchy))
          dataTable.Rows.Add(text, hierarchy, index);

        index++;
      }

      return dataTable.Rows.Count > 0;
    }
  }
}
