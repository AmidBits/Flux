namespace Flux
{
  public static partial class Em
  {
    public static System.Collections.Generic.List<(int Index, int Length)> GetValueMatches(this SpanMaker<char> source, string pattern)
    {
      var list = new System.Collections.Generic.List<(int Index, int Length)>();
      foreach (var vm in new System.Text.RegularExpressions.Regex(pattern).EnumerateMatches(source.AsReadOnlySpan()))
        list.Add((vm.Index, vm.Length));
      return list;
    }

    public static SpanMaker<char> Remove(this SpanMaker<char> source, string pattern)
    {
      var vms = source.GetValueMatches(pattern);

      for (var i = vms.Count - 1; i >= 0; i--)
      {
        var (index, length) = vms[i];

        source = source.Remove(index, length);
      }

      return source;
    }

#if NET9_0_OR_GREATER

    public static SpanMaker<char> Replace(this SpanMaker<char> source, string pattern, int count, params System.ReadOnlySpan<char> replacement)
    {
      var vms = source.GetValueMatches(pattern);

      for (var i = vms.Count - 1; i >= 0; i--)
      {
        var (index, length) = vms[i];

        source = source.Remove(index, length).Insert(index, count, replacement);
      }

      return source;
    }

#else

    public static SpanMaker<char> Replace(this SpanMaker<char> source, string pattern, int count, System.ReadOnlySpan<char> replacement)
    {
      var vms = source.GetValueMatches(pattern);

      for (var i = vms.Count - 1; i >= 0; i--)
      {
        var (index, length) = vms[i];

        source = source.Remove(index, length).Insert(index, count, replacement);
      }

      return source;
    }

#endif

  }

  public ref struct SpanMaker<T>
  {
    private const int DefaultBufferSize = 2;

    private T[] m_array;

    private int m_head; // Start of buffer data.
    private int m_tail; // End of buffer data.

    private SpanMaker(T[] array, int head, int tail)
    {
      m_array = array;

      m_head = head;
      m_tail = tail;
    }

    public SpanMaker(int capacity)
    {
      m_array = capacity >= 1 ? System.Buffers.ArrayPool<T>.Shared.Rent(int.Max(capacity, DefaultBufferSize).Pow2AwayFromZero(false)) : System.Array.Empty<T>();

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }

    public SpanMaker() : this(DefaultBufferSize) { }

#if NET9_0_OR_GREATER

    public SpanMaker(params System.ReadOnlySpan<T> span)
      : this(span.Length + DefaultBufferSize)
    {
      m_head = m_array.Length / 2 - span.Length / 2;

      span.CopyTo(m_array.AsSpan().Slice(m_head, span.Length));

      m_tail = m_head + span.Length;
    }

#else

    public SpanMaker(System.ReadOnlySpan<T> span)
      : this(span.Length + DefaultBufferSize)
    {
      span.CopyTo(m_array.AsSpan().Slice(m_head, span.Length));

      m_tail = span.Length;
    }

#endif

    public void Deconstruct(out T[] array, out int head, out int tail) { array = m_array; head = m_head; tail = m_tail; }

    /// <summary>
    /// <para>Gets the element at the specified index in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public T this[int index]
    {
      get => (index >= 0 && index <= Count) ? m_array[m_head + index] : throw new System.ArgumentOutOfRangeException(nameof(index));
      set => m_array[m_head + index] = (index >= 0 && index <= Count) ? value : throw new System.ArgumentOutOfRangeException(nameof(index));
    }

    /// <summary>
    /// <para>Gets the number of elements in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    public int Count
      => m_tail - m_head;

    /// <summary>
    /// <para>The unused capacity on the append (left) side of the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    private int FreeAppend => m_array.Length - m_tail;

    /// <summary>
    /// <para>The unused capacity on the prepend (right) side of the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    private int FreePrepend => m_head;

#if NET9_0_OR_GREATER

    /// <summary>Append <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    public SpanMaker<T> Append(int count, params System.Collections.Generic.ICollection<T> collection)
    {
      var totalAppend = collection.Count * count;

      var (array, head, tail) = EnsureCapacityAppend(totalAppend);

      var index = tail - totalAppend;

      while (count-- > 0)
      {
        collection.CopyTo(array, index);

        index += collection.Count;
      }

      return new(array, head, tail);
    }

    /// <summary>Append <paramref name="span"/>, <paramref name="count"/> times.</summary>
    public SpanMaker<T> Append(int count, params System.ReadOnlySpan<T> span)
    {
      var totalAppend = span.Length * count;

      var (array, head, tail) = EnsureCapacityAppend(totalAppend);

      var index = tail - totalAppend;

      while (count-- > 0)
      {
        span.CopyTo(array.AsSpan().Slice(index, span.Length));

        index += span.Length;
      }

      return new(array, head, tail);
    }

#else

    /// <summary>Append <paramref name="value"/>, <paramref name="count"/> times.</summary>
    public SpanMaker<T> Append(int count, T value)
    {
      var (array, head, tail) = EnsureCapacityAppend(count);

      System.Array.Fill(array, value, tail - count, count);

      return new(array, head, tail);
    }

    /// <summary>Append <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    public SpanMaker<T> Append(int count, System.Collections.Generic.ICollection<T> collection)
    {
      var totalAppend = collection.Count * count;

      var (array, head, tail) = EnsureCapacityAppend(totalAppend);

      var index = tail - totalAppend;

      while (count-- > 0)
      {
        collection.CopyTo(array, index);

        index += collection.Count;
      }

      return new(array, head, tail);
    }

    /// <summary>Append <paramref name="span"/>, <paramref name="count"/> times.</summary>
    public SpanMaker<T> Append(int count, System.ReadOnlySpan<T> span)
    {
      var totalAppend = span.Length * count;

      var (array, head, tail) = EnsureCapacityAppend(totalAppend);

      var index = tail - totalAppend;

      while (count-- > 0)
      {
        span.CopyTo(array.AsSpan().Slice(index, span.Length));

        index += span.Length;
      }

      return new(array, head, tail);
    }

#endif

    /// <summary>Creates a non-allocating <see cref="System.ReadOnlySpan{T}"/> of the elements in the <see cref="SpanMaker{T}"/>.</summary>
    public System.ReadOnlySpan<T> AsReadOnlySpan()
      => new(m_array, m_head, m_tail - m_head);

    /// <summary>Creates a non-allocating <see cref="System.Span{T}"/> of the elements in the <see cref="SpanMaker{T}"/>. This provides partial direct access into the underlying array storage for the SpanBuilder.</summary>
    /// <remarks>Use with caution!</remarks>
    public System.Span<T> AsSpan()
      => new(m_array, m_head, m_tail - m_head);

    public void Clear()
    {
      System.Array.Clear(m_array);

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }

    public SpanMaker<T> Duplicate(System.Func<T, bool> predicate, int count)
    {
      var sm = this;

      for (var index = 0; index < Count; index++)
        if (sm[index] is var value && predicate(value))
        {
          sm = sm.Insert(index, count, value);

          index += count;
        }

      return sm;
    }

    private (T[] array, int head, int tail) EnsureCapacityAppend(int needAppend)
    {
      var (array, head, tail) = this;

      if (FreeAppend > needAppend) // There is already room to prepend.
        return (array, head, tail + needAppend);
      else if (FreePrepend + FreeAppend > needAppend) // There is room, if current data is moved.
      {
        var offset = (needAppend - FreeAppend);
        System.Array.Copy(array, head, array, head - offset, Count);
        return (array, head - offset, head + needAppend);
      }

      var totalSize = FreePrepend + Count + needAppend + FreeAppend;

      array = System.Buffers.ArrayPool<T>.Shared.Rent(int.Max(totalSize, DefaultBufferSize).Pow2AwayFromZero(true));

      head = FreePrepend;
      tail = FreePrepend + Count + needAppend;

      System.Array.Copy(m_array, m_head, array, head, Count); // Copy old content.

      return (array, head, tail);
    }

    private (T[] array, int head, int tail) EnsureCapacityInsert(int indexInsert, int needInsert)
    {
      var (array, head, tail) = this;

      var indexArray = head + indexInsert;

      if (FreePrepend > needInsert) // Available on prepend side.
      {
        var newHead = head - needInsert;

        System.Array.Copy(array, head, array, newHead, indexInsert);

        return (array, newHead, tail);
      }
      else if (FreeAppend > needInsert) // Available on append side.
      {
        var startInsert = head + indexInsert;

        System.Array.Copy(array, startInsert, array, startInsert + needInsert, tail - startInsert);

        return (array, head, tail + needInsert);
      }
      else if (FreePrepend + FreeAppend > needInsert) // Available overall.
      {
        System.Array.Copy(m_array, m_head, array, 0, indexInsert);
        System.Array.Copy(m_array, m_head + indexInsert, array, m_head + indexInsert + (indexInsert + needInsert - indexArray), Count - indexInsert);

        return (array, 0, tail + needInsert - FreePrepend);
      }

      var totalSize = FreePrepend + Count + FreeAppend + needInsert;

      array = System.Buffers.ArrayPool<T>.Shared.Rent(int.Max(totalSize, DefaultBufferSize).Pow2AwayFromZero(true));

      head = FreePrepend;
      tail = head + Count + needInsert;

      System.Array.Copy(m_array, m_head, array, head, indexInsert);
      System.Array.Copy(m_array, m_head + indexInsert, array, tail - (Count - indexInsert), Count - indexInsert);

      return (array, head, tail);
    }

    private (T[] array, int head, int tail) EnsureCapacityPrepend(int needPrepend)
    {
      var (array, head, tail) = this;

      if (needPrepend < FreePrepend) // There is already room to prepend.
        return (array, head - needPrepend, tail);
      else if (needPrepend < FreePrepend + FreeAppend) // There is room, if current data is moved.
      {
        var offset = (needPrepend - FreePrepend);
        System.Array.Copy(array, head, array, head + offset, Count);
        return (array, 0, tail + offset);
      }

      var totalSize = FreePrepend + needPrepend + Count + FreeAppend;

      array = System.Buffers.ArrayPool<T>.Shared.Rent(int.Max(totalSize, DefaultBufferSize).Pow2AwayFromZero(true));

      head = FreePrepend;
      tail = head + needPrepend + Count;

      System.Array.Copy(m_array, m_head, array, head + needPrepend, Count); // Copy old content.

      return (array, head, tail);
    }

#if NET9_0_OR_GREATER

    /// <summary>Insert <paramref name="count"/> of <paramref name="collection"/> starting at <paramref name="index"/>.</summary>
    public SpanMaker<T> Insert(int index, int count, params System.Collections.Generic.ICollection<T> collection)
    {
      var totalInsert = collection.Count * count;

      var (array, head, tail) = EnsureCapacityInsert(index, totalInsert);

      index += head;

      while (count-- > 0)
      {
        collection.CopyTo(array, index);

        index += collection.Count;
      }

      return new(array, head, tail);
    }

    /// <summary>Insert <paramref name="count"/> of <paramref name="span"/> starting at <paramref name="index"/>.</summary>
    public SpanMaker<T> Insert(int index, int count, params System.ReadOnlySpan<T> span)
    {
      var totalInsert = span.Length * count;

      var (array, head, tail) = EnsureCapacityInsert(index, totalInsert);

      while (count-- > 0)
      {
        span.CopyTo(array.AsSpan().Slice(head + index, span.Length));

        index += span.Length;
      }

      return new(array, head, tail);
    }

#else

    /// <summary>Insert <paramref name="count"/> of <paramref name="value"/> starting at <paramref name="index"/>.</summary>
    public SpanMaker<T> Insert(int index, int count, T value)
    {
      var (array, head, tail) = EnsureCapacityInsert(index, count);

      System.Array.Fill(array, value, index + head, count);

      return new(array, head, tail);
    }

    /// <summary>Insert <paramref name="count"/> of <paramref name="collection"/> starting at <paramref name="index"/>.</summary>
    public SpanMaker<T> Insert(int index, int count, System.Collections.Generic.ICollection<T> collection)
    {
      var totalInsert = collection.Count * count;

      var (array, head, tail) = EnsureCapacityInsert(index, totalInsert);

      index += head;

      while (count-- > 0)
      {
        collection.CopyTo(array, index);

        index += collection.Count;
      }

      return new(array, head, tail);
    }

    /// <summary>Insert <paramref name="count"/> of <paramref name="span"/> starting at <paramref name="index"/>.</summary>
    public SpanMaker<T> Insert(int index, int count, System.ReadOnlySpan<T> span)
    {
      var totalInsert = span.Length * count;

      var (array, head, tail) = EnsureCapacityInsert(index, totalInsert);

      while (count-- > 0)
      {
        span.CopyTo(array.AsSpan().Slice(head + index, span.Length));

        index += span.Length;
      }

      return new(array, head, tail);
    }

#endif

    public SpanMaker<T> Insert(int index, SpanMaker<T> spanGlue, int count) => Insert(index, count, spanGlue.AsReadOnlySpan());

#if NET9_0_OR_GREATER

    /// <summary>
    /// <para>Normalize (trim down) any consecutive <paramref name="values"/> (or all items, if none specified) that occur more than <paramref name="maxAdjacentCount"/> in the <see cref="SpanBuilder{T}"/>. Uses the specfied <paramref name="equalityComparer"/>.</para>
    /// </summary>
    public SpanMaker<T> NormalizeAdjacent(int maxAdjacentCount, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, params System.ReadOnlySpan<T> values)
    {
      if (maxAdjacentCount < 1) throw new System.ArgumentNullException(nameof(maxAdjacentCount));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var normalizedTail = m_head;

      T previous = default!;
      var adjacentLength = 1;

      for (var i = m_head; i < m_tail; i++)
      {
        var current = m_array[i];

        var isEqual = values.Length > 0 // Use span or just items?
          ? (values.Exists(c => equalityComparer.Equals(c, current)) && values.Exists(c => equalityComparer.Equals(c, previous))) // Is both current and previous in items?
          : equalityComparer.Equals(current, previous); // Are current and previous items equal?

        if (!isEqual || adjacentLength < maxAdjacentCount)
        {
          m_array[normalizedTail++] = current;

          previous = current;
        }

        adjacentLength = !isEqual ? 1 : adjacentLength + 1;
      }

      m_tail = normalizedTail;

      return this;
    }

#else

    /// <summary>
    /// <para>Normalize (trim down) any consecutive <paramref name="values"/> (or all items, if none specified) that occur more than <paramref name="maxAdjacentCount"/> in the <see cref="SpanBuilder{T}"/>. Uses the specfied <paramref name="equalityComparer"/>.</para>
    /// </summary>
    public SpanMaker<T> NormalizeAdjacent(int maxAdjacentCount, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, params T[] values)
    {
      System.ArgumentNullException.ThrowIfNull(values);

      if (maxAdjacentCount < 1) throw new System.ArgumentNullException(nameof(maxAdjacentCount));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var normalizedTail = m_head;

      T previous = default!;
      var adjacentLength = 1;

      for (var i = m_head; i < m_tail; i++)
      {
        var current = m_array[i];

        var isEqual = values.Length > 0 // Use span or just items?
          ? (System.Array.Exists(values, c => equalityComparer.Equals(c, current)) && System.Array.Exists(values, c => equalityComparer.Equals(c, previous))) // Is both current and previous in items?
          : equalityComparer.Equals(current, previous); // Are current and previous items equal?

        if (!isEqual || adjacentLength < maxAdjacentCount)
        {
          m_array[normalizedTail++] = current;

          previous = current;
        }

        adjacentLength = !isEqual ? 1 : adjacentLength + 1;
      }

      m_tail = normalizedTail;

      return this;
    }

#endif

    /// <summary>
    /// <para>Normalize all adjacent duplicates. Uses the specified <paramref name="equalityComparer"/>.</para>
    /// </summary>
    public SpanMaker<T> NormalizeAdjacentDuplicates(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (Count >= 2)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var removeIndex = m_head + 1;

        for (var index = m_head + 1; index < m_tail; index++)
          if (!equalityComparer.Equals(m_array[removeIndex - 1], m_array[index]))
            m_array[removeIndex++] = m_array[index];

        m_tail = removeIndex;
      }

      return this;
    }

    /// <summary>Normalize any one or more consecutive items satisfied by the <paramref name="predicate"/> to one instance of <paramref name="replacement"/>.</summary>
    /// <example>"".Normalize(char.IsWhiteSpace, ' ');</example>
    /// <example>"".Normalize(c => c == ' ', ' ');</example>
    /// <remarks>Normalizing (here) means removing leading/trailing (either end of the <see cref="SpanBuilder{T}"/>) and replacing one or more consecutive characters satisfying the <paramref name="predicate"/> within the <see cref="SpanBuilder{T}"/> to one instance of <paramref name="replacement"/>. No replacements, i.e. only removals, are performed if elements are on left or right edge.</remarks>
    public SpanMaker<T> NormalizeAll(System.Func<T, bool> predicate, T replacement)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      var normalizedMark = m_head;

      var isPrevious = true;

      for (var mark = m_head; mark < m_tail; mark++)
      {
        var item = m_array[mark];

        var isCurrent = predicate(item);

        if (!(isPrevious && isCurrent))
        {
          m_array[normalizedMark++] = isCurrent ? replacement : item;

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedMark--;

      m_tail = normalizedMark;

      return this;
    }

#if NET9_0_OR_GREATER

    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public SpanMaker<T> PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    {
      var sm = this;

      if (totalWidth > sm.Count)
      {
        var quotient = System.Math.DivRem(totalWidth - sm.Count, 2, out var remainder);

        sm = sm.PadLeft(sm.Count + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        sm = sm.PadRight(totalWidth, paddingRight);
      }

      return sm;
    }

    /// <summary>Pads this <see cref="SpanMaker{T}"/> on the left with the specified padding string.</summary>
    public SpanMaker<T> PadLeft(int totalWidth, params System.ReadOnlySpan<T> padding)
    {
      var sm = this;
      sm = Prepend((totalWidth - sm.Count) / padding.Length + 1, padding);
      sm = sm.Remove(0, sm.Count - totalWidth);
      return sm;
    }

    /// <summary>Pads this <see cref="SpanMaker{T}"/> on the right with the specified padding string.</summary>
    public SpanMaker<T> PadRight(int totalWidth, params System.ReadOnlySpan<T> padding)
    {
      var sm = this;
      sm = sm.Append((totalWidth - sm.Count) / padding.Length + 1, padding);
      sm = sm.Remove(totalWidth);
      return sm;
    }

#else

    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public SpanMaker<T> PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    {
      if (totalWidth > Count)
      {
        var quotient = System.Math.DivRem(totalWidth - Count, 2, out var remainder);

        PadLeft(Count + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        PadRight(totalWidth, paddingRight);
      }

      return this;
    }

    /// <summary>Pads this <see cref="SpanMaker{T}"/> on the left with the specified padding string.</summary>
    public SpanMaker<T> PadLeft(int totalWidth, System.ReadOnlySpan<T> padding) => Prepend((totalWidth - Count) / padding.Length + 1, padding).Remove(0, Count - totalWidth);

    /// <summary>Pads this <see cref="SpanMaker{T}"/> on the right with the specified padding string.</summary>
    public SpanMaker<T> PadRight(int totalWidth, System.ReadOnlySpan<T> padding) => Append((totalWidth - Count) / padding.Length + 1, padding).Remove(totalWidth);

#endif

#if NET9_0_OR_GREATER

    /// <summary>
    /// <para>Prepends <paramref name="collection"/>, <paramref name="count"/> times, to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanMaker<T> Prepend(int count, params System.Collections.Generic.ICollection<T> collection)
    {
      var (array, head, tail) = EnsureCapacityPrepend(collection.Count * count);

      var index = head;

      while (count-- > 0)
      {
        collection.CopyTo(array, index);

        index += collection.Count;
      }

      return new(array, head, tail);
    }

    /// <summary>
    /// <para>Prepends <paramref name="span"/>, <paramref name="count"/> times, to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="span"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanMaker<T> Prepend(int count, params System.ReadOnlySpan<T> span)
    {
      var (array, head, tail) = EnsureCapacityPrepend(span.Length * count);

      var index = head;

      while (count-- > 0)
      {
        span.CopyTo(array.AsSpan().Slice(index, span.Length));

        index += span.Length;
      }

      return new(array, head, tail);
    }

#else

    /// <summary>
    /// <para>Prepends <paramref name="value"/>, <paramref name="count"/> times, to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanMaker<T> Prepend(int count, T value)
    {
      var (array, head, tail) = EnsureCapacityPrepend(count);

      while (count-- > 0)
        array[head + count] = value;

      return new(array, head, tail);
    }

    /// <summary>
    /// <para>Prepends <paramref name="collection"/>, <paramref name="count"/> times, to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanMaker<T> Prepend(int count, System.Collections.Generic.ICollection<T> collection)
    {
      var (array, head, tail) = EnsureCapacityPrepend(collection.Count * count);

      var index = head;

      while (count-- > 0)
      {
        collection.CopyTo(array, index);

        index += collection.Count;
      }

      return new(array, head, tail);
    }

    /// <summary>
    /// <para>Prepends <paramref name="span"/>, <paramref name="count"/> times, to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="span"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanMaker<T> Prepend(int count, System.ReadOnlySpan<T> span)
    {
      var (array, head, tail) = EnsureCapacityPrepend(span.Length * count);

      var index = head;

      while (count-- > 0)
      {
        span.CopyTo(array.AsSpan().Slice(index, span.Length));

        index += span.Length;
      }

      return new(array, head, tail);
    }

#endif

    /// <summary>
    /// <para>Removes the specified range [index..(index + count)] of elements in the <see cref="SpanMaker{T}">.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public SpanMaker<T> Remove(int index, int count)
    {
      if (index < 0 || index >= Count) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (count < 0 || index + count > Count) throw new System.ArgumentOutOfRangeException(nameof(count));

      if (index == 0)
        return RemoveLeft(count);

      if (index + count == Count)
        return RemoveRight(count);

      var mark = index + m_head;

      if (m_head <= m_array.Length - m_tail) // Shrink from start.
      {

        System.Array.Copy(m_array, m_head, m_array, m_head + count, mark - m_head);

        return new(m_array, m_head + count, m_tail);
      }
      else // Otherwise shrink from end.
      {

        System.Array.Copy(m_array, mark + count, m_array, mark, m_tail - mark - count);

        return new(m_array, m_head, m_tail - count);
      }
    }

    /// <summary>
    /// <para>Removes the specified range [index..] of elements from the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    public SpanMaker<T> Remove(int index)
      // Direct code modifications, checks needed.
      => index < 0 || index >= Count
      ? throw new System.ArgumentOutOfRangeException(nameof(index))
      : new(m_array, m_head, m_head + index);

    public SpanMaker<T> RemoveAll(System.Func<T, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      var removedMark = m_head; // This represents the "lag" mark, and becomes the new m_tail in the end.

      for (var mark = m_head; mark < m_tail; mark++)
        if (m_array[mark] is var c && !predicate(c))
          m_array[removedMark++] = c;

      m_tail = removedMark;

      return this;
    }

    public SpanMaker<T> RemoveLeft(int count)
      // Direct code modifications, checks needed.
      => count < 0 || count > Count
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : new(m_array, m_head + count, m_tail);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public SpanMaker<T> RemoveRight(int count)
      // Direct code modifications, checks needed.
      => count < 0 || count > Count
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : new(m_array, m_head, m_tail - count);

    public SpanMaker<T> Repeat(int count)
      => Append(3, AsReadOnlySpan().ToArray().AsReadOnlySpan());

#if NET9_0_OR_GREATER

    /// <summary>
    /// <para>Replaces all elements satisfying the <paramref name="predicate"/> with <paramref name="count"/> of <paramref name="replacement"/> in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public SpanMaker<T> Replace(System.Func<T, bool> predicate, int count, params System.ReadOnlySpan<T> replacement)
    {
      var sm = this;

      for (var i = sm.m_tail - sm.m_head; i >= 0; i--)
        if (predicate(sm.m_array[sm.m_head + i]))
          sm = sm.Remove(i, 1).Insert(i, count, replacement);

      return sm;
    }

#else

    /// <summary>
    /// <para>Replaces all elements satisfying the <paramref name="predicate"/> with <paramref name="count"/> of <paramref name="replacement"/> in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public SpanMaker<T> Replace(System.Func<T, bool> predicate, int count, System.ReadOnlySpan<T> replacement)
    {
      var sm = this;

      for (var i = sm.m_tail - sm.m_head; i >= 0; i--)
        if (predicate(sm.m_array[sm.m_head + i]))
          sm = sm.Remove(i, 1).Insert(i, 1, replacement);

      return sm;
    }

#endif

    public SpanMaker<T> ReplaceIfEqualAt(int startAt, System.ReadOnlySpan<T> find, System.ReadOnlySpan<T> replacement, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sm = this;

      if (AsReadOnlySpan().EqualsAt(startAt, find, equalityComparer))
      {
        sm = sm.Remove(startAt, find.Length).Insert(startAt, 1, replacement);
      }

      return sm;
    }

    /// <summary>
    /// <para>Reverses all elements in the range {<paramref name="index"/>, <paramref name="count"/>}.</para>
    /// </summary>
    /// <param name="index">Because Swap() is re-used, index is kept abstract, i.e. zero-based.</param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanMaker<T> Reverse(int index, int count)
    {
      var oppositeIndex = index + count - 1;

      while (index < oppositeIndex)
        Swap(index++, oppositeIndex--);

      return this;
    }

    /// <summary>
    /// <para>Reverses elements in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <returns></returns>
    public SpanMaker<T> Reverse() => Reverse(0, Count);

    /// <summary>
    /// <para>Swaps two elements at the specified [<paramref name="indexA"/>] and [<paramref name="indexB"/>] in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    /// <returns></returns>
    public SpanMaker<T> Swap(int indexA, int indexB)
    {
      if (indexA != indexB)
        (this[indexA], this[indexB]) = (this[indexB], this[indexA]);

      return this;
    }

    /// <summary>
    /// <para>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the beginning of the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    public SpanMaker<T> TrimLeft(System.Func<T, bool> predicate) => new(m_array, m_head + AsReadOnlySpan().CommonPrefixLength(0, predicate), m_tail);

    /// <summary>
    /// <para>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the end of the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    public SpanMaker<T> TrimRight(System.Func<T, bool> predicate) => new(m_array, m_head, m_tail - AsReadOnlySpan().CommonSuffixLength(0, predicate));

    public override string ToString()
      => AsReadOnlySpan().ToString();

    //public System.Collections.Generic.IEnumerator<T> GetEnumerator() => AsReadOnlySpan().ToArray().AsEnumerable().GetEnumerator();
    //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
