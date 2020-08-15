using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceCoefficient<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.S�rensenDiceIndex<T>().GetCoefficient(source.ToArray(), target.ToArray(), comparer);
    /// <summary>The S�rensen�Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/S�rensen�Dice_coefficient"/>
    public static double S�rensenDiceCoefficient<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.S�rensenDiceIndex<T>().GetCoefficient(source.ToArray(), target.ToArray());
  }
}
