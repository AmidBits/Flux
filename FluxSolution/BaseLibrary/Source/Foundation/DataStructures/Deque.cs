namespace Flux.DataStructures
{
  public interface IDeque<T>
  {
    bool IsEmpty { get; }
    T Dequeue();
    void Enqueue(T value);
    System.Collections.Generic.IEnumerable<T> PeekQueue();
    System.Collections.Generic.IEnumerable<T> PeekStack();
    T Pop();
    void Push(T value);
  }

  /// <summary>A queue, for which elements can be added to or removed from either the front (head) or back (tail).</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Double-ended_queue"/>
  /// <seealso cref="https://referencesource.microsoft.com/#mscorlib/system/collections/queue.cs,f7cdfd0f848ca249"/>
  public class Deque<T>
    : System.Collections.Generic.IEnumerable<T>, IDeque<T>
  {
    public static readonly IDeque<T> Empty = new EmptyDeque();

    private const string DequeIsEmpty = @"The deque is empty.";

    private T[] m_array = System.Array.Empty<T>();
    private int m_tail, m_head;
    private int m_size;
    private readonly int m_sizeMargin = 32;
    private readonly double m_growFactor = 2.0;// Math.GoldenRatio.Value;
    private int m_version;

    public T this[int index]
    {
      get => m_array[index >= 0 && index < m_size ? (m_head + index) % m_array.Length : throw new System.ArgumentOutOfRangeException(nameof(index))];
      set => m_array[index >= 0 && index < m_size ? (m_head + index) % m_array.Length : throw new System.ArgumentOutOfRangeException(nameof(index))] = value;
    }

    public int Capacity { get => m_size; set => SetCapacity(value); }

    public int Length
      => m_size;

    public Deque() { }
    public Deque(int capacity)
      => SetCapacity(capacity);
    public Deque(System.Collections.Generic.ICollection<T> collection)
    {
      if (collection is null) throw new System.ArgumentNullException(nameof(collection));

      foreach (var item in collection)
        Enqueue(item);
    }

    public bool IsEmpty => m_size == 0;
    //public T DequeueLeft() => Dequeue();
    //public T DequeueRight() => Pop();
    //public void EnqueueLeft(T item) => Enqueue(item);
    //public void EnqueueRight(T item) => Push(item);
    //public T PeekLeft() => m_array[m_head];
    //public T PeekRight() => m_array[m_tail - 1];

    public virtual void Clear()
    {
      if (m_head < m_tail) System.Array.Clear(m_array, m_head, m_size);
      else
      {
        System.Array.Clear(m_array, m_head, m_array.Length - m_head);
        System.Array.Clear(m_array, 0, m_tail);
      }

      m_head = 0;
      m_tail = 0;
      m_size = 0;
      m_version++;
    }

    public virtual bool Contains(T item)
    {
      for (int count = m_size, index = m_head; count-- > 0; index = (index + 1) % m_array.Length)
      {
        var arrayItem = m_array[index];

        if (item == null)
        {
          if (arrayItem == null) return true;
        }
        else if (arrayItem != null && arrayItem.Equals(item))
        {
          return true;
        }
      }

      return false;
    }

    public T Dequeue()
    {
      if (m_size > 0)
      {
        T removed = m_array[m_head];

        m_array[m_head] = default!;
        m_head = (m_head + 1) % m_array.Length;
        m_size--;
        m_version++;

        return removed;
      }
      else throw new System.InvalidOperationException(DequeIsEmpty);
    }
    public void Enqueue(T item)
    {
      if (m_size == m_array.Length)
      {
        var capacity = (int)(m_array.Length * m_growFactor);

        if (capacity < m_array.Length + m_sizeMargin) capacity = m_array.Length + m_sizeMargin;

        SetCapacity(capacity);
      }

      m_array[m_tail] = item;
      m_tail = (m_tail + 1) % m_array.Length;
      m_size++;
      m_version++;
    }

    public System.Collections.Generic.IEnumerable<T> PeekQueue()
    {
      for (var index = m_head; index != m_tail; index = (index + 1) % m_array.Length)
      {
        yield return m_array[index];
      }
    }
    public System.Collections.Generic.IEnumerable<T> PeekStack()
    {
      for (var index = m_tail - 1; index != m_head; index = ((index - 1) + m_array.Length) % m_array.Length)
      {
        yield return m_array[index];
      }

      yield return m_array[m_head];
    }

    public T Pop()
    {
      if (m_size > 0)
      {
        m_tail = (m_tail - 1 + m_array.Length) % m_array.Length;

        T removed = m_array[m_tail];

        m_array[m_tail] = default!;
        m_size--;
        m_version++;

        return removed;
      }
      else throw new System.InvalidOperationException(DequeIsEmpty);
    }
    public void Push(T item)
    {
      if (m_size == m_array.Length)
      {
        var capacity = (int)(m_array.Length * m_growFactor);

        if (capacity < m_array.Length + m_sizeMargin) capacity = m_array.Length + m_sizeMargin;

        SetCapacity(capacity);
      }

      m_head = (m_head - 1 + m_array.Length) % m_array.Length;
      m_array[m_head] = item;
      m_size++;
      m_version++;
    }

    private void SetCapacity(int capacity)
    {
      var array = new T[capacity];

      if (m_size > 0)
      {
        if (m_head < m_tail)
        {
          System.Array.Copy(m_array, m_head, array, 0, m_size);
        }
        else
        {
          System.Array.Copy(m_array, m_head, array, 0, m_array.Length - m_head);
          System.Array.Copy(m_array, 0, array, m_array.Length - m_head, m_tail);
        }
      }

      m_array = array;
      m_head = 0;
      m_tail = (m_size == capacity) ? 0 : m_size;
    }

    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
      => new Enumerator(this);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    private class EmptyDeque
      : IDeque<T>
    {
      public bool IsEmpty => true;

      public T Dequeue() => throw new System.NotImplementedException(nameof(EmptyDeque));
      public void Enqueue(T value) => throw new System.NotImplementedException(nameof(EmptyDeque));
      public System.Collections.Generic.IEnumerable<T> PeekQueue() { yield break; }
      public System.Collections.Generic.IEnumerable<T> PeekStack() { yield break; }
      public T Pop() => throw new System.NotImplementedException(nameof(EmptyDeque));
      public void Push(T value) => throw new System.NotImplementedException(nameof(EmptyDeque));
    }

    private class Enumerator
      : System.Collections.Generic.IEnumerator<T>
    {
      private const string DequeHasChanged = @"The deque has changed.";

      private T m_current;
      private int m_index;
      private readonly object m_lock = new object();
      private readonly Deque<T> m_source;
      private readonly int m_version;

      public Enumerator(Deque<T> source)
      {
        m_current = default!;
        m_index = source.m_head;
        m_source = source;
        m_version = source.m_version;
      }

      public T Current
        => m_current;
      object System.Collections.IEnumerator.Current
        => m_current!;

      public void Dispose() { }

      public bool MoveNext()
      {
        lock (m_lock)
        {
          if (m_version != m_source.m_version) throw new System.InvalidOperationException(DequeHasChanged);
          else if (m_index != m_source.m_tail)
          {
            m_index = (m_index + 1) % m_source.Length;
            m_current = m_source[m_index];

            return true;
          }
          else
          {
            m_current = default!;

            return false;
          }
        }
      }

      public void Reset()
      {
        if (m_version != m_source.m_version) throw new System.InvalidOperationException(DequeHasChanged);

        m_index = m_source.m_head;
      }
    }
  }
}
