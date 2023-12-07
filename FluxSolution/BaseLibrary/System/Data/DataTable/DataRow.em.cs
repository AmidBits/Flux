namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a new array with the data from the specified column names.</summary>
    public static object[] GetValues(this System.Data.DataRow source, params string[] columnNames)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var array = new object[columnNames.Length];
      for (var index = array.Length - 1; index >= 0; index--)
        array[index] = source[columnNames[index]];
      return array;
    }

    /// <summary>Returns a new array with the data from the specified column indices.</summary>
    public static object[] GetValues(this System.Data.DataRow source, params int[] columnIndices)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var array = new object[columnIndices.Length];
      for (var index = array.Length - 1; index >= 0; index--)
        array[index] = source[columnIndices[index]];
      return array;
    }
  }
}
