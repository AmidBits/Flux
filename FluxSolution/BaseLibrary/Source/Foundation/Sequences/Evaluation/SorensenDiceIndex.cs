using System.Linq;

namespace Flux.Metrical
{
  public class SørensenDiceIndex<T>
    : AMetrical<T>, ISimilarityCoefficient<T>
  {
    public SørensenDiceIndex()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    { }
    public SørensenDiceIndex(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      : base(equalityComparer)
    { }

    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public double GetSimilarityCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => 2.0 * source.ToArray().Intersect(target.ToArray(), EqualityComparer).Count() / (source.Length + target.Length);
  }
}
