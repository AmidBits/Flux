namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public static T[] LongestIncreasingSubsequence<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IComparer<T> comparer)
    {
      var sourceLength = source.Length;

      var v1 = new int[sourceLength];
      var v0 = new int[sourceLength + 1];

      var length = 0;

      for (var i = 0; i < sourceLength; i++)
      {
        var lo = 1;
        var hi = length;

        while (lo <= hi)
        {
          var mid = System.Convert.ToInt32(System.Math.Ceiling((lo + hi) / 2.0)); // Binary middle index.

          if (comparer.Compare(source[v0[mid]], source[i]) < 0)
            lo = mid + 1;
          else
            hi = mid - 1;
        }

        var newLength = lo; // After searching, lo is 1 greater than the length of the longest prefix of X[i].

        v1[i] = v0[newLength - 1]; // The predecessor of X[i] is the last index of the subsequence of length newL-1;
        v0[newLength] = i;

        if (newLength > length)
          length = newLength;
      }

      var lis = new T[length]; // Reconstruct the longest increasing subsequence (LIS).

      for (int i = length - 1, k = v0[length]; i >= 0; i--, k = v1[k])
        lis[i] = source[k];

      return lis;
    }
    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public static T[] LongestIncreasingSubsequence<T>(this System.ReadOnlySpan<T> source)
      => LongestIncreasingSubsequence(source, System.Collections.Generic.Comparer<T>.Default);
  }
}
