namespace Flux.SequenceMetrics
{
  public class DamerauLevenshteinDistance<T>
    : IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
    where T : notnull
  {
    /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences, using the specified comparer. Implemented based on the Wiki article.</summary>
    /// <remarks>Takes into account: insertions, deletions, substitutions, or adjacent transpositions.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      Helper.OptimizeEnds(source, target, comparer, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

      if (sourceCount == 0) return targetCount;
      else if (targetCount == 0) return sourceCount;

      var matrix = new int[sourceCount + 2, targetCount + 2];

      int maxDistance = sourceCount + targetCount;

      matrix[0, 0] = maxDistance;

      var dictionary = new System.Collections.Generic.Dictionary<T, int>(sourceCount + targetCount, comparer);

      for (int i = 0; i <= sourceCount; i++)
      {
        matrix[i + 1, 1] = i; matrix[i + 1, 0] = maxDistance;

        if (i < sourceCount && !dictionary.ContainsKey(source[i])) dictionary.Add(source[i], 0);
      }
      for (int j = 0; j <= targetCount; j++)
      {
        matrix[1, j + 1] = j; matrix[0, j + 1] = maxDistance;

        if (j < targetCount && !dictionary.ContainsKey(target[j])) dictionary.Add(target[j], 0);
      }

      for (int i = 1; i <= sourceCount; i++)
      {
        var sourceItem = source[i - 1];

        var db = 0;

        for (int j = 1; j <= targetCount; j++)
        {
          var targetItem = target[j - 1];

          var k = dictionary[targetItem];
          var l = db;

          var cost = 1;

          if (comparer.Equals(sourceItem, targetItem))
          {
            cost = 0;

            db = j;
          }

          matrix[i + 1, j + 1] = System.Math.Min(System.Math.Min(System.Math.Min(matrix[i, j + 1] + 1, matrix[i + 1, j] + 1), matrix[i, j] + cost), matrix[k, l] + (i - k - 1) + 1 + (j - l - 1)); // Minimum cost of deletion, insertion, substitution and adjacent transposition, resp.

          #region Possible optimized version of the if/else above, but needs verification!
          //if (comparer.Equals(sourceItem, targetItem))
          //{
          //  matrix[i + 1, j + 1] = matrix[i, j];

          //  db = j;
          //}
          //else
          //{
          //  matrix[i + 1, j + 1] = Math.Min(matrix[i, j], Math.Min(matrix[i + 1, j], matrix[i, j + 1])) + 1;
          //}

          //matrix[i + 1, j + 1] = Math.Min(matrix[i + 1, j + 1], matrix[k, l] + (i - k - 1) + 1 + (j - l - 1));
          #endregion Possible optimized version of the if/else above, but needs verification!
        }

        dictionary[sourceItem] = i;
      }

      return matrix[sourceCount + 1, targetCount + 1];
    }
    /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences, using the specified comparer. Implemented based on the Wiki article.</summary>
    /// <remarks>Takes into account: insertions, deletions, substitutions, or adjacent transpositions.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
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
