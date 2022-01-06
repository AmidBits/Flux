namespace Flux.Metrical
{
	/// <summary>The edit distance is a way of quantifying how dissimilar two sequences (e.g., words) are to one another by counting the minimum number of operations required to transform one sequence into the other.</summary>
	/// <see cref="https://en.wikipedia.org/wiki/Edit_distance"/>
	/// <remarks>Can be derived from (a.Length + b.Length - 2 * IMetricLength).</remarks>
	public interface IEditDistanceCustomizable<T>
	{
		/// <summary>Meter the custom edit distance between two sets. The edit distance is the minimum-weight series of edit operations that transforms one set into the other.</summary>
		/// <param name="source">The source set.</param>
		/// <param name="target">The target set.</param>
		/// <returns>A metric that represents the custom edit distance of equality between two sets.</returns>
		/// <see cref="https://en.wikipedia.org/wiki/Edit_distance"/>
		double GetCustomEditDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
	}
}
