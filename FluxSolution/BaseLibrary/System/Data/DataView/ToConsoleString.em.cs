using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsDataView
  {
    public static System.Data.DataColumnCollection GetDataColumnCollection(this System.Data.DataView source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Table is null) throw new System.ArgumentException("The Table in the DataView is null.");

      return source.Table.Columns;
    }

    public static int[] MaxColumnWidths(this System.Data.DataView source, bool includeColumnNames, bool uniformWidths)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Table is null) throw new System.ArgumentException("The Table in the DataView is null.");
      if (source.Count == 0) throw new System.ArgumentOutOfRangeException(nameof(source), "The DataView is empty.");

      var maxColumnWidths = source.Table.Columns.Cast<System.Data.DataColumn>()
       .Select((e, i) => source.Cast<System.Data.DataRowView>().Max(dr => $"{dr[i]}".Length));

      if (includeColumnNames)
        maxColumnWidths = maxColumnWidths
          .Zip(source.Table.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName.Length))
          .Select(w => includeColumnNames ? System.Math.Max(w.First, w.Second) : w.First);

      var maxColumnWidth = maxColumnWidths.Max();

      return maxColumnWidths.Select(mw => uniformWidths ? System.Math.Max(mw, maxColumnWidth) : mw).ToArray();
    }

    /// <summary>Returns the data table as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings(this System.Data.DataView source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool includeColumnNames = true, bool uniformWidth = false, bool centerContent = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var horizontalSeparatorString = horizontalSeparator == '\0' ? null : horizontalSeparator.ToString();

      var maxColumnWidths = source.MaxColumnWidths(includeColumnNames, uniformWidth);

      var verticalLine = verticalSeparator == '\0' ? null : string.Join(horizontalSeparatorString, maxColumnWidths.Select(width => new string(verticalSeparator, width)));

      var horizontalLineFormat = string.Join(horizontalSeparatorString, maxColumnWidths.Select((width, index) => $"{{{index},-{width}}}"));

      if (includeColumnNames)
      {
        yield return string.Format(horizontalLineFormat, source!.Table!.Columns.Cast<System.Data.DataColumn>().Select((e, i) => centerContent ? e.ColumnName.ToStringBuilder().PadEven(maxColumnWidths[i], ' ', ' ').ToString() : e.ColumnName).ToArray());

        if (verticalLine is not null)
          yield return verticalLine;
      }

      for (var r = 0; r < source.Count; r++)
      {
        if (verticalLine is not null && r > 0)
          yield return verticalLine;

        var values = source[r].Row.ItemArray is var array && centerContent ? array.Select((e, i) => $"{e}".ToStringBuilder().PadEven(maxColumnWidths[i], ' ', ' ').ToString()).ToArray() : array;

        yield return string.Format(horizontalLineFormat, values);
      }
    }

    /// <summary>Returns the data table as a ready-to-print grid-like formatted string, that can be printed in the console.</summary>
    public static string ToConsoleString(this System.Data.DataView source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool includeColumnNames = true, bool uniformWidth = false, bool centerContent = false)
      => string.Join(System.Environment.NewLine, ToConsoleStrings(source, horizontalSeparator, verticalSeparator, includeColumnNames, uniformWidth));
  }
}
