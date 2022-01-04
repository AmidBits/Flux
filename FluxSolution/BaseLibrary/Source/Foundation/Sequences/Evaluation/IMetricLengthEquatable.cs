namespace Flux.Metrical
{
  /// <summary>Represents a metric of length.</summary>
	public interface IMetricLengthEquatable<T>
  {
    /// <summary>Compute a length metric for the two sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <returns>A metric that represents a length of some relation between the two sequences.</returns>
		int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
