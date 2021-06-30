namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns a new data table with values from the IDataReader. The specified table name is used in constructing the data table.</summary>
    public static System.Data.DataTable ToDataTable(this System.Data.IDataReader source, string tableName)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var dt = new System.Data.DataTable(tableName);
      dt.Load(source);
      return dt;
    }

    /// <summary>Creates a new data table from the IDataReader using the specified column names.</summary>
    public static System.Data.DataTable ToDataTable(this System.Data.IDataReader source, params string[] columnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var dt = new System.Data.DataTable();

      foreach (var columnName in columnNames)
        dt.Columns.Add(columnName);

      do
      {
        while (source.Read())
        {
          var itemArray = new object[columnNames.Length];
          for (var index = columnNames.Length - 1; index >= 0; index--)
            itemArray[index] = source[columnNames[index]];

          var dr = dt.NewRow();
          dr.ItemArray = itemArray;
          dt.Rows.Add(dr);
        }
      }
      while (source.NextResult());

      return dt;
    }
    /// <summary>Creates a new data table from the IDataReader using the specified column indices.</summary>
    public static System.Data.DataTable ToDataTable(this System.Data.IDataReader source, params int[] columnIndices)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var dt = new System.Data.DataTable();

      foreach (var columnIndex in columnIndices)
        dt.Columns.Add(source.GetNameEx(columnIndex));

      do
      {
        while (source.Read())
        {
          var itemArray = new object[columnIndices.Length];
          for (var index = columnIndices.Length - 1; index >= 0; index--)
            itemArray[index] = source.GetValue(columnIndices[index]);

          var dr = dt.NewRow();
          dr.ItemArray = itemArray;
          dt.Rows.Add(dr);
        }
      }
      while (source.NextResult());

      return dt;
    }
  }
}
