using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static object[,] ToArray(this System.Data.DataTable source, int columnStartIndex, int columnCount, int rowStartIndex, int rowCount)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var array = new object[rowCount, columnCount];

      for (var row = rowStartIndex + rowCount - 1; row >= rowStartIndex; row--)
        for (var column = columnStartIndex + columnCount - 1; column >= columnStartIndex; column--)
          array[row - rowStartIndex, column - columnStartIndex] = source.Rows[row][column];

      return array;
    }
    public static object[,] ToArray(this System.Data.DataTable source, bool includeColumnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var rowStartIndex = includeColumnNames ? 1 : 0;

      var array = new object[rowStartIndex + source.Rows.Count, source.Columns.Count];

      if (includeColumnNames)
        for (var column = 0; column < source.Columns.Count; column++)
          array[0, column] = source.Columns[column].ColumnName;

      for (var row = source.Rows.Count - 1; row >= 0; row--)
        for (var column = source.Columns.Count - 1; column >= 0; column--)
          array[rowStartIndex + row, column] = source.Rows[row][column];

      return array;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

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
        if (!(verticalSeparatorRow is null) && (row > 0 || includeColumnNames))
          yield return string.Join(horizontalSeparator, columnMaxWidths.Select((width, index) => new string(verticalSeparator, width)));

        yield return string.Format(System.Globalization.CultureInfo.InvariantCulture, format, source.Rows[row].ItemArray);
      }
    }
    /// <summary>Returns the data table as a ready-to-print grid-like formatted string, that can be printed in the console.</summary>
    public static string ToConsoleString(this System.Data.DataTable source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false, bool includeColumnNames = true)
      => string.Join(System.Environment.NewLine, ToConsoleStrings(source, horizontalSeparator, verticalSeparator, uniformMaxWidth, includeColumnNames));

    /// <summary>Creates a new XDocument with the data from the DataTable.</summary>
    public static System.Xml.Linq.XDocument ToXDocument(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var xd = new System.Xml.Linq.XDocument();
      using (var xw = xd.CreateWriter())
        source.WriteXml(xw);
      return xd;
    }

    /// <summary>Creates a new XmlDocument with the data from the DataTable.</summary>
    public static System.Xml.XmlDocument ToXmlDocument(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var xd = new System.Xml.XmlDocument();
      using (var writer = xd.CreateNavigator().AppendChild())
        source.WriteXml(writer);
      return xd;
    }
  }
}
