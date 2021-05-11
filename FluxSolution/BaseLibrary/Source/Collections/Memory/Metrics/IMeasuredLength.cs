namespace Flux.Memory.Metrics
{
  /// <summary>Represents some arbitrary measure of length between two sequences. Some cannot be used to derive direct metrics. E.g. the longest common substring, since there can be many between two strings.</summary>
	public interface IMeasuredLength<T>
  {
    /// <summary>Compute a length measure for the two specified sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <returns>A measure that represents a length of some relation between the two sequences.</returns>
		int GetMeasuredLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
