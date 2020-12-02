using System.Linq;

namespace Flux
{
  public static partial class SpanMetricsEm
  {
    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceCoefficient<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SpanMetrics.S�rensenDiceIndex<T>(comparer).GetCoefficient((T[])source, (T[])target);
    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceCoefficient<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => new SpanMetrics.S�rensenDiceIndex<T>().GetCoefficient((T[])source, (T[])target);

    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SpanMetrics.S�rensenDiceIndex<T>(comparer).GetCoefficient(source, target);
    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new SpanMetrics.S�rensenDiceIndex<T>().GetCoefficient(source, target);
  }

  namespace SpanMetrics
  {
    public class S�rensenDiceIndex<T>
      : ASpanMetrics<T>
    {
      public S�rensenDiceIndex()
        : this(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }
      public S�rensenDiceIndex(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        : base(equalityComparer)
      {
      }

      /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
      public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => 2.0 * source.ToArray().Intersect(target.ToArray(), EqualityComparer).Count() / (source.Length + target.Length);
    }
  }
}
