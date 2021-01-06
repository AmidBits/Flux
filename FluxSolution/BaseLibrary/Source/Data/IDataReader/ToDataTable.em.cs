using System.Linq;

namespace Flux
{
  public static partial class SystemDataEm
  {
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
          var dr = dt.NewRow();
          dr.ItemArray = columnNames.Select(cn => source[cn]).ToArray();
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
          var dr = dt.NewRow();
          dr.ItemArray = columnIndices.Select(i => source.GetValue(i)).ToArray();
          dt.Rows.Add(dr);
        }
      }
      while (source.NextResult());

      return dt;
    }
  }
}
