namespace Flux
{
  public static partial class Fx
  {
    public static object[,] ToTwoDimensionalArray(this System.Data.DataTable source, bool includeColumnNames, int rowStartIndex, int rowCount, int columnStartIndex, int columnCount)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var offset = includeColumnNames ? 1 : 0;

      var array = new object[rowCount + offset, columnCount];

      if (includeColumnNames)
        for (var column = columnStartIndex + columnCount - 1; column >= columnStartIndex; column--)
          array[0, column - columnStartIndex] = source.Columns[column].ColumnName;

      for (var row = rowStartIndex + rowCount - 1; row >= rowStartIndex; row--)
        for (var column = columnStartIndex + columnCount - 1; column >= columnStartIndex; column--)
          array[row - rowStartIndex + offset, column - columnStartIndex] = source.Rows[row][column];

      return array;
    }

    public static object[,] ToTwoDimensionalArray(this System.Data.DataTable source, bool includeColumnNames)
      => source.ToTwoDimensionalArray(includeColumnNames, 0, source.Rows.Count, 0, source.Columns.Count);
  }
}
