using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsDataView
  {
    public static int[] MaxColumnWidths(this System.Data.DataView source, bool includeColumnNames, bool uniformWidths)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Table is null) throw new System.ArgumentException("The Table in the DataView is null.");
      if (source.Count == 0) throw new System.ArgumentOutOfRangeException(nameof(source), "The DataView is empty.");

      var maxColumnWidths = source.Table.Columns.Cast<System.Data.DataColumn>()
       .Select((e, i) => source.Cast<System.Data.DataRowView>().Max(dr => $"{dr[i]}".Length));

      if (includeColumnNames)
      {
        maxColumnWidths = maxColumnWidths
          .Zip(source.Table.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName.Length))
          .Select(w => includeColumnNames ? System.Math.Max(w.First, w.Second) : w.First);
      }

      var maxColumnWidth = maxColumnWidths.Max();

      return maxColumnWidths.Select(mw => uniformWidths ? System.Math.Max(mw, maxColumnWidth) : mw).ToArray();
    }

    /// <summary>Returns the data table as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings(this System.Data.DataView source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool includeColumnNames = true, bool uniformWidths = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var maxColumnWidths = source.MaxColumnWidths(includeColumnNames, uniformWidths);

      var horizontalLineFormat = string.Join(horizontalSeparator, maxColumnWidths.Select((width, index) => $"{{{index},-{width}}}"));

      var verticalLine = string.Join(horizontalSeparator, maxColumnWidths.Select((e, i) => new string(verticalSeparator, e)));

      if (includeColumnNames)
        yield return string.Format(System.Globalization.CultureInfo.InvariantCulture, horizontalLineFormat, source!.Table!.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ToArray());

      for (var r = 0; r < source.Count; r++)
      {
        if (verticalSeparator != '\0' && (r > 0 || includeColumnNames))
          yield return verticalLine;

        yield return string.Format(horizontalLineFormat, source[r].Row.ItemArray);
      }
    }

    /// <summary>Returns the data table as a ready-to-print grid-like formatted string, that can be printed in the console.</summary>
    public static string ToConsoleString(this System.Data.DataView source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool includeColumnNames = true, bool uniformWidths = false)
      => string.Join(System.Environment.NewLine, ToConsoleStrings(source, horizontalSeparator, verticalSeparator, includeColumnNames, uniformWidths));
  }
}
