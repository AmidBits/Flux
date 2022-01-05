namespace Flux.Metrical
{
  /// <summary>Represents a measure of similarity between two sets.</summary>
	public interface IMeasuredSimilarityEquatable<T>
  {
		/// <summary>Measure a similarity of two sets.</summary>
		/// <param name="source">The source set.</param>
		/// <param name="target">The target set.</param>
		/// <returns>A measure that represents a similarity of equality between two sets.</returns>
		double GetMeasuredSimilarity(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
	}
}
