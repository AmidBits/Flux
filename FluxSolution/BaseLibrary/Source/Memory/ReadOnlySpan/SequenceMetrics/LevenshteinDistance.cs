namespace Flux.SequenceMetrics
{
  public class LevenshteinDistance<T>
      : IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
  {
    /// <summary>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other. Uses the specified comparer.</summary>
    /// <see cref = "https://en.wikipedia.org/wiki/Levenshtein_distance" />
    public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      Helper.OptimizeEnds(source, target, comparer, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

      if (sourceCount == 0) return targetCount;
      else if (targetCount == 0) return sourceCount;

      var v1 = new int[targetCount + 1];
      var v0 = new int[targetCount + 1];

      for (var j = 0; j <= targetCount; j++) v0[j] = j;

      for (var i = 0; i < sourceCount; i++)
      {
        var rotate = v1; v1 = v0; v0 = rotate;

        v0[0] = i + 1; // The first element is delete (i + 1) chars from source to match empty target.

        for (var j = 0; j < targetCount; j++)
        {
          v0[j + 1] = System.Math.Min(System.Math.Min(v1[j + 1] + 1, v0[j] + 1), comparer.Equals(source[i], target[j]) ? v1[j] : v1[j] + 1); // Minimum costs of deletion, insertion and substitution, resp.
        }
      }

      return v0[targetCount];

      #region Optimized version with only one vector and variables for prior costs, not thoroughly tested!
      //var v0 = new int[target.Length]; // Current row of costs.

      //for (var j = 0; j < target.Length; j++) v0[j] = j + 1; // Initialize v1 (the previous row of costs) to an edit distance for an empty source, i.e. the the number of characters to delete from target.

      //var current = 0;

      //for (var i = 0; i < source.Length; i++)
      //{
      //  current = i;

      //  for (int j = 0, left = i; j < target.Length; j++)
      //  {
      //    var above = current;
      //    current = left; // cost on diagonal (substitution)
      //    left = v0[j];

      //    if (!comparer.Equals(source[i], target[j]))
      //    {
      //      current = Math.Min(above + 1, left + 1, current + 1);
      //    }

      //    v0[j] = current;
      //  }
      //}

      //return v0[target.Length - 1];
      #endregion Optimized version with only one vector and variables for prior costs, not thoroughly tested!

      #region Another optimized version with one vector and temp variables this time, not thoroughly tested!
      //var v0 = new int[target.Length + 1]; // Current row of costs.
      //for (var i = 0; i < target.Length; v0[i] = i++) ;

      //for (var i = 0; i < source.Length; i++)
      //{
      //  v0[0] = i; // initialize with zero cost

      //  for (int j = 0, lastDiagonal = i; j < target.Length; j++)
      //  {
      //    var substitute = lastDiagonal + (comparer.Equals(source[i], target[j]) ? 0 : 1); // last diagonal cost + possible inequality cost

      //    lastDiagonal = v0[j + 1]; // remember the cost of the last diagonal stripe

      //    var insert = v0[j] + 1;
      //    var delete = lastDiagonal + 1; // reuse the recently cached last diagonal in order to reduce use of indexer

      //    v0[j + 1] = Math.Min(substitute, insert, delete);
      //  }
      //}

      //return v0[target.Length];
      #endregion Another optimized version with one vector and temp variables this time, not thoroughly tested!
    }
    public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetMetricDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

    #region ISimpleMatchingCoefficient<T>
    /// <see cref="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
    public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => 1.0 - GetSimpleMatchingDistance(source, target, comparer);
    /// <see cref="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
    public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetSimpleMatchingCoefficient(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
    #endregion ISimpleMatchingCoefficient<T>

    #region ISimpleMatchingDistance<T>
    /// <see cref="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
    public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => (double)GetMetricDistance(source, target, comparer) / (double)System.Math.Max(source.Length, target.Length);
    /// <see cref="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
    public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetSimpleMatchingDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
    #endregion ISimpleMatchingDistance<T>
  }
}
