namespace Flux
{
  public static partial class Fx
  {
    ///// <summary>Create a new data table from the specified sequence.</summary>
    ///// <param name="source">The source sequence.</param>
    ///// <param name="tableName">The name of the data table.</param>
    ///// <param name="namesSelector">The column names selector to use.</param>
    ///// <param name="typesSelector">The column types selector to use.</param>
    ///// <param name="valuesSelector">A array selector used to extract the data for each row in the data table.</param>
    ///// <exception cref="System.ArgumentNullException"/>
    //public static System.Data.DataTable ToDataTable<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, string tableName, System.Func<TSource, object[], string[]> namesSelector, System.Func<TSource, object[], System.Type[]> typesSelector, System.Func<TSource, int, object[]> valuesSelector)
    //{
    //  System.ArgumentNullException.ThrowIfNull(valuesSelector);

    //  var dt = new System.Data.DataTable(tableName);

    //  using var e = source.ThrowOnNull().GetEnumerator();

    //  if (e.MoveNext())
    //  {
    //    var rowIndex = 0;

    //    var names = namesSelector(e.Current, valuesSelector(e.Current, rowIndex));
    //    var types = typesSelector(e.Current, valuesSelector(e.Current, rowIndex));

    //    for (var columnIndex = 0; columnIndex < names.Length; columnIndex++)
    //      dt.Columns.Add(names[columnIndex], types[columnIndex]);

    //    do
    //    {
    //      var values = valuesSelector(e.Current, rowIndex++);

    //      if (values is not null)
    //        dt.Rows.Add(values);
    //    }
    //    while (e.MoveNext());
    //  }

    //  return dt;
    //}

    //public static System.Data.DataTable ToDataTable<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, string tableName, int columnCount, System.Func<TSource, int, object[]> valuesSelector)
    //  => ToDataTable(source, tableName, (e, ia) => System.Linq.Enumerable.Range(0, columnCount).Select(i => i.ToSingleOrdinalColumnName()).ToArray(), (e, ia) => System.Linq.Enumerable.Range(1, columnCount).Select(i => typeof(object)).ToArray(), valuesSelector);

    //public static System.Data.DataTable ToDataTable(this System.Collections.Generic.IEnumerable<string[]> source, bool hasColumnNames, string? tableName = null)
    //{
    //  var dt = new System.Data.DataTable(tableName);

    //  using var e = source.ThrowOnNull().GetEnumerator();

    //  if (e.MoveNext())
    //  {
    //    var columnNames = hasColumnNames ? e.Current : e.Current.Length.ToNumberOfOrdinalColumnNames();

    //    for (var columnIndex = 0; columnIndex < columnNames.Length; columnIndex++)
    //      dt.Columns.Add(columnNames[columnIndex], typeof(string));

    //    do
    //    {
    //      dt.Rows.Add(e.Current);
    //    }
    //    while (e.MoveNext());
    //  }

    //  return dt;
    //}

    public static System.Data.DataTable ToDataTable(this System.Collections.Generic.IEnumerable<object[]> source, bool hasColumnNames, string? tableName = null)
    {
      var dt = new System.Data.DataTable(tableName);

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext() is var movedNext && movedNext)
      {
        var columnNames = hasColumnNames ? e.Current : e.Current.Length.ToNumberOfOrdinalColumnNames();
        var columnTypes = columnNames.Select(cn => typeof(object)).ToArray();

        if (hasColumnNames)
        {
          movedNext = e.MoveNext();

          if (movedNext)
            columnTypes = columnNames.Select((cn, i) => e.Current[i].GetType()).ToArray();
        }

        for (var columnIndex = 0; columnIndex < columnNames.Length; columnIndex++)
          dt.Columns.Add(columnNames[columnIndex].ToString(), columnTypes[columnIndex]);

        if (movedNext)
          do
          {
            dt.Rows.Add(e.Current);
          }
          while (e.MoveNext());
      }

      return dt;
    }
  }
}
