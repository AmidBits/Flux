namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Create a new array by using the valus from the existing array with contiguous strands (of rows or colums) in the specified <paramref name="dimension"/> at the <paramref name="index"/> and the number (<paramref name="count"/>) of strands to insert. All values from the <paramref name="source"/> are copied.</summary>
    public static T[,] Insert<T>(this T[,] source, int dimension, int index, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));
      if (dimension < 0 || dimension > 1) throw new System.ArgumentOutOfRangeException(nameof(dimension));
      if (index < 0 || index > source.GetLength(dimension)) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      T[,] target;

      switch (dimension)
      {
        case 0:
          target = new T[sourceLength0 + count, sourceLength1];
          for (var s0 = 0; s0 < sourceLength0; s0++)
            for (var s1 = 0; s1 < sourceLength1; s1++)
              target[s0 + (s0 >= index ? count : 0), s1] = source[s0, s1];
          break;
        case 1:
          target = new T[sourceLength0, sourceLength1 + count];
          for (var s0 = 0; s0 < sourceLength0; s0++)
            for (var s1 = 0; s1 < sourceLength1; s1++)
              target[s0, s1 + (s1 >= index ? count : 0)] = source[s0, s1];
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }

      return target;
    }

    /// <summary>Create a new array from the existing array, copy all elements and insert the specified items at the specified dimension and index.</summary>
    /// <param name="source">The source array from where the new array as is based..</param>
    /// <param name="dimension">The dimension to resize.</param>
    /// <param name="index">The index in the dimension where the strands should be inserted, e.g. which row or column to fill.</param>
    /// <param name="count">The number of strands to add for the dimension.</param>
    /// <param name="items">The items to fill at index. If less or more than the number of slots in the array, as many as can be copied will be.</param>
    public static T[,] Insert<T>(this T[,] source, int dimension, int index, int count, params T[] items)
    {
      var target = Insert(source, dimension, index, count);

      switch (dimension)
      {
        case 0:
          if (items.Length > 0)
            Fill(target, index, 0, count, target.GetLength(1), items);
          break;
        case 1:
          if (items.Length > 0)
            Fill(target, 0, index, target.GetLength(0), count, items);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }

      return target;
    }
  }
}
