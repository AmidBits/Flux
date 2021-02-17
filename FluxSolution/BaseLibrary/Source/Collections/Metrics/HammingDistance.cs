using System.Linq;

namespace Flux
{
  namespace SpanMetrics
  {
    /// <summary>The Hamming distance between two sequences of equal length is the number of positions at which the corresponding symbols are different.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hamming_distance"/>
    /// <returns>The minimum number of substitutions required to change the source to target, or the minimum number of errors that could have transformed source to target.</returns>
    public class HammingDistance<T>
      : ASpanMetrics<T>, IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
    {
      public HammingDistance()
        : base(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }
      public HammingDistance(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        : base(equalityComparer)
      {
      }

      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        if (source.Length != target.Length) throw new System.ArgumentException($"The source length ({source.Length}) and the target length ({target.Length}) must be equal.");

        var equalCount = 0;

        for (var index = source.Length - 1; index >= 0; index--)
          if (!EqualityComparer.Equals(source[index], target[index]))
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