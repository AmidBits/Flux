using System.Linq;

namespace Flux
{
  public static partial class XtensionsData
  {
    public static System.Collections.Generic.IEnumerable<object> GetColumn(this System.Data.DataTable source, int columnIndex)
      => source.GetDataColumn(columnIndex).Prepend(source.Columns[columnIndex].ColumnName);
    public static System.Collections.Generic.IEnumerable<object[]> GetColumns(this System.Data.DataTable source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Columns.Cast<System.Data.DataColumn>().Select(dc => source.GetColumn(dc.Ordinal).ToArray());

    public static System.Collections.Generic.IEnumerable<string> GetColumnNames(this System.Data.DataTable source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName);

    public static System.Collections.Generic.IEnumerable<object> GetDataColumn(this System.Data.DataTable source, int columnIndex)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Rows.Cast<System.Data.DataRow>().Select(dr => dr[columnIndex]);
    public static System.Collections.Generic.IEnumerable<object[]> GetDataColumns(this System.Data.DataTable source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Columns.Cast<System.Data.DataColumn>().Select(dc => source.GetDataColumn(dc.Ordinal).ToArray());

    public static System.Collections.Generic.IEnumerable<object> GetDataRow(this System.Data.DataTable source, int index)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Rows[index].ItemArray.Select(o => o);
    public static System.Collections.Generic.IEnumerable<object[]> GetDataRows(this System.Data.DataTable source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Rows.Cast<System.Data.DataRow>().Select(dr => dr.ItemArray.ToArray());

    public static System.Collections.Generic.IEnumerable<object[]> GetRows(this System.Data.DataTable source)
      => source.GetDataRows().Prepend(source.GetColumnNames().ToArray());
  }
}
