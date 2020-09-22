namespace Flux.SequenceMetrics
{
  /// <summary>The edit distance is a way of quantifying how dissimilar two sequences (e.g., words) are to one another by counting the minimum number of operations required to transform one sequence into the other.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Edit_distance"/>
  /// <remarks>Can be derived from (a.Length + b.Length - 2 * IMetricLength).</remarks>
  public interface IMetricDistance<T>
  {
    /// <summary>Calculate the edit distance between the two specified sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <returns>The edit distance, i.e. the minimum-weight series of edit operations that transforms one sequence into the other.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Edit_distance"/>
    int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
