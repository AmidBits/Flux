namespace Flux.Metrical
{
  /// <summary>Represents a measure of similarity between two sets.</summary>
	public interface IMeasuredSimilarityEquatable<T>
  {
		/// <summary>Compute a measure of similarity between the two sets.</summary>
		/// <param name="source">The source sequence.</param>
		/// <param name="target">The target sequence.</param>
		double GetMeasuredSimilarity(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
	}
}
