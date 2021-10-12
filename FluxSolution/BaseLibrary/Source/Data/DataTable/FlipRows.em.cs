namespace Flux
{
  public static partial class ExtensionMethods
  {
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
  }
}
