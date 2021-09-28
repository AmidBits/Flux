namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source columns reversed (mirrored).</summary>
    public static System.Data.DataTable FlipColumns(this System.Data.DataTable source)
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

    /// <summary>Reverse the columns of the <see cref="System.Data.DataTable"/> in-line. The process re-orders the columns (using the SetOrdinal() method) within the data table.</summary>
    public static void FlipColumnsInline(this System.Data.DataTable source, int startIndex, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (startIndex < 0 || startIndex > source.Columns.Count - 2) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (count < 1 || startIndex + count > source.Columns.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

      for (int columnIndex = startIndex + count - 1; columnIndex >= startIndex; columnIndex--)
        source.Columns[startIndex].SetOrdinal(columnIndex);
    }

    /// <summary>Creates a new <see cref="System.Data.DataTable"/> containing the source data rows reversed (mirrored).</summary>
    public static System.Data.DataTable FlipRows(this System.Data.DataTable source)
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

    /// <summary>Reverse the rows of the <see cref="System.Data.DataTable"/> in-line. The process swaps itemArray's within the data table.</summary>
    public static void FlipRowsInline(this System.Data.DataTable source, int startIndex, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (startIndex < 0 || startIndex > source.Rows.Count - 2) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (count < 1 || startIndex + count > source.Rows.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

      for (int sourceRowIndex = startIndex, targetRowIndex = startIndex + count - 1; sourceRowIndex < targetRowIndex; sourceRowIndex++, targetRowIndex--)
      {
        var itemArray = source.Rows[sourceRowIndex].ItemArray;
        source.Rows[sourceRowIndex].ItemArray = source.Rows[targetRowIndex].ItemArray;
        source.Rows[targetRowIndex].ItemArray = itemArray;
      }
    }
  }
}
