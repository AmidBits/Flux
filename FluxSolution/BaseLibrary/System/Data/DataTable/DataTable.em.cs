namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a new array with all column names from the data table.</summary>
    public static string[] AllColumnNames(this System.Data.DataTable source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var array = new string[source.Columns.Count];
      for (var index = array.Length - 1; index >= 0; index--)
        array[index] = source.Columns[index].ColumnName;
      return array;
    }
    /// <summary>Returns a new array of all column types in the data table.</summary>
    public static System.Type[] AllColumnTypes(this System.Data.DataTable source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var array = new System.Type[source.Columns.Count];
      for (var index = array.Length - 1; index >= 0; index--)
        array[index] = source.Columns[index].DataType;
      return array;
    }

    /// <summary>Returns a new sequence with all values for that column.</summary>
    public static System.Collections.Generic.IEnumerable<object> GetValuesInColumn(this System.Data.DataTable source, int columnIndex, bool inReverseOrder = false)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (columnIndex < 0 || columnIndex >= source.Columns.Count) throw new System.ArgumentOutOfRangeException(nameof(source));

      if (inReverseOrder)
        for (var rowIndex = source.Rows.Count - 1; rowIndex >= 0; rowIndex--)
          yield return source.Rows[rowIndex][columnIndex];
      else
        for (var rowIndex = 0; rowIndex < source.Rows.Count; rowIndex++)
          yield return source.Rows[rowIndex][columnIndex];
    }

    /// <summary>Removes (as in deletes) all DataColumn objects matching the specified names.</summary>
    public static void RemoveAllColumnsEqualTo(this System.Data.DataTable source, params string[] columnNames)
      => AllColumnNames(source).Join(columnNames, s => s, cn => cn, (s, cn) => cn).ToList().ForEach(cn => source.Columns.Remove(cn));
    /// <summary>Removes (as in deletes) all DataColumn objects NOT matching the specified names.</summary>
    public static void RemoveAllColumnsNotEqualTo(this System.Data.DataTable source, params string[] columnNames)
      => AllColumnNames(source).Except(columnNames).ToList().ForEach(cn => source.Columns.Remove(cn));

    /// <summary>Removes (as in deletes) all DataRow objects matching the specified filter expression.</summary>
    /// <see cref="https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable.select?view=netcore-3.1#System_Data_DataTable_Select_System_String_"/>
    /// <seealso cref="https://www.csharp-examples.net/dataview-rowfilter/"/>
    public static void RemoveAllRowsMatching(this System.Data.DataTable source, string filterExpression)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Select(filterExpression).ToList().ForEach(dr => source.Rows.Remove(dr));
    /// <summary>Removes (as in deletes) all DataRow objects NOT matching the specified filter expression.</summary>
    /// <see cref="https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable.select?view=netcore-3.1#System_Data_DataTable_Select_System_String_"/>
    /// <seealso cref="https://www.csharp-examples.net/dataview-rowfilter/"/>
    public static void RemoveAllRowsNotMatching(this System.Data.DataTable source, string filterExpression)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Select($"NOT ({filterExpression})").ToList().ForEach(dr => source.Rows.Remove(dr));
  }
}
