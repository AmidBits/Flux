namespace Flux.Metrical
{
  /// <summary>Represents an arbitrary length measure of equality between two sets. Some cannot be used to derive direct metrics. E.g. the longest common substring, since there can be many between two strings.</summary>
	public interface ILengthMeasureEvaluable<T>
  {
    /// <summary>A length measure within a set.</summary>
    /// <param name="source">The source set.</param>
		int GetLengthMeasure(System.ReadOnlySpan<T> source);
  }
}
