namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source data transposed (pivot).</summary>
    /// <param name="sourceColumnNames">Outputs the column names of the source data table.</param>
    /// <param name="targetColumnNames">If no target column names are specified, "Column_[ordinalIndex]" will be used.</param>
    public static System.Data.DataTable Transpose(this System.Data.DataTable source, out string sourceTableName, out string[] sourceColumnNames, string targetTableName, params string[] targetColumnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      sourceTableName = source.TableName;

      sourceColumnNames = new string[source.Columns.Count];
      for (var index = 0; index < sourceColumnNames.Length; index++)
        sourceColumnNames[index] = source.Columns[index].ColumnName;

      var columnNames = new string[source.Rows.Count];
      for (var index = 0; index < columnNames.Length; index++)
        columnNames[index] = index < targetColumnNames.Length ? targetColumnNames[index] : $"Column_{index + 1}"; // Add as many specified column names as available, then, if needed, create new column names.

      var target = new System.Data.DataTable();

      target.ExtendedProperties.Add(@"OriginalTableName", sourceTableName);
      target.ExtendedProperties.Add(@"OriginalColumnNames", sourceColumnNames);

      target.TableName = targetTableName;

      for (var index = 0; index < source.Rows.Count; index++)
        target.Columns.Add(index < targetColumnNames.Length ? targetColumnNames[index] : $"Column_{index + 1}"); // Add as many specified column names as available, then, if needed, create new column names.

      for (var columnIndex = 0; columnIndex < source.Columns.Count; columnIndex++)
      {
        var values = new object[columnNames.Length];
        for (var rowIndex = 0; rowIndex < values.Length; rowIndex++)
          values[rowIndex] = source.Rows[rowIndex][columnIndex];
        target.Rows.Add(values);
      }

      return target;
    }
  }
}
