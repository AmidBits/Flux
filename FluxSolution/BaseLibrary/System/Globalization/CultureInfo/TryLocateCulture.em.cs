namespace Flux
{
  public static partial class Fx
  {
    public static bool TryLocateCulture(this System.Globalization.CultureInfo source, System.Collections.Generic.IEnumerable<string> collection, out System.Collections.Generic.List<(int hierarchyLevel, int index, string text)> matches, out System.Data.DataTable dataTable)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      dataTable = new System.Data.DataTable("CultureLocator");

      dataTable.Columns.Add("Level", typeof(int));
      dataTable.Columns.Add("Index", typeof(int));
      dataTable.Columns.Add("Text", typeof(string));

      dataTable.DefaultView.Sort = "Level ASC, Index ASC";

      matches = new System.Collections.Generic.List<(int hierarchyLevel, int index, string text)>();

      using var e = collection.GetEnumerator();

      var index = 0;
      while (e.MoveNext())
      {
        if (source.TryMatchCulture(e.Current, out var hierarchy))
        {
          matches.Add((hierarchy, index, e.Current));
          dataTable.Rows.Add(hierarchy, index, e.Current);
        }

        index++;
      }

      matches.Sort();

      return (matches.Count > 0);
    }
  }
}
