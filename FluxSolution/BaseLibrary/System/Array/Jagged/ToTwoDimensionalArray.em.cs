namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class Fx
  {
    /// <summary>Returns the jagged array (i.e. an array of arrays) as a two-dimensional array.</summary>
    public static T[,] ToTwoDimensionalArray<T>(this T[][] source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var target = new T[source.Length, System.Linq.Enumerable.Max(source, t => t.Length)];

      for (var i0 = source.Length - 1; i0 >= 0; i0--)
        for (var i1 = source[i0].Length - 1; i1 >= 0; i1--)
          target[i0, i1] = source[i0][i1];

      return target;
    }
  }
}
