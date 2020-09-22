using System.Linq;

namespace Flux
{
  public static partial class XtendSequenceMetrics
  {
    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public static double SørensenDiceCoefficient<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.SørensenDiceIndex<T>(comparer).GetCoefficient(source.ToArray(), target.ToArray());
    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public static double SørensenDiceCoefficient<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.SørensenDiceIndex<T>().GetCoefficient(source.ToArray(), target.ToArray());

    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public static double SørensenDiceIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.SørensenDiceIndex<T>(comparer).GetCoefficient(source, target);
    /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
    public static double SørensenDiceIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new SequenceMetrics.SørensenDiceIndex<T>().GetCoefficient(source, target);
  }

  namespace SequenceMetrics
  {
    public class SørensenDiceIndex<T>
    {
      private System.Collections.Generic.IEqualityComparer<T> m_equalityComparer;

      public SørensenDiceIndex(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        => m_equalityComparer = equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default;
      public SørensenDiceIndex()
        : this(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }

      /// <summary>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
      public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => 2.0 * source.ToArray().Intersect(target.ToArray(), m_equalityComparer).Count() / (source.Length + target.Length);
    }
  }
}
