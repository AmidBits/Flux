using System.Linq;

namespace Flux
{
  public static partial class SystemDataEm
  {
    /// <summary>Returns the data table as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings(this System.Data.DataTable source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false, bool includeColumnNames = true)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var columnMaxWidths = System.Linq.Enumerable.Range(0, source.Columns.Count).Select(i => includeColumnNames ? System.Math.Max(source.Columns[i].ColumnName.Length, source.Rows.Cast<System.Data.DataRow>().Max(dr => $"{dr[i]}".Length)) : source.Rows.Cast<System.Data.DataRow>().Max(dr => $"{dr[i]}".Length));
      if (uniformMaxWidth) columnMaxWidths = columnMaxWidths.Select(w => columnMaxWidths.Max()).ToArray(); // If used, replace all with total max width.

      var format = string.Join(horizontalSeparator, columnMaxWidths.Select((width, index) => $"{{{index},-{width}}}"));

      var verticalSeparatorRow = verticalSeparator == '\0' ? null : string.Join(horizontalSeparator, columnMaxWidths.Select((width, index) => new string(verticalSeparator, width)));

      if (includeColumnNames)
        yield return string.Format(System.Globalization.CultureInfo.InvariantCulture, format, source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ToArray());

      for (var row = 0; row < source.Rows.Count; row++)
      {
        if (verticalSeparatorRow is not null && (row > 0 || includeColumnNames))
          yield return string.Join(horizontalSeparator, columnMaxWidths.Select((width, index) => new string(verticalSeparator, width)));

        yield return string.Format(System.Globalization.CultureInfo.InvariantCulture, format, source.Rows[row].ItemArray);
      }
    }
    /// <summary>Returns the data table as a ready-to-print grid-like formatted string, that can be printed in the console.</summary>
    public static string ToConsoleString(this System.Data.DataTable source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false, bool includeColumnNames = true)
      => string.Join(System.Environment.NewLine, ToConsoleStrings(source, horizontalSeparator, verticalSeparator, uniformMaxWidth, includeColumnNames));
  }
}
