namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Data.DataTable"/> containing the <paramref name="source"/> data rotated (clockwise) to the right.</para>
    /// </summary>
    /// <param name="sourceColumnNames">Outputs the column names of the source data table.</param>
    /// <param name="targetColumnNames">If less target column names than needed are specified, "Column_[ordinalIndex]" will be used.</param>
    public static System.Data.DataTable RotateToCopyCw(this System.Data.DataTable source, out string[] sourceColumnNames, params string[] targetColumnNames)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      sourceColumnNames = new string[source.Columns.Count];
      for (var index = sourceColumnNames.Length - 1; index >= 0; index--)
        sourceColumnNames[index] = source.Columns[index].ColumnName;

      var target = new System.Data.DataTable(source.TableName);

      for (var index = 0; index < source.Rows.Count; index++)
        target.Columns.Add(targetColumnNames.EnsureColumnNames(index));

      for (var columnIndex = 0; columnIndex < source.Columns.Count; columnIndex++)
      {
        var itemArray = new object[source.Rows.Count];
        for (var rowIndex = source.Rows.Count - 1; rowIndex >= 0; rowIndex--)
          itemArray[source.Rows.Count - 1 - rowIndex] = source.Rows[rowIndex][columnIndex];
        target.Rows.Add(itemArray);
      }

      return target;
    }
  }
}
