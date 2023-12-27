namespace Flux
{
  public static partial class Reflection
  {
    public static object?[][] ToJaggedArray(this System.Data.DataTable source, int columnStartIndex, int columnCount, int rowStartIndex, int rowCount)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var array = new object?[rowCount][];

      for (var row = rowStartIndex + rowCount - 1; row >= rowStartIndex; row--)
        array[row] = source.Rows[row].ItemArray.ToCopy(columnStartIndex, columnCount);

      return array;
    }

    public static object?[][] ToJaggedArray(this System.Data.DataTable source, bool includeColumnNames)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var rowStartIndex = includeColumnNames ? 1 : 0;

      var array = new object?[rowStartIndex + source.Rows.Count][];

      if (includeColumnNames)
        for (var column = 0; column < source.Columns.Count; column++)
          array[0] = source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ToArray();

      for (var row = source.Rows.Count - 1; row >= 0; row--)
        array[rowStartIndex + row] = source.Rows[row].ItemArray.ToArray();

      return array;
    }
  }
}
