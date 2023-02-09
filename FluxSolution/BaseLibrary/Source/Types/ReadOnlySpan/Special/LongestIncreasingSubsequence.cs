namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static int GetLongestIncreasingSubsequenceLength<T>(this System.ReadOnlySpan<T> source, out int[,] matrix, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      matrix = GetLongestAlternatingSubsequenceMatrix(source, out var length, comparer);

      return length;
    }

    /// <summary>
    /// <para>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</para>
    /// <see href="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    /// </summary>
    public static int[,] GetLongestIncreasingSubsequenceMatrix<T>(this System.ReadOnlySpan<T> source, out int length, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      length = 0; // Length is returned in the matrix[0, 0].

      var matrix = new int[2, source.Length + 1]; // [0,...] = smallest value indices (M), [1,...] = predecessor indices (P)

      for (var i = 0; i < source.Length; i++)
      {
        // Binary search for the largest positive j ≤ L such that X[M[j]] < X[i]

        var lo = 1;
        var hi = length + 1;

        while (lo < hi)
        {
          var mid = lo + ((hi - lo) / 2);

          if (comparer.Compare(source[matrix[0, mid]], source[i]) < 0)
            lo = mid + 1;
          else
            hi = mid;
        }

        var newL = lo; // After searching, lo is 1 greater than the length of the longest prefix of X[i].

        matrix[1, i] = matrix[0, newL - 1]; // The predecessor of X[i] is the last index of the subsequence of length newL-1.
        matrix[0, newL] = i;

        if (newL > length)
          length = newL; // If we found a subsequence longer than any we've found yet, update length.
      }

      return matrix;
    }

    /// <summary>
    /// <para>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</para>
    /// <see href="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    /// </summary>
    public static T[] GetLongestIncreasingSubsequenceValues<T>(this System.ReadOnlySpan<T> source, out int[,] matrix, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      matrix = GetLongestIncreasingSubsequenceMatrix(source, out var length, comparer);

      var result = new T[length];
      for (int i = length - 1, k = matrix[0, length]; i >= 0; i--, k = matrix[1, k])
        result[i] = source[k];
      return result;
    }
  }
}
