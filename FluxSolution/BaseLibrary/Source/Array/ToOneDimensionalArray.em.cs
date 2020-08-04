using System.Linq;

namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class XtensionsArray
  {
    /// <summary>Returns the jagged array (i.e. an array of arrays) as a two-dimensional array.</summary>
    public static T[] ToOneDimensionalArray<T>(this T[,] source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var array = new T[source.Length];
      var index = 0;

      foreach (var item in source)
      {
        if (index >= array.Length) break;

        array[index++] = item;
      }

      return array;
    }
  }
}
