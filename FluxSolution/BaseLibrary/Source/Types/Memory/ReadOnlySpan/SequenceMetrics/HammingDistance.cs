namespace Flux
{
  public static partial class XtendSequenceMetrics
  {
    /// <summary>The Hamming distance between two sequences of equal length is the number of positions at which the corresponding symbols are different. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hamming_distance"/>
    /// <returns>The minimum number of substitutions required to change the source to target, or the minimum number of errors that could have transformed source to target.</returns>
    public static int HammingDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.HammingDistance<T>().GetMetricDistance(source, target, comparer);
    /// <summary>The Hamming distance between two sequences of equal length is the number of positions at which the corresponding symbols are different. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hamming_distance"/>
    /// <returns>The minimum number of substitutions required to change the source to target, or the minimum number of errors that could have transformed source to target.</returns>
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
      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      {
        comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        if (source.Length != target.Length) throw new System.ArgumentException($"The source length ({source.Length}) and the target length ({target.Length}) must be equal.");

        var equalCount = 0;

        for (var index = source.Length - 1; index >= 0; index--)
          if (!comparer.Equals(source[index], target[index]))
            equalCount++;

        return equalCount;
      }
      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetMetricDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
        => 1.0 - GetSimpleMatchingDistance(source, target, comparer);
      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetSimpleMatchingCoefficient(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
        => (double)GetMetricDistance(source, target, comparer) / (double)System.Math.Max(source.Length, target.Length);
      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetSimpleMatchingDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
    }
  }
}