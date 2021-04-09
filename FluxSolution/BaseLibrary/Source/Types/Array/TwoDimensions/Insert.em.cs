namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Create a new array from the existing array, copy all elements and insert the specified items at the specified dimension and index.</summary>
    /// <param name="source">The source array from where the new array as is based..</param>
    /// <param name="dimension">The dimension to resize.</param>
    /// <param name="index">The index in the dimension where the items should be added, e.g. which row or column to fill. If -1 then add at the end of the dimension.</param>
    /// <param name="items">The items to fill at index. If less or more than the number of slots in the array, as many as can be copied will be.</param>
    /// <returns></returns>
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static T[,] Insert<T>(this T[,] source, int dimension, int index, params T[] items)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      //if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));
      if (dimension < 0 || dimension > 1) throw new System.ArgumentOutOfRangeException(nameof(dimension));

      if (index < 0)
        index = source.GetLength(dimension);

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[sourceLength0 + (dimension == 0 ? 1 : 0), sourceLength1 + (dimension == 1 ? 1 : 0)];

      for (var s0 = 0; s0 < sourceLength0; s0++)
      {
        var t0 = s0 + (dimension == 0 && s0 >= index ? 1 : 0);

        for (var s1 = 0; s1 < sourceLength1; s1++)
        {
          var t1 = s1 + (dimension == 1 && s1 >= index ? 1 : 0);

          target[t0, t1] = source[s0, s1];
        }
      }

      if (items is null || items.Length == 0)
        items = new T[target.GetLength(dimension)];

      var minLength = System.Math.Min(source.GetLength(dimension), items.Length);

      switch (dimension)
      {
        case 0:
          for (var index0 = 0; index0 < minLength; index0++)
            target[index, index0] = items[index0];
          break;
        case 1:
          for (int index1 = 0; index1 < minLength; index1++)
            target[index1, index] = items[index1];
          break;
      }

      return target;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
  }
}
