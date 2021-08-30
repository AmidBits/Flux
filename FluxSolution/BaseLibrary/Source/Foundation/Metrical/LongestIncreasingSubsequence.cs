namespace Flux.Metrical
{
  /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
  public class LongestIncreasingSubsequence<T>
  {
    private System.Collections.Generic.IComparer<T> m_comparer;

    public LongestIncreasingSubsequence(System.Collections.Generic.IComparer<T> comparer)
      => m_comparer = comparer;
    public LongestIncreasingSubsequence()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }

    public T[] GetLongestIncreasingSubsequence(System.ReadOnlySpan<T> source)
    {
      var sourceLength = source.Length;

      var pi = new int[sourceLength];
      var ki = new int[sourceLength + 1];

      var length = 0;

      for (var i = 0; i < sourceLength; i++)
      {
        var lo = 1;
        var hi = length;

        while (lo <= hi)
        {
          var mid = System.Convert.ToInt32(System.Math.Ceiling((lo + hi) / 2.0)); // Binary middle index.

          if (m_comparer.Compare(source[ki[mid]], source[i]) < 0)
            lo = mid + 1;
          else
            hi = mid - 1;
        }

        var newLength = lo; // After searching, lo is 1 greater than the length of the longest prefix of X[i].

        pi[i] = ki[newLength - 1]; // The predecessor of X[i] is the last index of the subsequence of length newL-1;
        ki[newLength] = i;

        if (newLength > length)
          length = newLength;
      }

      var lis = new T[length]; // Reconstruct the longest increasing subsequence (LIS).
      var k = ki[length];
      for (int i = length - 1; i >= 0; i--)
      {
        lis[i] = source[k];
        k = pi[k];
      }
      return lis;
    }
  }
}
