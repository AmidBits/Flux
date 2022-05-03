namespace Flux
{
  public class StringBuilder<TType>
    : System.Collections.Generic.IList<TType>
  {
    private const int DefaultBufferSize = 16;

    private TType[] m_array;

    private int m_head; // Start of content.
    private int m_tail; // End of content.

    private StringBuilder(int count)
    {
      m_array = new TType[count];

      m_head = count / 2;
      m_tail = count / 2;
    }
    public StringBuilder()
      : this(DefaultBufferSize)
    {
    }

    private void DoubleBuffer()
    {
      var length = m_tail - m_head;

      var array = new TType[m_array.Length * 2];

      var head = (array.Length - length) / 2;
      var tail = head + length;

      System.Array.Copy(m_array, m_head, array, head, length);

      m_array = array;

      m_head = head;
      m_tail = tail;
    }

    private void EnsureBuffer(int length)
    {
      while (m_head < length || m_tail + length > m_array.Length)
        DoubleBuffer();
    }

    public StringBuilder<TType> Append(TType value)
    {
      m_version++;

      if (m_tail == m_array.Length)
        DoubleBuffer();

      m_array[m_tail++] = value;

      return this;
    }
    public StringBuilder<TType> Append(System.ReadOnlySpan<TType> span)
    {
      m_version++;

      EnsureBuffer(span.Length);

      span.CopyTo(m_array, m_tail);

      m_tail += span.Length;

      return this;
    }

    public StringBuilder<TType> Insert(int startIndex, int count, TType filler = default!)
    {
      m_version++;
      EnsureBuffer(count);
      startIndex += m_head;
      if (m_head >= m_array.Length - m_tail) // Grow from start.
      {
        System.Array.Copy(m_array, m_head, m_array, m_head - count, startIndex - m_head);
        System.Array.Fill(m_array, filler, startIndex - count, count);
        m_head -= count;
      }
      else // Otherwise grow from end.
      {
        System.Array.Copy(m_array, startIndex, m_array, startIndex + count, m_tail - startIndex);
        System.Array.Fill(m_array, filler, startIndex, count);
        m_tail += count;
      }
      return this;
    }

    public StringBuilder<TType> Prepend(TType value)
    {
      m_version++;

      if (m_head == 0)
        EnsureBuffer(1);

      m_array[--m_head] = value;

      return this;
    }
    public StringBuilder<TType> Prepend(System.ReadOnlySpan<TType> span)
    {
      m_version++;

      EnsureBuffer(span.Length);

      m_head -= span.Length;

      span.CopyTo(m_array, m_head);

      return this;
    }

    public StringBuilder<TType> Remove(int startIndex, int count)
    {
      m_version++;
      startIndex += m_head;
      if (m_head <= m_array.Length - m_tail) // Shrink from start.
      {
        System.Array.Copy(m_array, m_head, m_array, m_head + count, startIndex - m_head);
        System.Array.Fill(m_array, default, m_head, count);
        m_head += count;
      }
      else // Otherwise shrink from end.
      {
        System.Array.Copy(m_array, startIndex + count, m_array, startIndex, m_tail - startIndex - count);
        System.Array.Fill(m_array, default, m_head, startIndex + count);
        m_tail -= count;
      }
      return this;
    }

    public System.ReadOnlySpan<TType> ToReadOnlySpan(int startAt, int count)
    {
      if (m_head + startAt > m_tail) throw new System.ArgumentOutOfRangeException(nameof(startAt));
      if (m_head + startAt + count > m_tail) throw new System.ArgumentOutOfRangeException(nameof(count));

      var a = new TType[count];
      System.Array.Copy(m_array, m_head + startAt, a, 0, count);
      return a;
    }
    public System.Span<TType> ToSpan(int startAt, int count)
    {
      if (m_head + startAt > m_tail) throw new System.ArgumentOutOfRangeException(nameof(startAt));
      if (m_head + startAt + count > m_tail) throw new System.ArgumentOutOfRangeException(nameof(count));

      var a = new TType[count];
      System.Array.Copy(m_array, m_head + startAt, a, 0, count);
      return a;
    }

    #region Implementation of IList<TType>
    private int m_version = 0;

    public TType this[int index]
    {
      get => m_array[index];
      set
      {
        m_version++;
        m_array[index] = value;
      }
    }
    public int Count
      => m_tail - m_head;

    public bool IsReadOnly
      => false;

    public void Add(TType item)
    {
      m_version++;
      Append(item);
    }

    public void Clear()
    {
      m_version++;

      m_array = new TType[DefaultBufferSize];

      m_head = DefaultBufferSize / 2;
      m_tail = DefaultBufferSize / 2;
    }

    public bool Contains(TType item)
      => System.Array.Exists(m_array, t => t?.Equals(item) ?? false);

    public void CopyTo(TType[] array, int arrayIndex)
      => m_array.CopyTo(array, arrayIndex);

    public System.Collections.Generic.IEnumerator<TType> GetEnumerator()
    {
      for (var index = m_head; index < m_tail; index++)
        yield return m_array[index];
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    public int IndexOf(TType item)
      => System.Array.IndexOf(m_array, item);

    public void Insert(int index, TType item)
    {
      m_version++;
      m_array = m_array.Insert(index, item);
    }

    public bool Remove(TType item)
    {
      if (IndexOf(item) is var index && index > -1)
      {
        RemoveAt(index);
        return true;
      }
      return false;
    }

    public void RemoveAt(int index)
      => Remove(index, 1);
    #endregion Implementation of IList<TType>
  }

  public class CharStringBuilder
    : StringBuilder<char>
  {
  }

  public class RuneStringBuilder
    : StringBuilder<System.Text.Rune>
  {
  }
}
