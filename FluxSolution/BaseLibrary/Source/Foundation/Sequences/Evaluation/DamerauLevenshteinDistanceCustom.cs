namespace Flux.Metrical
{
  /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</summary>
  /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Triangle_inequality"/>
  /// <remarks>Implemented based on the Wiki article.</remarks>
  public sealed class DamerauLevenshteinDistanceCustom<T>
    : AMetrical<T>, IMatrixCustomDp<T>, IMetricDistanceCustom<T>
    where T : notnull
  {
    public double CostOfDeletion { get; set; } = 1;
    public double CostOfInsertion { get; set; } = 1;
    public double CostOfSubstitution { get; set; } = 1;
    public double CostOfTransposition { get; set; } = 1;

    public DamerauLevenshteinDistanceCustom(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      : base(equalityComparer)
    { }
    public DamerauLevenshteinDistanceCustom()
      : base()
    { }

    /// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
    public double[,] GetCustomDpMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var sourceLength = source.Length;
      var targetLength = target.Length;

      var ldg = new double[sourceLength + 2, targetLength + 2];

      var dr = new System.Collections.Generic.Dictionary<T, int>(EqualityComparer); // Dictionary of items from both lists.
      for (var si = sourceLength - 1; si >= 0; si--)
        if (!dr.ContainsKey(source[si]))
          dr[source[si]] = 0;
      for (var ti = targetLength - 1; ti >= 0; ti--)
        if (!dr.ContainsKey(target[ti]))
          dr[target[ti]] = 0;

      var maxDistance = (sourceLength + targetLength) * CostOfInsertion;

      ldg[0, 0] = maxDistance;

      for (var i = sourceLength + 1; i >= 1; i--)
      {
        ldg[i, 1] = (i - 1) * CostOfInsertion;
        ldg[i, 0] = maxDistance;
      }
      for (var j = targetLength + 1; j >= 1; j--)
      {
        ldg[1, j] = (j - 1) * CostOfInsertion;
        ldg[0, j] = maxDistance;
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
            ldg[si, ti + 1] + CostOfDeletion,
            ldg[si + 1, ti] + CostOfInsertion,
            isEqual ? ldg[si, ti] : ldg[si, ti] + CostOfSubstitution,
            ldg[lsi, ltim] + (si - lsi - 1) + CostOfTransposition + (ti - ltim - 1)
          );

          if (isEqual)
            ltim = ti;
        }

        dr[sourceItem] = si;
      }

      return ldg;
    }

    public double GetCustomMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      OptimizeEnds(source, target, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

      if (sourceCount == 0) return targetCount;
      else if (targetCount == 0) return sourceCount;

      var matrix = GetCustomDpMatrix(source, target);

      return matrix[sourceCount + 1, targetCount + 1];
    }
  }
}
