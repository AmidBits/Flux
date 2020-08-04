using System.Linq;

namespace Flux.Collections.Generic
{
  public class LazyListCollection<T>
    : Flux.Disposable, System.Collections.Generic.IList<T>
  {
    private readonly System.Collections.Generic.IList<T> m_cache = new System.Collections.Generic.List<T>();
    private readonly System.Collections.Generic.Queue<System.Collections.Generic.IEnumerator<T>> m_enumerators = new System.Collections.Generic.Queue<System.Collections.Generic.IEnumerator<T>>();
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
      m_enumerators.Clear();
    }

    #region Implementation of IList<T>
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

    public bool IsReadOnly => m_cache.IsReadOnly;

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

    public System.Collections.Generic.IEnumerator<T> GetEnumerator() => AllEnumeratorsExhausted ? m_cache.GetEnumerator() : new ExhaustEnumerators(this);
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

    private class ExhaustEnumerators
      : System.Collections.Generic.IEnumerator<T>
    {
      private const string ExceptionMessageVersionChanged = @"The list has changed.";

      private T m_current;
      private int m_index;
      private readonly object m_lock = new object();
      private readonly LazyListCollection<T> m_source;
      private readonly int m_version;

      public ExhaustEnumerators(LazyListCollection<T> source)
      {
        m_current = default!;
        m_index = -1;
        m_source = source;
        m_version = m_source.m_version;
      }

      public T Current
        => m_current;
      object? System.Collections.IEnumerator.Current
        => m_current;

      public void Dispose() { }

      public bool MoveNext()
      {
        lock (m_lock)
        {
          if (m_version != m_source.m_version) throw new System.InvalidOperationException(ExceptionMessageVersionChanged);

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
        if (m_version != m_source.m_version) throw new System.InvalidOperationException(ExceptionMessageVersionChanged);

        m_index = -1;
      }
    }
  }
}
