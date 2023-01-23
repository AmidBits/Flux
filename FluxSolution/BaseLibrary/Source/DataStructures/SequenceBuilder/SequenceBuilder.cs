namespace Flux
{
  public record class SequenceBuilder<T>
  {
    private const int DefaultBufferSize = 32;

    private int m_version;

    private T[] m_buffer;

    private int m_head; // Start of buffer data.
    private int m_tail; // End of buffer data.

    private SequenceBuilder(int capacity)
    {
      var powerOf2Capacity = capacity.Pow2AwayFromZero(false, out int _);

      m_buffer = System.Buffers.ArrayPool<T>.Shared.Rent(powerOf2Capacity);

      m_head = m_buffer.Length / 2;
      m_tail = m_buffer.Length / 2;
    }
    public SequenceBuilder() : this(DefaultBufferSize) { }

    public SequenceBuilder(T value) : this() => Append(value);
    public SequenceBuilder(System.ReadOnlySpan<T> readOnlySpan) : this(readOnlySpan.Length) => Append(readOnlySpan);
    public SequenceBuilder(System.Span<T> span) : this(span.Length) => Append(span);
    public SequenceBuilder(System.Collections.Generic.IEnumerable<T> collection) : this() => Append(collection);

    /// <summary>Grows a uniform (left and right) buffer capacity to at least that specified.</summary>
    private void EnsureUniformSpace(int length)
    {
      if (Length + length + length < Capacity) // We got space, just need to make sure we have it on both sides.
      {
        var newHead = (m_buffer.Length - Length) / 2;
        var newTail = newHead + Length;

        System.Array.Copy(m_buffer, m_head, m_buffer, newHead, Length);

        if (newHead > m_head)
          System.Array.Clear(m_buffer, m_head, newHead - m_head);
        else if (newHead < m_head)
          System.Array.Clear(m_buffer, newTail, m_head - newHead);

        m_head = newHead;
        m_tail = newTail;
      }
      else if (m_head < length || m_tail + length > Capacity) // Not enough uniform space available.
      {
        var allocateLength = BitOps.Pow2AwayFromZero(length * 2, false, out int _);

        var newArray = System.Buffers.ArrayPool<T>.Shared.Rent(allocateLength + Capacity);
        System.Array.Clear(newArray);

        var newHead = (newArray.Length - Length) / 2;
        var newTail = newHead + Length;

        System.Array.Copy(m_buffer, m_head, newArray, newHead, Length);

        System.Buffers.ArrayPool<T>.Shared.Return(m_buffer);

        m_buffer = newArray;

        m_head = newHead;
        m_tail = newTail;
      }
    }
    /// <summary>Grows an append (right) buffer capacity to at least that specified.</summary>
    private void EnsureAppendSpace(int length)
    {
      if (m_tail + length > Capacity) // Not enough append space available.
      {
        var allocateLength = BitOps.Pow2AwayFromZero(length, false, out int _);

        var newArray = System.Buffers.ArrayPool<T>.Shared.Rent(allocateLength + Capacity);
        System.Array.Clear(newArray);

        System.Array.Copy(m_buffer, m_head, newArray, m_head, Length);

        System.Buffers.ArrayPool<T>.Shared.Return(m_buffer);

        m_buffer = newArray;
      }
    }
    /// <summary>Grows a prepend (left) buffer capacity to at least that specified.</summary>
    private void EnsurePrependSpace(int length)
    {
      if (m_head < length) // Not enough prepend space available.
      {
        if (Capacity - Length < length) // Not enough prepend space to shift.
        {
          var allocateLength = BitOps.Pow2AwayFromZero(length, false, out int _);

          var newArray = System.Buffers.ArrayPool<T>.Shared.Rent(allocateLength + Capacity);
          System.Array.Clear(newArray);

          var newHead = m_head + allocateLength;
          var newTail = m_tail + allocateLength;

          System.Array.Copy(m_buffer, m_head, newArray, newHead, Length);

          System.Buffers.ArrayPool<T>.Shared.Return(m_buffer);

          m_buffer = newArray;

          m_head = newHead;
          m_tail = newTail;
        }
        else if (Length > 0) // Enough space to shift existing content.
        {
          System.Array.Copy(m_buffer, m_head, m_buffer, m_head + length, Length); // Enough prepend space, so utilize by moving content.
          System.Array.Clear(m_buffer, m_head, length); // Clear prepend space.

          m_head += length;
          m_tail += length;
        }
        else // Enough space to alter head/tail.
        {
          m_head = length;
          m_tail = length;
        }
      }
    }

    /// <summary>Gets or sets the item at the specified item position in this instance.</summary>
    public T this[int index] { get => GetValue(index); set => SetValue(index, value); }

    /// <summary>The current capacity of the builder.</summary>
    public int Capacity => m_buffer.Length;
    /// <summary>The content length of the builder.</summary>
    public int Length => m_tail - m_head;

    /// <summary>Append <paramref name="count"/> of <paramref name="value"/>.</summary>
    public SequenceBuilder<T> Append(T value, int count = 1)
    {
      m_version++;

      EnsureAppendSpace(count);

      while (count-- > 0)
        m_buffer[m_tail++] = value;

      return this;
    }
    /// <summary>Append the <paramref name="values"/>.</summary>
    public SequenceBuilder<T> Append(System.ReadOnlySpan<T> values)
    {
      m_version++;

      EnsureAppendSpace(values.Length);

      values.CopyTo(m_buffer, m_tail);

      m_tail += values.Length;

      return this;
    }
    /// <summary>Append a <paramref name="collection"/>.</summary>
    public SequenceBuilder<T> Append(System.Collections.Generic.IEnumerable<T> collection)
    {
      m_version++;

      foreach (var item in collection)
        Append(item);

      return this;
    }

    /// <summary>Creates a non-allocating <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public System.ReadOnlySpan<T> AsReadOnlySpan()
      => new(m_buffer, m_head, m_tail - m_head);

    /// <summary>Creates a non-allocating <see cref="System.Span{T}"/>.</summary>
    public System.Span<T> AsSpan()
      => new(m_buffer, m_head, m_tail - m_head);

    /// <summary>Remove all values from the builder.</summary>
    public SequenceBuilder<T> Clear()
    {
      m_version++;

      System.Array.Clear(m_buffer);

      m_head = m_buffer.Length / 2;
      m_tail = m_buffer.Length / 2;

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

    ///// <summary>Yields the number of characters that the source and the target have in common at the end.</summary>
    //public int CountEqualAtEnd(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  var sourceIndex = Length;
    //  var targetIndex = target.Length;

    //  var minLength = System.Math.Min(sourceIndex, targetIndex);

    //  for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
    //    if (!equalityComparer.Equals(this[sourceIndex], target[targetIndex]))
    //      return atEnd;

    //  return minLength;
    //}

    ///// <summary>Yields the number of characters that the source and the target have in common from the start.</summary>
    //public int CountEqualAtStart(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  var minLength = System.Math.Min(Length, target.Length);

    //  var index = 0;
    //  while (index < minLength && equalityComparer.Equals(this[index], target[index]))
    //    index++;
    //  return index;
    //}

    ///// <summary>Indicates whether <paramref name="source"/> ends with <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or the default if null.</summary>
    //public bool EndsWith(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  var sourceIndex = Length;
    //  var targetIndex = target.Length;

    //  if (sourceIndex < targetIndex)
    //    return false;

    //  while (--sourceIndex >= 0 && --targetIndex >= 0)
    //    if (!equalityComparer.Equals(this[sourceIndex], target[targetIndex]))
    //      return false;

    //  return true;
    //}

    ///// <summary>Returns whether the specified part of the target is found at the specified index in the string, using the specified comparer.</summary>
    //public bool EqualsAt(int index, System.ReadOnlySpan<T> target, int targetIndex, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  if (index < 0 || targetIndex < 0 || length <= 0 || index + length > Length || targetIndex + length > target.Length)
    //    return false;

    //  while (length-- > 0)
    //    if (!equalityComparer.Equals(this[index++], target[targetIndex++]))
    //      return false;

    //  return true;
    //}

    /// <summary>Gets the value at the specified index.</summary>
    public T GetValue(int index)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      return m_buffer[m_head + index];
    }

    ///// <summary>Reports the first index of the specified rune in the string builder, or -1 if not found. Uses the specified comparer.</summary>
    //public int IndexOf(T target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  for (var index = 0; index < Length; index++)
    //    if (equalityComparer.Equals(this[index], target))
    //      return index;

    //  return -1;
    //}

    ///// <summary>Returns the first index of the specified string in the string builder, or -1 if not found. Uses the specified comparer.</summary>
    //public int IndexOf(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  for (int index = 0, lastIndex = Length - target.Length; index <= lastIndex; index++)
    //    if (EqualsAt(index, target, 0, target.Length, equalityComparer))
    //      return index;

    //  return -1;
    //}

    /// <summary>Insert <paramref name="count"/> of <paramref name="value"/> starting at <paramref name="startAt"/>.</summary>
    public SequenceBuilder<T> Insert(int startAt, T value, int count = 1)
    {
      m_version++;

      EnsureUniformSpace(count);

      startAt += m_head;

      if (m_head >= m_buffer.Length - m_tail) // Grow from start.
      {
        System.Array.Copy(m_buffer, m_head, m_buffer, m_head - count, startAt - m_head);
        System.Array.Fill(m_buffer, value, startAt - count, count);
        m_head -= count;
      }
      else // Otherwise grow from end.
      {
        System.Array.Copy(m_buffer, startAt, m_buffer, startAt + count, m_tail - startAt);
        System.Array.Fill(m_buffer, value, startAt, count);
        m_tail += count;
      }

      return this;
    }
    /// <summary>Insert the <paramref name="values"/> starting at <paramref name="startAt"/>.</summary>
    public SequenceBuilder<T> Insert(int startAt, System.ReadOnlySpan<T> values)
    {
      m_version++;

      EnsureUniformSpace(values.Length);

      startAt += m_head;

      if (m_head >= m_buffer.Length - m_tail) // Grow from start.
      {
        System.Array.Copy(m_buffer, m_head, m_buffer, m_head - values.Length, startAt - m_head);
        values.CopyTo(m_buffer, startAt - values.Length);
        m_head -= values.Length;
      }
      else // Otherwise grow from end.
      {
        System.Array.Copy(m_buffer, startAt, m_buffer, startAt + values.Length, m_tail - startAt);
        values.CopyTo(m_buffer, startAt);
        m_tail += values.Length;
      }

      return this;
    }
    /// <summary>Insert a <paramref name="collection"/> starting at <paramref name="startAt"/>.</summary>
    public SequenceBuilder<T> Insert(int startAt, System.Collections.Generic.IEnumerable<T> collection)
    {
      m_version++;

      foreach (var item in collection)
        Insert(startAt++, item);

      return this;
    }

    ///// <summary>Determines whether the sequence is a palindrome.</summary>
    //public bool IsPalindrome(int startIndex, int endIndex, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  if (startIndex < 0 || startIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
    //  if (endIndex < startIndex || endIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  for (int indexL = startIndex, indexR = endIndex; indexL < indexR; indexL++, indexR--)
    //    if (!equalityComparer.Equals(this[indexL], this[indexR]))
    //      return false;

    //  return true;
    //}

    ///// <summary>Reports the last index of the specified rune in the string builder, or -1 if not found. Uses the specified comparer.</summary>
    //public int LastIndexOf(T target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  for (var index = Length - 1; index >= 0; index--)
    //    if (equalityComparer.Equals(this[index], target))
    //      return index;

    //  return -1;
    //}

    ///// <summary>Reports the last index of the specified string in the string builder. Or -1 if not found. Uses the specified comparer.</summary>
    //public int LastIndexOf(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  for (var index = Length - target.Length; index >= 0; index--)
    //    if (EqualsAt(index, target, 0, target.Length, equalityComparer))
    //      return index;

    //  return -1;
    //}

    ///// <summary>Returns a string containing the left most specified number of characters, if available, otherwise as many as there are.</summary>
    //public System.ReadOnlySpan<T> LeftMost(int maxCount)
    //  => AsReadOnlySpan().Slice(0, System.Math.Min(Length, System.Math.Max(maxCount < 0 ? throw new System.ArgumentOutOfRangeException(nameof(maxCount)) : maxCount, 0)));

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

    /// <summary>Normalize where the <paramref name="predicate"/> is satisfied using the <paramref name="normalizer"/> throughout the builder. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public SequenceBuilder<T> NormalizeAll(T normalizer, System.Func<T, bool> predicate)
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
          SetValue(normalizedIndex++, isCurrent ? normalizer : character);

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      return Remove(normalizedIndex, Length - normalizedIndex);
    }
    /// <summary>Normalize the <paramref name="normalizeValues"/> using the <paramref name="normalizer"/> throughout the builder. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public SequenceBuilder<T> NormalizeAll(T normalizer, System.Collections.Generic.IList<T> normalizeValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return NormalizeAll(normalizer, t => normalizeValues.Contains(t, equalityComparer));
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

    ///// <summary>The Prefix function for this sequence is an array of length n where the i-th element is equal to the greatest number of elements starting from the position i that coincide with the first elements of source. I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</summary>
    //// https://cp-algorithms.com/string/prefix-function.html
    //public int[] PrefixFunction(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  var sourceLength = Length;

    //  var p = new int[sourceLength];

    //  for (int i = 1; i < sourceLength; i++)
    //  {
    //    var j = p[i - 1];

    //    while (j > 0 && !equalityComparer.Equals(this[i], this[j]))
    //      j = p[j - 1];

    //    if (equalityComparer.Equals(this[i], this[j]))
    //      j++;

    //    p[i] = j;
    //  }

    //  return p;
    //}

    /// <summary>Prepends with a <paramref name="value"/>.</summary>
    public SequenceBuilder<T> Prepend(T value)
    {
      m_version++;

      EnsurePrependSpace(1);

      m_buffer[--m_head] = value;

      return this;
    }
    /// <summary>Prepends with the <paramref name="values"/>.</summary>
    public SequenceBuilder<T> Prepend(System.ReadOnlySpan<T> values)
    {
      m_version++;

      EnsurePrependSpace(values.Length);

      values.CopyTo(m_buffer, m_head -= values.Length);

      return this;
    }

    /// <summary>Removes the specified range of values from the builder.</summary>
    public SequenceBuilder<T> Remove(int startAt, int count)
    {
      m_version++;

      startAt += m_head;

      if (m_head <= m_buffer.Length - m_tail) // Shrink from start.
      {
        System.Array.Copy(m_buffer, m_head, m_buffer, m_head + count, startAt - m_head);
        System.Array.Fill(m_buffer, default, m_head, count);
        m_head += count;
      }
      else // Otherwise shrink from end.
      {
        System.Array.Copy(m_buffer, startAt + count, m_buffer, startAt, m_tail - startAt - count);
        m_tail -= count;
        System.Array.Fill(m_buffer, default, m_tail, count);
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

    ///// <summary>Returns a string containing the right most specified number of characters, if available, otherwise as many as there are.</summary>
    //public System.ReadOnlySpan<T> RightMost(int maxCount)
    //  => maxCount < 0 ? throw new System.ArgumentOutOfRangeException(nameof(maxCount)) : maxCount < Length ? AsReadOnlySpan().Slice(Length - maxCount, maxCount) : AsReadOnlySpan();

    /// <summary>Repeats the values in the builder <paramref name="count"/> times.</summary>
    public SequenceBuilder<T> Repeat(int count)
    {
      var original = AsReadOnlySpan().ToArray();

      while (count-- > 0)
        Append(original.AsReadOnlySpan());

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

      m_buffer[m_head + index] = value;
    }

    /// <summary>Shuffle all values in the builder. Uses the specified <paramref name="rng"/>, or default if null.</summary>
    public SequenceBuilder<T> Shuffle(System.Random? rng)
    {
      rng ??= new System.Random();

      for (var index = Length - 1; index > 0; index--) // Shuffle each element by swapping with a random element of a lower index.
        Swap(index, rng.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.

      return this;
    }

    ///// <summary>Creates a sequence of substrings, as a split of the StringBuilder content based on the characters in an array. There is no change to the StringBuilder content.</summary>
    //public System.Collections.Generic.IEnumerable<T[]> Split(System.StringSplitOptions options, System.Collections.Generic.IList<T> separators, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  var startIndex = 0;

    //  var sourceLength = Length;

    //  for (var index = startIndex; index < sourceLength; index++)
    //  {
    //    if (separators.Any(c => equalityComparer.Equals(c, this[index])))
    //    {
    //      if (index != startIndex || options != System.StringSplitOptions.RemoveEmptyEntries)
    //        yield return AsReadOnlySpan().Slice(startIndex, index - startIndex).ToArray();

    //      startIndex = index + 1;
    //    }
    //  }

    //  if (startIndex < sourceLength)
    //    yield return AsReadOnlySpan().Slice(startIndex, sourceLength - startIndex).ToArray();
    //}

    ///// <summary>Indicates whether <paramref name="source"/> starts with <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or the default if null.</summary>
    //public bool StartsWith(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  var sourceLength = Length;
    //  var targetLength = target.Length;

    //  if (sourceLength < targetLength)
    //    return false;

    //  for (var index = targetLength - 1; index >= 0; index--)
    //    if (!equalityComparer.Equals(this[index], target[index]))
    //      return false;

    //  return true;
    //}

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

    ///// <summary>The Z-function for this sequence is an array of length n where the i-th element is equal to the greatest number of elements starting from the position i that coincide with the first elements of source. I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</summary>
    //// https://cp-algorithms.com/string/z-function.html
    //public int[] ZFunction(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  var sourceLength = Length;

    //  var z = new int[sourceLength];

    //  for (int i = 1, l = 0, r = 0; i < sourceLength; i++)
    //  {
    //    if (i <= r)
    //      z[i] = System.Math.Min(r - i + 1, z[i - l]);

    //    while (i + z[i] < sourceLength && equalityComparer.Equals(this[z[i]], this[i + z[i]]))
    //      z[i]++;

    //    if (i + z[i] - 1 > r)
    //    {
    //      l = i;
    //      r = i + z[i] - 1;
    //    }
    //  }

    //  return z;
    //}

    public string ToString(int startAt) => AsReadOnlySpan().ToString(startAt);
    public string ToString(int startAt, int count) => AsReadOnlySpan().ToString(startAt, count - startAt);

    #region Object overrides.
    public override string ToString() => AsReadOnlySpan().ToString(0);
    #endregion Object overrides.
  }
}
