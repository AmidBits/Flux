namespace Flux
{
  public static partial class IEnumerables
  {
    ///// <summary>Create a new data table from the specified sequence.</summary>
    ///// <param name="source">The source sequence.</param>
    ///// <param name="tableName">The name of the data table.</param>
    ///// <param name="namesSelector">The column names selector to use.</param>
    ///// <param name="typesSelector">The column types selector to use.</param>
    ///// <param name="valuesSelector">A array selector used to extract the data for each row in the data table.</param>
    ///// <exception cref="System.ArgumentNullException"/>
    public static System.Data.DataTable ToDataTable(this System.Collections.Generic.IEnumerable<object[]> source, bool hasFieldNames = false, bool adoptFieldTypes = false, string? tableName = null)
    {
      var dt = new System.Data.DataTable(tableName);

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext() is var movedNext && movedNext)
      {
        var columnNames = e.Current.Length.ToMultipleOrdinalColumnNames();

        if (hasFieldNames) // If has-field-names let's use those for columnNames.
          columnNames = e.Current.Select((e, i) => e?.ToString() ?? columnNames[i]).ToArray();

        var columnTypes = columnNames.Select(cn => typeof(object)).ToArray(); // Default to System.Object for columnTypes.

        if (hasFieldNames) // The first row has field-names..
        {
          movedNext = e.MoveNext(); // ..which may be of other types than the data, so let's move to the data.

          if (movedNext && adoptFieldTypes)
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
