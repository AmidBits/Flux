namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Data.DataTable"/> containing the <paramref name="source"/> data rotated (counter-clockwise) to the left.</para>
    /// </summary>
    /// <param name="targetColumnNames">If less target column names than needed are specified, "Column[ordinalIndex]" will be used.</param>
    public static System.Data.DataTable RotateToCopyCcw(this System.Data.DataTable source, params string[] targetColumnNames)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var rowCount = source.Rows.Count;
      var columnCount = source.Columns.Count;

      var target = new System.Data.DataTable(source.TableName);

      for (var index = 0; index < rowCount; index++)
        target.Columns.Add(targetColumnNames.EnsureColumnName(index), typeof(string));

      for (var columnIndex = columnCount - 1; columnIndex >= 0; columnIndex--)
      {
        var itemArray = new object[rowCount];
        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
          itemArray[rowIndex] = source.Rows[rowIndex][columnIndex];
        target.Rows.Add(itemArray);
      }

      return target;
    }
  }
}
