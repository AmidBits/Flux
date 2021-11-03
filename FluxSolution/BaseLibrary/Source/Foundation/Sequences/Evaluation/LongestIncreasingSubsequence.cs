namespace Flux.Metrical
{
  /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
  public class LongestIncreasingSubsequence<T>
  {
    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public LongestIncreasingSubsequence(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer ?? throw new System.ArgumentNullException(nameof(comparer));
    public LongestIncreasingSubsequence()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }

    public T[] GetLongestIncreasingSubsequence(System.ReadOnlySpan<T> source)
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

          if (Comparer.Compare(source[v0[mid]], source[i]) < 0)
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
  }
}
