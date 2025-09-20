namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Rotate a two-dimensional array in-place in a clock-wise direction.</para>
    /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
    /// <para>Forming Cycles: O(n^2) Time and O(1) Space. Without any extra space, rotate the array in form of cycles. For example, a 4 X 4 matrix will have 2 cycles. The first cycle is formed by its 1st row, last column, last row, and 1st column. The second cycle is formed by the 2nd row, second-last column, second-last row, and 2nd column. The idea is for each square cycle, to swap the elements involved with the corresponding cell in the matrix in an anticlockwise direction i.e. from top to left, left to bottom, bottom to right, and from right to top one at a time using nothing but a temporary variable to achieve this.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static void RotateCcw<T>(this T[,] source, T[,]? target = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      target ??= source;

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);

      if (target.GetLength(0) < sl1 || target.GetLength(1) < sl0)
        throw new System.ArgumentException($"Array rotation counter-clockwise requires target minimum dimensional symmetry.");

      var sl0m1 = sl0 - 1;

      for (var i = 0; i < sl0 / 2; i++) // Consider all cycles one by one.
      {
        var sl0m1mi = sl0m1 - i;

        for (var j = i; j < sl0m1mi; j++) // Consider elements in group of 4 as P1, P2, P3 & P4 in current square.
        {
          var sl0m1mj = sl0m1 - j;

          // P1 = [i, j]
          // P2 = [j, n-1-i]
          // P3 = [n-1-i, n-1-j]
          // P4 = [n-1-j, i]

          var pt = source[i, j];                          // Move P1 to pt

          target[i, j] = source[j, sl0m1mi];              // Move P2 to P1
          target[j, sl0m1mi] = source[sl0m1mi, sl0m1mj];  // Move P3 to P4
          target[sl0m1mi, sl0m1mj] = source[sl0m1mj, i];  // Move P4 to P3
          target[sl0m1mj, i] = pt;                        // Move pt to P4
        }
      }
    }

    /// <summary>
    /// <para>Rotate a two-dimensional array in-place in a counter-clock-wise direction.</para>
    /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
    /// <para>Forming Cycles: O(n^2) Time and O(1) Space. Without any extra space, rotate the array in form of cycles. For example, a 4 X 4 matrix will have 2 cycles. The first cycle is formed by its 1st row, last column, last row, and 1st column. The second cycle is formed by the 2nd row, second-last column, second-last row, and 2nd column. The idea is for each square cycle, to swap the elements involved with the corresponding cell in the matrix in an anticlockwise direction i.e. from top to left, left to bottom, bottom to right, and from right to top one at a time using nothing but a temporary variable to achieve this.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static void RotateCw<T>(this T[,] source, T[,]? target = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      target ??= source;

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);

      if (target.GetLength(0) < sl1 || target.GetLength(1) < sl0)
        throw new System.ArgumentException($"Array rotation clockwise requires target minimum dimensional symmetry.");

      var sl0m1 = sl0 - 1;

      for (var i = 0; i < sl0 / 2; i++) // Consider all cycles one by one.
      {
        var sl0m1mi = sl0m1 - i;

        for (var j = i; j < sl0m1mi; j++) // Consider elements in group of 4 as P1, P2, P3 & P4 in current square.
        {
          var sl0m1mj = sl0m1 - j;

          // P1 = [i, j]
          // P2 = [n-1-j, i]
          // P3 = [n-1-i, n-1-j]
          // P4 = [j, n-1-i]

          var pt = source[i, j];                          // Move P1 to pt

          target[i, j] = source[sl0m1mj, i];              // Move P2 to P1
          target[sl0m1mj, i] = source[sl0m1mi, sl0m1mj];  // Move P3 to P4
          target[sl0m1mi, sl0m1mj] = source[j, sl0m1mi];  // Move P4 to P3
          target[j, sl0m1mi] = pt;                        // Move pt to P4
        }
      }
    }
  }
}
