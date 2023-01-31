//namespace Flux
//{
//  /// <summary>
//  /// <para>Represents the simple matching coefficient (SMC) used for comparing the similarity and diversity of sample sets. The simple matching distance (SMD) can also be derived from this (SMD = 1 - SMC).</para>
//  /// <see href="https://en.wikipedia.org/wiki/Simple_matching_coefficient"/>
//  /// </summary>
//  public interface ISimpleMatchingCoefficient<T>
//  {
//    /// <summary>Compute a simple matching coefficient for the two specified sequences.</summary>
//    /// <param name="source">The source sequence.</param>
//    /// <param name="target">The target sequence.</param>
//    /// <remarks>The SMC can be derived from (1 - <see cref="ISimpleMatchingDistance{T}.GetSimpleMatchingDistance(ReadOnlySpan{T}, ReadOnlySpan{T})"/>).</remarks>
//    /// <returns>A metric representing a SMC in relation to the two sequences. The simple matching distance (SMD) can be derived by (1 - SMC).</returns>
//    double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);

//    double GetDerivedSMD(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target) => 1d - GetSimpleMatchingCoefficient(source, target);
//  }
//}
