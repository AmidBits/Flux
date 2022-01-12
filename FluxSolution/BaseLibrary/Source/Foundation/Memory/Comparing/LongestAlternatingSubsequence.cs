namespace Flux.Metrical
{
  /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longest_alternating_subsequence"/>
  public sealed class LongestAlternatingSubsequence<T>
  {
    public LongestAlternatingSubsequence(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer;
    public LongestAlternatingSubsequence()
      : this(System.Collections.Generic.Comparer<T>.Default)
    {
    }

    public System.Collections.Generic.IComparer<T> Comparer { get; private set; }

    public int[,] GetMatrix(System.ReadOnlySpan<T> source, out int length)
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
          var cmp = Comparer.Compare(source[j], source[i]);

          if (cmp < 0 && matrix[j, 1] + 1 is var mj1p1 && matrix[i, 0] < mj1p1)
            matrix[i, 0] = mj1p1;

          if (cmp > 0 && matrix[j, 0] + 1 is var mj0p1 && matrix[i, 1] < mj0p1)
            matrix[i, 1] = mj0p1;
        }

        length = System.Math.Max(length, System.Math.Max(matrix[i, 0], matrix[i, 1]));
      }

      return matrix;
    }

    public T[] GetSubsequence(System.ReadOnlySpan<T> source, out int[,] matrix)
    {
      matrix = GetMatrix(source, out var length);

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

    public int GetLengthMeasure(System.ReadOnlySpan<T> source)
    {
      GetMatrix(source, out var length);

      return length;
    }
  }
}
