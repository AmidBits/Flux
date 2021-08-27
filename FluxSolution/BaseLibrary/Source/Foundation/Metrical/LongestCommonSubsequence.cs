using System.Linq;

namespace Flux.Metrical
{
  /// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/> 
  /// <seealso cref="http://www.geeksforgeeks.org/longest-common-subsequence/"/>
  /// <seealso cref="https://www.ics.uci.edu/~eppstein/161/960229.html"/>
  /// <remarks>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
  /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
  public class LongestCommonSubsequence<T>
    : AMetrical<T>, IFullMatrix<T>, IMetricDistance<T>, IMetricLength<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
  {
    public LongestCommonSubsequence()
      : base(System.Collections.Generic.EqualityComparer<T>.Default)
    { }
    public LongestCommonSubsequence(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      : base(equalityComparer)
    { }

    /// <summary></summary>
    public int[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var lcsg = new int[source.Length + 1, target.Length + 1];

      for (int si = source.Length - 1; si >= 0; si--)
        lcsg[si, 0] = 0;
      for (int ti = target.Length - 1; ti >= 0; ti--)
        lcsg[0, ti] = 0;

      for (int si = 0; si <= source.Length; si++)
        for (int ti = 0; ti <= target.Length; ti++)
          lcsg[si, ti] = EqualityComparer.Equals(source[si - 1], target[ti - 1]) ? lcsg[si - 1, ti - 1] + 1 : System.Math.Max(lcsg[si - 1, ti], lcsg[si, ti - 1]);

      return lcsg;
    }

    /// <summary>Returns the items comprising the longest sub-sequence.</summary>
    public System.Collections.Generic.List<T> GetList(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var lcs = new System.Collections.Generic.List<T>();

      var lcsg = GetFullMatrix(source, target);

      var bt = Backtrack(lcsg, source, target, source.Length, target.Length).ToList();

      var k = source.Length;
      var l = target.Length;

      while (k > 0 && l > 0)
      {
        if (EqualityComparer.Equals(source[k - 1], target[l - 1]))
        {
          lcs.Insert(0, source[k - 1]);

          k--;
          l--;
        }
        else if (lcsg[k, l - 1] > lcsg[k - 1, l]) // If not same, then go in the direction of the greater one.
          l--;
        else
          k--;
      }

      return lcs;

      System.Collections.Generic.IEnumerable<T> Backtrack(int[,] C, System.ReadOnlySpan<T> aStr, System.ReadOnlySpan<T> bStr, int x, int y)
      {
        if (x == 0 | y == 0)
          return System.Linq.Enumerable.Empty<T>();
        if (EqualityComparer.Equals(aStr[x - 1], bStr[y - 1])) // x-1, y-1
          return Backtrack(C, aStr, bStr, x - 1, y - 1).Append(aStr[x - 1]); // x-1
        if (C[x, y - 1] > C[x - 1, y])
          return Backtrack(C, aStr, bStr, x, y - 1);
        return Backtrack(C, aStr, bStr, x - 1, y);
      }
    }

    public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => source.Length + target.Length - 2 * GetMetricLength(source, target);

    public int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      OptimizeEnds(source, target, out source, out target, out var sourceCount, out var targetCount, out var equalAtStart, out var equalAtEnd);

      var v1 = new int[targetCount + 1];
      var v0 = new int[targetCount + 1];

      for (var i = sourceCount - 1; i >= 0; i--)
      {
        var swap = v1; v1 = v0; v0 = swap;

        for (var j = targetCount - 1; j >= 0; j--)
        {
          v0[j] = EqualityComparer.Equals(source[i], target[j]) ? v1[j + 1] + 1 : System.Math.Max(v1[j], v0[j + 1]);
        }
      }

      return v0[0] + equalAtStart + equalAtEnd;
    }

    public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => (double)GetMetricLength(source, target) / (double)System.Math.Max(source.Length, target.Length);

    public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => 1.0 - GetSimpleMatchingCoefficient(source, target);
  }
}
