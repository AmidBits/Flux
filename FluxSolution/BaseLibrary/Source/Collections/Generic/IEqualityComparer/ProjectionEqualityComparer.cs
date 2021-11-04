namespace Flux.DataStructures.Generic
{
  public static class ProjectionEqualityComparer
  {
    public static ProjectionEqualityComparer<TSource, TKey> Create<TSource, TKey>(System.Func<TSource, TKey> projection)
      where TKey : notnull
      => new(projection);

    //public static ProjectionEqualityComparer<TSource, TKey> Create<TSource, TKey>(TSource ignored, System.Func<TSource, TKey> projection)
    //  where TKey : notnull
    //  => new ProjectionEqualityComparer<TSource, TKey>(projection);
  }

  public static class ProjectionEqualityComparer<TSource>
  {
    public static ProjectionEqualityComparer<TSource, TKey> Create<TKey>(System.Func<TSource, TKey> projection)
      where TKey : notnull
      => new(projection);
  }

  public class ProjectionEqualityComparer<TSource, TKey>
    : System.Collections.Generic.IEqualityComparer<TSource>
    where TKey : notnull
  {
    private readonly System.Collections.Generic.IEqualityComparer<TKey> m_equalityComparer;
    private readonly System.Func<TSource, TKey> m_projection;

    public ProjectionEqualityComparer(System.Func<TSource, TKey> projection, System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));
      if (projection is null) throw new System.ArgumentNullException(nameof(projection));

      m_equalityComparer = equalityComparer;
      m_projection = projection;
    }
    public ProjectionEqualityComparer(System.Func<TSource, TKey> projection)
        : this(projection, System.Collections.Generic.EqualityComparer<TKey>.Default)
    { }

    public bool Equals(TSource? x, TSource? y)
      => x is not null && y is not null && ((x is null && y is null) || m_equalityComparer.Equals(m_projection(x), m_projection(y)));
    public int GetHashCode(TSource obj)
      => obj is null ? throw new System.ArgumentNullException(nameof(obj)) : m_equalityComparer.GetHashCode(m_projection(obj));
  }
}
