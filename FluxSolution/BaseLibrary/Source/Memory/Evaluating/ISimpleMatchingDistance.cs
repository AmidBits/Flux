namespace Flux
{
  /// <summary>
  /// <para>Represents the simple matching distance (SMD) which measures dissimilarity and diversity of sample sets. The simple matching coefficient (SMC) can also be derived from this (SMC = 1 - SMD).</para>
  /// <see href="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
  /// </summary>
  public interface ISimpleMatchingDistance<T>
  {
    /// <summary>Compute a simple matching distance for the two specified sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <remarks>
    /// <para>The SMD can be derived from (1 - <see cref="ISimpleMatchingCoefficient{T}.GetSimpleMatchingCoefficient(ReadOnlySpan{T}, ReadOnlySpan{T})"/>).</para>
    /// <para>The SMD can also be derived from (<see cref="IEditDistanceEquatable{T}.GetEditDistance(ReadOnlySpan{T}, ReadOnlySpan{T})"/> / Max(a, b)).</para>
    /// </remarks>
    /// <returns>A metric representing a SMD in relation to the two sequences. The simple matching coefficient (SMC) can be derived by (1 - SMD).</returns>
    double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);

    double GetDerivedSMC(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target) => 1d - GetSimpleMatchingDistance(source, target);
  }
}
