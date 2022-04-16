using System.Linq;

namespace Flux
{
  public static partial class IDataReaderEm
  {
    /// <summary>Creates a new data table with values from the IDataReader. The specified table name is used in constructing the data table.</summary>
    public static System.Data.DataTable ToDataTable(this System.Data.IDataReader source, string tableName)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var dt = new System.Data.DataTable(tableName);

      for (var index = 0; index < source.FieldCount; index++)
        dt.Columns.Add(source.GetNameEx(index));

      dt.Load(source);

      return dt;
    }

    /// <summary>Creates a new data table from the IDataReader using the specified column names.</summary>
    public static System.Data.DataTable ToDataTable(this System.Data.IDataReader source, string tableName, System.Collections.Generic.IEnumerable<string> columnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var dt = new System.Data.DataTable(tableName);

      foreach (var columnName in columnNames)
        dt.Columns.Add(columnName);

      do
      {
        while (source.Read())
        {
          var itemArray = new object[dt.Columns.Count];
          for (var index = dt.Columns.Count - 1; index >= 0; index--)
            itemArray[index] = source[dt.Columns[index].ColumnName];

          var dr = dt.NewRow();
          dr.ItemArray = itemArray;
          dt.Rows.Add(dr);
        }
      }
      while (source.NextResult());

      return dt;
    }
    /// <summary>Creates a new data table from the IDataReader using the specified column indices.</summary>
    public static System.Data.DataTable ToDataTable(this System.Data.IDataReader source, string tableName, int[] columnIndices)
      => ToDataTable(source, tableName, System.Linq.Enumerable.Range(0, columnIndices.Length).Select(i => source.GetNameEx(columnIndices[i])));
  }
}
