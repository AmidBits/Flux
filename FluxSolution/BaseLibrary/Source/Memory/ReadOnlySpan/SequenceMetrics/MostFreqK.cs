using System.Linq;

namespace Flux.SequenceMetrics
{
  public class MostFreqK<T>
    : IMeasuredDistance<T>
  {
    public enum SimilarityScoringBehavior
    {
      /// <summary>For each element match, when also the frequencies match, add only one of the frequencies to the similary score.</summary>
      OnlyOneFrequencyWhenEqual,
      /// <summary>For each element match, add both frequencies to the similarity score.</summary>
      SumOfBothFrequencies
    }

    /// <summary>The number of hash sets (i.e. element and frequency) to use.</summary>
    public int K { get; set; }

    /// <summary>Used when computing the measured distance.</summary>
    public int MaxDistance { get; set; }

    public SimilarityScoringBehavior ScoringBehavior { get; set; } = SimilarityScoringBehavior.OnlyOneFrequencyWhenEqual;

    public MostFreqK(int k, int maxDistance)
    {
      K = k;

      MaxDistance = maxDistance;
    }

    /// <summary>The MostFreqKDistance is a string metric technique for quickly estimating how similar two ordered sets or strings are.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Most_frequent_k_characters#Most_frequent_K_hashing"/> 
    public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> Hashing(System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      foreach (var kvp in source.GroupBy(c => c, comparer).Select(g => new System.Collections.Generic.KeyValuePair<T, int>(g.Key, g.Count())).OrderByDescending(g => g.Value).Take(K))
      {
        yield return kvp;
      }
    }
    /// <summary>The MostFreqKDistance is a string metric technique for quickly estimating how similar two ordered sets or strings are, using the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Most_frequent_k_characters#Most_frequent_K_hashing"/> 
    public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> Hashing(System.Collections.Generic.IEnumerable<T> source)
      => Hashing(source, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>The MostFreqKDistance is a string metric technique for quickly estimating how similar two ordered sets or strings are.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Most_frequent_k_characters#Most_frequent_K_distance"/> 
    public int GetSimilarity(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> hashing1, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> hashing2, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var similarity = 0;

      foreach (var kvp1 in hashing1 ?? throw new System.ArgumentNullException(nameof(hashing2)))
        foreach (var kvp2 in hashing2 ?? throw new System.ArgumentNullException(nameof(hashing2)))
          if (comparer.Equals(kvp1.Key, kvp2.Key))
          {
            switch (ScoringBehavior)
            {
              case SimilarityScoringBehavior.OnlyOneFrequencyWhenEqual when kvp1.Value == kvp2.Value:
                similarity += kvp1.Value;
                break;
              case SimilarityScoringBehavior.SumOfBothFrequencies:
                similarity += kvp1.Value + kvp2.Value;
                break;
            }
          }

      return similarity;
    }
    /// <summary>The MostFreqKDistance is a string metric technique for quickly estimating how similar two ordered sets or strings are, using the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Most_frequent_k_characters#Most_frequent_K_distance"/> 
    public int GetSimilarity(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> hashing1, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> hashing2)
      => GetSimilarity(hashing1, hashing2, System.Collections.Generic.EqualityComparer<T>.Default);

    #region IMeasuredDistance
    /// <summary>The MostFreqKDistance is a string metric technique for quickly estimating how similar two ordered sets or strings are, according to the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Most_frequent_k_characters#String_distance_wrapper_function"/> 
    public int GetMeasuredDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => MaxDistance - GetSimilarity(Hashing(source.ToArray(), comparer), Hashing(target.ToArray(), comparer), comparer);
    /// <summary>The MostFreqKDistance is a string metric technique for quickly estimating how similar two ordered sets or strings are, according to the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Most_frequent_k_characters#String_distance_wrapper_function"/> 
    public int GetMeasuredDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetMeasuredDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
    #endregion IMeasuredDistance
  }
}
