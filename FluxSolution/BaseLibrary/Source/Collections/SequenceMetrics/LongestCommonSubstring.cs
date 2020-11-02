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
    : SequenceMetric<T>, IMeasuredLength<T>
    {
      public LongestCommonSubstring()
        : this(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }
      public LongestCommonSubstring(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        : base(equalityComparer)
      {
      }

      public int GetMeasuredLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        var maxLength = 0;

        var v1 = new int[target.Length + 1];
        var v0 = new int[target.Length + 1];

        for (var i = source.Length - 1; i >= 0; i--)
        {
          var swap = v1; v1 = v0; v0 = swap;

          for (var j = target.Length - 1; j >= 0; j--)
          {
            if (EqualityComparer.Equals(source[i], target[j]))
            {
              v0[j] = v1[j + 1] + 1;

              maxLength = System.Math.Max(maxLength, v0[j]);
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
