namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
    /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-subsequence/"/></para>
    /// <para><seealso href="https://www.ics.uci.edu/~eppstein/161/960229.html"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
    /// <remarks>
    /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para>Implemented based on the Wiki article.</para>
    /// <para>This algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
    /// </remarks>
    public static int LongestCommonSubsequenceLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      TrimCommonEnds(source, target, out source, out target, out var equalAtStart, out var equalAtEnd, equalityComparer);

      var v1 = new int[target.Length + 1];
      var v0 = new int[target.Length + 1];

      for (var i = source.Length - 1; i >= 0; i--)
      {
        (v0, v1) = (v1, v0);

        for (var j = target.Length - 1; j >= 0; j--)
          v0[j] = equalityComparer.Equals(source[i], target[j]) ? v1[j + 1] + 1 : System.Math.Max(v1[j], v0[j + 1]);
      }

      return v0[0] + equalAtStart + equalAtEnd;
    }

    /// <summary>
    /// <para>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
    /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-subsequence/"/></para>
    /// <para><seealso href="https://www.ics.uci.edu/~eppstein/161/960229.html"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
    /// <remarks>
    /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para>Implemented based on the Wiki article.</para>
    /// </remarks>
    public static int[,] LongestCommonSubsequenceMatrix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var lcsg = new int[source.Length + 1, target.Length + 1];

      for (int si = source.Length - 1; si >= 0; si--)
        lcsg[si, 0] = 0;
      for (int ti = target.Length - 1; ti >= 0; ti--)
        lcsg[0, ti] = 0;

      for (int si = 0; si < source.Length; si++)
        for (int ti = 0; ti < target.Length; ti++)
          lcsg[si + 1, ti + 1] = equalityComparer.Equals(source[si], target[ti]) ? lcsg[si, ti] + 1 : System.Math.Max(lcsg[si + 1, ti], lcsg[si, ti + 1]);

      return lcsg;
    }

    /// <summary>
    /// <para>Compute the longest common subsequence (LCS) edit distance when only insertion and deletion is allowed (not substitution), or when the cost of the substitution is double of the cost of an insertion or deletion.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <remarks>
    /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para>Implemented based on the Wiki article.</para>
    /// <para>This algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
    /// </remarks>
    public static int LongestCommonSubsequenceMetric<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => (source.Length + target.Length) - 2 * LongestCommonSubsequenceLength(source, target, equalityComparer);

    /// <summary>
    /// <para>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
    /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-subsequence/"/></para>
    /// <para><seealso href="https://www.ics.uci.edu/~eppstein/161/960229.html"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
    /// <remarks>
    /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para>Implemented based on the Wiki article.</para>
    /// <para>This algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
    /// </remarks>
    public static double LongestCommonSubsequenceSmc<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => 1d - LongestCommonSubsequenceSmd(source, target, equalityComparer);

    /// <summary>
    /// <para>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
    /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-subsequence/"/></para>
    /// <para><seealso href="https://www.ics.uci.edu/~eppstein/161/960229.html"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
    /// <remarks>
    /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para>Implemented based on the Wiki article.</para>
    /// <para>This algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
    /// </remarks>
    public static double LongestCommonSubsequenceSmd<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => (double)LongestCommonSubsequenceMetric(source, target, equalityComparer) / (double)System.Math.Max(source.Length, target.Length);

    /// <summary>
    /// <para>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para>Returns the items comprising the longest sub-sequence.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/></para>
    /// <para><seealso href="http://www.geeksforgeeks.org/longest-common-subsequence/"/></para>
    /// <para><seealso href="https://www.ics.uci.edu/~eppstein/161/960229.html"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <remarks>
    /// <para>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</para>
    /// <para>Implemented based on the Wiki article.</para>
    /// </remarks>
    public static System.Collections.Generic.List<T> LongestCommonSubsequenceValues<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      matrix = LongestCommonSubsequenceMatrix(source, target);

      var lcsv = new System.Collections.Generic.List<T>();

      var si = source.Length;
      var ti = target.Length;

      while (si > 0 && ti > 0)
      {
        if (equalityComparer.Equals(source[si - 1], target[ti - 1]))
        {
          lcsv.Insert(0, source[si - 1]);

          si--;
          ti--;
        }
        else if (matrix[si, ti - 1] > matrix[si - 1, ti]) // If not same, then go in the direction of the greater one.
          ti--;
        else
          si--;
      }

      return lcsv;
    }
  }
}
