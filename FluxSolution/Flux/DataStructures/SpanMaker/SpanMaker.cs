namespace Flux
{
  public ref struct SpanMaker<T>
  {
    private const int DefaultBufferSize = 2;

    private T[] m_array;

    private int m_head; // Start of buffer data.
    private int m_tail; // End of buffer data.

    internal SpanMaker(T[] array, int head, int tail)
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

    public SpanMaker(System.ReadOnlySpan<T> span)
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
      m_head = m_array.Length / 2 - span.Length / 2;

      span.CopyTo(m_array.AsSpan().Slice(m_head, span.Length));

      m_tail = m_head + span.Length;
    }

#endif

    public readonly void Deconstruct(out T[] array, out int head, out int tail) { array = m_array; head = m_head; tail = m_tail; }

    /// <summary>
    /// <para>Gets the element at the specified index in a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public T this[int index]
    {
      get => (index >= 0 && index <= Length) ? m_array[m_head + index] : throw new System.ArgumentOutOfRangeException(nameof(index));
      set => m_array[m_head + index] = (index >= 0 && index <= Length) ? value : throw new System.ArgumentOutOfRangeException(nameof(index));
    }

    /// <summary>
    /// <para>The unused capacity on the append (left) side of a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    private readonly int FreeAppend => m_array.Length - m_tail;

    /// <summary>
    /// <para>The unused capacity on the prepend (right) side of a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    private readonly int FreePrepend => m_head;

    /// <summary>
    /// <para>Gets the number of elements in a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    public readonly int Length => m_tail - m_head;

    public SpanMaker<T> Append(int count, SpanMaker<T> other) => Append(count, other.AsReadOnlySpan());

    public SpanMaker<T> Append(SpanMaker<T> other) => Append(1, other.AsReadOnlySpan());

    #region Append

#if XNET9_0_OR_GREATER

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

    /// <summary>Append <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    public SpanMaker<T> Append(params System.Collections.Generic.ICollection<T> collection) => Append(1, collection);

    /// <summary>Append <paramref name="span"/>, <paramref name="count"/> times.</summary>
    public SpanMaker<T> Append(params System.ReadOnlySpan<T> span) => Append(1, span);

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

    /// <summary>Append <paramref name="value"/>.</summary>
    public SpanMaker<T> Append(T value) => Append(1, value);

    /// <summary>Append <paramref name="collection"/>.</summary>
    public SpanMaker<T> Append(System.Collections.Generic.ICollection<T> collection) => Append(1, collection);

    /// <summary>Append <paramref name="span"/>.</summary>
    public SpanMaker<T> Append(System.ReadOnlySpan<T> span) => Append(1, span);

#endif

    #endregion // Append

    /// <summary>Creates a non-allocating <see cref="System.ReadOnlySpan{T}"/> of the elements in the <see cref="SpanMaker{T}"/>.</summary>
    public readonly System.ReadOnlySpan<T> AsReadOnlySpan() => new(m_array, m_head, m_tail - m_head);

    /// <summary>Creates a non-allocating <see cref="System.Span{T}"/> of the elements in the <see cref="SpanMaker{T}"/>. This provides partial direct access into the underlying array storage for the SpanBuilder.</summary>
    /// <remarks>Use with caution!</remarks>
    public readonly System.Span<T> AsSpan() => new(m_array, m_head, m_tail - m_head);

    public void Clear()
    {
      System.Array.Clear(m_array);

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }

    /// <summary>
    /// <para>Duplicates the elements satisfying the <paramref name="predicate"/> <paramref name="count"/> times.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanMaker<T> Duplicate(System.Func<T, bool> predicate, int count)
    {
      var sm = this;

      for (var index = 0; index < sm.Length; index++)
        if (sm[index] is var value && predicate(value))
        {
          sm = sm.Insert(index, count, value);

          index += count;
        }

      return sm;
    }

    internal (T[] array, int head, int tail) EnsureCapacityAppend(int needAppend)
    {
      var (array, head, tail) = this;

      if (FreeAppend > needAppend) // There is already room to prepend.
        return (array, head, tail + needAppend);
      else if (FreePrepend + FreeAppend > needAppend) // There is room, if current data is moved.
      {
        var offset = (needAppend - FreeAppend);
        System.Array.Copy(array, head, array, head - offset, Length);
        return (array, head - offset, array.Length);
      }

      var totalSize = FreePrepend + Length + needAppend + FreeAppend;

      array = System.Buffers.ArrayPool<T>.Shared.Rent(int.Max(totalSize, DefaultBufferSize).Pow2AwayFromZero(true));

      head = FreePrepend;
      tail = FreePrepend + Length + needAppend;

      System.Array.Copy(m_array, m_head, array, head, Length); // Copy old content.

      return (array, head, tail);
    }

    internal (T[] array, int head, int tail) EnsureCapacityInsert(int indexInsert, int needInsert)
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
        System.Array.Copy(m_array, m_head + indexInsert, array, m_head + indexInsert + (indexInsert + needInsert - indexArray), Length - indexInsert);

        return (array, 0, tail + needInsert - FreePrepend);
      }

      var totalSize = FreePrepend + Length + FreeAppend + needInsert;

      array = System.Buffers.ArrayPool<T>.Shared.Rent(int.Max(totalSize, DefaultBufferSize).Pow2AwayFromZero(true));

      head = FreePrepend;
      tail = head + Length + needInsert;

      System.Array.Copy(m_array, m_head, array, head, indexInsert);
      System.Array.Copy(m_array, m_head + indexInsert, array, tail - (Length - indexInsert), Length - indexInsert);

      return (array, head, tail);
    }

    internal (T[] array, int head, int tail) EnsureCapacityPrepend(int needPrepend)
    {
      var (array, head, tail) = this;

      if (needPrepend < FreePrepend) // There is already room to prepend.
        return (array, head - needPrepend, tail);
      else if (needPrepend < FreePrepend + FreeAppend) // There is room, if current data is moved.
      {
        var offset = (needPrepend - FreePrepend);
        System.Array.Copy(array, head, array, head + offset, Length);
        return (array, 0, tail + offset);
      }

      var totalSize = FreePrepend + needPrepend + Length + FreeAppend;

      array = System.Buffers.ArrayPool<T>.Shared.Rent(int.Max(totalSize, DefaultBufferSize).Pow2AwayFromZero(true));

      head = FreePrepend;
      tail = head + needPrepend + Length;

      System.Array.Copy(m_array, m_head, array, head + needPrepend, Length); // Copy old content.

      return (array, head, tail);
    }

    public SpanMaker<T> Insert(int index, SpanMaker<T> spanGlue, int count) => Insert(index, count, spanGlue.AsReadOnlySpan().ToArray().AsReadOnlySpan());

    public SpanMaker<T> Insert(int index, SpanMaker<T> spanGlue) => Insert(index, 1, spanGlue.AsReadOnlySpan().ToArray().AsReadOnlySpan());

    #region Insert

#if XNET9_0_OR_GREATER

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

    public SpanMaker<T> Insert(int index, params System.Collections.Generic.ICollection<T> collection) => Insert(index, 1, collection);

    public SpanMaker<T> Insert(int index, params System.ReadOnlySpan<T> span) => Insert(index, 1, span);

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

    #endregion // Insert

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
      if (Length >= 2)
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

#if XNET9_0_OR_GREATER

    /// <summary>
    /// <para>Pad evenly on both sides (with <paramref name="leftBias"/> if true, or right-bias if false) using <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> to the specified <paramref name="totalWidth"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="paddingLeft"></param>
    /// <param name="paddingRight"></param>
    /// <param name="leftBias"></param>
    /// <returns></returns>
    public SpanMaker<T> PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    {
      var sm = this;

      if (totalWidth > sm.Length)
      {
        var quotient = System.Math.DivRem(totalWidth - sm.Length, 2, out var remainder);

        sm = sm.PadLeft(sm.Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        sm = sm.PadRight(totalWidth, paddingRight);
      }

      return sm;
    }

    /// <summary>
    /// <para>Pad evenly on both sides (with <paramref name="leftBias"/> if true, or right-bias if false) using <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> to the specified <paramref name="totalWidth"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="paddingLeft"></param>
    /// <param name="paddingRight"></param>
    /// <param name="leftBias"></param>
    /// <returns></returns>
    public SpanMaker<T> PadEven(int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true) => PadEven(totalWidth, new T[] { paddingLeft }, new T[] { paddingRight }, leftBias);

    /// <summary>
    /// <para>Pad on the left using <paramref name="padding"/> to the specified <paramref name="totalWidth"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public SpanMaker<T> PadLeft(int totalWidth, params System.ReadOnlySpan<T> padding)
    {
      var sm = this;
      sm = sm.Prepend((totalWidth - sm.Length) / padding.Length + 1, padding);
      sm = sm.Remove(0, sm.Length - totalWidth);
      return sm;
    }

    /// <summary>
    /// <para>Pad on the right using <paramref name="padding"/> to the specified <paramref name="totalWidth"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public SpanMaker<T> PadRight(int totalWidth, params System.ReadOnlySpan<T> padding)
    {
      var sm = this;
      sm = sm.Append((totalWidth - sm.Length) / padding.Length + 1, padding);
      sm = sm.Remove(totalWidth);
      return sm;
    }

#else

    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public SpanMaker<T> PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    {
      var sm = this;

      if (totalWidth > sm.Length)
      {
        var quotient = System.Math.DivRem(totalWidth - sm.Length, 2, out var remainder);

        sm = sm.PadLeft(sm.Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        sm = sm.PadRight(totalWidth, paddingRight);
      }

      return sm;
    }

    /// <summary>
    /// <para>Pad evenly on both sides (with <paramref name="leftBias"/> if true, or right-bias if false) using <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> to the specified <paramref name="totalWidth"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="paddingLeft"></param>
    /// <param name="paddingRight"></param>
    /// <param name="leftBias"></param>
    /// <returns></returns>
    public SpanMaker<T> PadEven(int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true)
      => PadEven(totalWidth, new T[] { paddingLeft }, new T[] { paddingRight }, leftBias);

    /// <summary>Pads this <see cref="SpanMaker{T}"/> on the left with the specified padding string.</summary>
    public SpanMaker<T> PadLeft(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      var sm = this;
      sm = sm.Prepend((totalWidth - sm.Length) / padding.Length + 1, padding);
      sm = sm.Remove(0, sm.Length - totalWidth);
      return sm;
    }

    public SpanMaker<T> PadLeft(int totalWidth, T padding) => PadLeft(totalWidth, new T[] { padding });

    /// <summary>Pads this <see cref="SpanMaker{T}"/> on the right with the specified padding string.</summary>
    public SpanMaker<T> PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      var sm = this;
      sm = sm.Append((totalWidth - sm.Length) / padding.Length + 1, padding);
      sm = sm.Remove(totalWidth);
      return sm;
    }

    public SpanMaker<T> PadRight(int totalWidth, T padding) => PadRight(totalWidth, new T[] { padding });

#endif

    public SpanMaker<T> Prepend(int count, SpanMaker<T> other) => Prepend(count, other.AsReadOnlySpan());

    public SpanMaker<T> Prepend(SpanMaker<T> other) => Prepend(1, other.AsReadOnlySpan());

    #region Prepend

#if XNET9_0_OR_GREATER

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

    public SpanMaker<T> Prepend(params System.Collections.Generic.ICollection<T> collection) => Prepend(1, collection);

    public SpanMaker<T> Prepend(params System.ReadOnlySpan<T> span) => Prepend(1, span);

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

    /// <summary>Append<paramref name = "value" />.</ summary >
    public SpanMaker<T> Prepend(params T[] value) => Prepend(1, value.AsReadOnlySpan());

    /// <summary>Append<paramref name = "collection" />.</ summary >
    public SpanMaker<T> Prepend(System.Collections.Generic.ICollection<T> collection) => Prepend(1, collection);

    /// <summary>Append<paramref name = "span" />.</ summary >
    public SpanMaker<T> Prepend(System.ReadOnlySpan<T> span) => Prepend(1, span);

#endif

    #endregion // Prepend

    /// <summary>
    /// <para>Removes the specified range [index..(index + count)] of elements in the <see cref="SpanMaker{T}">.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public SpanMaker<T> Remove(int index, int count)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (count < 0 || index + count > Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      if (index == 0)
        return RemoveLeft(count);

      if (index + count == Length)
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

    public SpanMaker<T> Remove(System.Range range) => Remove(range.Start.Value, range.End.Value - range.Start.Value);

    /// <summary>
    /// <para>Removes the specified range [index..] of elements from the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    public SpanMaker<T> Remove(int index)
      // Direct code modifications, checks needed.
      => index < 0 || index >= Length
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
      => count < 0 || count > Length
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
      => count < 0 || count > Length
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : new(m_array, m_head, m_tail - count);

    /// <summary>
    /// <para>Repeats the content in a <see cref="SpanMaker{T}"/> <paramref name="count"/> times.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanMaker<T> Repeat(int count) => Append(count, AsReadOnlySpan().ToArray().AsReadOnlySpan());

#if XNET9_0_OR_GREATER

    /// <summary>
    /// <para>Replaces all elements satisfying the <paramref name="predicate"/> with <paramref name="count"/> of <paramref name="replacement"/> in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public SpanMaker<T> Replace(System.Func<T, bool> predicate, int count, params System.ReadOnlySpan<T> replacement)
    {
      var sm = this;

      for (var i = sm.Length - 1; i >= 0; i--)
        if (predicate(sm[i]))
        {
          sm = sm.Remove(i, 1);
          sm = sm.Insert(i, count, replacement);
        }

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
          sm = sm.Remove(i, 1).Insert(i, count, replacement);

      return sm;
    }

#endif

    public SpanMaker<T> ReplaceIfEqualAt(int startAt, System.ReadOnlySpan<T> find, System.ReadOnlySpan<T> replacement, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sm = this;

      if (AsReadOnlySpan().EqualsAt(startAt, find, equalityComparer))
        sm = sm.Remove(startAt, find.Length).Insert(startAt, 1, replacement);

      return sm;
    }

    /// <summary>
    /// <para>Create a new <see cref="SpanMaker{T}"/> from a subset of a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public SpanMaker<T> Slice(int index, int length)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (length < 0 || index + length > Length) throw new System.ArgumentOutOfRangeException(nameof(length));

      return new(m_array, m_head + index, m_head + index + length);
    }

    /// <summary>
    /// <para>Swaps two elements at the specified [<paramref name="indexA"/>] and [<paramref name="indexB"/>] in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    /// <returns></returns>
    public bool Swap(int indexA, int indexB) => AsSpan().Swap(indexA, indexB);

    /// <summary>
    /// <para>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the beginning of the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    public SpanMaker<T> TrimLeft(System.Func<T, bool> predicate) => new(m_array, m_head + AsReadOnlySpan().CommonPrefixLength(0, predicate), m_tail);

    /// <summary>
    /// <para>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the end of the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    public SpanMaker<T> TrimRight(System.Func<T, bool> predicate) => new(m_array, m_head, m_tail - AsReadOnlySpan().CommonSuffixLength(0, predicate));


    /// <summary>Remove the specified wrapping characters from the source, if they exist. E.g. brackets, or parenthesis.</summary>
    public SpanMaker<T> Unwrap(T left, T right, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sm = this;
      if (AsReadOnlySpan().IsWrapped(left, right, equalityComparer))
      {
        sm = sm.Remove(0, 1);
        sm = sm.Remove(sm.Length - 1, 1);
      }
      return sm;
    }

    /// <summary>Remove the specified wrap strings from the source, if they exist.</summary>
    public SpanMaker<T> Unwrap(System.ReadOnlySpan<T> left, System.ReadOnlySpan<T> right, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sm = this;
      if (AsReadOnlySpan().IsWrapped(left, right, equalityComparer))
      {
        sm = sm.Remove(0, left.Length);
        sm = sm.Remove(sm.Length - right.Length, right.Length);
      }
      return sm;
    }

    /// <summary>Add the specified wrap characters to the source. E.g. brackets, or parenthesis.</summary>
    public SpanMaker<T> Wrap(T left, T right)
    {
      var sm = this;
      sm = sm.Insert(0, 1, left);
      sm = sm.Append(right);
      return sm;
    }

    /// <summary>Add the specified wrap strings to the source.</summary>
    public SpanMaker<T> Wrap(System.ReadOnlySpan<T> left, System.ReadOnlySpan<T> right)
    {
      var sm = this;
      sm = sm.Insert(0, 1, left);
      sm = sm.Append(right);
      return sm;
    }


    //public string ToString(int index, int length)
    //  => AsReadOnlySpan().Slice(index, length).ToString();

    public override string ToString() => AsReadOnlySpan().ToString();

    //public System.Collections.Generic.IEnumerator<T> GetEnumerator() => AsReadOnlySpan().ToArray().AsEnumerable().GetEnumerator();
    //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
