namespace Flux.SequenceMetrics
{
  /// <summary>Represents some arbitrary measure of length between two sequences. Some cannot be used to derive direct metrics. E.g. the longest common substring, since there can be many between two strings.</summary>
	public interface IMeasuredLength<T>
  {
    /// <summary>Compute a length measure for the two specified sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <param name="comparer">The equality comparer to use when comparing elements in the sequences.</param>
    /// <returns>A measure that represents a length of some relation between the two sequences.</returns>
		int GetMeasuredLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer);
    /// <summary>Compute a length measure for the two specified sequences. Uses the default equality comparer.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <returns>A measure that represents a length of some relation between the two sequences.</returns>
    int GetMeasuredLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
