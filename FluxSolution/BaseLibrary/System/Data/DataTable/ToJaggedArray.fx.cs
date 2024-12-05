namespace Flux
{
  public static partial class Fx
  {
    public static object?[][] ToJaggedArray(this System.Data.DataTable source, bool includeColumnNames, int columnStartIndex, int columnCount, int rowStartIndex, int rowCount)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var arrayStartIndex = (includeColumnNames ? 1 : 0);

      var array = new object?[arrayStartIndex + rowCount][];

      if (includeColumnNames)
        array[0] = source.Columns.Cast<System.Data.DataColumn>().Skip(columnStartIndex).Take(columnCount).Select(dc => dc.ColumnName).ToArray();

      for (var index = 0; index < rowCount; index++)
        array[arrayStartIndex + index] = source.Rows[rowStartIndex + index].ItemArray.ToCopy(columnStartIndex, columnCount);

      return array;
    }

    public static object?[][] ToJaggedArray(this System.Data.DataTable source, bool includeColumnNames)
      => source.ToJaggedArray(includeColumnNames, 0, source.Columns.Count, 0, source.Rows.Count);
  }
}
