namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Rotate a two-dimensional array in-place in a counter-clock-wise or clock-wise direction.</para>
    /// <para>Forming Cycles: O(n^2) Time and O(1) Space. Without any extra space, rotate the array in form of cycles. For example, a 4 X 4 matrix will have 2 cycles. The first cycle is formed by its 1st row, last column, last row, and 1st column. The second cycle is formed by the 2nd row, second-last column, second-last row, and 2nd column. The idea is for each square cycle, to swap the elements involved with the corresponding cell in the matrix in an anticlockwise direction i.e. from top to left, left to bottom, bottom to right, and from right to top one at a time using nothing but a temporary variable to achieve this.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="direction"></param>
    public static void RotateInPlace<T>(this T[,] source, RotationalDirection direction)
    {
      source.AssertRank(2);
      source.AssertDimensionallySymmetrical(out var length);

      var lm1 = length - 1;

      for (var i = 0; i < length / 2; i++) // Consider all cycles one by one.
      {
        var lm1mi = lm1 - i;

        for (var j = i; j < lm1mi; j++) // Consider elements in group of 4 as P1, P2, P3 & P4 in current square.
        {
          var lm1mj = lm1 - j;

          var pt = source[i, j];                        // Move P1 to pt

          switch (direction)
          {
            case RotationalDirection.ClockWise: // Swap elements in clockwise order. The elements in the group are:
              // P1 = [i, j]
              // P2 = [n-1-j, i]
              // P3 = [n-1-i, n-1-j]
              // P4 = [j, n-1-i]
              source[i, j] = source[lm1mj, i];          // Move P2 to P1
              source[lm1mj, i] = source[lm1mi, lm1mj];  // Move P3 to P4
              source[lm1mi, lm1mj] = source[j, lm1mi];  // Move P4 to P3
              source[j, lm1mi] = pt;                    // Move pt to P4
              break;
            case RotationalDirection.CounterClockWise:  // Swap elements in counter-clockwise order. The elements in this group are:
              // P1 = [i, j]
              // P2 = [j, n-1-i]
              // P3 = [n-1-i, n-1-j]
              // P4 = [n-1-j, i]
              source[i, j] = source[j, lm1mi];          // Move P2 to P1
              source[j, lm1mi] = source[lm1mi, lm1mj];  // Move P3 to P4
              source[lm1mi, lm1mj] = source[lm1mj, i];  // Move P4 to P3
              source[lm1mj, i] = pt;                    // Move pt to P4
              break;
          }
        }
      }
    }
  }
}
