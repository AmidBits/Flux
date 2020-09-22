using System.Linq;

namespace Flux
{
  public static partial class XtendSequenceMetrics
  {
    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceCoefficient<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.S�rensenDiceIndex<T>(comparer).GetCoefficient(source.ToArray(), target.ToArray());
    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceCoefficient<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.S�rensenDiceIndex<T>().GetCoefficient(source.ToArray(), target.ToArray());

    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.S�rensenDiceIndex<T>(comparer).GetCoefficient(source, target);
    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new SequenceMetrics.S�rensenDiceIndex<T>().GetCoefficient(source, target);
  }

  namespace SequenceMetrics
  {
    public class S�rensenDiceIndex<T>
    {
      private System.Collections.Generic.IEqualityComparer<T> m_equalityComparer;

      public S�rensenDiceIndex(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        => m_equalityComparer = equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default;
      public S�rensenDiceIndex()
        : this(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }

      /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
      public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => 2.0 * source.ToArray().Intersect(target.ToArray(), m_equalityComparer).Count() / (source.Length + target.Length);
    }
  }
}
