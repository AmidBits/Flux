using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public static double SørensenDiceCoefficient<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new IndexedMetrics.SørensenDiceIndex<T>(comparer).GetCoefficient((T[])source, (T[])target);
    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public static double SørensenDiceCoefficient<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => new IndexedMetrics.SørensenDiceIndex<T>().GetCoefficient((T[])source, (T[])target);

    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public static double SørensenDiceIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new IndexedMetrics.SørensenDiceIndex<T>(comparer).GetCoefficient(source, target);
    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public static double SørensenDiceIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new IndexedMetrics.SørensenDiceIndex<T>().GetCoefficient(source, target);
  }

  namespace IndexedMetrics
  {
    public class SørensenDiceIndex<T>
      : AIndexedMetrics<T>
    {
      public SørensenDiceIndex()
        : this(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }
      public SørensenDiceIndex(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        : base(equalityComparer)
      {
      }

      /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
      public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => 2.0 * source.ToArray().Intersect(target.ToArray(), EqualityComparer).Count() / (source.Length + target.Length);
    }
  }
}
