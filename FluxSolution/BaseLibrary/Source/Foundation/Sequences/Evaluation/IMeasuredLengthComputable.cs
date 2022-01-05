namespace Flux.Metrical
{
  /// <summary>Represents some arbitrary measure of length of a sequence. Some cannot be used to derive direct metrics. E.g. the longest common substring, since there can be many between two strings.</summary>
	public interface IMeasuredLengthComputable<T>
  {
    /// <summary>Measure a length of a sequence.</summary>
    /// <param name="source">The sequence to measure.</param>
    /// <returns>A measure that represents a computational length of a sequence.</returns>
		int GetMeasuredLength(System.ReadOnlySpan<T> source);
  }
}
