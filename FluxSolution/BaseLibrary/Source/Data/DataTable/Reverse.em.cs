namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source data transposed (pivot).</summary>
    /// <param name="sourceColumnNames">Outputs the column names of the source data table.</param>
    /// <param name="targetColumnNames">If no target column names are specified, "Column_[ordinalIndex]" will be used.</param>
    public static System.Data.DataTable ReverseColumns(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var target = new System.Data.DataTable(source.TableName);

      for (var index = source.Columns.Count - 1; index >= 0; index--)
        target.Columns.Add(source.Columns[index].ColumnName, source.Columns[index].DataType);

      for (var index = source.Rows.Count-1; index >=0; index--)
      {
        var values = new object[source.Columns.Count];
        //for (var index = source.Columns.Count - 1; index >= 0; index--)
        //  values[columnIndex] = source.Rows[rowIndex][columnIndex];
        target.Rows.Add(values);
      }

      return target;
    }

    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source data transposed (pivot).</summary>
    /// <param name="sourceColumnNames">Outputs the column names of the source data table.</param>
    /// <param name="targetColumnNames">If no target column names are specified, "Column_[ordinalIndex]" will be used.</param>
    public static System.Data.DataTable ReverseRows(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var target = new System.Data.DataTable(source.TableName);

      //for (var index = 0; index < source.Rows.Count; index++)
      //  target.Columns.Add(index < targetColumnNames.Length ? targetColumnNames[index] : $"Column_{index + 1}"); // Add as many specified column names as available, then, if needed, create new column names.

      //for (var columnIndex = 0; columnIndex < source.Columns.Count; columnIndex++)
      //{
      //  var values = new object[columnNames.Length];
      //  for (var rowIndex = 0; rowIndex < values.Length; rowIndex++)
      //    values[rowIndex] = source.Rows[rowIndex][columnIndex];
      //  target.Rows.Add(values);
      //}

      return target;
    }
  }
}
