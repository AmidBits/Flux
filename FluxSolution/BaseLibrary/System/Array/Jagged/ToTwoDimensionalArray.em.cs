namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>
    /// <para>Returns the jagged array (i.e. an array of arrays) as a two-dimensional array.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static T[,] ToTwoDimensionalArray<T>(this T[][] source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var target = new T[source.Length, source.Max(t => t.Length)];

      for (var i0 = source.Length - 1; i0 >= 0; i0--)
        for (var i1 = source[i0].Length - 1; i1 >= 0; i1--)
          target[i0, i1] = source[i0][i1];

      return target;
    }
  }
}
