namespace Flux.Metrical
{
  /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
  public sealed class LongestIncreasingSubsequence<T>
    : IMeasuredLengthComputable<T>
  {
    public System.Collections.Generic.Comparer<T> Comparer { get; }

    public LongestIncreasingSubsequence(System.Collections.Generic.Comparer<T> comparer)
    {
      Comparer = comparer;
    }
    public LongestIncreasingSubsequence()
      : this(System.Collections.Generic.Comparer<T>.Default)
    {
    }

    public int GetMeasuredLength(System.ReadOnlySpan<T> source)
    {
      var sourceLength = source.Length;

      var matrix = new int[sourceLength, 2];

      for (var i = 0; i < sourceLength; i++) // Initialize BOTH dimensions to 1.
      {
        matrix[i, 0] = 1; // Length of the longest alternating subsequence ending at index i and last element is greater than its previous element.
        matrix[i, 1] = 1; // Length of the longest alternating subsequence ending at index i and last element is smaller than its previous element.
      }

      var las = 1;

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

        las = System.Math.Max(las, System.Math.Max(matrix[i, 0], matrix[i, 1]));
      }

      //var list = new System.Collections.Generic.List<int>();
      //var mark = 0;
      //for (var i = 0; i < source.Length; i++)
      //{
      //  var max = System.Math.Max(matrix[i, 0], matrix[i, 1]);
      //  if (max > mark)
      //  {
      //    list.Add( i );
      //    mark++;
      //  }
      //}

      return las;
    }
  }
}
