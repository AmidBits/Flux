namespace Flux
{
  public static partial class Streams
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Data.DataTable"/> from the content in the <paramref name="source"/> <see cref="System.IO.TextReader"/> using <paramref name="predicate"/> and <paramref name="resultSelector"/>. The <paramref name="tableName"/> (defaults to "DataTable") and <paramref name="columnNames"/> (defaults to "Column_n") are optional.</para>
    /// </summary>
    /// <param name="source">The <see cref="System.IO.TextReader"/>.</param>
    /// <param name="predicate">Determines whether a line from the reader will be used.</param>
    /// <param name="resultSelector">Converts a line into an array of strings.</param>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="columnNames">Zero or more column names. If there aren't enough column names, or none, column names in the format of "Column_n" will be added.</param>
    /// <returns></returns>
    public static System.Data.DataTable ToDataTable(this System.IO.TextReader source, System.Func<string, bool> predicate, System.Func<string, string[]> resultSelector, string tableName, params string[] columnNames)
    {
      var dataTable = new System.Data.DataTable(tableName ?? $"Table");

      if (columnNames is not null && columnNames.Length > 0)
        for (var i = 0; i < columnNames.Length; i++)
          dataTable.Columns.Add(columnNames[i]);

      foreach (var array in source.ReadLines(predicate, resultSelector))
      {
        if (dataTable.Columns.Count < array.Length)
          for (var i = dataTable.Columns.Count; i < array.Length; i++)
            dataTable.Columns.Add(i.ToSingleOrdinalColumnName());

        dataTable.Rows.Add(array);
      }

      return dataTable;
    }
  }
}
