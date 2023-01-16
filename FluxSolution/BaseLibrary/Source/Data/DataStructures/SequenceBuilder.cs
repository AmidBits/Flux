namespace Flux
{
  #region Extension methods.
  public static partial class ExtensionMethods
  {
    public static void AppendLine(this SequenceBuilder<char> source, System.ReadOnlySpan<char> value)
    {
      source.Append(value);
      source.Append(System.Environment.NewLine);
    }

    public static void InsertLine(this SequenceBuilder<char> source, int index, System.ReadOnlySpan<char> value)
    {
      source.Insert(index, value);
      source.Insert(index + value.Length, System.Environment.NewLine);
    }

    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.), if the predicate is satisfied.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
    public static void InsertOrdinalIndicatorSuffix(this SequenceBuilder<char> source, System.Func<string, string, bool> predicate)
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

          if (predicate(source.AsReadOnlySpan().ToString(0, index + 1), suffix))
            source.Insert(index + 1, suffix);
        }

        wasDigit = isDigit;
      }
    }
    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    public static void InsertOrdinalIndicatorSuffix(this SequenceBuilder<char> source)
      => InsertOrdinalIndicatorSuffix(source, (s1, s2) => true);

    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static void JoinToCamelCase(this SequenceBuilder<char> source, System.Func<char, bool> predicate)
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
    public static void JoinToCamelCase(this SequenceBuilder<char> source)
      => JoinToCamelCase(source, char.IsWhiteSpace);

    public static void MakeNumbersFixedLength(this SequenceBuilder<char> source, int length)
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
    public static void ReplaceDiacriticalLatinStrokes(this SequenceBuilder<char> source)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = (char)((System.Text.Rune)source[index]).ReplaceDiacriticalLatinStroke().Value;
    }

    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static void SplitFromCamelCase(this SequenceBuilder<char> source)
    {
      for (var index = source.Length - 1; index > 0; index--)
        if (char.IsUpper(source[index]) && (!char.IsUpper(source[index - 1]) || char.IsLower(source[index + 1])))
          source.Insert(index, ' ');
    }

    /// <summary>Creates a new builder from the source.</summary>
    public static SequenceBuilder<T> ToSequenceBuilder<T>(this System.ReadOnlySpan<T> source)
      => new(source);

    /// <summary>Creates a new builder from the source.</summary>
    public static SequenceBuilder<T> ToSequenceBuilder<T>(this System.Span<T> source)
      => new(source);

    public static SequenceBuilder<char> ToSequenceBuilderOfChar(this SequenceBuilder<System.Text.Rune> source)
    {
      var target = new SequenceBuilder<char>();

      for (var index = 0; index < source.Length; index++)
        target.Append(source[index].ToString());

      return target;
    }

    public static SequenceBuilder<System.Text.Rune> ToSequenceBuilderOfRune(this SequenceBuilder<char> source)
    {
      var target = new SequenceBuilder<System.Text.Rune>();

      foreach (var rune in source.AsReadOnlySpan().EnumerateRunes())
        target.Append(rune);

      return target;
    }

    public static string ToString(this SequenceBuilder<System.Text.Rune> source, int startAt, int count)
      => source.AsReadOnlySpan().ToString(startAt, count);

    public static string ToString(this SequenceBuilder<char> source, int startAt, int count)
      => source.AsReadOnlySpan().ToString(startAt, count);
  }
  #endregion Extension methods.

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

    public SequenceBuilder(System.ReadOnlySpan<T> collection) : this(collection.Length) => Append(collection);

    public SequenceBuilder(System.Span<T> collection) : this(collection.Length) => Append(collection);

    public SequenceBuilder() : this(DefaultBufferSize) { }

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
    public T this[int index]
    { get => GetValue(index); set => SetValue(index, value); }

    /// <summary>The current capacity of the builder.</summary>
    public int Capacity
      => m_buffer.Length;

    /// <summary>The content length of the builder.</summary>
    public int Length
      => m_tail - m_head;

    /// <summary>Appends the value to the builder.</summary>
    public SequenceBuilder<T> Append(T value, int count = 1)
    {
      m_version++;

      EnsureAppendSpace(count);

      while (count-- > 0)
        m_buffer[m_tail++] = value;

      return this;
    }

    /// <summary>Appends the values to the builder.</summary>
    public SequenceBuilder<T> Append(System.ReadOnlySpan<T> value)
    {
      m_version++;

      EnsureAppendSpace(value.Length);

      value.CopyTo(m_buffer, m_tail);

      m_tail += value.Length;

      return this;
    }

    /// <summary>Returns a non-allocating readonly span.</summary>
    public System.ReadOnlySpan<T> AsReadOnlySpan()
      => new(m_buffer, m_head, m_tail - m_head);

    /// <summary>Returns a non-allocating span.</summary>
    public System.Span<T> AsSpan()
      => new(m_buffer, m_head, m_tail - m_head);

    /// <summary>Removes all items from the builder.</summary>
    public void Clear()
    {
      m_version++;

      System.Array.Clear(m_buffer);

      m_head = m_buffer.Length / 2;
      m_tail = m_buffer.Length / 2;
    }

    /// <summary>Gets the value at the specified index.</summary>
    public T GetValue(int index)
    {
      if (index < 0 || index >= Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      return m_buffer[m_head + index];
    }

    /// <summary>Inserts the value at the specified index of the builder.</summary>
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

    /// <summary>Inserts the values at the specified index of the builder.</summary>
    public SequenceBuilder<T> Insert(int startAt, System.ReadOnlySpan<T> value)
    {
      m_version++;

      EnsureUniformSpace(value.Length);

      startAt += m_head;

      if (m_head >= m_buffer.Length - m_tail) // Grow from start.
      {
        System.Array.Copy(m_buffer, m_head, m_buffer, m_head - value.Length, startAt - m_head);
        value.CopyTo(m_buffer, startAt - value.Length);
        m_head -= value.Length;
      }
      else // Otherwise grow from end.
      {
        System.Array.Copy(m_buffer, startAt, m_buffer, startAt + value.Length, m_tail - startAt);
        value.CopyTo(m_buffer, startAt);
        m_tail += value.Length;
      }

      return this;
    }

    /// <summary>Creates a new readonlyspan with the specified (or all if none specified) consecutive characters in the string normalized. Uses the specfied comparer.</summary>
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

    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public SequenceBuilder<T> NormalizeAll(T normalizeWith, System.Func<T, bool> predicate)
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
          SetValue(normalizedIndex++, isCurrent ? normalizeWith : character);

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      return Remove(normalizedIndex, Length - normalizedIndex);
    }

    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public SequenceBuilder<T> NormalizeAll(T normalizeWith, System.Collections.Generic.IList<T> normalize, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return NormalizeAll(normalizeWith, t => normalize.Contains(t, equalityComparer));
    }

    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
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

    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding strings for left and right respectively.</summary>
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

    /// <summary>Pads this StringBuilder on the left with the specified padding character.</summary>
    public SequenceBuilder<T> PadLeft(int totalWidth, T padding)
    {
      if (Length < totalWidth)
        Insert(0, padding, totalWidth - Length);

      return this;
    }

    /// <summary>Pads this StringBuilder on the left with the specified padding string.</summary>
    public SequenceBuilder<T> PadLeft(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Insert(0, padding);

      return Remove(0, Length - totalWidth);
    }

    /// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
    public SequenceBuilder<T> PadRight(int totalWidth, T padding)
      => Append(padding, totalWidth - Length);

    /// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
    public SequenceBuilder<T> PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (Length < totalWidth)
        Append(padding);

      return Remove(totalWidth, Length - totalWidth);
    }

    /// <summary>Inserts the value at the start of the builder.</summary>
    public SequenceBuilder<T> Prepend(T value)
    {
      m_version++;

      EnsurePrependSpace(1);

      m_buffer[--m_head] = value;

      return this;
    }

    /// <summary>Inserts the values at the start of the builder.</summary>
    public SequenceBuilder<T> Prepend(System.ReadOnlySpan<T> value)
    {
      m_version++;

      EnsurePrependSpace(value.Length);

      value.CopyTo(m_buffer, m_head -= value.Length);

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

    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed.</summary>
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

    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the specified comparer.</summary>
    public SequenceBuilder<T> RemoveAll(System.Collections.Generic.IList<T> remove, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return RemoveAll(t => remove.Contains(t, equalityComparer));
    }

    /// <summary>Repears the content of the  the specified number of times.</summary>
    public SequenceBuilder<T> Repeat(int count)
    {
      var original = AsReadOnlySpan().ToArray();

      while (count-- > 0)
        Append(original);

      return this;
    }

    /// <summary>Replace (in-place) all characters using the specified replacement selector function.</summary>
    public SequenceBuilder<T> ReplaceAll(System.Func<T, T> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = Length - 1; index >= 0; index--)
        SetValue(index, replacementSelector(GetValue(index)));

      return this;
    }

    /// <summary>Replace (in-place) all characters satisfying the predicate with the specified character.</summary>
    public SequenceBuilder<T> ReplaceAll(T replacement, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = Length - 1; index >= 0; index--)
        if (predicate(GetValue(index)))
          SetValue(index, replacement);

      return this;
    }

    /// <summary>Replace (in-place) all specified elements with the specified element. Uses the specified comparer.</summary>
    public SequenceBuilder<T> ReplaceAll(T replacement, System.Collections.Generic.IList<T> replace, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return ReplaceAll(replacement, t => replace.Any(r => equalityComparer.Equals(r, t)));
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

    /// <summary>Shuffle all items in the builder. Uses the specified Random, or default if null.</summary>
    public SequenceBuilder<T> Shuffle(System.Random? rng)
    {
      rng ??= new System.Random();

      for (var index = Length - 1; index > 0; index--) // Shuffle each element by swapping with a random element of a lower index.
        Swap(index, rng.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.

      return this;
    }

    /// <summary>Swap two elements by the specified indices.</summary>
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

    #region Object overrides.
    public override string ToString() => AsReadOnlySpan().ToString(0);
    #endregion Object overrides.
  }
}
