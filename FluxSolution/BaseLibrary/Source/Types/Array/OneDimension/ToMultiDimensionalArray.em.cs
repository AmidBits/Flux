namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayJagged
  {
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static T[,] ToTwoDimensionalArray<T>(this T[] source, int length0, int length1)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var target = new T[length0, length1];

      for (var i0 = length0 - 1; i0 >= 0; i0--)
        for (var i1 = length1 - 1; i1 >= 0; i1--)
          target[i0, i1] = source[i0 * length0 + i1];

      return target;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
  }
}
