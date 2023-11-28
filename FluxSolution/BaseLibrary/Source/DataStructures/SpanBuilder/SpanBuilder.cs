namespace Flux
{
  public ref struct SpanBuilder<T>
  //where T : notnull
  {
    private const int DefaultBufferSize = 16;

    private T[] m_array;

    private int m_head; // Start of buffer data.
    private int m_tail; // End of buffer data.

    //private System.Collections.Generic.IEqualityComparer<T> m_equalityComparer = System.Collections.Generic.EqualityComparer<T>.Default;

    private SpanBuilder(int capacity)
    {
      m_array = capacity >= 1 ? System.Buffers.ArrayPool<T>.Shared.Rent((int)capacity.ToBigInteger().RoundToPow2(false, RoundingMode.AwayFromZero, out var _, out var _)) : System.Array.Empty<T>();

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }
    public SpanBuilder() : this(DefaultBufferSize) { }

    public SpanBuilder(T value, int count = 1) : this() => Append(value, count);
    public SpanBuilder(System.Collections.Generic.ICollection<T> collection, int count = 1) : this(collection.Count * count) => Append(collection, count);
    public SpanBuilder(System.ReadOnlySpan<T> readOnlySpan, int count = 1) : this(0) => Append(readOnlySpan, count);

    /// <summary>Gets or sets the item at the specified item position in this instance.</summary>
    public readonly T this[int index]
    {
      get => (index >= 0 && index < Length) ? m_array[m_head + index] : throw new System.ArgumentOutOfRangeException(nameof(index));
      set => m_array[m_head + index] = (index >= 0 && index < Length) ? value : throw new System.ArgumentOutOfRangeException(nameof(index));
    }

    /// <summary>This provides direct access to the underlying array storage for the SpanBuilder.</summary>
    /// <remarks>Use with caution!</remarks>
    public T[] InternalArray { readonly get => m_array; init => m_array = value; }

    /// <summary>The current total capacity of the builder buffer.</summary>
    public readonly int Capacity => m_array.Length;
    /// <summary>The current partial capacity of the builder buffer right-side (append).</summary>
    private readonly int CapacityAppend => m_array.Length - m_tail;
    /// <summary>The current partial capacity of the builder buffer left-side (prepend).</summary>
    private readonly int CapacityPrepend => m_head;

    public readonly int Length => m_tail - m_head;

    /// <summary>Append <paramref name="count"/> of <paramref name="value"/>.</summary>
    public SpanBuilder<T> Append(T value, int count)
    {
      EnsureAppendCapacity(count);

      while (count-- > 0)
        m_array[m_tail++] = value;

      return this;
    }
    /// <summary>Append a <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Append(System.Collections.Generic.ICollection<T> collection, int count)
    {
      EnsureAppendCapacity(collection.Count * count);

      while (count-- > 0)
      {
        collection.CopyTo(m_array, m_tail);

        m_tail += collection.Count;
      }

      return this;
    }
    /// <summary>Append a <paramref name=sequence"/>, <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Append(System.Collections.Generic.IEnumerable<T> sequence, int count)
      => Append(sequence.ToList(), count);
    /// <summary>Append <paramref name="count"/> of <paramref name="values"/>.</summary>
    public SpanBuilder<T> Append(System.ReadOnlySpan<T> values, int count)
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
    public readonly System.ReadOnlySpan<T> AsReadOnlySpan()
      => new(m_array, m_head, m_tail - m_head);
    /// <summary>Creates a non-allocating <see cref="System.ReadOnlySpan{T}"/> starting at <paramref name="startIndex"/>.</summary>
    public readonly System.ReadOnlySpan<T> AsReadOnlySpan(int startIndex)
      => AsReadOnlySpan()[startIndex..];
    /// <summary>Creates a non-allocating <see cref="System.ReadOnlySpan{T}"/> starting at <paramref name="startIndex"/> and <paramref name="count"/> elements.</summary>
    public readonly System.ReadOnlySpan<T> AsReadOnlySpan(int startIndex, int count)
      => AsReadOnlySpan()[startIndex..count];

    /// <summary>Creates a non-allocating <see cref="System.Span{T}"/>. This provides partial direct access to the underlying array storage for the SpanBuilder.</summary>
    /// <remarks>Use with caution!</remarks>
    public readonly System.Span<T> AsSpan()
      => new(m_array, m_head, m_tail - m_head);
    /// <summary>Creates a non-allocating <see cref="System.Span{T}"/> starting at <paramref name="startIndex"/>. This provides partial direct access to the underlying array storage for the SpanBuilder.</summary>
    /// <remarks>Use with caution!</remarks>
    public readonly System.Span<T> AsSpan(int startIndex)
      => AsSpan()[startIndex..];
    /// <summary>Creates a non-allocating <see cref="System.Span{T}"/> starting at <paramref name="startIndex"/> with <paramref name="count"/> elements. This provides partial direct access to the underlying array storage for the SpanBuilder.</summary>
    /// <remarks>Use with caution!</remarks>
    public readonly System.Span<T> AsSpan(int startIndex, int count)
      => AsSpan()[startIndex..count];

    public void Clear()
    {
      System.Array.Clear(m_array);

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }

    //public bool Contains(T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  for (var index = m_head; index < m_tail; index++)
    //    if (equalityComparer.Equals(m_array[index], value))
    //      return true;

    //  return false;
    //}

    /// <summary>Internally copy <paramref name="count"/> values from <paramref name="sourceIndex"/> to <paramref name="targetIndex"/>.</summary>
    /// <param name="sourceIndex"></param>
    /// <param name="targetIndex"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public readonly SpanBuilder<T> Copy(int sourceIndex, int targetIndex, int count)
    {
      if (sourceIndex < 0 || sourceIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex));
      if (targetIndex < 0 || targetIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(targetIndex));
      if (count < 0 || sourceIndex + count > Length || targetIndex + count > Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      sourceIndex += m_head;
      targetIndex += m_head;

      while (count-- > 0)
        m_array[targetIndex++] = m_array[sourceIndex++];

      return this;
    }

    /// <summary>Copies the entire <see cref="System.Collections.Generic.List{T}"/> to a compatible one-dimensional <paramref name="array"/>, starting at the specified <paramref name="startIndex"/> of the target array.</summary>
    /// <param name="array"></param>
    /// <param name="startIndex"></param>
    public readonly void CopyTo(T[] array, int startIndex)
      => System.Array.Copy(m_array, m_head, array, startIndex, Length);

    public readonly void CopyTo(int sourceIndex, System.Collections.Generic.IList<T> target, int targetIndex, int count)
    {
      if (sourceIndex < 0 || sourceIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex));
      if (targetIndex < 0 || targetIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(targetIndex));
      if (count < 0 || sourceIndex + count > Length || targetIndex + count > Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      sourceIndex += m_head;

      while (count-- > 0)
        target[targetIndex++] = this[sourceIndex++];
    }

    public readonly int Count(T target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var count = 0;

      for (var i = m_head; i < m_tail; i++)
        if (equalityComparer.Equals(target, m_array[i]))
          count++;

      return count;
    }

    /// <summary>Duplicates the specified <paramref name="values"/>, <paramref name="count"/> times, throughout. If no values are specified, all characters are duplicated <paramref name="count"/> times. If the string builder is empty, nothing is duplicated. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public SpanBuilder<T> Duplicate(System.ReadOnlySpan<T> values, int count, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < Length; index++)
      {
        var sourceValue = this[index];

        if (values.Length == 0 || values.IndexOf(sourceValue, equalityComparer) > -1)
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
        var array = System.Buffers.ArrayPool<T>.Shared.Rent((int)totalCapacity.ToBigInteger().RoundToPow2(true, RoundingMode.AwayFromZero, out var _, out var _));

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
          var array = System.Buffers.ArrayPool<T>.Shared.Rent((int)totalCapacity.ToBigInteger().RoundToPow2(true, RoundingMode.AwayFromZero, out var _, out var _));

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
          var array = System.Buffers.ArrayPool<T>.Shared.Rent((int)totalCapacity.ToBigInteger().RoundToPow2(true, RoundingMode.AwayFromZero, out var _, out var _));

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

    /// <summary>Insert <paramref name="count"/> of <paramref name="value"/> starting at <paramref name="startIndex"/>.</summary>
    public SpanBuilder<T> Insert(int startIndex, T value, int count)
    {
      EnsureUniformCapacity(count);

      System.Array.Copy(m_array, m_head, m_array, m_head - count, startIndex); // Copy left portion of content.

      startIndex += m_head;

      System.Array.Fill(m_array, value, startIndex - count, count);

      m_head -= count;

      return this;
    }
    /// <summary>Insert <paramref name="count"/> of <paramref name="collection"/> starting at <paramref name="startIndex"/>.</summary>
    public SpanBuilder<T> Insert(int startIndex, System.Collections.Generic.ICollection<T> collection, int count)
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
    public SpanBuilder<T> Insert(int startIndex, System.ReadOnlySpan<T> span, int count)
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

    /// <summary>Repeats the values in the builder <paramref name="count"/> times.</summary>
    public SpanBuilder<T> InsertEvery(T insert, int interval)
    {
      for (var index = Length - 1; index >= 0; index--)
        if (index > 0 && index % interval == interval - 1)
          Insert(index, insert, 1);

      return this;
    }

    /// <summary>Normalize (trim down) any consecutive <paramref name="values"/> (or all items, if none specified) that occur more than <paramref name="maxAdjacentCount"/> in the <see cref="SpanBuilder{T}"/>. Uses the specfied <paramref name="equalityComparer"/>.</summary>
    public SpanBuilder<T> NormalizeAdjacent(int maxAdjacentCount, System.ReadOnlySpan<T> values, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (maxAdjacentCount < 1) throw new System.ArgumentNullException(nameof(maxAdjacentCount));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var normalizedMark = m_head;

      T previous = default!;
      var adjacentLength = 1;

      for (var i = m_head; i < m_tail; i++)
      {
        var current = m_array[i];

        var isEqual = values.Length > 0 // Use span or just items?
          ? (values.IndexOf(current, equalityComparer) > -1 && values.IndexOf(previous, equalityComparer) > -1) // Is both current and previous in items?
          : equalityComparer.Equals(current, previous); // Are current and previous items equal?

        if (!isEqual || adjacentLength < maxAdjacentCount)
        {
          m_array[normalizedMark++] = current;

          previous = current;
        }

        adjacentLength = !isEqual ? 1 : adjacentLength + 1;
      }

      m_tail = normalizedMark;

      return this;
    }

    /// <summary>Normalize all consecutive items where the <paramref name="replacementSelector"/> returns any element but default(<typeparamref name="T"/>) with the element returned by <paramref name="replacementSelector"/>.</summary>
    /// <example>"".NormalizeAll(char.IsWhiteSpace, ' ');</example>
    /// <example>"".NormalizeAll(c => c == ' ', ' ');</example>
    /// <remarks>Normalizing (here) means removing leading/trailing (either end of the <see cref="SpanBuilder{T}"/>) and replacing consecutive characters within the <see cref="SpanBuilder{T}"/> with <paramref name="replacement"/>. No replacements are performed if elements are on left or right edge.</remarks>
    public SpanBuilder<T> NormalizeAll(System.Func<T, bool> predicate, System.Func<T, T> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      var normalizedMark = m_head;

      var isPrevious = true;

      for (var i = m_head; i < m_tail; i++)
      {
        var c = m_array[i];

        var isCurrent = predicate(c);

        if (!(isPrevious && isCurrent))
        {
          m_array[normalizedMark++] = isCurrent ? replacementSelector(c) : c;

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedMark--;

      m_tail = normalizedMark;

      return this;
    }

    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public SpanBuilder<T> PadEven(int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true)
    {
      if (totalWidth > Length)
      {
        var quotient = System.Math.DivRem(totalWidth - Length, 2, out var remainder);

        PadLeft(Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
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
        PadRight(totalWidth, paddingRight);
      }

      return this;
    }

    /// <summary>Pads this StringBuilder on the left with the specified padding character.</summary>
    public SpanBuilder<T> PadLeft(int totalWidth, T padding)
      => Insert(0, padding, totalWidth - Length);
    /// <summary>Pads this StringBuilder on the left with the specified padding string.</summary>
    public SpanBuilder<T> PadLeft(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      Insert(0, padding, (totalWidth - Length) / padding.Length + 1);
      Remove(0, Length - totalWidth);

      return this;
    }

    /// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
    public SpanBuilder<T> PadRight(int totalWidth, T padding)
      => Append(padding, totalWidth - Length);
    /// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
    public SpanBuilder<T> PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      Append(padding, (totalWidth - Length) / padding.Length + 1);
      Remove(totalWidth, Length - totalWidth);

      return this;
    }

    /// <summary>Prepends with a <paramref name="value"/>, <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Prepend(T value, int count)
    {
      EnsurePrependCapacity(count);

      while (count-- > 0)
        m_array[--m_head] = value;

      return this;
    }
    /// <summary>Prepends with the <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Prepend(System.Collections.Generic.ICollection<T> collection, int count)
    {
      EnsurePrependCapacity(collection.Count);

      while (count-- > 0)
        collection.CopyTo(m_array, m_head -= collection.Count);

      return this;
    }
    /// <summary>Prepends with the <paramref name="span"/>, <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Prepend(System.ReadOnlySpan<T> span, int count)
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

    /// <summary>Remove all characters satisfying the predicate from the string.</summary>
    /// <example>"".RemoveAll(char.IsWhiteSpace);</example>
    /// <example>"".RemoveAll(c => c == ' ');</example>
    public SpanBuilder<T> RemoveAll(System.Func<T, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      var removedMark = m_head; // This represents the "lag" mark, and becomes the new m_tail in the end.

      for (var i = m_head; i < m_tail; i++)
        if (m_array[i] is var c && !predicate(c))
          m_array[removedMark++] = c;

      m_tail = removedMark;

      return this;
    }

    public SpanBuilder<T> RemoveEvery(int interval)
    {
      var removedMark = m_head;

      for (var i = m_head; i < m_tail; i++)
        if ((i - m_head) % interval < interval - 1)
          m_array[removedMark++] = m_array[i];

      m_tail = removedMark;

      return this;
    }

    /// <summary>Repeats the values in the builder <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Repeat(int count)
      => Append(new SpanBuilder<T>(AsReadOnlySpan(), count).AsReadOnlySpan(), 1);

    /// <summary>Replace all characters satisfying the <paramref name="predicate"/> with the result of the <paramref name="replacementSelector"/>.</summary>
    public SpanBuilder<T> ReplaceAll(System.Func<T, bool> predicate, System.Func<T, System.Collections.Generic.ICollection<T>> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      for (var index = Length - 1; index >= 0; index--)
        if (this[index] is var c && predicate(c))
          Remove(index, 1).Insert(index, replacementSelector(c), 1);

      return this;
    }

    public SpanBuilder<T> ReplaceIfEqualAt(int startAt, System.ReadOnlySpan<T> find, System.ReadOnlySpan<T> replacement, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      if (AsReadOnlySpan().EqualsAt(startAt, find, equalityComparer))
      {
        Remove(startAt, find.Length);
        Insert(startAt, replacement, 1);
      }

      return this;
    }

    /// <summary>Swap two values at the specified indices.</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public readonly SpanBuilder<T> Swap(int indexA, int indexB)
    {
      if (indexA != indexB)
      {
        var vA = this[indexA];
        var vB = this[indexB];

        this[indexA] = vB;
        this[indexB] = vA;
      }

      return this;
    }

    /// <summary>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the beginning of the <see cref="SpanBuilder{T}"/>.</summary>
    public SpanBuilder<T> TrimLeft(System.Func<T, bool> predicate)
    {
      while (m_head < m_tail)
        if (predicate(m_array[m_head]))
          m_head++;
        else
          break;

      return this;
    }

    /// <summary>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the end of the <see cref="SpanBuilder{T}"/>.</summary>
    public SpanBuilder<T> TrimRight(System.Func<T, bool> predicate)
    {
      while (m_tail > m_head)
        if (predicate(m_array[m_tail - 1]))
          m_tail--;
        else
          break;

      return this;
    }

    public string ToString(int startIndex, int count)
    {
      var sb = new System.Text.StringBuilder();

      while (count-- > 0)
        sb.Append($"{this[startIndex++]}");

      return sb.ToString();
    }

    public string ToString(int startIndex) => ToString(startIndex, Length);

    public override string ToString() => this.ToString(0, Length);
  }
}
