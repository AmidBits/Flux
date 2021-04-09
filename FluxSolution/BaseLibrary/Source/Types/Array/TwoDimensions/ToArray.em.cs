namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Creates a new two-dimensional array with data from the source array. Use pre and post arguments to add surrounding space in the array.</summary>
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static T[,] ToArray<T>(this T[,] source, int sourceIndex0, int sourceIndex1, int count0, int count1, int preCount0, int preCount1, int postCount0, int postCount1)
    {
      var target = new T[preCount0 + count0 + postCount0, preCount1 + count1 + postCount1];

      CopyTo(source, target, sourceIndex0, sourceIndex1, preCount0, preCount1, count0, count1);

      return target;
    }
    /// <summary>Creates a new two-dimensional array with data from the source array.</summary>
    public static T[,] ToArray<T>(this T[,] source, int sourceIndex0, int sourceIndex1, int count0, int count1)
      => ToArray(source, sourceIndex0, sourceIndex1, count0, count1, 0, 0, 0, 0);

#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
  }
}
