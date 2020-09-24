using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Finding the longest consecutive sequence of elements common to two or more strings. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
    /// <param name="source">The primary string.</param>
    /// <param name="target">The secondary string.</param>
    /// <returns>The longest number of consecutive elements common to both source and target.</returns>
    public static int LongestCommonSubstring<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.LongestCommonSubstring<T>(comparer).GetMeasuredLength(source.ToArray(), target.ToArray());
    /// <summary>Finding the longest consecutive sequence of elements common to two or more strings. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
    /// <param name="source">The primary string.</param>
    /// <param name="target">The secondary string.</param>
    /// <returns>The longest number of consecutive elements common to both source and target.</returns>
    public static int LongestCommonSubstring<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.LongestCommonSubstring<T>().GetMeasuredLength(source.ToArray(), target.ToArray());

    /// <summary>Finding the longest consecutive sequence of elements common to two or more sequences. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
    /// <param name="source">The primary sequence.</param>
    /// <param name="target">The secondary sequence.</param>
    /// <returns>The longest number of consecutive elements common to both source and target.</returns>
    public static int LongestCommonSubstring<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.LongestCommonSubstring<T>(comparer).GetMeasuredLength(source, target);
    /// <summary>Finding the longest consecutive sequence of elements common to two or more sequences. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
    /// <param name="source">The primary sequence.</param>
    /// <param name="target">The secondary sequence.</param>
    /// <returns>The longest number of consecutive elements common to both source and target.</returns>
    public static int LongestCommonSubstring<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new SequenceMetrics.LongestCommonSubstring<T>().GetMeasuredLength(source, target);
  }

  namespace SequenceMetrics
  {
    /// <summary>Finding the longest consecutive sequence of elements common to two or more sequences.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
    public class LongestCommonSubstring<T>
    : IMeasuredLength<T>
    {
      private System.Collections.Generic.IEqualityComparer<T> m_equalityComparer;

      public LongestCommonSubstring(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        => m_equalityComparer = equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default;
      public LongestCommonSubstring()
        : this(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }

      public int GetMeasuredLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        Helper.OptimizeEnds(source, target, m_equalityComparer, out source, out target, out var sourceCount, out var targetCount, out var equalAtStart, out var equalAtEnd);

        var maxLength = equalAtStart > equalAtEnd ? equalAtStart : equalAtEnd;

        var v1 = new int[targetCount + 1];
        var v0 = new int[targetCount + 1];

        for (var i = sourceCount - 1; i >= 0; i--)
        {
          var swap = v1; v1 = v0; v0 = swap;

          for (var j = targetCount - 1; j >= 0; j--)
          {
            if (m_equalityComparer.Equals(source[i], target[j]))
            {
              maxLength = System.Math.Max(maxLength, v0[j] = v1[j + 1] + 1); // Note: inline assignment.
            }
            else
            {
              v0[j] = 0;
            }
          }
        }

        return maxLength;
      }
    }
  }
}
