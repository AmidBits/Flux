namespace Flux.Metrical
{
  /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</summary>
  /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Triangle_inequality"/>
  /// <remarks>Implemented based on the Wiki article.</remarks>
  public class DamerauLevenshteinDistance<T>
    : AMetrical<T>, IMatrixDp<T>, IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
    where T : notnull
  {
    public DamerauLevenshteinDistance(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      : base(equalityComparer)
    { }
    public DamerauLevenshteinDistance()
      : base()
    { }

    /// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
    public int[,] GetDpMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var sourceLength = source.Length;
      var targetLength = target.Length;

      var ldg = new int[sourceLength + 2, targetLength + 2];

      var dr = new System.Collections.Generic.Dictionary<T, int>(EqualityComparer); // Dictionary of items from both lists.
      for (var si = sourceLength - 1; si >= 0; si--)
        if (!dr.ContainsKey(source[si]))
          dr[source[si]] = 0;
      for (var ti = targetLength - 1; ti >= 0; ti--)
        if (!dr.ContainsKey(target[ti]))
          dr[target[ti]] = 0;

      var maxDistance = sourceLength + targetLength;

      ldg[0, 0] = maxDistance;

      for (var si = sourceLength + 1; si >= 1; si--)
      {
        ldg[si, 1] = si - 1;
        ldg[si, 0] = maxDistance;
      }
      for (var ti = targetLength + 1; ti >= 1; ti--)
      {
        ldg[1, ti] = ti - 1;
        ldg[0, ti] = maxDistance;
      }

      for (var si = 1; si <= sourceLength; si++)
      {
        var ltim = 0; // Last target index of target item matching source item.

        var sourceItem = source[si - 1];

        for (var ti = 1; ti <= targetLength; ti++)
        {
          var targetItem = target[ti - 1];

          var lsi = dr[targetItem]; // Last source index of source item.

          var isEqual = EqualityComparer.Equals(sourceItem, targetItem);

          ldg[si + 1, ti + 1] = Maths.Min(
            ldg[si, ti + 1] + 1, // Deletion.
            ldg[si + 1, ti] + 1, // Insertion
            isEqual ? ldg[si, ti] : ldg[si, ti] + 1, // Substitution.
            ldg[lsi, ltim] + (si - lsi - 1) + 1 + (ti - ltim - 1) // Transposition.
          );

          if (isEqual)
            ltim = ti;
        }

        dr[sourceItem] = si;
      }

      return ldg;
    }

    ///// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
    //public double[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion, double costOfInsertion, double costOfSubstitution, double costOfTransposition)
    //{
    //  var sourceLength = source.Length;
    //  var targetLength = target.Length;

    //  var ldg = new double[sourceLength + 2, targetLength + 2];

    //  var dr = new System.Collections.Generic.Dictionary<T, int>(EqualityComparer); // Dictionary of items from both lists.
    //  for (var si = sourceLength - 1; si >= 0; si--)
    //    if (!dr.ContainsKey(source[si]))
    //      dr[source[si]] = 0;
    //  for (var ti = targetLength - 1; ti >= 0; ti--)
    //    if (!dr.ContainsKey(target[ti]))
    //      dr[target[ti]] = 0;

    //  var maxDistance = (sourceLength + targetLength) * costOfInsertion;

    //  ldg[0, 0] = maxDistance;

    //  for (var si = sourceLength + 1; si >= 1; si--)
    //  {
    //    ldg[si, 1] = (si - 1);
    //    ldg[si, 0] = maxDistance;
    //  }
    //  for (var ti = targetLength + 1; ti >= 1; ti--)
    //  {
    //    ldg[1, ti] = (ti - 1);
    //    ldg[0, ti] = maxDistance;
    //  }

    //  for (var si = 1; si <= sourceLength; si++)
    //  {
    //    var ltim = 0; // Last target index of target item matching source item.

    //    var sourceItem = source[si - 1];

    //    for (var ti = 1; ti <= targetLength; ti++)
    //    {
    //      var targetItem = target[ti - 1];

    //      var lsi = dr[targetItem]; // Last source index of source item.

    //      var isEqual = EqualityComparer.Equals(sourceItem, targetItem);

    //      ldg[si + 1, ti + 1] = Maths.Min(
    //        ldg[si, ti + 1] + costOfDeletion,
    //        ldg[si + 1, ti] + costOfInsertion,
    //        isEqual ? ldg[si, ti] : ldg[si, ti] + costOfSubstitution,
    //        ldg[lsi, ltim] + (si - lsi - 1) + costOfTransposition + (ti - ltim - 1)
    //      );

    //      if (isEqual)
    //        ltim = ti;
    //    }

    //    dr[sourceItem] = si;
    //  }

    //  return ldg;
    //}

    public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      OptimizeEnds(source, target, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

      if (sourceCount == 0) return targetCount;
      else if (targetCount == 0) return sourceCount;

      var matrix = GetDpMatrix(source, target);

      return matrix[sourceCount + 1, targetCount + 1];
    }

    public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => 1 - GetSimpleMatchingDistance(source, target);

    public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => (double)GetMetricDistance(source, target) / (double)System.Math.Max(source.Length, target.Length);
  }
}
