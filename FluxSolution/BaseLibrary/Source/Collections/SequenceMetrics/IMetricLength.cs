namespace Flux.SequenceMetrics
{
  /// <summary>Represents a metric of length.</summary>
	public interface IMetricLength<T>
  {
    /// <summary>Compute a length metric for the two sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <returns>A metric that represents a length of some relation between the two sequences.</returns>
		int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
