namespace Flux.Metrical
{
  /// <summary>Represents a length metric of equality between two sets.</summary>
	public interface ILengthMetricEquatable<T>
  {
    /// <summary>A length metric between two sets.</summary>
    /// <param name="source">The source set.</param>
    /// <param name="target">The target set.</param>
		int GetLengthMetric(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
