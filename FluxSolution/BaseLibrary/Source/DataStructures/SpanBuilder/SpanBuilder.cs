using System.Linq;
using System.Reflection;

namespace Flux
{
  public ref struct SpanBuilder<T>
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
    public T this[int index]
    {
      get => (index >= 0 && index < Length) ? m_array[m_head + index] : throw new System.ArgumentOutOfRangeException(nameof(index));
      set => m_array[m_head + index] = (index >= 0 && index < Length) ? value : throw new System.ArgumentOutOfRangeException(nameof(index));
    }

    /// <summary>This provides direct access to the underlying array storage for the SpanBuilder.</summary>
    /// <remarks>Use with caution!</remarks>
    public T[] InternalArray { get => m_array; init => m_array = value; }

    /// <summary>The current total capacity of the builder buffer.</summary>
    public int Capacity => m_array.Length;

    /// <summary>The current partial capacity of the builder buffer right-side (append).</summary>
    private int CapacityAppend => m_array.Length - m_tail;

    /// <summary>The current partial capacity of the builder buffer left-side (prepend).</summary>
    private int CapacityPrepend => m_head;

    public int Length => m_tail - m_head;

    //public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get => m_equalityComparer; set => m_equalityComparer = value ?? System.Collections.Generic.EqualityComparer<T>.Default; }

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
    public System.ReadOnlySpan<T> AsReadOnlySpan() => new(m_array, m_head, m_tail - m_head);

    /// <summary>This provides partial direct access to the underlying array storage for the SpanBuilder via a <see cref="System.Span{T}"/>.</summary>
    /// <remarks>Use with caution!</remarks>
    public System.Span<T> AsSpan() => new(m_array, m_head, m_tail - m_head);

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

    /// <summary>Internally copy <paramref name="count"/> items from <paramref name="sourceIndex"/> to <paramref name="targetIndex"/>.</summary>
    /// <param name="sourceIndex"></param>
    /// <param name="targetIndex"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public SpanBuilder<T> Copy(int sourceIndex, int targetIndex, int count)
    {
      if (sourceIndex < 0 || sourceIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex));
      if (targetIndex < 0 || targetIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex));
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
    public void CopyTo(T[] array, int startIndex)
    {
      for (var index = m_head; index < m_tail; index++)
        array[startIndex++] = m_array[index];
    }

    //public void CopyTo(int sourceIndex, System.Collections.Generic.IList<T> target, int targetIndex, int count)
    //{
    //  while (count-- > 0)
    //    target[targetIndex++] = this[sourceIndex++];
    //}

    /// <summary>Returns the <paramref name="source"/> with the specified <paramref name="values"/> duplicated by the specified <paramref name="count"/> throughout. If no values are specified, all characters are replicated. If the string builder is empty, nothing is replicated. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
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

    /// <summary>Normalize the specified (or all if none specified) consecutive characters in the string. Uses the specfied comparer.</summary>
    public SpanBuilder<T> NormalizeAdjacent(int maxAdjacentLength, System.ReadOnlySpan<T> items, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (maxAdjacentLength < 1) throw new System.ArgumentNullException(nameof(maxAdjacentLength));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var index = 0;
      T previous = default!;
      var adjacentLength = 1;

      for (var indexOfSource = 0; indexOfSource < Length; indexOfSource++)
      {
        var current = this[indexOfSource];

        var isEqual = items.Length > 0 // Use list or just characters?
          ? (items.IndexOf(current, equalityComparer) > -1 && items.IndexOf(previous, equalityComparer) > -1) // Is both current and previous in characters?
          : equalityComparer.Equals(current, previous); // Is current and previous character equal?

        if (!isEqual || adjacentLength < maxAdjacentLength)
        {
          this[index++] = current;

          previous = current;
        }

        adjacentLength = !isEqual ? 1 : adjacentLength + 1;
      }

      return Remove(index, Length - index);
    }

    /// <summary>Normalize the specified (or all if none specified) consecutive characters in the string. Uses the default comparer.</summary>
    public SpanBuilder<T> NormalizeAdjacent(int maxAdjacentLength, params T[] items)
      => NormalizeAdjacent(maxAdjacentLength, items, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Normalize all sequences of characters satisfying the predicate throughout the string. Normalizing means removing leading/trailing and replacing certain consecutive characters with a single specified character.</summary>
    /// <example>"".NormalizeAll(' ', char.IsWhiteSpace);</example>
    /// <example>"".NormalizeAll(' ', c => c == ' ');</example>
    public SpanBuilder<T> NormalizeAll(T replacement, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var normlizedIndex = 0;

      var isPrevious = true;

      for (var sourceIndex = 0; sourceIndex < Length; sourceIndex++)
      {
        var character = this[sourceIndex];

        var isCurrent = predicate(character);

        if (!(isPrevious && isCurrent))
        {
          this[normlizedIndex++] = isCurrent ? replacement : character;

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normlizedIndex--;

      return normlizedIndex == Length ? this : Remove(normlizedIndex, Length - normlizedIndex);
    }

    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing and replacing sequences of specified characters with a single specified character.</summary>
    public SpanBuilder<T> NormalizeAll(T replacement, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] items)
      => NormalizeAll(replacement, t => items.Contains(t, equalityComparer));

    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing and replacing sequences of specified characters with a single specified character.</summary>
    public SpanBuilder<T> NormalizeAll(T replacement, params T[] items)
      => NormalizeAll(replacement, items.Contains);

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

    /// <summary>Pads this StringBuilder on the left with the specified padding character.</summary>
    public SpanBuilder<T> PadLeft(int totalWidth, T padding)
      => Insert(0, padding, totalWidth - Length);

    /// <summary>Pads this StringBuilder on the left with the specified padding string.</summary>
    public SpanBuilder<T> PadLeft(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Insert(0, padding, 1);

      Remove(0, Length - totalWidth);

      return this;
    }

    /// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
    public SpanBuilder<T> PadRight(int totalWidth, T padding)
      => Append(padding, totalWidth - Length);

    /// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
    public SpanBuilder<T> PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Append(padding, 1);

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
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = Length;

      var removedIndex = 0;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
        if (this[sourceIndex] is var character && !predicate(character))
          this[removedIndex++] = character;

      return Remove(removedIndex, Length - removedIndex);
    }

    /// <summary>Remove the specified characters. Uses the specified comparer.</summary>
    public SpanBuilder<T> RemoveAll(System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] remove)
      => RemoveAll(t => remove.Contains(t, equalityComparer));

    /// <summary>Remove the specified characters. Uses the default comparer.</summary>
    public SpanBuilder<T> RemoveAll(params T[] remove)
      => RemoveAll(remove.Contains);

    public SpanBuilder<T> RemoveEvery(int interval)
    {
      for (var index = Length - 1; index >= 0; index--)
        if (index > 0 && index % interval == interval - 1)
          Remove(index, 1);

      return this;
    }

    /// <summary>Repeats the values in the builder <paramref name="count"/> times.</summary>
    public SpanBuilder<T> Repeat(int count)
      => Append(new SpanBuilder<T>(AsReadOnlySpan(), count).AsReadOnlySpan(), 1);

    /// <summary>Replace all characters using the specified replacement selector function. If the replacement selector returns null/default or empty, no replacement is made.</summary>
    public SpanBuilder<T> ReplaceAll(System.Func<T, T[]> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = Length - 1; index >= 0; index--)
        if (replacementSelector(this[index]) is var replacement && (replacement is not null && replacement.Length > 0))
          Remove(index, 1).Insert(index, replacement.AsSpan(), 1);

      return this;
    }

    /// <summary>Replace all characters using the specified replacement selector function.</summary>
    public SpanBuilder<T> ReplaceAll(System.Func<T, T> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = Length - 1; index >= 0; index--)
        this[index] = replacementSelector(this[index]);

      return this;
    }

    /// <summary>Replace all characters satisfying the predicate with the specified character.</summary>
    /// <example>"".ReplaceAll(replacement, char.IsWhiteSpace);</example>
    public SpanBuilder<T> ReplaceAll(T replacement, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = Length - 1; index >= 0; index--)
        if (predicate(this[index]))
          this[index] = replacement;

      return this;
    }

    /// <summary>Replace all specified characters with the specified character.</summary>
    public SpanBuilder<T> ReplaceAll(T replacement, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] replace)
      => ReplaceAll(replacement, t => replace.Contains(t, equalityComparer));

    /// <summary>Replace all specified characters with the specified character.</summary>
    public SpanBuilder<T> ReplaceAll(T replacement, params T[] replace)
      => ReplaceAll(replacement, replace.Contains);

    public SpanBuilder<T> ReplaceIfEqualAt(int startAt, System.ReadOnlySpan<T> key, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      if (AsReadOnlySpan().EqualsAt(startAt, key, equalityComparer))
      {
        Remove(startAt, key.Length);
        Insert(startAt, value, 1);
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

    //public void SetValue(int index, T value) => m_array[m_head + index] = (index >= 0 && index < Count) ? value : throw new System.ArgumentOutOfRangeException(nameof(index));

    /// <summary>Swap two values at the specified indices.</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public SpanBuilder<T> Swap(int indexA, int indexB)
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

    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public SpanBuilder<T> TrimLeft(T separator, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < Length; index++)
        if (equalityComparer.Equals(this[index], separator))
          Remove(index, 1);
        else break;

      return this;
    }

    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public SpanBuilder<T> TrimRight(T separator, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = Length - 1; index >= 0; index--)
        if (equalityComparer.Equals(this[index], separator))
          Remove(index, 1);
        else break;

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
