namespace Flux
{
  public sealed class SpanBuilder<T>
    : System.Collections.Generic.IEnumerable<T>
  {
    private const int DefaultBufferSize = 32;

    public T[] m_array;

    private int m_head; // Start of buffer data.
    private int m_tail; // End of buffer data.

    public SpanBuilder(int capacity)
    {
      m_array = capacity >= 1 ? System.Buffers.ArrayPool<T>.Shared.Rent(BinaryInteger.RoundUpToPowerOf2(int.Max(capacity, DefaultBufferSize), false)) : [];

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }

    public SpanBuilder() : this(DefaultBufferSize) { }

    public SpanBuilder(System.ReadOnlySpan<T> ros) : this(ros.Length) { Append(ros); }

    /// <summary>
    /// <para>Gets or sets the element at the specified index in a <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public T this[int index]
    {
      get
      {
        System.Index.AssertInRange(index, Length);

        return m_array[m_head + index];
      }

      set
      {
        System.Index.AssertInRange(index, Length);

        m_array[m_head + index] = value;
      }
    }

    /// <summary>
    /// <para>The unused capacity on the append (left) side of a <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    private int FreeAppend => m_array.Length - m_tail;

    /// <summary>
    /// <para>The unused capacity on the prepend (right) side of a <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    private int FreePrepend => m_head;

    /// <summary>
    /// <para>Gets the number of elements in a <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    public int Length => m_tail - m_head;

    #region Append

    public SpanBuilder<T> Append(int length)
    {
      var appendArrayIndex = EnsureCapacityAppend(length);

      System.Array.Clear(m_array, appendArrayIndex, length);

      return this;
    }

    public SpanBuilder<T> Append(T item, int count = 1)
    {
      var appendArrayIndex = EnsureCapacityPrepend(count);

      while (count-- > 0)
      {
        m_array[appendArrayIndex] = item;

        appendArrayIndex++;
      }

      return this;
    }

    /// <summary>
    /// <para>Append <paramref name="collection"/> <paramref name="count"/> times to the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanBuilder<T> Append(System.Collections.Generic.ICollection<T> collection, int count = 1)
    {
      var appendArrayIndex = EnsureCapacityAppend(count * collection.Count);

      while (count-- > 0)
      {
        collection.CopyTo(m_array, appendArrayIndex);

        appendArrayIndex += collection.Count;
      }

      return this;
    }

    /// <summary>
    /// <para>Append <paramref name="span"/> <paramref name="count"/> times to the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="span"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanBuilder<T> Append(System.ReadOnlySpan<T> span, int count = 1)
    {
      var appendArrayIndex = EnsureCapacityAppend(count * span.Length);

      while (count-- > 0)
      {
        span.CopyTo(m_array.AsSpan().Slice(appendArrayIndex, span.Length));

        appendArrayIndex += span.Length;
      }

      return this;
    }

    //#endif

    #endregion // Append

    #region As..

    /// <summary>Creates a non-allocating <see cref="System.ReadOnlySpan{T}"/> of the elements in the <see cref="SpanBuilder{T}"/>.</summary>
    public System.ReadOnlySpan<T> AsReadOnlySpan() => new(m_array, m_head, Length);

    /// <summary>Creates a non-allocating <see cref="System.Span{T}"/> of the elements in the <see cref="SpanBuilder{T}"/>. This provides partial direct access into the underlying array storage for the <see cref="SpanBuilder{T}"/>.</summary>
    /// <remarks>Use with caution!</remarks>
    public System.Span<T> AsSpan() => new(m_array, m_head, Length);

    #endregion

    #region Clear

    public void Clear()
    {
      System.Array.Clear(m_array);

      m_head = m_array.Length / 2;
      m_tail = m_array.Length / 2;
    }

    #endregion

    #region EnsureCapacity..

    private int EnsureCapacityAppend(int needAppend)
    {
      var totalNeed = Length + needAppend;

      if (FreeAppend >= needAppend) // There is already room to prepend.
        m_tail += needAppend;
      else if (FreePrepend + FreeAppend >= needAppend) // There is room, if current data is moved.
      {
        var head = (m_array.Length - totalNeed) / 2;
        var tail = head + Length + needAppend;

        System.Array.Copy(m_array, m_head, m_array, head, Length);

        (m_head, m_tail) = (head, tail);
      }
      else
      {
        var totalSize = FreePrepend + Length + needAppend + FreeAppend;

        var array = System.Buffers.ArrayPool<T>.Shared.Rent(BinaryInteger.RoundUpToPowerOf2(int.Max(totalSize, DefaultBufferSize), true));

        var head = (m_array.Length - totalNeed) / 2;
        var tail = head + Length + needAppend;

        System.Array.Copy(m_array, m_head, array, head, Length); // Copy old content.

        (m_array, m_head, m_tail) = (array, head, tail);

        System.Buffers.ArrayPool<T>.Shared.Return(m_array);
      }

      return m_tail - needAppend;
    }

    private int EnsureCapacityInsert(int indexInsert, int needInsert)
    {
      var totalNeed = Length + needInsert;

      if (FreePrepend >= needInsert) // Available on prepend side.
      {
        System.Array.Copy(m_array, m_head, m_array, m_head - needInsert, indexInsert);

        m_head -= needInsert;
      }
      else if (FreeAppend >= needInsert) // Available on append side.
      {
        var startInsert = m_head + indexInsert;

        System.Array.Copy(m_array, startInsert, m_array, startInsert + needInsert, m_tail - startInsert);

        m_tail += needInsert;
      }
      else if (FreePrepend + FreeAppend >= needInsert) // Available overall.
      {
        var head = (m_array.Length - totalNeed) / 2;
        var tail = head + needInsert + Length;

        System.Array.Copy(m_array, m_head, m_array, head, indexInsert);
        System.Array.Copy(m_array, m_head + indexInsert, m_array, tail - (Length - indexInsert), Length - indexInsert);

        (m_head, m_tail) = (head, tail);
      }
      else
      {
        var totalSize = FreePrepend + Length + FreeAppend + needInsert;

        var array = System.Buffers.ArrayPool<T>.Shared.Rent(BinaryInteger.RoundUpToPowerOf2(int.Max(totalSize, DefaultBufferSize), true));

        var head = (array.Length - totalNeed) / 2;
        var tail = head + Length + needInsert;

        System.Array.Copy(m_array, m_head, array, head, indexInsert);
        System.Array.Copy(m_array, m_head + indexInsert, array, tail - (Length - indexInsert), Length - indexInsert);

        (m_array, m_head, m_tail) = (array, head, tail);

        System.Buffers.ArrayPool<T>.Shared.Return(m_array);
      }

      return m_head + indexInsert;
    }

    private int EnsureCapacityPrepend(int needPrepend)
    {
      var totalNeed = Length + needPrepend;

      if (FreePrepend >= needPrepend) // There is already room to prepend.
        m_head -= needPrepend;
      else if (FreePrepend + FreeAppend >= needPrepend) // There is room, if current data is moved.
      {
        var head = (m_array.Length - totalNeed) / 2;
        var tail = head + Length + needPrepend;

        System.Array.Copy(m_array, m_head, m_array, needPrepend + head, Length);

        (m_head, m_tail) = (head, tail);
      }
      else
      {
        var totalSize = FreePrepend + needPrepend + Length + FreeAppend;

        var array = System.Buffers.ArrayPool<T>.Shared.Rent(BinaryInteger.RoundUpToPowerOf2(int.Max(totalSize, DefaultBufferSize), true));

        var head = (array.Length - totalNeed) / 2;
        var tail = head + needPrepend + Length;

        System.Array.Copy(m_array, m_head, array, needPrepend + head, Length); // Copy old content.

        (m_array, m_head, m_tail) = (array, head, tail);

        System.Buffers.ArrayPool<T>.Shared.Return(m_array);
      }

      return m_head;
    }

    #endregion

    #region GetRandom

    /// <summary>
    /// <para>Returns a random element from the <paramref name="source"/>. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public T GetRandom(System.Random? rng = null)
      => m_array[m_head + (rng ?? System.Random.Shared).Next(Length)];

    /// <summary>
    /// <para>Attempts to fetch a random element from the <paramref name="source"/> into <paramref name="result"/> and returns whether successful. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
    /// </summary>
    public bool TryGetRandom(out T result, System.Random? rng = null)
    {
      try
      {
        result = GetRandom(rng);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    #endregion

    #region Insert

    public SpanBuilder<T> Insert(int index, int length)
    {
      System.Index.AssertInRange(index, Length);

      var insertArrayIndex = EnsureCapacityInsert(index, length);

      System.Array.Clear(m_array, insertArrayIndex, length);

      return this;
    }

    public SpanBuilder<T> Insert(int index, T fill, int length)
    {
      System.Index.AssertInRange(index, Length);

      var insertArrayIndex = EnsureCapacityInsert(index, length);

      while (--length >= 0)
        m_array[insertArrayIndex++] = fill;

      return this;
    }

    /// <summary>
    /// <para>Insert all elements from a <paramref name="collection"/>, <paramref name="count"/> times, into the <see cref="SpanBuilder{T}"/> starting at <paramref name="index"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public SpanBuilder<T> Insert(int index, System.Collections.Generic.ICollection<T> collection)
    {
      System.Index.AssertInRange(index, Length);

      var insertArrayIndex = EnsureCapacityInsert(index, collection.Count);

      collection.CopyTo(m_array, insertArrayIndex);

      return this;
    }

    /// <summary>
    /// <para>Insert all elements from a <paramref name="span"/>, <paramref name="count"/> times, into the <see cref="SpanBuilder{T}"/> starting at <paramref name="index"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <param name="span"></param>
    /// <returns></returns>
    public SpanBuilder<T> Insert(int index, params System.ReadOnlySpan<T> span)
    {
      System.Index.AssertInRange(index, Length);

      var insertArrayIndex = EnsureCapacityInsert(index, span.Length);

      span.CopyTo(m_array.AsSpan().Slice(insertArrayIndex, span.Length));

      return this;
    }

    #endregion

    #region NormalizeConsecutive

    ///// <summary>
    ///// <para>Normalize (reduce count of) any consecutive elements from <paramref name="values"/> (or all, if empty or null) that occur more than <paramref name="maxAdjacentCount"/> in the <see cref="SpanBuilder{T}"/>.</para>
    ///// <para>Uses the specfied <paramref name="equalityComparer"/>, or default if null.</para>
    ///// </summary>
    ///// <param name="maxAdjacentCount">The maximum number of adjacent/consecutive items to allow.</param>
    ///// <param name="equalityComparer">The equality comparer to use, or <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> if null.</param>
    ///// <param name="values">The items to normalize.</param>
    ///// <returns></returns>
    ///// <exception cref="System.ArgumentNullException"></exception>
    //public SpanBuilder<T> NormalizeAdjacent(int maxAdjacentCount, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, bool any = false, params T[] values)
    //{
    //  if (maxAdjacentCount < 1) throw new System.ArgumentNullException(nameof(maxAdjacentCount));

    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  var normalizedTail = m_head;

    //  T previous = default!;
    //  var adjacentLength = 1;

    //  for (var i = m_head; i < m_tail; i++)
    //  {
    //    var current = m_array[i];

    //    var isEqual = values.Contains(current, equalityComparer) && (any ? values.Contains(previous, equalityComparer) : equalityComparer.Equals(current, previous));

    //    if (!isEqual || adjacentLength < maxAdjacentCount)
    //    {
    //      m_array[normalizedTail++] = current;

    //      previous = current;
    //    }

    //    adjacentLength = !isEqual ? 1 : adjacentLength + 1;
    //  }

    //  m_tail = normalizedTail;

    //  return this;
    //}

    ///// <summary>
    ///// <para>Normalize all adjacent duplicates. Uses the specified <paramref name="equalityComparer"/>.</para>
    ///// </summary>
    ///// <param name="equalityComparer"></param>
    ///// <returns></returns>
    //public SpanBuilder<T> NormalizeDuplicates(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  if (Length >= 2)
    //  {
    //    equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //    var removeIndex = m_head + 1;

    //    for (var index = m_head + 1; index < m_tail; index++)
    //      if (!equalityComparer.Equals(m_array[removeIndex - 1], m_array[index]))
    //        m_array[removeIndex++] = m_array[index];

    //    m_tail = removeIndex;
    //  }

    //  return this;
    //}

    ///// <summary>
    ///// <para>Normalize any one or more consecutive elements satisfying the <paramref name="predicate"/> to a single <paramref name="replacement"/> element.</para>
    ///// <para><c>"".Normalize(char.IsWhiteSpace, ' ');</c></para>
    ///// <para><c>"".Normalize(c => c == ' ', ' ');</c></para>
    ///// </summary>
    ///// <remarks>Normalizing (here) means removing leading/trailing (i.e. on either end) and replacing one or more consecutive characters satisfying the <paramref name="predicate"/> within the <see cref="SpanBuilder{T}"/> to one instance of <paramref name="replacement"/>. No replacements, i.e. only removals, are performed if elements are on left or right edge.</remarks>
    //public SpanBuilder<T> NormalizeReplace(System.Func<T, bool> predicate, T replacement)
    //{
    //  System.ArgumentNullException.ThrowIfNull(predicate);

    //  var normalizedMark = m_head;

    //  var isPrevious = true;

    //  for (var mark = m_head; mark < m_tail; mark++)
    //  {
    //    var item = m_array[mark];

    //    var isCurrent = predicate(item);

    //    if (!(isPrevious && isCurrent))
    //    {
    //      m_array[normalizedMark++] = isCurrent ? replacement : item;

    //      isPrevious = isCurrent;
    //    }
    //  }

    //  if (isPrevious) normalizedMark--;

    //  m_tail = normalizedMark;

    //  return this;
    //}

    /// <summary>
    /// <para>Normalize all consecutive character instances satisfying the <paramref name="predicate"/> to <paramref name="maxConsecutiveLength"/>.</para>
    /// </summary>
    public SpanBuilder<T> NormalizeConsecutive(int maxConsecutiveLength, System.Func<T, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      if (maxConsecutiveLength < 1)
        throw new System.ArgumentOutOfRangeException(nameof(maxConsecutiveLength));

      var newtail = m_head;
      var isPrevious = false;
      var consecutiveLength = 1;

      for (var index = m_head; index < m_tail; index++)
      {
        var c = m_array[index];

        var isCurrent = predicate(c);

        var nonAdjacent = !(isCurrent && isPrevious);

        if (nonAdjacent || consecutiveLength < maxConsecutiveLength)
        {
          m_array[newtail++] = c;

          isPrevious = isCurrent;
        }

        if (nonAdjacent) consecutiveLength = 1;
        else consecutiveLength++;
      }

      m_tail = newtail;

      return this;
    }

    /// <summary>
    /// <para>Normalize all consecutive character instances of the specified <paramref name="itemsToNormalize"/> to <paramref name="maxConsecutiveLength"/>.</para>
    /// </summary>
    public SpanBuilder<T> NormalizeConsecutive(int maxConsecutiveLength, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] itemsToNormalize)
      => NormalizeConsecutive(maxConsecutiveLength, c => itemsToNormalize is null || itemsToNormalize.Length == 0 || itemsToNormalize.Contains(c, equalityComparer));

    #endregion

    #region Pad..

    /// <summary>
    /// <para>Pad evenly on both sides (with <paramref name="leftBias"/> if true, or right-bias if false) using <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> to the specified <paramref name="totalWidth"/> in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="paddingLeft"></param>
    /// <param name="paddingRight"></param>
    /// <param name="leftBias"></param>
    /// <returns></returns>
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

    /// <summary>
    /// <para>Pad evenly on both sides (with <paramref name="leftBias"/> if true, or right-bias if false) using <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> to the specified <paramref name="totalWidth"/> in the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="paddingLeft"></param>
    /// <param name="paddingRight"></param>
    /// <param name="leftBias"></param>
    /// <returns></returns>
    public SpanBuilder<T> PadEven(int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true)
      => PadEven(totalWidth, [paddingLeft], [paddingRight], leftBias);

    /// <summary>
    /// <para>Pad on the left using <paramref name="padding"/> to the specified <paramref name="totalWidth"/> in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public SpanBuilder<T> PadLeft(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      if (Length < totalWidth)
      {
        Prepend(padding, (totalWidth - Length) / padding.Length + 1);

        if (Length > totalWidth)
          RemoveLeft(Length - totalWidth);
      }

      return this;
    }

    /// <summary>
    /// <para>Pad on the left using <paramref name="padding"/> to the specified <paramref name="totalWidth"/> in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public SpanBuilder<T> PadLeft(int totalWidth, T padding)
    {
      Prepend(padding, totalWidth - Length);

      return this;
    }

    /// <summary>
    /// <para>Pad on the right using <paramref name="padding"/> to the specified <paramref name="totalWidth"/> in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public SpanBuilder<T> PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      if (Length < totalWidth)
      {
        Append(padding, (totalWidth - Length) / padding.Length + 1);

        if (Length > totalWidth)
          RemoveRight(Length - totalWidth);
      }

      return this;
    }

    /// <summary>
    /// <para>Pad on the right using <paramref name="padding"/> to the specified <paramref name="totalWidth"/> in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="totalWidth"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public SpanBuilder<T> PadRight(int totalWidth, T padding)
    {
      Append(padding, totalWidth - Length);

      return this;
    }

    #endregion

    #region Prepend

    public SpanBuilder<T> Prepend(int length)
    {
      var prependArrayIndex = EnsureCapacityPrepend(length);

      System.Array.Clear(m_array, prependArrayIndex, length);

      return this;
    }

    public SpanBuilder<T> Prepend(T item, int count = 1)
    {
      var prependArrayIndex = EnsureCapacityPrepend(count);

      while (count-- > 0)
        m_array[prependArrayIndex++] = item;

      return this;
    }

    /// <summary>
    /// <para>Prepends <paramref name="collection"/>, <paramref name="count"/> times, to the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanBuilder<T> Prepend(System.Collections.Generic.ICollection<T> collection, int count = 1)
    {
      var prependArrayIndex = EnsureCapacityPrepend(count * collection.Count);

      while (count-- > 0)
      {
        collection.CopyTo(m_array, prependArrayIndex);

        prependArrayIndex += collection.Count;
      }

      return this;
    }

    /// <summary>
    /// <para>Prepends <paramref name="span"/>, <paramref name="count"/> times, to the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="span"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public SpanBuilder<T> Prepend(System.ReadOnlySpan<T> span, int count = 1)
    {
      var prependArrayIndex = EnsureCapacityPrepend(count * span.Length);

      while (count-- > 0)
      {
        span.CopyTo(m_array.AsSpan().Slice(prependArrayIndex, span.Length));

        prependArrayIndex += span.Length;
      }

      return this;
    }

    #endregion // Prepend

    #region Remove

    /// <summary>
    /// <para>Removes the specified range [index..(index + length)] of elements in the <see cref="SpanBuilder{T}">.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public SpanBuilder<T> Remove(int index, int length)
    {
      System.Range.AssertInRange(index, length, Length);

      if (index == 0)
        return RemoveLeft(length);

      if (index + length == Length)
        return RemoveRight(length);

      var removeIndex = m_head + index;

      if (m_head <= m_array.Length - m_tail) // If there is less room at the head, shrink from the head.
      {
        System.Array.Copy(m_array, m_head, m_array, m_head + length, removeIndex - m_head);

        m_head += length;
      }
      else // Otherwise there is less room at the tail, so shrink from the tail.
      {
        System.Array.Copy(m_array, removeIndex + length, m_array, removeIndex, m_tail - removeIndex - length);

        m_tail -= length;
      }

      return this;
    }

    /// <summary>
    /// <para>Removes the specified <see cref="System.Range"> of elements in the <see cref="SpanBuilder{T}">.</para>
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public SpanBuilder<T> Remove(Range range)
    {
      var (offset, length) = range.GetOffsetAndLength(Length);

      return Remove(offset, length);
    }

    /// <summary>
    /// <para>Remove all elements satisfying the <paramref name="predicate"/> from the <see cref="SpanBuilder{T}">.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public SpanBuilder<T> Remove(System.Func<T, int, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      var removeIndex = m_head; // This represents a "lag" index, and becomes the new m_tail in the end.

      for (var index = m_head; index < m_tail; index++)
      {
        var t = m_array[index];

        if (!predicate(t, index))
          m_array[removeIndex++] = t;
      }

      m_tail = removeIndex;

      return this;
    }

    /// <summary>
    /// <para>Removes <paramref name="count"/> elements on the left (at the beginning) of a <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public SpanBuilder<T> RemoveLeft(int count)
    {
      System.Index.AssertInRange(count - 1, Length);

      m_head += count;

      return this;
    }

    /// <summary>
    /// <para>Removes <paramref name="count"/> elements on the right (at the end) of a <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public SpanBuilder<T> RemoveRight(int count)
    {
      System.Index.AssertInRange(count - 1, Length);

      m_tail -= count;

      return this;
    }

    #endregion

    #region Replace

    /// <summary>
    /// <para>Replaces <paramref name="length"/> elements from <paramref name="index"/> with <paramref name="replacement"/> in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public SpanBuilder<T> Replace(int index, int length, System.ReadOnlySpan<T> replacement)
    {
      System.Range.AssertInRange(index, length, Length);

      if (replacement.Length > length) // If the replacement is longer than what it replaces, ensure capacity.
        EnsureCapacityInsert(index, replacement.Length - length);

      replacement.CopyTo(m_array.AsSpan(m_head + index, replacement.Length)); // Always insert replacement.

      if (replacement.Length < length) // If replacement is shorter, remove the remainder.
        Remove(index + replacement.Length, length - replacement.Length);

      return this;
    }

    /// <summary>
    /// <para>Replaces the <paramref name="slice"/> with <paramref name="replacement"/> in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="slice"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public SpanBuilder<T> Replace(System.Range range, System.ReadOnlySpan<T> replacement)
    {
      var (offset, length) = range.GetOffsetAndLength(Length);

      return Replace(offset, length, replacement);
    }

    /// <summary>
    /// <para>Replaces all elements satisfying a <paramref name="predicate"/> with the result from a <paramref name="replacementSelector"/> in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="replacementSelector"></param>
    /// <returns></returns>
    public SpanBuilder<T> Replace(System.Func<T, int, bool> predicate, System.Func<T, T> replacementSelector)
    {
      var index = 0;

      for (var mark = m_head; mark < m_tail; mark++)
        if (m_array[mark] is var c && predicate(c, index++))
          m_array[mark] = replacementSelector(c);

      return this;
    }

    /// <summary>
    /// <para>Replaces all elements satisfying a <paramref name="predicate"/> with the result from a <paramref name="replacementSelector"/> in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="replacementSelector"></param>
    /// <returns></returns>
    public SpanBuilder<T> Replace(System.Func<T, bool> predicate, System.Func<T, T> replacementSelector)
      => Replace((e, i) => predicate(e), replacementSelector);

    /// <summary>
    /// <para>Replaces all elements satisfying a <paramref name="predicate"/> with a <paramref name="replacement"/> in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public SpanBuilder<T> Replace(System.Func<T, int, bool> predicate, System.ReadOnlySpan<T> replacement)
    {
      for (var index = Length - 1; index >= 0; index--)
        if (predicate(m_array[m_head + index], index))
          Replace(index, 1, replacement);

      return this;
    }

    /// <summary>
    /// <para>Replaces all elements satisfying a <paramref name="predicate"/> with a <paramref name="replacement"/> in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public SpanBuilder<T> Replace(System.Func<T, bool> predicate, System.ReadOnlySpan<T> replacement)
      => Replace((e, i) => predicate(e), replacement);

    #endregion

    #region Reverse

    /// <summary>
    /// <para>Reverses the content of a <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public SpanBuilder<T> Reverse(int index, int length)
    {
      System.Range.AssertInRange(index, length, Length);

      var left = index;
      var right = index + length;

      while (left < right)
        Swap(left++, --right);

      return this;
    }

    public SpanBuilder<T> Reverse(System.Range range)
    {
      var (offset, length) = range.GetOffsetAndLength(Length);

      return Reverse(offset, length);
    }

    #endregion

    #region Slice

    public SpanBuilder<T> Slice(int index, int length)
    {
      System.Range.AssertInRange(index, length, Length);

      return new SpanBuilder<T>(AsReadOnlySpan().Slice(index, length));
    }

    public SpanBuilder<T> Slice(System.Range range)
    {
      var (offset, length) = range.GetOffsetAndLength(Length);

      return Slice(offset, length);
    }

    #endregion

    #region Swap..

    /// <summary>
    /// <para>Swaps two elements at the specified [<paramref name="indexA"/>] and [<paramref name="indexB"/>] in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    /// <returns></returns>
    public bool Swap(int indexA, int indexB)
      => AsSpan().Swap(indexA, indexB);

    /// <summary>
    /// <para>Swaps two elements at the specified [<paramref name="index"/>] and [0] in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool SwapWithFirst(int index)
      => AsSpan().Swap(index, 0);

    /// <summary>
    /// <para>Swaps two elements at the specified [<paramref name="index"/>] and [<see cref="Length"/> - 1] in the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool SwapWithLast(int index)
      => AsSpan().Swap(index, Length - 1);

    #endregion

    #region Trim..

    /// <summary>
    /// <para>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the beginning of the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public SpanBuilder<T> TrimCommonPrefix(System.Func<T, int, bool> predicate, int maxTrimLength = int.MaxValue)
      => RemoveLeft(AsReadOnlySpan().CommonPrefixLength(predicate, maxTrimLength));

    /// <summary>
    /// <para>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the beginning of the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public SpanBuilder<T> TrimCommonPrefix(System.Func<T, bool> predicate, int maxTrimLength = int.MaxValue)
      => RemoveLeft(AsReadOnlySpan().CommonPrefixLength(predicate, maxTrimLength));

    /// <summary>
    /// <para>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the end of the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public SpanBuilder<T> TrimCommonSuffix(System.Func<T, int, bool> predicate, int maxTrimLength = int.MaxValue)
      => RemoveRight(AsReadOnlySpan().CommonSuffixLength(predicate, maxTrimLength));

    /// <summary>
    /// <para>Trims all consecutive occurences that satisfies the <paramref name="predicate"/> at the end of the <see cref="SpanBuilder{T}"/>.</para>
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public SpanBuilder<T> TrimCommonSuffix(System.Func<T, bool> predicate, int maxTrimLength = int.MaxValue)
      => RemoveRight(AsReadOnlySpan().CommonSuffixLength(predicate, maxTrimLength));

    #endregion Trim methods

    #region Implemented interfaces

    public System.Collections.Generic.IEnumerator<T> GetEnumerator() => AsReadOnlySpan().ToArray().AsEnumerable().GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

    public string ToString(int index)
      => AsReadOnlySpan().Slice(index).ToString();

    public string ToString(int index, int length)
      => AsReadOnlySpan().Slice(index, length).ToString();

    #region Implemented overides

    public override string ToString()
      => AsReadOnlySpan().ToString();

    #endregion
  }
}
