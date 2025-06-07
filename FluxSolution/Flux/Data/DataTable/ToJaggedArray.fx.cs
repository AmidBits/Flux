namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new jagged array with the specified section from the <paramref name="source"/> <see cref="System.Data.DataTable"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="includeColumnNames">Whether to include column names as the first row.</param>
    /// <param name="columnStartIndex"></param>
    /// <param name="columnCount"></param>
    /// <param name="rowStartIndex"></param>
    /// <param name="rowCount"></param>
    /// <returns></returns>
    public static object?[][] ToJaggedArray(this System.Data.DataTable source, bool includeColumnNames, int columnStartIndex, int columnCount, int rowStartIndex, int rowCount)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var arrayStartIndex = includeColumnNames ? 1 : 0;

      var array = new object?[arrayStartIndex + rowCount][];

      if (includeColumnNames)
        array[0] = source.Columns.Cast<System.Data.DataColumn>().Skip(columnStartIndex).Take(columnCount).Select(dc => dc.ColumnName).ToArray();

      for (var index = 0; index < rowCount; index++)
        array[arrayStartIndex + index] = source.Rows[rowStartIndex + index].ItemArray.ToCopy(columnStartIndex, columnCount);

      return array;
    }

    /// <summary>
    /// <para>Creates a new jagged array from the <paramref name="source"/> <see cref="System.Data.DataTable"/>.</para>
    /// <para>This function do NOT make copies of each <see cref="System.Data.DataRow.ItemArray"/> in <paramref name="source"/>. The new jagged array points to the same arrays as the <see cref="System.Data.DataTable"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="includeColumnNames">Whether to include column names as the first row.</param>
    /// <returns></returns>
    public static object?[][] ToJaggedArray(this System.Data.DataTable source, bool includeColumnNames)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var arrayStartIndex = includeColumnNames ? 1 : 0;

      var rowCount = source.Rows.Count;

      var array = new object?[rowCount][];

      if (includeColumnNames)
        array[0] = [.. source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName)];

      for (var index = 0; index < rowCount; index++)
        array[arrayStartIndex + index] = source.Rows[index].ItemArray;

      return array;
    }
  }
}
