namespace Flux.Numerics.Permutations
{
  public sealed class HeapsAlgorithm<T>
  {
    private readonly System.Collections.Generic.IEnumerator<T[]> m_enumerator;

    public HeapsAlgorithm(T[] source) => m_enumerator = source.PermuteHeapsAlgorithm().GetEnumerator();

    public HeapsAlgorithm<T> GetEnumerator() => this;

    /// <summary>
    /// <para>Gets the current permutation.</para>
    /// </summary>
    public T[] Current => m_enumerator.Current;

    /// <summary>
    /// <para>Advances to the next permutation.</para>
    /// </summary>
    /// <returns>Whether successfully advanced to the next permutation.</returns>
    public bool MoveNext() => m_enumerator.MoveNext();
  }
}
