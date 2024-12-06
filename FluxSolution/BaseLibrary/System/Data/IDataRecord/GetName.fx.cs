namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a string with the column name (header) for the specified index in the current row. This version will replace null or blank names with "Column_N" where N is the column index + 1.</para>
    /// </summary>
    public static string GetNameEx(this System.Data.IDataRecord source, int index)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.GetName(index) is var name && string.IsNullOrWhiteSpace(name) ? index.ToOrdinalName() : name;
    }

    /// <summary>
    /// <para>Results in a string array of all column names.</para>
    /// </summary>
    public static string[] GetNames(this System.Data.IDataRecord source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var names = new string[source.FieldCount];

      for (var index = 0; index < source.FieldCount; index++)
        names[index] = source.GetNameEx(index);

      return names;
    }
  }
}
