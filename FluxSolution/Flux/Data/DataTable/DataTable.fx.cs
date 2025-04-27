namespace Flux
{
  public static partial class Fx
  {
    public static string ToUrgfString(this System.Data.DataColumn source) => source.ColumnName;
    public static string ToUrgfString(this System.Data.DataColumnCollection source) => string.Join('\u001F', source.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName));
    public static string ToUrgfString(this System.Data.DataRow source) => string.Join('\u001F', source.ItemArray);
    public static string ToUrgfString(this System.Data.DataRowCollection source) => string.Join('\u001E', source.Cast<System.Data.DataRow>().Select(dr => dr.ToUrgfString()));
    public static string ToUrgfString(this System.Data.DataTable source) => source.Columns.ToUrgfString() + '\u001E' + source.Rows.ToUrgfString();
    public static string ToUrgfString(this System.Data.DataTableCollection source) => string.Join('\u001D', source.Cast<System.Data.DataTable>().Select(dt => dt.ToUrgfString()));

    public static int GetColumnOrdinalIndex(this System.Data.DataTable source, string columnName)
      => source.Columns[columnName]?.Ordinal ?? throw new System.ArgumentOutOfRangeException(nameof(columnName));

    //    /// <summary>Returns a new sequence with all values for that column.</summary>
    //    public static System.Collections.Generic.List<object> GetValuesInColumn(this System.Data.DataTable source, int columnIndex, bool reverseOrder = false)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);

    //      if (columnIndex < 0 || columnIndex >= source.Columns.Count) throw new System.ArgumentOutOfRangeException(nameof(source));

    //      var list = source.Rows.Cast<System.Data.DataRow>().Select(dr => dr[columnIndex]).ToList();

    //      if (reverseOrder)
    //        list.Reverse();

    //      return list;
    //    }

    //    /// <summary>Removes (as in deletes) all DataColumn objects matching the specified names.</summary>
    //    public static void RemoveAllColumnsIn(this System.Data.DataTable source, params string[] columnNames)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);

    //      source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).Join(columnNames, s => s, cn => cn, (s, cn) => cn).ToList().ForEach(cn => source.Columns.Remove(cn));
    //    }

    //    /// <summary>Removes (as in deletes) all DataColumn objects NOT matching the specified names.</summary>
    //    public static void RemoveAllColumnsExcept(this System.Data.DataTable source, params string[] columnNames)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);

    //      source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).Except(columnNames).ToList().ForEach(cn => source.Columns.Remove(cn));
    //    }

    //    /// <summary>Removes (as in deletes) all DataRow objects matching the specified filter expression.</summary>
    //    /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable.select?view=netcore-3.1#System_Data_DataTable_Select_System_String_"/>
    //    /// <seealso cref="https://www.csharp-examples.net/dataview-rowfilter/"/>
    //    public static void RemoveAllRowsMatching(this System.Data.DataTable source, string filterExpression)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);

    //      source.Select(filterExpression).ToList().ForEach(dr => source.Rows.Remove(dr));
    //    }

    //    /// <summary>Removes (as in deletes) all DataRow objects NOT matching the specified filter expression.</summary>
    //    /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable.select?view=netcore-3.1#System_Data_DataTable_Select_System_String_"/>
    //    /// <seealso cref="https://www.csharp-examples.net/dataview-rowfilter/"/>
    //    public static void RemoveAllRowsNotMatching(this System.Data.DataTable source, string filterExpression)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);

    //      source.Select($"NOT ({filterExpression})").ToList().ForEach(dr => source.Rows.Remove(dr));
    //    }
  }
}
