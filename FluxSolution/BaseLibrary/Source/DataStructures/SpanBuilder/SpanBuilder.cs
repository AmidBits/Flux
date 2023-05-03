using System.Linq;

namespace Flux
{
  public ref struct SpanBuilder<T>
  {
    private const int DefaultBufferSize = 16;

    private T[] m_array;

    private int m_head; // Start of buffer data.
    private int m_tail; // End of buffer data.

    private SpanBuilder(int capacity)
    {
      m_array = capacity >= 1 ? System.Buffers.ArrayPool<T>.Shared.Rent((int)capacity.ToBigInteger().RoundToPowOf2AwayFromZero(false)) : System.Array.Empty<T>();

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }
    public SpanBuilder() : this(DefaultBufferSize) { }

    public SpanBuilder(T value, int count = 1) : this() => Append(value, count);
    public SpanBuilder(System.Collections.Generic.ICollection<T> collection, int count = 1) : this(collection.Count * count) => Append(collection, count);
    public SpanBuilder(System.ReadOnlySpan<T> readOnlySpan, int count = 1) : this(0) => Append(readOnlySpan, count);

    /// <summary>Gets or sets the item at the specified item position in this instance.</summary>
    public T this[int index] { get => GetValue(index); set => SetValue(index, value); }

    /// <summary>This provides direct access to the underlying array storage for the SpanBuilder.</summary>
    /// <remarks>Use with caution!</remarks>
    public T[] Array { get => m_array; init => m_array = value; }

    /// <summary>The current total capacity of the builder buffer.</summary>
    public int Capacity => m_array.Length;

    /// <summary>The current partial capacity of the builder buffer right-side (append).</summary>
    private int CapacityAppend => m_array.Length - m_tail;

    /// <summary>The current partial capacity of the builder buffer left-side (prepend).</summary>
    private int CapacityPrepend => m_head;

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

    /// <summary>This provides partial direct access to the underlying array storage for the SpanBuilder via a <see cref="System.Span{T}"/>.</summary>
    /// <remarks>Use with caution!</remarks>
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

    /// <summary>Returns the <paramref name="source"/> with the specified <paramref name="values"/> duplicated by the specified <paramref name="count"/> throughout. If no values are specified, all characters are replicated. If the string builder is empty, nothing is replicated. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public SpanBuilder<T> Duplicate(System.ReadOnlySpan<T> values, int count, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      for (var index = 0; index < Length; index++)
      {
        var sourceValue = GetValue(index);

        if (values.Length == 0 || values.IndexOf(sourceValue, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default) > -1)
        {
          Insert(index, sourceValue, count);

          index += count;
        }
      }

      return this;
    }

    /// <summary>Grows a uniform (i.e. both left and right satisfies) buffer capacity to at least that specified.</summary>
    private void EnsureUniformCapacity(int sideCapacity)
    {
      var totalCapacity = System.Math.Max(DefaultBufferSize, sideCapacity + Length + sideCapacity);

      if (totalCapacity < Capacity) // We got overall capacity, just need to make sure we have it on both sides.
      {
        if (CapacityPrepend < sideCapacity || CapacityAppend < sideCapacity) // If any one side is below capacity, we center the content to ensure uniform capacity.
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
        var array = System.Buffers.ArrayPool<T>.Shared.Rent((int)totalCapacity.ToBigInteger().RoundToPowOf2AwayFromZero(true));

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
      if (CapacityAppend < appendCapacity) // Not enough append capacity.
      {
        var totalCapacity = System.Math.Max(DefaultBufferSize, CapacityPrepend + Length + appendCapacity);

        if (Capacity <= totalCapacity) // Not enough total capacity.
        {
          var array = System.Buffers.ArrayPool<T>.Shared.Rent((int)totalCapacity.ToBigInteger().RoundToPowOf2AwayFromZero(true));

          var head = (array.Length - Length - appendCapacity) / 2;
          var tail = head + Length;

          System.Array.Copy(m_array, m_head, array, head, Length); // Copy old content.

          System.Buffers.ArrayPool<T>.Shared.Return(m_array); // Recycle the old array.

          m_array = array;

          m_head = head;
          m_tail = tail;
        }
        else if (CapacityAppend < appendCapacity) // Enough capacity, center content if needed.
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
      if (CapacityPrepend < prependCapacity) // Not enough prepend capacity.
      {
        var totalCapacity = System.Math.Max(DefaultBufferSize, prependCapacity + Length + CapacityAppend);

        if (Capacity < totalCapacity) // Not enough total capacity, allocate new array.
        {
          var array = System.Buffers.ArrayPool<T>.Shared.Rent((int)totalCapacity.ToBigInteger().RoundToPowOf2AwayFromZero(true));

          var head = (array.Length - Length + prependCapacity) / 2;
          var tail = head + Length;

          System.Array.Copy(m_array, m_head, array, head, Length); // Copy content.

          System.Buffers.ArrayPool<T>.Shared.Return(m_array); // Recycle the old array.

          m_array = array;

          m_head = head;
          m_tail = tail;
        }
        else if (CapacityPrepend < prependCapacity) // Enough total capacity, center content if needed.
        {
          var head = (Capacity - Length) / 2;
          var tail = head + Length;

          System.Array.Copy(m_array, m_head, m_array, head, Length); // Enough prepend space, so utilize by moving content.

          m_head = head;
          m_tail = tail;
        }
      }
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

    /// <summary>Normalize the specified (or all if none specified) consecutive <paramref name="values"/> in the string normalized. Uses the specfied <paramref name="equalityComparer"/>, or default if null.</summary>
    public SpanBuilder<T> NormalizeAdjacent(System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var targetIndex = 0;
      var previous = default(T);

      for (var sourceIndex = 0; sourceIndex < Length; sourceIndex++)
      {
        var current = GetValue(sourceIndex);

        if (!equalityComparer.Equals(current, previous) || (values.Count > 0 && !values.Contains(current, equalityComparer)))
        {
          SetValue(targetIndex++, current);

          previous = current;
        }
      }

      return Remove(targetIndex, Length - targetIndex);
    }

    /// <summary>Normalize where the <paramref name="predicate"/> is satisfied using the <paramref name="normalizedValue"/> throughout the builder. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public SpanBuilder<T> NormalizeAll(T normalizedValue, System.Func<T, bool>? predicate = null)
    {
      var normalizedIndex = 0;

      var isPrevious = true; // Set to true in order for trimming to occur on the left.

      for (var index = 0; index < Length; index++)
      {
        var character = GetValue(index);

        var isCurrent = predicate?.Invoke(character) ?? true;

        if (!(isPrevious && isCurrent))
        {
          SetValue(normalizedIndex++, isCurrent ? normalizedValue : character);

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      return Remove(normalizedIndex, Length - normalizedIndex);
    }

    /// <summary>Normalize the <paramref name="normalizeValues"/> using the <paramref name="normalizedValue"/> throughout the builder. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public SpanBuilder<T> NormalizeAll(T normalizedValue, System.Collections.Generic.IList<T> normalizeValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return NormalizeAll(normalizedValue, t => normalizeValues.Contains(t, equalityComparer));
    }

    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public SpanBuilder<T> PadEven(int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true)
    {
      if (totalWidth > Length)
      {
        var quotient = System.Math.DivRem(totalWidth - Length, 2, out var remainder);

        PadLeft(Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        // The two lines below are the original right biased (always) which works great.
        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        PadRight(totalWidth, paddingRight);
      }

      return this;
    }

    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public SpanBuilder<T> PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    {
      if (totalWidth > Length)
      {
        var quotient = System.Math.DivRem(totalWidth - Length, 2, out var remainder);

        PadLeft(Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        // The two lines below are the original right biased (always) which works great.
        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        PadRight(totalWidth, paddingRight);
      }

      return this;
    }

    /// <summary>Pad on the left with the specified <paramref name="padding"/>.</summary>
    public SpanBuilder<T> PadLeft(int totalWidth, T padding)
    {
      if (Length < totalWidth)
        Insert(0, padding, totalWidth - Length);

      return this;
    }

    /// <summary>Pad on the left with the specified <paramref name="padding"/>.</summary>
    public SpanBuilder<T> PadLeft(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Insert(0, padding);

      return Remove(0, Length - totalWidth);
    }

    /// <summary>Pad on the right with the specified <paramref name="padding"/>.</summary>
    public SpanBuilder<T> PadRight(int totalWidth, T padding)
      => Append(padding, totalWidth - Length);

    /// <summary>Pad on the right with the specified <paramref name="padding"/>.</summary>
    public SpanBuilder<T> PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Append(padding);

      return Remove(totalWidth, Length - totalWidth);
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

    /// <summary>Removes the specified range of values from the builder.</summary>
    public SpanBuilder<T> Remove(int startIndex) => Remove(startIndex, Length - startIndex);

    /// <summary>Remove all items where the <paramref name="predicate"/> is satisfied.</summary>
    public SpanBuilder<T> RemoveAll(System.Func<T, bool>? predicate = null)
    {
      var removedIndex = 0;

      for (var index = 0; index < Length; index++)
      {
        var value = GetValue(index);

        if (!predicate?.Invoke(value) ?? true)
          SetValue(removedIndex++, value);
      }

      return Remove(removedIndex, Length - removedIndex);
    }

    /// <summary>Remove all <paramref name="removeValues"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public SpanBuilder<T> RemoveAll(System.Collections.Generic.IList<T> removeValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return RemoveAll(t => removeValues.Contains(t, equalityComparer));
    }

    /// <summary>Repeats the values in the builder <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Repeat(int count) => Append(new SpanBuilder<T>(AsReadOnlySpan(), count).AsSpan());

    /// <summary>Replace <paramref name="key"/> with <paramref name="value"/> if it exists at <paramref name="startAt"/> in <paramref name="source"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    /// <exception cref="System.ArgumentNullException"></exception>
    public SpanBuilder<T> ReplaceIfEqualAt(int startAt, System.ReadOnlySpan<T> key, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (AsReadOnlySpan().EqualsAt(startAt, key, 0, key.Length, equalityComparer))
      {
        Remove(startAt, key.Length);
        Insert(startAt, value);
      }

      return this;
    }

    /// <summary>Reverse all items in the range [startIndex, endIndex], in the builder.</summary>
    public SpanBuilder<T> Reverse(int startIndex, int endIndex)
    {
      if (startIndex < 0 || startIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      for (startIndex += m_head, endIndex += m_head; startIndex < endIndex; startIndex++, endIndex--)
        (m_array[endIndex], m_array[startIndex]) = (m_array[startIndex], m_array[endIndex]);

      return this;
    }

    public SpanBuilder<T> Reverse() => Reverse(0, Length - 1);

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

    public string ToString(int startIndex, int count)
    {
      var sb = new System.Text.StringBuilder();

      while (count-- > 0)
        sb.Append(GetValue(startIndex++)?.ToString() ?? string.Empty);

      return sb.ToString();
    }
    public string ToString(int startIndex) => ToString(startIndex, Length);

    #region Object overrides.
    public override string ToString() => ToString(0, Length);
    #endregion Object overrides.
  }
}
