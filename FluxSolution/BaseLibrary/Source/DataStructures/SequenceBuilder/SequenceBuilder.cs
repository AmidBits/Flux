namespace Flux
{
  public record class SequenceBuilder<T>
    : System.Collections.Generic.IEnumerable<T>
  {
    private const int DefaultBufferSize = 32;

    private int m_version;

    private T[] m_array;

    private int m_head; // Start of buffer data.
    private int m_tail; // End of buffer data.

    private SequenceBuilder(int capacity)
    {
      m_array = capacity >= 1 ? System.Buffers.ArrayPool<T>.Shared.Rent(capacity.Pow2AwayFromZero(false, out int _)) : System.Array.Empty<T>();

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }
    public SequenceBuilder() : this(DefaultBufferSize) { }

    public SequenceBuilder(T value, int count = 1) : this() => Append(value, count);
    public SequenceBuilder(System.Collections.Generic.ICollection<T> collection, int count = 1) : this(collection.Count * count) => Append(collection, count);
    //public SequenceBuilder(System.ReadOnlySpan<T> readOnlySpan, int count = 1) : this(readOnlySpan.Length * count) => Append(readOnlySpan, count);
    public SequenceBuilder(System.ReadOnlySpan<T> readOnlySpan, int count = 1) : this(0) => Append(readOnlySpan, count);

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

    ///// <summary>Grows a uniform (left and right) buffer capacity to at least that specified.</summary>
    //private void EnsureUniformCapacity(int length)
    //{
    //  if (Length + length + length < Capacity) // We got space, just need to make sure we have it on both sides.
    //  {
    //    var newHead = (m_buffer.Length - Length) / 2;
    //    var newTail = newHead + Length;

    //    System.Array.Copy(m_buffer, m_head, m_buffer, newHead, Length);

    //    if (newHead > m_head)
    //      System.Array.Clear(m_buffer, m_head, newHead - m_head);
    //    else if (newHead < m_head)
    //      System.Array.Clear(m_buffer, newTail, m_head - newHead);

    //    m_head = newHead;
    //    m_tail = newTail;
    //  }
    //  else if (m_head < length || m_tail + length > Capacity) // Not enough uniform space available.
    //  {
    //    var allocateLength = BitOps.Pow2AwayFromZero(length * 2, false, out int _);

    //    var newArray = System.Buffers.ArrayPool<T>.Shared.Rent(allocateLength + Capacity);
    //    System.Array.Clear(newArray);

    //    var newHead = (newArray.Length - Length) / 2;
    //    var newTail = newHead + Length;

    //    System.Array.Copy(m_buffer, m_head, newArray, newHead, Length);

    //    System.Buffers.ArrayPool<T>.Shared.Return(m_buffer);

    //    m_buffer = newArray;

    //    m_head = newHead;
    //    m_tail = newTail;
    //  }
    //}
    ///// <summary>Grows an append (right) buffer capacity to at least that specified.</summary>
    //private void EnsureAppendCapacity(int length)
    //{
    //  if (m_tail + length > Capacity) // Not enough append space available.
    //  {
    //    var allocateLength = BitOps.Pow2AwayFromZero(length, false, out int _);

    //    var newArray = System.Buffers.ArrayPool<T>.Shared.Rent(allocateLength + Capacity);
    //    System.Array.Clear(newArray);

    //    System.Array.Copy(m_buffer, m_head, newArray, m_head, Length);

    //    System.Buffers.ArrayPool<T>.Shared.Return(m_buffer);

    //    m_buffer = newArray;
    //  }
    //}
    ///// <summary>Grows a prepend (left) buffer capacity to at least that specified.</summary>
    //private void EnsurePrependCapacity(int length)
    //{
    //  if (m_head < length) // Not enough prepend space available.
    //  {
    //    if (Capacity - Length < length) // Not enough prepend space to shift.
    //    {
    //      var allocateLength = BitOps.Pow2AwayFromZero(length, false, out int _);

    //      var newArray = System.Buffers.ArrayPool<T>.Shared.Rent(allocateLength + Capacity);
    //      System.Array.Clear(newArray);

    //      var newHead = m_head + allocateLength;
    //      var newTail = m_tail + allocateLength;

    //      System.Array.Copy(m_buffer, m_head, newArray, newHead, Length);

    //      System.Buffers.ArrayPool<T>.Shared.Return(m_buffer);

    //      m_buffer = newArray;

    //      m_head = newHead;
    //      m_tail = newTail;
    //    }
    //    else if (Length > 0) // Enough space to shift existing content.
    //    {
    //      System.Array.Copy(m_buffer, m_head, m_buffer, m_head + length, Length); // Enough prepend space, so utilize by moving content.
    //      System.Array.Clear(m_buffer, m_head, length); // Clear prepend space.

    //      m_head += length;
    //      m_tail += length;
    //    }
    //    else // Enough space to alter head/tail.
    //    {
    //      m_head = length;
    //      m_tail = length;
    //    }
    //  }
    //}

    /// <summary>Gets or sets the item at the specified item position in this instance.</summary>
    public T this[int index] { get => GetValue(index); set => SetValue(index, value); }

    /// <summary>The current capacity of the builder.</summary>
    public int Capacity => m_array.Length;
    /// <summary>The content length of the builder.</summary>
    public int Length => m_tail - m_head;

    /// <summary>Append <paramref name="count"/> of <paramref name="value"/>.</summary>
    public SequenceBuilder<T> Append(T value, int count = 1)
    {
      m_version++;

      EnsureAppendCapacity(count);

      while (count-- > 0)
        m_array[m_tail++] = value;

      return this;
    }
    /// <summary>Append a <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    public SequenceBuilder<T> Append(System.Collections.Generic.ICollection<T> collection, int count = 1)
    {
      m_version++;

      EnsureAppendCapacity(collection.Count * count);

      while (count-- > 0)
      {
        collection.CopyTo(m_array, m_tail);

        m_tail += collection.Count;
      }

      return this;
    }
    /// <summary>Append <paramref name="count"/> of <paramref name="values"/>.</summary>
    public SequenceBuilder<T> Append(System.ReadOnlySpan<T> values, int count = 1)
    {
      m_version++;

      EnsureAppendCapacity(values.Length * count);

      while (count-- > 0)
      {
        values.CopyTo(m_array, m_tail);

        m_tail += values.Length;
      }

      return this;
    }

    /// <summary>Creates a non-allocating <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public System.ReadOnlySpan<T> AsReadOnlySpan()
      => new(m_array, m_head, m_tail - m_head);

    /// <summary>Creates a non-allocating <see cref="System.Span{T}"/>.</summary>
    public System.Span<T> AsSpan()
      => new(m_array, m_head, m_tail - m_head);

    /// <summary>Remove all values from the builder.</summary>
    public SequenceBuilder<T> Clear()
    {
      m_version++;

      System.Array.Clear(m_array);

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;

      return this;
    }

    public SequenceBuilder<T> CopyOver(int fromIndex, int toIndex, int count)
    {
      if (fromIndex < 0 || fromIndex > Length - 1) throw new System.ArgumentOutOfRangeException(nameof(fromIndex));
      if (toIndex < 0 || toIndex > Length - 1) throw new System.ArgumentOutOfRangeException(nameof(fromIndex));
      if (count < 0 || fromIndex + count > Length || toIndex + count > Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      for (; count > 0; count--)
        this[toIndex++] = this[fromIndex++];

      return this;
    }

    //public void CopyTo(int sourceStartIndex, System.Collections.Generic.IList<T> target, int targetStartIndex, int count)
    //{
    //  if (sourceStartIndex < 0 || sourceStartIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(sourceStartIndex));
    //  if (target is null) throw new System.ArgumentNullException(nameof(target));
    //  if (targetStartIndex < 0 || targetStartIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(targetStartIndex));
    //  if (count <= 0 || (sourceStartIndex + count) > Length || (targetStartIndex + count) > target.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

    //  while (count-- > 0)
    //    target[targetStartIndex++] = this[sourceStartIndex++];
    //}

    /// <summary>Returns the <paramref name="source"/> with the specified <paramref name="values"/> duplicated by the specified <paramref name="count"/> throughout. If no values are specified, all characters are replicated. If the string builder is empty, nothing is replicated. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public SequenceBuilder<T> Duplicate(System.ReadOnlySpan<T> values, int count, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
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

    /// <summary>Gets the value at the specified index.</summary>
    public T GetValue(int index)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      return m_array[m_head + index];
    }

    /// <summary>Insert <paramref name="count"/> of <paramref name="value"/> starting at <paramref name="startAt"/>.</summary>
    public SequenceBuilder<T> Insert(int startAt, T value, int count = 1)
    {
      m_version++;

      EnsureUniformCapacity(count);

      System.Array.Copy(m_array, m_head, m_array, m_head - count, startAt); // Copy left portion of content.

      startAt += m_head;

      System.Array.Fill(m_array, value, startAt - count, count);

      m_head -= count;

      return this;
    }
    /// <summary>Insert <paramref name="count"/> of <paramref name="collection"/> starting at <paramref name="startAt"/>.</summary>
    public SequenceBuilder<T> Insert(int startAt, System.Collections.Generic.ICollection<T> collection, int count = 1)
    {
      m_version++;

      var totalLength = collection.Count * count;

      EnsureUniformCapacity(totalLength);

      System.Array.Copy(m_array, m_head, m_array, m_head - totalLength, startAt); // Copy left portion of content.

      startAt += m_head;
      m_head -= totalLength;

      while (count-- > 0)
        collection.CopyTo(m_array, startAt -= collection.Count);

      return this;
    }
    /// <summary>Insert <paramref name="count"/> of <paramref name="readOnlySpan"/> starting at <paramref name="startAt"/>.</summary>
    public SequenceBuilder<T> Insert(int startAt, System.ReadOnlySpan<T> readOnlySpan, int count = 1)
    {
      m_version++;

      var totalLength = readOnlySpan.Length * count;

      EnsureUniformCapacity(totalLength);

      System.Array.Copy(m_array, m_head, m_array, m_head - totalLength, startAt); // Copy left portion of content.

      startAt += m_head;

      while (count-- > 0)
        readOnlySpan.CopyTo(m_array, startAt -= readOnlySpan.Length);

      m_head -= totalLength;

      return this;
    }

    /// <summary>Determines whether the string is a palindrome.</summary>
    public bool IsPalindrome(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (int indexL = 0, indexR = Length - 1; indexL < indexR; indexL++, indexR--)
        if (!equalityComparer.Equals(GetValue(indexL), GetValue(indexR)))
          return false;

      return true;
    }

    /// <summary>Normalize the specified (or all if none specified) consecutive <paramref name="values"/> in the string normalized. Uses the specfied <paramref name="equalityComparer"/>, or default if null.</summary>
    public SequenceBuilder<T> NormalizeAdjacent(System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      m_version++;

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
    public SequenceBuilder<T> NormalizeAll(T normalizedValue, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var normalizedIndex = 0;

      var isPrevious = true; // Set to true in order for trimming to occur on the left.

      for (var index = 0; index < Length; index++)
      {
        var character = GetValue(index);

        var isCurrent = predicate(character);

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
    public SequenceBuilder<T> NormalizeAll(T normalizedValue, System.Collections.Generic.IList<T> normalizeValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return NormalizeAll(normalizedValue, t => normalizeValues.Contains(t, equalityComparer));
    }

    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public SequenceBuilder<T> PadEven(int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true)
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
    public SequenceBuilder<T> PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
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
    public SequenceBuilder<T> PadLeft(int totalWidth, T padding)
    {
      if (Length < totalWidth)
        Insert(0, padding, totalWidth - Length);

      return this;
    }
    /// <summary>Pad on the left with the specified <paramref name="padding"/>.</summary>
    public SequenceBuilder<T> PadLeft(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Insert(0, padding);

      return Remove(0, Length - totalWidth);
    }

    /// <summary>Pad on the right with the specified <paramref name="padding"/>.</summary>
    public SequenceBuilder<T> PadRight(int totalWidth, T padding)
      => Append(padding, totalWidth - Length);
    /// <summary>Pad on the right with the specified <paramref name="padding"/>.</summary>
    public SequenceBuilder<T> PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Append(padding);

      return Remove(totalWidth, Length - totalWidth);
    }

    /// <summary>Prepends with a <paramref name="value"/>, <paramref name="count"/> times.</summary>
    public SequenceBuilder<T> Prepend(T value, int count = 1)
    {
      m_version++;

      EnsurePrependCapacity(count);

      while (count-- > 0)
        m_array[--m_head] = value;

      return this;
    }
    /// <summary>Prepends with the <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    public SequenceBuilder<T> Prepend(System.Collections.Generic.ICollection<T> collection, int count = 1)
    {
      m_version++;

      EnsurePrependCapacity(collection.Count);

      while (count-- > 0)
        collection.CopyTo(m_array, m_head -= collection.Count);

      return this;
    }
    /// <summary>Prepends with the <paramref name="readOnlySpan"/>, <paramref name="count"/> times.</summary>
    public SequenceBuilder<T> Prepend(System.ReadOnlySpan<T> readOnlySpan, int count = 1)
    {
      m_version++;

      EnsurePrependCapacity(readOnlySpan.Length * count);

      while (count-- > 0)
        readOnlySpan.CopyTo(m_array, m_head -= readOnlySpan.Length);

      return this;
    }

    /// <summary>Removes the specified range of values from the builder.</summary>
    public SequenceBuilder<T> Remove(int startAt, int count)
    {
      m_version++;

      startAt += m_head;

      if (m_head <= m_array.Length - m_tail) // Shrink from start.
      {
        System.Array.Copy(m_array, m_head, m_array, m_head + count, startAt - m_head);
        System.Array.Fill(m_array, default, m_head, count);
        m_head += count;
      }
      else // Otherwise shrink from end.
      {
        System.Array.Copy(m_array, startAt + count, m_array, startAt, m_tail - startAt - count);
        m_tail -= count;
        System.Array.Fill(m_array, default, m_tail, count);
      }

      return this;
    }

    /// <summary>Remove all items where the <paramref name="predicate"/> is satisfied.</summary>
    public SequenceBuilder<T> RemoveAll(System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var removedIndex = 0;

      for (var index = 0; index < Length; index++)
      {
        var value = GetValue(index);

        if (!predicate(value))
          SetValue(removedIndex++, value);
      }

      return Remove(removedIndex, Length - removedIndex);
    }
    /// <summary>Remove all <paramref name="removeValues"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public SequenceBuilder<T> RemoveAll(System.Collections.Generic.IList<T> removeValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return RemoveAll(t => removeValues.Contains(t, equalityComparer));
    }

    /// <summary>Repeats the values in the builder <paramref name="count"/> times.</summary>
    public SequenceBuilder<T> Repeat(int count)
    {
      m_version++;

      Append(new SequenceBuilder<T>().Append(AsReadOnlySpan(), count).AsSpan());

      return this;
    }

    /// <summary>Replace all values using the specified replacement selector.</summary>
    public SequenceBuilder<T> ReplaceAll(System.Func<T, T> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = Length - 1; index >= 0; index--)
        SetValue(index, replacementSelector(GetValue(index)));

      return this;
    }
    /// <summary>Replace all values with <paramref name="replacement"/> where the <paramref name="predicate"/> is satisfied.</summary>
    public SequenceBuilder<T> ReplaceAll(T replacement, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = Length - 1; index >= 0; index--)
        if (predicate(GetValue(index)))
          SetValue(index, replacement);

      return this;
    }
    /// <summary>Replace all <paramref name="replaceValues"/> with <paramref name="replacement"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public SequenceBuilder<T> ReplaceAll(T replacement, System.Collections.Generic.IList<T> replaceValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return ReplaceAll(replacement, t => replaceValues.Any(r => equalityComparer.Equals(r, t)));
    }

    /// <summary>Replace <paramref name="key"/> with <paramref name="value"/> if it exists at <paramref name="startAt"/> in <paramref name="source"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    /// <exception cref="System.ArgumentNullException"></exception>
    public SequenceBuilder<T> ReplaceIfEqualAt(int startAt, System.ReadOnlySpan<T> key, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (AsReadOnlySpan().EqualsAt(startAt, key, 0, key.Length, equalityComparer))
      {
        Remove(startAt, key.Length);
        Insert(startAt, value);
      }

      return this;
    }

    /// <summary>Reverse all items in the range [startIndex, endIndex], in the builder.</summary>
    public SequenceBuilder<T> Reverse(int startIndex, int endIndex)
    {
      if (startIndex < 0 || startIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      while (startIndex < endIndex)
        Swap(startIndex++, endIndex--);

      return this;
    }

    public void SetValue(int index, T value)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      m_version++;

      m_array[m_head + index] = value;
    }

    /// <summary>Shuffle all values in the builder. Uses the specified <paramref name="rng"/>, or default if null.</summary>
    public SequenceBuilder<T> Shuffle(System.Random? rng)
    {
      rng ??= new System.Random();

      for (var index = Length - 1; index > 0; index--) // Shuffle each element by swapping with a random element of a lower index.
        Swap(index, rng.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.

      return this;
    }

    /// <summary>Creates a sequence of substrings, as a split of the StringBuilder content based on the characters in an array. There is no change to the StringBuilder content.</summary>
    public System.Collections.Generic.IEnumerable<SequenceBuilder<T>> Split(System.StringSplitOptions options, System.Collections.Generic.IList<T> separators, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var startIndex = 0;

      for (var index = startIndex; index < Length; index++)
      {
        if (separators.Any(c => equalityComparer.Equals(c, GetValue(index))))
        {
          if (index != startIndex || options != System.StringSplitOptions.RemoveEmptyEntries)
            yield return AsReadOnlySpan().Slice(startIndex, index - startIndex).ToSequenceBuilder();

          startIndex = index + 1;
        }
      }

      if (startIndex < Length)
        yield return AsReadOnlySpan().Slice(startIndex, Length - startIndex).ToSequenceBuilder();
    }

    /// <summary>Swap two values by the specified indices.</summary>
    public SequenceBuilder<T> Swap(int indexA, int indexB)
    {
      if (indexA != indexB)
      {
        m_version++;

        var a = GetValue(indexA);
        var b = GetValue(indexB);

        SetValue(indexA, b);
        SetValue(indexB, a);
      }

      return this;
    }

    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
    {
      for (var index = 0; index < Length; index++)
        yield return GetValue(index);
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    //public string ToString(int startAt) => AsReadOnlySpan().ToString(startAt);
    //public string ToString(int startAt, int count) => AsReadOnlySpan().ToString(startAt, count - startAt);

    //public System.Text.StringBuilder ToStringBuilder(int startAt, int count)
    //{
    //  var sb = new System.Text.StringBuilder(count);

    //  for (var index = startAt; count > 0; index++)
    //    sb.Append(this[index]);

    //  return sb;
    //}
    //public System.Text.StringBuilder ToStringBuilder(int startAt) => ToStringBuilder(startAt, Length);
    //public System.Text.StringBuilder ToStringBuilder() => ToStringBuilder(0, Length);

    #region Object overrides.
    public override string ToString() => AsReadOnlySpan().ToString(0, Length);
    #endregion Object overrides.
  }
}
