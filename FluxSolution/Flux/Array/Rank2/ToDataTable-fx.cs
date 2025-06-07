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
      source.AssertRank(2);

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var dt = new System.Data.DataTable();

      for (var i1 = 0; i1 < sourceLength1; i1++)
      {
        var columnName = default(string);

        if (sourceHasColumnNames) // First choice, if the parameter 'sourceHasColumnNames' is true, use source value.
          columnName = source[0, i1]?.ToString();

        if (i1 < customColumnNames.Length) // Second choice (and possibility of overriding source-column-name), use custom column name.
          columnName = customColumnNames[i1];

        dt.Columns.Add(columnName ?? i1.ToSingleOrdinalColumnName());
      }

      for (var i0 = sourceHasColumnNames ? 1 : 0; i0 < sourceLength0; i0++)
      {
        var array = new object[sourceLength1];
        for (var i1 = 0; i1 < sourceLength1; i1++)
          array[i1] = source[i0, i1]!;
        dt.Rows.Add(array);
      }

      return dt;
    }
  }
}
