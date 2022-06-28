namespace Flux
{
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
    public System.Span<T> AsSpan()
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

    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the specfied comparer.</summary>
    public void NormalizeAdjacent(System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var targetIndex = 0;
      var previous = default(T);

      for (var sourceIndex = 0; sourceIndex < m_bufferPosition; sourceIndex++)
      {
        var current = m_buffer[sourceIndex];

        if (!equalityComparer.Equals(current, previous) || (values.Count > 0 && !values.Contains(current, equalityComparer)))
        {
          m_buffer[targetIndex++] = current;

          previous = current;
        }
      }

      m_bufferPosition = targetIndex;

      Clear();
    }
    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the default comparer.</summary>
    public void NormalizeAdjacent(System.Collections.Generic.IList<T> values)
      => NormalizeAdjacent(values, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public void NormalizeAll(T normalizeWith, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var normalizedIndex = 0;

      var isPrevious = true; // Set to true in order for trimming to occur on the left.

      for (var sourceIndex = 0; sourceIndex < m_bufferPosition; sourceIndex++)
      {
        var character = m_buffer[sourceIndex];

        var isCurrent = predicate(character);

        if (!(isPrevious && isCurrent))
        {
          m_buffer[normalizedIndex++] = isCurrent ? normalizeWith : character;

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      m_bufferPosition = normalizedIndex;

      Clear();
    }
    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public void NormalizeAll(T normalizeWith, System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> normalize)
      => NormalizeAll(normalizeWith, t => normalize.Contains(t, equalityComparer));
    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the default equality comparer.</summary>
    public void NormalizeAll(T normalizeWith, System.Collections.Generic.IList<T> normalize)
      => NormalizeAll(normalizeWith, normalize.Contains);

    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
    public void PadEven(int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true)
    {
      if (totalWidth > Length)
      {
        var quotient = System.Math.DivRem(totalWidth - Length, 2, out var remainder);

        PadLeft(Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        // The two lines below are the original right biased (always) which works great.
        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        PadRight(totalWidth, paddingRight);
      }
    }
    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding strings for left and right respectively.</summary>
    public void PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    {
      if (totalWidth > Length)
      {
        var quotient = System.Math.DivRem(totalWidth - Length, 2, out var remainder);

        PadLeft(Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        // The two lines below are the original right biased (always) which works great.
        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        PadRight(totalWidth, paddingRight);
      }
    }

    /// <summary>Pads this StringBuilder on the left with the specified padding character.</summary>
    public void PadLeft(int totalWidth, T padding)
      => Insert(0, padding, totalWidth - Length);
    /// <summary>Pads this StringBuilder on the left with the specified padding string.</summary>
    public void PadLeft(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Insert(0, padding);

      Remove(0, Length - totalWidth);
    }

    /// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
    public void PadRight(int totalWidth, T padding)
      => Append(padding, totalWidth - Length);
    /// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
    public void PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Append(padding);

      Remove(totalWidth, Length - totalWidth);
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

    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed.</summary>
    public void RemoveAll(System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var removedIndex = 0;

      for (var sourceIndex = 0; sourceIndex < m_bufferPosition; sourceIndex++)
      {
        var sourceValue = m_buffer[sourceIndex];

        if (!predicate(sourceValue))
          m_buffer[removedIndex++] = sourceValue;
      }

      m_bufferPosition = removedIndex;

      Cleanup();
    }
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the specified comparer.</summary>
    public void RemoveAll([System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> remove)
      => RemoveAll(t => remove.Contains(t, equalityComparer));
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the default comparer.</summary>
    public void RemoveAll(System.Collections.Generic.IList<T> remove)
      => RemoveAll(remove.Contains);

    /// <summary>Returns the source replicated (copied) the specified number of times.</summary>
    public void Repeat(int count)
    {
      var slice = AsReadOnlySpan();

      while (count-- > 0)
        Insert(m_bufferPosition, slice);
    }

    /// <summary>Reverse all characters in the range [startIndex, endIndex] within the span builder.</summary>
    public void Reverse(int startIndex, int endIndex)
    {
      if (startIndex < 0 || startIndex >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      while (startIndex < endIndex)
        SwapImpl(startIndex++, endIndex--);
    }
    /// <summary>Reverse all characters within the span builder.</summary>
    public void Reverse()
      => Reverse(0, m_bufferPosition - 1);

    /// <summary>Swap two elements by the specified indices.</summary>
    internal void SwapImpl(int indexA, int indexB)
    {
      if (indexA != indexB)
        (m_buffer[indexB], m_buffer[indexA]) = (m_buffer[indexA], m_buffer[indexB]);
    }
    /// <summary>Swap two elements by the specified indices.</summary>
    public void Swap(int indexA, int indexB)
    {
      if (m_bufferPosition == 0)
        throw new System.ArgumentException(@"The span builder is empty.");
      else if (indexA < 0 || indexA >= Length)
        throw new System.ArgumentOutOfRangeException(nameof(indexA));
      else if (indexB < 0 || indexB >= Length)
        throw new System.ArgumentOutOfRangeException(nameof(indexB));
      else
        SwapImpl(indexA, indexB);
    }

    public void SwapFirstWith(int index)
      => SwapImpl(0, index);

    public void SwapLastWith(int index)
      => SwapImpl(index, m_bufferPosition - 1);

    /// <summary>Creates a new list from the specified array from the specified offset and count.</summary>
    public T[] ToArray(int offset, int count)
    {
      if (offset < 0 || offset >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || offset + count >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(count));

      var target = new T[count];
      AsReadOnlySpan().Slice(offset, count).CopyTo(target);
      return target;
    }
    /// <summary>Creates a new list from the specified array from the specified offset to the end.</summary>
    public T[] ToArray(int offset)
      => ToArray(offset, m_bufferPosition - offset);
    /// <summary>Creates a new list from the specified span builder.</summary>
    public T[] ToArray()
      => ToArray(0, m_bufferPosition);

    /// <summary>Creates a new list from the specified array from the specified offset and count.</summary>
    public System.Collections.Generic.List<T> ToList(int offset, int count)
    {
      if (offset < 0 || offset >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || offset + count >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(count));

      var target = new System.Collections.Generic.List<T>(count);
      while (count-- > 0)
        target.Add(m_buffer[offset++]);
      return target;
    }
    /// <summary>Creates a new list from the specified array from the specified offset to the end.</summary>
    public System.Collections.Generic.List<T> ToList(int offset)
      => ToList(offset, m_bufferPosition - offset);
    /// <summary>Creates a new list from the specified span builder.</summary>
    public System.Collections.Generic.List<T> ToList()
      => ToList(0, m_bufferPosition);
  }
}
