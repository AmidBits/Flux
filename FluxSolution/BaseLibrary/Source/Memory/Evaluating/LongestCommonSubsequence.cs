namespace Flux.Metrical
{
  /// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/> 
  /// <seealso cref="http://www.geeksforgeeks.org/longest-common-subsequence/"/>
  /// <seealso cref="https://www.ics.uci.edu/~eppstein/161/960229.html"/>
  /// <remarks>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
  /// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
  public sealed class LongestCommonSubsequence<T>
    : IEditDistanceDynamicProgrammable<T>, IEditDistanceEquatable<T>, IEditDistanceOptimizable<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
  {
    public LongestCommonSubsequence(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
    public LongestCommonSubsequence()
      => EqualityComparer = System.Collections.Generic.EqualityComparer<T>.Default;

    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    /// <summary></summary>
    
    public int[,] GetDpMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var lcsg = new int[source.Length + 1, target.Length + 1];

      for (int si = source.Length - 1; si >= 0; si--)
        lcsg[si, 0] = 0;
      for (int ti = target.Length - 1; ti >= 0; ti--)
        lcsg[0, ti] = 0;

      for (int si = 0; si < source.Length; si++)
        for (int ti = 0; ti < target.Length; ti++)
          lcsg[si + 1, ti + 1] = EqualityComparer.Equals(source[si], target[ti]) ? lcsg[si, ti] + 1 : System.Math.Max(lcsg[si + 1, ti], lcsg[si, ti + 1]);

      return lcsg;
    }

    /// <summary>Returns the items comprising the longest sub-sequence.</summary>
    
    public System.Collections.Generic.IList<T> GetSubsequence(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int[,] matrix)
    {
      matrix = GetDpMatrix(source, target);

      var lcs = new System.Collections.Generic.List<T>();

      var si = source.Length;
      var ti = target.Length;

      while (si > 0 && ti > 0)
      {
        if (EqualityComparer.Equals(source[si - 1], target[ti - 1]))
        {
          lcs.Insert(0, source[si - 1]);

          si--;
          ti--;
        }
        else if (matrix[si, ti - 1] > matrix[si - 1, ti]) // If not same, then go in the direction of the greater one.
          ti--;
        else
          si--;
      }

      return lcs;
    }

    
    public int GetEditDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => source.Length + target.Length - 2 * GetLengthMetric(source, target);

    
    public int GetLengthMetric(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      ((IEditDistanceOptimizable<T>)this).OptimizeEnds(source, target, out source, out target, out var sourceCount, out var targetCount, out var equalAtStart, out var equalAtEnd);

      var v1 = new int[targetCount + 1];
      var v0 = new int[targetCount + 1];

      for (var i = sourceCount - 1; i >= 0; i--)
      {
        (v0, v1) = (v1, v0);

        for (var j = targetCount - 1; j >= 0; j--)
          v0[j] = EqualityComparer.Equals(source[i], target[j]) ? v1[j + 1] + 1 : System.Math.Max(v1[j], v0[j + 1]);
      }

      return v0[0] + equalAtStart + equalAtEnd;
    }

    
    public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => (double)GetLengthMetric(source, target) / (double)System.Math.Max(source.Length, target.Length);

    
    public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => 1.0 - GetSimpleMatchingCoefficient(source, target);
  }
}
