namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static void AppendLine(ref this SpanBuilder<char> source, System.ReadOnlySpan<char> value)
    {
      source.Append(value);
      source.Append(System.Environment.NewLine);
    }

    public static void InsertLine(ref this SpanBuilder<char> source, int index, System.ReadOnlySpan<char> value)
    {
      source.Insert(index, value);
      source.Insert(index + value.Length, System.Environment.NewLine);
    }

    public static string ToString(ref this SpanBuilder<char> source, int startIndex, int length)
      => source.AsReadOnlySpan().Slice(startIndex, length).ToString();
    public static string ToString(ref this SpanBuilder<char> source, int startIndex)
      => source.ToString(startIndex, source.Length - startIndex);
  }

  public ref partial struct SpanBuilder<T>
    where T : notnull
  {
    private System.Span<T> m_buffer;
    private int m_bufferPosition;

    public SpanBuilder(int capacity)
    {
      m_buffer = System.Buffers.ArrayPool<T>.Shared.Rent(capacity);
      m_bufferPosition = 0;
    }
    public SpanBuilder(System.ReadOnlySpan<T> value)
      : this(System.Convert.ToInt32(System.Math.Pow(2, System.Convert.ToInt32(System.Math.Log(value.Length - 1, 2)) + 1)))
    {
      value.CopyTo(m_buffer);
      m_bufferPosition = value.Length;
    }
    public SpanBuilder(System.Span<T> value)
      : this(System.Convert.ToInt32(System.Math.Pow(2, System.Convert.ToInt32(System.Math.Log(value.Length - 1, 2)) + 1)))
    {
      value.CopyTo(m_buffer);
      m_bufferPosition = value.Length;
    }
    public SpanBuilder(T[] value)
      : this(System.Convert.ToInt32(System.Math.Pow(2, System.Convert.ToInt32(System.Math.Log(value.Length - 1, 2)) + 1)))
    {
      value.CopyTo(m_buffer);
      m_bufferPosition = value.Length;
    }
    public SpanBuilder()
      : this(32)
    {
    }

    /// <summary>The current capacity of the SpanBuilder.</summary>
    public int Capacity
      => m_buffer.Length;

    /// <summary>The length of the current content of the SpanBuilder.</summary>
    public int Length
      => m_bufferPosition;

    /// <summary>Gets or sets the item at the specified item position in this instance.</summary>
    public ref T this[int index]
      => ref m_buffer[index];

    /// <summary>Adds the value to this instance.</summary>
    public void Append(T value, int count = 1)
    {
      EnsureCapacity(m_bufferPosition + count);

      while (count-- > 0)
        m_buffer[m_bufferPosition++] = value;
    }
    /// <summary>Adds the sequence of items to this instance.</summary>
    public void Append(ReadOnlySpan<T> value)
    {
      if (m_bufferPosition + value.Length is var needed && needed > m_buffer.Length) EnsureCapacity(needed * 2);

      value.CopyTo(m_buffer[m_bufferPosition..]);

      m_bufferPosition += value.Length;
    }

    public System.ReadOnlySpan<T> AsReadOnlySpan()
      => m_buffer[..m_bufferPosition];

    private void Cleanup()
      => m_buffer.Slice(m_bufferPosition).Clear();

    /// <summary>Removes all items in this instance.</summary>
    public void Clear()
      => m_bufferPosition = 0;

    //public static SpanBuilder<T> Create(T[] value)
    //  => new SpanBuilder<T> { m_buffer = value, m_bufferPosition = value.Length };

    /// <summary>Grows the buffer capacity to at least that specified.</summary>
    private int EnsureCapacity(int capacity = 0)
    {
      var realCapacity = m_buffer.Length;

      if (capacity > realCapacity)
      {
        if (realCapacity == 0)
          realCapacity = 32;

        while (capacity > realCapacity)
          realCapacity <<= 1;

        //var newCapacity = capacity > m_buffer.Length ? capacity : m_buffer.Length * 2;

        var rented = System.Buffers.ArrayPool<T>.Shared.Rent(realCapacity);
        m_buffer.CopyTo(rented);
        m_buffer = rented;
        System.Buffers.ArrayPool<T>.Shared.Return(rented);
      }

      return realCapacity;
    }

    /// <summary>Inserts the value into this instance at the specified index.</summary>
    public void Insert(int index, T value, int count = 1)
    {
      if (index < 0 || index > m_bufferPosition) throw new ArgumentOutOfRangeException(nameof(index));

      EnsureCapacity(m_bufferPosition + count);

      var moveIndex = index + count;

      m_buffer[index..m_bufferPosition].CopyTo(m_buffer[moveIndex..]); // Move right side of old content.

      m_bufferPosition += count; // Update the position (length) before copying data (count is altered below).

      while (count-- > 0)
        m_buffer[index++] = value;
    }
    /// <summary>Inserts the sequence of items into this instance at the specified index.</summary>
    public void Insert(int index, System.ReadOnlySpan<T> value)
    {
      if (index < 0 || index > m_bufferPosition) throw new ArgumentOutOfRangeException(nameof(index));

      if (m_bufferPosition + value.Length is var needed && needed > m_buffer.Length)
        EnsureCapacity(needed);

      var endIndex = index + value.Length;

      m_buffer[index..m_bufferPosition].CopyTo(m_buffer[endIndex..]); // Move right side of old content.

      value.CopyTo(m_buffer[index..endIndex]); // Insert new content in buffer gap.

      m_bufferPosition += value.Length;
    }

    /// <summary>Removes the specified range of items from this instance.</summary>
    public void Remove(int startIndex, int length)
    {
      if (startIndex < 0 || startIndex >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (length < 0 || startIndex + length is var endIndex && endIndex > m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(length));

      m_buffer[(startIndex + length)..m_bufferPosition].CopyTo(m_buffer[startIndex..]);

      m_bufferPosition -= length;

      m_buffer.Slice(m_bufferPosition, length).Clear();

      Cleanup();
    }
  }
}
