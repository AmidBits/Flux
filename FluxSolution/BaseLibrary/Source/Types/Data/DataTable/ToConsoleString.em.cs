//using System.Linq;

//namespace Flux
//{
//  public static partial class ExtensionMethodsDataTable
//  {
//    /// <summary>Returns the data table as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
//    [System.Obsolete("Use the DefaultView property to convert a DataTable to a string.", true)]
//    public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings(this System.Data.DataTable source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false, bool includeColumnNames = true) { yield break; }
//    //{
//    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

//    //  var columns = source.Columns;
//    //  var rows = source.Rows;

//    //  var columnMaxWidths = System.Linq.Enumerable.Range(0, columns.Count).Select(i => System.Math.Max(includeColumnNames ? columns[i].ColumnName.Length : 0, rows.Cast<System.Data.DataRow>().Max(dr => $"{dr[i]}".Length))) is var mw && uniformMaxWidth ? mw.Select(w => mw.Max()).ToList() : mw.ToList();

//    //  var format = string.Join(horizontalSeparator, columnMaxWidths.Select((width, index) => $"{{{index},-{width}}}"));

//    //  var verticalSeparatorRow = verticalSeparator == '\0' ? null : string.Join(horizontalSeparator, columnMaxWidths.Select((width, index) => new string(verticalSeparator, width)));

//    //  if (includeColumnNames)
//    //    yield return string.Format(System.Globalization.CultureInfo.InvariantCulture, format, columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ToArray());

//    //  for (var row = 0; row < rows.Count; row++)
//    //  {
//    //    if (verticalSeparatorRow is not null && (row > 0 || includeColumnNames))
//    //      yield return string.Join(horizontalSeparator, columnMaxWidths.Select((width, index) => new string(verticalSeparator, width)));

//    //    yield return string.Format(System.Globalization.CultureInfo.InvariantCulture, format, rows[row].ItemArray);
//    //  }
//    //}
//    /// <summary>Returns the data table as a ready-to-print grid-like formatted string, that can be printed in the console.</summary>
//    [System.Obsolete("Use the DefaultView property to convert a DataTable to a string.", true)]
//    public static string ToConsoleString(this System.Data.DataTable source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false, bool includeColumnNames = true)
//      => string.Join(System.Environment.NewLine, ToConsoleStrings(source, horizontalSeparator, verticalSeparator, uniformMaxWidth, includeColumnNames));
//  }
//}
