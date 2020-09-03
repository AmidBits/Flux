namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Creates a new sequence with every n-th element from the sequence.</summary>
    public static System.Data.DataTable ToDataTable<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, object[]> valuesSelector, params string[] columnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (valuesSelector is null) throw new System.ArgumentNullException(nameof(valuesSelector));

      var dt = new System.Data.DataTable();

      foreach (var columnName in columnNames)
        dt.Columns.Add(columnName);

      var index = 0;

      foreach (var element in source)
      {
        var dr = dt.NewRow();
        dr.ItemArray = valuesSelector(element, index++);
        dt.Rows.Add(dr);
      }

      return dt;
    }
  }
}
