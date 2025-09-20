namespace Flux
{
  public ref struct SpanMaker<T>
  {
    private const int DefaultBufferSize = 2;

    private readonly T[] m_array;

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

    #region Append

    public SpanMaker<T> Append(int count, SpanMaker<T> other) => Append(count, other.AsReadOnlySpan());

    public SpanMaker<T> Append(SpanMaker<T> other) => Append(1, other.AsReadOnlySpan());

    //#if NET9_0_OR_GREATER

    //    /// <summary>Append <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    //    public SpanMaker<T> Append(int count, params System.Collections.Generic.ICollection<T> collection)
    //    {
    //      var totalAppend = collection.Count * count;

    //      var (array, head, tail) = EnsureCapacityAppend(totalAppend);

    //      var index = tail - totalAppend;

    //      while (count-- > 0)
    //      {
    //        collection.CopyTo(array, index);

    //        index += collection.Count;
    //      }

    //      return new(array, head, tail);
    //    }

    //    /// <summary>Append <paramref name="span"/>, <paramref name="count"/> times.</summary>
    //    public SpanMaker<T> Append(int count, params System.ReadOnlySpan<T> span)
    //    {
    //      var totalAppend = span.Length * count;

    //      var (array, head, tail) = EnsureCapacityAppend(totalAppend);

    //      var index = tail - totalAppend;

    //      while (count-- > 0)
    //      {
    //        span.CopyTo(array.AsSpan().Slice(index, span.Length));

    //        index += span.Length;
    //      }

    //      return new(array, head, tail);
    //    }

    //    /// <summary>Append <paramref name="collection"/>, <paramref name="count"/> times.</summary>
    //    public SpanMaker<T> Append(params System.Collections.Generic.ICollection<T> collection) => Append(1, collection);

    //    /// <summary>Append <paramref name="span"/>, <paramref name="count"/> times.</summary>
    //    public SpanMaker<T> Append(params System.ReadOnlySpan<T> span) => Append(1, span);

    //#else

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

    //#endif

    #endregion // Append

    /// <summary>Creates a non-allocating <see cref="System.ReadOnlySpan{T}"/> of the elements in the <see cref="SpanMaker{T}"/>.</summary>
    public readonly System.ReadOnlySpan<T> AsReadOnlySpan() => new(m_array, m_head, m_tail - m_head);

    /// <summary>Creates a non-allocating <see cref="System.Span{T}"/> of the elements in the <see cref="SpanMaker{T}"/>. This provides partial direct access into the underlying array storage for the <see cref="SpanMaker{T}"/>.</summary>
    /// <remarks>Use with caution!</remarks>
    public readonly System.Span<T> AsSpan() => new(m_array, m_head, m_tail - m_head);

    public void Clear()
    {
      System.Array.Clear(m_array);

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }

    #region Duplicate methods

    /// <summary>
    /// <para>Duplicates all elements satisfying the <paramref name="predicate"/>, <paramref name="count"/> times, in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public readonly SpanMaker<T> Duplicate(System.Func<T, bool> predicate, int count)
    {
      var totalDuplicatesToInsert = 0;
      for (var index = m_head; index < m_tail; index++)
        if (predicate(m_array[index]))
          totalDuplicatesToInsert += count;

      var (array, head, tail) = EnsureCapacityAppend(totalDuplicatesToInsert);

      var targetIndex = tail;
      var sourceIndex = tail - totalDuplicatesToInsert;

      while (targetIndex > sourceIndex)
      {
        var sc = array[--sourceIndex];

        if (predicate(sc))
        {
          targetIndex -= count;

          System.Array.Fill(array, sc, --targetIndex, count + 1);
        }
        else
          array[--targetIndex] = sc;
      }

      return new SpanMaker<T>(array, head, tail);
    }

    /// <summary>
    /// <para>Duplicates all elements that equals any <paramref name="values"/>, <paramref name="count"/> times, in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public readonly SpanMaker<T> Duplicate(int count, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, params T[] values) => Duplicate(c => values.Contains(c, equalityComparer), count);

    #endregion // Duplicate methods

    #region Insert

    /// <summary>
    /// <para>Inserts all elements from <paramref name="another"/> span-maker, <paramref name="count"/> times, into the <see cref="SpanMaker{T}"/> starting at <paramref name="index"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="another"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanMaker<T> Insert(int index, SpanMaker<T> another, int count) => Insert(index, count, another.AsReadOnlySpan().ToArray().AsReadOnlySpan());

    /// <summary>
    /// <para>Inserts all elements from <paramref name="another"/> span-maker into the <see cref="SpanMaker{T}"/> starting at <paramref name="index"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="another"></param>
    /// <returns></returns>
    public SpanMaker<T> Insert(int index, SpanMaker<T> another) => Insert(index, 1, another.AsReadOnlySpan().ToArray().AsReadOnlySpan());

#if NET9_0_OR_GREATER

    /// <summary>
    /// <para>Insert all elements from a <paramref name="collection"/>, <paramref name="count"/> times, into the <see cref="SpanMaker{T}"/> starting at <paramref name="index"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
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

    /// <summary>
    /// <para>Insert all elements from a <paramref name="span"/>, <paramref name="count"/> times, into the <see cref="SpanMaker{T}"/> starting at <paramref name="index"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <param name="span"></param>
    /// <returns></returns>
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

    /// <summary>
    /// <para>Insert all elements from a <paramref name="collection"/> into the <see cref="SpanMaker{T}"/> starting at <paramref name="index"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public SpanMaker<T> Insert(int index, params System.Collections.Generic.ICollection<T> collection) => Insert(index, 1, collection);

    /// <summary>
    /// <para>Insert all elements from a <paramref name="span"/> into the <see cref="SpanMaker{T}"/> starting at <paramref name="index"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="span"></param>
    /// <returns></returns>
    public SpanMaker<T> Insert(int index, params System.ReadOnlySpan<T> span) => Insert(index, 1, span);

#else

    /// <summary>Insert <paramref name="count"/> of <paramref name="value"/> starting at <paramref name="index"/>.</summary>
    public SpanMaker<T> Insert(int index, int count, T value)
    {
      var (array, head, tail) = EnsureCapacityInsert(index, count);

      System.Array.Fill(array, value, index + head, count);

      return new(array, head, tail);
    }

    public SpanMaker<T> Insert(int index, T value) => Insert(index, 1, value);

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

    public SpanMaker<T> Insert(int index, System.Collections.Generic.ICollection<T> collection) => Insert(index, 1, collection);

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

    public SpanMaker<T> Insert(int index, System.ReadOnlySpan<T> span) => Insert(index, 1, span);

#endif

    #endregion // Insert

    #region (internal) EnsureCapacity methods

    //readonly internal void EnsureCapacityAppendTest(int needAppend, out T[] array, out int head, out int tail)
    //{
    //  (array, head, tail) = this;

    //  if (FreeAppend > needAppend) // There is already room to prepend.
    //  {
    //    tail += needAppend;
    //    return;
    //  }
    //  else if (FreePrepend + FreeAppend > needAppend) // There is room, if current data is moved.
    //  {
    //    var offset = (needAppend - FreeAppend);
    //    System.Array.Copy(array, head, array, head - offset, Length);
    //    head -= offset;
    //    tail = array.Length;
    //    return;
    //  }

    //  var totalSize = FreePrepend + Length + needAppend + FreeAppend;

    //  array = System.Buffers.ArrayPool<T>.Shared.Rent(int.Max(totalSize, DefaultBufferSize).Pow2AwayFromZero(true));

    //  head = FreePrepend;
    //  tail = FreePrepend + Length + needAppend;

    //  System.Array.Copy(m_array, m_head, array, head, Length); // Copy old content.

    //  return;
    //}

    readonly internal (T[] array, int head, int tail) EnsureCapacityAppend(int needAppend)
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

    readonly internal (T[] array, int head, int tail) EnsureCapacityInsert(int indexInsert, int needInsert)
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

    readonly internal (T[] array, int head, int tail) EnsureCapacityPrepend(int needPrepend)
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

    #endregion // EnsureCapacity methods

    #region (internal) AssertValid methods

    readonly internal void AssertValidIndexAndLength(int index, int length)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (length < 0 || index + length > Length) throw new System.ArgumentOutOfRangeException(nameof(length));
    }

    //readonly internal void AssertValidSlice(Slice slice)
    //  => AssertValidIndexAndLength(slice.Index, slice.Length);

    #endregion // AssertValid methods

    #region Normalization methods

    /// <summary>
    /// <para>Normalize (reduce count of) any consecutive elements from <paramref name="values"/> (or all, if empty or null) that occur more than <paramref name="maxAdjacentCount"/> in the <see cref="SpanMaker{T}"/>.</para>
    /// <para>Uses the specfied <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="maxAdjacentCount">The maximum number of adjacent/consecutive items to allow.</param>
    /// <param name="equalityComparer">The equality comparer to use, or <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> if null.</param>
    /// <param name="values">The items to normalize.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    public SpanMaker<T> NormalizeAdjacent(int maxAdjacentCount, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, bool any = false, params T[] values)
    {
      if (maxAdjacentCount < 1) throw new System.ArgumentNullException(nameof(maxAdjacentCount));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var normalizedTail = m_head;

      T previous = default!;
      var adjacentLength = 1;

      for (var i = m_head; i < m_tail; i++)
      {
        var current = m_array[i];

        var isEqual = any
          ? values.Contains(current, equalityComparer) && values.Contains(previous, equalityComparer)
          : values.Contains(current, equalityComparer) && equalityComparer.Equals(current, previous);

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

    /// <summary>
    /// <para>Normalize all adjacent duplicates. Uses the specified <paramref name="equalityComparer"/>.</para>
    /// </summary>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public SpanMaker<T> NormalizeDuplicates(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
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

    /// <summary>
    /// <para>Normalize any one or more consecutive elements satisfying the <paramref name="predicate"/> to a single <paramref name="replacement"/> element.</para>
    /// <para><c>"".Normalize(char.IsWhiteSpace, ' ');</c></para>
    /// <para><c>"".Normalize(c => c == ' ', ' ');</c></para>
    /// </summary>
    /// <remarks>Normalizing (here) means removing leading/trailing (i.e. on either end) and replacing one or more consecutive characters satisfying the <paramref name="predicate"/> within the <see cref="SpanMaker{T}"/> to one instance of <paramref name="replacement"/>. No replacements, i.e. only removals, are performed if elements are on left or right edge.</remarks>
    public SpanMaker<T> NormalizeReplace(System.Func<T, bool> predicate, T replacement)
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

    #endregion // Normalization methods

    #region Pad (even/left/right) methods

    //#if NET9_0_OR_GREATER

    //    /// <summary>
    //    /// <para>Pad evenly on both sides (with <paramref name="leftBias"/> if true, or right-bias if false) using <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> to the specified <paramref name="totalWidth"/> in the <see cref="SpanMaker{T}"/>.</para>
    //    /// </summary>
    //    /// <param name="totalWidth"></param>
    //    /// <param name="paddingLeft"></param>
    //    /// <param name="paddingRight"></param>
    //    /// <param name="leftBias"></param>
    //    /// <returns></returns>
    //    public SpanMaker<T> PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    //    {
    //      var sm = this;

    //      if (totalWidth > sm.Length)
    //      {
    //        var quotient = System.Math.DivRem(totalWidth - sm.Length, 2, out var remainder);

    //        sm = sm.PadLeft(sm.Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
    //        sm = sm.PadRight(totalWidth, paddingRight);
    //      }

    //      return sm;
    //    }

    //    /// <summary>
    //    /// <para>Pad evenly on both sides (with <paramref name="leftBias"/> if true, or right-bias if false) using <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> to the specified <paramref name="totalWidth"/> in the <see cref="SpanMaker{T}"/>.</para>
    //    /// </summary>
    //    /// <param name="totalWidth"></param>
    //    /// <param name="paddingLeft"></param>
    //    /// <param name="paddingRight"></param>
    //    /// <param name="leftBias"></param>
    //    /// <returns></returns>
    //    public SpanMaker<T> PadEven(int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true) => PadEven(totalWidth, new T[] { paddingLeft }, new T[] { paddingRight }, leftBias);

    //    /// <summary>
    //    /// <para>Pad on the left using <paramref name="padding"/> to the specified <paramref name="totalWidth"/> in the <see cref="SpanMaker{T}"/>.</para>
    //    /// </summary>
    //    /// <param name="totalWidth"></param>
    //    /// <param name="padding"></param>
    //    /// <returns></returns>
    //    public SpanMaker<T> PadLeft(int totalWidth, params System.ReadOnlySpan<T> padding)
    //    {
    //      var sm = this;
    //      sm = sm.Prepend((totalWidth - sm.Length) / padding.Length + 1, padding);
    //      sm = sm.Remove(0, sm.Length - totalWidth);
    //      return sm;
    //    }

    //    /// <summary>
    //    /// <para>Pad on the right using <paramref name="padding"/> to the specified <paramref name="totalWidth"/> in the <see cref="SpanMaker{T}"/>.</para>
    //    /// </summary>
    //    /// <param name="totalWidth"></param>
    //    /// <param name="padding"></param>
    //    /// <returns></returns>
    //    public SpanMaker<T> PadRight(int totalWidth, params System.ReadOnlySpan<T> padding)
    //    {
    //      var sm = this;
    //      sm = sm.Append((totalWidth - sm.Length) / padding.Length + 1, padding);
    //      sm = sm.Remove(totalWidth);
    //      return sm;
    //    }

    //#else

    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public SpanMaker<T> PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    {
      var sm = this;

      if (totalWidth > sm.Length)
      {
        var (quotient, remainder) = int.DivRem(totalWidth - sm.Length, 2);

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
      var totalPrepend = totalWidth - Length;

      var (array, head, tail) = EnsureCapacityPrepend(totalPrepend);

      var index = head;

      while (totalPrepend > padding.Length)
      {
        padding.CopyTo(array.AsSpan(index, padding.Length));

        index += padding.Length;
        totalPrepend -= padding.Length;
      }

      if (totalPrepend > 0)
        padding[..totalPrepend].CopyTo(array.AsSpan(index, totalPrepend));

      return new(array, head, tail);
    }

    public SpanMaker<T> PadLeft(int totalWidth, T padding)
    {
      var totalPrepend = totalWidth - Length;

      var (array, head, tail) = EnsureCapacityPrepend(totalPrepend);

      System.Array.Fill(array, padding, head, totalPrepend);

      return new(array, head, tail);
    }

    /// <summary>Pads this <see cref="SpanMaker{T}"/> on the right with the specified padding string.</summary>
    public SpanMaker<T> PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      var totalAppend = totalWidth - Length;

      var (array, head, tail) = EnsureCapacityAppend(totalAppend);

      var index = tail - totalAppend;

      while (totalAppend > padding.Length)
      {
        padding.CopyTo(array.AsSpan(index, padding.Length));

        index += padding.Length;
        totalAppend -= padding.Length;
      }

      if (totalAppend > 0)
        padding[..totalAppend].CopyTo(array.AsSpan(index));

      return new(array, head, tail);
    }

    public SpanMaker<T> PadRight(int totalWidth, T padding)
    {
      var totalAppend = totalWidth - Length;

      var (array, head, tail) = EnsureCapacityAppend(totalAppend);

      System.Array.Fill(array, padding, tail - totalAppend, totalAppend);

      return new(array, head, tail);
    }

    //#endif

    #endregion // Pad (even/left/right) methods

    #region Prepend

    public SpanMaker<T> Prepend(int count, SpanMaker<T> other) => Prepend(count, other.AsReadOnlySpan());

    public SpanMaker<T> Prepend(SpanMaker<T> other) => Prepend(1, other.AsReadOnlySpan());

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

    #region Remove methods

    /// <summary>
    /// <para>Removes the specified range [index..(index + count)] of elements in the <see cref="SpanMaker{T}">.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public SpanMaker<T> Remove(int index, int length)
    {
      AssertValidIndexAndLength(index, length);

      if (index == 0)
        return RemoveLeft(length);

      if (index + length == Length)
        return RemoveRight(length);

      var mark = index + m_head;

      if (m_head <= m_array.Length - m_tail) // Shrink from start.
      {
        System.Array.Copy(m_array, m_head, m_array, m_head + length, mark - m_head);

        return new(m_array, m_head + length, m_tail);
      }
      else // Otherwise shrink from end.
      {
        System.Array.Copy(m_array, mark + length, m_array, mark, m_tail - mark - length);

        return new(m_array, m_head, m_tail - length);
      }
    }

    public SpanMaker<T> Remove(Range range)
    {
      var (offset, length) = range.GetOffsetAndLength(Length);

      return Remove(offset, length);
    }

    ///// <summary>
    ///// <para>Removes the specified range [index..] of elements from the <see cref="SpanMaker{T}"/>.</para>
    ///// </summary>
    //public SpanMaker<T> Remove(int index)
    //{
    //  if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));

    //  return new(m_array, m_head, m_head + index);
    //}

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

    /// <summary>
    /// <para>Removes <paramref name="count"/> elements on the left (at the beginning) of a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public readonly SpanMaker<T> RemoveLeft(int count)
    {
      if (count < 0 || count > Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      return new(m_array, m_head + count, m_tail);
    }

    /// <summary>
    /// <para>Removes <paramref name="count"/> elements on the right (at the end) of a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public readonly SpanMaker<T> RemoveRight(int count)
    {
      if (count < 0 || count > Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      return new(m_array, m_head, m_tail - count);
    }

    #endregion // Remove methods

    /// <summary>
    /// <para>Repeats the content in a <see cref="SpanMaker{T}"/> <paramref name="count"/> times.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanMaker<T> Repeat(int count)
    {
      var repeatLength = Length;

      var totalAppend = count * repeatLength;

      var (array, head, tail) = EnsureCapacityAppend(totalAppend);

      var index = tail - totalAppend;

      while (--count >= 0)
      {
        System.Array.Copy(array, head, array, index, repeatLength);

        index += repeatLength;
      }

      return new(array, head, tail);
    }

    #region Replace methods

    /// <summary>
    /// <para>Replaces the <paramref name="length"/> elements from <paramref name="index"/> with <paramref name="replacement"/> in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public SpanMaker<T> Replace(int index, int length, System.ReadOnlySpan<T> replacement)
    {
      AssertValidIndexAndLength(index, length);

      if (replacement.Length is var replacementLength && replacementLength <= length)
      {
        replacement.CopyTo(m_array.AsSpan(m_head + index, replacementLength));

        if (replacementLength < length)
          return Remove(index + replacementLength, length - replacementLength);

        return this;
      }
      else
      {
        var (array, head, tail) = EnsureCapacityInsert(index, replacementLength - length);
        replacement.CopyTo(array.AsSpan(head + index, replacementLength));
        return new(array, head, tail);
      }
    }

    /// <summary>
    /// <para>Replaces the <paramref name="slice"/> with <paramref name="replacement"/> in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="slice"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public SpanMaker<T> Replace(System.Range range, System.ReadOnlySpan<T> replacement)
    {
      var (offset, length) = range.GetOffsetAndLength(Length);

      return Replace(offset, length, replacement);
    }

    /// <summary>
    /// <para>Replaces all elements satisfying the <paramref name="predicate"/> with <paramref name="count"/> of <paramref name="replacement"/> in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="count"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public SpanMaker<T> Replace(System.Func<T, bool> predicate, System.ReadOnlySpan<T> replacement)
    {
      var sm = this;

      for (var i = sm.Length - 1; i >= 0; i--)
        if (predicate(sm[i]))
          sm = sm.Replace(i, 1, replacement);

      return sm;
    }

    public SpanMaker<T> ReplaceIfEqualAt(int startAt, System.ReadOnlySpan<T> find, System.ReadOnlySpan<T> replacement, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sm = this;

      if (AsReadOnlySpan()[startAt..].IsCommonPrefix(find, equalityComparer))
        sm = sm.Replace(startAt, find.Length, replacement);

      return sm;
    }

    #endregion // Replace methods

    /// <summary>
    /// <para>Reverses the content of a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public SpanMaker<T> Reverse(int index, int length)
    {
      AssertValidIndexAndLength(index, length);

      var left = index;
      var right = index + length;

      while (left < right)
        Swap(left++, --right);

      return this;
    }

    /// <summary>
    /// <para>Create a new <see cref="SpanMaker{T}"/> from <paramref name="index"/> and <paramref name="length"/> of a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public readonly SpanMaker<T> Slice(int index, int length)
    {
      AssertValidIndexAndLength(index, length);

      return new(m_array, m_head + index, m_head + index + length);
    }

    /// <summary>
    /// <para>Create a new <see cref="SpanMaker{T}"/> from <paramref name="index"/> to the end of a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public readonly SpanMaker<T> Slice(int index) => RemoveLeft(index);

    /// <summary>
    /// <para>Swaps two elements at the specified [<paramref name="indexA"/>] and [<paramref name="indexB"/>] in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    /// <returns></returns>
    public bool Swap(int indexA, int indexB) => AsSpan().Swap(indexA, indexB);

    #region Trim methods

    /// <summary>
    /// <para>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the beginning of the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    public SpanMaker<T> TrimLeft(System.Func<T, bool> predicate) => RemoveLeft(AsReadOnlySpan().CommonPrefixLength(predicate));

    /// <summary>
    /// <para>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the end of the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    public SpanMaker<T> TrimRight(System.Func<T, bool> predicate) => RemoveRight(AsReadOnlySpan().CommonSuffixLength(predicate));

    #endregion // Trim methods

    #region Wrap methods

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

    #endregion // Wrap methods

    //public string ToString(int index, int length)
    //  => AsReadOnlySpan().Slice(index, length).ToString();

    public override readonly string ToString() => AsReadOnlySpan().ToString();

    //public System.Collections.Generic.IEnumerator<T> GetEnumerator() => AsReadOnlySpan().ToArray().AsEnumerable().GetEnumerator();
    //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
