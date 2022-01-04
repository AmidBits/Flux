using System.Linq;

namespace Flux.Metrical
{
  /// <summary>The MostFreqKDistance is a string metric technique for quickly estimating how similar two ordered sets or strings are.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Most_frequent_k_characters#Most_frequent_K_hashing"/> 
  public sealed class MostFreqK<T>
    : IMeasuredDistanceEquatable<T>
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

    /// <summary>Specifies the scoring behavior to employ when computing the measured distance.</summary>
    public SimilarityScoringBehavior ScoringBehavior { get; set; } = SimilarityScoringBehavior.OnlyOneFrequencyWhenEqual;

    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public MostFreqK(int k, int maxDistance, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      K = k;
      MaxDistance = maxDistance;
      EqualityComparer = equalityComparer;
    }
    public MostFreqK(int k, int maxDistance)
      : this(k, maxDistance, System.Collections.Generic.EqualityComparer<T>.Default)
    { }

    public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> Hashing(System.Collections.Generic.IEnumerable<T> source)
    {
      foreach (var kvp in source.GroupBy(c => c, EqualityComparer).Select(g => new System.Collections.Generic.KeyValuePair<T, int>(g.Key, g.Count())).OrderByDescending(g => g.Value).Take(K))
      {
        yield return kvp;
      }
    }

    public int GetSimilarity(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> hashing1, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> hashing2)
    {
      var similarity = 0;

      foreach (var kvp1 in hashing1 ?? throw new System.ArgumentNullException(nameof(hashing2)))
        foreach (var kvp2 in hashing2 ?? throw new System.ArgumentNullException(nameof(hashing2)))
          if (EqualityComparer.Equals(kvp1.Key, kvp2.Key))
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

    public int GetMeasuredDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => MaxDistance - GetSimilarity(Hashing(source.ToArray()), Hashing(target.ToArray()));
  }
}
