namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Creates a new two-dimensional array from the jagged array (i.e. an array of arrays). The outer array becomes dimension-0 (rows) and the inner arrays make up each dimension-1 (columns).</para>
    /// </summary>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static T[,] ToTwoDimensionalArray<T>(this T[][] source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var target = new T[source.Length, source.Max(t => t.Length)];

      for (var i = source.Length - 1; i >= 0; i--)
        for (var j = source[i].Length - 1; j >= 0; j--)
          target[i, j] = source[i][j];

      return target;
    }
  }
}
