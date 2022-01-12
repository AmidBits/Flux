namespace Flux.Metrical
{
  /// <summary>Represents the simple matching distance (SMD) which measures dissimilarity and diversity of sample sets. The simple matching coefficient (SMC) can also be derived from this (SMC = 1 - SMD).</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
  public interface ISimpleMatchingDistance<T>
  {
    /// <summary>Compute a simple matching distance for the two specified sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <returns>A metric representing a SMD in relation to the two sequences. The simple matching coefficient (SMC) can be derived by (1 - SMD).</returns>
    /// <remarks>The SMC can be derived from (1 - ISimpleMatchingDistance). The SMC can be derived from (IMetricDistance / Max(a, b)).</remarks>
    double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
