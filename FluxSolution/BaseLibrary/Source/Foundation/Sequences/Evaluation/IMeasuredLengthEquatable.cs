namespace Flux.Metrical
{
  /// <summary>Represents an arbitrary length measure of equality between two sets. Some cannot be used to derive direct metrics. E.g. the longest common substring, since there can be many between two strings.</summary>
	public interface IMeasuredLengthEquatable<T>
  {
    /// <summary>Measure a length of two sets.</summary>
    /// <param name="source">The source set.</param>
    /// <param name="target">The target set.</param>
    /// <returns>A measure that represents a length of equality between two sets.</returns>
		int GetMeasuredLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
