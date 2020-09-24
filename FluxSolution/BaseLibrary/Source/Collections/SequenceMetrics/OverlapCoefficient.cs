using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    public static double OverlapCoefficient<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.OverlapCoefficient<T>(comparer).GetCoefficient(source.ToArray(), target.ToArray());
    /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    public static double OverlapCoefficient<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? target)
      => new SequenceMetrics.OverlapCoefficient<T>().GetCoefficient(source.ToArray(), target.ToArray());

    /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    public static double OverlapCoefficient<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.OverlapCoefficient<T>(comparer).GetCoefficient(source, target);
    /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    public static double OverlapCoefficient<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new SequenceMetrics.OverlapCoefficient<T>().GetCoefficient(source, target);
  }

  namespace SequenceMetrics
  {
    public class OverlapCoefficient<T>
    {
      private System.Collections.Generic.IEqualityComparer<T> m_equalityComparer;

      public OverlapCoefficient(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        => m_equalityComparer = equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default;
      public OverlapCoefficient()
        : this(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }

      /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
      public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => (double)source.ToArray().Intersect(target.ToArray(), m_equalityComparer).Count() / (double)System.Math.Min(source.Length, target.Length);
    }
  }
}
