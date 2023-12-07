namespace Flux
{
  public static partial class Fx
  {
    public static object[,] To2dArray(this System.Data.DataTable source, int columnStartIndex, int columnCount, int rowStartIndex, int rowCount)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var array = new object[rowCount, columnCount];

      for (var row = rowStartIndex + rowCount - 1; row >= rowStartIndex; row--)
        for (var column = columnStartIndex + columnCount - 1; column >= columnStartIndex; column--)
          array[row - rowStartIndex, column - columnStartIndex] = source.Rows[row][column];

      return array;
    }

    public static object[,] To2dArray(this System.Data.DataTable source, bool includeColumnNames)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
  }
}
