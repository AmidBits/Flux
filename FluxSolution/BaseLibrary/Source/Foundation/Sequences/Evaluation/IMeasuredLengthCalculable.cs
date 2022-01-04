namespace Flux.Metrical
{
  /// <summary>Represents some arbitrary measure of length of a sequence. Some cannot be used to derive direct metrics. E.g. the longest common substring, since there can be many between two strings.</summary>
	public interface IMeasuredLengthCalculable<T>
  {
    /// <summary>Compute a length measure of the specified sequence.</summary>
    /// <param name="source">The source sequence.</param>
    /// <returns>A measure that represents a length of some relation between the two sequences.</returns>
		int GetMeasuredLength(System.ReadOnlySpan<T> source);
  }
}
