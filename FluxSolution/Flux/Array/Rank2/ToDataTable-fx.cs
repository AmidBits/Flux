namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Create a new <see cref="System.Data.DataTable"/> from <paramref name="source"/>.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static System.Data.DataTable ToDataTable<T>(this T[,] source, bool sourceHasColumnNames, params string[] customColumnNames)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);

      var dt = new System.Data.DataTable();

      for (var i1 = 0; i1 < sl1; i1++)
      {
        var columnName = default(string);

        if (sourceHasColumnNames) // First choice, if sourceHasColumnNames is true, use source value.
          columnName = source[0, i1]?.ToString();
        else if (i1 < customColumnNames.Length) // Second choice, if sourceColumnNames is false, use custom column name, if present.
          columnName = customColumnNames[i1];

        dt.Columns.Add(columnName ?? i1.ToSingleOrdinalColumnName()); // Third choice, if columnName is still null (string default), use ToSingleOrdinalColumnName(), e.g. "Column1".
      }

      for (var i0 = sourceHasColumnNames ? 1 : 0; i0 < sl0; i0++)
      {
        var array = new object[sl1];

        for (var i1 = 0; i1 < sl1; i1++)
          array[i1] = source[i0, i1]!;

        dt.Rows.Add(array);
      }

      return dt;
    }
  }
}
