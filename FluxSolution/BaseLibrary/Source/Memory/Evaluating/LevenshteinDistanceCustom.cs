//namespace Flux.Metrical
//{
//  /// <summary>
//  /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
//  /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
//  /// </summary>
//  /// <remarks>Implemented based on the Wiki article.</remarks>
//  public sealed class LevenshteinDistanceCustom<T>
//    : IEditDistanceCustomEquatable<T>, IEditDistanceOptimizable<T>
//  {
//    public double CostOfDeletion { get; set; } = 1;
//    public double CostOfInsertion { get; set; } = 1;
//    public double CostOfSubstitution { get; set; } = 1;

//    public LevenshteinDistanceCustom(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
//      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
//    public LevenshteinDistanceCustom()
//      => EqualityComparer = System.Collections.Generic.EqualityComparer<T>.Default;

//    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

//    /// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>

//    public double[,] GetCustomMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
//    {
//      var sourceLength = source.Length;
//      var targetLength = target.Length;

//      var ldg = new double[sourceLength + 1, targetLength + 1];

//      for (var si = 1; si <= sourceLength; si++)
//        ldg[si, 0] = si * CostOfInsertion;
//      for (var ti = 1; ti <= targetLength; ti++)
//        ldg[0, ti] = ti * CostOfInsertion;

//      for (var si = 1; si <= sourceLength; si++)
//        for (var ti = 1; ti <= targetLength; ti++)
//          ldg[si, ti] = double.Min(
//            ldg[si - 1, ti] + CostOfDeletion,
//            double.Min(
//              ldg[si, ti - 1] + CostOfInsertion,
//              EqualityComparer.Equals(source[si - 1], target[ti - 1]) ? ldg[si - 1, ti - 1] : ldg[si - 1, ti - 1] + CostOfSubstitution
//            )
//          );

//      return ldg;
//    }

//    public double GetCustomEditDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
//    {
//      ((IEditDistanceOptimizable<T>)this).OptimizeEnds(source, target, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

//      if (sourceCount == 0) return targetCount;
//      else if (targetCount == 0) return sourceCount;

//      var v1 = new double[targetCount + 1]; // Row of costs, one row back (previous row).
//      var v0 = new double[targetCount + 1]; // Row of costs, current row.

//      for (var j = 0; j <= targetCount; j++)
//        v0[j] = j * CostOfInsertion; // Initialize the 'previous' (swapped to 'v1' in loop) row.

//      for (var i = 0; i < sourceCount; i++)
//      {
//        (v0, v1) = (v1, v0);

//        v0[0] = i + CostOfDeletion; // The first element is delete (i + 1) chars from source to match empty target.

//        for (var j = 0; j < targetCount; j++)
//        {
//          v0[j + 1] = double.Min(
//            v1[j + 1] + CostOfDeletion, // Deletion.
//            double.Min(
//              v0[j] + CostOfInsertion, // Insertion.
//              EqualityComparer.Equals(source[i], target[j]) ? v1[j] : v1[j] + CostOfSubstitution // Substitution.
//            )
//          );
//        }
//      }

//      return v0[targetCount];
//    }
//  }
//}
