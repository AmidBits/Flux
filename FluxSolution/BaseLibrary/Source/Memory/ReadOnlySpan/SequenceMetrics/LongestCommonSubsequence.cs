namespace Flux
{
  public static partial class XtendSequenceMetrics
  {
    /// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/> 
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-subsequence/"/>
    /// <seealso cref="https://www.ics.uci.edu/~eppstein/161/960229.html"/>
    /// <remarks>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
    /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
    public static int LongestCommonSubsequence<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.LongestCommonSubsequence<T>().GetMetricLength(source, target, comparer);
    /// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/> 
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-subsequence/"/>
    /// <seealso cref="https://www.ics.uci.edu/~eppstein/161/960229.html"/>
    /// <remarks>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
    /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
    public static int LongestCommonSubsequence<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new SequenceMetrics.LongestCommonSubsequence<T>().GetMetricLength(source, target);
  }

  namespace SequenceMetrics
  {
    public class LongestCommonSubsequence<T>
    : IMetricDistance<T>, IMetricLength<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
    {
      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
        => source.Length + target.Length - 2 * GetMetricLength(source, target, comparer);
      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetMetricDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

      /// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences. Uses the specified comparer.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/> 
      /// <seealso cref="http://www.geeksforgeeks.org/longest-common-subsequence/"/>
      /// <seealso cref="https://www.ics.uci.edu/~eppstein/161/960229.html"/>
      /// <remarks>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
      /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
      public int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      {
        comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        Helper.OptimizeEnds(source, target, comparer, out source, out target, out var sourceCount, out var targetCount, out var equalAtStart, out var equalAtEnd);

        var v1 = new int[targetCount + 1];
        var v0 = new int[targetCount + 1];

        for (var i = sourceCount - 1; i >= 0; i--)
        {
          var swap = v1; v1 = v0; v0 = swap;

          for (var j = target.Length - 1; j >= 0; j--)
          {
            v0[j] = comparer.Equals(source[i], target[j]) ? v1[j + 1] + 1 : System.Math.Max(v1[j], v0[j + 1]);
          }
        }

        return v0[0] + equalAtStart + equalAtEnd;
      }
      /// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences. Uses the default comparer.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/> 
      /// <seealso cref="http://www.geeksforgeeks.org/longest-common-subsequence/"/>
      /// <seealso cref="https://www.ics.uci.edu/~eppstein/161/960229.html"/>
      /// <remarks>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
      /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
      public int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetMetricLength(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

      #region ISimpleMatchingCoefficient<T>
      /// <see cref="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
        => (double)GetMetricLength(source, target, comparer) / (double)System.Math.Max(source.Length, target.Length);
      /// <see cref="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetSimpleMatchingCoefficient(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
      #endregion ISimpleMatchingCoefficient<T>

      #region ISimpleMatchingDistance<T>
      /// <see cref="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
        => 1.0 - GetSimpleMatchingCoefficient(source, target, comparer);
      /// <see cref="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetSimpleMatchingDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
      #endregion ISimpleMatchingDistance<T>
    }
  }
}
