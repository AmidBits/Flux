using System.Linq;

namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayJagged
  {
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    /// <summary>Returns the jagged array (i.e. an array of arrays) as a two-dimensional array.</summary>
    public static T[,] ToTwoDimensionalArray<T>(this T[][] source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var length0 = source.Length;
      var maxLength1 = source.Max(t => t.Length);

      var array = new T[length0, maxLength1];

      for (var i0 = 0; i0 < length0; i0++)
      {
        var length1 = source[i0].Length;

        for (var i1 = 0; i1 < length1; i1++)
          array[i0, i1] = source[i0][i1];
      }

      return array;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
  }
}
