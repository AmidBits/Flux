namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Create a new array with the elements from <paramref name="source"/> and by inserting <paramref name="count"/> new contiguous strands (of rows or colums) in the specified <paramref name="dimension"/> at the <paramref name="index"/>. All values from the <paramref name="source"/> are copied.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static T[,] InsertToCopy<T>(this T[,] source, int dimension, int index, int count)
    {
      source.AssertRank(2);

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

    /// <summary>
    /// <para>Create a new array with the elements from <paramref name="source"/> and by inserting <paramref name="count"/> new contiguous strands (of rows or colums) in the specified <paramref name="dimension"/> at the <paramref name="index"/>. All values from the <paramref name="source"/> are copied.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static T[,] InsertToCopy<T>(this T[,] source, ArrayDimensionLabel dimension, int index, int count) => source.InsertToCopy((int)dimension, index, count);

    /// <summary>Create a new array from the existing array, copy all elements and insert the specified items at the specified dimension and index.</summary>
    /// <param name="source">The source array from where the new array as is based.</param>
    /// <param name="dimension">The dimension to resize.</param>
    /// <param name="index">The index in the dimension where the strands should be inserted, e.g. which row or column to fill.</param>
    /// <param name="count">The number of strands to add for the dimension.</param>
    /// <param name="pattern">The items to fill at index. Using a sort of continuous flow fill.</param>
    public static T[,] InsertToCopy<T>(this T[,] source, int dimension, int index, int count, params T[] pattern)
    {
      var target = InsertToCopy(source, dimension, index, count);

      switch (dimension)
      {
        case 0:
          if (pattern.Length > 0)
            Fill(target, index, 0, count, target.GetLength(1), pattern);
          break;
        case 1:
          if (pattern.Length > 0)
            Fill(target, 0, index, target.GetLength(0), count, pattern);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }

      return target;
    }

    /// <summary>Create a new array from the existing array, copy all elements and insert the specified items at the specified dimension and index.</summary>
    /// <param name="source">The source array from where the new array as is based.</param>
    /// <param name="dimension">The dimension to resize.</param>
    /// <param name="index">The index in the dimension where the strands should be inserted, e.g. which row or column to fill.</param>
    /// <param name="count">The number of strands to add for the dimension.</param>
    /// <param name="pattern">The items to fill at index. Using a sort of continuous flow fill.</param>
    public static T[,] InsertToCopy<T>(this T[,] source, ArrayDimensionLabel dimension, int index, int count, params T[] pattern) => source.InsertToCopy((int)dimension, index, count, pattern);
  }
}
