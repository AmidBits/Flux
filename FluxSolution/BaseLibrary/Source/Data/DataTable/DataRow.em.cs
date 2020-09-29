namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns an array (copied) of the data from the specified columns.</summary>
    public static object[] GetValues(this System.Data.DataRow source, params string[] columnNames)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var array = new object[columnNames.Length];
      for (var index = array.Length - 1; index >= 0; index--)
        array[index] = source[columnNames[index]];
      return array;
    }
  }
}
