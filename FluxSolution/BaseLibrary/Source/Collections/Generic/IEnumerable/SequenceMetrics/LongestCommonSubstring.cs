using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Finding the longest consecutive sequence of elements common to two or more strings. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
    /// <param name="source">The primary string.</param>
    /// <param name="target">The secondary string.</param>
    /// <returns>The longest number of consecutive elements common to both source and target.</returns>
    public static int LongestCommonSubstring<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.LongestCommonSubstring<T>().GetMeasuredLength(source.ToArray(), target.ToArray(), comparer);
    /// <summary>Finding the longest consecutive sequence of elements common to two or more strings. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
    /// <param name="source">The primary string.</param>
    /// <param name="target">The secondary string.</param>
    /// <returns>The longest number of consecutive elements common to both source and target.</returns>
    public static int LongestCommonSubstring<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.LongestCommonSubstring<T>().GetMeasuredLength(source.ToArray(), target.ToArray(), System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
