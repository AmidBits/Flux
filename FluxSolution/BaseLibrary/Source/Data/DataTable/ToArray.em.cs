namespace Flux
{
  public static partial class XtendData
  {
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static object[,] ToArray(this System.Data.DataTable source, bool includeColumnName)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var offset = includeColumnName ? 1 : 0;

      var array = new object[source.Rows.Count + offset, source.Columns.Count];

      if (includeColumnName)
      {
        for (var column = 0; column < source.Columns.Count; column++)
        {
          array[0, column] = source.Columns[column].ColumnName;
        }
      }

      for (var row = 0; row < source.Rows.Count; row++)
      {
        for (var column = 0; column < source.Columns.Count; column++)
        {
          array[row + offset, column] = source.Rows[row][column];
        }
      }

      return array;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
  }
}
