namespace Flux.SequenceMetrics
{
  /// <summary>Represents a measure of length. Some cannot be used to derive direct metrics. E.g. the longest common substring, since there can be many between two strings.</summary>
	public interface IMeasuredLength<T>
  {
    /// <summary>Compute a length measure for the two specified sequences.</summary>
    /// <param name="source">The primary sequence.</param>
    /// <param name="target">The secondary sequence.</param>
    /// <returns>A measure that represents a length in relation to the two specified sequences.</returns>
		int GetMeasuredLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer);
    int GetMeasuredLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
    //=> GetMeasuredLength(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
