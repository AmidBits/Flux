namespace Flux.Metrical
{
  /// <summary>Represents a normalized [0, 1] edit distance between two sets. It's not a metric in the mathematical sense of that term because it does not obey the triangle inequality</summary>
  /// <returns>The score is normalized [0, 1] such that 0 means an exact match and 1 means there is no similarity. The similarity score is the inversion, (1 - distance).</returns>
  public interface IEditDistanceNormalizable<T>
  {
    /// <summary>Compute a normalized edit distance [0, 1] between two sets. The edit distance is the minimum-weight series of edit operations that transforms one set into the other.</summary>
    /// <param name="source">The source set.</param>
    /// <param name="target">The target set.</param>
    /// <returns>A normalized edit distance [0, 1] between two sets. Closer to 0 (less edits needed) means more alike and closer to 1 (more edits needed) means more unlike.</returns>
		double GetNormalizedEditDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
