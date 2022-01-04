using System.Linq;

namespace Flux.Metrical
{
  /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
  public sealed class SørensenDiceIndex<T>
    : IMeasuredSimilarityEquatable<T>
  {
    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public SørensenDiceIndex(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
    public SørensenDiceIndex()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    { }

    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    public double GetSimilarityCoefficient(T[] source, T[] target)
      => 2.0 * source.Intersect(target, EqualityComparer).Count() / (source.Length + target.Length);
    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    public double GetMeasuredSimilarity(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetSimilarityCoefficient(source.ToArray(), target.ToArray());
  }
}
