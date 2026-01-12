namespace Flux
{
  /// <summary>
  /// <para>Compares two sequences, using reference equality, null inequality, length and whether their corresponding elements compare equal according to the specified <see cref="System.Collections.Generic.IEqualityComparer{T}"/>.</para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public sealed class SequenceEqualityComparer<T>
    : System.Collections.Generic.IEqualityComparer<System.Collections.Generic.IEnumerable<T>>
  {
    /// <summary>
    /// <para>An instance of the <see cref="SequenceEqualityComparer{T}"/> with default parameters.</para>
    /// <para>Compares two sequences, using reference equality, null inequality, length and whether their corresponding elements compare equal according to <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/>.</para>
    /// </summary>
    public static System.Collections.Generic.IEqualityComparer<System.Collections.Generic.IEnumerable<T>> Default { get; } = new SequenceEqualityComparer<T>(System.Collections.Generic.EqualityComparer<T>.Default);

    private readonly System.Collections.Generic.IEqualityComparer<T> m_equalityComparer;

    public SequenceEqualityComparer(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => m_equalityComparer = equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default;

    public bool Equals(System.Collections.Generic.IEnumerable<T>? x, System.Collections.Generic.IEnumerable<T>? y)
      => object.ReferenceEquals(x, y) || (x is not null && y is not null && x.SequenceEqual(y, m_equalityComparer));

    public int GetHashCode(System.Collections.Generic.IEnumerable<T> obj)
      => obj?.SequenceHashCode() ?? 0;
  }
}
