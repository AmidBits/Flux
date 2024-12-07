namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Results in a string array of all column values in the current row.</summary>
    public static string GetString(this System.Data.IDataRecord source, int index, string nullString)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.IsDBNull(index) ? nullString : source.GetValue(index).ToString() ?? nullString;
    }

    /// <summary>Results in a sequence of strings of all column values in the current row.</summary>
    public static string[] GetStrings(this System.Data.IDataRecord source, string nullString)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var strings = new string[source.FieldCount];

      for (var index = source.FieldCount - 1; index >= 0; index--)
        strings[index] = source.GetString(index, nullString);

      return strings;
    }
  }
}
