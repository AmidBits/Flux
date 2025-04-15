namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>The longest alternating subsequence problem, one wants to find a subsequence of a given <paramref name="source"/> in which the elements are in alternating order, and in which the sequence is as long as possible. Uses the specified <paramref name="comparer"/>, default if null.</para>
    /// <see href="https://en.wikipedia.org/wiki/Longest_alternating_subsequence"/>
    /// </summary>
    /// <param name="source">Source sequence in which to find the longest alternating subsequence.</param>
    /// <param name="length">The length of the longest alternating subsequence that was found.</param>
    /// <param name="comparer">Uses the specified comparer, default if null.</param>
    /// <returns>The matrix of the longest alternating subsequence that was found, using dynamic programming.</returns>
    /// <remarks>
    /// <para>Implemented based on the Wiki article.</para>
    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
    /// </remarks>
    public static int LongestAlternatingSubsequenceLength<T>(this System.ReadOnlySpan<T> source, out int[,] matrix, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      matrix = LongestAlternatingSubsequenceMatrix(source, out var length, comparer);

      return length;
    }

    /// <summary>
    /// <para>The longest alternating subsequence problem, one wants to find a subsequence of a given <paramref name="source"/> in which the elements are in alternating order, and in which the sequence is as long as possible. Uses the specified <paramref name="comparer"/>, default if null.</para>
    /// <see href="https://en.wikipedia.org/wiki/Longest_alternating_subsequence"/>
    /// </summary>
    /// <param name="source">Source sequence in which to find the longest alternating subsequence.</param>
    /// <param name="length">The length of the longest alternating subsequence that was found.</param>
    /// <param name="comparer">Uses the specified comparer, default if null.</param>
    /// <returns>The matrix of the longest alternating subsequence that was found, using dynamic programming.</returns>
    /// <remarks>
    /// <para>Implemented based on the Wiki article.</para>
    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
    /// </remarks>
    public static int[,] LongestAlternatingSubsequenceMatrix<T>(this System.ReadOnlySpan<T> source, out int length, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var matrix = new int[source.Length, 2];

      for (var i = 0; i < source.Length; i++) // Initialize BOTH dimensions to 1.
      {
        matrix[i, 0] = 1; // Length of the longest alternating subsequence ending at index i and last element is greater than its previous element.
        matrix[i, 1] = 1; // Length of the longest alternating subsequence ending at index i and last element is smaller than its previous element.
      }

      length = 0;

      for (var i = 1; i < source.Length; i++)
      {
        for (var j = 0; j < i; j++)
        {
          var cmp = comparer.Compare(source[j], source[i]);

          if (cmp < 0 && matrix[j, 1] + 1 is var mj1p1 && matrix[i, 0] < mj1p1)
            matrix[i, 0] = mj1p1;

          if (cmp > 0 && matrix[j, 0] + 1 is var mj0p1 && matrix[i, 1] < mj0p1)
            matrix[i, 1] = mj0p1;
        }

        length = int.Max(length, int.Max(matrix[i, 0], matrix[i, 1]));
      }

      return matrix;
    }

    /// <summary>
    /// <para>The longest alternating subsequence problem, one wants to find a subsequence of a given <paramref name="source"/> in which the elements are in alternating order, and in which the sequence is as long as possible. Uses the specified <paramref name="comparer"/>, default if null.</para>
    /// <see href="https://en.wikipedia.org/wiki/Longest_alternating_subsequence"/>
    /// </summary>
    /// <param name="source">Source sequence in which to find the longest alternating subsequence.</param>
    /// <param name="matrix">The matrix of the longest alternating subsequence that was found, using dynamic programming.</param>
    /// <param name="comparer">Uses the specified comparer, default if null.</param>
    /// <returns>The longest alternating subsequence that was found.</returns>
    /// <remarks>
    /// <para>Implemented based on the Wiki article.</para>
    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
    /// </remarks>
    public static T[] LongestAlternatingSubsequenceValues<T>(this System.ReadOnlySpan<T> source, out int[,] matrix, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      matrix = LongestAlternatingSubsequenceMatrix(source, out var length, comparer);

      if (length > 0)
      {
        var lasv = new T[length];

        for (int i = 0, mark = 0; i < source.Length; i++)
        {
          var max = int.Max(matrix[i, 0], matrix[i, 1]);

          if (max > mark)
          {
            lasv[mark] = source[i];

            mark++;
          }
        }

        return lasv;
      }

      return System.Array.Empty<T>();
    }
  }
}
