namespace Flux.DataStructures
{
  /// <summary>Enumerates a sequence once. It is an <see cref="IReadOnlyList{T}"/> that grows as elements from the specified sequence are enumerated. The <see cref="IReadOnlyList{T}"/> property Count and the indexer only reflect elements buffered "so far". The TryGetElementAt(index) can be used manuallly, with an index greater than or equal to Count in order to dynamically buffer more elements. The process is all automatic when using the GetEnumerator() method.</summary>
  public sealed class BufferedReadOnlyList<T>
    : Disposable, System.Collections.Generic.IReadOnlyList<T>
  {
    private System.Collections.Generic.IEnumerator<T>? m_enumerator = null;
    private readonly System.Collections.Generic.List<T> m_list = new();
    private readonly object m_lock = new();

    public BufferedReadOnlyList(System.Collections.Generic.IEnumerable<T> collection) => m_enumerator = collection.GetEnumerator();

    protected override void DisposeManaged()
    {
      m_enumerator?.Dispose();
      m_enumerator = null;
    }

    public void GetAllElements() => TryGetElementAt(int.MaxValue, out var _);

    public bool TryGetElementAt(int index, out T result)
    {
      lock (m_lock)
      {
        while (index >= m_list.Count)
        {
          if (!(m_enumerator?.MoveNext() ?? false))
          {
            DisposeManaged();
            break;
          }

          m_list.Add(m_enumerator.Current);
        }

        if (index < m_list.Count)
        {
          result = m_list[index];
          return true;
        }

        result = default!;
        return false;
      }
    }

    #region Implementation of IReadOnlyList<T>

    /// <summary>Indexer for buffered elements currently in the <see cref="IReadOnlyList{T}"/>.</summary>
    public T this[int index] => m_list[index];

    /// <summary>The count of buffered elements currently in the <see cref="IReadOnlyList{T}"/>.</summary>
    public int Count => m_list.Count;

    /// <summary>Get an enumerator that enumerates and buffers all elements in the original sequence.</summary>
    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
    //=> new BufferedReadOnlyListEnumerator(this);
    {
      for (var index = 0; index < m_list.Count; index++)
        if (TryGetElementAt(index, out var item))
          yield return item;
        else
          yield break;
    }
    /// <summary>Get an enumerator that enumerates and buffers all elements in the original sequence.</summary>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion Implementation of IReadOnlyList<T>

    public override string ToString() => $"{GetType().Name} {{ Enumerator = {(m_enumerator is null ? "Closed" : "Open")}, Count = {Count} }}";

    //private sealed class BufferedReadOnlyListEnumerator
    //  : System.Collections.Generic.IEnumerator<T>
    //{
    //  private T m_current;
    //  private int m_index;
    //  private readonly BufferedReadOnlyList<T> m_source;

    //  public BufferedReadOnlyListEnumerator(BufferedReadOnlyList<T> source)
    //  {
    //    m_current = default!;
    //    m_index = -1;
    //    m_source = source;
    //  }

    //  public T Current => m_current;
    //  object? System.Collections.IEnumerator.Current => m_current;

    //  public bool MoveNext() => m_source.TryGetElementAt(++m_index, out m_current);

    //  public void Reset() => m_index = -1;

    //  public void Dispose() { } // This class does not yet allocate any resources on its own.
    //}
  }
}
