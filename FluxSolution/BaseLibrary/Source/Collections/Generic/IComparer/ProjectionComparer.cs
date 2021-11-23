namespace Flux.DataStructures.Generic
{
  public static class ProjectionComparer
  {
    public static ProjectionComparer<TSource, TKey> Create<TSource, TKey>(System.Func<TSource, TKey> projection)
      => new(projection);

    //public static ProjectionComparer<TSource, TKey> Create<TSource, TKey>(TSource ignored, System.Func<TSource, TKey> projection)
    //  => new ProjectionComparer<TSource, TKey>(projection);
  }

  public static class ProjectionComparer<TSource>
  {
    public static ProjectionComparer<TSource, TKey> Create<TKey>(System.Func<TSource, TKey> projection)
      => new(projection);
  }

  public sealed class ProjectionComparer<TSource, TKey>
    : System.Collections.Generic.IComparer<TSource>
  {
    private readonly System.Collections.Generic.IComparer<TKey> m_comparer;
    private readonly System.Func<TSource, TKey> m_projection;

    public ProjectionComparer(System.Func<TSource, TKey> projection, System.Collections.Generic.IComparer<TKey> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));
      if (projection is null) throw new System.ArgumentNullException(nameof(projection));

      m_comparer = comparer;
      m_projection = projection;
    }
    public ProjectionComparer(System.Func<TSource, TKey> projection)
      : this(projection, System.Collections.Generic.Comparer<TKey>.Default)
    { }

    public int Compare(TSource? x, TSource? y)
      => (x is null && y is null) ? 0 : x is null ? -1 : y is null ? 1 : m_comparer.Compare(m_projection(x), m_projection(y));
  }
}
