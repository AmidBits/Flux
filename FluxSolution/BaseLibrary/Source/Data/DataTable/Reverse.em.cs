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

      for (var rowIndex = 0; rowIndex < source.Rows.Count; rowIndex++)
      {
        var values = new object[source.Columns.Count];
        System.Array.Copy(source.Rows[rowIndex].ItemArray, values, values.Length);
        System.Array.Reverse(values);
        target.Rows.Add(values);
      }

      return target;
    }

    /// <summary>Reverse the columns the data in-line.</summary>
    public static void ReverseColumnsInline(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (int columnIndex = source.Columns.Count - 1; columnIndex >= 0; columnIndex--)
        source.Columns[0].SetOrdinal(columnIndex);
    }

    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source data transposed (pivot).</summary>
    /// <param name="sourceColumnNames">Outputs the column names of the source data table.</param>
    /// <param name="targetColumnNames">If no target column names are specified, "Column_[ordinalIndex]" will be used.</param>
    public static System.Data.DataTable ReverseRows(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var target = new System.Data.DataTable(source.TableName);

      for (var index = 0; index < source.Columns.Count; index++)
        target.Columns.Add(source.Columns[index].ColumnName, source.Columns[index].DataType);

      for (var rowIndex = source.Rows.Count - 1; rowIndex >= 0; rowIndex--)
      {
        var values = new object[source.Columns.Count];
        System.Array.Copy(source.Rows[rowIndex].ItemArray, values, values.Length);
        target.Rows.Add(values);
      }

      return target;
    }

    /// <summary>Reverse the rows the data in-line.</summary>
    public static void ReverseRowsInline(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (int sourceRowIndex = 0, targetRowIndex = source.Rows.Count - 1; sourceRowIndex < targetRowIndex; sourceRowIndex++, targetRowIndex--)
      {
        var itemArray = source.Rows[sourceRowIndex].ItemArray;
        source.Rows[sourceRowIndex].ItemArray = source.Rows[targetRowIndex].ItemArray;
        source.Rows[targetRowIndex].ItemArray = itemArray;
      }
    }
  }
}
