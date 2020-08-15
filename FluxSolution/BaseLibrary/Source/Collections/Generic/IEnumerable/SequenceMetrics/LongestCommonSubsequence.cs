using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/> 
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-subsequence/"/>
    /// <seealso cref="https://www.ics.uci.edu/~eppstein/161/960229.html"/>
    /// <remarks>It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
    /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
    public static int LongestCommonSubsequence<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.LongestCommonSubsequence<T>().GetMetricLength(source.ToArray(), target.ToArray(), comparer);
    /// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/> 
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-subsequence/"/>
    /// <seealso cref="https://www.ics.uci.edu/~eppstein/161/960229.html"/>
    /// <remarks>It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
    /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
    public static int LongestCommonSubsequence<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.LongestCommonSubsequence<T>().GetMetricLength(source.ToArray(), target.ToArray(), System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
