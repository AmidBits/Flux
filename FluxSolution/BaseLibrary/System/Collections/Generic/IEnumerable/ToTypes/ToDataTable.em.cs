namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    /// <summary>Create a new data table from the specified sequence.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="tableName">The name of the data table.</param>
    /// <param name="namesSelector">The column names selector to use.</param>
    /// <param name="typesSelector">The column types selector to use.</param>
    /// <param name="valuesSelector">A array selector used to extract the data for each row in the data table.</param>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Data.DataTable ToDataTable<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, string tableName, System.Func<TSource, object[], string[]> namesSelector, System.Func<TSource, object[], System.Type[]> typesSelector, System.Func<TSource, int, object[]> valuesSelector)
    {
      if (valuesSelector is null) throw new System.ArgumentNullException(nameof(valuesSelector));

      var dt = new System.Data.DataTable(tableName);

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext())
      {
        var rowIndex = 0;

        var names = namesSelector(e.Current, valuesSelector(e.Current, rowIndex));
        var types = typesSelector(e.Current, valuesSelector(e.Current, rowIndex));

        for (var columnIndex = 0; columnIndex < names.Length; columnIndex++)
          dt.Columns.Add(names[columnIndex], types[columnIndex]);

        do
        {
          var values = valuesSelector(e.Current, rowIndex++);

          if (values is not null)
            dt.Rows.Add(values);
        }
        while (e.MoveNext());
      }

      return dt;
    }
    public static System.Data.DataTable ToDataTable<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, string tableName, int columnCount, System.Func<TSource, int, object[]> valuesSelector)
      => ToDataTable(source, tableName, (e, ia) => System.Linq.Enumerable.Range(1, columnCount).Select(i => $"Column_{i}").ToArray(), (e, ia) => System.Linq.Enumerable.Range(1, columnCount).Select(i => typeof(object)).ToArray(), valuesSelector);

  }
}
