using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns an array of column names.</summary>
    public static string[] GetColumnNames(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var array = new string[source.Columns.Count];
      for (var index = array.Length - 1; index >= 0; index--)
        array[index] = source.Columns[index].ColumnName;
      return array;
    }
    /// <summary>Returns an array of column types.</summary>
    public static System.Type[] GetColumnTypes(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var array = new System.Type[source.Columns.Count];
      for (var index = array.Length - 1; index >= 0; index--)
        array[index] = source.Columns[index].DataType;
      return array;
    }

    /// <summary>Returns a sequence of DataColumn objects from the DataTable.</summary>
    public static System.Collections.Generic.IEnumerable<System.Data.DataColumn> GetDataColumns(this System.Data.DataTable source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Columns.Cast<System.Data.DataColumn>();
    /// <summary>Returns a sequence of DataRow objects from the DataTable.</summary>
    public static System.Collections.Generic.IEnumerable<System.Data.DataRow> GetDataRows(this System.Data.DataTable source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Rows.Cast<System.Data.DataRow>();

    /// <summary>Removes (as in deletes) all DataColumn objects matching the specified names.</summary>
    public static void RemoveAllColumnsEqualTo(this System.Data.DataTable source, params string[] columnNames)
      => GetColumnNames(source).Except(columnNames).ToList().ForEach(cn => source.Columns.Remove(cn));
    /// <summary>Removes (as in deletes) all DataColumn objects NOT matching the specified names.</summary>
    public static void RemoveAllColumnsNotEqualTo(this System.Data.DataTable source, params string[] columnNames)
      => GetColumnNames(source).Except(columnNames).ToList().ForEach(cn => source.Columns.Remove(cn));

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
