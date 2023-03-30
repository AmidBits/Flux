namespace Flux
{
  public static partial class ExtensionMethodsDataTable
  {
    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source data rotated left.</summary>
    /// <param name="sourceColumnNames">Outputs the column names of the source data table.</param>
    /// <param name="targetColumnNames">If less target column names than needed are specified, "Column_[ordinalIndex]" will be used.</param>
    public static System.Data.DataTable RotateCounterClockwise(this System.Data.DataTable source, out string[] sourceColumnNames, params string[] targetColumnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      sourceColumnNames = new string[source.Columns.Count];
      for (var index = sourceColumnNames.Length - 1; index >= 0; index--)
        sourceColumnNames[index] = source.Columns[index].ColumnName;

      var target = new System.Data.DataTable(source.TableName);

      for (var index = 0; index < source.Rows.Count; index++)
        target.Columns.Add(targetColumnNames.AsColumnName(index));

      for (var columnIndex = source.Columns.Count - 1; columnIndex >= 0; columnIndex--)
      {
        var itemArray = new object[source.Rows.Count];
        for (var rowIndex = 0; rowIndex < source.Rows.Count; rowIndex++)
          itemArray[rowIndex] = source.Rows[rowIndex][columnIndex];
        target.Rows.Add(itemArray);
      }

      return target;
    }
  }
}
