namespace Flux.Metrical
{
  /// <summary>Represents the simple matching coefficient (SMC) used for comparing the similarity and diversity of sample sets. The simple matching distance (SMD) can also be derived from this (SMD = 1 - SMC).</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
  public interface ISimpleMatchingCoefficientEquatable<T>
  {
    /// <summary>Compute a simple matching coefficient for the two specified sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <returns>A metric representing a SMC in relation to the two sequences. The simple matching distance (SMD) can be derived by (1 - SMC).</returns>
    /// <remarks>The SMD can be derived from (1 - ISimpleMatchingCoefficient). The SMD can be derived from (IMetricDistance / Max(a, b)).</remarks>
    double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
