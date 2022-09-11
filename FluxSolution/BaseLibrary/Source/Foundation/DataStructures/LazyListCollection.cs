namespace Flux.DataStructures
{
  public sealed class LazyListCollection<T>
    : Disposable, System.Collections.Generic.IList<T>
  {
    private readonly System.Collections.Generic.List<T> m_cache = new();
    private readonly System.Collections.Generic.Queue<System.Collections.Generic.IEnumerator<T>> m_enumerators = new();
    private int m_version;

    public bool AllEnumeratorsExhausted { get; private set; }

    public LazyListCollection(System.Collections.Generic.IEnumerable<T> collection)
    {
      m_version++;

      Attach(collection);
    }
    public LazyListCollection(params T[] collection)
      : this(collection.AsEnumerable()) { }

    public void Attach(System.Collections.Generic.IEnumerable<T> collection)
    {
      m_version++;

      AllEnumeratorsExhausted = false;

      m_enumerators.Enqueue((collection ?? throw new System.ArgumentNullException(nameof(collection))).GetEnumerator());
    }
    public void Attach(params T[] collection)
      => Attach(collection.AsEnumerable());

    protected override void DisposeManaged()
    {
      while (m_enumerators.Count > 0)
        m_enumerators.Dequeue().Dispose();
    }

    #region Implementation of IList<T>
    //public void EnsureOrder()
    //{
    //  while (m_enumerators.Count > 0)
    //  {
    //    var e = m_enumerators.Dequeue();

    //    while (e.MoveNext())
    //      m_cache.Add(e.Current);

    //    e.Dispose();
    //  }
    //}

    public T this[int index]
    {
      get => m_cache[index];
      set
      {
        m_version++;
        m_cache[index] = value;
      }
    }

    public int Count => m_cache.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
      m_version++;
      m_cache.Add(item);
    }

    public void Clear()
    {
      m_version++;
      m_cache.Clear();
    }

    public bool Contains(T item) => m_cache.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => m_cache.CopyTo(array, arrayIndex);

    public System.Collections.Generic.IEnumerator<T> GetEnumerator() => AllEnumeratorsExhausted ? m_cache.GetEnumerator() : new LazyListEnumerator(this);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    public int IndexOf(T item) => m_cache.IndexOf(item);

    public void Insert(int index, T item)
    {
      m_version++;
      m_cache.Insert(index, item);
    }

    public bool Remove(T item)
    {
      m_version++;
      return m_cache.Remove(item);
    }

    public void RemoveAt(int index)
    {
      m_version++;
      m_cache.RemoveAt(index);
    }
    #endregion Implementation of IList<T>

    public override string ToString()
      => $"{GetType().Name} {{ Count = {Count} }}";

    private sealed class LazyListEnumerator
      : System.Collections.Generic.IEnumerator<T>
    {
      private T m_current;
      private int m_index;
      private readonly object m_lock = new();
      private readonly LazyListCollection<T> m_source;
      private readonly int m_version;

      public LazyListEnumerator(LazyListCollection<T> source)
      {
        m_source = source;
        m_version = source.m_version;
        m_index = -1;
        m_current = default!;
      }

      public T Current
        => m_current;
      object? System.Collections.IEnumerator.Current
        => m_current;

      public bool MoveNext()
      {
        lock (m_lock)
        {
          ValidateVersion();

          m_index++;

          if (m_index < m_source.Count)
          {
            m_current = m_source[m_index];

            return true;
          }

          while (!m_source.AllEnumeratorsExhausted)
          {
            if (m_source.m_enumerators.Count > 0)
            {
              if (m_source.m_enumerators.Peek().MoveNext())
              {
                m_current = m_source.m_enumerators.Peek().Current;

                m_source.m_cache.Add(m_current);

                return true;
              }
              else
              {
                m_source.m_enumerators.Dequeue();

                m_source.AllEnumeratorsExhausted = m_source.m_enumerators.Count == 0;
              }
            }
          }
        }

        return false;
      }

      public void Reset()
      {
        ValidateVersion();

        m_index = -1;
      }

      private void ValidateVersion()
      {
        if (m_version != m_source.m_version)
          throw new System.Exception(@"The list has changed.");
      }

      public void Dispose() // This class does not yet allocate any resources on its own.
      {
      }
    }
  }
}
