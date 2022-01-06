namespace Flux.Metrical
{
  /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longest_alternating_subsequence"/>
  public sealed class LongestAlternatingSubsequence
  {
    public static int[,] GetMatrix<T>(System.ReadOnlySpan<T> source, System.Collections.Generic.IComparer<T> comparer, out int length)
    {
      var sourceLength = source.Length;

      var matrix = new int[sourceLength, 2];

      for (var i = 0; i < sourceLength; i++) // Initialize BOTH dimensions to 1.
      {
        matrix[i, 0] = 1; // Length of the longest alternating subsequence ending at index i and last element is greater than its previous element.
        matrix[i, 1] = 1; // Length of the longest alternating subsequence ending at index i and last element is smaller than its previous element.
      }

      length = 0;

      for (var i = 1; i < sourceLength; i++)
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
    public static int[,] GetMatrix<T>(System.ReadOnlySpan<T> source, out int length)
      => GetMatrix(source, System.Collections.Generic.Comparer<T>.Default, out length);
    public static int[,] GetMatrix<T>(System.ReadOnlySpan<T> source, System.Collections.Generic.IComparer<T> comparer)
      => GetMatrix(source, comparer, out var _);
    public static int[,] GetMatrix<T>(System.ReadOnlySpan<T> source)
      => GetMatrix(source, out var _);

    public static T[] GetSubsequence<T>(System.ReadOnlySpan<T> source, System.Collections.Generic.IComparer<T> comparer)
    {
      var matrix = GetMatrix(source, comparer, out var length);

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
    public static T[] GetSubsequence<T>(System.ReadOnlySpan<T> source)
      => GetSubsequence(source, System.Collections.Generic.Comparer<T>.Default);
  }
}
