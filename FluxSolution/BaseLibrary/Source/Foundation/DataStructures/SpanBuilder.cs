namespace Flux
{
  #region Extension methods.
  public static partial class ExtensionMethods
  {
    public static void AppendLine(ref this SpanBuilder<char> source, System.ReadOnlySpan<char> value)
    {
      source.Append(value);
      source.Append(System.Environment.NewLine);
    }

    public static void InsertLine(ref this SpanBuilder<char> source, int index, System.ReadOnlySpan<char> value)
    {
      source.Insert(index, value);
      source.Insert(index + value.Length, System.Environment.NewLine);
    }

    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.), if the predicate is satisfied.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
    public static void InsertOrdinalIndicatorSuffix(ref this SpanBuilder<char> source, System.Func<string, string, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var wasDigit = false;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var c = source[index];

        var isDigit = char.IsDigit(c);

        if (isDigit && !wasDigit)
        {
          var isBetweenTenAndTwenty = index > 0 && source[index - 1] == '1';

          var suffix = c switch
          {
            '1' when !isBetweenTenAndTwenty => @"st",
            '2' when !isBetweenTenAndTwenty => @"nd",
            '3' when !isBetweenTenAndTwenty => @"rd",
            _ => @"th"
          };

          if (predicate(source.ToString(0, index + 1), suffix))
            source.Insert(index + 1, suffix);
        }

        wasDigit = isDigit;
      }
    }
    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    public static void InsertOrdinalIndicatorSuffix(ref this SpanBuilder<char> source)
      => InsertOrdinalIndicatorSuffix(ref source, (s1, s2) => true);

    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static void JoinToCamelCase(ref this SpanBuilder<char> source, System.Func<char, bool> predicate)
    {
      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index]))
        {
          do { source.Remove(index, 1); }
          while (predicate(source[index]));

          if (index < source.Length)
            source[index] = char.ToUpper(source[index]);
        }
    }
    public static void JoinToCamelCase(ref this SpanBuilder<char> source)
      => JoinToCamelCase(ref source, char.IsWhiteSpace);

    public static void MakeNumbersFixedLength(ref this SpanBuilder<char> source, int length)
    {
      bool wasDigit = false;
      var digitCount = 0;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var isDigit = char.IsDigit(source[index]);

        if (!isDigit && wasDigit && digitCount < length)
          source.Insert(index + digitCount, '0', length - digitCount);
        else if (isDigit && !wasDigit)
          digitCount = 1;
        else
          digitCount++;

        wasDigit = isDigit;
      }

      if (wasDigit) source.Insert(0, '0', length - digitCount);
    }

    /// <summary>Create a new char array with all diacritical (latin) strokes, which are not covered by the normalization forms in NET, replaced. Can be done simplistically because the diacritical latin stroke characters (and replacements) all fit in a single char.</summary>
    public static void ReplaceDiacriticalLatinStrokes(ref this SpanBuilder<char> source)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = (char)((System.Text.Rune)source[index]).ReplaceDiacriticalLatinStroke().Value;
    }

    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static void SplitFromCamelCase(ref this SpanBuilder<char> source)
    {
      for (var index = source.Length - 1; index > 0; index--)
        if (char.IsUpper(source[index]) && (!char.IsUpper(source[index - 1]) || char.IsLower(source[index + 1])))
          source.Insert(index, ' ');
    }

    public static SpanBuilder<char> ToCharSpanBuilder(this ref SpanBuilder<System.Text.Rune> source)
    {
      var target = new SpanBuilder<char>();

      for (var index = 0; index < source.Length; index++)
        target.Append(source[index].ToString());

      return target;
    }

    //public static SpanBuilder<System.Text.Rune> ToRuneBuilder(ref this SpanBuilder<char> source)
    //{
    //  var runes = new SpanBuilder<System.Text.Rune>();

    //  for (var index = 0; index < source.Length; index++)
    //  {
    //    var c1 = source[index];

    //    if (char.IsHighSurrogate(c1))
    //    {
    //      var c2 = source[++index];

    //      if (!char.IsLowSurrogate(c2))
    //        throw new System.InvalidOperationException(@"Missing low surrogate (required after high surrogate).");

    //      runes.Append(new System.Text.Rune(c1, c2));
    //    }
    //    else if (char.IsLowSurrogate(c1))
    //      throw new System.InvalidOperationException(@"Unexpected low surrogate (only allowed after high surrogate).");
    //    else
    //      runes.Append(new System.Text.Rune(c1));
    //  }

    //  return runes;
    //}

    public static SpanBuilder<System.Text.Rune> ToRuneSpanBuilder(this ref SpanBuilder<char> source)
    {
      var target = new SpanBuilder<System.Text.Rune>();

      foreach (var rune in source.AsReadOnlySpan().EnumerateRunes())
        target.Append(rune);

      return target;
    }

    public static string ToString(ref this SpanBuilder<char> source, int startIndex, int length)
      => source.AsReadOnlySpan().Slice(startIndex, length).ToString();
    public static string ToString(ref this SpanBuilder<char> source, int startIndex)
      => source.ToString(startIndex, source.Length - startIndex);
  }
  #endregion Extension methods.

  public ref struct SpanBuilder<T>
    where T : notnull
  {
    private const int DefaultBufferSize = 32;

    private System.Span<T> m_buffer;

    private int m_position;

    public SpanBuilder(int capacity)
    {
      var powerOf2Capacity = BitOps.FoldRight(capacity) + 1;

      m_buffer = System.Buffers.ArrayPool<T>.Shared.Rent(powerOf2Capacity);

      m_position = 0;
    }
    public SpanBuilder(System.ReadOnlySpan<T> value)
      : this(value.Length)
      => Append(value);
    public SpanBuilder(System.Span<T> value)
      : this(value.Length)
      => Append(value);
    public SpanBuilder()
      : this(32)
    {
    }

    /// <summary>Gets or sets the item at the specified item position in this instance.</summary>
    public ref T this[int index]
      => ref m_buffer[index];

    /// <summary>The current capacity of the builder.</summary>
    public int Capacity
      => m_buffer.Length;

    /// <summary>The content length of the builder.</summary>
    public int Length
      => m_position;

    //private int EnsureFreeCapacity(int freeCapacity)
    //{
    //  if (Capacity - Length < freeCapacity)
    //  {
    //    var capacity = BitOps.FoldRight(DefaultBufferSize + Capacity + freeCapacity) + 1;

    //    var rented = System.Buffers.ArrayPool<T>.Shared.Rent(capacity);
    //    System.Array.Clear(rented);
    //    System.Array.Copy(m_buffer, rented, m_position);
    //    System.Buffers.ArrayPool<T>.Shared.Return(m_buffer);
    //    m_buffer = rented;
    //  }
    //}

    /// <summary>Grows the buffer capacity to at least that specified.</summary>
    private int EnsureCapacity(int capacity = 0)
    {
      var realCapacity = m_buffer.Length;

      if (capacity > realCapacity)
      {
        realCapacity = realCapacity == 0 ? DefaultBufferSize : BitOps.FoldRight(realCapacity) + 1;

        var rented = System.Buffers.ArrayPool<T>.Shared.Rent(realCapacity);
        System.Array.Clear(rented);
        m_buffer.CopyTo(rented);
        m_buffer = rented;
        System.Buffers.ArrayPool<T>.Shared.Return(rented);
      }

      return realCapacity;
    }

    /// <summary>Clears all unused capacity.</summary>
    private void Cleanup()
      => m_buffer.Slice(m_position).Clear();

    /// <summary>Appends the value to the builder.</summary>
    public void Append(T value, int count = 1)
    {
      EnsureCapacity(m_position + count);

      while (count-- > 0)
        m_buffer[m_position++] = value;
    }
    /// <summary>Appends the values to the builder.</summary>
    public void Append(ReadOnlySpan<T> value)
    {
      EnsureCapacity(m_position + value.Length);

      value.CopyTo(m_buffer[m_position..]);

      m_position += value.Length;
    }

    /// <summary>Returns a non-allocating readonly span.</summary>
    public System.ReadOnlySpan<T> AsReadOnlySpan()
      => m_buffer[..m_position];
    /// <summary>Returns a non-allocating span.</summary>
    public System.Span<T> AsSpan()
      => m_buffer[..m_position];

    /// <summary>Removes all items from the builder.</summary>
    public void Clear()
      => m_position = 0;

    /// <summary>Creates a new builder with the same content.</summary>
    public SpanBuilder<T> Clone()
      => new(AsReadOnlySpan());

    /// <summary>Gets the value at the specified index.</summary>
    public T GetValue(int index)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      return m_buffer[index];
    }

    /// <summary>Inserts the value at the specified index of the builder.</summary>
    public void Insert(int index, T value, int count = 1)
    {
      if (index < 0 || index > m_position) throw new ArgumentOutOfRangeException(nameof(index));

      EnsureCapacity(m_position + count);

      var moveIndex = index + count;

      m_buffer[index..m_position].CopyTo(m_buffer[moveIndex..]); // Move right side of old content.

      m_position += count; // Update the position (length) before copying data (count is altered below).

      while (count-- > 0)
        m_buffer[index++] = value;
    }
    /// <summary>Inserts the values at the specified index of the builder.</summary>
    public void Insert(int index, System.ReadOnlySpan<T> value)
    {
      if (index < 0 || index > m_position) throw new ArgumentOutOfRangeException(nameof(index));

      if (m_position + value.Length is var needed && needed > m_buffer.Length)
        EnsureCapacity(needed);

      var endIndex = index + value.Length;

      m_buffer[index..m_position].CopyTo(m_buffer[endIndex..]); // Move right side of old content.

      value.CopyTo(m_buffer[index..endIndex]); // Insert new content in buffer gap.

      m_position += value.Length;
    }

    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the specfied comparer.</summary>
    public void NormalizeAdjacent(System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var targetIndex = 0;
      var previous = default(T);

      for (var sourceIndex = 0; sourceIndex < m_position; sourceIndex++)
      {
        var current = m_buffer[sourceIndex];

        if (!equalityComparer.Equals(current, previous) || (values.Count > 0 && !values.Contains(current, equalityComparer)))
        {
          m_buffer[targetIndex++] = current;

          previous = current;
        }
      }

      m_position = targetIndex;

      Cleanup();
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

      for (var sourceIndex = 0; sourceIndex < m_position; sourceIndex++)
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

      m_position = normalizedIndex;

      Cleanup();
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

    /// <summary>Inserts the value at the start of the builder.</summary>
    public void Prepend(T value)
      => Insert(0, value);
    /// <summary>Inserts the values at the start of the builder.</summary>
    public void Prepend(System.ReadOnlySpan<T> value)
      => Insert(0, value);

    /// <summary>Removes the specified range of values from the builder.</summary>
    public void Remove(int startIndex, int length)
    {
      if (startIndex < 0 || startIndex >= m_position) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (length < 0 || startIndex + length is var endIndex && endIndex > m_position) throw new System.ArgumentOutOfRangeException(nameof(length));

      m_buffer[(startIndex + length)..m_position].CopyTo(m_buffer[startIndex..]);

      m_position -= length;

      m_buffer.Slice(m_position, length).Clear();

      Cleanup();
    }

    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed.</summary>
    public void RemoveAll(System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var removedIndex = 0;

      for (var sourceIndex = 0; sourceIndex < m_position; sourceIndex++)
      {
        var sourceValue = m_buffer[sourceIndex];

        if (!predicate(sourceValue))
          m_buffer[removedIndex++] = sourceValue;
      }

      m_position = removedIndex;

      Cleanup();
    }
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the specified comparer.</summary>
    public void RemoveAll([System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> remove)
      => RemoveAll(t => remove.Contains(t, equalityComparer));
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the default comparer.</summary>
    public void RemoveAll(System.Collections.Generic.IList<T> remove)
      => RemoveAll(remove.Contains);

    /// <summary>Repears the content of the  the specified number of times.</summary>
    public void Repeat(int count)
    {
      var original = AsReadOnlySpan().ToArray();

      while (count-- > 0)
        Append(original);
    }

    /// <summary>Replace (in-place) all characters using the specified replacement selector function.</summary>
    public void ReplaceAll(System.Func<T, T> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = m_position - 1; index >= 0; index--)
        m_buffer[index] = replacementSelector(m_buffer[index]);
    }
    /// <summary>Replace (in-place) all characters satisfying the predicate with the specified character.</summary>
    public void ReplaceAll(T replacement, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = m_position - 1; index >= 0; index--)
        if (predicate(m_buffer[index]))
          m_buffer[index] = replacement;
    }
    /// <summary>Replace (in-place) all specified elements with the specified element. Uses the specified comparer.</summary>
    public void ReplaceAll(T replacement, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, params T[] replace)
      => ReplaceAll(replacement, t => System.Array.Exists(replace, e => equalityComparer.Equals(e, t)));
    /// <summary>Replace (in-place) all specified elements with the specified element. Uses the default comparer.</summary>
    public void ReplaceAll(T replacement, params T[] replace)
      => ReplaceAll(replacement, System.Collections.Generic.EqualityComparer<T>.Default, replace);

    /// <summary>Reverse all characters in the range [startIndex, endIndex] within the span builder.</summary>
    public void Reverse(int startIndex, int endIndex)
    {
      if (startIndex < 0 || startIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= Length) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      while (startIndex < endIndex)
        Swap(startIndex++, endIndex--);
    }
    /// <summary>Reverse all characters within the span builder.</summary>
    public void Reverse()
      => Reverse(0, Length - 1);

    public void SetValue(int index, T value)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      m_buffer[index] = value;
    }

    /// <summary>Returns a shuffled (randomized) sequence. Uses the specified Random.</summary>
    public void Shuffle(System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      for (var index = m_position - 1; index > 0; index--) // Shuffle each element by swapping with a random element of a lower index.
        Swap(index, rng.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.
    }

    /// <summary>Swap two elements by the specified indices.</summary>
    public void Swap(int indexA, int indexB)
    {
      if (indexA != indexB)
      {
        var a = GetValue(indexA);
        var b = GetValue(indexB);

        SetValue(indexA, b);
        SetValue(indexB, a);
      }
    }

    /// <summary>Remove the specified wrap strings from the source, if they exist.</summary>
    public void Unwrap(T left, T right)
    {
      if (AsReadOnlySpan().IsWrapped(left, right))
      {
        Remove(0, 1);
        Remove(Length - 1, 1);
      }
    }
    /// <summary>Remove the specified wrap strings from the source, if they exist.</summary>
    public void Unwrap(System.ReadOnlySpan<T> left, System.ReadOnlySpan<T> right)
    {
      if (AsReadOnlySpan().IsWrapped(left, right))
      {
        Remove(0, left.Length);
        Remove(Length - right.Length, right.Length);
      }
    }

    /// <summary>Add the specified characters to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped. E.g. in SQL brackets, or parenthesis.</summary>
    public void Wrap(T left, T right)
    {
      Insert(0, left);
      Append(right);
    }
    /// <summary>Add the specified wrap strings to the source, if they do not already exist. Change the default force to true to always wrap the source, even if it is null (which produces a wrapped empty string) or already wrapped.</summary>
    public void Wrap(System.ReadOnlySpan<T> left, System.ReadOnlySpan<T> right)
    {
      Insert(0, left);
      Append(right);
    }
  }
}
