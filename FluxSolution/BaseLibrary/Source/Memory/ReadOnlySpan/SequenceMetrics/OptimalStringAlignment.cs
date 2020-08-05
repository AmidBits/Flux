namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Edit_distance"/>
    public static int OptimalStringAlignment<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.OptimalStringAlignment<T>().GetMetricDistance(source, target, comparer);
    /// <summary>Computes the optimal sequence alignment (OSA) using the default comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Edit_distance"/>
    public static int OptimalStringAlignment<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new SequenceMetrics.OptimalStringAlignment<T>().GetMetricDistance(source, target);
  }

  namespace SequenceMetrics
  {
    public class OptimalStringAlignment<T>
    : IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
    {
      /// <summary>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein.</summary>
      /// <seealso cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/Edit_distance"/>
      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      {
        comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        Helper.OptimizeEnds(source, target, comparer, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

        if (sourceCount == 0) return targetCount;
        else if (targetCount == 0) return sourceCount;

        var v2 = new int[targetCount + 1]; // Row of costs, two rows back.
        var v1 = new int[targetCount + 1]; // Row of costs, one row back (previous).
        var v0 = new int[targetCount + 1]; // Current row of costs.

        for (int j = 0; j <= targetCount; j++) v0[j] = j; // Initialize v1 (the previous row of costs) to an edit distance for an empty source, i.e. the the number of characters to delete from target.

        for (int i = 1; i <= sourceCount; i++)
        {
          var rotate = v2; v2 = v1; v1 = v0; v0 = rotate; // Rotate and reuse buffered rows of the cost matrix.

          v0[0] = i; // Edit distance is delete (i) chars from source to match empty target.

          for (int j = 1; j <= targetCount; j++)
          {
            var sourceM1 = source[i - 1]; // Cache the last source element, because it's used twice below.
            var targetM1 = target[j - 1]; // Cache the last target element, because it's used twice below.

            var cost = comparer.Equals(sourceM1, targetM1) ? 0 : 1; // Cache comparison cost.

            if (i > 1 && j > 1 && comparer.Equals(sourceM1, target[j - 2]) && comparer.Equals(source[i - 2], targetM1))
            {
              v0[j] = System.Math.Min(System.Math.Min(System.Math.Min(v1[j] + 1, v0[j - 1] + 1), v1[j - 1] + cost), v2[j - 2] + cost); // Store minimum cost of deletion, insertion, substitution and transposition, respectively.
            }
            else
            {
              v0[j] = System.Math.Min(System.Math.Min(v1[j] + 1, v0[j - 1] + 1), v1[j - 1] + cost); // Store minimum cost of deletion, insertion and substitution, respectively.
            }
          }
        }

        return v0[targetCount];
      }
      /// <summary>Computes the optimal sequence alignment (OSA) using the default comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein.</summary>
      /// <seealso cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/Edit_distance"/>
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
}