namespace Flux.Sequence.Metrics
{
  /// <summary>Represents some arbitrary measure of distance between two sequences.</summary>
	public interface ISimilarityCoefficient<T>
  {
		/// <summary>Compute a coefficient representing the similarity of the two sequences.</summary>
		/// <param name="source">The source sequence.</param>
		/// <param name="target">The target sequence.</param>
		double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
	}
}
