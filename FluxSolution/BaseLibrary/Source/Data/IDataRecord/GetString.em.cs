namespace Flux
{
  public static partial class XtensionsData
  {
    /// <summary>Results in a string array of all column values in the current row.</summary>
    public static string GetString(this System.Data.IDataRecord source, int index, string nullString) => source.IsDBNull(index) ? nullString : source.GetValue(index).ToString() ?? nullString;

    /// <summary>Results in a string of a value in the current row. This version will also format some data types so that they can be appropriately reconstituted.</summary>
    public static string GetStringEx(this System.Data.IDataRecord source, int index, string nullString) => (source.GetValue(index)) switch
    {
      null => nullString,
      System.DBNull _ => nullString,
      System.String s => s,
      System.DateTime dt => dt.Millisecond >= 1000 ? dt.ToString(@"yyyy-MM-dd HH:mm:ss.fffffff") : dt.Millisecond >= 1 ? dt.ToString(@"yyyy-MM-dd HH:mm:ss.fff") : dt.ToString(@"yyyy-MM-dd HH:mm:ss"),
      System.Byte[] ba => System.Convert.ToBase64String(ba),
      System.Object o => o.ToString() ?? nullString
    };

    /// <summary>Results in a sequence of strings of all column values in the current row.</summary>
    public static System.Collections.Generic.IEnumerable<string> GetStrings(this System.Data.IDataRecord source, string nullString)
    {
      for (var index = 0; index < source.FieldCount; index++)
      {
        yield return source.GetString(index, nullString);
      }
    }
  }
}
