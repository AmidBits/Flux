namespace Flux.SequenceMetrics
{
  /// <summary>Represents a metric of length. Some cannot be used to derive direct metrics. E.g. the longest common substring, since there can be many between two strings.</summary>
	public interface IMetricLength<T>
  {
    /// <summary>Compute a length metric for the two specified sequences.</summary>
    /// <param name="source">The primary sequence.</param>
    /// <param name="target">The secondary sequence.</param>
    /// <returns>A metric that represents a length in relation to the two specified sequences.</returns>
		int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer);
    int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
    //=> GetMetricLength(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
