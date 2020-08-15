namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class XtendArray
  {
    /// <summary>Create and return a new array with data from the current array.</summary>
    /// <param name="absoluteLength0">The length of dimension 0 in the new array.</param>
    /// <param name="absoluteLength1">The length of dimension 1 in the new array.</param>
    /// <param name="copyCount0">Copy this many elements from dimension 0, if possible.</param>
    /// <param name="copyCount1">Copy this many elements from dimension 1, if possible.</param>
    /// <param name="sourceIndex0">Copy data from dimension 0 starting at this index.</param>
    /// <param name="sourceIndex1">Copy data from dimension 1 starting at this index.</param>
    /// <param name="targetIndex0">Insert from this index on.</param>
    /// <param name="targetIndex1">Insert from this index on.</param>
    /// <returns></returns>
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional.
    public static T[,] Create<T>(this T[,] source, int absoluteLength0, int absoluteLength1, int copyCount0, int copyCount1, int sourceIndex0, int sourceIndex1, int targetIndex0, int targetIndex1)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (copyCount0 < 0) copyCount0 = source.GetLength(0);
      if (copyCount1 < 0) copyCount1 = source.GetLength(1);

      var target = new T[absoluteLength0, absoluteLength1];

      var maxCount0 = System.Math.Min(System.Math.Min(copyCount0, source.GetLength(0) - sourceIndex0), absoluteLength0 - targetIndex0);
      var maxCount1 = System.Math.Min(System.Math.Min(copyCount1, source.GetLength(1) - sourceIndex1), absoluteLength1 - targetIndex1);

      for (int i0 = 0; i0 < maxCount0; i0++)
      {
        for (int i1 = 0; i1 < maxCount1; i1++)
        {
          target[i0 + targetIndex0, i1 + targetIndex1] = source[i0 + sourceIndex0, i1 + sourceIndex1];
        }
      }

      return target;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional.
  }
}
