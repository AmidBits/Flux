namespace Flux.Metrical
{
  /// <summary>Represents an arbitrary length measure of equality between two sets. Some cannot be used to derive direct metrics. E.g. the longest common substring, since there can be many between two strings.</summary>
	public interface ILengthMeasureEquatable<T>
  {
    /// <summary>A length measure between two sets.</summary>
    /// <param name="source">The source set.</param>
    /// <param name="target">The target set.</param>
		int GetLengthMeasure(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
