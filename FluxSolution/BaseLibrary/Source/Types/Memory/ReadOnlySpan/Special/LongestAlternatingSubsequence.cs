namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static int GetLongestAlternatingSubsequenceLength<T>(this System.ReadOnlySpan<T> source, out int[,] matrix, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      matrix = GetLongestAlternatingSubsequenceMatrix(source, out var length, comparer);

      return length;
    }

    /// <summary>
    /// <para>The longest alternating subsequence problem, one wants to find a subsequence of a given sequence in which the elements are in alternating order, and in which the sequence is as long as possible. Uses the specified comparer.</para>
    /// <see href="https://en.wikipedia.org/wiki/Longest_alternating_subsequence"/>
    /// </summary>
    public static int[,] GetLongestAlternatingSubsequenceMatrix<T>(this System.ReadOnlySpan<T> source, out int length, System.Collections.Generic.IComparer<T>? comparer = null)
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

        length = System.Math.Max(length, System.Math.Max(matrix[i, 0], matrix[i, 1]));
      }

      return matrix;
    }

    /// <summary>
    /// <para>The longest alternating subsequence problem, one wants to find a subsequence of a given sequence in which the elements are in alternating order, and in which the sequence is as long as possible. Uses the specified comparer.</para>
    /// <see href="https://en.wikipedia.org/wiki/Longest_alternating_subsequence"/>
    /// </summary>
    public static T[] GetLongestAlternatingSubsequenceValues<T>(this System.ReadOnlySpan<T> source, out int[,] matrix, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      matrix = GetLongestAlternatingSubsequenceMatrix(source, out var length, comparer);

      var subsequence = new T[length];

      for (int i = 0, mark = 0; i < source.Length; i++)
      {
        var max = System.Math.Max(matrix[i, 0], matrix[i, 1]);

        if (max > mark)
        {
          subsequence[mark] = source[i];

          mark++;
        }
      }

      return subsequence;
    }
  }
}
