using System.Linq;

namespace Flux.SequenceMetrics
{
  public class SørensenDiceIndex<T>
  {
    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => 2.0 * source.ToArray().Intersect(target.ToArray(), comparer).Count() / (source.Length + target.Length);
    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetCoefficient(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
