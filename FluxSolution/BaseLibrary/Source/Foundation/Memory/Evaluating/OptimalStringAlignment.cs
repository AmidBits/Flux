namespace Flux.Metrical
{
  /// <summary>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</summary>
  /// <seealso cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Edit_distance"/>
  /// <remarks>Implemented based on the Wiki article.</remarks>
  public sealed class OptimalStringAlignment<T>
    : IEditDistanceBacktrackable<T>, IEditDistanceEquatable<T>, IEditDistanceOptimizable<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
  {
    public OptimalStringAlignment(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
    public OptimalStringAlignment()
      => EqualityComparer = System.Collections.Generic.EqualityComparer<T>.Default;

    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    /// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
    [System.Diagnostics.Contracts.Pure]
    public int[,] GetMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var sourceLength = source.Length;
      var targetLength = target.Length;

      var ldg = new int[sourceLength + 1, targetLength + 1];

      for (var si = 1; si <= sourceLength; si++)
        ldg[si, 0] = si;
      for (var ti = 1; ti <= targetLength; ti++)
        ldg[0, ti] = ti;

      for (int si = 1; si <= sourceLength; si++)
      {
        var sourceItem = source[si - 1];

        for (int ti = 1; ti <= targetLength; ti++)
        {
          var targetItem = target[ti - 1];

          ldg[si, ti] = ldg[si, ti] = Maths.Min(
            ldg[si - 1, ti] + 1, // Deletion.
            ldg[si, ti - 1] + 1, // Insertion.
            EqualityComparer.Equals(sourceItem, targetItem) ? ldg[si - 1, ti - 1] : ldg[si - 1, ti - 1] + 1, // Substitution.
            si > 1 && ti > 1 && EqualityComparer.Equals(sourceItem, target[ti - 2]) && EqualityComparer.Equals(source[si - 2], targetItem) ? ldg[si - 2, ti - 2] + 1 : int.MaxValue // Transposition.
          );
        }
      }

      return ldg;
    }

    ///// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
    //public double[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion, double costOfInsertion, double costOfSubstitution, double costOfTransposition)
    //{
    //  var ldg = new double[source.Length + 1, target.Length + 1];

    //  for (var si = 1; si <= source.Length; si++)
    //    ldg[si, 0] = si * costOfInsertion;
    //  for (var ti = 1; ti <= target.Length; ti++)
    //    ldg[0, ti] = ti * costOfInsertion;

    //  for (int si = 1; si <= source.Length; si++)
    //  {
    //    var sourceItem = source[si - 1];

    //    for (int ti = 1; ti <= target.Length; ti++)
    //    {
    //      var targetItem = target[ti - 1];

    //      ldg[si, ti] = ldg[si, ti] = Maths.Min(
    //        ldg[si - 1, ti] + costOfDeletion,
    //        ldg[si, ti - 1] + costOfInsertion,
    //        EqualityComparer.Equals(sourceItem, targetItem) ? ldg[si - 1, ti - 1] : ldg[si - 1, ti - 1] + costOfSubstitution,
    //        si > 1 && ti > 1 && EqualityComparer.Equals(sourceItem, target[ti - 2]) && EqualityComparer.Equals(source[si - 2], targetItem) ? ldg[si - 2, ti - 2] + costOfTransposition : double.MaxValue
    //      );
    //    }
    //  }

    //  return ldg;
    //}

    [System.Diagnostics.Contracts.Pure]
    public int GetEditDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      ((IEditDistanceOptimizable<T>)this).OptimizeEnds(source, target, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

      if (sourceCount == 0) return targetCount;
      else if (targetCount == 0) return sourceCount;

      var v2 = new int[targetCount + 1]; // Row of costs, two rows back.
      var v1 = new int[targetCount + 1]; // Row of costs, one row back (previous).
      var v0 = new int[targetCount + 1]; // Row of costs, current row.

      for (int ti = 0; ti <= targetCount; ti++) v0[ti] = ti; // Initialize v1 (the previous row of costs) to an edit distance for empty source items, i.e. the the number of characters to delete from target.

      for (int si = 1; si <= sourceCount; si++)
      {
        var rotate = v2; v2 = v1; v1 = v0; v0 = rotate; // Rotate and reuse buffered rows of the cost matrix.

        v0[0] = si; // Edit distance is delete (i) chars from source to match empty target.

        var sourceItem = source[si - 1];

        for (int ti = 1; ti <= targetCount; ti++)
        {
          var targetItem = target[ti - 1];

          v0[ti] = Maths.Min(
            v1[ti] + 1, // Deletion.
            v0[ti - 1] + 1, // Insertion.
            EqualityComparer.Equals(sourceItem, targetItem) ? v1[ti - 1] : v1[ti - 1] + 1, // Substitution.
            si > 1 && ti > 1 && EqualityComparer.Equals(sourceItem, target[ti - 2]) && EqualityComparer.Equals(source[si - 2], targetItem) ? v2[ti - 2] + 1 : int.MaxValue // Transposition.
          );
        }
      }

      return v0[targetCount];
    }

    [System.Diagnostics.Contracts.Pure]
    public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => 1.0 - GetSimpleMatchingDistance(source, target);

    [System.Diagnostics.Contracts.Pure]
    public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => (double)GetEditDistance(source, target) / (double)System.Math.Max(source.Length, target.Length);
  }
}