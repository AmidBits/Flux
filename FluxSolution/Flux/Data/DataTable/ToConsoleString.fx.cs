namespace Flux
{
  public static partial class Fx
  {
    //public static int[] MaxColumnWidths(this System.Data.DataView source, ConsoleStringOptions? options = null)
    //{
    //  System.ArgumentNullException.ThrowIfNull(source);
    //  System.ArgumentNullException.ThrowIfNull(source.Table);

    //  options ??= new ConsoleStringOptions();

    //  if (source.Count == 0) throw new System.ArgumentOutOfRangeException(nameof(source), "The DataView is empty.");

    //  var maxColumnWidths = source.Table.Columns.Cast<System.Data.DataColumn>().Select((e, i) => source.Cast<System.Data.DataRowView>().Max(dr => $"{dr[i]}".Length));

    //  if (options.IncludeColumnNames)
    //    maxColumnWidths = maxColumnWidths
    //      .Zip(source.Table.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName.Length))
    //      .Select(w => options.IncludeColumnNames ? System.Math.Max(w.First, w.Second) : w.First);

    //  var maxColumnWidth = maxColumnWidths.Max();

    //  return maxColumnWidths.Select(mw => options.UniformWidth ? System.Math.Max(mw, maxColumnWidth) : mw).ToArray();
    //}

    /// <summary>Returns the data table as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static string ToConsoleString(this System.Data.DataTable source, ConsoleFormatOptions? options = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      options ??= ConsoleFormatOptions.Default;

      if (source.Rows.Count == 0) throw new System.ArgumentOutOfRangeException(nameof(source), "The DataView is empty.");

      var sm = new SpanMaker<char>();

      var horizontalSeparatorString = options.HorizontalSeparator?.ToString();

      #region MaxWidths

      var maxWidths = new int[source.Columns.Count];

      for (var r = source.Rows.Count - 1; r >= 0; r--)
        for (var c = maxWidths.Length - 1; c >= 0; c--)
          maxWidths[c] = int.Max(maxWidths[c], $"{source.Rows[r][c]}".Length);

      if (options.IncludeColumnNames)
        for (var c = maxWidths.Length - 1; c >= 0; c--)
          maxWidths[c] = int.Max(maxWidths[c], source.Columns[c].ColumnName.Length);

      var maxWidth = maxWidths.Max();

      if (options.UniformWidth)
        for (var c = maxWidths.Length - 1; c >= 0; c--)
          maxWidths[c] = maxWidth;

      #endregion // MaxWidths

      var verticalLine = options.VerticalSeparator is null ? null : string.Join(horizontalSeparatorString, maxWidths.Select(width => options.VerticalSeparator.ToSpanMaker().PadRight(width, options.VerticalSeparator).ToString()));

      var horizontalLineFormat = string.Join(horizontalSeparatorString, maxWidths.Select((width, index) => $"{{{index},-{width}}}"));

      if (options.IncludeColumnNames)
      {
        sm = sm.AppendLine(string.Format(horizontalLineFormat, source.Columns.Cast<System.Data.DataColumn>().Select((e, i) => options.CenterContent ? new SpanMaker<char>(e.ColumnName).PadEven(maxWidths[i], " ", " ").ToString() : e.ColumnName).ToArray()));

        if (verticalLine is not null)
          sm = sm.AppendLine(verticalLine);
      }

      for (var r = 0; r < source.Rows.Count; r++)
      {
        if (verticalLine is not null && r > 0)
          sm = sm.AppendLine(verticalLine);

        var values = source.Rows[r].ItemArray is var array && options.CenterContent ? array.Select((e, i) => new SpanMaker<char>($"{e}").PadEven(maxWidths[i], ' ', ' ').ToString()).ToArray() : array;

        sm = sm.AppendLine(string.Format(horizontalLineFormat, values));
      }

      return sm.ToString();
    }
  }
}
