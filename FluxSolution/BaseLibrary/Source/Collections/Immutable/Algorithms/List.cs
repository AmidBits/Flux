namespace Flux.Collections.Immutable
{
  public sealed class List<TValue>
    : IList<TValue>
    where TValue : System.IComparable<TValue>
  {
    public static readonly IList<TValue> Empty = new EmptyList();

    private readonly TValue m_value;
    private readonly IList<TValue> m_tail;

    private List(TValue value, IList<TValue> tail)
    {
      m_value = value;

      m_tail = tail;
    }

    #region IList implementation
    public bool IsEmpty
      => false;
    public TValue Value
      => m_value;
    public IList<TValue> Add(TValue value)
      => new List<TValue>(m_value, m_tail.Add(value));
    public bool Contains(TValue value)
      => m_value.Equals(value) || m_tail.Contains(value);
    public IList<TValue> Remove(TValue value)
      => value.Equals(m_value) ? m_tail : m_tail.Remove(value);
    #endregion IList implementation

    private System.Collections.Generic.IEnumerator<TValue> Enumerate()
    {
      var node = (List<TValue>)this;

      yield return node.m_value;

      while (!node.m_tail.IsEmpty)
      {
        yield return node.m_value;

        node = (List<TValue>)node.m_tail;
      }
    }

    #region IEnumerable
    public System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
      => Enumerate();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion IEnumerable

    private sealed class EmptyList
      : IList<TValue>
    {
      #region IList implementation
      public bool IsEmpty
        => true;
      public TValue Value
        => throw new System.Exception(nameof(EmptyList));
      public IList<TValue> Add(TValue value)
        => new List<TValue>(value, this);
      public bool Contains(TValue value)
        => throw new System.Exception(nameof(EmptyList));
      public IList<TValue> Remove(TValue value)
        => throw new System.Exception(nameof(EmptyList));
      #endregion IList implementation

      #region IEnumerable
      public System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
      { yield break; }
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
      #endregion IEnumerable
    }
  }
}
