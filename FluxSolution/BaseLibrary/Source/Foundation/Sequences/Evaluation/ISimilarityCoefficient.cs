namespace Flux.Metrical
{
  /// <summary>Represents some arbitrary measure of distance between two sequences.</summary>
	public interface ISimilarityCoefficient<T>
  {
		/// <summary>Compute a coefficient representing the similarity of the two sequences.</summary>
		/// <param name="source">The source sequence.</param>
		/// <param name="target">The target sequence.</param>
		double GetSimilarityCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
	}
}