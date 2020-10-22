//namespace Flux
//{
//  public static partial class Xtensions
//  {
//    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source data transposed (pivot).</summary>
//    /// <param name="sourceColumnNames">Outputs the column names of the source data table.</param>
//    /// <param name="targetColumnNames">If no target column names are specified, "Column_[ordinalIndex]" will be used.</param>
//    public static System.Data.DataTable RotateLeft(this System.Data.DataTable source)
//    {
//      if (source is null) throw new System.ArgumentNullException(nameof(source));

//      var target = new System.Data.DataTable(source.TableName);

//      for (var index = source.Columns.Count - 1; index >= 0; index--)
//        target.Columns.Add(source.Columns[index].ColumnName, source.Columns[index].DataType);

//      for (var rowIndex = 0; rowIndex < source.Rows.Count; rowIndex++)
//      {
//        var values = new object[source.Columns.Count];
//        for (var columnIndex = source.Columns.Count - 1; columnIndex >= 0; columnIndex--)
//          values[columnIndex] = source.Rows[rowIndex][columnIndex];
//        System.Array.Reverse(values);
//        target.Rows.Add(values);
//      }

//      return target;
//    }

//    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source data transposed (pivot).</summary>
//    /// <param name="sourceColumnNames">Outputs the column names of the source data table.</param>
//    /// <param name="targetColumnNames">If no target column names are specified, "Column_[ordinalIndex]" will be used.</param>
//    public static System.Data.DataTable RotateRight(this System.Data.DataTable source)
//    {
//      if (source is null) throw new System.ArgumentNullException(nameof(source));

//      var target = new System.Data.DataTable(source.TableName);

//      for (var index = 0; index < source.Rows.Count; index++)
//        target.Columns.Add(source.Columns[index].ColumnName, source.Columns[index].DataType);

//      for (var rowIndex = source.Columns.Count - 1; rowIndex >= 0; rowIndex--)
//      {
//        var values = new object[source.Columns.Count];
//        for (var columnIndex = source.Columns.Count - 1; columnIndex >= 0; columnIndex--)
//          values[columnIndex] = source.Rows[rowIndex][columnIndex];
//        target.Rows.Add(values);
//      }

//      return target;
//    }
//  }
//}
