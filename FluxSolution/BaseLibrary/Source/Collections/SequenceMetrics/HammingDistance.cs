using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>The Hamming distance between two sequences of equal length is the number of positions at which the corresponding symbols are different.</summary>
    public static int HammingDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.HammingDistance<T>(comparer).GetMetricDistance(source.ToArray(), target.ToArray());
    /// <summary>The Hamming distance between two sequences of equal length is the number of positions at which the corresponding symbols are different.</summary>
    public static int HammingDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.HammingDistance<T>().GetMetricDistance(source.ToArray(), target.ToArray());

    /// <summary>The Hamming distance between two sequences of equal length is the number of positions at which the corresponding symbols are different.</summary>
    public static int HammingDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.HammingDistance<T>(comparer).GetMetricDistance(source, target);
    /// <summary>The Hamming distance between two sequences of equal length is the number of positions at which the corresponding symbols are different.</summary>
    public static int HammingDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new SequenceMetrics.HammingDistance<T>().GetMetricDistance(source, target);
  }

  namespace SequenceMetrics
  {
    /// <summary>The Hamming distance between two sequences of equal length is the number of positions at which the corresponding symbols are different.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hamming_distance"/>
    /// <returns>The minimum number of substitutions required to change the source to target, or the minimum number of errors that could have transformed source to target.</returns>
    public class HammingDistance<T>
      : IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
    {
      private System.Collections.Generic.IEqualityComparer<T> m_equalityComparer;

      public HammingDistance(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        => m_equalityComparer = equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default;
      public HammingDistance()
        : this(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }

      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        if (source.Length != target.Length) throw new System.ArgumentException($"The source length ({source.Length}) and the target length ({target.Length}) must be equal.");

        var equalCount = 0;

        for (var index = source.Length - 1; index >= 0; index--)
          if (!m_equalityComparer.Equals(source[index], target[index]))
            equalCount++;

        return equalCount;
      }

      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => 1.0 - GetSimpleMatchingDistance(source, target);

      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => (double)GetMetricDistance(source, target) / (double)System.Math.Max(source.Length, target.Length);
    }
  }
}