using System.Linq;

namespace Flux
{
  public static partial class XtendSequenceMetrics
  {
    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.S�rensenDiceIndex<T>().GetCoefficient(source, target, comparer);
    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new SequenceMetrics.S�rensenDiceIndex<T>().GetCoefficient(source, target);
  }

  namespace SequenceMetrics
  {
    public class S�rensenDiceIndex<T>
    {
      /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
      public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
        => 2.0 * source.ToArray().Intersect(target.ToArray(), comparer).Count() / (source.Length + target.Length);
      /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
      public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetCoefficient(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
    }
  }
}
