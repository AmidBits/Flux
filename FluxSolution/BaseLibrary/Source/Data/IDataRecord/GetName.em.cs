namespace Flux
{
  public static partial class XtendData
  {
    /// <summary>Returns a string with the column name (header) for the specified index in the current row. This version will replace null or blank names with "ColumnN" where N is the column index + 1.</summary>
    public static string GetNameEx(this System.Data.IDataRecord source, int index)
      => source.GetName(index) is var name && string.IsNullOrWhiteSpace(name) ? $"Column{index + 1}" : name;

    /// <summary>Results in a string array of all column names.</summary>
    public static System.Collections.Generic.IEnumerable<string> GetNames(this System.Data.IDataRecord source)
//      => source.GetFields((idr, i) => idr.GetName(i));
    {
      for (var index = 0; index < source.FieldCount; index++)
      {
        yield return source.GetName(index);
      }
    }
    /// <summary>Results in a string array of all column names.</summary>
    public static System.Collections.Generic.IEnumerable<string> GetNamesEx(this System.Data.IDataRecord source)
    {
      for (var index = 0; index < source.FieldCount; index++)
      {
        yield return source.GetNameEx(index);
      }
    }
  }
}
