namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Data.DataTable"/> containing the <paramref name="source"/> columns reversed (mirrored).</para>
    /// </summary>
    public static System.Data.DataTable FlipColumnsToCopy(this System.Data.DataTable source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var target = new System.Data.DataTable(source.TableName);

      for (var index = source.Columns.Count - 1; index >= 0; index--)
        target.Columns.Add(source.Columns[index].ColumnName, source.Columns[index].DataType, source.Columns[index].Expression);

      for (var rowIndex = 0; rowIndex < source.Rows.Count; rowIndex++)
      {
        var values = new object[source.Columns.Count];
        System.Array.Copy(source.Rows[rowIndex].ItemArray, values, values.Length);
        System.Array.Reverse(values);
        target.Rows.Add(values);
      }

      return target;
    }
  }
}
