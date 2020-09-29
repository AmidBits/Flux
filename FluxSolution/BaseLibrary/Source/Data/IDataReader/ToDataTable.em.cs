using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a new data table from the sequence using the value selector and the column names.</summary>
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
  }
}
