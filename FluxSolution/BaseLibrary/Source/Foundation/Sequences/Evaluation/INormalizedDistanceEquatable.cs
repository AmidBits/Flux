namespace Flux.Metrical
{
  /// <summary>Represents a normalized distance [0, 1] between two sequences. It's not a metric in the mathematical sense of that term because it does not obey the triangle inequality</summary>
  /// <returns>The score is normalized such that 0 means an exact match and 1 means there is no similarity. The similarity score is the inversion, (1 - distance).</returns>
  public interface INormalizedDistanceEquatable<T>
  {
    /// <summary>Compute a normalized distance score [0, 1] between the two sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <returns>A normalized distance score [0, 1]. Closer to 0 means more alike and closer to 1 means more unlike.</returns>
		double GetNormalizedDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
