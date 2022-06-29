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

  public ref partial struct SpanBuilder<T>
    where T : notnull
  {
    private System.Span<T> m_buffer;
    private int m_bufferPosition;

    public SpanBuilder(int capacity)
    {
      m_buffer = System.Buffers.ArrayPool<T>.Shared.Rent(capacity);
      m_bufferPosition = 0;
    }
    public SpanBuilder(System.ReadOnlySpan<T> value)
      : this(System.Convert.ToInt32(System.Math.Pow(2, System.Convert.ToInt32(System.Math.Log(value.Length - 1, 2)) + 1)))
    {
      value.CopyTo(m_buffer);
      m_bufferPosition = value.Length;
    }
    public SpanBuilder(System.Span<T> value)
      : this(System.Convert.ToInt32(System.Math.Pow(2, System.Convert.ToInt32(System.Math.Log(value.Length - 1, 2)) + 1)))
    {
      value.CopyTo(m_buffer);
      m_bufferPosition = value.Length;
    }
    public SpanBuilder(T[] value)
      : this(System.Convert.ToInt32(System.Math.Pow(2, System.Convert.ToInt32(System.Math.Log(value.Length - 1, 2)) + 1)))
    {
      value.CopyTo(m_buffer);
      m_bufferPosition = value.Length;
    }
    public SpanBuilder()
      : this(32)
    {
    }

    /// <summary>The current capacity of the SpanBuilder.</summary>
    public int Capacity
      => m_buffer.Length;

    /// <summary>The length of the current content of the SpanBuilder.</summary>
    public int Length
      => m_bufferPosition;

    /// <summary>Gets or sets the item at the specified item position in this instance.</summary>
    public ref T this[int index]
      => ref m_buffer[index];

    /// <summary>Adds the value to this instance.</summary>
    public void Append(T value, int count = 1)
    {
      EnsureCapacity(m_bufferPosition + count);

      while (count-- > 0)
        m_buffer[m_bufferPosition++] = value;
    }
    /// <summary>Adds the sequence of items to this instance.</summary>
    public void Append(ReadOnlySpan<T> value)
    {
      if (m_bufferPosition + value.Length is var needed && needed > m_buffer.Length) EnsureCapacity(needed * 2);

      value.CopyTo(m_buffer[m_bufferPosition..]);

      m_bufferPosition += value.Length;
    }

    public System.ReadOnlySpan<T> AsReadOnlySpan()
      => m_buffer[..m_bufferPosition];
    public System.Span<T> AsSpan()
      => m_buffer[..m_bufferPosition];

    private void Cleanup()
      => m_buffer.Slice(m_bufferPosition).Clear();

    /// <summary>Removes all items in this instance.</summary>
    public void Clear()
      => m_bufferPosition = 0;

    public SpanBuilder<T> Clone()
      => new SpanBuilder<T>(m_buffer.Slice(0, m_bufferPosition));

    //public static SpanBuilder<T> Create(T[] value)
    //  => new SpanBuilder<T> { m_buffer = value, m_bufferPosition = value.Length };

    /// <summary>Grows the buffer capacity to at least that specified.</summary>
    private int EnsureCapacity(int capacity = 0)
    {
      var realCapacity = m_buffer.Length;

      if (capacity > realCapacity)
      {
        if (realCapacity == 0)
          realCapacity = 32;

        while (capacity > realCapacity)
          realCapacity <<= 1;

        //var newCapacity = capacity > m_buffer.Length ? capacity : m_buffer.Length * 2;

        var rented = System.Buffers.ArrayPool<T>.Shared.Rent(realCapacity);
        m_buffer.CopyTo(rented);
        m_buffer = rented;
        System.Buffers.ArrayPool<T>.Shared.Return(rented);
      }

      return realCapacity;
    }

    /// <summary>Inserts the value into this instance at the specified index.</summary>
    public void Insert(int index, T value, int count = 1)
    {
      if (index < 0 || index > m_bufferPosition) throw new ArgumentOutOfRangeException(nameof(index));

      EnsureCapacity(m_bufferPosition + count);

      var moveIndex = index + count;

      m_buffer[index..m_bufferPosition].CopyTo(m_buffer[moveIndex..]); // Move right side of old content.

      m_bufferPosition += count; // Update the position (length) before copying data (count is altered below).

      while (count-- > 0)
        m_buffer[index++] = value;
    }
    /// <summary>Inserts the sequence of items into this instance at the specified index.</summary>
    public void Insert(int index, System.ReadOnlySpan<T> value)
    {
      if (index < 0 || index > m_bufferPosition) throw new ArgumentOutOfRangeException(nameof(index));

      if (m_bufferPosition + value.Length is var needed && needed > m_buffer.Length)
        EnsureCapacity(needed);

      var endIndex = index + value.Length;

      m_buffer[index..m_bufferPosition].CopyTo(m_buffer[endIndex..]); // Move right side of old content.

      value.CopyTo(m_buffer[index..endIndex]); // Insert new content in buffer gap.

      m_bufferPosition += value.Length;
    }

    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the specfied comparer.</summary>
    public void NormalizeAdjacent(System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var targetIndex = 0;
      var previous = default(T);

      for (var sourceIndex = 0; sourceIndex < m_bufferPosition; sourceIndex++)
      {
        var current = m_buffer[sourceIndex];

        if (!equalityComparer.Equals(current, previous) || (values.Count > 0 && !values.Contains(current, equalityComparer)))
        {
          m_buffer[targetIndex++] = current;

          previous = current;
        }
      }

      m_bufferPosition = targetIndex;

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

      for (var sourceIndex = 0; sourceIndex < m_bufferPosition; sourceIndex++)
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

      m_bufferPosition = normalizedIndex;

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

    /// <summary>Removes the specified range of items from this instance.</summary>
    public void Remove(int startIndex, int length)
    {
      if (startIndex < 0 || startIndex >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (length < 0 || startIndex + length is var endIndex && endIndex > m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(length));

      m_buffer[(startIndex + length)..m_bufferPosition].CopyTo(m_buffer[startIndex..]);

      m_bufferPosition -= length;

      m_buffer.Slice(m_bufferPosition, length).Clear();

      Cleanup();
    }

    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed.</summary>
    public void RemoveAll(System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var removedIndex = 0;

      for (var sourceIndex = 0; sourceIndex < m_bufferPosition; sourceIndex++)
      {
        var sourceValue = m_buffer[sourceIndex];

        if (!predicate(sourceValue))
          m_buffer[removedIndex++] = sourceValue;
      }

      m_bufferPosition = removedIndex;

      Cleanup();
    }
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the specified comparer.</summary>
    public void RemoveAll([System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> remove)
      => RemoveAll(t => remove.Contains(t, equalityComparer));
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the default comparer.</summary>
    public void RemoveAll(System.Collections.Generic.IList<T> remove)
      => RemoveAll(remove.Contains);

    /// <summary>Returns the source replicated (copied) the specified number of times.</summary>
    public void Repeat(int count)
    {
      var slice = AsReadOnlySpan();

      while (count-- > 0)
        Insert(m_bufferPosition, slice);
    }

    /// <summary>Replace (in-place) all characters using the specified replacement selector function.</summary>
    public void ReplaceAll(System.Func<T, T> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = m_bufferPosition - 1; index >= 0; index--)
        m_buffer[index] = replacementSelector(m_buffer[index]);
    }
    /// <summary>Replace (in-place) all characters satisfying the predicate with the specified character.</summary>
    public void ReplaceAll(T replacement, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = m_bufferPosition - 1; index >= 0; index--)
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
      if (startIndex < 0 || startIndex >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (endIndex < startIndex || endIndex >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

      while (startIndex < endIndex)
        SwapImpl(startIndex++, endIndex--);
    }
    /// <summary>Reverse all characters within the span builder.</summary>
    public void Reverse()
      => Reverse(0, m_bufferPosition - 1);

    /// <summary>Returns a shuffled (randomized) sequence. Uses the specified Random.</summary>
    public void Shuffle(System.Random random)
    {
      if (random is null) throw new System.ArgumentNullException(nameof(random));

      for (var index = m_bufferPosition - 1; index > 0; index--) // Shuffle each element by swapping with a random element of a lower index.
        Swap(index, random.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.
    }
    /// <summary>Returns a shuffled (randomized) sequence. Uses the cryptographic Random.</summary>
    public void Shuffle()
      => Shuffle(Randomization.NumberGenerator.Crypto);

    /// <summary>Swap two elements by the specified indices.</summary>
    internal void SwapImpl(int indexA, int indexB)
    {
      if (indexA != indexB)
        (m_buffer[indexB], m_buffer[indexA]) = (m_buffer[indexA], m_buffer[indexB]);
    }
    /// <summary>Swap two elements by the specified indices.</summary>
    public void Swap(int indexA, int indexB)
    {
      if (m_bufferPosition == 0)
        throw new System.ArgumentException(@"The span builder is empty.");
      else if (indexA < 0 || indexA >= Length)
        throw new System.ArgumentOutOfRangeException(nameof(indexA));
      else if (indexB < 0 || indexB >= Length)
        throw new System.ArgumentOutOfRangeException(nameof(indexB));
      else
        SwapImpl(indexA, indexB);
    }

    public void SwapFirstWith(int index)
      => SwapImpl(0, index);

    public void SwapLastWith(int index)
      => SwapImpl(index, m_bufferPosition - 1);

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
