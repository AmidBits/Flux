namespace Flux.SequenceMetrics
{
  /// <summary>Compute and return a measure of distance/similarity between two sequences. It's not a metric in the mathematical sense of that term because it does not obey the triangle inequality</summary>
  /// <returns>Similarity (0 means both strings are completely different)</returns>
  /// <see cref=""/>
  public interface INormalizedDistance<T>
  {
    /// <summary>Compute a normalized distance score for the two specified sequences.</summary>
    /// <param name="source">The primary sequence.</param>
    /// <param name="target">The secondary sequence.</param>
    /// <returns>A normalized distance score (0.0 - 1.0).</returns>
		double GetNormalizedDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer);
    double GetNormalizedDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
    //=> GetNormalizedDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
