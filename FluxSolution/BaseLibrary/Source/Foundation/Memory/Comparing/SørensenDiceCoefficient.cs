namespace Flux.Metrical
{
  /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
  public sealed class SørensenDiceCoefficient<T>
  {
    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public SørensenDiceCoefficient(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
    public SørensenDiceCoefficient()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    { }

    public double MeasureSimilarity(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetSimilarityCoefficient(source.ToArray(), target.ToArray(), EqualityComparer);

    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    public static double GetSimilarityCoefficient(T[] source, T[] target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => 2.0 * System.Linq.Enumerable.Count(System.Linq.Enumerable.Intersect(source, target, equalityComparer)) / (source.Length + target.Length);
  }
}
