namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Data.DataTable"/> containing the <paramref name="source"/> data rows reversed (mirrored).</para>
    /// </summary>
    public static System.Data.DataTable FlipRowsToCopy(this System.Data.DataTable source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var target = new System.Data.DataTable(source.TableName);

      for (var index = 0; index < source.Columns.Count; index++)
        target.Columns.Add(source.Columns[index].ColumnName, source.Columns[index].DataType, source.Columns[index].Expression);

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
