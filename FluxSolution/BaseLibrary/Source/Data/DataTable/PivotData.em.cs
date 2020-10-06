namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source data pivoted.</summary>
    /// <param name="sourceColumnNames">Outputs the column names of the source data table.</param>
    /// <param name="targetColumnNames">If no target column names are specified, "Column_[ordinalIndex]" will be used.</param>
    public static System.Data.DataTable PivotData(this System.Data.DataTable source, out string[] sourceColumnNames, params string[] targetColumnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      sourceColumnNames = new string[source.Columns.Count];
      for (var index = 0; index < sourceColumnNames.Length; index++)
        sourceColumnNames[index] = source.Columns[index].ColumnName;

      var columnNames = new string[source.Rows.Count];

      if (targetColumnNames != null)
        for (var index = 0; index < targetColumnNames.Length; index++)
          columnNames[index] = targetColumnNames[index];

      for (var index = targetColumnNames?.Length ?? 0; index < source.Rows.Count; index++)
        columnNames[index] = $"Column_{index + 1}";

      var target = new System.Data.DataTable();

      foreach (var columnName in columnNames)
        target.Columns.Add(columnName);

      for (var columnIndex = 0; columnIndex < source.Columns.Count; columnIndex++)
      {
        var values = new object[columnNames.Length];
        for (var rowIndex = 0; rowIndex < values.Length; rowIndex++)
          values[rowIndex] = source.Rows[rowIndex][columnIndex];
        target.Rows.Add(values);
      }

      target.ExtendedProperties.Add($"{nameof(PivotData)}.ColumnNames", sourceColumnNames);

      return target;
    }
    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source data pivoted.</summary>
    /// <param name="targetColumnNames">If no target column names are specified, "Column_[ordinalIndex]" will be used.</param>
    public static System.Data.DataTable PivotData(this System.Data.DataTable source, params string[] targetColumnNames)
      => source.PivotData(out var sourceColumnNames, targetColumnNames);
  }
}
