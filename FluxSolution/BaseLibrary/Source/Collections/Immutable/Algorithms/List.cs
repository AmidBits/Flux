namespace Flux.Collections.Immutable
{
  public sealed class List<T>
    : IList<T>
    where T : System.IComparable<T>
  {
    public static readonly IList<T> Empty = new EmptyList();

    private readonly T m_value;
    private readonly IList<T> m_tail;

    private List(T value, IList<T> tail)
    {
      m_value = value;

      m_tail = tail;
    }

    #region IList implementation
    public bool IsEmpty
      => false;
    public T Value
      => m_value;
    public IList<T> Add(T value)
      => new List<T>(m_value, m_tail.Add(value));
    public bool Contains(T value)
      => m_value.Equals(value) || m_tail.Contains(value);
    public IList<T> Remove(T value)
      => value.Equals(m_value) ? m_tail : m_tail.Remove(value);
    #endregion IList implementation

    private System.Collections.Generic.IEnumerator<T> Enumerate()
    {
      var node = (List<T>)this;

      yield return node.m_value;

      while (!node.m_tail.IsEmpty)
      {
        yield return node.m_value;

        node = (List<T>)node.m_tail;
      }
    }

    #region IEnumerable
    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
      => Enumerate();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion IEnumerable

    private sealed class EmptyList
      : IList<T>
    {
      #region IList implementation
      public bool IsEmpty
        => true;
      public T Value
        => throw new System.Exception(nameof(EmptyList));
      public IList<T> Add(T value)
        => new List<T>(value, this);
      public bool Contains(T value)
        => throw new System.Exception(nameof(EmptyList));
      public IList<T> Remove(T value)
        => throw new System.Exception(nameof(EmptyList));
      #endregion IList implementation

      #region IEnumerable
      public System.Collections.Generic.IEnumerator<T> GetEnumerator()
      { yield break; }
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
      #endregion IEnumerable
    }
  }
}
