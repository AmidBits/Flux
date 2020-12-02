using System.Linq;

namespace Flux
{
  public static partial class DataTableEm
  {
    /// <summary>Returns a new data table with values from the seqeuence. The specified table name, column names and types are used in constructing the data table.</summary>
    public static System.Data.DataTable ToDataTable(this System.Collections.Generic.IEnumerable<object[]> source, string tableName, System.Collections.Generic.IList<string> columnNames, System.Collections.Generic.IList<System.Type> types)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (columnNames is null) throw new System.ArgumentNullException(nameof(columnNames));
      if (types is null) throw new System.ArgumentNullException(nameof(types));

      var dt = new System.Data.DataTable(tableName);
      for (var index = 0; index < columnNames.Count; index++)
        dt.Columns.Add(columnNames[index], types[index]);
      foreach (var values in source)
        dt.Rows.Add(values);
      return dt;
    }
    /// <summary>Returns a new data table with values from the seqeuence. The specified table name and column names are used in constructing the data table.</summary>
    public static System.Data.DataTable ToDataTable(this System.Collections.Generic.IEnumerable<string[]> source, string tableName, System.Collections.Generic.IList<string> columnNames)
      => ToDataTable(source, tableName, columnNames, System.Linq.Enumerable.Repeat(typeof(string), (columnNames ?? throw new System.ArgumentNullException(nameof(columnNames))).Count).ToList());

    /// <summary>Returns a new data table with values from the IDataReader. The specified table name is used in constructing the data table.</summary>
    public static System.Data.DataTable ToDataTable(this System.Data.IDataReader source, string tableName)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var dt = new System.Data.DataTable(tableName);
      dt.Load(source);
      return dt;
    }
  }
}
