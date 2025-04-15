namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Create a new two-dimensional array from <paramref name="source"/> with the strands of the specified <paramref name="dimension"/> (rows or columns) flipped.</para>
    /// </summary>
    /// <remarks>
    /// <para>An array is arbitrary in terms of e.g. rows and columns, so one can consider dimension 0 as the row dimension and dimension 1 as the column dimension, making it a row-major scenario.</para>
    /// <para>If data appears as [column, row] (so to speak), use <see cref="TransposeInPlace{T}(T[,])"/> or <see cref="TransposeToCopy{T}(T[,])"/> to make them [row, column].</para>
    /// </remarks>
    public static T[,] FlipToCopy<T>(this T[,] source, int dimension)
    {
      source.AssertRank(2);

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[sourceLength0, sourceLength1];

      switch (dimension)
      {
        case 0:
          var l0m1 = sourceLength0 - 1;
          var l0d2 = sourceLength0 / 2;
          for (var s1 = 0; s1 < sourceLength1; s1++)
            for (var s0 = 0; s0 <= l0d2; s0++)
            {
              target[s0, s1] = source[l0m1 - s0, s1];
              target[l0m1 - s0, s1] = source[s0, s1];
            }
          break;
        case 1:
          var l1m1 = sourceLength1 - 1;
          var l1d2 = sourceLength1 / 2;
          for (var s0 = 0; s0 < sourceLength0; s0++)
            for (var s1 = 0; s1 <= l1d2; s1++)
            {
              target[s0, s1] = source[s0, l1m1 - s1];
              target[s0, l1m1 - s1] = source[s0, s1];
            }
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }

      return target;
    }

    /// <summary>
    /// <para>Create a new two-dimensional array from <paramref name="source"/> with the strands of the specified <paramref name="dimension"/> (rows or columns) flipped.</para>
    /// </summary>
    /// <remarks>
    /// <para>An array is arbitrary in terms of e.g. rows and columns, so one can consider dimension 0 as the row dimension and dimension 1 as the column dimension, making it a row-major scenario.</para>
    /// <para>If data appears as [column, row] (so to speak), use <see cref="TransposeInPlace{T}(T[,])"/> or <see cref="TransposeToCopy{T}(T[,])"/> to make them [row, column].</para>
    /// </remarks>
    public static void FlipToCopy<T>(this T[,] source, ArrayDimensionLabel dimension) => source.FlipToCopy((int)dimension);
  }
}
