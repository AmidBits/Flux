namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public static T[] LongestIncreasingSubsequence<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Metrical.LongestIncreasingSubsequence<T>(comparer).GetSubsequence(source, out var _);
    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public static T[] LongestIncreasingSubsequence<T>(this System.ReadOnlySpan<T> source)
      => LongestIncreasingSubsequence(source, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public static T[] LongestIncreasingSubsequence<T>(this SequenceBuilder<T> source, System.Collections.Generic.IComparer<T> comparer)
      where T : notnull
      => new Metrical.LongestIncreasingSubsequence<T>(comparer).GetSubsequence(source.AsReadOnlySpan(), out var _);
    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public static T[] LongestIncreasingSubsequence<T>(this SequenceBuilder<T> source)
      where T : notnull
      => LongestIncreasingSubsequence(source, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace Metrical
  {
    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public sealed class LongestIncreasingSubsequence<T>
    {
      public LongestIncreasingSubsequence(System.Collections.Generic.IComparer<T> comparer)
        => Comparer = comparer;
      public LongestIncreasingSubsequence()
        : this(System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public System.Collections.Generic.IComparer<T> Comparer { get; private set; }


      public int[,] GetMatrix(System.ReadOnlySpan<T> source, out int length)
      {
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

            if (Comparer.Compare(source[matrix[0, mid]], source[i]) < 0)
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


      public T[] GetSubsequence(System.ReadOnlySpan<T> source, out int[,] matrix)
      {
        matrix = GetMatrix(source, out var length);

        var result = new T[length];
        for (int i = length - 1, k = matrix[0, length]; i >= 0; i--, k = matrix[1, k])
          result[i] = source[k];
        return result;
      }


      public int GetLengthMeasure(System.ReadOnlySpan<T> source)
      {
        GetMatrix(source, out var length);

        return length;
      }
    }
  }
}
