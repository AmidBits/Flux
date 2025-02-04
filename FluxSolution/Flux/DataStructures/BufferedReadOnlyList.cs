namespace Flux.DataStructures
{
  /// <summary>Enumerates a sequence only once, or as far as the original sequence is enumerated. It is an <see cref="IReadOnlyList{T}"/> that grows as elements from the specified sequence are enumerated. The <see cref="IReadOnlyList{T}"/> property Count and the indexer only reflect elements buffered "so far". The TryGetElementAt(index) can be used manuallly, with an index greater than or equal to Count in order to dynamically buffer more elements. The process is all automatic when using the GetEnumerator() method.</summary>
  public sealed class BufferedReadOnlyList<T>(System.Collections.Generic.IEnumerator<T> collection)
    : Disposable, System.Collections.Generic.IReadOnlyList<T>
  {
    private System.Collections.Generic.IEnumerator<T>? m_enumerator = collection;
    private readonly System.Collections.Generic.List<T> m_buffer = [];
    private readonly System.Threading.Lock m_lock = new();

    public BufferedReadOnlyList(System.Collections.Generic.IEnumerable<T> collection) : this(collection.GetEnumerator()) { }

    protected override void DisposeManaged()
    {
      m_enumerator?.Dispose();
      m_enumerator = null;
      m_buffer.Clear();
    }

    /// <summary>Exhausts the source of all its elements into the <see cref="BufferedReadOnlyList{T}"/> for consumption as a whole.</summary>
    /// <remarks>It could be costly not to exploit the initial enumeration.</remarks>
    public void ExhaustSource() => TryGetElementAt(int.MaxValue, out var _);

    /// <summary>Return the element at <paramref name="index"/> in out parameter <paramref name="result"/>.</summary>
    /// <remarks>If the index is not already in buffer, then buffer all elements up to <paramref name="index"/>.</remarks>
    public bool TryGetElementAt(int index, out T result)
    {
      if (index >= 0) // Skip negative indices.
        lock (m_lock)
        {
          if (m_enumerator is not null) // Skip if exhausted.
            while (index >= m_buffer.Count) // As long as there are elements in source, buffer up to index.
            {
              if (!(m_enumerator?.MoveNext() ?? false))
              {
                DisposeManaged(); // If we reach the end of the source, dispose of the source.
                break;
              }

              m_buffer.Add(m_enumerator.Current);
            }

          if (index < m_buffer.Count) // If the index is occupied in buffer.
          {
            result = m_buffer[index];
            return true;
          }
        }

      result = default!;
      return false;
    }

    #region Implementation of IReadOnlyList<T>

    public T this[int index] => m_buffer[index];

    public int Count => m_buffer.Count;

    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
    //=> new BufferedReadOnlyListEnumerator(this);
    {
      for (var index = 0; index < m_buffer.Count; index++)
        if (TryGetElementAt(index, out var item))
          yield return item;
        else
          yield break;
    }
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
