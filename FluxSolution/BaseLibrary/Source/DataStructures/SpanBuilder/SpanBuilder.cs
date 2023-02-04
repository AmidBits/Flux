namespace Flux
{
  public ref struct SpanBuilder<T>
  {
    private const int DefaultBufferSize = 32;

    private T[] m_array;

    private int m_head; // Start of buffer data.
    private int m_tail; // End of buffer data.

    private SpanBuilder(int capacity)
    {
      m_array = capacity >= 1 ? System.Buffers.ArrayPool<T>.Shared.Rent(capacity.Pow2AwayFromZero(false, out int _)) : System.Array.Empty<T>();

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }
    public SpanBuilder() : this(DefaultBufferSize) { }

    public SpanBuilder(T value, int count = 1) : this() => Append(value, count);
    public SpanBuilder(System.Collections.Generic.ICollection<T> collection, int count = 1) : this(collection.Count * count) => Append(collection, count);
    public SpanBuilder(System.ReadOnlySpan<T> readOnlySpan, int count = 1) : this(0) => Append(readOnlySpan, count);

    private int AppendCapacity => m_array.Length - m_tail;
    private int PrependCapacity => m_head;

    /// <summary>Grows a uniform (i.e. both left and right satisfies) buffer capacity to at least that specified.</summary>
    private void EnsureUniformCapacity(int sideCapacity)
    {
      var totalCapacity = int.Max(DefaultBufferSize, sideCapacity + Length + sideCapacity);

      if (totalCapacity < Capacity) // We got overall capacity, just need to make sure we have it on both sides.
      {
        if (PrependCapacity < sideCapacity || AppendCapacity < sideCapacity) // If any one side is below capacity, we center the content to ensure uniform capacity.
        {
          var head = (Capacity - Length) / 2; // Center content.
          var tail = head + Length;

          System.Array.Copy(m_array, m_head, m_array, head, Length); // Copy content.

          m_head = head;
          m_tail = tail;
        }
      }
      else // Not enough uniform capacity available.
      {
        var array = System.Buffers.ArrayPool<T>.Shared.Rent(totalCapacity.Pow2AwayFromZero(true, out int _));

        var head = (array.Length - Length) / 2;
        var tail = head + Length;

        System.Array.Copy(m_array, m_head, array, head, Length); // Copy old content.

        System.Buffers.ArrayPool<T>.Shared.Return(m_array); // Recycle the old array.

        m_array = array;

        m_head = head;
        m_tail = tail;
      }
    }

    /// <summary>Grows an append (right) buffer capacity to at least that specified.</summary>
    private void EnsureAppendCapacity(int appendCapacity)
    {
      if (AppendCapacity < appendCapacity) // Not enough append capacity.
      {
        var totalCapacity = int.Max(DefaultBufferSize, PrependCapacity + Length + appendCapacity);

        if (Capacity <= totalCapacity) // Not enough total capacity.
        {
          var array = System.Buffers.ArrayPool<T>.Shared.Rent(totalCapacity.Pow2AwayFromZero(true, out int _));

          var head = (array.Length - Length - appendCapacity) / 2;
          var tail = head + Length;

          System.Array.Copy(m_array, m_head, array, head, Length); // Copy old content.

          System.Buffers.ArrayPool<T>.Shared.Return(m_array); // Recycle the old array.

          m_array = array;

          m_head = head;
          m_tail = tail;
        }
        else if (AppendCapacity < appendCapacity) // Enough capacity, center content if needed.
        {
          var head = (Capacity - Length) / 2;
          var tail = head + Length;

          System.Array.Copy(m_array, m_head, m_array, head, Length); // Copy content within itself.

          m_head = head;
          m_tail = tail;
        }
      }
    }

    /// <summary>Grows a prepend (left) buffer capacity to at least that specified.</summary>
    private void EnsurePrependCapacity(int prependCapacity)
    {
      if (PrependCapacity < prependCapacity) // Not enough prepend capacity.
      {
        var totalCapacity = int.Max(DefaultBufferSize, prependCapacity + Length + AppendCapacity);

        if (Capacity < totalCapacity) // Not enough total capacity, allocate new array.
        {
          var array = System.Buffers.ArrayPool<T>.Shared.Rent(totalCapacity.Pow2AwayFromZero(true, out int _));

          var head = (array.Length - Length + prependCapacity) / 2;
          var tail = head + Length;

          System.Array.Copy(m_array, m_head, array, head, Length); // Copy content.

          System.Buffers.ArrayPool<T>.Shared.Return(m_array); // Recycle the old array.

          m_array = array;

          m_head = head;
          m_tail = tail;
        }
        else if (PrependCapacity < prependCapacity) // Enough total capacity, center content if needed.
        {
          var head = (Capacity - Length) / 2;
          var tail = head + Length;

          System.Array.Copy(m_array, m_head, m_array, head, Length); // Enough prepend space, so utilize by moving content.

          m_head = head;
          m_tail = tail;
        }
      }
    }

    /// <summary>Gets or sets the item at the specified item position in this instance.</summary>
    public T this[int index] { get => GetValue(index); set => SetValue(index, value); }

    /// <summary>The current capacity of the builder.</summary>
    public int Capacity => m_array.Length;
    /// <summary>The current content length of the builder.</summary>
    public int Length => m_tail - m_head;

    /// <summary>Append <paramref name="count"/> of <paramref name="value"/>.</summary>
    public SpanBuilder<T> Append(T value, int count = 1)
    {
      EnsureAppendCapacity(count);

      while (count-- > 0)
        m_array[m_tail++] = value;

      return this;
    }

    /// <summary>Append a <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Append(System.Collections.Generic.ICollection<T> collection, int count = 1)
    {
      EnsureAppendCapacity(collection.Count * count);

      while (count-- > 0)
      {
        collection.CopyTo(m_array, m_tail);

        m_tail += collection.Count;
      }

      return this;
    }

    /// <summary>Append <paramref name="count"/> of <paramref name="values"/>.</summary>
    public SpanBuilder<T> Append(System.ReadOnlySpan<T> values, int count = 1)
    {
      EnsureAppendCapacity(values.Length * count);

      while (count-- > 0)
      {
        values.CopyTo(m_array, m_tail);

        m_tail += values.Length;
      }

      return this;
    }

    /// <summary>Creates a non-allocating <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public System.ReadOnlySpan<T> AsReadOnlySpan() => new(m_array, m_head, m_tail - m_head);

    /// <summary>Creates a non-allocating <see cref="System.Span{T}"/>.</summary>
    public System.Span<T> AsSpan() => new(m_array, m_head, m_tail - m_head);

    /// <summary>Remove all values from the builder.</summary>
    public SpanBuilder<T> Clear()
    {
      System.Array.Clear(m_array);

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;

      return this;
    }

    public SpanBuilder<T> CopyInPlace(int fromIndex, int toIndex, int count)
    {
      if (fromIndex < 0 || fromIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(fromIndex));
      if (toIndex < 0 || toIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(fromIndex));
      if (count < 0 || fromIndex + count > Length || toIndex + count > Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      fromIndex += m_head;
      toIndex += m_head;

      while (count-- > 0)
        m_array[toIndex++] = m_array[fromIndex++];

      return this;
    }

    public void CopyTo(int sourceIndex, System.Collections.Generic.IList<T> target, int targetIndex, int count)
    {
      while (count-- > 0)
        target[targetIndex++] = GetValue(sourceIndex++);
    }

    /// <summary>Gets the value at the specified index.</summary>
    public T GetValue(int index) => (index >= 0 && index < Length) ? m_array[m_head + index] : throw new System.ArgumentOutOfRangeException(nameof(index));

    /// <summary>Insert <paramref name="count"/> of <paramref name="value"/> starting at <paramref name="startIndex"/>.</summary>
    public SpanBuilder<T> Insert(int startIndex, T value, int count = 1)
    {
      EnsureUniformCapacity(count);

      System.Array.Copy(m_array, m_head, m_array, m_head - count, startIndex); // Copy left portion of content.

      startIndex += m_head;

      System.Array.Fill(m_array, value, startIndex - count, count);

      m_head -= count;

      return this;
    }

    /// <summary>Insert <paramref name="count"/> of <paramref name="collection"/> starting at <paramref name="startIndex"/>.</summary>
    public SpanBuilder<T> Insert(int startIndex, System.Collections.Generic.ICollection<T> collection, int count = 1)
    {
      var totalLength = collection.Count * count;

      EnsureUniformCapacity(totalLength);

      System.Array.Copy(m_array, m_head, m_array, m_head - totalLength, startIndex); // Copy left portion of content.

      startIndex += m_head;
      m_head -= totalLength;

      while (count-- > 0)
        collection.CopyTo(m_array, startIndex -= collection.Count);

      return this;
    }

    /// <summary>Insert <paramref name="count"/> of <paramref name="span"/> starting at <paramref name="startIndex"/>.</summary>
    public SpanBuilder<T> Insert(int startIndex, System.ReadOnlySpan<T> span, int count = 1)
    {
      var totalLength = span.Length * count;

      EnsureUniformCapacity(totalLength);

      System.Array.Copy(m_array, m_head, m_array, m_head - totalLength, startIndex); // Copy left portion of content.

      startIndex += m_head;
      m_head -= totalLength;

      while (count-- > 0)
        span.CopyTo(m_array, startIndex -= span.Length);

      return this;
    }

    /// <summary>Prepends with a <paramref name="value"/>, <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Prepend(T value, int count = 1)
    {
      EnsurePrependCapacity(count);

      while (count-- > 0)
        m_array[--m_head] = value;

      return this;
    }

    /// <summary>Prepends with the <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Prepend(System.Collections.Generic.ICollection<T> collection, int count = 1)
    {
      EnsurePrependCapacity(collection.Count);

      while (count-- > 0)
        collection.CopyTo(m_array, m_head -= collection.Count);

      return this;
    }

    /// <summary>Prepends with the <paramref name="span"/>, <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Prepend(System.ReadOnlySpan<T> span, int count = 1)
    {
      EnsurePrependCapacity(span.Length * count);

      while (count-- > 0)
        span.CopyTo(m_array, m_head -= span.Length);

      return this;
    }

    /// <summary>Removes the specified range of values from the builder.</summary>
    public SpanBuilder<T> Remove(int startIndex, int count)
    {
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
        m_tail -= count;
        System.Array.Fill(m_array, default, m_tail, count);
      }

      return this;
    }

    /// <summary>Reverse all items in the range [startIndex, endIndex], in the builder.</summary>
    public SpanBuilder<T> Reverse(int startIndex, int endIndex)
    {
      if (startIndex < 0 || startIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      for (startIndex += m_head, endIndex += m_head; startIndex < endIndex; startIndex++, endIndex--)
      {
        var tmp = m_array[startIndex];
        m_array[startIndex] = m_array[endIndex];
        m_array[endIndex] = tmp;
      }

      return this;
    }

    public void SetValue(int index, T value) => m_array[m_head + index] = (index >= 0 && index < Length) ? value : throw new System.ArgumentOutOfRangeException(nameof(index));

    /// <summary>Swap two values at the specified indices.</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public SpanBuilder<T> Swap(int indexA, int indexB)
    {
      if (indexA != indexB)
      {
        var vA = GetValue(indexA);
        var vB = GetValue(indexB);

        SetValue(indexA, vB);
        SetValue(indexB, vA);
      }

      return this;
    }

    #region Object overrides.
    public override string ToString() => AsReadOnlySpan().ToString(0, Length);
    #endregion Object overrides.
  }
}
