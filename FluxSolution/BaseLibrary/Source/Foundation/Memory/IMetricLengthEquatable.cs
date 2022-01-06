namespace Flux.Metrical
{
  /// <summary>Represents a length metric of equality between two sets.</summary>
	public interface IMetricLengthEquatable<T>
  {
    /// <summary>Meter a length of two sets.</summary>
    /// <param name="source">The source set.</param>
    /// <param name="target">The target set.</param>
    /// <returns>A metric that represents a length of equality between two sets.</returns>
		int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
